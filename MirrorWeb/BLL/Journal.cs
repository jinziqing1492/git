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
    public class Journal
    {
        public Journal()
        {
            ReJournalInfo.SetTableName("journal");
            journalyear.SetTableName("journal");
            journalarticle.SetTableName("journal");
        }

        public Journal(string type)
        {
            ReJournalInfo.SetTableName(type);
            journalyear.SetTableName(type);
            journalarticle.SetTableName(type);
        }

        private static readonly IJournalInfo ReJournalInfo = SelectData.CreateJournalInfo();

        private static readonly IJournalYearInfo journalyear = SelectData.CreateJournalYearInfo();

        private static readonly IJournalArticle journalarticle = SelectData.CreateJournalArticle();

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(JournalInfo journalinfo)
        {
            if (null == journalinfo)
            {
                return false;
            }
            return ReJournalInfo.Add(journalinfo);
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

            JournalInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //删除年信息中的文章
            bool IsSuccess = journalarticle.DeleteByWhere("BaseId='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            //删除年信息
            IsSuccess = journalyear.DeleteByWhere("BaseId='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReJournalInfo.Delete(id);
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
            return ReJournalInfo.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(JournalInfo journalinfo)
        {
            if (null == journalinfo)
            {
                return false;
            }

            return ReJournalInfo.Update(journalinfo);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JournalInfo GetItem(string id)
        {
            return ReJournalInfo.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<JournalInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReJournalInfo.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">期刊的BASEID</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            JournalInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //更改期刊年信息状态
            int record = 0;
            IList<JournalYearInfo> listpic = journalyear.GetList("BaseId='" + id + "'", 1, 1000, out record, false);
            bool Flag = false;
            if (listpic!=null)
            {
                foreach (JournalYearInfo journalyearinfo in listpic)
                {
                    Flag = journalyear.SetState(journalyearinfo.SYS_FLD_DOI, state);
                    if (!Flag)
                    {
                        return false;
                    }
                }
            }

            //更改期刊文章状态
            JournalArticle journalarticle = new JournalArticle();
            record = 0;
            IList<JournalArticleInfo> listjour = journalarticle.GetList("BaseId='" + id + "'", 1, 1000, out record, false);
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

            return ReJournalInfo.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReJournalInfo.GetCount(strWhere);
        }
    }
}
