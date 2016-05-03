using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

using DRMS.Model;
using DRMS.BLL;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class JournalDetail : System.Web.UI.Page
    {
        protected string mType { get; set; }
        protected string vpath { get; set; }
        protected string filename { get; set; }

        protected string userIP { get; set; }
        public string DataBaseName { get; set; }


        protected string KeyWordUrlFormat = "<a  href=\"/view/DBThemeNav.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";

        protected string NewsPaperArticleUrl = "ArticleDetail.aspx?doi={0}&type={1}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookdoi = Request["doi"];
                //type=1为标准 0为图书 2为工具书
                string type = Tool.NormalFunction.GetQueryString("type", "1");
                mType = type;
                DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
                string sql = mydbtype.GetHashCode().ToString();
                DataBaseName = EnumDescription.GetFieldText(mydbtype) + "&nbsp;>&nbsp;细览页";
                hidDoi.Value = bookdoi;
                BindData(bookdoi, type);

            }
        }

        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="bookdoi"></param>
        /// <param name="type"></param>
        protected void BindAttachment(string bookdoi, string type, string bookName, string bookPath)
        {
            Attachment atta = new Attachment();
            int recordcount = 0;
            IList<AttachmentInfo> mylist = atta.GetList("parentdoi=\"" + bookdoi + "\" ", 1, 100, out recordcount, false);

            StringBuilder str = new StringBuilder();
            string formali = "<a href=\"#\" ><li><span class=\"attname\">{2}:{0}  </span><span class=\"attopt\"  doi=\"{1}\" attname=\"{3}\" type=\"0\" bookname=\"{4}\">申请下载</span></li></a>";
            string formali2 = "<a href=\"#\" ><li><span class=\"attname\">{2}:{0}  </span><span class=\"attopt\"  doi=\"{1}\" attname=\"{3}\" type=\"1\" bookname=\"{4}\">申请下载</span></li></a>";

            str.Append("<div class=\"singleDetail\"><h2>附件</h2> <div class=\"sgbmr\"><ul>");

            if (!string.IsNullOrEmpty(bookPath))
            {
                str.AppendFormat(formali2, "pdf文件", HttpUtility.UrlEncode(bookPath), "主文件", HttpUtility.UrlEncode(bookName), HttpUtility.UrlEncode(bookName));

            }

            if (mylist != null && mylist.Count > 0)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    str.AppendFormat(formali, mylist[i].Name, mylist[i].SYS_FLD_DOI, mylist[i].Type, HttpUtility.UrlEncode(mylist[i].Name), HttpUtility.UrlEncode(bookName));
                }
            }
            str.Append("</ul></div></div>");
            ltl_attachment.Text = str.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="type"></param>
        private void BindData(string doi, string type)
        {
            string filePath = "";//对应的pdf文件名称
            string title = "";
            // string digest = "";
            string strXml = "";
            string path = "";
            string note = "";
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.JOURNAL || mydbtype == DataBaseType.JOURNALYEAR)
            {
                JournalYear bll = new JournalYear();
                JournalYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                filename = HttpUtility.UrlEncode(filename);
                title = info.CNAME;
                strXml = info.SYS_FLD_CATALOG;
                note = info.Note;
                string pubdate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【期刊类型】</span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【所属年期】</span>{0}</li>", info.YEAR + "年" + info.ISSUE + "期");
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", pubdate);
                sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                // sb.AppendFormat("<li><span>【实施时间】</span>{0}</li>", dateimplate);
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.JOURNAL, LogType.BROWSE, doi, title, "浏览期刊");
                }

            }
            else if (mydbtype == DataBaseType.MAGAZINE || mydbtype == DataBaseType.MAGAZINEYEAR)
            {
                //根据doi 获取实体
                MagazineYear bll = new MagazineYear();
                MagazineYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                title = info.CNAME;
                strXml = info.SYS_FLD_CATALOG;
                filePath = info.SYS_FLD_FILEPATH;
                note = info.Note;
                //string resouceType = info.SYS_FLD_BOOKTYPE == 1 ? "2" : "0";
                string pubdate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【所属年期】</span>{0}</li>", info.YEAR + "年" + info.ISSUE + "期");
                sb.AppendFormat("<li><span>【本期主编】</span>{0}</li>", info.Chiefeditor);
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", pubdate);
                sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.MAGAZINE, LogType.BROWSE, doi, title, "浏览杂志");
                }
            }
            else if (mydbtype == DataBaseType.NEWSPAPER || mydbtype == DataBaseType.NEWSPAPERYEAR)
            {
                //根据doi 获取实体
                NewsPaperYear bll = new NewsPaperYear();
                NewsPaperYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                title = info.CNAME;
                // digest = info.Digest;
                strXml = info.SYS_FLD_CATALOG;
                filePath = info.SYS_FLD_FILEPATH;
                note = info.Note;
                //string resouceType = info.SYS_FLD_BOOKTYPE == 1 ? "2" : "0";
                string pubdate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【所属年期】</span>{0}</li>", info.YEAR + "年" + info.ISSUE + "期");
                sb.AppendFormat("<li><span>【本期主编】</span>{0}</li>", info.Chiefeditor);
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", pubdate);
                sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.NEWSPAPER, LogType.BROWSE, doi, title, "浏览报纸");
                }

            }
            else if (mydbtype == DataBaseType.YEARBOOK)
            {
                //根据doi 获取实体
                YearBookYear bll = new YearBookYear();
                YearBookYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                title = info.Name;
                // digest = info.Digest;
                strXml = info.SYS_FLD_CATALOG;
                filePath = info.SYS_FLD_FILEPATH;
                note = info.Note;
                //string resouceType = info.SYS_FLD_BOOKTYPE == 1 ? "2" : "0";
                string issueDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【年鉴年份】</span>{0}</li>", info.Year);
                sb.AppendFormat("<li><span>【ＩＳＢＮ】</span>{0}</li>", info.ISBN);
                sb.AppendFormat("<li><span>【出 版 社】</span>{0}</li>", info.PrintDep);
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", issueDate);
                sb.AppendFormat("<li><span>【责任说明】</span>{0}</li>", info.LiabilityDesc);
                sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.YEARBOOK, LogType.BROWSE, doi, title, "浏览年鉴");
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                string res = "english";
                JournalYear bll = new JournalYear(res);
                JournalYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                filename = HttpUtility.UrlEncode(filename);
                title = info.CNAME;
                strXml = info.SYS_FLD_CATALOG;
                note = info.Note;
                string pubdate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【期刊类型】</span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【所属年期】</span>{0}</li>", info.YEAR + "年" + info.ISSUE + "期");
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", pubdate);
                sb.AppendFormat("<li><span>【关 键 词】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                // sb.AppendFormat("<li><span>【实施时间】</span>{0}</li>", dateimplate);
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.ENGLISHRES, LogType.BROWSE, doi, title, "浏览期刊");
                }

            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                string res = "study";
                JournalYear bll = new JournalYear(res);
                JournalYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                filename = HttpUtility.UrlEncode(filename);
                title = info.CNAME;
                strXml = info.SYS_FLD_CATALOG;
                note = info.Note;
                string pubdate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【期刊类型】</span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【所属年期】</span>{0}</li>", info.YEAR + "年" + info.ISSUE + "期");
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", pubdate);
                sb.AppendFormat("<li><span>【关 键 词】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                // sb.AppendFormat("<li><span>【实施时间】</span>{0}</li>", dateimplate);
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.STUDYRES, LogType.BROWSE, doi, title, "浏览期刊");
                }

            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                string res = "owner";
                JournalYear bll = new JournalYear(res);
                JournalYearInfo info = bll.GetItem(doi);
                if (info == null)
                {
                    BindDataNoResult();
                    return;
                }

                //为变量赋值
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                path = info.SYS_FLD_COVERPATH;
                filename = info.SYS_FLD_FILEPATH;
                filename = HttpUtility.UrlEncode(filename);
                title = info.CNAME;
                strXml = info.SYS_FLD_CATALOG;
                note = info.Note;
                string pubdate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【期刊类型】</span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【所属年期】</span>{0}</li>", info.YEAR + "年" + info.ISSUE + "期");
                sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", pubdate);
                sb.AppendFormat("<li><span>【关 键 词】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                // sb.AppendFormat("<li><span>【实施时间】</span>{0}</li>", dateimplate);
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(type, doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.ENGLISHRES, LogType.BROWSE, doi, title, "浏览期刊");
                }

            }


            //绑定图书Title
            title = title.Replace("\r\n", "");
            string notediv = Utility.Utility.DealNoteTitle(note);
            if (string.IsNullOrEmpty(notediv))
            {
                lt_Title.Text = title;
            }
            else
            {
                lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
            }

            //图片显示控件属性初始化
            this.ctrl_Zoom.ThumbImgSrc = "/View/ShowPic.aspx?ptype=0&vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(path);
            this.ctrl_Zoom.BigImgSrc = "/View/ShowPic.aspx?vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(path);
            this.ctrl_Zoom.SearchImgSrc = "../images/magnifier.png";
            this.ctrl_Zoom.ImgDescription = CNKI.BaseFunction.NormalFunction.ReplaceLabel(title);
            this.ctrl_Zoom.NoImgSrc = "";

            //绑定内容简介
            //  lt_Digest.Text = digest;

            //绑定目录
            BindCataLog(strXml);
            ///绑定文章列表
            BindArticle(doi, type);

            string realPath = Utility.FileManagementUtility.GetFilePath(vpath, filePath);
            BindAttachment(doi, type, title, realPath);

            //显示数据
            haveResult.Visible = true;
            noResult.Visible = false;
        }


        /// <summary>
        /// 绑定文章 列表
        /// </summary>
        /// <param name="doi"></param>
        private void BindArticle(string doi, string type)
        {
            int recordcount = 0;
            IList<string> tableList = new List<string>();

            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.NEWSPAPER)
            {
                NewsPaperArticle newsPaperControl = new NewsPaperArticle();
                IList<NewsPaperArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //报纸版面
                    List<NewsPaperArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_parentdoi == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (NewsPaperArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<NewsPaperArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_parentdoi == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                str.Append(item.Title);
                                str.Append("</h2></td></tr>");
                                foreach (NewsPaperArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Title);
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.JOURNAL)
            {
                JournalArticle newsPaperControl = new JournalArticle();
                IList<JournalArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //期刊文章类目
                    List<JournalArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_PARENTDOI == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (JournalArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<JournalArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_PARENTDOI == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                str.Append(item.Name);
                                str.Append("</h2></td></tr>");
                                foreach (JournalArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Name);
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.MAGAZINE)
            {
                MagazineArticle newsPaperControl = new MagazineArticle();
                IList<MagazineArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //杂志文章类目
                    List<MagazineArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_parentdoi == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (MagazineArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<MagazineArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_parentdoi == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                str.Append(item.Title);
                                str.Append("</h2></td></tr>");
                                foreach (MagazineArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Title);
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.YEARBOOK)
            {
                YearBookArticle newsPaperControl = new YearBookArticle();
                IList<YearBookArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //年鉴文章类目
                    List<YearBookArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_PARENTDOI == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (YearBookArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<YearBookArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_PARENTDOI == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                str.Append(item.Title);
                                str.Append("</h2></td></tr>");
                                foreach (YearBookArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Title);
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                string res = "english";
                JournalArticle newsPaperControl = new JournalArticle(res);
                IList<JournalArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //期刊文章类目
                    List<JournalArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_PARENTDOI == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (JournalArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<JournalArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_PARENTDOI == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                //str.Append(item.Name);
                                str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, item.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + item.Name + "</a>");
                                str.Append("</h2></td></tr>");
                                foreach (JournalArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Name + "</a>");
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                string res = "study";
                JournalArticle newsPaperControl = new JournalArticle(res);
                IList<JournalArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //期刊文章类目
                    List<JournalArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_PARENTDOI == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (JournalArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<JournalArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_PARENTDOI == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                //str.Append(item.Name);
                                str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, item.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + item.Name + "</a>");
                                str.Append("</h2></td></tr>");
                                foreach (JournalArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Name + "</a>");
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                string res = "owner";
                JournalArticle newsPaperControl = new JournalArticle(res);
                IList<JournalArticleInfo> mylist = newsPaperControl.GetList("parentdoi=\"" + doi + "\"", 1, 100, out recordcount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    //期刊文章类目
                    List<JournalArticleInfo> myChapterList = mylist.Where(x => x.SYS_FLD_PARENTDOI == "0").ToList();
                    if (myChapterList != null)
                    {
                        foreach (JournalArticleInfo item in myChapterList)
                        {
                            //文章列表
                            List<JournalArticleInfo> myArticleList = mylist.Where(x => x.SYS_FLD_PARENTDOI == item.SYS_FLD_DOI).ToList();
                            if (myArticleList != null)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("<table cellpadding=\"5\" cellspacing=\"5\" border=\"0\" width=\"100%\">");
                                str.Append("<tr><td><h2>");
                                //str.Append(item.Name);
                                str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, item.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + item.Name + "</a>");
                                str.Append("</h2></td></tr>");
                                foreach (JournalArticleInfo myarticle in myArticleList)
                                {
                                    str.Append("<tr><td>");
                                    str.Append("<a href=\"" + string.Format(NewsPaperArticleUrl, myarticle.SYS_FLD_DOI, type) + "\" target=\"_blank\">" + myarticle.Name + "</a>");
                                    str.Append("</td></tr>");
                                }
                                str.Append("</table>");
                                tableList.Add(str.ToString());
                            }
                        }
                    }
                }
            }

            if (tableList.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<div class=\"singleDetail\"><h2>文章列表</h2>");
                str.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">");

                for (int i = 0; i < tableList.Count; i = i + 2)
                {
                    str.Append("<tr><td valign=\"top\" class=\"articletd\">");
                    str.Append(tableList[i]);
                    str.Append("</td>");
                    str.Append("<td valign=\"top\" class=\"articletd\">");
                    if (i + 1 < tableList.Count)
                    {
                        str.Append(tableList[i + 1]);
                    }
                    str.Append("</td></tr>");
                }

                str.Append("</table>");
                str.Append("</div></div>");
                ltl_ArticleList.Text = str.ToString();
            }

        }


        /// <summary>
        /// </summary>
        /// <param name="strXml"></param>
        private void BindCataLog(string strXml)
        {
            if (string.IsNullOrEmpty(strXml))
            {
                return;
            }
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(strXml);
            }
            catch
            {
                return;
            }
            string xsltPath = "/xslt/Catalog.xslt";
            lt_catalog.Text = Utility.Utility.XmlToString(xd, xsltPath);
            if (!string.IsNullOrEmpty(lt_catalog.Text))
            {
                lt_catalog.Text = "  <div class=\"singleDetail\"><h2>目录</h2><div class=\"sm\">" + lt_catalog.Text + "</div></div>";
            }
        }

        /// <summary>
        /// 绑定价格信息 并根据各种条件生成不同的操作按钮
        /// </summary>
        /// <param name="resouceType"></param>
        /// <param name="doi"></param>
        /// <param name="title"></param>
        private void BindPrice(string resouceType, string doi, string title)
        {
            string charpterUrl = "ChapterRead.aspx?doi=" + doi + "&type=" + resouceType;

            StringBuilder sb = new StringBuilder();

            //生成购买按钮
            string uName = User.Identity.Name;
            string isOrg = Utility.Utility.GetRole();

            sb.Append("<div class='btn-book'>");

            //在线阅读
            sb.Append("<a href='/AdminknReader/default.aspx?doi=" + doi + "&type=" + resouceType + "' class='btn-online'></a>");
            //按章节阅读
            if (resouceType == DataBaseType.JOURNALYEAR.GetHashCode().ToString() || resouceType == DataBaseType.JOURNAL.GetHashCode().ToString() ||
                resouceType == DataBaseType.YEARBOOK.GetHashCode().ToString() || resouceType == DataBaseType.ENGLISHRES.GetHashCode().ToString() ||
                resouceType == DataBaseType.STUDYRES.GetHashCode().ToString())
                sb.Append("<a href='/view/ArticleDetail.aspx?yissuedoi=" + doi + "&type=" + resouceType + "' class='btn-articleview' ></a>");

            if (resouceType == DataBaseType.OWNERRES.GetHashCode().ToString())
            {
                //判断是否有文章
                JournalArticle articleBll = new JournalArticle("owner");
                int recordcount=0;
                IList<JournalArticleInfo> lists = articleBll.GetList("PARENTDOI='" + doi + "'", 1, 1, out recordcount, false);
                if (lists != null && lists.Count > 0)
                {
                    sb.Append("<a href='/view/ArticleDetail.aspx?yissuedoi=" + doi + "&type=" + resouceType + "' class='btn-articleview' ></a>");
                }
            }

            sb.Append("<a href='javascript:void(0)' class='btn-favorite' onclick='favoriteData(\"" + doi + "\"," + resouceType + ",\"" + title + "\")'></a>");

            sb.Append("</div>");

            lt_price.Text = sb.ToString();
        }


        /// <summary>
        /// 当未查询到任何记录时，在前台显示未查到记录
        /// </summary>
        private void BindDataNoResult()
        {
            noResult.Visible = true;
            haveResult.Visible = false;
        }

        /// <summary>
        /// 添加关键词的url
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private string GetKeyWordUrl(string keyWord)
        {
            string[] keywordStr = keyWord.Split(new string[] { ";", ",", "；", "，" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder str = new StringBuilder();
            foreach (string s in keywordStr)
            {
                str.AppendFormat(KeyWordUrlFormat, HttpUtility.UrlEncode(s), s);
                str.Append(" ");
            }
            return str.ToString();
        }
    }
}