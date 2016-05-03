using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using DRMS.Model;
using DRMS.BLL;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class ConferencePaperDetail : System.Web.UI.Page
    {

        protected string Doi = CNKI.BaseFunction.NormalFunction.GetQueryString("doi", "");
        protected string mType { get; set; }
        protected string vpath { get; set; }
        protected string filename { get; set; }
        protected string userNameLogin = HttpContext.Current.User.Identity.Name;
        protected string userIP { get; set; }
        protected string KeyWordUrlFormat = "<a  href=\"/view/DBThemeNav.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";
        public string DataBaseName { get; set; }


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
                DataBaseName = "论文集&nbsp;>&nbsp;细览页";
                hidDoi.Value = Doi;
                BindData(Doi);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="type"></param>
        private void BindData(string doi)
        {
            string title = "";
            string digest = "";
            string strXml = "";
            string path = "";
            ConferencePaper bll = new ConferencePaper();
            ConferencePaperInfo info = bll.GetItem(doi);
            if (info == null )
            {
                BindDataNoResult();
                return;
            }

            //为变量赋值
            vpath = info.SYS_FLD_VIRTUALPATHTAG;
            path = info.SYS_FLD_COVERPATH;
            title = info.Name;
            digest = info.Digest.Length > 300 ? info.Digest.Substring(0, 300) + "..." : info.Digest;
            strXml = info.SYS_FLD_CATALOG;

            string dateissued = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");

            //绑定会议论文集的概要信息
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<li><span class='sAuthor'>【主编人员】</span>{0}</li>", info.Author);
            sb.AppendFormat("<li><span>【主办单位】</span>{0}</li>", info.ConferenceOrganiser);
            sb.AppendFormat("<li><span>【关 键 词】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
            sb.AppendFormat("<li><span>【会议名称】 </span>{0}</li>", info.ConferenceName);
            sb.AppendFormat("<li><span>【会议时间】</span>{0}</li>", dateissued);
            lt_summary.Text = sb.ToString();

            //绑定价格信息
            BindPrice("5", doi, title);

            //记录日志
            if (!IsPostBack)
            {
                Log logBll = new Log();
                logBll.Add(DataBaseType.CONFERENCEPAPER, LogType.BROWSE, doi, title, "浏览会议论文");
            }
            //绑定Title
            title = title.Replace("\r\n", "");
            string notediv = Utility.Utility.DealNoteTitle(info.Note);
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
            lt_Digest.Text = digest;

            //绑定目录
            BindCataLog();

            //显示数据
            haveResult.Visible = true;
            noResult.Visible = false;
        }

        /// <summary>
        /// 绑定目录
        /// </summary>
        private void BindCataLog()
        {
            //string detailPage = "/view/ConferenceArticleDetail.aspx?doi=";
            string detailPage = "/view/ArticleDetail.aspx?type=" + DataBaseType.CONFERENCEPAPER.GetHashCode().ToString() + "&doi=";
            StringBuilder str = new StringBuilder();
            int count = 0;
            string StrWhere = string.Format(" PARENTDOI='{0}' AND ISONLINE=1", Doi);
            ConferenceArticle cfa = new ConferenceArticle();
            IList<ConferenceArticleInfo> list = cfa.GetList(StrWhere, 1, 100, out count, true);
            if (count > 100)
            {
                int newcount = count;
                list = cfa.GetList(StrWhere, 1, newcount, out count, true);
            }
            if (list != null && list.Count != 0)
            {
                str.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">");
                for (int i = 0; i < list.Count; i = i + 2)
                {                    
                    string detailPagenew = detailPage + list[i].SYS_FLD_DOI;
                    string titlelist = list[i].Name.Length > 30 ? list[i].Name.Substring(0, 30) + "..." : list[i].Name;

                    str.Append("<tr><td valign=\"top\" class=\"articletd\">");
                    str.AppendFormat("<a href=\"{0}\">{1}&nbsp;&nbsp{2}&nbsp{3}</a>", detailPagenew, titlelist, list[i].FirstAuthor, list[i].Author); 
                    str.Append("</td>");
                    str.Append("<td valign=\"top\" class=\"articletd\">");

                    if (i + 1 < list.Count)
                    {
                        detailPagenew = detailPage + list[i+1].SYS_FLD_DOI;
                        titlelist = list[i+1].Name.Length > 30 ? list[i+1].Name.Substring(0, 30) + "..." : list[i+1].Name;
                        str.AppendFormat("<a href=\"{0}\">{1}&nbsp;&nbsp{2}&nbsp{3}</a>", detailPagenew, titlelist, list[i + 1].FirstAuthor, list[i + 1].Author); 
                    }
                    str.Append("</td></tr>");
                }
                str.Append("</table>");

            }
            lt_catalog.Text = str.ToString();
        }
        /// <summary>
        /// 当未查询到任何记录时，在前台显示未查到记录
        /// </summary>
        private void BindDataNoResult()
        {
            noResult.Visible = true;
            haveResult.Visible = false;
        }

        private void BindPrice(string resouceType, string doi, string title)
        {
            //string charpterUrl = "CharpterReade.aspx?doi=" + doi + "&type=9";

            StringBuilder sb = new StringBuilder();
            //购买按钮
            sb.Append("<div class='btn-book'>");
            string uName = User.Identity.Name;
            //判断用户是否购买了该书的阅读和下载权限
            sb.Append("<a href='/AdminknReader/default.aspx?doi=" + doi + "&type=" + resouceType + "' class='btn-online'></a>");
            // sb.Append("<a href='" + charpterUrl + "' class='btn-chapterbuy'></a>");
            sb.Append("</div>");
            lt_price.Text = sb.ToString();
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
                str.Append(string.Format(KeyWordUrlFormat, HttpUtility.UrlEncode(s), s) + " ");
            }
            return str.ToString();
        }
    }
}