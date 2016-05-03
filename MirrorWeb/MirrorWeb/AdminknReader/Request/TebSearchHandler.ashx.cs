using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using DRMS.BLL;
using DRMS.Model;
using DRMS.MirrorWeb.Utility;

namespace AdminKNReader.Request
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    ///// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TebSearchHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strPage = context.Request["p"];
            string strBookID = context.Request["b"];
            string strWidth = context.Request["w"];
            string strHeight = context.Request["h"];
            string strText = context.Request["t"];
            string mPressID = context.Request["press"];

            //string strPath = string.Format("{0}\\{1}.teb", ConfigurationManager.AppSettings["BookPath"], strBookID);
            string strPath = GetRealPath(strBookID, mPressID);
            knbookLib.ReaderExWrap ReaderExObj = GetReaderEx(strBookID, strPath);

            knbookLib.SearchResults ResultsObj = (knbookLib.SearchResults)ReaderExObj.Search(strText, Convert.ToInt32(strPage));

            knbookLib.Box BoxObj = null;

            int iX = 0;
            int iY = 0;
            int iWidth = Convert.ToInt32(strWidth);
            int iHeight = Convert.ToInt32(strHeight);
            ReaderExObj.GetPageSize(Convert.ToInt32(strPage), out iX, out iY);
            StringBuilder sbRet = new StringBuilder();
            if (ResultsObj != null)
            {
                for (int i = 0; i < ResultsObj.Count; i++)
                {
                    for (int j = 0; j < ResultsObj.get_RectCount(i); j++)
                    {
                        BoxObj = (knbookLib.Box)ResultsObj.get_Rect(i, j);
                        sbRet.AppendFormat("<div style='left:{0}px;top:{1}px;height:{2}px;width:{3}px;' class='overdiv'></div>",
                            (BoxObj.Left * iWidth) / iX,
                            (BoxObj.Top * iHeight) / iY,
                            ((BoxObj.Bottom - BoxObj.Top) * iHeight) / iY,
                            ((BoxObj.Right - BoxObj.Left) * iWidth) / iX);
                    }
                }
            }

            string strRet = string.Format("{0}${1}", strPage, sbRet.ToString());
            sbRet = null;

            context.Response.Write(strRet);

        }

        private knbookLib.ReaderExWrap GetReaderEx(string BookID, string Path)
        {
            Hashtable htHandler = null;
            if (HttpContext.Current.Application["ReaderHandler"] == null)
            {
                htHandler = new Hashtable();
                HttpContext.Current.Application["ReaderHandler"] = htHandler;
            }
            else
            {
                htHandler = (Hashtable)HttpContext.Current.Application["ReaderHandler"];
            }

            knbookLib.ReaderExWrap r = null;

            if (htHandler[BookID] != null)
            {
                r = (knbookLib.ReaderExWrap)htHandler[BookID];
            }
            else
            {
                r = new knbookLib.ReaderExWrap();
                lock (htHandler.SyncRoot)
                {
                    if (htHandler[BookID] == null)
                    {
                        htHandler.Add(BookID, r);
                    }
                }
                r.Open(Path, null);
            }

            return r;
        }

        public string GetRealPath(string doi, string mPressID)
        {
            string FilePath = string.Empty;
            if (mPressID == DataBaseType.BOOKTDATA.GetHashCode().ToString())
            {
                Book bookdal = new Book();
                BookInfo item = bookdal.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.REFERENCEBOOK.GetHashCode().ToString())
            {
                ToolBook bookdal = new ToolBook();
                ToolBookInfo item = bookdal.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.CRITERION.GetHashCode().ToString())
            {
                //标准
                StdData bookdal = new StdData();
                StdDataInfo item = bookdal.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.CONFERENCEPAPER.GetHashCode().ToString())
            {
                //论文集
                ConferencePaper paper = new ConferencePaper();
                ConferencePaperInfo item = paper.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.CONFERENCEARTICLE.GetHashCode().ToString())
            {
                //会议论文文章
                ConferenceArticle article = new ConferenceArticle();
                ConferenceArticleInfo item = article.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.BOOKCHAPTER.GetHashCode().ToString() || mPressID == DataBaseType.STDDATACHAPTER.GetHashCode().ToString())
            {
                //章节
                Chapter bll = new Chapter();
                ChapterInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.THESIS.GetHashCode().ToString())
            {
                //学位论文
                Thesis bll = new Thesis();
                ThesisInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.NEWSPAPER.GetHashCode().ToString() || mPressID == DataBaseType.NEWSPAPERYEAR.GetHashCode().ToString())
            {
                //报纸年表
                NewsPaperYear bll = new NewsPaperYear();
                NewsPaperYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.MAGAZINE.GetHashCode().ToString() || mPressID == DataBaseType.MAGAZINEYEAR.GetHashCode().ToString())
            {
                //杂志年表
                MagazineYear bll = new MagazineYear();
                MagazineYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.JOURNAL.GetHashCode().ToString() || mPressID == DataBaseType.JOURNALYEAR.GetHashCode().ToString())
            {
                //杂志年表
                JournalYear bll = new JournalYear();
                JournalYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.YEARBOOK.GetHashCode().ToString())
            {
                //杂志年表
                YearBookYear bll = new YearBookYear();
                YearBookYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            else if (mPressID == DataBaseType.CONTRACT.GetHashCode().ToString())
            {
                //合同表
                Contract bll = new Contract();
                ContractInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                }
            }
            return FilePath;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
