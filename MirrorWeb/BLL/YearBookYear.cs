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
    public class YearBookYear
    {
        private static readonly IYearBookYearInfo ReYearBookYear = SelectData.CreateYearBookYearInfo();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(YearBookYearInfo yearbookyear)
        {
            if (null == yearbookyear)
            {
                return false;
            }
            return ReYearBookYear.Add(yearbookyear);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">年鉴年信息的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReYearBookYear.SetIsOnline(id, isOnLine, dateTime);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            //YearBookYearInfo book = ReYearBookYear.GetItem(id);
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            //获取年鉴
            YearBookYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //删除图片
            Pic p = new Pic();
            bool IsSuccess = p.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }
            //删除附件
            Attachment atta = new Attachment();
            IsSuccess = atta.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }
            //删除论文文章
            YearBookArticle cpter = new YearBookArticle();
            IsSuccess = cpter.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReYearBookYear.Delete(id);
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
            return ReYearBookYear.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(YearBookYearInfo yearbookyear)
        {
            if (null == yearbookyear)
            {
                return false;
            }

            return ReYearBookYear.Update(yearbookyear);
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
            return ReYearBookYear.AddStaticDownload(id);
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
            return ReYearBookYear.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public YearBookYearInfo GetItem(string id)
        {
            return ReYearBookYear.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<YearBookYearInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReYearBookYear.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">年鉴年信息的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            YearBookYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
            int recordCount = 0;
            DRMS.IDAL.IPic p = new DRMS.TPIServerDAL.Pic();
            //IList<PicInfo> lstPic = p.GetList("ParentDoi='" + id + "'", 0, 1000, out recordCount, true);
            //if (recordCount > 1000)
            //    lstPic = p.GetList("ParentDoi='" + id + "'", 0, recordCount, out recordCount, true);
            //for (int i = 0; i < recordCount; i++)
            //{
            //    bool IsSuccess = p.SetState(lstPic[i].SYS_FLD_DOI, state);
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}
            bool IsSuccess = p.SetStateByWhere("ParentDoi='" + id + "'", state);
            if (!IsSuccess)
            {
                return false;
            }
            //修改章节的状态
            recordCount = 0;
            DRMS.IDAL.IYearBookArticle cpter = new DRMS.TPIServerDAL.YearBookArticle();
            //IList<YearBookArticleInfo> lstYearBookArticle = cpter.GetList("ParentDoi='" + id + "'", 0, 1000, out recordCount, true);
            //if (recordCount > 1000)
            //    lstPic = p.GetList("ParentDoi='" + id + "'", 0, recordCount, out recordCount, true);
            //for (int i = 0; i < recordCount; i++)
            //{
            //    bool IsSuccess = cpter.SetState(lstYearBookArticle[i].SYS_FLD_DOI, state);
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}
            IsSuccess = cpter.SetStateByWhere("ParentDoi='" + id + "'", state);
            if (!IsSuccess)
            {
                return false;
            }
            return ReYearBookYear.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReYearBookYear.GetCount(strWhere);
        }
    }
}
