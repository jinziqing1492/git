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
    public class YearBookYearInfo : IYearBookYearInfo
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["YearBookYearInfo"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_ENNAME = "ENNAME";
        private const string PARM_BASEID = "BASEID";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_LIABILITYDESC = "LIABILITYDESC";
        private const string PARM_EDITORG = "EDITORG";
        private const string PARM_CHARGEDEP = "CHARGEDEP";
        private const string PARM_HOSTDEP = "HOSTDEP";
        private const string PARM_LAYOUTDEP = "LAYOUTDEP";
        private const string PARM_PRINTDEP = "PRINTDEP";
        private const string PARM_ISBN = "ISBN";
        private const string PARM_ISSN = "ISSN";
        private const string PARM_CN = "CN";
        private const string PARM_ISSUEDEP = "ISSUEDEP";
        private const string PARM_ISSUEDATE = "ISSUEDATE";
        private const string PARM_YEAR = "YEAR";
        private const string PARM_COUNTRY = "COUNTRY";
        private const string PARM_CITY = "CITY";
        private const string PARM_PROVICE = "PROVICE";
        private const string PARM_DIGEST = "DIGEST";
        private const string PARM_COUNTY = "COUNTY";
        private const string PARM_THEMEWORD = "THEMEWORD";
        private const string PARM_LANGUAGE = "LANGUAGE";
        private const string PARM_EXECUTIVEEDITOR = "EXECUTIVEEDITOR";
        private const string PARM_CHARCOUNT = "CHARCOUNT";
        private const string PARM_SHEETS = "SHEETS";
        private const string PARM_PRINTING = "PRINTING";
        private const string PARM_MAXPAGENO = "MAXPAGENO";
        private const string PARM_PDFTOTALCOUNT = "PDFTOTALCOUNT";
        private const string PARM_BINDINGFORMAT = "BINDINGFORMAT";
        private const string PARM_PRICE = "PRICE";
        private const string PARM_EPRICE = "EPRICE";
        private const string PARM_FULLTEXT = "FULLTEXT";
        private const string PARM_ANNOTATIONS = "ANNOTATIONS";
        private const string PARM_TOTALVOLUME = "TOTALVOLUME";
        private const string PARM_TOTALBOOK = "TOTALBOOK";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_SYS_FLD_REFERENCE = "SYS_FLD_REFERENCE";
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
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_DEPARTMENT = "DEPARTMENT";
        private const string PARM_ISSUE = "issue";
        private const string PARM_SYS_FLD_BOOKINFO = "Sys_fld_BookInfo";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(Model.YearBookYearInfo item)
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
            if (!string.IsNullOrEmpty(item.Baseid))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.Baseid);
            }
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.LiabilityDesc))
            {
                paramList.Add(PARM_LIABILITYDESC);
                paramList.Add(item.LiabilityDesc);
            }
            if (!string.IsNullOrEmpty(item.EditOrg))
            {
                paramList.Add(PARM_EDITORG);
                paramList.Add(item.EditOrg);
            }
            if (!string.IsNullOrEmpty(item.ChargeDep))
            {
                paramList.Add(PARM_CHARGEDEP);
                paramList.Add(item.ChargeDep);
            }
            if (!string.IsNullOrEmpty(item.Hostdep))
            {
                paramList.Add(PARM_HOSTDEP);
                paramList.Add(item.Hostdep);
            }
            if (!string.IsNullOrEmpty(item.LayoutDep))
            {
                paramList.Add(PARM_LAYOUTDEP);
                paramList.Add(item.LayoutDep);
            }
            if (!string.IsNullOrEmpty(item.PrintDep))
            {
                paramList.Add(PARM_PRINTDEP);
                paramList.Add(item.PrintDep);
            }
            if (!string.IsNullOrEmpty(item.ISBN))
            {
                paramList.Add(PARM_ISBN);
                paramList.Add(item.ISBN);
            }
            if (!string.IsNullOrEmpty(item.issn))
            {
                paramList.Add(PARM_ISSN);
                paramList.Add(item.issn);
            }
            if (!string.IsNullOrEmpty(item.Cn))
            {
                paramList.Add(PARM_CN);
                paramList.Add(item.Cn);
            }
            if (!string.IsNullOrEmpty(item.IssueDep))
            {
                paramList.Add(PARM_ISSUEDEP);
                paramList.Add(item.IssueDep);
            }
            if (item.IssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ISSUEDATE);
                paramList.Add(item.IssueDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Year))
            {
                paramList.Add(PARM_YEAR);
                paramList.Add(item.Year);
            }
            if (!string.IsNullOrEmpty(item.Country))
            {
                paramList.Add(PARM_COUNTRY);
                paramList.Add(item.Country);
            }
            if (!string.IsNullOrEmpty(item.City))
            {
                paramList.Add(PARM_CITY);
                paramList.Add(item.City);
            }
            if (!string.IsNullOrEmpty(item.Provice))
            {
                paramList.Add(PARM_PROVICE);
                paramList.Add(item.Provice);
            }
            if (!string.IsNullOrEmpty(item.Digest))
            {
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
            }
            if (!string.IsNullOrEmpty(item.County))
            {
                paramList.Add(PARM_COUNTY);
                paramList.Add(item.County);
            }
            if (!string.IsNullOrEmpty(item.ThemeWord))
            {
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.ThemeWord);
            }
            if (!string.IsNullOrEmpty(item.Language))
            {
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            }
            if (!string.IsNullOrEmpty(item.ExecutiveEditor))
            {
                paramList.Add(PARM_EXECUTIVEEDITOR);
                paramList.Add(item.ExecutiveEditor);
            }
            if (!string.IsNullOrEmpty(item.CharCount))
            {
                paramList.Add(PARM_CHARCOUNT);
                paramList.Add(item.CharCount);
            }
            if (!string.IsNullOrEmpty(item.Sheets))
            {
                paramList.Add(PARM_SHEETS);
                paramList.Add(item.Sheets);
            }
            paramList.Add(PARM_PRINTING);
            paramList.Add(item.Printing.ToString());
            if (!string.IsNullOrEmpty(item.MaxPageNO))
            {
                paramList.Add(PARM_MAXPAGENO);
                paramList.Add(item.MaxPageNO);
            }
            paramList.Add(PARM_PDFTOTALCOUNT);
            paramList.Add(item.PdfTotalCount.ToString());
            if (!string.IsNullOrEmpty(item.BindingFormat))
            {
                paramList.Add(PARM_BINDINGFORMAT);
                paramList.Add(item.BindingFormat);
            }
            if (!string.IsNullOrEmpty(item.Price))
            {
                paramList.Add(PARM_PRICE);
                paramList.Add(item.Price);
            }
            if (!string.IsNullOrEmpty(item.EPrice))
            {
                paramList.Add(PARM_EPRICE);
                paramList.Add(item.EPrice);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            if (!string.IsNullOrEmpty(item.Annotations))
            {
                paramList.Add(PARM_ANNOTATIONS);
                paramList.Add(item.Annotations);
            }
            paramList.Add(PARM_TOTALVOLUME);
            paramList.Add(item.TotalVolume.ToString());
            paramList.Add(PARM_TOTALBOOK);
            paramList.Add(item.TotalBook.ToString());
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            {
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
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
            if (!string.IsNullOrEmpty(item.Department))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
            }

            if (!string.IsNullOrEmpty(item.Issue))
            {
                paramList.Add(PARM_ISSUE);
                paramList.Add(item.Issue);
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
        public bool Update(Model.YearBookYearInfo item)
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
            //if (!string.IsNullOrEmpty(item.ENName))
            //{
                paramList.Add(PARM_ENNAME);
                paramList.Add(item.ENName);
            //}
            //if (!string.IsNullOrEmpty(item.Baseid))
            //{
                paramList.Add(PARM_BASEID);
                paramList.Add(item.Baseid);
            //}
            //if (!string.IsNullOrEmpty(item.Author))
            //{
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            //}
            //if (!string.IsNullOrEmpty(item.LiabilityDesc))
            //{
                paramList.Add(PARM_LIABILITYDESC);
                paramList.Add(item.LiabilityDesc);
            //}
            //if (!string.IsNullOrEmpty(item.EditOrg))
            //{
                paramList.Add(PARM_EDITORG);
                paramList.Add(item.EditOrg);
            //}
            //if (!string.IsNullOrEmpty(item.ChargeDep))
            //{
                paramList.Add(PARM_CHARGEDEP);
                paramList.Add(item.ChargeDep);
            //}
            //if (!string.IsNullOrEmpty(item.Hostdep))
            //{
                paramList.Add(PARM_HOSTDEP);
                paramList.Add(item.Hostdep);
            //}
            //if (!string.IsNullOrEmpty(item.LayoutDep))
            //{
                paramList.Add(PARM_LAYOUTDEP);
                paramList.Add(item.LayoutDep);
            //}
            //if (!string.IsNullOrEmpty(item.PrintDep))
            //{
                paramList.Add(PARM_PRINTDEP);
                paramList.Add(item.PrintDep);
            //}
            //if (!string.IsNullOrEmpty(item.ISBN))
            //{
                paramList.Add(PARM_ISBN);
                paramList.Add(item.ISBN);
            //}
            //if (!string.IsNullOrEmpty(item.issn))
            //{
                paramList.Add(PARM_ISSN);
                paramList.Add(item.issn);
            //}
            //if (!string.IsNullOrEmpty(item.Cn))
            //{
                paramList.Add(PARM_CN);
                paramList.Add(item.Cn);
            //}
            //if (!string.IsNullOrEmpty(item.IssueDep))
            //{
                paramList.Add(PARM_ISSUEDEP);
                paramList.Add(item.IssueDep);
            //}
            //if (item.IssueDate != DateTime.MinValue)
            //{
                paramList.Add(PARM_ISSUEDATE);
                paramList.Add(item.IssueDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.Year))
            //{
                paramList.Add(PARM_YEAR);
                paramList.Add(item.Year);
            //}
            //if (!string.IsNullOrEmpty(item.Country))
            //{
                paramList.Add(PARM_COUNTRY);
                paramList.Add(item.Country);
            //}
            //if (!string.IsNullOrEmpty(item.City))
            //{
                paramList.Add(PARM_CITY);
                paramList.Add(item.City);
            //}
            //if (!string.IsNullOrEmpty(item.Provice))
            //{
                paramList.Add(PARM_PROVICE);
                paramList.Add(item.Provice);
            //}
            //if (!string.IsNullOrEmpty(item.Digest))
            //{
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
            //}
            //if (!string.IsNullOrEmpty(item.County))
            //{
                paramList.Add(PARM_COUNTY);
                paramList.Add(item.County);
            //}
            //if (!string.IsNullOrEmpty(item.ThemeWord))
            //{
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.ThemeWord);
            //}
            //if (!string.IsNullOrEmpty(item.Language))
            //{
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            //}
            //if (!string.IsNullOrEmpty(item.ExecutiveEditor))
            //{
                paramList.Add(PARM_EXECUTIVEEDITOR);
                paramList.Add(item.ExecutiveEditor);
            //}
            //if (!string.IsNullOrEmpty(item.CharCount))
            //{
                paramList.Add(PARM_CHARCOUNT);
                paramList.Add(item.CharCount);
            //}
            //if (!string.IsNullOrEmpty(item.Sheets))
            //{
                paramList.Add(PARM_SHEETS);
                paramList.Add(item.Sheets);
            //}
            paramList.Add(PARM_PRINTING);
            paramList.Add(item.Printing);
            //if (!string.IsNullOrEmpty(item.MaxPageNO))
            //{
                paramList.Add(PARM_MAXPAGENO);
                paramList.Add(item.MaxPageNO);
            //}
            paramList.Add(PARM_PDFTOTALCOUNT);
            paramList.Add(item.PdfTotalCount.ToString());
            //if (!string.IsNullOrEmpty(item.BindingFormat))
            //{
                paramList.Add(PARM_BINDINGFORMAT);
                paramList.Add(item.BindingFormat);
            //}
            //if (!string.IsNullOrEmpty(item.Price))
            //{
                paramList.Add(PARM_PRICE);
                paramList.Add(item.Price);
            //}
            //if (!string.IsNullOrEmpty(item.EPrice))
            //{
                paramList.Add(PARM_EPRICE);
                paramList.Add(item.EPrice);
            //}
            //if (!string.IsNullOrEmpty(item.FullText))
            //{
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            //}
            //if (!string.IsNullOrEmpty(item.Annotations))
            //{
                paramList.Add(PARM_ANNOTATIONS);
                paramList.Add(item.Annotations);
            //}
            paramList.Add(PARM_TOTALVOLUME);
            paramList.Add(item.TotalVolume.ToString());
            paramList.Add(PARM_TOTALBOOK);
            paramList.Add(item.TotalBook.ToString());
            //if (!string.IsNullOrEmpty(item.Keywords))
            //{
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            //}
            //if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            //{
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
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
            paramList.Add(PARM_SYS_FLD_RES_LEVEL);
            paramList.Add(item.SYS_FLD_RES_LEVEL.ToString());
            paramList.Add(PARM_SYS_FLD_ISHASATTACH);
            paramList.Add(item.SYS_FLD_ISHASATTACH.ToString());
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
            //if (!string.IsNullOrEmpty(item.SYS_FLD_OTHERFORMAT))
            //{
                paramList.Add(PARM_SYS_FLD_OTHERFORMAT);
                paramList.Add(item.SYS_FLD_OTHERFORMAT);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_PRINTFINGER))
            //{
                paramList.Add(PARM_SYS_FLD_PRINTFINGER);
                paramList.Add(item.SYS_FLD_PRINTFINGER);
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
            //if (!string.IsNullOrEmpty(item.Department))
            //{
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
            //}

            //if (!string.IsNullOrEmpty(item.Issue))
            //{
                paramList.Add(PARM_ISSUE);
                paramList.Add(item.Issue);
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
        public Model.YearBookYearInfo GetItem(string doi)
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
                Model.YearBookYearInfo entry = new Model.YearBookYearInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                entry.Baseid = rs.GetValue(PARM_BASEID) ?? "";
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.LiabilityDesc = rs.GetValue(PARM_LIABILITYDESC) ?? "";
                entry.EditOrg = rs.GetValue(PARM_EDITORG) ?? "";
                entry.ChargeDep = rs.GetValue(PARM_CHARGEDEP) ?? "";
                entry.Hostdep = rs.GetValue(PARM_HOSTDEP) ?? "";
                entry.LayoutDep = rs.GetValue(PARM_LAYOUTDEP) ?? "";
                entry.PrintDep = rs.GetValue(PARM_PRINTDEP) ?? "";
                entry.ISBN = rs.GetValue(PARM_ISBN) ?? "";
                entry.issn = rs.GetValue(PARM_ISSN) ?? "";
                entry.Cn = rs.GetValue(PARM_CN) ?? "";
                entry.IssueDep = rs.GetValue(PARM_ISSUEDEP) ?? "";
                entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                entry.Year = rs.GetValue(PARM_YEAR) ?? "";
                entry.Issue = rs.GetValue(PARM_ISSUE) ?? "";
                entry.Country = rs.GetValue(PARM_COUNTRY) ?? "";
                entry.City = rs.GetValue(PARM_CITY) ?? "";
                entry.Provice = rs.GetValue(PARM_PROVICE) ?? "";
                entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                entry.County = rs.GetValue(PARM_COUNTY) ?? "";
                entry.ThemeWord = rs.GetValue(PARM_THEMEWORD) ?? "";
                entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                entry.ExecutiveEditor = rs.GetValue(PARM_EXECUTIVEEDITOR) ?? "";
                entry.CharCount = rs.GetValue(PARM_CHARCOUNT) ?? "";
                entry.Sheets = rs.GetValue(PARM_SHEETS) ?? "";
                entry.Printing = rs.GetValue(PARM_PRINTING);
                entry.MaxPageNO = rs.GetValue(PARM_MAXPAGENO) ?? "";
                entry.PdfTotalCount = StructTrans.TransNum(rs.GetValue(PARM_PDFTOTALCOUNT));
                entry.BindingFormat = rs.GetValue(PARM_BINDINGFORMAT) ?? "";
                entry.Price = rs.GetValue(PARM_PRICE) ?? "";
                entry.EPrice = rs.GetValue(PARM_EPRICE) ?? "";
                entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                entry.Annotations = rs.GetValue(PARM_ANNOTATIONS) ?? "";
                entry.TotalVolume = StructTrans.TransNum(rs.GetValue(PARM_TOTALVOLUME));
                entry.TotalBook = StructTrans.TransNum(rs.GetValue(PARM_TOTALBOOK));
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
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
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";
                entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";
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
        public List<Model.YearBookYearInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<Model.YearBookYearInfo> entryList = new List<Model.YearBookYearInfo>();
                Model.YearBookYearInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new Model.YearBookYearInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                    entry.Baseid = rs.GetValue(PARM_BASEID) ?? "";
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.LiabilityDesc = rs.GetValue(PARM_LIABILITYDESC) ?? "";
                    entry.EditOrg = rs.GetValue(PARM_EDITORG) ?? "";
                    entry.ChargeDep = rs.GetValue(PARM_CHARGEDEP) ?? "";
                    entry.Hostdep = rs.GetValue(PARM_HOSTDEP) ?? "";
                    entry.LayoutDep = rs.GetValue(PARM_LAYOUTDEP) ?? "";
                    entry.PrintDep = rs.GetValue(PARM_PRINTDEP) ?? "";
                    entry.ISBN = rs.GetValue(PARM_ISBN) ?? "";
                    entry.issn = rs.GetValue(PARM_ISSN) ?? "";
                    entry.Cn = rs.GetValue(PARM_CN) ?? "";
                    entry.IssueDep = rs.GetValue(PARM_ISSUEDEP) ?? "";
                    entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                    entry.Year = rs.GetValue(PARM_YEAR) ?? "";
                    entry.Issue = rs.GetValue(PARM_ISSUE) ?? "";
                    entry.Country = rs.GetValue(PARM_COUNTRY) ?? "";
                    entry.City = rs.GetValue(PARM_CITY) ?? "";
                    entry.Provice = rs.GetValue(PARM_PROVICE) ?? "";
                    entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                    entry.County = rs.GetValue(PARM_COUNTY) ?? "";
                    entry.ThemeWord = rs.GetValue(PARM_THEMEWORD) ?? "";
                    entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                    entry.ExecutiveEditor = rs.GetValue(PARM_EXECUTIVEEDITOR) ?? "";
                    entry.CharCount = rs.GetValue(PARM_CHARCOUNT) ?? "";
                    entry.Sheets = rs.GetValue(PARM_SHEETS) ?? "";
                    entry.Printing = rs.GetValue(PARM_PRINTING);
                    entry.MaxPageNO = rs.GetValue(PARM_MAXPAGENO) ?? "";
                    entry.PdfTotalCount = StructTrans.TransNum(rs.GetValue(PARM_PDFTOTALCOUNT));
                    entry.BindingFormat = rs.GetValue(PARM_BINDINGFORMAT) ?? "";
                    entry.Price = rs.GetValue(PARM_PRICE) ?? "";
                    entry.EPrice = rs.GetValue(PARM_EPRICE) ?? "";
                    entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                    entry.Annotations = rs.GetValue(PARM_ANNOTATIONS) ?? "";
                    entry.TotalVolume = StructTrans.TransNum(rs.GetValue(PARM_TOTALVOLUME));
                    entry.TotalBook = StructTrans.TransNum(rs.GetValue(PARM_TOTALBOOK));
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
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
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";

                  //  entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";

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
            List<Model.YearBookYearInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list == null)
            {
                return true;//list为空时，表示表中没有相关数据，在KBASE中，删除不存在的记录返回为空
            }

            foreach (Model.YearBookYearInfo info in list)
            {
                //删除图片
                Pic p = new Pic();
                bool IsSuccess = p.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }
                //删除附件
                Attachment atta = new Attachment();
                IsSuccess = atta.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }
                //删除文章
                YearBookYearInfo cpter = new YearBookYearInfo();
                IsSuccess = cpter.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                if (!IsSuccess)
                {
                    return false;
                }
            }
            //删除年鉴年表
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
        /// <param name="id">年鉴年表的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改年鉴年表状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">年鉴年表的SYS_FLD_DOI</param>
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
