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
    public class DataBaseList
    {
        private static readonly IDataBaseList ReDataBaseList = SelectData.CreateDataBaseList();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(DataBaseListInfo databaselist)
        {
            if (null == databaselist)
            {
                return false;
            }
            return ReDataBaseList.Add(databaselist);
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

            DataBaseListInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReDataBaseList.Delete(id);
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
            return ReDataBaseList.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(DataBaseListInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReDataBaseList.Update(book);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataBaseListInfo GetItem(string id)
        {
            return ReDataBaseList.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<DataBaseListInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReDataBaseList.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReDataBaseList.GetCount(strWhere);
        }

        private static Dictionary<string, string> DataBaseListType;
        
        /// <summary>
        /// 获取前缀类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPrefix(string DataBaseType)
        {
            if (string.IsNullOrWhiteSpace(DataBaseType))
            {
                return string.Empty;
            }
            GetDataBaseListType();
            if (DataBaseListType != null)
            {
                if (DataBaseListType.ContainsKey(DataBaseType))
                {
                    return DataBaseListType[DataBaseType];
                }
            }

            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> GetDataBaseListType()
        {
            if (DataBaseListType == null)
            {
                DataBaseListType = new Dictionary<string, string>();
                int recordCount = 0;
                IList<DataBaseListInfo> mylist = ReDataBaseList.GetList(" order by date", 1, 1000, out recordCount, true);

                if (mylist != null)
                {
                    if (mylist.Count < recordCount)
                    {
                        mylist = ReDataBaseList.GetList("", 1, recordCount, out recordCount, true);
                    }

                    if (mylist != null)
                    {
                        for (int i = 0; i < mylist.Count; i++)
                        {
                            if (!DataBaseListType.ContainsKey(mylist[i].DatabaseType.ToString()))
                            {
                                DataBaseListType.Add(mylist[i].DatabaseType.ToString(), mylist[i].dirprefix);
                            }
                        }
                    }

                }
            }
            return DataBaseListType;
        }
    }
}
