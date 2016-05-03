using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.Web.Utility
{
    public class FileMngrModel
    {
        public string mainf { get; set; }
        public string cover { get; set; }
        public string attach { get; set; }
    }

    /// <summary>
    /// 文件管理
    /// </summary>
    public abstract class FileManagementUtility
    {
        /// <summary>
        /// 文件名
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath(string vpath, string fileName)
        {
            string reValue = null;
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(vpath))
            {
                return null;
            }

            //获取文档的真实路径
            string virtulpath = Config.GetVirtalPath(vpath);
            HttpServerUtility Server = HttpContext.Current.Server;
            string nhFolderPath = Server.MapPath("~/" + virtulpath);
            if (!string.IsNullOrEmpty(nhFolderPath))
            {
                if (fileName.IndexOf("\\") != 0)
                {
                    nhFolderPath += "\\";
                }
                reValue = nhFolderPath + fileName;
            }
            return reValue;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取文件的相对路径，(不含文件名)
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetFilePath(DataBaseType dbtype, string parentDoi)
        {
            string virtualTag = ConfigurationManager.AppSettings["virtualTag"];
            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //获取前缀
            string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
            string returnPath = "\\" + prefix + "\\" + parentDoi;

            if (!Directory.Exists(path + returnPath))
            {
                Directory.CreateDirectory(path + returnPath);
            }
            return path + returnPath;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取主文件的物理路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetFilePathByResDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = GetResObjByDoi(dbtype, doi);
            if (bm == null)
                return "";
            string virtualTag = bm.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = bm.SYS_FLD_FILEPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //主文件实际完整物理路径
            string actualPath = path.TrimEnd('\\') + relativelyPath;
            return actualPath;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取主文件的虚拟路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetFileVirPathByResDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = GetResObjByDoi(dbtype, doi);
            if (bm == null)
                return "";
            string virtualTag = bm.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = bm.SYS_FLD_FILEPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = "../" + virtualpath.TrimStart('/').TrimEnd('/') + "/";
            //主文件实际完整物理路径
            string actualPath = path + relativelyPath.Replace('\\', '/').TrimStart('/');
            return actualPath;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取封面的物理路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetCoverPathByResDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = GetResObjByDoi(dbtype, doi);
            if (bm == null)
                return "";
            string virtualTag = bm.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = bm.SYS_FLD_COVERPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //主文件实际完整物理路径
            string actualPath = path.TrimEnd('\\') + relativelyPath;
            return actualPath;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取封面的相对路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetCoverVirPathByResDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = GetResObjByDoi(dbtype, doi);
            if (bm == null)
                return "";
            string virtualTag = bm.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = bm.SYS_FLD_COVERPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = "../" + virtualpath.TrimStart('/').TrimEnd('/') + "/";
            //主文件实际完整物理路径
            string actualPath = path + relativelyPath.Replace('\\', '/').TrimStart('/');
            return actualPath;
        }

        /// <summary>
        /// 根据给定的doi和数据类型获取XML的物理路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetXMLPathByResDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = GetResObjByDoi(dbtype, doi);
            if (bm == null)
                return "";
            string virtualTag = bm.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = bm.SYS_FLD_XMLPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //主文件实际完整物理路径
            string actualPath = path.TrimEnd('\\') + relativelyPath;
            return actualPath;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取XML的虚拟路径
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetXMLVirPathByResDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = GetResObjByDoi(dbtype, doi);
            if (bm == null)
                return "";
            string virtualTag = bm.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = bm.SYS_FLD_XMLPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = "../" + virtualpath.TrimStart('/').TrimEnd('/') + "/";
            //主文件实际完整物理路径
            string actualPath = path + relativelyPath.Replace('\\', '/').TrimStart('/');
            return actualPath;
        }

        /// <summary>
        /// 根据给定的doi和数据类型获取文件的相对路径，(不含文件名)
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetAttachFilePath(DataBaseType dbtype, string parentDoi)
        {
            string virtualTag = ConfigurationManager.AppSettings["virtualTag"];
            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理路径
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //获取前缀
            string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
            string returnPath = "\\" + prefix + "\\" + parentDoi + "\\Source";

            if (!Directory.Exists(path + returnPath))
            {
                Directory.CreateDirectory(path + returnPath);
            }
            return path + returnPath;
        }
		/// <summary>
		/// 上传图书章节（根据存在章节进行操作）
		/// </summary>
		/// <param name="fileName">文件名及路径</param>
		/// <param name="dbtype">数据库类型</param>
		/// <param name="parentdoi">图书doi</param>
		/// <param name="doi">章节doi</param>
		/// <param name="virtualTag">虚拟目录标识</param>
		/// <returns>返回这个文件的相对路径</returns>
		public static string UploadFile(string fileName, DataBaseType dbtype, string parentdoi, string doi, out string virtualTag)
		{
			virtualTag = "";
			if (string.IsNullOrEmpty(fileName))
			{
				return string.Empty;
			}
			virtualTag = ConfigurationManager.AppSettings["virtualTag"];
			string virtualpath = Config.GetVirtalPath(virtualTag);
			//取得物理路径
			string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
			//获取前缀
			string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
			string fileExtension = Path.GetExtension(fileName);
			string returnPath = "\\" + prefix + "\\" + parentdoi + "\\Chapter\\" + doi + fileExtension;

			if (!Directory.Exists(path + "\\" + prefix + "\\" + parentdoi + "\\Chapter\\"))
			{
				Directory.CreateDirectory(path + "\\" + prefix + "\\" + parentdoi + "\\Chapter\\");
			}
			try
			{
                if (File.Exists(path + returnPath))
                {
                    File.Delete(path + returnPath);
                }
				File.Move(fileName, path + returnPath);
				return returnPath;
			}
			catch
			{
				return string.Empty;
			}

		}

        /// <summary>
        /// 根据给定的doi和数据类型获取文件的物理路径，(不含文件名)
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetAttachFilePath(string doi)
        {
            Attachment a = new Attachment();
            AttachmentInfo ai = a.GetItem(doi);
            if (ai == null)
                return "";
            string virtualTag = ai.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = ai.SYS_FLD_FILEPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理路径
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //主文件实际完整物理路径
            string actualPath = path.TrimEnd('\\') + relativelyPath;
            return actualPath;
        }
        /// <summary>
        /// 根据给定的doi和数据类型获取文件的相对路径，(不含文件名)
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="parentDoi"></param>
        /// <returns></returns>
        public static string GetAttachVirFilePath(string doi)
        {
            Attachment attBll = new Attachment();
            AttachmentInfo attaItem = attBll.GetItem(doi);
            if (attaItem == null)
                return "";
            string virtualTag = attaItem.SYS_FLD_VIRTUALPATHTAG;
            string relativelyPath = attaItem.SYS_FLD_FILEPATH;
            if (string.IsNullOrEmpty(virtualTag) || string.IsNullOrEmpty(relativelyPath))
                return "";

            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理地址
            string path = "../" + virtualpath.TrimStart('/').TrimEnd('/') + "/";
            //主文件实际完整物理路径
            string actualPath = path + relativelyPath.Replace('\\', '/').TrimStart('/');
            return actualPath;
        }

        /// <summary>
        /// 上传主文件
        /// </summary>
        /// <param name="upfile">文件流</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="doi">主文件doi</param>
        /// <returns>返回这个文件的相对路径</returns>
        public static string UploadFile(HttpPostedFile upfile, DataBaseType dbtype, string doi)
        {
            if (upfile == null || upfile.ContentLength == 0)
            {
                return string.Empty;
            }
            string virtualTag = ConfigurationManager.AppSettings["virtualTag"];
            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理路径
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //获取前缀
            string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
            string fileExtension = Path.GetExtension(upfile.FileName);
            string returnPath = "\\" + prefix + "\\" + doi + "\\" + doi + fileExtension;

            if (!Directory.Exists(path + "\\" + prefix + "\\" + doi))
            {
                Directory.CreateDirectory(path + "\\" + prefix + "\\" + doi);
            }

            try
            {
                upfile.SaveAs(path + returnPath);
                switch (fileExtension.ToLower())
                {
                    case ".gif":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                    case ".png":
                        {
                            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(path + returnPath), FileMode.Open);
                            Utility.MakeThumbnail(fs, path + "\\" + prefix + "\\" + doi + "\\" + doi + "_small" + fileExtension, 132, 0, "w", ".jpg");
                        }
                        break;
                    default:
                        break;
                }
                return returnPath;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 上传主文件 这个方法是根据已经存在文件来处理
        /// </summary>
        /// <param name="upfile">文件名及路径</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="doi">主文件doi</param>
        /// <returns>返回这个文件的相对路径</returns>
        public static string UploadFile(string fileName, DataBaseType dbtype, string doi, out string virtualTag)
        {
            virtualTag = "";
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }
            virtualTag = ConfigurationManager.AppSettings["virtualTag"];
            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理路径
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //获取前缀
            string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
            string fileExtension = Path.GetExtension(fileName);
            string returnPath = "\\" + prefix + "\\" + doi + "\\" + doi + fileExtension;

            if (!Directory.Exists(path + "\\" + prefix + "\\" + doi))
            {
                Directory.CreateDirectory(path + "\\" + prefix + "\\" + doi);
            }

            try
            {
                if (File.Exists(path + returnPath))
                    File.Delete(path + returnPath);
                File.Move(fileName, path + returnPath);

                switch (fileExtension.ToLower())
                {
                    case ".gif":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                    case ".png":
                        {
                            FileStream fs = new FileStream(path + returnPath, FileMode.Open);
                            Utility.MakeThumbnail(fs, path + "\\" + prefix + "\\" + doi + "\\" + doi + "_small" + fileExtension, 132, 0, "w", ".jpg");
                        }
                        break;
                    default:
                        break;
                }
                return returnPath;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 上传附件文件
        /// </summary>
        /// <param name="attachFile">附件文件流</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="parentDoi">主文件doi</param>
        /// <param name="attachType">附件类型</param>
        /// <returns></returns>
        public static bool UploadAttachFile(HttpPostedFile attachFile, DataBaseType dbtype, string parentDoi, string attachType)
        {
            if (attachFile == null || attachFile.ContentLength == 0)
            {
                return false;
            }
            string virtualTag = ConfigurationManager.AppSettings["virtualTag"];
            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理路径
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //获取前缀
            string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
            string fileExtension = Path.GetExtension(attachFile.FileName);
            string returnPath = "\\" + prefix + "\\" + parentDoi + "\\Source\\" + Path.GetFileName(attachFile.FileName);

            if (!Directory.Exists(path + "\\" + prefix + "\\" + parentDoi + "\\Source"))
            {
                Directory.CreateDirectory(path + "\\" + prefix + "\\" + parentDoi + "\\Source");
            }
            try
            {
                attachFile.SaveAs(path + returnPath);
                //需要给附件表 添加数据
                Attachment attBll = new Attachment();
                AttachmentInfo attaItem = new AttachmentInfo();
                attaItem.PARENTDOI = parentDoi;
                attaItem.Name = Path.GetFileName(attachFile.FileName);
                attaItem.Sys_fld_Adddate = DateTime.Now;
                attaItem.Sys_fld_Adduser = Utility.GetUserName();
                attaItem.SYS_FLD_DOI = CNKI.BaseFunction.RandomId.Get();
                attaItem.SYS_FLD_FILEPATH = returnPath;
                attaItem.SYS_FLD_VIRTUALPATHTAG = virtualTag;
                attaItem.Type = attachType;
                attaItem.Sys_fld_filetype = Path.GetExtension(attachFile.FileName);
                return attBll.Add(attaItem);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 上传附件文件 
        /// </summary>
        /// <param name="attachFile">附件文件名</param>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="parentDoi">主文件doi</param>
        /// <param name="attachType">附件类型</param>
        /// <returns></returns>
        public static bool UploadAttachFile(string attachFile, DataBaseType dbtype, string parentDoi, string attachType)
        {
            if (string.IsNullOrEmpty(attachFile))
            {
                return false;
            }
            string virtualTag = ConfigurationManager.AppSettings["virtualTag"];
            string virtualpath = Config.GetVirtalPath(virtualTag);
            //取得物理路径
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + virtualpath);
            //获取前缀
            string prefix = DRMS.BLL.DataBaseList.GetPrefix(dbtype.GetHashCode().ToString());
            string fileExtension = Path.GetExtension(attachFile);
            string returnPath = "\\" + prefix + "\\" + parentDoi + "\\Source\\" + Path.GetFileName(attachFile);

            if (!Directory.Exists(path + "\\" + prefix + "\\" + parentDoi + "\\Source"))
            {
                Directory.CreateDirectory(path + "\\" + prefix + "\\" + parentDoi + "\\Source");
            }
            try
            {
                if (File.Exists(path + returnPath))
                    File.Delete(path + returnPath);
                File.Move(attachFile, path + returnPath);
                //需要给附件表 添加数据
                Attachment attBll = new Attachment();
                AttachmentInfo attaItem = new AttachmentInfo();
                attaItem.PARENTDOI = parentDoi;
                attaItem.Name = Path.GetFileName(attachFile);
                attaItem.Sys_fld_Adddate = DateTime.Now;
                attaItem.Sys_fld_Adduser = Utility.GetUserName();
                attaItem.SYS_FLD_DOI = CNKI.BaseFunction.RandomId.Get();
                attaItem.SYS_FLD_FILEPATH = returnPath;
                attaItem.SYS_FLD_VIRTUALPATHTAG = virtualTag;
                attaItem.Type = attachType;
                attaItem.Sys_fld_filetype = Path.GetExtension(attachFile);
                return attBll.Add(attaItem);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据doi获取资源对象
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="doi"></param>
        /// <returns></returns>
        public static BaseModel GetResObjByDoi(DataBaseType dbtype, string doi)
        {
            BaseModel bm = null;
            switch (dbtype)
            {
                case DataBaseType.BOOKTDATA:
                    Book b = new Book();
                    bm = b.GetItem(doi);
                    break;
                case DataBaseType.CONFERENCEARTICLE:
                    ConferenceArticle conarticle = new ConferenceArticle();
                    bm = conarticle.GetItem(doi);
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    ConferencePaper conferencepaper = new ConferencePaper();
                    bm = conferencepaper.GetItem(doi);
                    break;
                case DataBaseType.CRITERION:
                    StdData stddata = new StdData();
                    bm = stddata.GetItem(doi);
                    break;
                case DataBaseType.REFERENCEBOOK:
                    ToolBook toolbook = new ToolBook();
                    bm = toolbook.GetItem(doi);
                    break;
                case DataBaseType.LOGICALDATABASE:
                    LogicalDataBase ldb = new LogicalDataBase();
                    bm = ldb.GetItem(doi);
                    break;
                case DataBaseType.AUDIODATA:
                    Audio audio = new Audio();
                    bm = audio.GetItem(doi);
                    break;
                case DataBaseType.JOURNALARTICLE:
                    JournalArticle jarticle = new JournalArticle();
                    bm = jarticle.GetItem(doi);
                    break;
                case DataBaseType.MAGAZINEARTICLE:
                    MagazineArticle marticle = new MagazineArticle();
                    bm = marticle.GetItem(doi);
                    break;
                case DataBaseType.NEWSPAPERARTICLE:
                    NewsPaperArticle narticle = new NewsPaperArticle();
                    bm = narticle.GetItem(doi);
                    break;
                case DataBaseType.PICDATA:
                    Pic pic = new Pic();
                    bm = pic.GetItem(doi);
                    break;
                case DataBaseType.VIDEODATA:
                    Video video = new Video();
                    bm = video.GetItem(doi);
                    break;
                case DataBaseType.YEARBOOK:
                    YearBookYear yarticle = new YearBookYear();
                    bm = yarticle.GetItem(doi);
                    break;
                case DataBaseType.CONTRACT:
                    Contract contract = new Contract();
                    bm = contract.GetItem(doi);
                    if (bm != null && !string.IsNullOrEmpty(bm.SYS_FLD_FILEPATH))
                    {
                        bm.SYS_FLD_COVERPATH = bm.SYS_FLD_FILEPATH.Replace(".pdf", ".jpg");
                    }
                    break;
                case DataBaseType.AUTHOR:
                    Author author = new Author();
                    bm = author.GetItem(doi);
                    break;
                case DataBaseType.ORG:
                    Org org = new Org();
                    bm = org.GetItem(doi);
                    break;
                case DataBaseType.ORIGINALDATA:
                    OriginalData original = new OriginalData();
                    bm = original.GetItem(doi);
                    break;
                case DataBaseType.THESIS:
                    Thesis thesis = new Thesis();
                    bm = thesis.GetItem(doi);
                    break;
                case DataBaseType.JOURNALYEAR:
                    JournalYear journalyear = new JournalYear();
                    bm = journalyear.GetItem(doi);
                    break;
                case DataBaseType.NEWSPAPERYEAR:
                    NewsPaperYear newspaperyear = new NewsPaperYear();
                    bm = newspaperyear.GetItem(doi);
                    break;
                case DataBaseType.MAGAZINEYEAR:
                    MagazineYear magazineyear = new MagazineYear();
                    bm = magazineyear.GetItem(doi);
                    break;
                default:
                    break;
            }
            return bm;
        }

        /// <summary>  
        /// 获取下载类型  
        /// </summary>  
        /// <param name="fileExt"></param>  
        /// <returns></returns>  
        public static string GetContentType(string fileExt)
        {
            string ContentType;
            switch (fileExt)
            {
                case "asf":
                    ContentType = "video/x-ms-asf"; break;
                case "avi":
                    ContentType = "video/avi"; break;
                case "doc":
                case "docx":
                    ContentType = "application/msword"; break;
                case "zip":
                    ContentType = "application/zip"; break;
                case "xls":
                case "xlsx":
                    ContentType = "application/vnd.ms-excel"; break;
                case "gif":
                    ContentType = "image/gif"; break;
                case "jpg":
                case "jpeg":
                case ".bmp":
                case ".jfif":
                    ContentType = "image/jpeg"; break;
                case "png":
                    ContentType = "image/png"; break;
                case ".tif":
                case ".tiff":
                    ContentType = "image/tiff"; break;
                case "wav":
                    ContentType = "audio/wav"; break;
                case "mp3":
                    ContentType = "audio/mpeg3"; break;
                case "mpg":
                    ContentType = "video/mpeg"; break;
                case "mepg":
                    ContentType = "video/mpeg"; break;
                case "rtf":
                    ContentType = "application/rtf"; break;
                case "html":
                case "htm":
                    ContentType = "text/html"; break;
                case "xml":
                    ContentType = "text/xml"; break;
                case "txt":
                    ContentType = "text/plain"; break;
                case "pdf":
                    ContentType = "application/pdf"; break;
                default:
                    ContentType = "application/octet-stream";
                    break;
            }
            return ContentType;
        }
    }
}