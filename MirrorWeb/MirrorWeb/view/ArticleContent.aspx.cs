using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text.RegularExpressions;

using Tool = CNKI.BaseFunction;
using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.view
{
    public partial class ArticleContent : System.Web.UI.Page
    {
        protected string CurrentArticle_Doi { get; set; }
        protected string CurrentArticle_URL = "<a  href=\"/view/ArticleDetail.aspx?doi={0}&type={1}\" target=\"_top\">{2}</a>";
        protected string CurrentArticleSource_URL = "<a  href=\"/view/ConferencePaperDetail.aspx?doi={0}\" target=\"_top\">{1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Articledoi = Request["doi"];//文章doi
                string pID = Request["pID"];
                string pName = Request["parentName"];
                string type = Request["dbtype"];
                BindData(Articledoi, pID, pName, type);
            }
        }

        /// <summary>
        /// 绑定当前浏览的文章内容
        /// </summary>
        private void BindData(string articleDoi, string pID, string parentName, string type)
        {
            CurrentArticle_Doi = articleDoi;
            string title = "";
            string content = "";
            string articleContent = "";

            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.NEWSPAPER || mydbtype == DataBaseType.NEWSPAPERYEAR)
            {
                NewsPaperArticle bll = new NewsPaperArticle();
                NewsPaperArticleInfo info = bll.GetItem(articleDoi);
                if (info != null)
                {
                    title = info.Title;
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.JOURNAL || mydbtype == DataBaseType.JOURNALYEAR)
            {
                JournalArticle bll = new JournalArticle();
                JournalArticleInfo info = bll.GetItem(articleDoi);
                if (info != null)
                {
                    title = info.Name;
                    if (!string.IsNullOrWhiteSpace(info.Note))
                    {
                        title = title + Utility.XmlToHtml.DealNoteTitle(info.Note);
                    }
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.MAGAZINE || mydbtype == DataBaseType.MAGAZINEYEAR)
            {
                MagazineArticle bll = new MagazineArticle();
                MagazineArticleInfo info = bll.GetItem(articleDoi);
                if (info != null)
                {
                    title = info.Title;
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.YEARBOOK)
            {
                YearBookArticle bll = new YearBookArticle();
                YearBookArticleInfo info = bll.GetItem(articleDoi);
                if (info != null)
                {
                    title = info.Title;
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.CONFERENCEPAPER || mydbtype == DataBaseType.CONFERENCEARTICLE)
            {
                ConferenceArticle bll = new ConferenceArticle();
                ConferenceArticleInfo info = bll.GetItem(articleDoi);
                if (info != null)
                {
                    title = info.Name;
                    string pName = info.ParentName;
                    string source = pName;
                    if (!string.IsNullOrEmpty(pName))
                    {
                        source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticleSource_URL, info.ParentDoi, pName) + "</span>";
                    }
                    //lt_title.Text = source;//绑定文章来源信息

                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    if (!string.IsNullOrEmpty(content))
                    {
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                JournalArticle bll = new JournalArticle("english");
                JournalArticleInfo info = bll.GetItem(articleDoi);
                string role = Utility.Utility.GetRole();
                if (info != null)
                {
                    title = info.Name;
                    if (!string.IsNullOrWhiteSpace(info.Note))
                    {
                        title = title + Utility.XmlToHtml.DealNoteTitle(info.Note);
                    }
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                JournalArticle bll = new JournalArticle("study");
                JournalArticleInfo info = bll.GetItem(articleDoi);
                string role = Utility.Utility.GetRole();
                if (info != null)
                {
                    title = info.Name;
                    if (!string.IsNullOrWhiteSpace(info.Note))
                    {
                        title = title + Utility.XmlToHtml.DealNoteTitle(info.Note);
                    }
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                JournalArticle bll = new JournalArticle("owner");
                JournalArticleInfo info = bll.GetItem(articleDoi);
                string role = Utility.Utility.GetRole();
                if (info != null)
                {
                    title = info.Name;
                    if (!string.IsNullOrWhiteSpace(info.Note))
                    {
                        title = title + Utility.XmlToHtml.DealNoteTitle(info.Note);
                    }
                    content = GetContent(info.SYS_FLD_PARAXML, info.FullText);
                    string pName = parentName;
                    if (!string.IsNullOrEmpty(content))
                    {
                        //content = "<p>" + "&nbsp;&nbsp;" + content + "</p>";
                        string source = pName;
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                        }
                        articleContent = "<h4>" + title + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(title))
                        {
                            articleContent = "<p>该章节没有预览信息!</p>";
                        }
                        else
                        {
                            string source = pName;
                            if (!string.IsNullOrEmpty(pName))
                            {
                                source = pName == "" ? "" : "<span class=\"contentsource\">" + "<font color='gray'>来源</font>：" + string.Format(CurrentArticle_URL, info.SYS_FLD_DOI, type, pName) + "</span>";
                            }
                            articleContent = "<h4>" + title + source + "</h4><p>该章节没有预览信息!</p>";
                        }
                    }
                }
                else
                {
                    articleContent = "<p>该章节没有预览信息!</p>";
                }
            }

            lt_content.Text = articleContent;
        }

        /// <summary>
        /// 获取xml的正文
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        private string GetContent(string xmlContent,string fullContent)
        {
            string content = string.Empty;
            XmlDocument doc = new XmlDocument();
            //  content = Utility.XmlToHtml.ClearCDATA(content);
            try
            {
                doc.LoadXml(xmlContent);
                content = Utility.XmlToHtml.DealXml(doc, 0,false);
            }
            catch
            {
                content = fullContent;
                if (!string.IsNullOrEmpty(content))
                {
                    content = "<p>" + content + "</p>";
                }
            }
            return content;
        }

    }
}