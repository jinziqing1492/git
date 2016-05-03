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
    public class Video : IVideo
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Video"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_THEMEWORD = "THEMEWORD";
        private const string PARM_TYPE = "TYPE";
        private const string PARM_NOTE = "NOTE";
        private const string PARM_LANGUAGE = "LANGUAGE";
        private const string PARM_SOURCE = "SOURCE";
        private const string PARM_ISSUEDATE = "ISSUEDATE";
        private const string PARM_VIDEOTYPE = "VIDEOTYPE";
        private const string PARM_VIDEOSIZE = "VIDEOSIZE";
        private const string PARM_VIDEOTIME = "VIDEOTIME";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_AUTHORDESC = "AUTHORDESC";
        private const string PARM_ACCESSORIES = "ACCESSORIES";
        private const string PARM_COPYRIGHTBEGINDATE = "COPYRIGHTBEGINDATE";
        private const string PARM_COPYRIGHTYEAR = "COPYRIGHTYEAR";
        private const string PARM_ALLRIGHTRESERVED = "ALLRIGHTRESERVED";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_SYS_FLD_PARENTTYPE = "SYS_FLD_PARENTTYPE";
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
        private const string PARM_SYS_FLD_ABSTRACT = "SYS_FLD_ABSTRACT";
        private const string PARM_SYS_FLD_HITCOUNT = "SYS_FLD_HITCOUNT";
        private const string PARM_SYS_FLD_DOWNLOAD = "SYS_FLD_DOWNLOAD";
        private const string PARM_SYS_FLD_LDBID = "SYS_FLD_LDBID";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_COPYRIGHTENDDATE = "COPYRIGHTENDDATE";
        private const string PARM_SYS_FLD_ISIMPORT = "SYS_FLD_ISIMPORT";
        private const string PARM_SYS_FLD_CHAPTERDOI = "SYS_FLD_CHAPTERDOI";
        private const string PARM_DEPARTMENT = "DEPARTMENT";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(VideoInfo item)
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
            if (!string.IsNullOrEmpty(item.Keywords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            }
            if (!string.IsNullOrEmpty(item.Description))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.Description);
            }
            if (!string.IsNullOrEmpty(item.Themeword))
            {
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.Themeword);
            }
            paramList.Add(PARM_TYPE);
            paramList.Add(item.Type.ToString());
            if (!string.IsNullOrEmpty(item.Note))
            {
                paramList.Add(PARM_NOTE);
                paramList.Add(item.Note);
            }
            if (!string.IsNullOrEmpty(item.Language))
            {
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            }
            if (!string.IsNullOrEmpty(item.Source))
            {
                paramList.Add(PARM_SOURCE);
                paramList.Add(item.Source);
            }
            if (item.IssueDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ISSUEDATE);
                paramList.Add(item.IssueDate.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(item.VideoType))
            {
                paramList.Add(PARM_VIDEOTYPE);
                paramList.Add(item.VideoType);
            }
            if (!string.IsNullOrEmpty(item.VideoSize))
            {
                paramList.Add(PARM_VIDEOSIZE);
                paramList.Add(item.VideoSize);
            }
            if (!string.IsNullOrEmpty(item.videoTime))
            {
                paramList.Add(PARM_VIDEOTIME);
                paramList.Add(item.videoTime);
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
            if (!string.IsNullOrEmpty(item.Accessories))
            {
                paramList.Add(PARM_ACCESSORIES);
                paramList.Add(item.Accessories);
            }
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
            if (!string.IsNullOrEmpty(item.ParentDoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            }
            paramList.Add(PARM_SYS_FLD_PARENTTYPE);
            paramList.Add(item.Sys_fld_ParentType.ToString());
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


            if (!string.IsNullOrEmpty(item.SYS_FLD_CHAPTERDOI))
            {
                paramList.Add(PARM_SYS_FLD_CHAPTERDOI);
                paramList.Add(item.SYS_FLD_CHAPTERDOI);
            }
            if (!string.IsNullOrEmpty(item.Department))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
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
        public bool Update(VideoInfo item)
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
            //if (!string.IsNullOrEmpty(item.Keywords))
            //{
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.Keywords);
            //}
            //if (!string.IsNullOrEmpty(item.Description))
            //{
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.Description);
            //}
            //if (!string.IsNullOrEmpty(item.Themeword))
            //{
                paramList.Add(PARM_THEMEWORD);
                paramList.Add(item.Themeword);
            //}
            paramList.Add(PARM_TYPE);
            paramList.Add(item.Type.ToString());
            //if (!string.IsNullOrEmpty(item.Note))
            //{
                paramList.Add(PARM_NOTE);
                paramList.Add(item.Note);
            //}
            //if (!string.IsNullOrEmpty(item.Language))
            //{
                paramList.Add(PARM_LANGUAGE);
                paramList.Add(item.Language);
            //}
            //if (!string.IsNullOrEmpty(item.Source))
            //{
                paramList.Add(PARM_SOURCE);
                paramList.Add(item.Source);
            //}
            //if (item.IssueDate != DateTime.MinValue)
            //{
                paramList.Add(PARM_ISSUEDATE);
                paramList.Add(item.IssueDate.ToString("yyyy-MM-dd"));
            //}
            //if (!string.IsNullOrEmpty(item.VideoType))
            //{
                paramList.Add(PARM_VIDEOTYPE);
                paramList.Add(item.VideoType);
            //}
            paramList.Add(PARM_VIDEOSIZE);
            paramList.Add(item.VideoSize);
            paramList.Add(PARM_VIDEOTIME);
            paramList.Add(item.videoTime);
            //if (!string.IsNullOrEmpty(item.Author))
            //{
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            //}
            //if (!string.IsNullOrEmpty(item.AuthorDesc))
            //{
                paramList.Add(PARM_AUTHORDESC);
                paramList.Add(item.AuthorDesc);
            //}
            //if (!string.IsNullOrEmpty(item.Accessories))
            //{
                paramList.Add(PARM_ACCESSORIES);
                paramList.Add(item.Accessories);
            //}
            //if (item.CopyrightBeginDate != DateTime.MinValue)
            //{
                paramList.Add(PARM_COPYRIGHTBEGINDATE);
                paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd"));
            //}
            //if (!string.IsNullOrEmpty(item.CopyrightYear))
            //{
                paramList.Add(PARM_COPYRIGHTYEAR);
                paramList.Add(item.CopyrightYear);
            //}
            //if (!string.IsNullOrEmpty(item.Allrightreserved))
            //{
                paramList.Add(PARM_ALLRIGHTRESERVED);
                paramList.Add(item.Allrightreserved);
            //}
            //if (!string.IsNullOrEmpty(item.ParentDoi))
            //{
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            //}
            paramList.Add(PARM_SYS_FLD_PARENTTYPE);
            paramList.Add(item.Sys_fld_ParentType.ToString());
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
            //if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            //{
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            //}
            paramList.Add(PARM_SYS_FLD_ISHASATTACH);
            paramList.Add(item.SYS_FLD_ISHASATTACH.ToString());
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


            //if (!string.IsNullOrEmpty(item.SYS_FLD_CHAPTERDOI))
            //{
                paramList.Add(PARM_SYS_FLD_CHAPTERDOI);
                paramList.Add(item.SYS_FLD_CHAPTERDOI);
            //}
            //if (!string.IsNullOrEmpty(item.Department))
            //{
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
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
        public VideoInfo GetItem(string doi)
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
                VideoInfo entry = new VideoInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
                entry.Type = StructTrans.TransNum(rs.GetValue(PARM_TYPE));
                entry.Note = rs.GetValue(PARM_NOTE) ?? "";
                entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
                entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                entry.VideoType = rs.GetValue(PARM_VIDEOTYPE) ?? "";
                entry.VideoSize = rs.GetValue(PARM_VIDEOSIZE);
                entry.videoTime = rs.GetValue(PARM_VIDEOTIME);
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.AuthorDesc = rs.GetValue(PARM_AUTHORDESC) ?? "";
                entry.Accessories = rs.GetValue(PARM_ACCESSORIES) ?? "";
                entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.Sys_fld_ParentType = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
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
                entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
                entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                entry.SYS_FLD_CHAPTERDOI = rs.GetValue(PARM_SYS_FLD_CHAPTERDOI) ?? "";
                entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";

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
        public List<VideoInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<VideoInfo> entryList = new List<VideoInfo>();
                VideoInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new VideoInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
                    entry.Type = StructTrans.TransNum(rs.GetValue(PARM_TYPE));
                    entry.Note = rs.GetValue(PARM_NOTE) ?? "";
                    entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                    entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
                    entry.IssueDate = StructTrans.TransDate(rs.GetValue(PARM_ISSUEDATE));
                    entry.VideoType = rs.GetValue(PARM_VIDEOTYPE) ?? "";
                    entry.VideoSize = rs.GetValue(PARM_VIDEOSIZE);
                    entry.videoTime = rs.GetValue(PARM_VIDEOTIME);
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.AuthorDesc = rs.GetValue(PARM_AUTHORDESC) ?? "";
                    entry.Accessories = rs.GetValue(PARM_ACCESSORIES) ?? "";
                    entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                    entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                    entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                    entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                    entry.Sys_fld_ParentType = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
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
                    entry.SYS_FLD_ABSTRACT = rs.GetValue(PARM_SYS_FLD_ABSTRACT) ?? "";
                    entry.Sys_fld_Hitcount = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_HITCOUNT));
                    entry.Sys_fld_Download = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_DOWNLOAD));
                    entry.SYS_FLD_LDBID = rs.GetValue(PARM_SYS_FLD_LDBID) ?? "";
                    entry.IsOnline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
                    entry.SYS_FLD_CHAPTERDOI = rs.GetValue(PARM_SYS_FLD_CHAPTERDOI) ?? "";
                    entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";

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
            //删除视频
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
        /// <param name="id">视频的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改视频状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">视频的SYS_FLD_DOI</param>
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
