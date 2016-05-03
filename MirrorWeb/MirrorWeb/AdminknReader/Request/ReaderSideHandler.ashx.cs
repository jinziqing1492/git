using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;

using DRMS.BLL;
using DRMS.Model;
//using DRMS.Common;

namespace AdminKNReader.request
{
    /// <summary>
    /// ReaderSideHandler 的摘要说明
    /// </summary>
    public class ReaderSideHandler : IHttpHandler
    {

        string strPressId = string.Empty;
        string strCatalog = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strBookID = context.Request["b"];
            string strHeight = context.Request["h"];

            string strOperation = context.Request["op"];
            string strRet = string.Empty;
            strPressId = context.Request["p"];

            switch (strOperation)
            {
                case "0":   //图书信息
                    strRet = GetBookInfo(strBookID, strPressId);
                    break;
                case "1":   //目录
                    // strRet = "";
                    if (strPressId == DataBaseType.NEWSPAPER.GetHashCode().ToString())
                    {
                        strRet = GetCatolog(strBookID, strPressId);
                    }
                    else
                    {
                        GetBookInfo(strBookID, strPressId);
                        strRet = BindCataLog(strCatalog);
                    }
                    break;
                case "2":   //评论
                    strRet = "";
                    //strRet = GetComment(strHeight, strBookID);
                    break;
                case "3":   //历史
                    //strRet = GetHistory(strBookID, strHeight, context);
                    strRet = "";
                    break;
                default:
                    break;
            }

            context.Response.Write(strRet);
        }

        /// <summary>
        /// 获取目录
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="StrPressId"></param>
        /// <returns></returns>
        private string GetCatolog(string BookID, string strPressId)
        {
            StringBuilder str = new StringBuilder();
            string FormatStr = "<p><a href=\"javascript:void(0);\">{0}</a><span style=\"display:none;\">{1}</span></p>";
            if (strPressId == DataBaseType.NEWSPAPER.GetHashCode().ToString())
            {
                NewsPaperArticle bll = new NewsPaperArticle();
                int recordcount = 0;
                IList<NewsPaperArticleInfo> mylist = bll.GetList(" SYS_FLD_PARENTDOI=0 and parentdoi=\"" + BookID + "\" order by PAGENUM", 1, 100, out recordcount, false);

                if (mylist != null)
                {
                    foreach (NewsPaperArticleInfo item in mylist)
                    {
                        str.AppendFormat(FormatStr, item.Title, item.PageNUM);
                    }
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// 获取标准规范类的基本信息
        /// </summary>
        /// <param name="bookDoi"></param>
        /// <param name="bookType"></param>
        /// <returns></returns>
        private string GetBookInfo(string bookDoi, string bookType)
        {
            string result = string.Empty;

            if (bookType == DataBaseType.CRITERION.GetHashCode().ToString())
            {
                //标准
                result = GetStdInfo(bookDoi);
            }
            else if (bookType == DataBaseType.BOOKTDATA.GetHashCode().ToString())
            {
                //图书
                result = GetBookInfo1(bookDoi);
            }
            else if (bookType == DataBaseType.REFERENCEBOOK.GetHashCode().ToString())
            {
                //工具书
                result = GetToolBookInfo(bookDoi);
            }
            else if (bookType == DataBaseType.THESIS.GetHashCode().ToString())
            {
                //学位论文
                result = GetThesisInfo(bookDoi);
            }
            else if (bookType == DataBaseType.CONFERENCEPAPER.GetHashCode().ToString())
            {
                //会议论文
                result = GetConPaperInfo(bookDoi);
            }
            else if (bookType == DataBaseType.BOOKCHAPTER.GetHashCode().ToString() || bookType == DataBaseType.STDDATACHAPTER.GetHashCode().ToString())
            {
                //章节
                result = GetChapterInfo(bookDoi);
            }
            else if (bookType == DataBaseType.CONFERENCEARTICLE.GetHashCode().ToString())
            {
                //会议论文文章
                result = GetConArticleInfo(bookDoi);
            }
            else if (bookType == DataBaseType.NEWSPAPER.GetHashCode().ToString() || bookType == DataBaseType.NEWSPAPERYEAR.GetHashCode().ToString())
            {
                //报纸的基本信息
                result = GetNewsPaperInfo(bookDoi);
            }
            else if (bookType == DataBaseType.JOURNAL.GetHashCode().ToString() || bookType == DataBaseType.JOURNALYEAR.GetHashCode().ToString())
            {
                //期刊的基本信息
                result = GetJournalInfo(bookDoi);
            }
            else if (bookType == DataBaseType.MAGAZINE.GetHashCode().ToString() || bookType == DataBaseType.MAGAZINEYEAR.GetHashCode().ToString())
            {
                //杂志的基本信息
                result = GetMagazineInfo(bookDoi);
            }
            else if (bookType == DataBaseType.YEARBOOK.GetHashCode().ToString())
            {
                //年鉴的基本信息
                result = GetYearBookInfo(bookDoi);
            }
            else if (bookType == DataBaseType.CONTRACT.GetHashCode().ToString())
            {
                //合同 
                //result = GetContractInfo(bookDoi);
            }
            else if (bookType == DataBaseType.ENGLISHRES.GetHashCode().ToString())
            {
                //期刊的基本信息
                result = GetJournalInfo(bookDoi, "english");
            }
            else if (bookType == DataBaseType.STUDYRES.GetHashCode().ToString())
            {
                //期刊的基本信息
                result = GetJournalInfo(bookDoi, "study");
            }
            else if (bookType == DataBaseType.OWNERRES.GetHashCode().ToString())
            {
                //期刊的基本信息
                result = GetJournalInfo(bookDoi, "owner");
            }

            return result;
        }

        /// <summary>
        /// 获取图书的基本信息
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        private string GetBookInfo1(string BookID)
        {
            Book bookdal = new Book();
            BookInfo dt = bookdal.GetItem(BookID);
            string strRet = string.Empty;
            if (dt != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a></div>"
                                    , BookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + dt.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(dt.SYS_FLD_COVERPATH)
                                    , dt.Name
                                    , dt.Name
                                    , dt.Author
                                    , dt.Author
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.IssueDep, 50, "")
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.IssueDep, 50, "")
                                    , dt.IssueDate.ToShortDateString()
                                    , dt.IssueDate.ToShortDateString()
                                    );
                strCatalog = dt.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取标准信息
        /// </summary>
        /// <param name="Doi"></param>
        /// <returns></returns>
        private string GetStdInfo(string BookID)
        {

            StdData bookdal = new StdData();
            StdDataInfo dt = bookdal.GetItem(BookID);
            string strRet = string.Empty;
            if (dt != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=2' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=2' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>主编单位：{5}</em><br /><em title='{6}'>批准单位：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=2' target='_blank' title='{2}'>{3}</a></div>"
                                    , BookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + dt.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(dt.SYS_FLD_COVERPATH)
                                    , dt.Name
                                    , dt.Name
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.ProposeDep, 50, "")
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.ProposeDep, 50, "")
                                    , dt.ApproveDep
                                    , dt.ApproveDep
                                    , dt.Dateissued.ToShortDateString()
                                    , dt.Dateissued.ToShortDateString()
                                    );
                strCatalog = dt.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取图书的基本信息
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        private string GetBookInfo(string BookID)
        {
            Book bookdal = new Book();
            BookInfo dt = bookdal.GetItem(BookID);
            string strRet = string.Empty;
            if (dt != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a></div>"
                                    , BookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + dt.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(dt.SYS_FLD_COVERPATH)
                                    , dt.Name
                                    , dt.Name
                                    , dt.Author
                                    , dt.Author
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.IssueDep, 50, "")
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.IssueDep, 50, "")
                                    , dt.IssueDate.ToShortDateString()
                                    , dt.IssueDate.ToShortDateString()
                                    );
                strCatalog = dt.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取工具书的基本信息
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        private string GetToolBookInfo(string BookID)
        {
            ToolBook bookdal = new ToolBook();
            ToolBookInfo dt = bookdal.GetItem(BookID);
            string strRet = string.Empty;
            if (dt != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a></div>"
                                    , BookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + dt.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(dt.SYS_FLD_COVERPATH)
                                    , dt.Name
                                    , dt.Name
                                    , dt.Author
                                    , dt.Author
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.IssueDep, 50, "")
                                    , CNKI.BaseFunction.NormalFunction.GetSubStrOther(dt.IssueDep, 50, "")
                                    , dt.IssueDate.ToShortDateString()
                                    , dt.IssueDate.ToShortDateString()
                                    );
                strCatalog = dt.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取论文集中文章的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetConArticleInfo(string bookID)
        {
            ConferenceArticle bll = new ConferenceArticle();
            ConferenceArticleInfo info = bll.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                //通过parentdoi获取其所在论文集，然后获取其出版信息
                string issueDep = "";
                string IssueDate = "";
                string vpath = "";
                string path = "";
                ConferencePaper paper = new ConferencePaper();
                ConferencePaperInfo item = paper.GetItem(info.ParentDoi);
                if (item != null)
                {
                    issueDep = item.IssueDep;
                    IssueDate = item.IssueDate.ToShortDateString();
                    vpath = item.SYS_FLD_VIRTUALPATHTAG;
                    path = item.SYS_FLD_COVERPATH;
                }

                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + vpath + "&path=" + path
                                    , info.Name
                                    , info.Name
                                    , info.Author
                                    , info.Author
                                    , issueDep
                                    , issueDep
                                    , IssueDate
                                    , IssueDate
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }
        /// <summary>
        /// 获取章节的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetChapterInfo(string bookID)
        {
            Chapter bll = new Chapter();
            ChapterInfo info = bll.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                //通过parentDoi获取书籍的作者 出版社 及出版时间等信息
                string author = "";
                string issueDep = "";
                string issueDate = "";
                string vpath = "";
                string path = "";

                if (info.doctype == 0)
                {
                    Book book = new Book();
                    BookInfo item = book.GetItem(bookID);
                    if (item != null)
                    {
                        author = item.Author;
                        issueDep = item.IssueDep;
                        issueDate = item.IssueDate.ToShortDateString();
                        vpath = item.SYS_FLD_VIRTUALPATHTAG;
                        path = item.SYS_FLD_COVERPATH;
                    }
                }
                else if (info.doctype == 1)
                {
                    StdData std = new StdData();
                    StdDataInfo item = std.GetItem(bookID);
                    if (item != null)
                    {
                        author = CNKI.BaseFunction.NormalFunction.GetSubStrOther(item.ProposeDep, 50, "");
                        issueDep = item.ApproveDep;
                        issueDate = item.Dateissued.ToShortDateString();
                        vpath = item.SYS_FLD_VIRTUALPATHTAG;
                        path = item.SYS_FLD_COVERPATH;
                    }
                }


                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=1' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + vpath + "&path=" + path
                                    , info.Title
                                    , info.Title
                                    , author
                                    , author
                                    , issueDep
                                    , issueDep
                                    , issueDate
                                    , issueDate
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }
        /// <summary>
        /// 获取学位论文的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetThesisInfo(string bookID)
        {
            Thesis bll = new Thesis();
            ThesisInfo info = bll.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=9' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=9' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>学院：{7}</em><br /> 提交日期：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=9' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH)
                                    , info.Name
                                    , info.Name
                                    , info.Author
                                    , info.Author
                                    , info.Academy
                                    , info.Academy
                                    , info.PaperSubmissionDate.ToShortDateString()
                                    , info.PaperSubmissionDate.ToShortDateString()
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }
        /// <summary>
        /// 获取论文集的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetConPaperInfo(string bookID)
        {
            ConferencePaper paper = new ConferencePaper();
            ConferencePaperInfo info = paper.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=5' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/BookDetail.aspx?doi={0}&type=5' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>作者：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/BookDetail.aspx?doi={0}&type=5' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH)
                                    , info.Name
                                    , info.Name
                                    , info.Author
                                    , info.Author
                                    , info.IssueDep
                                    , info.IssueDep
                                    , info.IssueDate.ToShortDateString()
                                    , info.IssueDate.ToShortDateString()
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取报纸的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetNewsPaperInfo(string bookID)
        {
            NewsPaperYear paper = new NewsPaperYear();
            NewsPaperYearInfo info = paper.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=8' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/JournalDetail.aspx?doi={0}&type=8' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>年期：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=8' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH)
                                    , info.CNAME
                                    , info.CNAME
                                    , info.YEAR + "年" + info.ISSUE + "期"
                                    , info.YEAR + "年" + info.ISSUE + "期"
                                    , info.Pubdep
                                    , info.Pubdep
                                    , info.Pubdate.ToShortDateString()
                                    , info.Pubdate.ToShortDateString()
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取期刊的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetJournalInfo(string bookID, string res = "")
        {
            JournalYear bll = new JournalYear(res);
            JournalYearInfo info = bll.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=4' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/JournalDetail.aspx?doi={0}&type=4' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>年期：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=4' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH)
                                    , info.CNAME
                                    , info.CNAME
                                    , info.YEAR + "年" + info.ISSUE + "期"
                                    , info.YEAR + "年" + info.ISSUE + "期"
                                    , info.Pubdep
                                    , info.Pubdep
                                    , info.Pubdate.ToShortDateString()
                                    , info.Pubdate.ToShortDateString()
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取杂志的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetMagazineInfo(string bookID)
        {
            MagazineYear bll = new MagazineYear();
            MagazineYearInfo info = bll.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=7' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/JournalDetail.aspx?doi={0}&type=7' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>年期：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=7' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH)
                                    , info.CNAME
                                    , info.CNAME
                                    , info.YEAR + "年" + info.ISSUE + "期"
                                    , info.YEAR + "年" + info.ISSUE + "期"
                                    , info.Pubdep
                                    , info.Pubdep
                                    , info.Pubdate.ToShortDateString()
                                    , info.Pubdate.ToShortDateString()
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 获取年鉴的基本信息
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private string GetYearBookInfo(string bookID)
        {
            YearBookYear bll = new YearBookYear();
            YearBookYearInfo info = bll.GetItem(bookID);
            string strRet = string.Empty;
            if (info != null)
            {
                strRet = string.Format(@"<div id='read_book' class='read_book'><p class='book_pic'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=8' target='_blank' title='{2}'>
                                <img src='{1}' alt='{2}' width='100' /></a>
                                 <br/ ><a href='/view/JournalDetail.aspx?doi={0}&type=8' target='_blank' title='{2}'>{3}</a>
                               </p>
                                <p> <em title='{4}'>年份：{5}</em><br /><em title='{6}'>出版社：{7}</em><br /> 出版时间：{8}</p>
                                </div>
                                <div id='readbook_name' class='readbook_name' style='display: none;'>
                                <a href='/view/JournalDetail.aspx?doi={0}&type=8' target='_blank' title='{2}'>{3}</a></div>"
                                    , bookID
                                    , "/view/ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH)
                                    , info.Name
                                    , info.Name
                                    , info.Year + "年"
                                    , info.Year + "年"
                                    , info.IssueDep
                                    , info.IssueDep
                                    , info.IssueDate.ToShortDateString()
                                    , info.IssueDate.ToShortDateString()
                                    );
                strCatalog = info.SYS_FLD_CATALOG;
            }
            return strRet;
        }

        /// <summary>
        /// 绑定目录
        /// </summary>
        /// <param name="strXml"></param>
        private string BindCataLog(string strXml)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(strXml))
            {
                return result;
            }
            strXml = strCatalog;
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(strXml);
            }
            catch
            {
                return result;
            }
            string xsltPath = "/xslt/Catalog.xslt";

            result = DRMS.MirrorWeb.Utility.Utility.XmlToString(xd, xsltPath);
            return result;
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