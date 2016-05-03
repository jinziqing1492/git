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
    public class Chapter : IChapter
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Chapter"];
        #region IArticle 字段
        private const string PARM_TITLE = "TITLE";
        private const string PARM_CONTENT = "CONTENT";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_PARENTNAME = "PARENTNAME";
        private const string PARM_PUBDATE = "PUBDATE";
        private const string PARM_FINDDATE = "FINDDATE";
        private const string PARM_DOCTYPE = "DOCTYPE";
        private const string PARM_KEYWORD = "KEYWORDS";
        private const string PARM_SYS_FLD_PARENTDOI = "SYS_FLD_PARENTDOI";
        private const string PARM_SYS_FLD_PAGENO = "SYS_FLD_PAGENO";
        private const string PARM_SYS_FLD_ISPART = "SYS_FLD_ISPART";
        private const string PARM_SYS_FLD_ORDERNUM = "SYS_FLD_ORDERNUM";
        private const string PARM_SYS_FLD_XPATH = "SYS_FLD_XPATH";
        private const string PARM_SYS_FLD_ORIGINALID = "SYS_FLD_ORIGINALID";
        private const string PARM_SYS_FLD_MARK_USERNAME = "SYS_FLD_MARK_USERNAME";
        private const string PARM_SYS_FLD_MARK_DATE = "SYS_FLD_MARK_DATE";
        private const string PARM_SYS_FLD_MARK_STATE = "SYS_FLD_MARK_STATE";
        private const string PARM_SYS_FLD_CHECK_USERNAME = "SYS_FLD_CHECK_USERNAME";
        private const string PARM_SYS_FLD_CHECK_DATE = "SYS_FLD_CHECK_DATE";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";
        private const string PARM_SYS_FLD_ERROR_DESCRIPT = "SYS_FLD_ERROR_DESCRIPT";
        private const string PARM_SYS_FLD_CLASSFICATION = "SYS_FLD_CLASSFICATION";
        private const string PARM_SYS_FLD_CATALOG = "SYS_FLD_CATALOG";
        private const string PARM_SYS_FLD_RES_LEVEL = "SYS_FLD_RES_LEVEL";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_SYS_FLD_ISHASATTACH = "SYS_FLD_ISHASATTACH";
        private const string PARM_SYS_FLD_VSM = "SYS_FLD_VSM";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_COVERPATH = "SYS_FLD_COVERPATH";
        private const string PARM_SYS_FLD_SRCFILENAME = "SYS_FLD_SRCFILENAME";
        private const string PARM_SYS_FLD_XMLPATH = "SYS_FLD_XMLPATH";
        private const string PARM_SYS_FLD_OTHERFORMAT = "SYS_FLD_OTHERFORMAT";
        private const string PARM_SYS_FLD_PRINTFINGER = "SYS_FLD_PRINTFINGER";
        private const string PARM_SYS_FLD_ABSTRACT = "SYS_FLD_ABSTRACT";
        private const string PARM_SYS_FLD_HITCOUNT = "SYS_FLD_HITCOUNT";
        private const string PARM_SYS_FLD_DOWNLOAD = "SYS_FLD_DOWNLOAD";
        private const string PARM_SYS_FLD_LDBID = "SYS_FLD_LDBID";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_ONLINEDOI = "ONLINEDOI";
        private const string PARM_COPYRIGHTENDDATE = "COPYRIGHTENDDATE";
        private const string PARM_SYS_FLD_ISIMPORT = "SYS_FLD_ISIMPORT";
        private const string PARM_SYS_FLD_PARAXML = "SYS_FLD_PARAXML";
        private const string PARM_COPYRIGHTBEGINDATE = "COPYRIGHTBEGINDATE";
        private const string PARM_COPYRIGHTYEAR = "COPYRIGHTYEAR";
        private const string PARM_ALLRIGHTRESERVED = "ALLRIGHTRESERVED";

        private const string PARM_SYS_FLD_PARAXML_U = "SYS_FLD_PARAXML_U";


        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ChapterInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Title))
            {
                paramList.Add(PARM_TITLE);
                paramList.Add(item.Title);
            }
            if (!string.IsNullOrEmpty(item.Content))
            {
                paramList.Add(PARM_CONTENT);
                paramList.Add(item.Content);
            }
            if (!string.IsNullOrEmpty(item.ParentDoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (!string.IsNullOrEmpty(item.pubdate))
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.pubdate);
            }
            if (!string.IsNullOrEmpty(item.finddate))
            {
                paramList.Add(PARM_FINDDATE);
                paramList.Add(item.finddate);
            }
            paramList.Add(PARM_DOCTYPE);
            paramList.Add(item.doctype.ToString());
            if (!string.IsNullOrEmpty(item.keyword))
            {
                paramList.Add(PARM_KEYWORD);
                paramList.Add(item.keyword);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_PARENTDOI))
            {
                paramList.Add(PARM_SYS_FLD_PARENTDOI);
                paramList.Add(item.SYS_FLD_PARENTDOI);
            }
            paramList.Add(PARM_SYS_FLD_PAGENO);
            paramList.Add(item.Sys_fld_PageNo.ToString());
            paramList.Add(PARM_SYS_FLD_ISPART);
            paramList.Add(item.SYS_FLD_ISPART.ToString());
            paramList.Add(PARM_SYS_FLD_ORDERNUM);
            paramList.Add(item.Sys_fld_ordernum.ToString());
            if (!string.IsNullOrEmpty(item.Sys_fld_xpath))
            {
                paramList.Add(PARM_SYS_FLD_XPATH);
                paramList.Add(item.Sys_fld_xpath);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_originalID))
            {
                paramList.Add(PARM_SYS_FLD_ORIGINALID);
                paramList.Add(item.Sys_fld_originalID);
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
            paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            }
            paramList.Add(PARM_SYS_FLD_ISHASATTACH);
            paramList.Add(item.SYS_FLD_ISHASATTACH.ToString());
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
            if (!string.IsNullOrEmpty(item.SYS_FLD_OTHERFORMAT))
            {
                paramList.Add(PARM_SYS_FLD_OTHERFORMAT);
                paramList.Add(item.SYS_FLD_OTHERFORMAT);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_PRINTFINGER))
            {
                paramList.Add(PARM_SYS_FLD_PRINTFINGER);
                paramList.Add(item.SYS_FLD_PRINTFINGER);
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
            if (!string.IsNullOrEmpty(item.COPYRIGHTENDDATE))
            {
                paramList.Add(PARM_COPYRIGHTENDDATE);
                paramList.Add(item.COPYRIGHTENDDATE);
            }
            paramList.Add(PARM_SYS_FLD_ISIMPORT);
            paramList.Add(item.Sys_fld_isimport.ToString());
            if (!string.IsNullOrEmpty(item.CopyrightYear))
            {
                paramList.Add(PARM_COPYRIGHTYEAR);
                paramList.Add(item.CopyrightYear);
            }
            if (!string.IsNullOrEmpty(item.Allrightreserved))
            {
                paramList.Add(PARM_ALLRIGHTRESERVED);
                paramList.Add(item.Allrightreserved);
            }
            if (item.CopyrightBeginDate != DateTime.MinValue)
            {
                paramList.Add(PARM_COPYRIGHTBEGINDATE);
                paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd HH:mm:ss"));
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
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(ChapterInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Title))
            {
                paramList.Add(PARM_TITLE);
                paramList.Add(item.Title);
            }
            if (!string.IsNullOrEmpty(item.Content))
            {
                paramList.Add(PARM_CONTENT);
                paramList.Add(item.Content);
            }
            if (!string.IsNullOrEmpty(item.ParentDoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (!string.IsNullOrEmpty(item.pubdate))
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.pubdate);
            }
            if (!string.IsNullOrEmpty(item.finddate))
            {
                paramList.Add(PARM_FINDDATE);
                paramList.Add(item.finddate);
            }
            paramList.Add(PARM_DOCTYPE);
            paramList.Add(item.doctype.ToString());
            if (!string.IsNullOrEmpty(item.keyword))
            {
                paramList.Add(PARM_KEYWORD);
                paramList.Add(item.keyword);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_PARENTDOI))
            {
                paramList.Add(PARM_SYS_FLD_PARENTDOI);
                paramList.Add(item.SYS_FLD_PARENTDOI);
            }
            paramList.Add(PARM_SYS_FLD_PAGENO);
            paramList.Add(item.Sys_fld_PageNo.ToString());
            paramList.Add(PARM_SYS_FLD_ISPART);
            paramList.Add(item.SYS_FLD_ISPART.ToString());
            paramList.Add(PARM_SYS_FLD_ORDERNUM);
            paramList.Add(item.Sys_fld_ordernum.ToString());
            if (!string.IsNullOrEmpty(item.Sys_fld_xpath))
            {
                paramList.Add(PARM_SYS_FLD_XPATH);
                paramList.Add(item.Sys_fld_xpath);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_originalID))
            {
                paramList.Add(PARM_SYS_FLD_ORIGINALID);
                paramList.Add(item.Sys_fld_originalID);
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
            paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
            paramList.Add(PARM_SYS_FLD_ISHASATTACH);
            paramList.Add(item.SYS_FLD_ISHASATTACH.ToString());
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
            if (!string.IsNullOrEmpty(item.SYS_FLD_OTHERFORMAT))
            {
                paramList.Add(PARM_SYS_FLD_OTHERFORMAT);
                paramList.Add(item.SYS_FLD_OTHERFORMAT);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_PRINTFINGER))
            {
                paramList.Add(PARM_SYS_FLD_PRINTFINGER);
                paramList.Add(item.SYS_FLD_PRINTFINGER);
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
            if (!string.IsNullOrEmpty(item.COPYRIGHTENDDATE))
            {
                paramList.Add(PARM_COPYRIGHTENDDATE);
                paramList.Add(item.COPYRIGHTENDDATE);
            }
            paramList.Add(PARM_SYS_FLD_ISIMPORT);
            paramList.Add(item.Sys_fld_isimport.ToString());

            paramList.Add(PARM_COPYRIGHTBEGINDATE);
            paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd HH:mm:ss"));

            paramList.Add(PARM_COPYRIGHTYEAR);
            paramList.Add(item.CopyrightYear);

            paramList.Add(PARM_ALLRIGHTRESERVED);
            paramList.Add(item.Allrightreserved);

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
        public ChapterInfo GetItem(string doi)
        {
            if (string.IsNullOrEmpty(doi))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
            RecordSet rs = TPIHelper.GetRecordSet_U(sqlQuery);
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
                ChapterInfo entry = new ChapterInfo();
                #region 判断字段并赋值
                entry.Title = rs.GetValue(PARM_TITLE) ?? "";
                entry.Content = rs.GetValue(PARM_CONTENT) ?? "";
                entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                entry.pubdate = rs.GetValue(PARM_PUBDATE) ?? "";
                entry.finddate = rs.GetValue(PARM_FINDDATE) ?? "";
                entry.doctype = StructTrans.TransNum(rs.GetValue(PARM_DOCTYPE));
                entry.keyword = rs.GetValue(PARM_KEYWORD) ?? "";
                entry.SYS_FLD_PARENTDOI = rs.GetValue(PARM_SYS_FLD_PARENTDOI) ?? "";
                entry.Sys_fld_PageNo = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PAGENO));
                entry.SYS_FLD_ISPART = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISPART));
                entry.Sys_fld_ordernum = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ORDERNUM));
                entry.Sys_fld_xpath = rs.GetValue(PARM_SYS_FLD_XPATH) ?? "";
                entry.Sys_fld_originalID = rs.GetValue(PARM_SYS_FLD_ORIGINALID) ?? "";
                entry.SYS_FLD_MARK_USERNAME = rs.GetValue(PARM_SYS_FLD_MARK_USERNAME) ?? "";
                entry.SYS_FLD_MARK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_MARK_DATE));
                entry.SYS_FLD_MARK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_MARK_STATE));
                entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                entry.SYS_FLD_ERROR_DESCRIPT = rs.GetValue(PARM_SYS_FLD_ERROR_DESCRIPT) ?? "";
                entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                entry.SYS_FLD_CATALOG = rs.GetValue(PARM_SYS_FLD_CATALOG) ?? "";
                entry.SYS_FLD_RES_LEVEL = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_RES_LEVEL));
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                entry.SYS_FLD_ISHASATTACH = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISHASATTACH));
                entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                entry.SYS_FLD_OTHERFORMAT = rs.GetValue(PARM_SYS_FLD_OTHERFORMAT) ?? "";
                entry.SYS_FLD_PRINTFINGER = rs.GetValue(PARM_SYS_FLD_PRINTFINGER) ?? "";
                entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
                entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));

                entry.SYS_FLD_PARAXML = string.IsNullOrEmpty(rs.GetValue(PARM_SYS_FLD_PARAXML_U)) ? (rs.GetValue(PARM_SYS_FLD_PARAXML) ?? "") : rs.GetValue(PARM_SYS_FLD_PARAXML_U);
               // entry.SYS_FLD_PARAXML_U = rs.GetValue(PARM_SYS_FLD_PARAXML_U) ?? "";

                entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";

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
        /// 根据分页获得多条数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<ChapterInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ChapterInfo> entryList = new List<ChapterInfo>();
                ChapterInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ChapterInfo();
                    #region 判断字段并赋值
                    entry.Title = rs.GetValue(PARM_TITLE) ?? "";
                    entry.Content = rs.GetValue(PARM_CONTENT) ?? "";
                    entry.ParentDoi = NormalFunction.ResetRedFlag(rs.GetValue(PARM_PARENTDOI)) ?? "";
                    entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                    entry.pubdate = rs.GetValue(PARM_PUBDATE) ?? "";
                    entry.finddate = rs.GetValue(PARM_FINDDATE) ?? "";
                    entry.doctype = StructTrans.TransNum(rs.GetValue(PARM_DOCTYPE));
                    entry.keyword = rs.GetValue(PARM_KEYWORD) ?? "";
                    entry.SYS_FLD_PARENTDOI = NormalFunction.ResetRedFlag(rs.GetValue(PARM_SYS_FLD_PARENTDOI)) ?? "";
                    entry.Sys_fld_PageNo = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PAGENO));
                    entry.SYS_FLD_ISPART = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISPART));
                    entry.Sys_fld_ordernum = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ORDERNUM));
                    entry.Sys_fld_xpath = rs.GetValue(PARM_SYS_FLD_XPATH) ?? "";
                    entry.Sys_fld_originalID = rs.GetValue(PARM_SYS_FLD_ORIGINALID) ?? "";
                    entry.SYS_FLD_MARK_USERNAME = rs.GetValue(PARM_SYS_FLD_MARK_USERNAME) ?? "";
                    entry.SYS_FLD_MARK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_MARK_DATE));
                    entry.SYS_FLD_MARK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_MARK_STATE));
                    entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                    entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                    entry.SYS_FLD_ERROR_DESCRIPT = rs.GetValue(PARM_SYS_FLD_ERROR_DESCRIPT) ?? "";
                    entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                    entry.SYS_FLD_CATALOG = rs.GetValue(PARM_SYS_FLD_CATALOG) ?? "";
                    entry.SYS_FLD_RES_LEVEL = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_RES_LEVEL));
                    entry.SYS_FLD_DOI = NormalFunction.ResetRedFlag(rs.GetValue(PARM_SYS_FLD_DOI)) ?? "";
                    entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                    entry.SYS_FLD_ISHASATTACH = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISHASATTACH));
                    entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                    entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                    entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                    entry.SYS_FLD_OTHERFORMAT = rs.GetValue(PARM_SYS_FLD_OTHERFORMAT) ?? "";
                    entry.SYS_FLD_PRINTFINGER = rs.GetValue(PARM_SYS_FLD_PRINTFINGER) ?? "";
                    entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
                    entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                    entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                    entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                    entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                    entry.SYS_FLD_PARAXML = rs.GetValue(PARM_SYS_FLD_PARAXML) ?? "";
                    entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                    entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                    entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";

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
            List<ChapterInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list != null)
            {
                foreach (ChapterInfo info in list)
                {
                    //删除图片
                    Pic p = new Pic();
                    bool IsSuccess = p.DeleteByWhere("Sys_fld_ChapterDoi='" + info.SYS_FLD_DOI + "'");
                    if (!IsSuccess)
                    {
                        return false;
                    }
                }
            }
            //删除章节
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
        /// <param name="id">章节的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改图书状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
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

        /// <summary>
        /// 对章节的逻辑库id进行更改
        /// </summary>
        /// <param name="ldbid">图书（标准）的逻辑库id</param>
        /// <param name="doi">图书（标准）doi</param>
        /// <returns></returns>
        public bool UpdateLDBID(string ldbid ,string doi)
        {
            if(string.IsNullOrEmpty(ldbid))
            {
                return false;
            }
            if(string.IsNullOrEmpty(doi))
            {
                return false;
            }
            string sql = string.Format("update {0} set SYS_FLD_LDBID='{1}' WHERE PARENTDOI='{2}'" ,TABLE_NAME ,ldbid ,doi);
            return TPIHelper.ExecSql(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetStateByWhere(string strWhere, int state)
        {
            if (string.IsNullOrWhiteSpace(strWhere))
            {
                return false;
            }
            //删除图片
            string sqlDelete = string.Format("UPDATE {0} set " + PARM_SYS_FLD_CHECK_STATE + "='{2}'  WHERE {1} ", TABLE_NAME, strWhere, state);
            return TPIHelper.ExecSql(sqlDelete);
        }
    }
}
