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
    public class Pic : IPic
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Pic"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_THEMEWORD = "THEMEWORD";
        private const string PARM_TYPE = "TYPE";
        private const string PARM_NOTE = "NOTE";
        private const string PARM_LANGUAGE = "LANGUAGE";
        private const string PARM_SOURCE = "SOURCE";
        private const string PARM_DATEISSUED = "DATEISSUED";
        private const string PARM_PICTYPE = "PICTYPE";
        private const string PARM_PICSIZE = "PICSIZE";
        private const string PARM_PICTIME = "PICTIME";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_AUTHORDESC = "AUTHORDESC";
        private const string PARM_PLACE = "PLACE";
        private const string PARM_COPYRIGHTBEGINDATE = "COPYRIGHTBEGINDATE";
        private const string PARM_COPYRIGHTYEAR = "COPYRIGHTYEAR";
        private const string PARM_ALLRIGHTRESERVED = "ALLRIGHTRESERVED";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_MODE = "MODE";
        private const string PARM_EXPOSURETIME = "EXPOSURETIME";
        private const string PARM_ISOSENSITIVITY = "ISOSENSITIVITY";
        private const string PARM_EXIFVERSION = "EXIFVERSION";
        private const string PARM_APERTURE = "APERTURE";
        private const string PARM_SHOOTINGTIME = "SHOOTINGTIME";
        private const string PARM_EXPOSURECOMPENSATION = "EXPOSURECOMPENSATION";
        private const string PARM_FOCAL = "FOCAL";
        private const string PARM_WHITEBALANCE = "WHITEBALANCE";
        private const string PARM_SCENETYPE = "SCENETYPE";
        private const string PARM_SYS_FLD_PARENTTYPE = "SYS_FLD_PARENTTYPE";
        private const string PARM_SYS_FLD_PAGENO = "SYS_FLD_PAGENO";
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
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_COPYRIGHTENDDATE = "COPYRIGHTENDDATE";
        private const string PARM_SYS_FLD_ISIMPORT = "SYS_FLD_ISIMPORT";
        private const string PARM_Sys_fld_ChapterDoi = "Sys_fld_ChapterDoi";
        private const string PARM_DEPARTMENT = "DEPARTMENT";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(PicInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
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
            if (item.Dateissued != DateTime.MinValue)
            {
                paramList.Add(PARM_DATEISSUED);
                paramList.Add(item.Dateissued.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.PicType))
            {
                paramList.Add(PARM_PICTYPE);
                paramList.Add(item.PicType);
            }
            paramList.Add(PARM_PICSIZE);
            paramList.Add(item.PicSize.ToString());
            paramList.Add(PARM_PICTIME);
            paramList.Add(item.PicTime.ToString());
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
            if (!string.IsNullOrEmpty(item.Place))
            {
                paramList.Add(PARM_PLACE);
                paramList.Add(item.Place);
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
            if (!string.IsNullOrEmpty(item.ParentDoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.ParentDoi);
            }
            if (!string.IsNullOrEmpty(item.Mode))
            {
                paramList.Add(PARM_MODE);
                paramList.Add(item.Mode);
            }
            if (!string.IsNullOrEmpty(item.ExposureTime))
            {
                paramList.Add(PARM_EXPOSURETIME);
                paramList.Add(item.ExposureTime);
            }
            if (!string.IsNullOrEmpty(item.ISOSensitivity))
            {
                paramList.Add(PARM_ISOSENSITIVITY);
                paramList.Add(item.ISOSensitivity);
            }
            if (!string.IsNullOrEmpty(item.ExifVersion))
            {
                paramList.Add(PARM_EXIFVERSION);
                paramList.Add(item.ExifVersion);
            }
            if (!string.IsNullOrEmpty(item.Aperture))
            {
                paramList.Add(PARM_APERTURE);
                paramList.Add(item.Aperture);
            }
            if (!string.IsNullOrEmpty(item.ShootingTime))
            {
                paramList.Add(PARM_SHOOTINGTIME);
                paramList.Add(item.ShootingTime);
            }
            if (!string.IsNullOrEmpty(item.ExposureCompensation))
            {
                paramList.Add(PARM_EXPOSURECOMPENSATION);
                paramList.Add(item.ExposureCompensation);
            }
            if (!string.IsNullOrEmpty(item.focal))
            {
                paramList.Add(PARM_FOCAL);
                paramList.Add(item.focal);
            }
            if (!string.IsNullOrEmpty(item.WhiteBalance))
            {
                paramList.Add(PARM_WHITEBALANCE);
                paramList.Add(item.WhiteBalance);
            }
            if (!string.IsNullOrEmpty(item.SceneType))
            {
                paramList.Add(PARM_SCENETYPE);
                paramList.Add(item.SceneType);
            }
            paramList.Add(PARM_SYS_FLD_PARENTTYPE);
            paramList.Add(item.Sys_fld_ParentType.ToString());
            paramList.Add(PARM_SYS_FLD_PAGENO);
            paramList.Add(item.Sys_fld_PageNo.ToString());
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
            if (!string.IsNullOrEmpty(item.Sys_fld_ChapterDoi))
            {
                paramList.Add(PARM_Sys_fld_ChapterDoi);
                paramList.Add(item.Sys_fld_ChapterDoi);
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
        public bool Update(PicInfo item)
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
            //if (item.Dateissued != DateTime.MinValue)
            //{
            paramList.Add(PARM_DATEISSUED);
            paramList.Add(item.Dateissued.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.PicType))
            //{
            paramList.Add(PARM_PICTYPE);
            paramList.Add(item.PicType);
            //}
            paramList.Add(PARM_PICSIZE);
            paramList.Add(item.PicSize.ToString());
            paramList.Add(PARM_PICTIME);
            paramList.Add(item.PicTime.ToString());
            //if (!string.IsNullOrEmpty(item.Author))
            //{
            paramList.Add(PARM_AUTHOR);
            paramList.Add(item.Author);
            //}
            //if (!string.IsNullOrEmpty(item.AuthorDESC))
            //{
            paramList.Add(PARM_AUTHORDESC);
            paramList.Add(item.AuthorDESC);
            //}
            //if (!string.IsNullOrEmpty(item.Place))
            //{
            paramList.Add(PARM_PLACE);
            paramList.Add(item.Place);
            //}
            //if (item.CopyrightBeginDate != DateTime.MinValue)
            //{
            paramList.Add(PARM_COPYRIGHTBEGINDATE);
            paramList.Add(item.CopyrightBeginDate.ToString("yyyy-MM-dd HH:mm:ss"));
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
            //if (!string.IsNullOrEmpty(item.Mode))
            //{
            paramList.Add(PARM_MODE);
            paramList.Add(item.Mode);
            //}
            //if (!string.IsNullOrEmpty(item.ExposureTime))
            //{
            paramList.Add(PARM_EXPOSURETIME);
            paramList.Add(item.ExposureTime);
            //}
            //if (!string.IsNullOrEmpty(item.ISOSensitivity))
            //{
            paramList.Add(PARM_ISOSENSITIVITY);
            paramList.Add(item.ISOSensitivity);
            //}
            //if (!string.IsNullOrEmpty(item.ExifVersion))
            //{
            paramList.Add(PARM_EXIFVERSION);
            paramList.Add(item.ExifVersion);
            //}
            //if (!string.IsNullOrEmpty(item.Aperture))
            //{
            paramList.Add(PARM_APERTURE);
            paramList.Add(item.Aperture);
            //}
            //if (!string.IsNullOrEmpty(item.ShootingTime))
            //{
            paramList.Add(PARM_SHOOTINGTIME);
            paramList.Add(item.ShootingTime);
            //}
            //if (!string.IsNullOrEmpty(item.ExposureCompensation))
            //{
            paramList.Add(PARM_EXPOSURECOMPENSATION);
            paramList.Add(item.ExposureCompensation);
            //}
            //if (!string.IsNullOrEmpty(item.focal))
            //{
            paramList.Add(PARM_FOCAL);
            paramList.Add(item.focal);
            //}
            //if (!string.IsNullOrEmpty(item.WhiteBalance))
            //{
            paramList.Add(PARM_WHITEBALANCE);
            paramList.Add(item.WhiteBalance);
            //}
            //if (!string.IsNullOrEmpty(item.SceneType))
            //{
            paramList.Add(PARM_SCENETYPE);
            paramList.Add(item.SceneType);
            //}
            paramList.Add(PARM_SYS_FLD_PARENTTYPE);
            paramList.Add(item.Sys_fld_ParentType.ToString());
            paramList.Add(PARM_SYS_FLD_PAGENO);
            paramList.Add(item.Sys_fld_PageNo.ToString());
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
            //if (!string.IsNullOrEmpty(item.Sys_fld_ChapterDoi))
            //{
            paramList.Add(PARM_Sys_fld_ChapterDoi);
            paramList.Add(item.Sys_fld_ChapterDoi);
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
        public PicInfo GetItem(string doi)
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
                PicInfo entry = new PicInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
                entry.Type = StructTrans.TransNum(rs.GetValue(PARM_TYPE));
                entry.Note = rs.GetValue(PARM_NOTE) ?? "";
                entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
                entry.Dateissued = StructTrans.TransDate(rs.GetValue(PARM_DATEISSUED));
                entry.PicType = rs.GetValue(PARM_PICTYPE) ?? "";
                entry.PicSize = StructTrans.TransNum(rs.GetValue(PARM_PICSIZE));
                entry.PicTime = StructTrans.TransNum(rs.GetValue(PARM_PICTIME));
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.AuthorDESC = rs.GetValue(PARM_AUTHORDESC) ?? "";
                entry.Place = rs.GetValue(PARM_PLACE) ?? "";
                entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.Mode = rs.GetValue(PARM_MODE) ?? "";
                entry.ExposureTime = rs.GetValue(PARM_EXPOSURETIME) ?? "";
                entry.ISOSensitivity = rs.GetValue(PARM_ISOSENSITIVITY) ?? "";
                entry.ExifVersion = rs.GetValue(PARM_EXIFVERSION) ?? "";
                entry.Aperture = rs.GetValue(PARM_APERTURE) ?? "";
                entry.ShootingTime = rs.GetValue(PARM_SHOOTINGTIME) ?? "";
                entry.ExposureCompensation = rs.GetValue(PARM_EXPOSURECOMPENSATION) ?? "";
                entry.focal = rs.GetValue(PARM_FOCAL) ?? "";
                entry.WhiteBalance = rs.GetValue(PARM_WHITEBALANCE) ?? "";
                entry.SceneType = rs.GetValue(PARM_SCENETYPE) ?? "";
                entry.Sys_fld_ParentType = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
                entry.Sys_fld_PageNo = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PAGENO));
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
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
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
        public List<PicInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<PicInfo> entryList = new List<PicInfo>();
                PicInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new PicInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
                    entry.Type = StructTrans.TransNum(rs.GetValue(PARM_TYPE));
                    entry.Note = rs.GetValue(PARM_NOTE) ?? "";
                    entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                    entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
                    entry.Dateissued = StructTrans.TransDate(rs.GetValue(PARM_DATEISSUED));
                    entry.PicType = rs.GetValue(PARM_PICTYPE) ?? "";
                    entry.PicSize = StructTrans.TransNum(rs.GetValue(PARM_PICSIZE));
                    entry.PicTime = StructTrans.TransNum(rs.GetValue(PARM_PICTIME));
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.AuthorDESC = rs.GetValue(PARM_AUTHORDESC) ?? "";
                    entry.Place = rs.GetValue(PARM_PLACE) ?? "";
                    entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                    entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                    entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                    entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                    entry.Mode = rs.GetValue(PARM_MODE) ?? "";
                    entry.ExposureTime = rs.GetValue(PARM_EXPOSURETIME) ?? "";
                    entry.ISOSensitivity = rs.GetValue(PARM_ISOSENSITIVITY) ?? "";
                    entry.ExifVersion = rs.GetValue(PARM_EXIFVERSION) ?? "";
                    entry.Aperture = rs.GetValue(PARM_APERTURE) ?? "";
                    entry.ShootingTime = rs.GetValue(PARM_SHOOTINGTIME) ?? "";
                    entry.ExposureCompensation = rs.GetValue(PARM_EXPOSURECOMPENSATION) ?? "";
                    entry.focal = rs.GetValue(PARM_FOCAL) ?? "";
                    entry.WhiteBalance = rs.GetValue(PARM_WHITEBALANCE) ?? "";
                    entry.SceneType = rs.GetValue(PARM_SCENETYPE) ?? "";
                    entry.Sys_fld_ParentType = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
                    entry.Sys_fld_PageNo = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PAGENO));
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
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
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
        /// 根据分页获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<PicInfo> GetList_NoFirst(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
            IList<int> paginationInterval = new List<int>();//Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);

            if (pageNo == 1)
            {
                pageCount = pageCount - 1;
                paginationInterval.Add(1);
                int pageRight = recordCount < pageCount ? recordCount : pageCount;
                paginationInterval.Add(pageRight);
            }
            else
            {
                paginationInterval.Add((pageNo - 1) * pageCount);
                int pageRight = pageNo * pageCount - 1;
                pageRight = recordCount < pageRight ? recordCount : pageRight;
                paginationInterval.Add(pageRight);
            }

            rs.Move(paginationInterval[0]);
            try
            {
                List<PicInfo> entryList = new List<PicInfo>();
                PicInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new PicInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Keywords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.Themeword = rs.GetValue(PARM_THEMEWORD) ?? "";
                    entry.Type = StructTrans.TransNum(rs.GetValue(PARM_TYPE));
                    entry.Note = rs.GetValue(PARM_NOTE) ?? "";
                    entry.Language = rs.GetValue(PARM_LANGUAGE) ?? "";
                    entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
                    entry.Dateissued = StructTrans.TransDate(rs.GetValue(PARM_DATEISSUED));
                    entry.PicType = rs.GetValue(PARM_PICTYPE) ?? "";
                    entry.PicSize = StructTrans.TransNum(rs.GetValue(PARM_PICSIZE));
                    entry.PicTime = StructTrans.TransNum(rs.GetValue(PARM_PICTIME));
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.AuthorDESC = rs.GetValue(PARM_AUTHORDESC) ?? "";
                    entry.Place = rs.GetValue(PARM_PLACE) ?? "";
                    entry.CopyrightBeginDate = StructTrans.TransDate(rs.GetValue(PARM_COPYRIGHTBEGINDATE));
                    entry.CopyrightYear = rs.GetValue(PARM_COPYRIGHTYEAR) ?? "";
                    entry.Allrightreserved = rs.GetValue(PARM_ALLRIGHTRESERVED) ?? "";
                    entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                    entry.Mode = rs.GetValue(PARM_MODE) ?? "";
                    entry.ExposureTime = rs.GetValue(PARM_EXPOSURETIME) ?? "";
                    entry.ISOSensitivity = rs.GetValue(PARM_ISOSENSITIVITY) ?? "";
                    entry.ExifVersion = rs.GetValue(PARM_EXIFVERSION) ?? "";
                    entry.Aperture = rs.GetValue(PARM_APERTURE) ?? "";
                    entry.ShootingTime = rs.GetValue(PARM_SHOOTINGTIME) ?? "";
                    entry.ExposureCompensation = rs.GetValue(PARM_EXPOSURECOMPENSATION) ?? "";
                    entry.focal = rs.GetValue(PARM_FOCAL) ?? "";
                    entry.WhiteBalance = rs.GetValue(PARM_WHITEBALANCE) ?? "";
                    entry.SceneType = rs.GetValue(PARM_SCENETYPE) ?? "";
                    entry.Sys_fld_ParentType = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
                    entry.Sys_fld_PageNo = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PAGENO));
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
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.COPYRIGHTENDDATE = rs.GetValue(PARM_COPYRIGHTENDDATE) ?? "";
                    entry.Sys_fld_isimport = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_ISIMPORT));
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
            //删除图片
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
        /// <param name="id">图片的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改图片状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
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
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
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

        /// <summary>
        /// 获取图片分组ID，图片详情页面使用，用于当前分组的图片显示完后，显示下一分组（或上一分组）的图片
        /// </summary>
        /// <param name="sqlwhere">查询限制条件</param>
        /// <param name="field">分组标识</param>
        /// <returns></returns>
        public List<PicInfo> GetGroupID(string sqlwhere, string field)
        {
            string sql = "";
            if (!string.IsNullOrWhiteSpace(sqlwhere))
            {
                sql = sqlwhere + " AND ";
            }
            sql = sql + " (" + field + "=* not " + field + " is null)" + " group by " + field;
            int recordCount = 0;
            string fields = string.Format("{0},{1},{2}", field, PARM_SYS_FLD_DOI, PARM_SYS_FLD_PARENTTYPE);
            RecordSet rs = TPIHelper.GetRecordPartField(TABLE_NAME, sql, fields);
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
            try
            {
                List<PicInfo> entryList = new List<PicInfo>();
                PicInfo entry = null;
                for (int i = 0; i < recordCount; i++)
                {
                    entry = new PicInfo();

                    #region 判断字段并赋值
                    if (field.ToUpper() == PARM_PARENTDOI.ToUpper())
                    {
                        entry.ParentDoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                    }
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.Sys_fld_ParentType = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
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
    }
}
