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
    public class JournalYear
    {
        public JournalYear()
        {
            ReJournalYearInfo.SetTableName("journal");
        }

        public JournalYear(string type)
        {
            ReJournalYearInfo.SetTableName(type);
            journalarticle.SetTableName(type);
        }

        private static readonly IJournalYearInfo ReJournalYearInfo = SelectData.CreateJournalYearInfo();
        private static readonly IJournalArticle journalarticle = SelectData.CreateJournalArticle();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(JournalYearInfo journalyearinfo)
        {
            if (null == journalyearinfo)
            {
                return false;
            }
            return ReJournalYearInfo.Add(journalyearinfo);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">期刊年信息的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            JournalYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReJournalYearInfo.SetIsOnline(id, isOnLine, dateTime);
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

            JournalYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //删除图片
            Pic picture = new Pic();
            bool issuccess = picture.DeleteByWhere("ParentDoi='" + id + "'");
            if (!issuccess)
            {
                return false;
            }

            //删除文章
            issuccess = journalarticle.DeleteByWhere("ParentDoi='" + id + "'");
            if (!issuccess)
            {
                return false;
            }

            //删除附件
            Attachment attach = new Attachment();
            issuccess = attach.DeleteByWhere("ParentDoi='" + id + "'");
            if (!issuccess)
            {
                return false;
            }

            return ReJournalYearInfo.Delete(id);
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
            return ReJournalYearInfo.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(JournalYearInfo journalyearinfo)
        {
            if (null == journalyearinfo)
            {
                return false;
            }

            return ReJournalYearInfo.Update(journalyearinfo);
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
            return ReJournalYearInfo.AddStaticDownload(id);
        }

        /// <summary>
        /// 点击量
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public bool AddHitCount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return ReJournalYearInfo.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public JournalYearInfo GetItem(string id)
        {
            return ReJournalYearInfo.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<JournalYearInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReJournalYearInfo.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">期刊年信息的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            JournalYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //更改图片状态
            Pic picture=new Pic();
            int record = 0;
            IList<PicInfo> listpic = picture.GetList("ParentDoi='" + id + "'", 1, 1000, out record, false);
            bool Flag = false;
            if (listpic != null)
            {
                foreach (PicInfo pictureinfo in listpic)
                {
                    Flag = picture.SetState(pictureinfo.SYS_FLD_DOI, state);
                    if (!Flag)
                    {
                        return false;
                    }
                }
            }

            //更改期刊状态
            record = 0;
            IList<JournalArticleInfo> listjour = journalarticle.GetList("ParentDoi='" + id + "'", 1, 1000, out record, false);
            if (listjour != null)
            {
                foreach (JournalArticleInfo articleinfo in listjour)
                {
                    Flag = journalarticle.SetState(articleinfo.SYS_FLD_DOI, state);
                    if (!Flag)
                    {
                        return false;
                    }
                }
            }

            return ReJournalYearInfo.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReJournalYearInfo.GetCount(strWhere);
        }
    }
}
