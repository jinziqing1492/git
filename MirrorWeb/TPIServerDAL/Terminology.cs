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
    public class Terminology : ITerminology
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Terminology"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_ENNAME = "ENNAME";
        private const string PARM_NAMEALLOGENEIC = "NAMEALLOGENEIC";
        private const string PARM_CONTENT = "CONTENT";
        private const string PARM_ENCONTENT = "ENCONTENT";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_AUTHORDESC = "AUTHORDESC";
        private const string PARM_METATYPE = "METATYPE";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_PARENTNAME = "PARENTNAME";
        private const string PARM_PUBDATE = "PUBDATE";
        private const string PARM_FINDDATE = "FINDDATE";
        private const string PARM_ACCESSORIES = "ACCESSORIES";
        private const string PARM_ATTRPATH = "ATTRPATH";
        private const string PARM_SYS_FLD_PARTXML = "SYS_FLD_PARTXML";
        private const string PARM_SYS_FLD_TITLE = "SYS_FLD_TITLE";
        private const string PARM_SYS_FLD_PARENTDOI = "SYS_FLD_PARENTDOI";
        private const string PARM_SYS_FLD_HASSUBENTRY = "SYS_FLD_HASSUBENTRY";
        private const string PARM_SYS_FLD_HASPARTCONTENT = "SYS_FLD_HASPARTCONTENT";
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
        private const string PARM_SYS_FLD_REFERENCE = "SYS_FLD_REFERENCE";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_ONLINEDOI = "ONLINEDOI";
        private const string PARM_SMARTSTR = "SMARTSTR";

        private const string PARM_SYS_FLD_PARAXML_U = "SYS_FLD_PARAXML_U";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(TerminologyInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.ENName))
            {
                paramList.Add(PARM_ENNAME);
                paramList.Add(item.ENName);
            }
            if (!string.IsNullOrEmpty(item.NameAllogeneic))
            {
                paramList.Add(PARM_NAMEALLOGENEIC);
                paramList.Add(item.NameAllogeneic);
            }
            if (!string.IsNullOrEmpty(item.Content))
            {
                paramList.Add(PARM_CONTENT);
                paramList.Add(item.Content);
            }
            if (!string.IsNullOrEmpty(item.ENcontent))
            {
                paramList.Add(PARM_ENCONTENT);
                paramList.Add(item.ENcontent);
            }
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.AuthorDESC))
            {
                paramList.Add(PARM_AUTHORDESC);
                paramList.Add(item.AuthorDESC);
            }
            paramList.Add(PARM_METATYPE);
            paramList.Add(item.metatype.ToString());
            if (!string.IsNullOrEmpty(item.ParentDOI))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDOI);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (item.Pubdate != DateTime.MinValue)
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.Pubdate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.Finddate != DateTime.MinValue)
            {
                paramList.Add(PARM_FINDDATE);
                paramList.Add(item.Finddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.accessories))
            {
                paramList.Add(PARM_ACCESSORIES);
                paramList.Add(item.accessories);
            }
            if (!string.IsNullOrEmpty(item.AttrPath))
            {
                paramList.Add(PARM_ATTRPATH);
                paramList.Add(item.AttrPath);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_partXml))
            {
                paramList.Add(PARM_SYS_FLD_PARTXML);
                paramList.Add(item.Sys_fld_partXml);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Title))
            {
                paramList.Add(PARM_SYS_FLD_TITLE);
                paramList.Add(item.Sys_fld_Title);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Parentdoi))
            {
                paramList.Add(PARM_SYS_FLD_PARENTDOI);
                paramList.Add(item.Sys_fld_Parentdoi);
            }
            paramList.Add(PARM_SYS_FLD_HASSUBENTRY);
            paramList.Add(item.Sys_fld_HasSubEntry.ToString());
            paramList.Add(PARM_SYS_FLD_HASPARTCONTENT);
            paramList.Add(item.Sys_fld_HasPartContent.ToString());
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
            if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            {
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
            }
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.IsOnline.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
            }
            if (!string.IsNullOrEmpty(item.Smartstr))
            {
                paramList.Add(PARM_SMARTSTR);
                paramList.Add(item.Smartstr);
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
        public bool Update(TerminologyInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.ENName))
            {
                paramList.Add(PARM_ENNAME);
                paramList.Add(item.ENName);
            }
            if (!string.IsNullOrEmpty(item.NameAllogeneic))
            {
                paramList.Add(PARM_NAMEALLOGENEIC);
                paramList.Add(item.NameAllogeneic);
            }
            if (!string.IsNullOrEmpty(item.Content))
            {
                paramList.Add(PARM_CONTENT);
                paramList.Add(item.Content);
            }
            if (!string.IsNullOrEmpty(item.ENcontent))
            {
                paramList.Add(PARM_ENCONTENT);
                paramList.Add(item.ENcontent);
            }
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.AuthorDESC))
            {
                paramList.Add(PARM_AUTHORDESC);
                paramList.Add(item.AuthorDESC);
            }
            paramList.Add(PARM_METATYPE);
            paramList.Add(item.metatype.ToString());
            if (!string.IsNullOrEmpty(item.ParentDOI))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDOI);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (item.Pubdate != DateTime.MinValue)
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.Pubdate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.Finddate != DateTime.MinValue)
            {
                paramList.Add(PARM_FINDDATE);
                paramList.Add(item.Finddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.accessories))
            {
                paramList.Add(PARM_ACCESSORIES);
                paramList.Add(item.accessories);
            }
            if (!string.IsNullOrEmpty(item.AttrPath))
            {
                paramList.Add(PARM_ATTRPATH);
                paramList.Add(item.AttrPath);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_partXml))
            {
                paramList.Add(PARM_SYS_FLD_PARTXML);
                paramList.Add(item.Sys_fld_partXml);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Title))
            {
                paramList.Add(PARM_SYS_FLD_TITLE);
                paramList.Add(item.Sys_fld_Title);
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Parentdoi))
            {
                paramList.Add(PARM_SYS_FLD_PARENTDOI);
                paramList.Add(item.Sys_fld_Parentdoi);
            }
            paramList.Add(PARM_SYS_FLD_HASSUBENTRY);
            paramList.Add(item.Sys_fld_HasSubEntry.ToString());
            paramList.Add(PARM_SYS_FLD_HASPARTCONTENT);
            paramList.Add(item.Sys_fld_HasPartContent.ToString());
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
            if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            {
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
            }
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.IsOnline.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
            }
            if (!string.IsNullOrEmpty(item.Smartstr))
            {
                paramList.Add(PARM_SMARTSTR);
                paramList.Add(item.Smartstr);
            }
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
        public TerminologyInfo GetItem(string doi)
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
                TerminologyInfo entry = new TerminologyInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                entry.NameAllogeneic = rs.GetValue(PARM_NAMEALLOGENEIC) ?? "";
                entry.Content = rs.GetValue(PARM_CONTENT) ?? "";
                entry.ENcontent = rs.GetValue(PARM_ENCONTENT) ?? "";
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.AuthorDESC = rs.GetValue(PARM_AUTHORDESC) ?? "";
                entry.metatype = StructTrans.TransNum(rs.GetValue(PARM_METATYPE));
                entry.ParentDOI = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                entry.Pubdate = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                entry.Finddate = StructTrans.TransDate(rs.GetValue(PARM_FINDDATE));
                entry.accessories = rs.GetValue(PARM_ACCESSORIES) ?? "";
                entry.AttrPath = rs.GetValue(PARM_ATTRPATH) ?? "";
               // entry.Sys_fld_partXml = rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "";
                entry.Sys_fld_Title = rs.GetValue(PARM_SYS_FLD_TITLE) ?? "";
                entry.Sys_fld_Parentdoi = rs.GetValue(PARM_SYS_FLD_PARENTDOI) ?? "";
                entry.Sys_fld_HasSubEntry = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HASSUBENTRY));
                entry.Sys_fld_HasPartContent = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HASPARTCONTENT));
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
                entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
                entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                entry.Smartstr = rs.GetValue(PARM_SMARTSTR) ?? "";
               // entry.SYS_FLD_PARAXML_U = rs.GetValue(PARM_SYS_FLD_PARAXML_U) ?? "";
                entry.SYS_FLD_PARAXML = string.IsNullOrEmpty(rs.GetValue(PARM_SYS_FLD_PARAXML_U)) ? (rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "") : rs.GetValue(PARM_SYS_FLD_PARAXML_U);

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
        /// 获取
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IList<TerminologyInfo> GetList(string strWhere)
        {
            RecordSet rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, strWhere);
            if (null == rs)
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
                IList<TerminologyInfo> slist = new List<TerminologyInfo>();
                for (int i = 0; i < rs.GetCount(); i++)
                {
                    TerminologyInfo entry = new TerminologyInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    //调用处理 含有 note的标题
                    string title = entry.Name;
                    entry.Note = Util.SubNote(ref title);
                    entry.Name = title;

                    entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                    entry.NameAllogeneic = rs.GetValue(PARM_NAMEALLOGENEIC) ?? "";
                    entry.Content = rs.GetValue(PARM_CONTENT) ?? "";
                    entry.ENcontent = rs.GetValue(PARM_ENCONTENT) ?? "";
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.AuthorDESC = rs.GetValue(PARM_AUTHORDESC) ?? "";
                    entry.metatype = StructTrans.TransNum(rs.GetValue(PARM_METATYPE));
                    entry.ParentDOI = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_PARENTDOI));
                    entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                    entry.Pubdate = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                    entry.Finddate = StructTrans.TransDate(rs.GetValue(PARM_FINDDATE));
                    entry.accessories = rs.GetValue(PARM_ACCESSORIES) ?? "";
                    entry.AttrPath = rs.GetValue(PARM_ATTRPATH) ?? "";
                    entry.Sys_fld_partXml = rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "";
                    entry.Sys_fld_Title = rs.GetValue(PARM_SYS_FLD_TITLE) ?? "";
                    entry.Sys_fld_Parentdoi =CNKI.BaseFunction.NormalFunction.ResetRedFlag( rs.GetValue(PARM_SYS_FLD_PARENTDOI));
                    entry.Sys_fld_HasSubEntry = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HASSUBENTRY));
                    entry.Sys_fld_HasPartContent = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HASPARTCONTENT));
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
                    entry.SYS_FLD_DOI = CNKI.BaseFunction.NormalFunction.ResetRedFlag( rs.GetValue(PARM_SYS_FLD_DOI));
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
                    entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
                    entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                    entry.Smartstr = rs.GetValue(PARM_SMARTSTR) ?? "";
                    #endregion
                    slist.Add(entry);

                    if (!rs.MoveNext())
                    {
                        break;
                    }
                }
                return slist;
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
        public List<TerminologyInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<TerminologyInfo> entryList = new List<TerminologyInfo>();
                TerminologyInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new TerminologyInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                    entry.NameAllogeneic = rs.GetValue(PARM_NAMEALLOGENEIC) ?? "";
                    entry.Content = rs.GetValue(PARM_CONTENT) ?? "";
                    entry.ENcontent = rs.GetValue(PARM_ENCONTENT) ?? "";
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.AuthorDESC = rs.GetValue(PARM_AUTHORDESC) ?? "";
                    entry.metatype = StructTrans.TransNum(rs.GetValue(PARM_METATYPE));
                    entry.ParentDOI = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_PARENTDOI));
                    entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                    entry.Pubdate = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                    entry.Finddate = StructTrans.TransDate(rs.GetValue(PARM_FINDDATE));
                    entry.accessories = rs.GetValue(PARM_ACCESSORIES) ?? "";
                    entry.AttrPath = rs.GetValue(PARM_ATTRPATH) ?? "";
                    entry.Sys_fld_partXml = rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "";
                    entry.Sys_fld_Title = rs.GetValue(PARM_SYS_FLD_TITLE) ?? "";
                    entry.Sys_fld_Parentdoi = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_SYS_FLD_PARENTDOI));
                    entry.Sys_fld_HasSubEntry = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HASSUBENTRY));
                    entry.Sys_fld_HasPartContent = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HASPARTCONTENT));
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
                    entry.SYS_FLD_DOI = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_SYS_FLD_DOI));
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
                    entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
                    entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                    entry.Smartstr = rs.GetValue(PARM_SMARTSTR) ?? "";
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

            //删除词条内容
            int record = 0;
            IList<TerminologyInfo> termlist = GetList(strWhere, 1, 1000, out record, false);
            Subterminology subterm = new Subterminology();
            if (termlist != null)
            {
                foreach (TerminologyInfo info in termlist)
                {
                    bool IsSuccess = subterm.DeleteByWhere("EntryDoi='" + info.SYS_FLD_DOI + "'");
                    if (!IsSuccess)
                    {
                        return false;
                    }
                    //删除图片
                    Pic p = new Pic();
                    IsSuccess = p.DeleteByWhere("Sys_fld_ChapterDoi='" + info.SYS_FLD_DOI + "'");
                    if (!IsSuccess)
                    {
                        return false;
                    }
                }
            }
            //删除术语、缩略语、工具书词条
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
        /// <param name="id">术语、缩略语、工具书词条的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改术语、缩略语、工具书词条状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">术语、缩略语、工具书词条的SYS_FLD_DOI</param>
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
        /// 获取子词条列表
        /// </summary>
        /// <param name="parentID">父分类ID</param>
        /// <returns>实体列表</returns>
        public IList<Model.TerminologyInfo> GetChildList(string parentID)
        {
            string sql = string.Format("{0} = '{1}' AND ISONLINE=1", PARM_SYS_FLD_PARENTDOI, parentID);
            return GetList(sql);
        }
        /// <summary>
        /// 获取子词条列表
        /// </summary>
        /// <param name="parentID">父分类ID</param>
        /// <returns>实体列表</returns>
        public IList<Model.TerminologyInfo> GetChildListByWhere(string where)
        {
            string sql = string.Format("{0} AND ISONLINE=1",where);
            return GetList(sql);
        }
        /// <summary>
        /// 对词条的逻辑库id进行更改
        /// </summary>
        /// <param name="ldbid">逻辑库id</param>
        /// <param name="doi">父类的doi</param>
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
            string sql = string.Format("UPDATE {0} SET SYS_FLD_LDBID='{1}' WHERE PARENTDOI='{2}'",TABLE_NAME,ldbid,doi);
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
