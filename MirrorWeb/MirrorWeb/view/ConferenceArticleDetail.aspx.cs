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
    public partial class ConferenceArticleDetail : System.Web.UI.Page
    {
        protected string CurrentArticleSource_URL = "<a  href=\"/view/ConferencePaperDetail.aspx?doi={0}&type=19\" target=\"_top\">{1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Articledoi = Request["doi"];//文章doi
                BindData(Articledoi);
            }
        }

        /// <summary>
        /// 绑定当前浏览的文章内容
        /// </summary>
        private void BindData(string articleDoi)
        {
            string title = "";
            string content = "";
            string articleContent = "";

            ConferenceArticle bll = new ConferenceArticle();
            ConferenceArticleInfo info = bll.GetItem(articleDoi);
            if (info != null)
            {
                title = info.Name;
                string pName = info.ParentName;
                string source = pName;
                if (!string.IsNullOrEmpty(pName))
                {
                    source = pName == "" ? "" : "<span>" + string.Format(CurrentArticleSource_URL, info.ParentDoi, pName) + "</span>";
                }
                lt_title.Text = source;//绑定文章来源信息

                content = GetContent(info.SYS_FLD_XMLPATH, info.FullText);
                if (!string.IsNullOrEmpty(content))
                {
                    articleContent = "<h4>" + title + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                }
                else
                {
                    if (string.IsNullOrEmpty(title))
                    {
                        articleContent = "<p>该章节没有预览信息!</p>";
                    }
                    else
                    {
                        articleContent = "<h4>" + title + "</h4><p>该章节没有预览信息!</p>";
                    }
                }
            }
            else
            {
                articleContent = "<p>该章节没有预览信息!</p>";
            }
            lt_content.Text = articleContent;
        }

        /// <summary>
        /// 获取xml的正文
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        private string GetContent(string xmlContent, string fullContent)
        {
            string content = string.Empty;
            XmlDocument doc = new XmlDocument();
            //  content = Utility.XmlToHtml.ClearCDATA(content);
            try
            {
                doc.LoadXml(xmlContent);
                content = Utility.XmlToHtml.DealXml(doc, 0, false);
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