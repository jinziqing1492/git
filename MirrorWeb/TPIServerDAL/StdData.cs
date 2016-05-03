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
    public class StdData : IStdData
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Stddata"];
        #region IArticle 字段
        private const string PARM_STDTYPE = "STDTYPE";
        private const string PARM_STDNO = "STDNO";
        private const string PARM_NAME = "NAME";
        private const string PARM_ENNAME = "ENNAME";
        private const string PARM_OTHERNAME = "OTHERNAME";
        private const string PARM_PARTNAME = "PARTNAME";
        private const string PARM_ENPARTNAME = "ENPARTNAME";
        private const string PARM_OTHERPARTNAME = "OTHERPARTNAME";
        private const string PARM_LANGUAGE = "LANGUAGE";
        private const string PARM_STAGE = "STAGE";
        private const string PARM_DATEISSUED = "DATEISSUED";
        private const string PARM_DATEIMPLEMENT = "DATEIMPLEMENT";
        private const string PARM_STATUS = "STATUS";
        private const string PARM_VERSION = "VERSION";
        private const string PARM_RECORDNO = "RECORDNO";
        private const string PARM_APPROVEDEP = "APPROVEDEP";
        private const string PARM_HOSTINSTITUTION = "HOSTINSTITUTION";
        private const string PARM_PROPOSEDEP = "PROPOSEDEP";
        private const string PARM_BELONGDEP = "BELONGDEP";
        private const string PARM_DRAFTDEP = "DRAFTDEP";
        private const string PARM_DRAFTPERSON = "DRAFTPERSON";
        private const string PARM_RMTHEAD = "RMTHEAD";
        private const string PARM_STYLEFORMATREVIEW = "STYLEFORMATREVIEW";
        private const string PARM_EXPLAINDEP = "EXPLAINDEP";
        private const string PARM_COMPILEDEP = "COMPILEDEP";
        private const string PARM_PUBLISHDEP = "PUBLISHDEP";
        private const string PARM_ISSUEDDEP = "ISSUEDDEP";
        private const string PARM_STDNOSERIES = "STDNOSERIES";
        private const string PARM_PARTSTDNO = "PARTSTDNO";
        private const string PARM_REPLACESTDNO = "REPLACESTDNO";
        private const string PARM_PRERELEASE = "PRERELEASE";
        private const string PARM_INTERNALSTDNO = "INTERNALSTDNO";
        private const string PARM_INTERNALSTDNAME = "INTERNALSTDNAME";
        private const string PARM_CONSISTENCY = "CONSISTENCY";
        private const string PARM_SCOPE = "SCOPE";
        private const string PARM_FULLTEXT = "FULLTEXT";
        private const string PARM_FULLTEXTLENGTH = "FULLTEXTLENGTH";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_DIGEST = "DIGEST";
        private const string PARM_REFSTDNO = "REFSTDNO";
        private const string PARM_ACCESSORIES = "ACCESSORIES";
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
        private const string PARM_BOOKID = "BOOKID";
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
        public bool Add(StdDataInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.stdtype))
            {
                paramList.Add(PARM_STDTYPE);
                paramList.Add(item.stdtype);
            }
            if (!string.IsNullOrEmpty(item.stdno))
            {
                paramList.Add(PARM_STDNO);
                paramList.Add(item.stdno);
            }
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
            if (!string.IsNullOrEmpty(item.otherName))
            {
                paramList.Add(PARM_OTHERNAME);
                paramList.Add(item.otherName);
            }
            if (!string.IsNullOrEmpty(item.PartName))
            {
                paramList.Add(PARM_PARTNAME);
                paramList.Add(item.PartName);
            }
            if (!string.IsNullOrEmpty(item.ENPartName))
            {
                paramList.Add(PARM_ENPARTNAME);
                paramList.Add(item.ENPartName);
            }
            if (!string.IsNullOrEmpty(item.OtherPartName))
            {
                paramList.Add(PARM_OTHERPARTNAME);
                paramList.Add(item.OtherPartName);
            }
            if (!string.IsNullOrEmpty(item.Language))
            {
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            }
            if (!string.IsNullOrEmpty(item.stage))
            {
                paramList.Add(PARM_STAGE);
                paramList.Add(item.stage);
            }
            if (item.Dateissued != DateTime.MinValue)
            {
                paramList.Add(PARM_DATEISSUED);
                paramList.Add(item.Dateissued.ToString("yyyy-MM-dd"));
            }
            if (item.DateImplement != DateTime.MinValue)
            {
                paramList.Add(PARM_DATEIMPLEMENT);
                paramList.Add(item.DateImplement.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(item.Status))
            {
                paramList.Add(PARM_STATUS);
                paramList.Add(item.Status);
            }
            if (!string.IsNullOrEmpty(item.Version))
            {
                paramList.Add(PARM_VERSION);
                paramList.Add(item.Version);
            }
            if (!string.IsNullOrEmpty(item.RecordNO))
            {
                paramList.Add(PARM_RECORDNO);
                paramList.Add(item.RecordNO);
            }
            if (!string.IsNullOrEmpty(item.ApproveDep))
            {
                paramList.Add(PARM_APPROVEDEP);
                paramList.Add(item.ApproveDep);
            }
            if (!string.IsNullOrEmpty(item.HostInstitution))
            {
                paramList.Add(PARM_HOSTINSTITUTION);
                paramList.Add(item.HostInstitution);
            }
            if (!string.IsNullOrEmpty(item.ProposeDep))
            {
                paramList.Add(PARM_PROPOSEDEP);
                paramList.Add(item.ProposeDep);
            }
            if (!string.IsNullOrEmpty(item.BelongDep))
            {
                paramList.Add(PARM_BELONGDEP);
                paramList.Add(item.BelongDep);
            }
            if (!string.IsNullOrEmpty(item.DraftDep))
            {
                paramList.Add(PARM_DRAFTDEP);
                paramList.Add(item.DraftDep);
            }
            if (!string.IsNullOrEmpty(item.DraftPerson))
            {
                paramList.Add(PARM_DRAFTPERSON);
                paramList.Add(item.DraftPerson);
            }
            if (!string.IsNullOrEmpty(item.RMTHead))
            {
                paramList.Add(PARM_RMTHEAD);
                paramList.Add(item.RMTHead);
            }
            if (!string.IsNullOrEmpty(item.StyleFormatReview))
            {
                paramList.Add(PARM_STYLEFORMATREVIEW);
                paramList.Add(item.StyleFormatReview);
            }
            if (!string.IsNullOrEmpty(item.ExplainDep))
            {
                paramList.Add(PARM_EXPLAINDEP);
                paramList.Add(item.ExplainDep);
            }
            if (!string.IsNullOrEmpty(item.CompileDep))
            {
                paramList.Add(PARM_COMPILEDEP);
                paramList.Add(item.CompileDep);
            }
            if (!string.IsNullOrEmpty(item.PublishDep))
            {
                paramList.Add(PARM_PUBLISHDEP);
                paramList.Add(item.PublishDep);
            }
            if (!string.IsNullOrEmpty(item.IssuedDep))
            {
                paramList.Add(PARM_ISSUEDDEP);
                paramList.Add(item.IssuedDep);
            }
            if (!string.IsNullOrEmpty(item.StdNoseries))
            {
                paramList.Add(PARM_STDNOSERIES);
                paramList.Add(item.StdNoseries);
            }
            if (!string.IsNullOrEmpty(item.PartstdNo))
            {
                paramList.Add(PARM_PARTSTDNO);
                paramList.Add(item.PartstdNo);
            }
            if (!string.IsNullOrEmpty(item.replaceStdNo))
            {
                paramList.Add(PARM_REPLACESTDNO);
                paramList.Add(item.replaceStdNo);
            }
            if (!string.IsNullOrEmpty(item.PreRelease))
            {
                paramList.Add(PARM_PRERELEASE);
                paramList.Add(item.PreRelease);
            }
            if (!string.IsNullOrEmpty(item.Internalstdno))
            {
                paramList.Add(PARM_INTERNALSTDNO);
                paramList.Add(item.Internalstdno);
            }
            if (!string.IsNullOrEmpty(item.Internalstdname))
            {
                paramList.Add(PARM_INTERNALSTDNAME);
                paramList.Add(item.Internalstdname);
            }
            if (!string.IsNullOrEmpty(item.Consistency))
            {
                paramList.Add(PARM_CONSISTENCY);
                paramList.Add(item.Consistency);
            }
            if (!string.IsNullOrEmpty(item.Scope))
            {
                paramList.Add(PARM_SCOPE);
                paramList.Add(item.Scope);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            paramList.Add(PARM_FULLTEXTLENGTH);
            paramList.Add(item.Fulltextlength.ToString());
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.Digest))
            {
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
            }
            if (!string.IsNullOrEmpty(item.RefstdNo))
            {
                paramList.Add(PARM_REFSTDNO);
                paramList.Add(item.RefstdNo);
            }
            if (!string.IsNullOrEmpty(item.ACCESSORIES))
            {
                paramList.Add(PARM_ACCESSORIES);
                paramList.Add(item.ACCESSORIES);
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
            if (!string.IsNullOrEmpty(item.BookId))
            {
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BookId);
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
        public bool Update(StdDataInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            //if (!string.IsNullOrEmpty(item.stdtype))
            //{
                paramList.Add(PARM_STDTYPE);
                paramList.Add(item.stdtype);
            //}
            //if (!string.IsNullOrEmpty(item.stdno))
            //{
                paramList.Add(PARM_STDNO);
                paramList.Add(item.stdno);
            //}
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
            //if (!string.IsNullOrEmpty(item.otherName))
            //{
                paramList.Add(PARM_OTHERNAME);
                paramList.Add(item.otherName);
            //}
            //if (!string.IsNullOrEmpty(item.PartName))
            //{
                paramList.Add(PARM_PARTNAME);
                paramList.Add(item.PartName);
            //}
            //if (!string.IsNullOrEmpty(item.ENPartName))
            //{
                paramList.Add(PARM_ENPARTNAME);
                paramList.Add(item.ENPartName);
            //}
            //if (!string.IsNullOrEmpty(item.OtherPartName))
            //{
                paramList.Add(PARM_OTHERPARTNAME);
                paramList.Add(item.OtherPartName);
            //}
            //if (!string.IsNullOrEmpty(item.Language))
            //{
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            //}
            //if (!string.IsNullOrEmpty(item.stage))
            //{
                paramList.Add(PARM_STAGE);
                paramList.Add(item.stage);
            //}
            //if (item.Dateissued != DateTime.MinValue)
            //{
                paramList.Add(PARM_DATEISSUED);
                paramList.Add(item.Dateissued.ToString("yyyy-MM-dd"));
            //}
            //if (item.DateImplement != DateTime.MinValue)
            //{
                paramList.Add(PARM_DATEIMPLEMENT);
                paramList.Add(item.DateImplement.ToString("yyyy-MM-dd"));
            //}
            //if (!string.IsNullOrEmpty(item.Status))
            //{
                paramList.Add(PARM_STATUS);
                paramList.Add(item.Status);
            //}
            //if (!string.IsNullOrEmpty(item.Version))
            //{
                paramList.Add(PARM_VERSION);
                paramList.Add(item.Version);
            //}
            //if (!string.IsNullOrEmpty(item.RecordNO))
            //{
                paramList.Add(PARM_RECORDNO);
                paramList.Add(item.RecordNO);
            //}
            //if (!string.IsNullOrEmpty(item.ApproveDep))
            //{
                paramList.Add(PARM_APPROVEDEP);
                paramList.Add(item.ApproveDep);
            //}
            //if (!string.IsNullOrEmpty(item.HostInstitution))
            //{
                paramList.Add(PARM_HOSTINSTITUTION);
                paramList.Add(item.HostInstitution);
            //}
            //if (!string.IsNullOrEmpty(item.ProposeDep))
            //{
                paramList.Add(PARM_PROPOSEDEP);
                paramList.Add(item.ProposeDep);
            //}
            //if (!string.IsNullOrEmpty(item.BelongDep))
            //{
                paramList.Add(PARM_BELONGDEP);
                paramList.Add(item.BelongDep);
            //}
            //if (!string.IsNullOrEmpty(item.DraftDep))
            //{
                paramList.Add(PARM_DRAFTDEP);
                paramList.Add(item.DraftDep);
            //}
            //if (!string.IsNullOrEmpty(item.DraftPerson))
            //{
                paramList.Add(PARM_DRAFTPERSON);
                paramList.Add(item.DraftPerson);
            //}
            //if (!string.IsNullOrEmpty(item.RMTHead))
            //{
                paramList.Add(PARM_RMTHEAD);
                paramList.Add(item.RMTHead);
            //}
            //if (!string.IsNullOrEmpty(item.StyleFormatReview))
            //{
                paramList.Add(PARM_STYLEFORMATREVIEW);
                paramList.Add(item.StyleFormatReview);
            //}
            //if (!string.IsNullOrEmpty(item.ExplainDep))
            //{
                paramList.Add(PARM_EXPLAINDEP);
                paramList.Add(item.ExplainDep);
            //}
            //if (!string.IsNullOrEmpty(item.CompileDep))
            //{
                paramList.Add(PARM_COMPILEDEP);
                paramList.Add(item.CompileDep);
            //}
            //if (!string.IsNullOrEmpty(item.PublishDep))
            //{
                paramList.Add(PARM_PUBLISHDEP);
                paramList.Add(item.PublishDep);
            //}
            //if (!string.IsNullOrEmpty(item.IssuedDep))
            //{
                paramList.Add(PARM_ISSUEDDEP);
                paramList.Add(item.IssuedDep);
            //}
            //if (!string.IsNullOrEmpty(item.StdNoseries))
            //{
                paramList.Add(PARM_STDNOSERIES);
                paramList.Add(item.StdNoseries);
            //}
            //if (!string.IsNullOrEmpty(item.PartstdNo))
            //{
                paramList.Add(PARM_PARTSTDNO);
                paramList.Add(item.PartstdNo);
            //}
            //if (!string.IsNullOrEmpty(item.replaceStdNo))
            //{
                paramList.Add(PARM_REPLACESTDNO);
                paramList.Add(item.replaceStdNo);
            //}
            //if (!string.IsNullOrEmpty(item.PreRelease))
            //{
                paramList.Add(PARM_PRERELEASE);
                paramList.Add(item.PreRelease);
            //}
            //if (!string.IsNullOrEmpty(item.Internalstdno))
            //{
                paramList.Add(PARM_INTERNALSTDNO);
                paramList.Add(item.Internalstdno);
            //}
            //if (!string.IsNullOrEmpty(item.Internalstdname))
            //{
                paramList.Add(PARM_INTERNALSTDNAME);
                paramList.Add(item.Internalstdname);
            //}
            //if (!string.IsNullOrEmpty(item.Consistency))
            //{
                paramList.Add(PARM_CONSISTENCY);
                paramList.Add(item.Consistency);
            //}
            //if (!string.IsNullOrEmpty(item.Scope))
            //{
                paramList.Add(PARM_SCOPE);
                paramList.Add(item.Scope);
            //}
            //if (!string.IsNullOrEmpty(item.FullText))
            //{
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            //}
            paramList.Add(PARM_FULLTEXTLENGTH);
            paramList.Add(item.Fulltextlength.ToString());
            //if (!string.IsNullOrEmpty(item.Keywords))
            //{
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            //}
            //if (!string.IsNullOrEmpty(item.Digest))
            //{
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
            //}
            //if (!string.IsNullOrEmpty(item.RefstdNo))
            //{
                paramList.Add(PARM_REFSTDNO);
                paramList.Add(item.RefstdNo);
            //}
            //if (!string.IsNullOrEmpty(item.ACCESSORIES))
            //{
                paramList.Add(PARM_ACCESSORIES);
                paramList.Add(item.ACCESSORIES);
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
            //if (!string.IsNullOrEmpty(item.BookId))
            //{
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BookId);
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
        public StdDataInfo GetItem(string doi)
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
                StdDataInfo entry = new StdDataInfo();
                #region 判断字段并赋值
                entry.stdtype = rs.GetValue(PARM_STDTYPE) ?? "";
                entry.stdno = rs.GetValue(PARM_STDNO) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                entry.otherName = rs.GetValue(PARM_OTHERNAME) ?? "";
                entry.PartName = rs.GetValue(PARM_PARTNAME) ?? "";
                entry.ENPartName = rs.GetValue(PARM_ENPARTNAME) ?? "";
                entry.OtherPartName = rs.GetValue(PARM_OTHERPARTNAME) ?? "";
                entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                entry.stage = rs.GetValue(PARM_STAGE) ?? "";
                entry.Dateissued = StructTrans.TransDate(rs.GetValue(PARM_DATEISSUED));
                entry.DateImplement = StructTrans.TransDate(rs.GetValue(PARM_DATEIMPLEMENT));
                entry.Status = rs.GetValue(PARM_STATUS) ?? "";
                entry.Version = rs.GetValue(PARM_VERSION) ?? "";
                entry.RecordNO = rs.GetValue(PARM_RECORDNO) ?? "";
                entry.ApproveDep = rs.GetValue(PARM_APPROVEDEP) ?? "";
                entry.HostInstitution = rs.GetValue(PARM_HOSTINSTITUTION) ?? "";
                entry.ProposeDep = rs.GetValue(PARM_PROPOSEDEP) ?? "";
                entry.BelongDep = rs.GetValue(PARM_BELONGDEP) ?? "";
                entry.DraftDep = rs.GetValue(PARM_DRAFTDEP) ?? "";
                entry.DraftPerson = rs.GetValue(PARM_DRAFTPERSON) ?? "";
                entry.RMTHead = rs.GetValue(PARM_RMTHEAD) ?? "";
                entry.StyleFormatReview = rs.GetValue(PARM_STYLEFORMATREVIEW) ?? "";
                entry.ExplainDep = rs.GetValue(PARM_EXPLAINDEP) ?? "";
                entry.CompileDep = rs.GetValue(PARM_COMPILEDEP) ?? "";
                entry.PublishDep = rs.GetValue(PARM_PUBLISHDEP) ?? "";
                entry.IssuedDep = rs.GetValue(PARM_ISSUEDDEP) ?? "";
                entry.StdNoseries = rs.GetValue(PARM_STDNOSERIES) ?? "";
                entry.PartstdNo = rs.GetValue(PARM_PARTSTDNO) ?? "";
                entry.replaceStdNo = rs.GetValue(PARM_REPLACESTDNO) ?? "";
                entry.PreRelease = rs.GetValue(PARM_PRERELEASE) ?? "";
                entry.Internalstdno = rs.GetValue(PARM_INTERNALSTDNO) ?? "";
                entry.Internalstdname = rs.GetValue(PARM_INTERNALSTDNAME) ?? "";
                entry.Consistency = rs.GetValue(PARM_CONSISTENCY) ?? "";
                entry.Scope = rs.GetValue(PARM_SCOPE) ?? "";
                entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                entry.Fulltextlength = StructTrans.TransNum(rs.GetValue(PARM_FULLTEXTLENGTH));
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                entry.RefstdNo = rs.GetValue(PARM_REFSTDNO) ?? "";
                entry.ACCESSORIES = rs.GetValue(PARM_ACCESSORIES) ?? "";
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
                entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
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
        /// 根据分页获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<StdDataInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<StdDataInfo> entryList = new List<StdDataInfo>();
                StdDataInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new StdDataInfo();
                    #region 判断字段并赋值
                    entry.stdtype = rs.GetValue(PARM_STDTYPE) ?? "";
                    entry.stdno = rs.GetValue(PARM_STDNO) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                    entry.otherName = rs.GetValue(PARM_OTHERNAME) ?? "";
                    entry.PartName = rs.GetValue(PARM_PARTNAME) ?? "";
                    entry.ENPartName = rs.GetValue(PARM_ENPARTNAME) ?? "";
                    entry.OtherPartName = rs.GetValue(PARM_OTHERPARTNAME) ?? "";
                    entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                    entry.stage = rs.GetValue(PARM_STAGE) ?? "";
                    entry.Dateissued = StructTrans.TransDate(rs.GetValue(PARM_DATEISSUED));
                    entry.DateImplement = StructTrans.TransDate(rs.GetValue(PARM_DATEIMPLEMENT));
                    entry.Status = rs.GetValue(PARM_STATUS) ?? "";
                    entry.Version = rs.GetValue(PARM_VERSION) ?? "";
                    entry.RecordNO = rs.GetValue(PARM_RECORDNO) ?? "";
                    entry.ApproveDep = rs.GetValue(PARM_APPROVEDEP) ?? "";
                    entry.HostInstitution = rs.GetValue(PARM_HOSTINSTITUTION) ?? "";
                    entry.ProposeDep = rs.GetValue(PARM_PROPOSEDEP) ?? "";
                    entry.BelongDep = rs.GetValue(PARM_BELONGDEP) ?? "";
                    entry.DraftDep = rs.GetValue(PARM_DRAFTDEP) ?? "";
                    entry.DraftPerson = rs.GetValue(PARM_DRAFTPERSON) ?? "";
                    entry.RMTHead = rs.GetValue(PARM_RMTHEAD) ?? "";
                    entry.StyleFormatReview = rs.GetValue(PARM_STYLEFORMATREVIEW) ?? "";
                    entry.ExplainDep = rs.GetValue(PARM_EXPLAINDEP) ?? "";
                    entry.CompileDep = rs.GetValue(PARM_COMPILEDEP) ?? "";
                    entry.PublishDep = rs.GetValue(PARM_PUBLISHDEP) ?? "";
                    entry.IssuedDep = rs.GetValue(PARM_ISSUEDDEP) ?? "";
                    entry.StdNoseries = rs.GetValue(PARM_STDNOSERIES) ?? "";
                    entry.PartstdNo = rs.GetValue(PARM_PARTSTDNO) ?? "";
                    entry.replaceStdNo = rs.GetValue(PARM_REPLACESTDNO) ?? "";
                    entry.PreRelease = rs.GetValue(PARM_PRERELEASE) ?? "";
                    entry.Internalstdno = rs.GetValue(PARM_INTERNALSTDNO) ?? "";
                    entry.Internalstdname = rs.GetValue(PARM_INTERNALSTDNAME) ?? "";
                    entry.Consistency = rs.GetValue(PARM_CONSISTENCY) ?? "";
                    entry.Scope = rs.GetValue(PARM_SCOPE) ?? "";
                    entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                    entry.Fulltextlength = StructTrans.TransNum(rs.GetValue(PARM_FULLTEXTLENGTH));
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                    entry.RefstdNo = rs.GetValue(PARM_REFSTDNO) ?? "";
                    entry.ACCESSORIES = rs.GetValue(PARM_ACCESSORIES) ?? "";
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
                    entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
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
            List<StdDataInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list != null)
            {
                foreach (StdDataInfo info in list)
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
                    //删除术语、缩略语
                    Terminology dal = new Terminology();
                    IsSuccess = dal.DeleteByWhere("parenturlid='" + info.SYS_FLD_DOI + "'");
                    if (!IsSuccess)
                    {
                        return false;
                    }
                }
            }
            //删除标准的数据
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
        /// <param name="id">标准的数据的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改标准的数据状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">标准的数据的SYS_FLD_DOI</param>
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
