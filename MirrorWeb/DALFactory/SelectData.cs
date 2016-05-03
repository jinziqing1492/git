using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

using DRMS.IDAL;

namespace DRMS.DALFactory
{
   public class SelectData
    {
        private static readonly string path = ConfigurationManager.AppSettings["DAL"];
        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectData() { }

        /// <summary>
        /// 获取一个图书数据的实例
        /// </summary>
        /// <returns></returns>
        public static IBook CreateBook()
        {
            string classname = path + ".Book";
            return (IBook)Assembly.Load(path).CreateInstance(classname);
        }
        public static IControlCss CreateControlCss()
        {
            string classname = path + ".ControlCss";
            return (IControlCss)Assembly.Load(path).CreateInstance(classname);
        }
       /// <summary>
       /// 获取一个工具书数据的实例
       /// </summary>
       /// <returns></returns>
        public static IToolBook CreateToolBook()
        {
            string classname = path + ".ToolBook";
            return (IToolBook)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个术语数据的实例
        /// </summary>
        /// <returns></returns>
        public static ITerminology CreateTerminology()
        {
            string classname = path + ".Terminology";
            return (ITerminology)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个术语内容数据的实例
        /// </summary>
        /// <returns></returns>
        public static ISubTerminology  CreateSubTerminology()
        {
            string classname = path + ".Subterminology";
            return (ISubTerminology)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个章节数据的实例
        /// </summary>
        /// <returns></returns>
        public static IChapter CreateChapter()
        {
            string classname = path + ".Chapter";
            return (IChapter)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个期刊数据的实例
        /// </summary>
        /// <returns></returns>
        public static IJournalInfo CreateJournalInfo()
        {
            string classname = path + ".JournalInfo";
            return (IJournalInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个期刊年数据的实例
        /// </summary>
        /// <returns></returns>
        public static IJournalYearInfo CreateJournalYearInfo()
        {
            string classname = path + ".JournalYearInfo";
            return (IJournalYearInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个期刊文章数据的实例
        /// </summary>
        /// <returns></returns>
        public static IJournalArticle CreateJournalArticle()
        {
            string classname = path + ".JournalArticle";
            return (IJournalArticle)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个学位论文数据的实例
        /// </summary>
        /// <returns></returns>
        public static IThesis CreateThesis()
        {
            string classname = path + ".Thesis";
            return (IThesis)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个会议论文数据的实例
        /// </summary>
        /// <returns></returns>
        public static IConferencePaper CreateConferencePager()
        {
            string classname = path + ".ConferencePaper";
            return (IConferencePaper)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个会议论文文章数据的实例
        /// </summary>
        /// <returns></returns>
        public static IConferenceArticle CreateConferenceArticle()
        {
            string classname = path + ".ConferenceArticle";
            return (IConferenceArticle)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个存放标准的数据的实例
        /// </summary>
        /// <returns></returns>
        public static IStdData CreateStdData()
        {
            string classname = path + ".StdData";
            return (IStdData)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个杂志信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static IMagazineInfo CreateMagazineInfo()
        {
            string classname = path + ".MagazineInfo";
            return (IMagazineInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个杂志年信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static IMagazineYearInfo CreateMagazineYearInfo()
        {
            string classname = path + ".MagazineYearInfo";
            return (IMagazineYearInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个杂志文章数据的实例
        /// </summary>
        /// <returns></returns>
        public static IMagazineArticle CreateMagazineArticle()
        {
            string classname = path + ".MagazineArticle";
            return (IMagazineArticle)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个报纸信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static INewsPaperInfo CreateNewsPaperInfo()
        {
            string classname = path + ".NewsPaperInfo";
            return (INewsPaperInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个报纸年信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static INewsPaperYearInfo CreateNewsPaperYearInfo()
        {
            string classname = path + ".NewsPaperYearInfo";
            return (INewsPaperYearInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个报纸文章数据的实例
        /// </summary>
        /// <returns></returns>
        public static INewsPaperArticle CreateNewsPaperArticle()
        {
            string classname = path + ".NewsPaperArticle";
            return (INewsPaperArticle)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个年鉴年数据的实例
        /// </summary>
        /// <returns></returns>
        public static IYearBookYearInfo CreateYearBookYearInfo()
        {
            string classname = path + ".YearBookYearInfo";
            return (IYearBookYearInfo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个年鉴文章数据的实例
        /// </summary>
        /// <returns></returns>
        public static IYearBookArticle CreateYearBookArticle()
        {
            string classname = path + ".YearBookArticle";
            return (IYearBookArticle)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个视频数据的实例
        /// </summary>
        /// <returns></returns>
        public static IVideo CreateVideo()
        {
            string classname = path + ".Video";
            return (IVideo)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个音频数据的实例
        /// </summary>
        /// <returns></returns>
        public static IAudio CreateAudio()
        {
            string classname = path + ".Audio";
            return (IAudio)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个图片数据的实例
        /// </summary>
        /// <returns></returns>
        public static IPic CreatePic()
        {
            string classname = path + ".Pic";
            return (IPic)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个附件数据的实例
        /// </summary>
        /// <returns></returns>
        public static IAttachment CreateAttachment()
        {
            string classname = path + ".Attachment";
            return (IAttachment)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个合同数据的实例
        /// </summary>
        /// <returns></returns>
        public static IContract CreateContract()
        {
            string classname = path + ".Contract";
            return (IContract)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个作者数据的实例
        /// </summary>
        /// <returns></returns>
        public static IAuthor CreateAuthor()
        {
            string classname = path + ".Author";
            return (IAuthor)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个机构数据的实例
        /// </summary>
        /// <returns></returns>
        public static IOrg CreateOrg()
        {
            string classname = path + ".Org";
            return (IOrg)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个分类数据的实例
        /// </summary>
        /// <returns></returns>
        public static ITheme CreateTheme()
        {
            string classname = path + ".Theme";
            return (ITheme)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个配置数据的实例
        /// </summary>
        /// <returns></returns>
        public static IConfig CreateConfig()
        {
            string classname = path + ".Config";
            return (IConfig)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个数据库数据的实例
        /// </summary>
        /// <returns></returns>
        public static IDataBaseList CreateDataBaseList()
        {
            string classname = path + ".DataBaseList";
            return (IDataBaseList)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个用户数据的实例
        /// </summary>
        /// <returns></returns>
        public static IUser CreateUser()
        {
            string classname = path + ".User";
            return (IUser)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个日志数据的实例
        /// </summary>
        /// <returns></returns>
        public static ILog CreateLog()
        {
            string classname = path + ".Log";
            return (ILog)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个逻辑数据库数据的实例
        /// </summary>
        /// <returns></returns>
        public static ILogicalDataBase CreateLogicalDataBase()
        {
            string classname = path + ".LogicalDataBase";
            return (ILogicalDataBase)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个用户包库数据的实例
        /// </summary>
        /// <returns></returns>
        public static IUserLDB CreateUserLDB()
        {
            string classname = path + ".UserLDB";
            return (IUserLDB)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个U盘电子书数据的实例
        /// </summary>
        /// <returns></returns>
        public static IUEBook CreateUEBook()
        {
            string classname = path + ".UEBook";
            return (IUEBook)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个U盘电子书包含信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static IUEBookList CreateUEBookList()
        {
            string classname = path + ".UEBookList";
            return (IUEBookList)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个收藏信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static IFavoriteData CreateFavoriteData()
        {
            string classname = path + ".FavoriteData";
            return (IFavoriteData)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个订阅信息数据的实例
        /// </summary>
        /// <returns></returns>
        public static ISubscribe CreateSubscribe()
        {
            string classname = path + ".Subscribe";
            return (ISubscribe)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个任务数据的实例
        /// </summary>
        /// <returns></returns>
        public static IMission CreateMission()
        {
            string classname = path + ".Mission";
            return (IMission)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个原始资料数据的实例
        /// </summary>
        /// <returns></returns>
        public static IOriginalData CreateOriginalData()
        {
            string classname = path + ".OriginalData";
            return (IOriginalData)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个原始资料分类数据的实例
        /// </summary>
        /// <returns></returns>
        public static IOriginalDataClass CreateOriginalDataClass()
        {
            string classname = path + ".OriginalDataClass";
            return (IOriginalDataClass)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个申请下载数据的实例
        /// </summary>
        /// <returns></returns>
        public static IDownLoadApply CreateDownLoadApply()
        {
            string classname = path + ".DownLoadApply";
            return (IDownLoadApply)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个导出任务的实例
        /// </summary>
        /// <returns></returns>
        public static IExportTask CreateExportTask()
        {
            string classname = path + ".ExportTask";
            return (IExportTask)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个导出任务资源列表的实例
        /// </summary>
        /// <returns></returns>
        public static IExportTaskList CreateExportTaskList()
        {
            string classname = path + ".ExportTaskList";
            return (IExportTaskList)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个中图分类的实例
        /// </summary>
        /// <returns></returns>
        public static IZTFlfCls CreateZTFlfCls()
        {
            string classname = path + ".ZTFlfCls";
            return (IZTFlfCls)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取一个部门管理类的实例
        /// </summary>
        /// <returns></returns>
        public static IDepartment CreateDepartment()
        {
            string classname = path + ".DepartmentDAL";
            return (IDepartment)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取导出excel所用系统表的实例
        /// </summary>
        /// <returns></returns>
        public static IHotstarSysField CreateHotstarSysField()
        {
            string classname = path + ".HotstarSysField";
            return (IHotstarSysField)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取自有资源类型类的实例
        /// </summary>
        /// <returns></returns>
        public static IOwnerResourceType CreateOwnerResourceType()
        {
            string classname = path + ".OwnerResourceType";
            return (IOwnerResourceType)Assembly.Load(path).CreateInstance(classname);
        }

        /// <summary>
        /// 获取资源数据的实例
        /// </summary>
        /// <returns></returns>
        public static IResourceData CreateResourceData()
        {
            string classname = path + ".ResourceData";
            return (IResourceData)Assembly.Load(path).CreateInstance(classname);
        }
        
        /// <summary>
        /// 获取资源数据的实例
        /// </summary>
        /// <returns></returns>
        public static IResourceType CreateResourceType()
        {
            string classname = path + ".ResourceType";
            return (IResourceType)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
