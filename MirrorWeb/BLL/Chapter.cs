using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class Chapter
    {
        private static readonly IChapter ReChapter = SelectData.CreateChapter();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(ChapterInfo chapter)
        {
            if (null == chapter)
            {
                return false;
            }
            return ReChapter.Add(chapter);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">章节的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReChapter.SetIsOnline(id, isOnLine, dateTime);
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
            if (string.IsNullOrEmpty(id))
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

            ChapterInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReChapter.Delete(id);
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
            return ReChapter.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(ChapterInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReChapter.Update(book);
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
            return ReChapter.AddStaticDownload(id);
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
            return ReChapter.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ChapterInfo GetItem(string id)
        {
            return ReChapter.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<ChapterInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReChapter.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">章节的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id) || (state != 0 && state != -1))
            {
                return false;
            }
            ChapterInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
           // int recordCount = 0;
            DRMS.IDAL.IPic p = new DRMS.TPIServerDAL.Pic();
            //IList<PicInfo> lstPic = p.GetList("Sys_fld_ChapterDoi='" + id + "'", 0, 1000, out recordCount, true);
            //if (recordCount > 1000)
            //    lstPic = p.GetList("Sys_fld_ChapterDoi='" + id + "'", 0, recordCount, out recordCount, true);
            //for (int i = 0; i < recordCount; i++)
            //{
            //    bool IsSuccess = p.SetState(lstPic[i].SYS_FLD_DOI, state);
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}

            bool IsSuccess = p.SetStateByWhere("Sys_fld_ChapterDoi='" + id + "'", state);
            if (!IsSuccess)
            {
                return false;
            }
            return ReChapter.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReChapter.GetCount(strWhere);
        }

           /// <summary>
        /// 对章节的逻辑库id进行更改
        /// </summary>
        /// <param name="ldbid">图书（标准）的逻辑库id</param>
        /// <param name="doi">图书（标准）doi</param>
        /// <returns></returns>
        public bool UpdateLDBID(string ldbid ,string doi)
        {
            return ReChapter.UpdateLDBID(ldbid ,doi);
        }

    }
}
