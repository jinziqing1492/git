﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class YearBookArticle
    {
        private static readonly IYearBookArticle ReYearBookArticle = SelectData.CreateYearBookArticle();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(YearBookArticleInfo yearbookarticle)
        {
            if (null == yearbookarticle)
            {
                return false;
            }
            return ReYearBookArticle.Add(yearbookarticle);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">年鉴文章信息的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            //获取年鉴
            YearBookArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReYearBookArticle.SetIsOnline(id, isOnLine, dateTime);
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

            //获取年鉴
            YearBookArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //删除图片
            Pic p = new Pic();
            bool IsSuccess = p.DeleteByWhere("Sys_fld_ChapterDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReYearBookArticle.Delete(id);
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
            return ReYearBookArticle.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(YearBookArticleInfo yearbookarticle)
        {
            if (null == yearbookarticle)
            {
                return false;
            }

            return ReYearBookArticle.Update(yearbookarticle);
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
            return ReYearBookArticle.AddStaticDownload(id);
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
            return ReYearBookArticle.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public YearBookArticleInfo GetItem(string id)
        {
            return ReYearBookArticle.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<YearBookArticleInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReYearBookArticle.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">年鉴文章信息的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id) || (state != 0 && state != -1))
            {
                return false;
            }
            YearBookArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
            int recordCount = 0;
            DRMS.IDAL.IPic p = new DRMS.TPIServerDAL.Pic();
            IList<PicInfo> lstPic = p.GetList("Sys_fld_ChapterDoi='" + id + "'", 0, 1000, out recordCount, true);
            if (recordCount > 1000)
                lstPic = p.GetList("Sys_fld_ChapterDoi='" + id + "'", 0, recordCount, out recordCount, true);
            for (int i = 0; i < recordCount; i++)
            {
                bool IsSuccess = p.SetState(lstPic[i].SYS_FLD_DOI, state);
                if (!IsSuccess)
                {
                    return false;
                }
            }

            return ReYearBookArticle.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReYearBookArticle.GetCount(strWhere);
        }
    }
}
