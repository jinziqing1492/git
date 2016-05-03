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
    public class JournalArticle
    {

        public JournalArticle()
        {
            ReJournalArticle.SetTableName("journal");
        }

        public JournalArticle(string type)
        {
            ReJournalArticle.SetTableName(type);
        }

        private static readonly IJournalArticle ReJournalArticle = SelectData.CreateJournalArticle();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(JournalArticleInfo journalarticleinfo)
        {
            if (null == journalarticleinfo)
            {
                return false;
            }
            return ReJournalArticle.Add(journalarticleinfo);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">期刊文章的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReJournalArticle.SetIsOnline(id, isOnLine, dateTime);
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

            JournalArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }


            //删除图片
            Pic p = new Pic();
            bool IsSuccess = p.DeleteByWhere("SYS_FLD_ChapterDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReJournalArticle.Delete(id);
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
            return ReJournalArticle.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(JournalArticleInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReJournalArticle.Update(book);
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
            return ReJournalArticle.AddStaticDownload(id);
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
            return ReJournalArticle.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JournalArticleInfo GetItem(string id)
        {
            return ReJournalArticle.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<JournalArticleInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReJournalArticle.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">期刊文章的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id) || (state != 0 && state != -1))
            {
                return false;
            }
            JournalArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
            Pic picture = new Pic();
            int record = 0;
            IList<PicInfo> list = picture.GetList("SYS_FLD_ChapterDoi='" + id + "'", 1, 1000, out record, false);
            foreach (PicInfo pictureinfo in list)
            {
                bool Flag = picture.SetState(pictureinfo.SYS_FLD_DOI, state);
                if (!Flag)
                {
                    return false;
                }
            }

            return ReJournalArticle.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReJournalArticle.GetCount(strWhere);
        }
    }
}
