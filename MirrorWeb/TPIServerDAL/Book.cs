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
    public class Book : IBook
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Book"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_ENNAME = "ENNAME";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_AUTHORDESC = "AUTHORDESC";
        private const string PARM_LIABILITYFORM = "LIABILITYFORM";
        private const string PARM_OTHERLIABLE = "OTHERLIABLE";
        private const string PARM_OTHERLIABLEDESC = "OTHERLIABLEDESC";
        private const string PARM_OTHERLIABLEFORM = "OTHERLIABLEFORM";
        private const string PARM_ENAUTHOR = "ENAUTHOR";
        private const string PARM_ENAUTHORDESC = "ENAUTHORDESC";
        private const string PARM_ENLIABILITYFORM = "ENLIABILITYFORM";
        private const string PARM_ISBN = "ISBN";
        private const string PARM_ISSUEDEP = "ISSUEDEP";
        private const string PARM_FIRSTISSUEDATE = "FIRSTISSUEDATE";
        private const string PARM_PRINTNUM = "PRINTNUM";
        private const string PARM_COPYRIGHTBEGINDATE = "COPYRIGHTBEGINDATE";
        private const string PARM_COPYRIGHTYEAR = "COPYRIGHTYEAR";
        private const string PARM_ALLRIGHTRESERVED = "ALLRIGHTRESERVED";
        private const string PARM_DIGEST = "DIGEST";
        private const string PARM_ONLINESALEADVICE = "ONLINESALEADVICE";
        private const string PARM_ESSENCEDIGEST = "ESSENCEDIGEST";
        private const string PARM_THEMEWORD = "THEMEWORD";
        private const string PARM_ISSUEDATE = "ISSUEDATE";
        private const string PARM_LANGUAGE = "LANGUAGE";
        private const string PARM_EXECUTIVEEDITOR = "EXECUTIVEEDITOR";
        private const string PARM_FORMAT = "FORMAT";
        private const string PARM_CHARCOUNT = "CHARCOUNT";
        private const string PARM_SHEETS = "SHEETS";
        private const string PARM_PRINTING = "PRINTING";
        private const string PARM_MAXPAGENO = "MAXPAGENO";
        private const string PARM_PDFTOTALCOUNT = "PDFTOTALCOUNT";
        private const string PARM_BINDINGFORMAT = "BINDINGFORMAT";
        private const string PARM_PRICE = "PRICE";
        private const string PARM_EPRICE = "EPRICE";
        private const string PARM_LEGALSTATEMENT = "LEGALSTATEMENT";
        private const string PARM_FULLTEXT = "FULLTEXT";
        private const string PARM_REGISTRATIONDATE = "REGISTRATIONDATE";
        private const string PARM_ANNOTATIONS = "ANNOTATIONS";
        private const string PARM_SERIESNAME = "SERIESNAME";
        private const string PARM_SERIESENAME = "SERIESENAME";
        private const string PARM_SERIESAUTHOR = "SERIESAUTHOR";
        private const string PARM_SERIESLIABLEFORM = "SERIESLIABLEFORM";
        private const string PARM_SERIESBOOKNO = "SERIESBOOKNO";
        private const string PARM_SERIESPRICE = "SERIESPRICE";
        private const string PARM_SERIESSYNOPSIS = "SERIESSYNOPSIS";
        private const string PARM_SERIESANNOTATION = "SERIESANNOTATION";
        private const string PARM_SERIESENDISSUEDATE = "SERIESENDISSUEDATE";
        private const string PARM_SERIESISISSUE = "SERIESISISSUE";
        private const string PARM_TOTALVOLUME = "TOTALVOLUME";
        private const string PARM_TOTALBOOK = "TOTALBOOK";
        private const string PARM_TOTALCHARACTOR = "TOTALCHARACTOR";
        private const string PARM_TOTALPRINTING = "TOTALPRINTING";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_NOTE = "NOTE";
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
        private const string PARM_SYS_FLD_DBTYPE = "SYS_FLD_DBTYPE";
        private const string PARM_SYS_FLD_LDBID = "SYS_FLD_LDBID";
        private const string PARM_SYS_FLD_REFERENCE = "SYS_FLD_REFERENCE";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_ONLINEDOI = "ONLINEDOI";
        private const string PARM_BOOKID = "BOOKID";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_COPYRIGHTENDDATE = "COPYRIGHTENDDATE";
        private const string PARM_SYS_FLD_ISIMPORT = "SYS_FLD_ISIMPORT";
        private const string PARM_DEPARTMENT = "DEPARTMENT";
        private const string PARM_READTYPE = "READTYPE";

        private const string PARM_SYS_FLD_BOOKINFO = "Sys_fld_BookInfo";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(BookInfo item)
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
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.AuthorDesc))
            {
                paramList.Add(PARM_AUTHORDESC);
                paramList.Add(item.AuthorDesc);
            }
            if (!string.IsNullOrEmpty(item.LiabilityForm))
            {
                paramList.Add(PARM_LIABILITYFORM);
                paramList.Add(item.LiabilityForm);
            }
            if (!string.IsNullOrEmpty(item.OtherLiable))
            {
                paramList.Add(PARM_OTHERLIABLE);
                paramList.Add(item.OtherLiable);
            }
            if (!string.IsNullOrEmpty(item.OtherLiableDesc))
            {
                paramList.Add(PARM_OTHERLIABLEDESC);
                paramList.Add(item.OtherLiableDesc);
            }
            if (!string.IsNullOrEmpty(item.OtherLiableForm))
            {
                paramList.Add(PARM_OTHERLIABLEFORM);
                paramList.Add(item.OtherLiableForm);
            }
            if (!string.IsNullOrEmpty(item.ENauthor))
            {
                paramList.Add(PARM_ENAUTHOR);
                paramList.Add(item.ENauthor);
            }
            if (!string.IsNullOrEmpty(item.ENAuthorDesc))
            {
                paramList.Add(PARM_ENAUTHORDESC);
                paramList.Add(item.ENAuthorDesc);
            }
            if (!string.IsNullOrEmpty(item.ENLiabilityForm))
            {
                paramList.Add(PARM_ENLIABILITYFORM);
                paramList.Add(item.ENLiabilityForm);
            }
            if (!string.IsNullOrEmpty(item.ISBN))
            {
                paramList.Add(PARM_ISBN);
                paramList.Add(item.ISBN);
            }
            if (!string.IsNullOrEmpty(item.IssueDep))
            {
                paramList.Add(PARM_ISSUEDEP);
                paramList.Add(item.IssueDep);
            }
            if (item.FirstIssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_FIRSTISSUEDATE);
                paramList.Add(item.FirstIssueDate.ToString("yyyy-MM-dd"));
            }
            paramList.Add(PARM_PRINTNUM);
            paramList.Add(item.PrintNUM);
            if (item.CopyrightBeginDate != DateTime.MinValue)
            {
                paramList.Add(PARM_COPYRIGHTBEGINDATE);
                paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd"));
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
            if (!string.IsNullOrEmpty(item.Digest))
            {
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
            }
            if (!string.IsNullOrEmpty(item.OnLineSaleAdvice))
            {
                paramList.Add(PARM_ONLINESALEADVICE);
                paramList.Add(item.OnLineSaleAdvice);
            }
            if (!string.IsNullOrEmpty(item.EssenceDigest))
            {
                paramList.Add(PARM_ESSENCEDIGEST);
                paramList.Add(item.EssenceDigest);
            }
            if (!string.IsNullOrEmpty(item.ThemeWord))
            {
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.ThemeWord);
            }
            if (item.IssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ISSUEDATE);
                paramList.Add(item.IssueDate.ToString("yyyy-MM-dd"));
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
            if (!string.IsNullOrEmpty(item.Format))
            {
                paramList.Add(PARM_FORMAT);
                paramList.Add(item.Format);
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
            if (!string.IsNullOrEmpty(item.Printing))
            {
                paramList.Add(PARM_PRINTING);
                paramList.Add(item.Printing);
            }
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
            if (!string.IsNullOrEmpty(item.LegalStatement))
            {
                paramList.Add(PARM_LEGALSTATEMENT);
                paramList.Add(item.LegalStatement);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            if (item.RegistrationDate != DateTime.MinValue)
            {
                paramList.Add(PARM_REGISTRATIONDATE);
                paramList.Add(item.RegistrationDate.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(item.Annotations))
            {
                paramList.Add(PARM_ANNOTATIONS);
                paramList.Add(item.Annotations);
            }
            if (!string.IsNullOrEmpty(item.SeriesName))
            {
                paramList.Add(PARM_SERIESNAME);
                paramList.Add(item.SeriesName);
            }
            if (!string.IsNullOrEmpty(item.SeriesEName))
            {
                paramList.Add(PARM_SERIESENAME);
                paramList.Add(item.SeriesEName);
            }
            if (!string.IsNullOrEmpty(item.SeriesAuthor))
            {
                paramList.Add(PARM_SERIESAUTHOR);
                paramList.Add(item.SeriesAuthor);
            }
            if (!string.IsNullOrEmpty(item.SeriesLiableForm))
            {
                paramList.Add(PARM_SERIESLIABLEFORM);
                paramList.Add(item.SeriesLiableForm);
            }
            if (!string.IsNullOrEmpty(item.SeriesBookNo))
            {
                paramList.Add(PARM_SERIESBOOKNO);
                paramList.Add(item.SeriesBookNo);
            }
            if (!string.IsNullOrEmpty(item.SeriesPrice))
            {
                paramList.Add(PARM_SERIESPRICE);
                paramList.Add(item.SeriesPrice);
            }
            if (!string.IsNullOrEmpty(item.SeriesSynopsis))
            {
                paramList.Add(PARM_SERIESSYNOPSIS);
                paramList.Add(item.SeriesSynopsis);
            }
            if (!string.IsNullOrEmpty(item.SeriesAnnotation))
            {
                paramList.Add(PARM_SERIESANNOTATION);
                paramList.Add(item.SeriesAnnotation);
            }
            if (item.SeriesEndIssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_SERIESENDISSUEDATE);
                paramList.Add(item.SeriesEndIssueDate.ToString("yyyy-MM-dd"));
            }
            paramList.Add(PARM_SERIESISISSUE);
            paramList.Add(item.SeriesIsIssue.ToString());
            paramList.Add(PARM_TOTALVOLUME);
            paramList.Add(item.TotalVolume.ToString());
            paramList.Add(PARM_TOTALBOOK);
            paramList.Add(item.TotalBook.ToString());
            paramList.Add(PARM_TOTALCHARACTOR);
            paramList.Add(item.TotalCharactor.ToString());
            paramList.Add(PARM_TOTALPRINTING);
            paramList.Add(item.TotalPrinting.ToString());
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.Note))
            {
                paramList.Add(PARM_NOTE);
                paramList.Add(item.Note);
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
            if (!string.IsNullOrEmpty(item.BookId))
            {
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BookId);
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, id);
            return TPIHelper.ExecSql(sqlDelete);
        }

         /// <summary>
         /// 更新
         /// </summary>
         /// <param name="item"></param>
         /// <returns></returns>
        public bool Update(BookInfo item)
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
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.AuthorDesc))
            {
                paramList.Add(PARM_AUTHORDESC);
                paramList.Add(item.AuthorDesc);
            }
            if (!string.IsNullOrEmpty(item.LiabilityForm))
            {
                paramList.Add(PARM_LIABILITYFORM);
                paramList.Add(item.LiabilityForm);
            }
            if (!string.IsNullOrEmpty(item.OtherLiable))
            {
                paramList.Add(PARM_OTHERLIABLE);
                paramList.Add(item.OtherLiable);
            }
            if (!string.IsNullOrEmpty(item.OtherLiableDesc))
            {
                paramList.Add(PARM_OTHERLIABLEDESC);
                paramList.Add(item.OtherLiableDesc);
            }
            if (!string.IsNullOrEmpty(item.OtherLiableForm))
            {
                paramList.Add(PARM_OTHERLIABLEFORM);
                paramList.Add(item.OtherLiableForm);
            }
            if (!string.IsNullOrEmpty(item.ENauthor))
            {
                paramList.Add(PARM_ENAUTHOR);
                paramList.Add(item.ENauthor);
            }
            if (!string.IsNullOrEmpty(item.ENAuthorDesc))
            {
                paramList.Add(PARM_ENAUTHORDESC);
                paramList.Add(item.ENAuthorDesc);
            }
            if (!string.IsNullOrEmpty(item.ENLiabilityForm))
            {
                paramList.Add(PARM_ENLIABILITYFORM);
                paramList.Add(item.ENLiabilityForm);
            }
            if (!string.IsNullOrEmpty(item.ISBN))
            {
                paramList.Add(PARM_ISBN);
                paramList.Add(item.ISBN);
            }
            if (!string.IsNullOrEmpty(item.IssueDep))
            {
                paramList.Add(PARM_ISSUEDEP);
                paramList.Add(item.IssueDep);
            }
            if (item.FirstIssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_FIRSTISSUEDATE);
                paramList.Add(item.FirstIssueDate.ToString("yyyy-MM-dd"));
            }
            paramList.Add(PARM_PRINTNUM);
            paramList.Add(item.PrintNUM);
            if (item.CopyrightBeginDate != DateTime.MinValue)
            {
                paramList.Add(PARM_COPYRIGHTBEGINDATE);
                paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd"));
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
            if (!string.IsNullOrEmpty(item.Digest))
            {
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
            }
            if (!string.IsNullOrEmpty(item.OnLineSaleAdvice))
            {
                paramList.Add(PARM_ONLINESALEADVICE);
                paramList.Add(item.OnLineSaleAdvice);
            }
            if (!string.IsNullOrEmpty(item.EssenceDigest))
            {
                paramList.Add(PARM_ESSENCEDIGEST);
                paramList.Add(item.EssenceDigest);
            }
            if (!string.IsNullOrEmpty(item.ThemeWord))
            {
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.ThemeWord);
            }
            if (item.IssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ISSUEDATE);
                paramList.Add(item.IssueDate.ToString("yyyy-MM-dd"));
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
            if (!string.IsNullOrEmpty(item.Format))
            {
                paramList.Add(PARM_FORMAT);
                paramList.Add(item.Format);
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
            if (!string.IsNullOrEmpty(item.Printing))
            {
                paramList.Add(PARM_PRINTING);
                paramList.Add(item.Printing);
            }
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
            if (!string.IsNullOrEmpty(item.LegalStatement))
            {
                paramList.Add(PARM_LEGALSTATEMENT);
                paramList.Add(item.LegalStatement);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            if (item.RegistrationDate != DateTime.MinValue)
            {
                paramList.Add(PARM_REGISTRATIONDATE);
                paramList.Add(item.RegistrationDate.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(item.Annotations))
            {
                paramList.Add(PARM_ANNOTATIONS);
                paramList.Add(item.Annotations);
            }
            if (!string.IsNullOrEmpty(item.SeriesName))
            {
                paramList.Add(PARM_SERIESNAME);
                paramList.Add(item.SeriesName);
            }
            if (!string.IsNullOrEmpty(item.SeriesEName))
            {
                paramList.Add(PARM_SERIESENAME);
                paramList.Add(item.SeriesEName);
            }
            if (!string.IsNullOrEmpty(item.SeriesAuthor))
            {
                paramList.Add(PARM_SERIESAUTHOR);
                paramList.Add(item.SeriesAuthor);
            }
            if (!string.IsNullOrEmpty(item.SeriesLiableForm))
            {
                paramList.Add(PARM_SERIESLIABLEFORM);
                paramList.Add(item.SeriesLiableForm);
            }
            if (!string.IsNullOrEmpty(item.SeriesBookNo))
            {
                paramList.Add(PARM_SERIESBOOKNO);
                paramList.Add(item.SeriesBookNo);
            }
            if (!string.IsNullOrEmpty(item.SeriesPrice))
            {
                paramList.Add(PARM_SERIESPRICE);
                paramList.Add(item.SeriesPrice);
            }
            if (!string.IsNullOrEmpty(item.SeriesSynopsis))
            {
                paramList.Add(PARM_SERIESSYNOPSIS);
                paramList.Add(item.SeriesSynopsis);
            }
            if (!string.IsNullOrEmpty(item.SeriesAnnotation))
            {
                paramList.Add(PARM_SERIESANNOTATION);
                paramList.Add(item.SeriesAnnotation);
            }
            if (item.SeriesEndIssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_SERIESENDISSUEDATE);
                paramList.Add(item.SeriesEndIssueDate.ToString("yyyy-MM-dd"));
            }
            paramList.Add(PARM_SERIESISISSUE);
            paramList.Add(item.SeriesIsIssue.ToString());
            paramList.Add(PARM_TOTALVOLUME);
            paramList.Add(item.TotalVolume.ToString());
            paramList.Add(PARM_TOTALBOOK);
            paramList.Add(item.TotalBook.ToString());
            paramList.Add(PARM_TOTALCHARACTOR);
            paramList.Add(item.TotalCharactor.ToString());
            paramList.Add(PARM_TOTALPRINTING);
            paramList.Add(item.TotalPrinting.ToString());
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.Note))
            {
                paramList.Add(PARM_NOTE);
                paramList.Add(item.Note);
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
            if (!string.IsNullOrEmpty(item.BookId))
            {
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BookId);
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

            if (item.ReadType != 0)
            {
                paramList.Add(PARM_READTYPE);
                paramList.Add(item.ReadType.ToString());
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
        /// 按id获取item
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public BookInfo GetItem(string doi)
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
                BookInfo entry = new BookInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.AuthorDesc = rs.GetValue(PARM_AUTHORDESC) ?? "";
                entry.LiabilityForm = rs.GetValue(PARM_LIABILITYFORM) ?? "";
                entry.OtherLiable = rs.GetValue(PARM_OTHERLIABLE) ?? "";
                entry.OtherLiableDesc = rs.GetValue(PARM_OTHERLIABLEDESC) ?? "";
                entry.OtherLiableForm = rs.GetValue(PARM_OTHERLIABLEFORM) ?? "";
                entry.ENauthor = rs.GetValue(PARM_ENAUTHOR) ?? "";
                entry.ENAuthorDesc = rs.GetValue(PARM_ENAUTHORDESC) ?? "";
                entry.ENLiabilityForm = rs.GetValue(PARM_ENLIABILITYFORM) ?? "";
                entry.ISBN = rs.GetValue(PARM_ISBN) ?? "";
                entry.IssueDep = rs.GetValue(PARM_ISSUEDEP) ?? "";
                entry.FirstIssueDate = StructTrans.TransDate(rs.GetValue(PARM_FIRSTISSUEDATE));
                entry.PrintNUM = rs.GetValue(PARM_PRINTNUM);
                entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                entry.OnLineSaleAdvice = rs.GetValue(PARM_ONLINESALEADVICE) ?? "";
                entry.EssenceDigest = rs.GetValue(PARM_ESSENCEDIGEST) ?? "";
                entry.ThemeWord = rs.GetValue(PARM_THEMEWORD) ?? "";
                entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                entry.ExecutiveEditor = rs.GetValue(PARM_EXECUTIVEEDITOR) ?? "";
                entry.Format = rs.GetValue(PARM_FORMAT) ?? "";
                entry.CharCount = rs.GetValue(PARM_CHARCOUNT) ?? "";
                entry.Sheets = rs.GetValue(PARM_SHEETS) ?? "";
                entry.Printing = rs.GetValue(PARM_PRINTING);
                entry.MaxPageNO = rs.GetValue(PARM_MAXPAGENO) ?? "";
                entry.PdfTotalCount = StructTrans.TransNum(rs.GetValue(PARM_PDFTOTALCOUNT));
                entry.BindingFormat = rs.GetValue(PARM_BINDINGFORMAT) ?? "";
                entry.Price = rs.GetValue(PARM_PRICE) ?? "";
                entry.EPrice = rs.GetValue(PARM_EPRICE) ?? "";
                entry.LegalStatement = rs.GetValue(PARM_LEGALSTATEMENT) ?? "";
              //  entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                entry.RegistrationDate = StructTrans.TransDate(rs.GetValue(PARM_REGISTRATIONDATE));
                entry.Annotations = rs.GetValue(PARM_ANNOTATIONS) ?? "";
                entry.SeriesName = rs.GetValue(PARM_SERIESNAME) ?? "";
                entry.SeriesEName = rs.GetValue(PARM_SERIESENAME) ?? "";
                entry.SeriesAuthor = rs.GetValue(PARM_SERIESAUTHOR) ?? "";
                entry.SeriesLiableForm = rs.GetValue(PARM_SERIESLIABLEFORM) ?? "";
                entry.SeriesBookNo = rs.GetValue(PARM_SERIESBOOKNO) ?? "";
                entry.SeriesPrice = rs.GetValue(PARM_SERIESPRICE) ?? "";
                entry.SeriesSynopsis = rs.GetValue(PARM_SERIESSYNOPSIS) ?? "";
                entry.SeriesAnnotation = rs.GetValue(PARM_SERIESANNOTATION) ?? "";
                entry.SeriesEndIssueDate = StructTrans.TransDate(rs.GetValue(PARM_SERIESENDISSUEDATE));
                entry.SeriesIsIssue = StructTrans.TransNum(rs.GetValue(PARM_SERIESISISSUE));
                entry.TotalVolume = StructTrans.TransNum(rs.GetValue(PARM_TOTALVOLUME));
                entry.TotalBook = StructTrans.TransNum(rs.GetValue(PARM_TOTALBOOK));
                entry.TotalCharactor = StructTrans.TransNum(rs.GetValue(PARM_TOTALCHARACTOR));
                entry.TotalPrinting = StructTrans.TransNum(rs.GetValue(PARM_TOTALPRINTING));
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                string title = entry.Name;
                entry.Note = Util.SubNote(ref title);
                entry.Name = title;
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
                entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";

                entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";
                entry.ReadType = StructTrans.TransNum(rs.GetValue(PARM_READTYPE));
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
        /// 按分页获取数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<BookInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            recordCount = 0;
           // RecordSet rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, sqlWhere);
            RecordSet rs = null;
            //判断是将表中所有字段取出还是只取常用的一部分
            if (IsAll)
            {
                rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, sqlWhere);
            }
            else
            {
                string Fields = "TITLE,Author,LANGUAGE,MAXPAGENO,PRINTNUM,IssueDep,ISBN,SYS_FLD_CHECK_DATE,SYS_FLD_DOI,IssueDate,SYS_FLD_VIRTUALPATHTAG,SYS_FLD_COVERPATH,DIGEST,SYS_FLD_FILEPATH,Keywords,PRICE";
                rs = TPIHelper.GetRecordPartField(TABLE_NAME, sqlWhere, Fields);
            }

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
                List<BookInfo> entryList = new List<BookInfo>();
                BookInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new BookInfo();
                    if (IsAll)
                    {
                        #region 判断字段并赋值
                        entry.Name = rs.GetValue(PARM_NAME) ?? "";
                        entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                        entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                        entry.AuthorDesc = rs.GetValue(PARM_AUTHORDESC) ?? "";
                        entry.LiabilityForm = rs.GetValue(PARM_LIABILITYFORM) ?? "";
                        entry.OtherLiable = rs.GetValue(PARM_OTHERLIABLE) ?? "";
                        entry.OtherLiableDesc = rs.GetValue(PARM_OTHERLIABLEDESC) ?? "";
                        entry.OtherLiableForm = rs.GetValue(PARM_OTHERLIABLEFORM) ?? "";
                        entry.ENauthor = rs.GetValue(PARM_ENAUTHOR) ?? "";
                        entry.ENAuthorDesc = rs.GetValue(PARM_ENAUTHORDESC) ?? "";
                        entry.ENLiabilityForm = rs.GetValue(PARM_ENLIABILITYFORM) ?? "";
                        entry.ISBN = rs.GetValue(PARM_ISBN) ?? "";
                        entry.IssueDep = rs.GetValue(PARM_ISSUEDEP) ?? "";
                        entry.FirstIssueDate = StructTrans.TransDate(rs.GetValue(PARM_FIRSTISSUEDATE));
                        entry.PrintNUM = rs.GetValue(PARM_PRINTNUM);
                        entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                        entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                        entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                        entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                        entry.OnLineSaleAdvice = rs.GetValue(PARM_ONLINESALEADVICE) ?? "";
                        entry.EssenceDigest = rs.GetValue(PARM_ESSENCEDIGEST) ?? "";
                        entry.ThemeWord = rs.GetValue(PARM_THEMEWORD) ?? "";
                        entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                        entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                        entry.ExecutiveEditor = rs.GetValue(PARM_EXECUTIVEEDITOR) ?? "";
                        entry.Format = rs.GetValue(PARM_FORMAT) ?? "";
                        entry.CharCount = rs.GetValue(PARM_CHARCOUNT) ?? "";
                        entry.Sheets = rs.GetValue(PARM_SHEETS) ?? "";
                        entry.Printing = rs.GetValue(PARM_PRINTING);
                        entry.MaxPageNO = rs.GetValue(PARM_MAXPAGENO) ?? "";
                        entry.PdfTotalCount = StructTrans.TransNum(rs.GetValue(PARM_PDFTOTALCOUNT));
                        entry.BindingFormat = rs.GetValue(PARM_BINDINGFORMAT) ?? "";
                        entry.Price = rs.GetValue(PARM_PRICE) ?? "";
                        entry.EPrice = rs.GetValue(PARM_EPRICE) ?? "";
                        entry.LegalStatement = rs.GetValue(PARM_LEGALSTATEMENT) ?? "";
                        //  entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                        entry.RegistrationDate = StructTrans.TransDate(rs.GetValue(PARM_REGISTRATIONDATE));
                        entry.Annotations = rs.GetValue(PARM_ANNOTATIONS) ?? "";
                        entry.SeriesName = rs.GetValue(PARM_SERIESNAME) ?? "";
                        entry.SeriesEName = rs.GetValue(PARM_SERIESENAME) ?? "";
                        entry.SeriesAuthor = rs.GetValue(PARM_SERIESAUTHOR) ?? "";
                        entry.SeriesLiableForm = rs.GetValue(PARM_SERIESLIABLEFORM) ?? "";
                        entry.SeriesBookNo = rs.GetValue(PARM_SERIESBOOKNO) ?? "";
                        entry.SeriesPrice = rs.GetValue(PARM_SERIESPRICE) ?? "";
                        entry.SeriesSynopsis = rs.GetValue(PARM_SERIESSYNOPSIS) ?? "";
                        entry.SeriesAnnotation = rs.GetValue(PARM_SERIESANNOTATION) ?? "";
                        entry.SeriesEndIssueDate = StructTrans.TransDate(rs.GetValue(PARM_SERIESENDISSUEDATE));
                        entry.SeriesIsIssue = StructTrans.TransNum(rs.GetValue(PARM_SERIESISISSUE));
                        entry.TotalVolume = StructTrans.TransNum(rs.GetValue(PARM_TOTALVOLUME));
                        entry.TotalBook = StructTrans.TransNum(rs.GetValue(PARM_TOTALBOOK));
                        entry.TotalCharactor = StructTrans.TransNum(rs.GetValue(PARM_TOTALCHARACTOR));
                        entry.TotalPrinting = StructTrans.TransNum(rs.GetValue(PARM_TOTALPRINTING));
                        entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                        entry.Note = rs.GetValue(PARM_NOTE) ?? "";
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
                        entry.SYS_FLD_DOI =  CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_SYS_FLD_DOI)) ?? "";
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
                        entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                        entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                        entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                        entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                        entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                        entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";
                        entry.ReadType = StructTrans.TransNum(rs.GetValue(PARM_READTYPE));

                        #endregion
                    }
                    else
                    {
                        //TITLE,IssueDep,ISBN,SYS_FLD_CHECK_DATE,IssueDate,SYS_FLD_VIRTUALPATHTAG,SYS_FLD_COVERPATH,SYS_FLD_ABSTRACT
                        entry.Name = rs.GetValue(PARM_NAME);
                        //调用处理 含有 note的标题
                        string title = entry.Name;
                        entry.Note = Util.SubNote(ref title);
                        entry.Name = title;
                        entry.ISBN = rs.GetValue(PARM_ISBN);
                        entry.Author = rs.GetValue(PARM_AUTHOR);
                        entry.Language = rs.GetValue(PARM_LANGUAGE);
                        entry.MaxPageNO = rs.GetValue(PARM_MAXPAGENO);
                        entry.PrintNUM = rs.GetValue(PARM_PRINTNUM);
                        entry.IssueDep = rs.GetValue(PARM_ISSUEDEP);
                        entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                        entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                        entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG);
                        entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH);
                        entry.Digest = rs.GetValue(PARM_DIGEST);
                        entry.SYS_FLD_DOI = CNKI.BaseFunction.NormalFunction.ResetRedFlag(rs.GetValue(PARM_SYS_FLD_DOI)) ?? "";
                        entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH);
                        entry.Keywords = rs.GetValue(PARM_KEYWORDS);
                        entry.Price = rs.GetValue(PARM_PRICE) ?? "";
                        //  entry.SYS_FLD_CATALOG = rs.GetValue(PARM_SYS_FLD_CATALOG);
                      //  entry.Sys_fld_BookInfo = rs.GetValue(PARM_SYS_FLD_BOOKINFO) ?? "";
                       // entry.SYS_FLD_DBTYPE = DataBaseType.BOOKTDATA;
                    }
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
            List<BookInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list != null)
            {
                foreach (BookInfo info in list)
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
                    //删除章节
                    Chapter cpter = new Chapter();
                    IsSuccess = cpter.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
                    if (!IsSuccess)
                    {
                        return false;
                    }
                }
            }
            //删除图书
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">图书的SYS_FLD_DOI</param>
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
        /// <param name="id">图书的SYS_FLD_DOI</param>
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
        /// 获取切词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetWord(string input)
        {
            return TPIHelper.GetWord(input);
        }

       /// <summary>
       /// 根据字段列表获取数据
       /// </summary>
       /// <param name="Fields"></param>
       /// <param name="tableName"></param>
       /// <param name="sqlWhere"></param>
       /// <param name="pageNo"></param>
       /// <param name="pageCount"></param>
       /// <param name="recordCount"></param>
       /// <returns></returns>
        public List<List<string>> GetDataByFieldList(List<string> Fields, string tableName, string sqlWhere, int pageNo, int pageCount, out int recordCount)
        {
            recordCount = 0;
            // RecordSet rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, sqlWhere);
            RecordSet rs = null;
            //判断是将表中所有字段取出还是只取常用的一部分
            if (Fields == null)
            {
                Fields = new List<string>();
                Fields.Add("*");
            }

           StringBuilder strFileds=new StringBuilder ();
           for (int i = 0; i < Fields.Count; i++)
           {
               if (i == 0)
               {
                   strFileds.Append(Fields[i]);
               }
               else
               {
                   strFileds.Append("," + Fields[i]);
               }
           }
           rs = TPIHelper.GetRecordPartField(tableName, sqlWhere, strFileds.ToString());
      

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
  
            //  获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<List<string>> entryList = new List<List<string>>();
                List<string> entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new List<string>();
                    if (Fields.Count == 1)
                    {
                        if (Fields[0] == "*")
                        {
                            int fieldcount = rs.GetFieldCount();
                            for (int j = 0; j < fieldcount; j++)
                            {
                                entry.Add(rs.GetValue(j) ?? "");
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Fields.Count; j++)
                        {
                            entry.Add(rs.GetValue(j) ?? "");
                        }
                    }
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
    }
}
