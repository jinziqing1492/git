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
    public class JournalYearInfo : IJournalYearInfo
    {
        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="type"></param>
        public void SetTableName(string type)
        {
            switch (type)
            {
                case "journal": TABLE_NAME = ConfigurationManager.AppSettings["JournalYearInfo"];
                    break;
                case "english": TABLE_NAME = ConfigurationManager.AppSettings["EnglishYear"];
                    break;
                case "study": TABLE_NAME = ConfigurationManager.AppSettings["Book1"];
                    break;
                case "owner": TABLE_NAME = ConfigurationManager.AppSettings["OwnerResBook"];
                    break;
                default: TABLE_NAME = ConfigurationManager.AppSettings["JournalYearInfo"];
                    break;
            }
        }

        private string TABLE_NAME = ConfigurationManager.AppSettings["JournalYearInfo"];
        #region IArticle 字段
        private const string PARM_BASEID = "BASEID";
        private const string PARM_CNAME = "CNAME";
        private const string PARM_YEAR = "YEAR";
        private const string PARM_ISSUE = "ISSUE";
        private const string PARM_YEARISSUE = "YEARISSUE";
        private const string PARM_THNAME = "THNAME";
        private const string PARM_TYPE = "TYPE";
        private const string PARM_PUBDEP = "PUBDEP";
        private const string PARM_RECOMMENDCOUNT = "RECOMMENDCOUNT";
        private const string PARM_PUBDATE = "PUBDATE";
        //private const string PARM_SYS_FLD_ORDERNUM = "SYS_FLD_ORDERNUM";
        private const string PARM_SYS_FLD_XPATH = "SYS_FLD_XPATH";
        //private const string PARM_SYS_FLD_ORIGINALID = "SYS_FLD_ORIGINALID";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_SYS_FLD_MARK_USERNAME = "SYS_FLD_MARK_USERNAME";
        private const string PARM_SYS_FLD_MARK_DATE = "SYS_FLD_MARK_DATE";
        private const string PARM_SYS_FLD_MARK_STATE = "SYS_FLD_MARK_STATE";
        private const string PARM_SYS_FLD_CHECK_USERNAME = "SYS_FLD_CHECK_USERNAME";
        private const string PARM_SYS_FLD_CHECK_DATE = "SYS_FLD_CHECK_DATE";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";
        private const string PARM_SYS_FLD_ERROR_DESCRIPT = "SYS_FLD_ERROR_DESCRIPT";
        private const string PARM_SYS_FLD_CLASSFICATION = "SYS_FLD_CLASSFICATION";
        private const string PARM_SYS_FLD_CATALOG = "SYS_FLD_CATALOG";
        //private const string PARM_SYS_FLD_RES_LEVEL = "SYS_FLD_RES_LEVEL";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_SYS_FLD_VSM = "SYS_FLD_VSM";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_COVERPATH = "SYS_FLD_COVERPATH";
        private const string PARM_SYS_FLD_SRCFILENAME = "SYS_FLD_SRCFILENAME";
        private const string PARM_SYS_FLD_XMLPATH = "SYS_FLD_XMLPATH";
        private const string PARM_SYS_FLD_HITCOUNT = "SYS_FLD_HITCOUNT";
        private const string PARM_SYS_FLD_DOWNLOAD = "SYS_FLD_DOWNLOAD";
        private const string PARM_SYS_FLD_LDBID = "SYS_FLD_LDBID";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_ONLINEDOI = "ONLINEDOI";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_COPYRIGHTENDDATE = "COPYRIGHTENDDATE";
        private const string PARM_SYS_FLD_ISIMPORT = "SYS_FLD_ISIMPORT";
        private const string PARM_DEPARTMENT = "DEPARTMENT";

        private const string PARM_SYS_FLD_BOOKINFO = "Sys_fld_BookInfo";
        private const string PARM_PRICE = "PRICE";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(Model.JournalYearInfo item)
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
            paramList.Add(PARM_YEAR);
            paramList.Add(item.YEAR.ToString());
            if (!string.IsNullOrEmpty(item.ISSUE))
            {
                paramList.Add(PARM_ISSUE);
                paramList.Add(item.ISSUE);
            }
            if (!string.IsNullOrEmpty(item.Yearissue))
            {
                paramList.Add(PARM_YEARISSUE);
                paramList.Add(item.Yearissue);
            }
            if (!string.IsNullOrEmpty(item.THNAME))
            {
                paramList.Add(PARM_THNAME);
                paramList.Add(item.THNAME);
            }
            if (!string.IsNullOrEmpty(item.Type))
            {
                paramList.Add(PARM_TYPE);
                paramList.Add(item.Type);
            }
            if (!string.IsNullOrEmpty(item.Pubdep))
            {
                paramList.Add(PARM_PUBDEP);
                paramList.Add(item.Pubdep);
            }
            paramList.Add(PARM_RECOMMENDCOUNT);
            paramList.Add(item.Recommendcount.ToString());
            if (item.Pubdate != DateTime.MinValue)
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.Pubdate.ToString("yyyy-MM-dd"));
            }
            //paramList.Add(PARM_SYS_FLD_ORDERNUM);
            //paramList.Add(item.Sys_fld_ordernum.ToString());
            if (!string.IsNullOrEmpty(item.Sys_fld_xpath))
            {
                paramList.Add(PARM_SYS_FLD_XPATH);
                paramList.Add(item.Sys_fld_xpath);
            }
            //if (!string.IsNullOrEmpty(item.Sys_fld_originalID))
            //{
            //    paramList.Add(PARM_SYS_FLD_ORIGINALID);
            //    paramList.Add(item.Sys_fld_originalID);
            //}
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
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
            if (!string.IsNullOrEmpty(item.SYS_FLD_ERROR_DESCRIPT))
            {
                paramList.Add(PARM_SYS_FLD_ERROR_DESCRIPT);
                paramList.Add(item.SYS_FLD_ERROR_DESCRIPT);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            {
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CATALOG))
            {
                paramList.Add(PARM_SYS_FLD_CATALOG);
                paramList.Add(item.SYS_FLD_CATALOG);
            }
            //paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            //paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VSM))
            {
                paramList.Add(PARM_SYS_FLD_VSM);
                paramList.Add(item.SYS_FLD_VSM);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_FILEPATH))
            {
                paramList.Add(PARM_SYS_FLD_FILEPATH);
                paramList.Add(item.SYS_FLD_FILEPATH);
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
            if (!string.IsNullOrEmpty(item.SYS_FLD_SRCFILENAME))
            {
                paramList.Add(PARM_SYS_FLD_SRCFILENAME);
                paramList.Add(item.SYS_FLD_SRCFILENAME);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_XMLPATH))
            {
                paramList.Add(PARM_SYS_FLD_XMLPATH);
                paramList.Add(item.SYS_FLD_XMLPATH);
            }
            paramList.Add(PARM_SYS_FLD_HITCOUNT);
            paramList.Add(item.Sys_fld_Hitcount.ToString());
            paramList.Add(PARM_SYS_FLD_DOWNLOAD);
            paramList.Add(item.Sys_fld_Download.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_LDBID))
            {
                paramList.Add(PARM_SYS_FLD_LDBID);
                paramList.Add(item.SYS_FLD_LDBID);
            }
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.IsOnline.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
            }
            if (item.Sys_fld_Adddate != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            {
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            }
            if (!string.IsNullOrEmpty(item.COPYRIGHTENDDATE))
            {
                paramList.Add(PARM_COPYRIGHTENDDATE);
                paramList.Add(item.COPYRIGHTENDDATE);
            }
            paramList.Add(PARM_SYS_FLD_ISIMPORT);
            paramList.Add(item.Sys_fld_isimport.ToString());
            if (!string.IsNullOrEmpty(item.Department))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
            }

            if (!string.IsNullOrEmpty(item.Sys_fld_BookInfo))
            {
                paramList.Add(PARM_SYS_FLD_BOOKINFO);
                paramList.Add(item.Sys_fld_BookInfo);
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
        /// <param name="doi"></param>
        /// <returns></returns>
        public bool Delete(string doi)
        {
            if (string.IsNullOrWhiteSpace(doi))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(Model.JournalYearInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            //if (!string.IsNullOrEmpty(item.BASEID))
            //{
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            //}
            if (!string.IsNullOrEmpty(item.CNAME))
            {
                paramList.Add(PARM_CNAME);
                paramList.Add(item.CNAME);
            }
            paramList.Add(PARM_YEAR);
            paramList.Add(item.YEAR.ToString());
            //if (!string.IsNullOrEmpty(item.ISSUE))
            //{
                paramList.Add(PARM_ISSUE);
                paramList.Add(item.ISSUE);
            //}
            //if (!string.IsNullOrEmpty(item.Yearissue))
            //{
                paramList.Add(PARM_YEARISSUE);
                paramList.Add(item.Yearissue);
            //}
            //if (!string.IsNullOrEmpty(item.THNAME))
            //{
                paramList.Add(PARM_THNAME);
                paramList.Add(item.THNAME);
            //}
            //if (!string.IsNullOrEmpty(item.Type))
            //{
                paramList.Add(PARM_TYPE);
                paramList.Add(item.Type);
            //}
            //if (!string.IsNullOrEmpty(item.Pubdep))
            //{
                paramList.Add(PARM_PUBDEP);
                paramList.Add(item.Pubdep);
            //}
            paramList.Add(PARM_RECOMMENDCOUNT);
            paramList.Add(item.Recommendcount.ToString());
            //if (item.Pubdate != DateTime.MinValue)
            //{
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.Pubdate.ToString("yyyy-MM-dd"));
            //}
            //paramList.Add(PARM_SYS_FLD_ORDERNUM);
            //paramList.Add(item.Sys_fld_ordernum.ToString());
            //if (!string.IsNullOrEmpty(item.Sys_fld_xpath))
            //{
            //    paramList.Add(PARM_SYS_FLD_XPATH);
            //    paramList.Add(item.Sys_fld_xpath);
            //}
            //if (!string.IsNullOrEmpty(item.Sys_fld_originalID))
            //{
            //    paramList.Add(PARM_SYS_FLD_ORIGINALID);
            //    paramList.Add(item.Sys_fld_originalID);
            //}
            //if (!string.IsNullOrEmpty(item.Keywords))
            //{
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_MARK_USERNAME))
            //{
                paramList.Add(PARM_SYS_FLD_MARK_USERNAME);
                paramList.Add(item.SYS_FLD_MARK_USERNAME);
            //}
            //if (item.SYS_FLD_MARK_DATE != DateTime.MinValue)
            //{
                paramList.Add(PARM_SYS_FLD_MARK_DATE);
                paramList.Add(item.SYS_FLD_MARK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            paramList.Add(PARM_SYS_FLD_MARK_STATE);
            paramList.Add(item.SYS_FLD_MARK_STATE.ToString());
            //if (!string.IsNullOrEmpty(item.SYS_FLD_CHECK_USERNAME))
            //{
                paramList.Add(PARM_SYS_FLD_CHECK_USERNAME);
                paramList.Add(item.SYS_FLD_CHECK_USERNAME);
            //}
            //if (item.SYS_FLD_CHECK_DATE != DateTime.MinValue)
            //{
                paramList.Add(PARM_SYS_FLD_CHECK_DATE);
                paramList.Add(item.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
            //if (!string.IsNullOrEmpty(item.SYS_FLD_ERROR_DESCRIPT))
            //{
                paramList.Add(PARM_SYS_FLD_ERROR_DESCRIPT);
                paramList.Add(item.SYS_FLD_ERROR_DESCRIPT);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            //{
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_CATALOG))
            //{
                paramList.Add(PARM_SYS_FLD_CATALOG);
                paramList.Add(item.SYS_FLD_CATALOG);
            //}
            //paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            //paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
            //if (!string.IsNullOrEmpty(item.SYS_FLD_VSM))
            //{
                paramList.Add(PARM_SYS_FLD_VSM);
                paramList.Add(item.SYS_FLD_VSM);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_FILEPATH))
            //{
                paramList.Add(PARM_SYS_FLD_FILEPATH);
                paramList.Add(item.SYS_FLD_FILEPATH);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            //{
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_COVERPATH))
            //{
                paramList.Add(PARM_SYS_FLD_COVERPATH);
                paramList.Add(item.SYS_FLD_COVERPATH);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_SRCFILENAME))
            //{
                paramList.Add(PARM_SYS_FLD_SRCFILENAME);
                paramList.Add(item.SYS_FLD_SRCFILENAME);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_XMLPATH))
            //{
                paramList.Add(PARM_SYS_FLD_XMLPATH);
                paramList.Add(item.SYS_FLD_XMLPATH);
            //}
            paramList.Add(PARM_SYS_FLD_HITCOUNT);
            paramList.Add(item.Sys_fld_Hitcount.ToString());
            paramList.Add(PARM_SYS_FLD_DOWNLOAD);
            paramList.Add(item.Sys_fld_Download.ToString());
            //if (!string.IsNullOrEmpty(item.SYS_FLD_LDBID))
            //{
                paramList.Add(PARM_SYS_FLD_LDBID);
                paramList.Add(item.SYS_FLD_LDBID);
            //}
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.IsOnline.ToString());
            //if (!string.IsNullOrEmpty(item.OnlineDoi))
            //{
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
            //}
            //if (item.Sys_fld_Adddate != DateTime.MinValue)
            //{
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            //{
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            //}
            //if (!string.IsNullOrEmpty(item.COPYRIGHTENDDATE))
            //{
                paramList.Add(PARM_COPYRIGHTENDDATE);
                paramList.Add(item.COPYRIGHTENDDATE);
            //}
            paramList.Add(PARM_SYS_FLD_ISIMPORT);
            paramList.Add(item.Sys_fld_isimport.ToString());
            //if (!string.IsNullOrEmpty(item.Department))
            //{
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
            //}

            //if (!string.IsNullOrEmpty(item.Sys_fld_BookInfo))
            //{
                paramList.Add(PARM_SYS_FLD_BOOKINFO);
                paramList.Add(item.Sys_fld_BookInfo);
            //}
            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_SYS_FLD_DOI + "='" + item.SYS_FLD_DOI + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id获得一条记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public Model.JournalYearInfo GetItem(string doi)
        {
            if (string.IsNullOrEmpty(doi))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
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
                Model.JournalYearInfo entry = new Model.JournalYearInfo();
                #region 判断字段并赋值
                entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                entry.CNAME = rs.GetValue(PARM_CNAME) ?? "";
                entry.YEAR = StructTrans.TransNum(rs.GetValue(PARM_YEAR));
                entry.ISSUE = rs.GetValue(PARM_ISSUE) ?? "";
                entry.Yearissue = rs.GetValue(PARM_YEARISSUE) ?? "";
                entry.THNAME = rs.GetValue(PARM_THNAME) ?? "";
                entry.Type = rs.GetValue(PARM_TYPE) ?? "";
                entry.Pubdep = rs.GetValue(PARM_PUBDEP) ?? "";
                entry.Recommendcount = StructTrans.TransNum(rs.GetValue(PARM_RECOMMENDCOUNT));
                entry.Pubdate = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                //entry.Sys_fld_ordernum = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ORDERNUM));
                entry.Sys_fld_xpath = rs.GetValue(PARM_SYS_FLD_XPATH) ?? "";
                //entry.Sys_fld_originalID = rs.GetValue(PARM_SYS_FLD_ORIGINALID) ?? "";
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.SYS_FLD_MARK_USERNAME = rs.GetValue(PARM_SYS_FLD_MARK_USERNAME) ?? "";
                entry.SYS_FLD_MARK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_MARK_DATE));
                entry.SYS_FLD_MARK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_MARK_STATE));
                entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                entry.SYS_FLD_ERROR_DESCRIPT = rs.GetValue(PARM_SYS_FLD_ERROR_DESCRIPT) ?? "";
                entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                entry.SYS_FLD_CATALOG = rs.GetValue(PARM_SYS_FLD_CATALOG) ?? "";
                //entry.SYS_FLD_RES_LEVEL = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_RES_LEVEL));
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";

                entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";
                entry.Price = rs.GetValue(PARM_PRICE) ?? "";

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
        /// 根据分页信息获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<Model.JournalYearInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<Model.JournalYearInfo> entryList = new List<Model.JournalYearInfo>();
                Model.JournalYearInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new Model.JournalYearInfo();
                    #region 判断字段并赋值
                    entry.BASEID = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_BASEID)) ?? "";
                    entry.CNAME = rs.GetValue(PARM_CNAME) ?? "";
                    entry.YEAR = StructTrans.TransNum(rs.GetValue(PARM_YEAR));
                    entry.ISSUE = rs.GetValue(PARM_ISSUE) ?? "";
                    entry.Yearissue = rs.GetValue(PARM_YEARISSUE) ?? "";
                    entry.THNAME = rs.GetValue(PARM_THNAME) ?? "";
                    entry.Type = rs.GetValue(PARM_TYPE) ?? "";
                    entry.Pubdep = rs.GetValue(PARM_PUBDEP) ?? "";
                    entry.Recommendcount = StructTrans.TransNum(rs.GetValue(PARM_RECOMMENDCOUNT));
                    entry.Pubdate = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                    //entry.Sys_fld_ordernum = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ORDERNUM));
                    entry.Sys_fld_xpath = rs.GetValue(PARM_SYS_FLD_XPATH) ?? "";
                    //entry.Sys_fld_originalID = rs.GetValue(PARM_SYS_FLD_ORIGINALID) ?? "";
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.SYS_FLD_MARK_USERNAME = rs.GetValue(PARM_SYS_FLD_MARK_USERNAME) ?? "";
                    entry.SYS_FLD_MARK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_MARK_DATE));
                    entry.SYS_FLD_MARK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_MARK_STATE));
                    entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                    entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                    entry.SYS_FLD_ERROR_DESCRIPT = rs.GetValue(PARM_SYS_FLD_ERROR_DESCRIPT) ?? "";
                    entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                    entry.SYS_FLD_CATALOG = rs.GetValue(PARM_SYS_FLD_CATALOG) ?? "";
                    //entry.SYS_FLD_RES_LEVEL = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_RES_LEVEL));
                    entry.SYS_FLD_DOI =CNKI.BaseFunction.NormalFunction.ResetRedFlag( rs.GetValue(PARM_SYS_FLD_DOI)) ?? "";
                    entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                    entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                    entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                    entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                    entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                    entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                    entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                    entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                    entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";
                   // entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";
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
            int record = 0;
            List<DRMS.Model.JournalYearInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list == null)
            {
                return true;//list为空时，表示表中没有相关数据，在KBASE中，删除不存在的记录返回为空
            }

            JournalArticle jounalarticle = new JournalArticle();
            Pic picture = new Pic();
            Attachment attach = new Attachment();
            foreach (DRMS.Model.JournalYearInfo info in list)
            {
                //删除图片信息
                bool IsSuccess = picture.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }

                //删除期刊文章信息
                IsSuccess = jounalarticle.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }

                //删除附录信息
                IsSuccess = attach.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }
            }
            //删除期刊年信息
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
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
            string sql = "UPDATE " + TABLE_NAME + " SET " + PARM_SYS_FLD_HITCOUNT + "=" + PARM_SYS_FLD_HITCOUNT + "+1 WHERE " + PARM_SYS_FLD_DOI + "='" + id + "'";
            return TPIHelper.ExecSql(sql);
        }

        /// <summary>
        /// 增加下载次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddStaticDownload(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            string sql = "UPDATE " + TABLE_NAME + " SET " + PARM_SYS_FLD_DOWNLOAD + "=" + PARM_SYS_FLD_DOWNLOAD + "+1 WHERE " + PARM_SYS_FLD_DOI + "='" + id + "'";
            return TPIHelper.ExecSql(sql);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">期刊年信息的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改期刊年信息状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
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
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_DATE='{1}',ISONLINE='{2}' WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, dateTime, isOnLine, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
