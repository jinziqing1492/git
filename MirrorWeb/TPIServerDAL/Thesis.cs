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
    public class Thesis : IThesis
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Thesis"];
        #region IArticle 字段
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_PYAUTHOR = "PYAUTHOR";
        private const string PARM_SNO = "SNO";
        private const string PARM_ACADEMY = "ACADEMY";
        private const string PARM_DEPARTMENTNAME = "DEPARTMENTNAME";
        private const string PARM_SUBJECT = "SUBJECT";
        private const string PARM_MAJOR = "MAJOR";
        private const string PARM_TELEPHONE = "TELEPHONE";
        private const string PARM_EMAIL = "EMAIL";
        private const string PARM_DEGREE = "DEGREE";
        private const string PARM_INSTRUCTOR = "INSTRUCTOR";
        private const string PARM_TUTORNAME1 = "TUTORNAME1";
        private const string PARM_PYTUTORNAME1 = "PYTUTORNAME1";
        private const string PARM_PYTUTORE1DEPARTMENT = "PYTUTORE1DEPARTMENT";
        private const string PARM_TUTORNAME2 = "TUTORNAME2";
        private const string PARM_PYTUTORNAME2 = "PYTUTORNAME2";
        private const string PARM_PYTUTORE2DEPARTMENT = "PYTUTORE2DEPARTMENT";
        private const string PARM_TUTORNAME3 = "TUTORNAME3";
        private const string PARM_PYTUTORNAME3 = "PYTUTORNAME3";
        private const string PARM_PYTUTORE3DEPARTMENT = "PYTUTORE3DEPARTMENT";
        private const string PARM_NAME = "TITLE";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_ABSTRACT = "ABSTRACT";
        private const string PARM_PAPERSUBMISSIONDATE = "PAPERSUBMISSIONDATE";
        private const string PARM_ORALDEFENSEDATE = "ORALDEFENSEDATE";
        private const string PARM_DEGREEAWARDDATE = "DEGREEAWARDDATE";
        private const string PARM_DEGREEYEAR = "DEGREEYEAR";
        private const string PARM_FUND = "FUND";
        private const string PARM_ENNAME = "ENTITLE";
        private const string PARM_ENKEYWORD = "ENKEYWORD";
        private const string PARM_ENABSTRACT = "ENABSTRACT";
        private const string PARM_RESEARCHFIELD = "RESEARCHFIELD";
        private const string PARM_SYS_FLD_REFERENCE = "SYS_FLD_REFERENCE";
        private const string PARM_FULLTEXT = "FULLTEXT";
        private const string PARM_RELEASEFIXEDYEAR = "RELEASEFIXEDYEAR";
        private const string PARM_COLLECTIONNUMBER = "COLLECTIONNUMBER";
        private const string PARM_SCHOOLCODE = "SCHOOLCODE";
        private const string PARM_THEMEWORD = "THEMEWORD";
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
        private const string PARM_SYS_FLD_VSM = "SYS_FLD_VSM";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_COVERPATH = "SYS_FLD_COVERPATH";
        private const string PARM_SYS_FLD_SRCFILENAME = "SYS_FLD_SRCFILENAME";
        private const string PARM_SYS_FLD_XMLPATH = "SYS_FLD_XMLPATH";
        private const string PARM_SYS_FLD_ABSTRACT = "SYS_FLD_ABSTRACT";
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

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ThesisInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.PYAuthor))
            {
                paramList.Add(PARM_PYAUTHOR);
                paramList.Add(item.PYAuthor);
            }
            if (!string.IsNullOrEmpty(item.SNO))
            {
                paramList.Add(PARM_SNO);
                paramList.Add(item.SNO);
            }
            if (!string.IsNullOrEmpty(item.Academy))
            {
                paramList.Add(PARM_ACADEMY);
                paramList.Add(item.Academy);
            }
            if (!string.IsNullOrEmpty(item.DepartmentName))
            {
                paramList.Add(PARM_DEPARTMENTNAME);
                paramList.Add(item.DepartmentName);
            }
            if (!string.IsNullOrEmpty(item.Subject))
            {
                paramList.Add(PARM_SUBJECT);
                paramList.Add(item.Subject);
            }
            if (!string.IsNullOrEmpty(item.Major))
            {
                paramList.Add(PARM_MAJOR);
                paramList.Add(item.Major);
            }
            if (!string.IsNullOrEmpty(item.Telephone))
            {
                paramList.Add(PARM_TELEPHONE);
                paramList.Add(item.Telephone);
            }
            if (!string.IsNullOrEmpty(item.Email))
            {
                paramList.Add(PARM_EMAIL);
                paramList.Add(item.Email);
            }
            if (!string.IsNullOrEmpty(item.Degree))
            {
                paramList.Add(PARM_DEGREE);
                paramList.Add(item.Degree);
            }
            if (!string.IsNullOrEmpty(item.Instructor))
            {
                paramList.Add(PARM_INSTRUCTOR);
                paramList.Add(item.Instructor);
            }
            if (!string.IsNullOrEmpty(item.TutorName1))
            {
                paramList.Add(PARM_TUTORNAME1);
                paramList.Add(item.TutorName1);
            }
            if (!string.IsNullOrEmpty(item.PYTutorName1))
            {
                paramList.Add(PARM_PYTUTORNAME1);
                paramList.Add(item.PYTutorName1);
            }
            if (!string.IsNullOrEmpty(item.PYTutore1Department))
            {
                paramList.Add(PARM_PYTUTORE1DEPARTMENT);
                paramList.Add(item.PYTutore1Department);
            }
            if (!string.IsNullOrEmpty(item.TutorName2))
            {
                paramList.Add(PARM_TUTORNAME2);
                paramList.Add(item.TutorName2);
            }
            if (!string.IsNullOrEmpty(item.PYTutorName2))
            {
                paramList.Add(PARM_PYTUTORNAME2);
                paramList.Add(item.PYTutorName2);
            }
            if (!string.IsNullOrEmpty(item.PYTutore2Department))
            {
                paramList.Add(PARM_PYTUTORE2DEPARTMENT);
                paramList.Add(item.PYTutore2Department);
            }
            if (!string.IsNullOrEmpty(item.TutorName3))
            {
                paramList.Add(PARM_TUTORNAME3);
                paramList.Add(item.TutorName3);
            }
            if (!string.IsNullOrEmpty(item.PYTutorName3))
            {
                paramList.Add(PARM_PYTUTORNAME3);
                paramList.Add(item.PYTutorName3);
            }
            if (!string.IsNullOrEmpty(item.PYTutore3Department))
            {
                paramList.Add(PARM_PYTUTORE3DEPARTMENT);
                paramList.Add(item.PYTutore3Department);
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.Abstract))
            {
                paramList.Add(PARM_ABSTRACT);
                paramList.Add(item.Abstract);
            }
            if (item.PaperSubmissionDate != DateTime.MinValue)
            {
                paramList.Add(PARM_PAPERSUBMISSIONDATE);
                paramList.Add(item.PaperSubmissionDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.OralDefenseDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ORALDEFENSEDATE);
                paramList.Add(item.OralDefenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.DegreeAwardDate != DateTime.MinValue)
            {
                paramList.Add(PARM_DEGREEAWARDDATE);
                paramList.Add(item.DegreeAwardDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.DegreeYear))
            {
                paramList.Add(PARM_DEGREEYEAR);
                paramList.Add(item.DegreeYear);
            }
            if (!string.IsNullOrEmpty(item.Fund))
            {
                paramList.Add(PARM_FUND);
                paramList.Add(item.Fund);
            }
            if (!string.IsNullOrEmpty(item.ENName))
            {
                paramList.Add(PARM_ENNAME);
                paramList.Add(item.ENName);
            }
            if (!string.IsNullOrEmpty(item.ENKeyWord))
            {
                paramList.Add(PARM_ENKEYWORD);
                paramList.Add(item.ENKeyWord);
            }
            if (!string.IsNullOrEmpty(item.ENAbstract))
            {
                paramList.Add(PARM_ENABSTRACT);
                paramList.Add(item.ENAbstract);
            }
            if (!string.IsNullOrEmpty(item.ResearchField))
            {
                paramList.Add(PARM_RESEARCHFIELD);
                paramList.Add(item.ResearchField);
            }
            if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            {
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
            }
            if (!string.IsNullOrEmpty(item.FullText))
            {
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            }
            if (!string.IsNullOrEmpty(item.ReleaseFixedYear))
            {
                paramList.Add(PARM_RELEASEFIXEDYEAR);
                paramList.Add(item.ReleaseFixedYear);
            }
            if (!string.IsNullOrEmpty(item.CollectionNumber))
            {
                paramList.Add(PARM_COLLECTIONNUMBER);
                paramList.Add(item.CollectionNumber);
            }
            if (!string.IsNullOrEmpty(item.SchoolCode))
            {
                paramList.Add(PARM_SCHOOLCODE);
                paramList.Add(item.SchoolCode);
            }
            if (!string.IsNullOrEmpty(item.Themeword))
            {
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.Themeword);
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
        public bool Update(ThesisInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            //if (!string.IsNullOrEmpty(item.Author))
            //{
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            //}
            //if (!string.IsNullOrEmpty(item.PYAuthor))
            //{
                paramList.Add(PARM_PYAUTHOR);
                paramList.Add(item.PYAuthor);
            //}
            //if (!string.IsNullOrEmpty(item.SNO))
            //{
                paramList.Add(PARM_SNO);
                paramList.Add(item.SNO);
            //}
            //if (!string.IsNullOrEmpty(item.Academy))
            //{
                paramList.Add(PARM_ACADEMY);
                paramList.Add(item.Academy);
            //}
            //if (!string.IsNullOrEmpty(item.DepartmentName))
            //{
                paramList.Add(PARM_DEPARTMENTNAME);
                paramList.Add(item.DepartmentName);
            //}
            //if (!string.IsNullOrEmpty(item.Subject))
            //{
                paramList.Add(PARM_SUBJECT);
                paramList.Add(item.Subject);
            //}
            //if (!string.IsNullOrEmpty(item.Major))
            //{
                paramList.Add(PARM_MAJOR);
                paramList.Add(item.Major);
            //}
            //if (!string.IsNullOrEmpty(item.Telephone))
            //{
                paramList.Add(PARM_TELEPHONE);
                paramList.Add(item.Telephone);
            //}
            //if (!string.IsNullOrEmpty(item.Email))
            //{
                paramList.Add(PARM_EMAIL);
                paramList.Add(item.Email);
            //}
            //if (!string.IsNullOrEmpty(item.Degree))
            //{
                paramList.Add(PARM_DEGREE);
                paramList.Add(item.Degree);
            //}
            //if (!string.IsNullOrEmpty(item.Instructor))
            //{
                paramList.Add(PARM_INSTRUCTOR);
                paramList.Add(item.Instructor);
            //}
            //if (!string.IsNullOrEmpty(item.TutorName1))
            //{
                paramList.Add(PARM_TUTORNAME1);
                paramList.Add(item.TutorName1);
            //}
            //if (!string.IsNullOrEmpty(item.PYTutorName1))
            //{
                paramList.Add(PARM_PYTUTORNAME1);
                paramList.Add(item.PYTutorName1);
            //}
            //if (!string.IsNullOrEmpty(item.PYTutore1Department))
            //{
                paramList.Add(PARM_PYTUTORE1DEPARTMENT);
                paramList.Add(item.PYTutore1Department);
            //}
            //if (!string.IsNullOrEmpty(item.TutorName2))
            //{
                paramList.Add(PARM_TUTORNAME2);
                paramList.Add(item.TutorName2);
            //}
            //if (!string.IsNullOrEmpty(item.PYTutorName2))
            //{
                paramList.Add(PARM_PYTUTORNAME2);
                paramList.Add(item.PYTutorName2);
            //}
            //if (!string.IsNullOrEmpty(item.PYTutore2Department))
            //{
                paramList.Add(PARM_PYTUTORE2DEPARTMENT);
                paramList.Add(item.PYTutore2Department);
            //}
            //if (!string.IsNullOrEmpty(item.TutorName3))
            //{
                paramList.Add(PARM_TUTORNAME3);
                paramList.Add(item.TutorName3);
            //}
            //if (!string.IsNullOrEmpty(item.PYTutorName3))
            //{
                paramList.Add(PARM_PYTUTORNAME3);
                paramList.Add(item.PYTutorName3);
            //}
            //if (!string.IsNullOrEmpty(item.PYTutore3Department))
            //{
                paramList.Add(PARM_PYTUTORE3DEPARTMENT);
                paramList.Add(item.PYTutore3Department);
            //}
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            //if (!string.IsNullOrEmpty(item.Keywords))
            //{
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            //}
            //if (!string.IsNullOrEmpty(item.Abstract))
            //{
                paramList.Add(PARM_ABSTRACT);
                paramList.Add(item.Abstract);
            //}
            //if (item.PaperSubmissionDate != DateTime.MinValue)
            //{
                paramList.Add(PARM_PAPERSUBMISSIONDATE);
                paramList.Add(item.PaperSubmissionDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (item.OralDefenseDate != DateTime.MinValue)
            //{
                paramList.Add(PARM_ORALDEFENSEDATE);
                paramList.Add(item.OralDefenseDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (item.DegreeAwardDate != DateTime.MinValue)
            //{
                paramList.Add(PARM_DEGREEAWARDDATE);
                paramList.Add(item.DegreeAwardDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.DegreeYear))
            //{
                paramList.Add(PARM_DEGREEYEAR);
                paramList.Add(item.DegreeYear);
            //}
            //if (!string.IsNullOrEmpty(item.Fund))
            //{
                paramList.Add(PARM_FUND);
                paramList.Add(item.Fund);
            //}
            //if (!string.IsNullOrEmpty(item.ENName))
            //{
                paramList.Add(PARM_ENNAME);
                paramList.Add(item.ENName);
            //}
            //if (!string.IsNullOrEmpty(item.ENKeyWord))
            //{
                paramList.Add(PARM_ENKEYWORD);
                paramList.Add(item.ENKeyWord);
            //}
            //if (!string.IsNullOrEmpty(item.ENAbstract))
            //{
                paramList.Add(PARM_ENABSTRACT);
                paramList.Add(item.ENAbstract);
            //}
            //if (!string.IsNullOrEmpty(item.ResearchField))
            //{
                paramList.Add(PARM_RESEARCHFIELD);
                paramList.Add(item.ResearchField);
            //}
            //if (!string.IsNullOrEmpty(item.Sys_Fld_Reference))
            //{
                paramList.Add(PARM_SYS_FLD_REFERENCE);
                paramList.Add(item.Sys_Fld_Reference);
            //}
            //if (!string.IsNullOrEmpty(item.FullText))
            //{
                paramList.Add(PARM_FULLTEXT);
                paramList.Add(item.FullText);
            //}
            //if (!string.IsNullOrEmpty(item.ReleaseFixedYear))
            //{
                paramList.Add(PARM_RELEASEFIXEDYEAR);
                paramList.Add(item.ReleaseFixedYear);
            //}
            //if (!string.IsNullOrEmpty(item.CollectionNumber))
            //{
                paramList.Add(PARM_COLLECTIONNUMBER);
                paramList.Add(item.CollectionNumber);
            //}
            //if (!string.IsNullOrEmpty(item.SchoolCode))
            //{
                paramList.Add(PARM_SCHOOLCODE);
                paramList.Add(item.SchoolCode);
            //}
            //if (!string.IsNullOrEmpty(item.Themeword))
            //{
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.Themeword);
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
        public ThesisInfo GetItem(string doi)
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
                ThesisInfo entry = new ThesisInfo();
                #region 判断字段并赋值
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.PYAuthor = rs.GetValue(PARM_PYAUTHOR) ?? "";
                entry.SNO = rs.GetValue(PARM_SNO) ?? "";
                entry.Academy = rs.GetValue(PARM_ACADEMY) ?? "";
                entry.Department = rs.GetValue(PARM_DEPARTMENTNAME) ?? "";
                entry.Subject = rs.GetValue(PARM_SUBJECT) ?? "";
                entry.Major = rs.GetValue(PARM_MAJOR) ?? "";
                entry.Telephone = rs.GetValue(PARM_TELEPHONE) ?? "";
                entry.Email = rs.GetValue(PARM_EMAIL) ?? "";
                entry.Degree = rs.GetValue(PARM_DEGREE) ?? "";
                entry.Instructor = rs.GetValue(PARM_INSTRUCTOR) ?? "";
                entry.TutorName1 = rs.GetValue(PARM_TUTORNAME1) ?? "";
                entry.PYTutorName1 = rs.GetValue(PARM_PYTUTORNAME1) ?? "";
                entry.PYTutore1Department = rs.GetValue(PARM_PYTUTORE1DEPARTMENT) ?? "";
                entry.TutorName2 = rs.GetValue(PARM_TUTORNAME2) ?? "";
                entry.PYTutorName2 = rs.GetValue(PARM_PYTUTORNAME2) ?? "";
                entry.PYTutore2Department = rs.GetValue(PARM_PYTUTORE2DEPARTMENT) ?? "";
                entry.TutorName3 = rs.GetValue(PARM_TUTORNAME3) ?? "";
                entry.PYTutorName3 = rs.GetValue(PARM_PYTUTORNAME3) ?? "";
                entry.PYTutore3Department = rs.GetValue(PARM_PYTUTORE3DEPARTMENT) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.Abstract = rs.GetValue(PARM_ABSTRACT) ?? "";
                entry.PaperSubmissionDate = StructTrans.TransDate(rs.GetValue(PARM_PAPERSUBMISSIONDATE));
                entry.OralDefenseDate = StructTrans.TransDate(rs.GetValue(PARM_ORALDEFENSEDATE));
                entry.DegreeAwardDate = StructTrans.TransDate(rs.GetValue(PARM_DEGREEAWARDDATE));
                entry.DegreeYear = rs.GetValue(PARM_DEGREEYEAR) ?? "";
                entry.Fund = rs.GetValue(PARM_FUND) ?? "";
                entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                entry.ENKeyWord = rs.GetValue(PARM_ENKEYWORD) ?? "";
                entry.ENAbstract = rs.GetValue(PARM_ENABSTRACT) ?? "";
                entry.ResearchField = rs.GetValue(PARM_RESEARCHFIELD) ?? "";
                entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
                entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                entry.ReleaseFixedYear = rs.GetValue(PARM_RELEASEFIXEDYEAR) ?? "";
                entry.CollectionNumber = rs.GetValue(PARM_COLLECTIONNUMBER) ?? "";
                entry.SchoolCode = rs.GetValue(PARM_SCHOOLCODE) ?? "";
                entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
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
                entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
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
        public List<ThesisInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ThesisInfo> entryList = new List<ThesisInfo>();
                ThesisInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ThesisInfo();
                    #region 判断字段并赋值
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.PYAuthor = rs.GetValue(PARM_PYAUTHOR) ?? "";
                    entry.SNO = rs.GetValue(PARM_SNO) ?? "";
                    entry.Academy = rs.GetValue(PARM_ACADEMY) ?? "";
                    entry.Department = rs.GetValue(PARM_DEPARTMENTNAME) ?? "";
                    entry.Subject = rs.GetValue(PARM_SUBJECT) ?? "";
                    entry.Major = rs.GetValue(PARM_MAJOR) ?? "";
                    entry.Telephone = rs.GetValue(PARM_TELEPHONE) ?? "";
                    entry.Email = rs.GetValue(PARM_EMAIL) ?? "";
                    entry.Degree = rs.GetValue(PARM_DEGREE) ?? "";
                    entry.Instructor = rs.GetValue(PARM_INSTRUCTOR) ?? "";
                    entry.TutorName1 = rs.GetValue(PARM_TUTORNAME1) ?? "";
                    entry.PYTutorName1 = rs.GetValue(PARM_PYTUTORNAME1) ?? "";
                    entry.PYTutore1Department = rs.GetValue(PARM_PYTUTORE1DEPARTMENT) ?? "";
                    entry.TutorName2 = rs.GetValue(PARM_TUTORNAME2) ?? "";
                    entry.PYTutorName2 = rs.GetValue(PARM_PYTUTORNAME2) ?? "";
                    entry.PYTutore2Department = rs.GetValue(PARM_PYTUTORE2DEPARTMENT) ?? "";
                    entry.TutorName3 = rs.GetValue(PARM_TUTORNAME3) ?? "";
                    entry.PYTutorName3 = rs.GetValue(PARM_PYTUTORNAME3) ?? "";
                    entry.PYTutore3Department = rs.GetValue(PARM_PYTUTORE3DEPARTMENT) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.Abstract = rs.GetValue(PARM_ABSTRACT) ?? "";
                    entry.PaperSubmissionDate = StructTrans.TransDate(rs.GetValue(PARM_PAPERSUBMISSIONDATE));
                    entry.OralDefenseDate = StructTrans.TransDate(rs.GetValue(PARM_ORALDEFENSEDATE));
                    entry.DegreeAwardDate = StructTrans.TransDate(rs.GetValue(PARM_DEGREEAWARDDATE));
                    entry.DegreeYear = rs.GetValue(PARM_DEGREEYEAR) ?? "";
                    entry.Fund = rs.GetValue(PARM_FUND) ?? "";
                    entry.ENName = rs.GetValue(PARM_ENNAME) ?? "";
                    entry.ENKeyWord = rs.GetValue(PARM_ENKEYWORD) ?? "";
                    entry.ENAbstract = rs.GetValue(PARM_ENABSTRACT) ?? "";
                    entry.ResearchField = rs.GetValue(PARM_RESEARCHFIELD) ?? "";
                    entry.Sys_Fld_Reference = rs.GetValue(PARM_SYS_FLD_REFERENCE) ?? "";
                    entry.FullText = rs.GetValue(PARM_FULLTEXT) ?? "";
                    entry.ReleaseFixedYear = rs.GetValue(PARM_RELEASEFIXEDYEAR) ?? "";
                    entry.CollectionNumber = rs.GetValue(PARM_COLLECTIONNUMBER) ?? "";
                    entry.SchoolCode = rs.GetValue(PARM_SCHOOLCODE) ?? "";
                    entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
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
                    entry.SYS_FLD_VSM = rs.GetValue(PARM_SYS_FLD_VSM) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                    entry.SYS_FLD_SRCFILENAME = rs.GetValue(PARM_SYS_FLD_SRCFILENAME) ?? "";
                    entry.SYS_FLD_XMLPATH = rs.GetValue(PARM_SYS_FLD_XMLPATH) ?? "";
                    entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
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
            List<Model.ThesisInfo> list = GetList(strWhere, 1, 1000, out record, false);
            if (list != null)
            {
                foreach (Model.ThesisInfo info in list)
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
                }
            }
            //删除学位论文
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
        /// <param name="id">学位论文的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改学位论文状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">学位论文的SYS_FLD_DOI</param>
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
