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
    public class MagazineArticle : IMagazineArticle
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["MagazineArticle"];
        #region IArticle 字段
        private const string PARM_TITLE = "TITLE";
        private const string PARM_SUBTITLE = "SUBTITLE";
        private const string PARM_FIRSTAUTHOR = "FIRSTAUTHOR";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_INSTITUTION = "INSTITUTION";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_CNABSTRACT = "CNABSTRACT";
        private const string PARM_ENTITLE = "ENTITLE";
        private const string PARM_SUBENTITLE = "SUBENTITLE";
        private const string PARM_ENAUTHOR = "ENAUTHOR";
        private const string PARM_ENABSTRACT = "ENABSTRACT";
        private const string PARM_ENKEYWORD = "ENKEYWORD";
        private const string PARM_PICPROVIDER = "PICPROVIDER";
        private const string PARM_ENPICPROVIDER = "ENPICPROVIDER";
        private const string PARM_PICAUTHOR = "PICAUTHOR";
        private const string PARM_ENPICAUTHOR = "ENPICAUTHOR";
        private const string PARM_CITATION = "CITATION";
        private const string PARM_FUND = "FUND";
        private const string PARM_YEAR = "YEAR";
        private const string PARM_ISSUE = "ISSUE";
        private const string PARM_CN = "CN";
        private const string PARM_ISSN = "ISSN";
        private const string PARM_PAGENUM = "PAGENUM";
        private const string PARM_YEARISSUE = "YEARISSUE";
        private const string PARM_THNAME = "THNAME";
        private const string PARM_ORDERNUM = "ORDERNUM";
        private const string PARM_SYS_FLD_PARENTDOI = "SYS_FLD_PARENTDOI";
        private const string PARM_COLUMNNAME = "COLUMNNAME";
        private const string PARM_BASEID = "BASEID";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_FULLTEXT = "FULLTEXT";
        private const string PARM_UPDATEDATE = "UPDATEDATE";
        private const string PARM_SYS_FLD_REFERENCE = "SYS_FLD_REFERENCE";
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
        private const string PARM_SYS_FLD_RES_LEVEL = "SYS_FLD_RES_LEVEL";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_SYS_FLD_VSM = "SYS_FLD_VSM";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_SRCFILENAME = "SYS_FLD_SRCFILENAME";
        private const string PARM_SYS_FLD_XMLPATH = "SYS_FLD_XMLPATH";
        private const string PARM_SYS_FLD_ABSTRACT = "SYS_FLD_ABSTRACT";
        private const string PARM_SYS_FLD_HITCOUNT = "SYS_FLD_HITCOUNT";
        private const string PARM_SYS_FLD_DOWNLOAD = "SYS_FLD_DOWNLOAD";
        private const string PARM_SYS_FLD_LDBID = "SYS_FLD_LDBID";
        private const string PARM_COPYRIGHTBEGINDATE = "COPYRIGHTBEGINDATE";
        private const string PARM_COPYRIGHTYEAR = "COPYRIGHTYEAR";
        private const string PARM_ALLRIGHTRESERVED = "ALLRIGHTRESERVED";
        private const string PARM_COPYRIGHTENDDATE = "COPYRIGHTENDDATE";
        private const string PARM_SYS_FLD_PARAXML = "SYS_FLD_PARAXML";
        private const string PARM_IsOnline = "IsOnline";
        private const string PARM_SYS_FLD_PARAXML_U = "SYS_FLD_PARAXML_U";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(MagazineArticleInfo item)
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
            if (!string.IsNullOrEmpty(item.SubTitle))
            {
                paramList.Add(PARM_SUBTITLE);
                paramList.Add(item.SubTitle);
            }
            if (!string.IsNullOrEmpty(item.FirstAuthor))
            {
                paramList.Add(PARM_FIRSTAUTHOR);
                paramList.Add(item.FirstAuthor);
            }
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.Institution))
            {
                paramList.Add(PARM_INSTITUTION);
                paramList.Add(item.Institution);
            }
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.CNAbstract))
            {
                paramList.Add(PARM_CNABSTRACT);
                paramList.Add(item.CNAbstract);
            }
            if (!string.IsNullOrEmpty(item.ENTitle))
            {
                paramList.Add(PARM_ENTITLE);
                paramList.Add(item.ENTitle);
            }
            if (!string.IsNullOrEmpty(item.SubEntitle))
            {
                paramList.Add(PARM_SUBENTITLE);
                paramList.Add(item.SubEntitle);
            }
            if (!string.IsNullOrEmpty(item.ENAuthor))
            {
                paramList.Add(PARM_ENAUTHOR);
                paramList.Add(item.ENAuthor);
            }
            if (!string.IsNullOrEmpty(item.ENAbstract))
            {
                paramList.Add(PARM_ENABSTRACT);
                paramList.Add(item.ENAbstract);
            }
            if (!string.IsNullOrEmpty(item.ENKeyWord))
            {
                paramList.Add(PARM_ENKEYWORD);
                paramList.Add(item.ENKeyWord);
            }
            if (!string.IsNullOrEmpty(item.Picprovider))
            {
                paramList.Add(PARM_PICPROVIDER);
                paramList.Add(item.Picprovider);
            }
            if (!string.IsNullOrEmpty(item.ENpicProvider))
            {
                paramList.Add(PARM_ENPICPROVIDER);
                paramList.Add(item.ENpicProvider);
            }
            if (!string.IsNullOrEmpty(item.Picauthor))
            {
                paramList.Add(PARM_PICAUTHOR);
                paramList.Add(item.Picauthor);
            }
            if (!string.IsNullOrEmpty(item.ENPicauthor))
            {
                paramList.Add(PARM_ENPICAUTHOR);
                paramList.Add(item.ENPicauthor);
            }
            if (!string.IsNullOrEmpty(item.Citation))
            {
                paramList.Add(PARM_CITATION);
                paramList.Add(item.Citation);
            }
            if (!string.IsNullOrEmpty(item.Fund))
            {
                paramList.Add(PARM_FUND);
                paramList.Add(item.Fund);
            }
            if (!string.IsNullOrEmpty(item.Year))
            {
                paramList.Add(PARM_YEAR);
                paramList.Add(item.Year);
            }
            if (!string.IsNullOrEmpty(item.Issue))
            {
                paramList.Add(PARM_ISSUE);
                paramList.Add(item.Issue);
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
            paramList.Add(PARM_PAGENUM);
            paramList.Add(item.PageNUM.ToString());
            if (!string.IsNullOrEmpty(item.yearissue))
            {
                paramList.Add(PARM_YEARISSUE);
                paramList.Add(item.yearissue);
            }
            if (!string.IsNullOrEmpty(item.THNAME))
            {
                paramList.Add(PARM_THNAME);
                paramList.Add(item.THNAME);
            }
            if (!string.IsNullOrEmpty(item.Ordernum))
            {
                paramList.Add(PARM_ORDERNUM);
                paramList.Add(item.Ordernum);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_parentdoi))
            {
                paramList.Add(PARM_SYS_FLD_PARENTDOI);
                paramList.Add(item.SYS_FLD_parentdoi);
            }
            if (!string.IsNullOrEmpty(item.columnname))
            {
                paramList.Add(PARM_COLUMNNAME);
                paramList.Add(item.columnname);
            }
            if (!string.IsNullOrEmpty(item.BASEID))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            }
            if (!string.IsNullOrEmpty(item.ParentDoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            {
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
            }
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
            paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
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
            if (item.CopyrightBeginDate != DateTime.MinValue)
            {
                paramList.Add(PARM_COPYRIGHTBEGINDATE);
                paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
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
            if (!string.IsNullOrEmpty(item.COPYRIGHTENDDATE))
            {
                paramList.Add(PARM_COPYRIGHTENDDATE);
                paramList.Add(item.COPYRIGHTENDDATE);
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
        public bool Update(MagazineArticleInfo item)
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
            if (!string.IsNullOrEmpty(item.SubTitle))
            {
                paramList.Add(PARM_SUBTITLE);
                paramList.Add(item.SubTitle);
            }
            if (!string.IsNullOrEmpty(item.FirstAuthor))
            {
                paramList.Add(PARM_FIRSTAUTHOR);
                paramList.Add(item.FirstAuthor);
            }
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.Institution))
            {
                paramList.Add(PARM_INSTITUTION);
                paramList.Add(item.Institution);
            }
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.CNAbstract))
            {
                paramList.Add(PARM_CNABSTRACT);
                paramList.Add(item.CNAbstract);
            }
            if (!string.IsNullOrEmpty(item.ENTitle))
            {
                paramList.Add(PARM_ENTITLE);
                paramList.Add(item.ENTitle);
            }
            if (!string.IsNullOrEmpty(item.SubEntitle))
            {
                paramList.Add(PARM_SUBENTITLE);
                paramList.Add(item.SubEntitle);
            }
            if (!string.IsNullOrEmpty(item.ENAuthor))
            {
                paramList.Add(PARM_ENAUTHOR);
                paramList.Add(item.ENAuthor);
            }
            if (!string.IsNullOrEmpty(item.ENAbstract))
            {
                paramList.Add(PARM_ENABSTRACT);
                paramList.Add(item.ENAbstract);
            }
            if (!string.IsNullOrEmpty(item.ENKeyWord))
            {
                paramList.Add(PARM_ENKEYWORD);
                paramList.Add(item.ENKeyWord);
            }
            if (!string.IsNullOrEmpty(item.Picprovider))
            {
                paramList.Add(PARM_PICPROVIDER);
                paramList.Add(item.Picprovider);
            }
            if (!string.IsNullOrEmpty(item.ENpicProvider))
            {
                paramList.Add(PARM_ENPICPROVIDER);
                paramList.Add(item.ENpicProvider);
            }
            if (!string.IsNullOrEmpty(item.Picauthor))
            {
                paramList.Add(PARM_PICAUTHOR);
                paramList.Add(item.Picauthor);
            }
            if (!string.IsNullOrEmpty(item.ENPicauthor))
            {
                paramList.Add(PARM_ENPICAUTHOR);
                paramList.Add(item.ENPicauthor);
            }
            if (!string.IsNullOrEmpty(item.Citation))
            {
                paramList.Add(PARM_CITATION);
                paramList.Add(item.Citation);
            }
            if (!string.IsNullOrEmpty(item.Fund))
            {
                paramList.Add(PARM_FUND);
                paramList.Add(item.Fund);
            }
            if (!string.IsNullOrEmpty(item.Year))
            {
                paramList.Add(PARM_YEAR);
                paramList.Add(item.Year);
            }
            if (!string.IsNullOrEmpty(item.Issue))
            {
                paramList.Add(PARM_ISSUE);
                paramList.Add(item.Issue);
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
            paramList.Add(PARM_PAGENUM);
            paramList.Add(item.PageNUM.ToString());
            if (!string.IsNullOrEmpty(item.yearissue))
            {
                paramList.Add(PARM_YEARISSUE);
                paramList.Add(item.yearissue);
            }
            if (!string.IsNullOrEmpty(item.THNAME))
            {
                paramList.Add(PARM_THNAME);
                paramList.Add(item.THNAME);
            }
            if (!string.IsNullOrEmpty(item.Ordernum))
            {
                paramList.Add(PARM_ORDERNUM);
                paramList.Add(item.Ordernum);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_parentdoi))
            {
                paramList.Add(PARM_SYS_FLD_PARENTDOI);
                paramList.Add(item.SYS_FLD_parentdoi);
            }
            if (!string.IsNullOrEmpty(item.columnname))
            {
                paramList.Add(PARM_COLUMNNAME);
                paramList.Add(item.columnname);
            }
            if (!string.IsNullOrEmpty(item.BASEID))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            }
            if (!string.IsNullOrEmpty(item.ParentDoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            {
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
            }
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
            paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
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

            paramList.Add(PARM_COPYRIGHTBEGINDATE);
            paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd HH:mm:ss"));

            paramList.Add(PARM_COPYRIGHTYEAR);
            paramList.Add(item.CopyrightYear);

            paramList.Add(PARM_ALLRIGHTRESERVED);
            paramList.Add(item.Allrightreserved);

            paramList.Add(PARM_COPYRIGHTENDDATE);
            paramList.Add(item.COPYRIGHTENDDATE);

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
        public MagazineArticleInfo GetItem(string doi)
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
                MagazineArticleInfo entry = new MagazineArticleInfo();
                #region 判断字段并赋值
                entry.Title = rs.GetValue(PARM_TITLE) ?? "";
                entry.SubTitle = rs.GetValue(PARM_SUBTITLE) ?? "";
                entry.FirstAuthor = rs.GetValue(PARM_FIRSTAUTHOR) ?? "";
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.Institution = rs.GetValue(PARM_INSTITUTION) ?? "";
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.CNAbstract = rs.GetValue(PARM_CNABSTRACT) ?? "";
                entry.ENTitle = rs.GetValue(PARM_ENTITLE) ?? "";
                entry.SubEntitle = rs.GetValue(PARM_SUBENTITLE) ?? "";
                entry.ENAuthor = rs.GetValue(PARM_ENAUTHOR) ?? "";
                entry.ENAbstract = rs.GetValue(PARM_ENABSTRACT) ?? "";
                entry.ENKeyWord = rs.GetValue(PARM_ENKEYWORD) ?? "";
                entry.Picprovider = rs.GetValue(PARM_PICPROVIDER) ?? "";
                entry.ENpicProvider = rs.GetValue(PARM_ENPICPROVIDER) ?? "";
                entry.Picauthor = rs.GetValue(PARM_PICAUTHOR) ?? "";
                entry.ENPicauthor = rs.GetValue(PARM_ENPICAUTHOR) ?? "";
                entry.Citation = rs.GetValue(PARM_CITATION) ?? "";
                entry.Fund = rs.GetValue(PARM_FUND) ?? "";
                entry.Year = rs.GetValue(PARM_YEAR) ?? "";
                entry.Issue = rs.GetValue(PARM_ISSUE) ?? "";
                entry.CN = rs.GetValue(PARM_CN) ?? "";
                entry.ISSN = rs.GetValue(PARM_ISSN) ?? "";
                entry.PageNUM = StructTrans.TransNum(rs.GetValue(PARM_PAGENUM));
                entry.yearissue = rs.GetValue(PARM_YEARISSUE) ?? "";
                entry.THNAME = rs.GetValue(PARM_THNAME) ?? "";
                entry.Ordernum = rs.GetValue(PARM_ORDERNUM) ?? "";
                entry.SYS_FLD_parentdoi = rs.GetValue(PARM_SYS_FLD_PARENTDOI) ?? "";
                entry.columnname = rs.GetValue(PARM_COLUMNNAME) ?? "";
                entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
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
                entry.SYS_FLD_RES_LEVEL = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_RES_LEVEL));
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
                entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
              //  entry.SYS_FLD_PARAXML = rs.GetValue(PARM_SYS_FLD_PARAXML) ?? "";
                entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_IsOnline));
               // entry.SYS_FLD_PARAXML_U = rs.GetValue(PARM_SYS_FLD_PARAXML_U) ?? "";
                entry.SYS_FLD_PARAXML = string.IsNullOrEmpty(rs.GetValue(PARM_SYS_FLD_PARAXML_U)) ? (rs.GetValue(PARM_SYS_FLD_PARAXML) ?? "") : rs.GetValue(PARM_SYS_FLD_PARAXML_U);
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
        public List<MagazineArticleInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<MagazineArticleInfo> entryList = new List<MagazineArticleInfo>();
                MagazineArticleInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new MagazineArticleInfo();
                    #region 判断字段并赋值
                    entry.Title = rs.GetValue(PARM_TITLE) ?? "";
                    entry.SubTitle = rs.GetValue(PARM_SUBTITLE) ?? "";
                    entry.FirstAuthor = rs.GetValue(PARM_FIRSTAUTHOR) ?? "";
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.Institution = rs.GetValue(PARM_INSTITUTION) ?? "";
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.CNAbstract = rs.GetValue(PARM_CNABSTRACT) ?? "";
                    entry.ENTitle = rs.GetValue(PARM_ENTITLE) ?? "";
                    entry.SubEntitle = rs.GetValue(PARM_SUBENTITLE) ?? "";
                    entry.ENAuthor = rs.GetValue(PARM_ENAUTHOR) ?? "";
                    entry.ENAbstract = rs.GetValue(PARM_ENABSTRACT) ?? "";
                    entry.ENKeyWord = rs.GetValue(PARM_ENKEYWORD) ?? "";
                    entry.Picprovider = rs.GetValue(PARM_PICPROVIDER) ?? "";
                    entry.ENpicProvider = rs.GetValue(PARM_ENPICPROVIDER) ?? "";
                    entry.Picauthor = rs.GetValue(PARM_PICAUTHOR) ?? "";
                    entry.ENPicauthor = rs.GetValue(PARM_ENPICAUTHOR) ?? "";
                    entry.Citation = rs.GetValue(PARM_CITATION) ?? "";
                    entry.Fund = rs.GetValue(PARM_FUND) ?? "";
                    entry.Year = rs.GetValue(PARM_YEAR) ?? "";
                    entry.Issue = rs.GetValue(PARM_ISSUE) ?? "";
                    entry.CN = rs.GetValue(PARM_CN) ?? "";
                    entry.ISSN = rs.GetValue(PARM_ISSN) ?? "";
                    entry.PageNUM = StructTrans.TransNum(rs.GetValue(PARM_PAGENUM));
                    entry.yearissue = rs.GetValue(PARM_YEARISSUE) ?? "";
                    entry.THNAME = rs.GetValue(PARM_THNAME) ?? "";
                    entry.Ordernum = rs.GetValue(PARM_ORDERNUM) ?? "";
                    entry.SYS_FLD_parentdoi = rs.GetValue(PARM_SYS_FLD_PARENTDOI) ?? "";
                    entry.columnname = rs.GetValue(PARM_COLUMNNAME) ?? "";
                    entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                    entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                    entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                    entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
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
                    entry.SYS_FLD_RES_LEVEL = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_RES_LEVEL));
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                    entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                    entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                    entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
                    entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                    entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                    entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                    entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                    entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                    entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.SYS_FLD_PARAXML = rs.GetValue(PARM_SYS_FLD_PARAXML) ?? "";
                    entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_IsOnline));

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
            List<MagazineArticleInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list == null)
            {
                return true;//list为空时，表示表中没有相关数据，在KBASE中，删除不存在的记录返回为空
            }

            Pic picture = new Pic();
            foreach (MagazineArticleInfo info in list)
            {
                //删除图片信息
                bool IsSuccess = picture.DeleteByWhere("SYS_FLD_ChapterDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }
            }

            //删除杂志文章
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
        /// <param name="id">杂志文章的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改杂志文章状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, state, id);
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
