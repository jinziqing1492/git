using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class Config
    {
        private static readonly IConfig ReConfig = SelectData.CreateConfig();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(ConfigInfo config)
        {
            if (null == config)
            {
                return false;
            }
            return ReConfig.Add(config);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            ConfigInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReConfig.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                return false;
            }
            return ReConfig.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(ConfigInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReConfig.Update(book);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConfigInfo GetItem(string id)
        {
            return ReConfig.GetItem(id);
        }

        /// <summary>
        /// 根据虚拟路径标示获取单个实体
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ConfigInfo GetItemByTag(string tag)
        {
            return ReConfig.GetItemByTag(tag);
        }

        public string GetRootDirByTag(string tag)
        {
            ConfigInfo info = ReConfig.GetItemByTag(tag);
            if (info != null)
            {
                return info.RootDir;
            }
            return "";
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<ConfigInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReConfig.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReConfig.GetCount(strWhere);
        }



        private static Dictionary<string, string> VirtualPathList;


        /// <summary>
        /// 获取虚拟目录
        /// </summary>
        /// <param name="virtalTag"></param>
        /// <returns></returns>
        public static string GetVirtalPath(string virtalTag)
        {
            if (string.IsNullOrWhiteSpace(virtalTag))
            {
                return string.Empty;
            }
            GetVirtalPathList();
            if (VirtualPathList != null)
            {
                if (VirtualPathList.ContainsKey(virtalTag))
                {
                    return VirtualPathList[virtalTag];
                }
            }

            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> GetVirtalPathList()
        {
            if (VirtualPathList == null)
            {
                VirtualPathList = new Dictionary<string, string>();
                int recordCount = 0;
                IList<ConfigInfo> mylist = ReConfig.GetList(" order by date", 1, 1, out recordCount, true);

                if (mylist != null)
                {
                    if (mylist.Count < recordCount)
                    {
                        mylist = ReConfig.GetList("", 1, recordCount, out recordCount, true);
                    }

                    if (mylist != null)
                    {
                        for (int i = 0; i < mylist.Count; i++)
                        {
                            if (!VirtualPathList.ContainsKey(mylist[i].VirtualPathTag))
                            {
                                VirtualPathList.Add(mylist[i].VirtualPathTag, mylist[i].VirtualPathName);
                                string drmKey = (CNKI.BaseFunction.StructTrans.TransNum(mylist[i].VirtualPathTag) * 100).ToString();
                                if (!VirtualPathList.ContainsKey(drmKey))
                                {
                                    VirtualPathList.Add(drmKey, "drm" + mylist[i].VirtualPathName);
                                }
                            }
                        }
                    }

                }
            }
            return VirtualPathList;
        }
    }
}
