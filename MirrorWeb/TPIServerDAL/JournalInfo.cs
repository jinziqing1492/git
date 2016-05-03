using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using DRMS.Model;
using DRMS.IDAL;
using CNKI.BaseFunction;
using TPI;

namespace DRMS.TPIServerDAL
{
    public class JournalInfo : IJournalInfo
    {
        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="type"></param>
        public void SetTableName(string type)
        {
            switch (type)
            {
                case "journal": TABLE_NAME = ConfigurationManager.AppSettings["JournalInfo"];
                    break;
                case "english": TABLE_NAME = ConfigurationManager.AppSettings["EnglishRes"];
                    break;
                case "study": TABLE_NAME = ConfigurationManager.AppSettings["Book1"];
                    break;
                case "owner": TABLE_NAME = ConfigurationManager.AppSettings["OwnerRes"];
                    break;
                default: TABLE_NAME = ConfigurationManager.AppSettings["JournalInfo"];
                    break;
            }
        }

        private string TABLE_NAME = ConfigurationManager.AppSettings["JournalInfo"];
        #region IArticle 字段
        private const string PARM_BASEID = "BASEID";
        private const string PARM_CNAME = "CNAME";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_FOUNDDATE = "FOUNDDATE";
        private const string PARM_OTHERNAME = "OTHERNAME";
        private const string PARM_CN = "CN";
        private const string PARM_ISSN = "ISSN";
        private const string PARM_TYPE = "TYPE";
        private const string PARM_HOSTDEP = "HOSTDEP";
        private const string PARM_PUBDEP = "PUBDEP";
        private const string PARM_PUBPLACE = "PUBPLACE";
        private const string PARM_LANGUAGE = "LANGUAGE";
        private const string PARM_ISRECOMMEND = "ISRECOMMEND";
        private const string PARM_SYS_FLD_MARK_USERNAME = "SYS_FLD_MARK_USERNAME";
        private const string PARM_SYS_FLD_MARK_DATE = "SYS_FLD_MARK_DATE";
        private const string PARM_SYS_FLD_MARK_STATE = "SYS_FLD_MARK_STATE";
        private const string PARM_SYS_FLD_CHECK_USERNAME = "SYS_FLD_CHECK_USERNAME";
        private const string PARM_SYS_FLD_CHECK_DATE = "SYS_FLD_CHECK_DATE";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";
        private const string PARM_SYS_FLD_CLASSFICATION = "SYS_FLD_CLASSFICATION";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_COVERPATH = "SYS_FLD_COVERPATH";
        private const string PARM_CHIEFEDITOR = "ChiefEditor";
        private const string PARM_CHIEFEMAIL = "ChiefEmail";
        private const string PARM_CONTRIBUTEEMAIL = "ContributeEmail";
        private const string PARM_CONTRACT = "Contract";



        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(Model.JournalInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.BASEID))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            }
            if (!string.IsNullOrEmpty(item.CNAME))
            {
                paramList.Add(PARM_CNAME);
                paramList.Add(item.CNAME);
            }
            if (!string.IsNullOrEmpty(item.Description))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.Description);
            }
            if (item.FoundDate != DateTime.MinValue)
            {
                paramList.Add(PARM_FOUNDDATE);
                paramList.Add(item.FoundDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.OtherName))
            {
                paramList.Add(PARM_OTHERNAME);
                paramList.Add(item.OtherName);
            }
            if (!string.IsNullOrEmpty(item.CN))
            {
                paramList.Add(PARM_CN);
                paramList.Add(item.CN);
            }
            if (!string.IsNullOrEmpty(item.ISSN))
            {
                paramList.Add(PARM_ISSN);
                paramList.Add(item.ISSN);
            }
            if (!string.IsNullOrEmpty(item.Type))
            {
                paramList.Add(PARM_TYPE);
                paramList.Add(item.Type);
            }
            if (!string.IsNullOrEmpty(item.Hostdep))
            {
                paramList.Add(PARM_HOSTDEP);
                paramList.Add(item.Hostdep);
            }
            if (!string.IsNullOrEmpty(item.Pubdep))
            {
                paramList.Add(PARM_PUBDEP);
                paramList.Add(item.Pubdep);
            }
            if (!string.IsNullOrEmpty(item.PubPlace))
            {
                paramList.Add(PARM_PUBPLACE);
                paramList.Add(item.PubPlace);
            }
            if (!string.IsNullOrEmpty(item.Language))
            {
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            }
            paramList.Add(PARM_ISRECOMMEND);
            paramList.Add(item.ISRecommend.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_MARK_USERNAME))
            {
                paramList.Add(PARM_SYS_FLD_MARK_USERNAME);
                paramList.Add(item.SYS_FLD_MARK_USERNAME);
            }
            if (item.SYS_FLD_MARK_DATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_MARK_DATE);
                paramList.Add(item.SYS_FLD_MARK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_SYS_FLD_MARK_STATE);
            paramList.Add(item.SYS_FLD_MARK_STATE.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_CHECK_USERNAME))
            {
                paramList.Add(PARM_SYS_FLD_CHECK_USERNAME);
                paramList.Add(item.SYS_FLD_CHECK_USERNAME);
            }
            if (item.SYS_FLD_CHECK_DATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_CHECK_DATE);
                paramList.Add(item.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            {
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_COVERPATH))
            {
                paramList.Add(PARM_SYS_FLD_COVERPATH);
                paramList.Add(item.SYS_FLD_COVERPATH);
            }

            if (!string.IsNullOrEmpty(item.ChiefEmail))
            {
                paramList.Add(PARM_CHIEFEMAIL);
                paramList.Add(item.ChiefEmail);
            }
            if (!string.IsNullOrEmpty(item.ChiefEditor))
            {
                paramList.Add(PARM_CHIEFEDITOR);
                paramList.Add(item.ChiefEditor);
            }
            if (!string.IsNullOrEmpty(item.ContributeEmail))
            {
                paramList.Add(PARM_CONTRIBUTEEMAIL);
                paramList.Add(item.ContributeEmail);
            }
            if (!string.IsNullOrEmpty(item.Contract))
            {
                paramList.Add(PARM_CONTRACT);
                paramList.Add(item.Contract);
            }
            #endregion
            try
            {
                return TPIHelper.Insert(TABLE_NAME, paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_BASEID, id);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(Model.JournalInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.BASEID))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            }
            if (!string.IsNullOrEmpty(item.CNAME))
            {
                paramList.Add(PARM_CNAME);
                paramList.Add(item.CNAME);
            }
            if (!string.IsNullOrEmpty(item.Description))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.Description);
            }
            if (item.FoundDate != DateTime.MinValue)
            {
                paramList.Add(PARM_FOUNDDATE);
                paramList.Add(item.FoundDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.OtherName))
            {
                paramList.Add(PARM_OTHERNAME);
                paramList.Add(item.OtherName);
            }
            if (!string.IsNullOrEmpty(item.CN))
            {
                paramList.Add(PARM_CN);
                paramList.Add(item.CN);
            }
            if (!string.IsNullOrEmpty(item.ISSN))
            {
                paramList.Add(PARM_ISSN);
                paramList.Add(item.ISSN);
            }
            if (!string.IsNullOrEmpty(item.Type))
            {
                paramList.Add(PARM_TYPE);
                paramList.Add(item.Type);
            }
            if (!string.IsNullOrEmpty(item.Hostdep))
            {
                paramList.Add(PARM_HOSTDEP);
                paramList.Add(item.Hostdep);
            }
            if (!string.IsNullOrEmpty(item.Pubdep))
            {
                paramList.Add(PARM_PUBDEP);
                paramList.Add(item.Pubdep);
            }
            if (!string.IsNullOrEmpty(item.PubPlace))
            {
                paramList.Add(PARM_PUBPLACE);
                paramList.Add(item.PubPlace);
            }
            if (!string.IsNullOrEmpty(item.Language))
            {
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            }
            paramList.Add(PARM_ISRECOMMEND);
            paramList.Add(item.ISRecommend.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_MARK_USERNAME))
            {
                paramList.Add(PARM_SYS_FLD_MARK_USERNAME);
                paramList.Add(item.SYS_FLD_MARK_USERNAME);
            }
            if (item.SYS_FLD_MARK_DATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_MARK_DATE);
                paramList.Add(item.SYS_FLD_MARK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_SYS_FLD_MARK_STATE);
            paramList.Add(item.SYS_FLD_MARK_STATE.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_CHECK_USERNAME))
            {
                paramList.Add(PARM_SYS_FLD_CHECK_USERNAME);
                paramList.Add(item.SYS_FLD_CHECK_USERNAME);
            }
            if (item.SYS_FLD_CHECK_DATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_CHECK_DATE);
                paramList.Add(item.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            {
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            }

            paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
            paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);

            paramList.Add(PARM_SYS_FLD_COVERPATH);
            paramList.Add(item.SYS_FLD_COVERPATH);

            paramList.Add(PARM_CHIEFEMAIL);
            paramList.Add(item.ChiefEmail);

            paramList.Add(PARM_CHIEFEDITOR);
            paramList.Add(item.ChiefEditor);

            paramList.Add(PARM_CONTRIBUTEEMAIL);
            paramList.Add(item.ContributeEmail);

            paramList.Add(PARM_CONTRACT);
            paramList.Add(item.Contract);

            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_BASEID + "='" + item.BASEID + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据条件获得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.JournalInfo GetItem(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_BASEID, id);
            RecordSet rs = TPIHelper.GetRecordSet(sqlQuery);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            try
            {
                Model.JournalInfo entry = new Model.JournalInfo();
                #region 判断字段并赋值
                entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                entry.CNAME = rs.GetValue(PARM_CNAME) ?? "";
                entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.FoundDate = StructTrans.TransDate(rs.GetValue(PARM_FOUNDDATE));
                entry.OtherName = rs.GetValue(PARM_OTHERNAME) ?? "";
                entry.CN = rs.GetValue(PARM_CN) ?? "";
                entry.ISSN = rs.GetValue(PARM_ISSN) ?? "";
                entry.Type = rs.GetValue(PARM_TYPE) ?? "";
                entry.Hostdep = rs.GetValue(PARM_HOSTDEP) ?? "";
                entry.Pubdep = rs.GetValue(PARM_PUBDEP) ?? "";
                entry.PubPlace = rs.GetValue(PARM_PUBPLACE) ?? "";
                entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                entry.ISRecommend = StructTrans.TransNum(rs.GetValue(PARM_ISRECOMMEND));
                entry.SYS_FLD_MARK_USERNAME = rs.GetValue(PARM_SYS_FLD_MARK_USERNAME) ?? "";
                entry.SYS_FLD_MARK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_MARK_DATE));
                entry.SYS_FLD_MARK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_MARK_STATE));
                entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";

                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                entry.Contract = rs.GetValue(PARM_CONTRACT) ?? "";
                entry.ContributeEmail = rs.GetValue(PARM_CONTRIBUTEEMAIL) ?? "";
                entry.ChiefEditor = rs.GetValue(PARM_CHIEFEDITOR) ?? "";
                entry.ChiefEmail = rs.GetValue(PARM_CHIEFEMAIL) ?? "";
               

                #endregion
                return entry;
            }
            catch
            {
                return null;
            }
            finally
            {
                rs.Close();
            }
        }

        /// <summary>
        /// 根据分页获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<Model.JournalInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            recordCount = 0;
            RecordSet rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, sqlWhere);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            //  获取总得记录数
            recordCount = rs.GetCount();
            rs.SetHitWordMarkFlag(RED_LEFT, RED_RIGHT);
            //  获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<Model.JournalInfo> entryList = new List<Model.JournalInfo>();
                Model.JournalInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new Model.JournalInfo();
                    #region 判断字段并赋值
                    entry.BASEID = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_BASEID)) ?? "";
                    entry.CNAME = rs.GetValue(PARM_CNAME) ?? "";
                    entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.FoundDate = StructTrans.TransDate(rs.GetValue(PARM_FOUNDDATE));
                    entry.OtherName = rs.GetValue(PARM_OTHERNAME) ?? "";
                    entry.CN = rs.GetValue(PARM_CN) ?? "";
                    entry.ISSN = rs.GetValue(PARM_ISSN) ?? "";
                    entry.Type = rs.GetValue(PARM_TYPE) ?? "";
                    entry.Hostdep = rs.GetValue(PARM_HOSTDEP) ?? "";
                    entry.Pubdep = rs.GetValue(PARM_PUBDEP) ?? "";
                    entry.PubPlace = rs.GetValue(PARM_PUBPLACE) ?? "";
                    entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                    entry.ISRecommend = StructTrans.TransNum(rs.GetValue(PARM_ISRECOMMEND));
                    entry.SYS_FLD_MARK_USERNAME = rs.GetValue(PARM_SYS_FLD_MARK_USERNAME) ?? "";
                    entry.SYS_FLD_MARK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_MARK_DATE));
                    entry.SYS_FLD_MARK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_MARK_STATE));
                    entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                    entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                    entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                    entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";

                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                    entry.Contract = rs.GetValue(PARM_CONTRACT) ?? "";
                    entry.ContributeEmail = rs.GetValue(PARM_CONTRIBUTEEMAIL) ?? "";
                    entry.ChiefEditor = rs.GetValue(PARM_CHIEFEDITOR) ?? "";
                    entry.ChiefEmail = rs.GetValue(PARM_CHIEFEMAIL) ?? "";

                    #endregion
                    entryList.Add(entry);
                    if (!rs.MoveNext())
                    {
                        break;
                    }
                }
                return entryList;
            }
            catch
            {
                return null;
            }
            finally
            {
                rs.Close();
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrWhiteSpace(strWhere))
            {
                return false;
            }

            int record=0;
            List<Model.JournalInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list == null)
            {
                return true;//list为空时，表示表中没有相关数据，在KBASE中，删除不存在的记录返回为空
            }

            JournalYearInfo journalyear = new JournalYearInfo();
            JournalArticle journalarticle = new JournalArticle();
            foreach (Model.JournalInfo info in list)
            {
                //删除期刊文章信息
                bool IsSuccess = journalarticle.DeleteByWhere("BaseId='" + info.BASEID + "'");
                if (!IsSuccess)
                {
                    return false;
                }

                //删除期刊年信息
                IsSuccess = journalyear.DeleteByWhere("BaseId='" + info.BASEID + "'");
                if (!IsSuccess)
                {
                    return false;
                }
            }

            //删除期刊
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件获得记录总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">期刊的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改期刊状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE BASEID='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }
    }
}
