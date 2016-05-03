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
    public class Pic
    {
        private static readonly IPic RePic = SelectData.CreatePic();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(PicInfo pic)
        {
            if (null == pic)
            {
                return false;
            }
            return RePic.Add(pic);
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

            PicInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return RePic.Delete(id);
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
            return RePic.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(PicInfo pic)
        {
            if (null == pic)
            {
                return false;
            }

            return RePic.Update(pic);
        }

        /// <summary>
        /// 增加下载次数 
        /// </summary>
        /// <param name="id">doi</param>
        /// <returns></returns>
        public bool AddStaticDownload(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return RePic.AddStaticDownload(id);
        }

        /// <summary>
        /// 点击量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddHitCount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return RePic.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PicInfo GetItem(string id)
        {
            return RePic.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<PicInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return RePic.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        public IList<PicInfo> GetList_NoFirst(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return RePic.GetList_NoFirst(strwhere, pageno, pagecount, out recordcount, IsAll);
        }



        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">图片的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            PicInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return RePic.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return RePic.GetCount(strWhere);
        }

        /// <summary>
        /// 获取图片分组ID，图片详情页面使用，用于当前分组的图片显示完后，显示下一分组（或上一分组）的图片
        /// </summary>
        /// <param name="sqlwhere">查询限制条件</param>
        /// <param name="field">分组标识</param>
        /// <returns></returns>
        public List<PicInfo> GetGroupID(string sqlwhere, string field)
        {
            return RePic.GetGroupID(sqlwhere, field);
        }
    }
}
