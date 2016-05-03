using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using System.Configuration;

using DRMS.Model;
using DRMS.BLL;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class PowerJournallist : BasePage.BasePage
    {
        protected string mType { get; set; }
        protected string vpath { get; set; }
        protected string filename { get; set; }
        protected string ClassofElectricalfence = ConfigurationManager.AppSettings["ClassofElectricalfence"].ToString();
        protected string ClassofElectronics = ConfigurationManager.AppSettings["ClassofElectronics"].ToString();
        protected string ClassofStd = ConfigurationManager.AppSettings["ClassofStd"].ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int allCount = 0;
                string StrWhere = "bookid=\"q?\"";

                //获取列表信息
                Book bll = new Book();
                IList<BookInfo> list = bll.GetList(StrWhere, 1, 1000, out allCount, false);
                if (list != null && list.Count > 0)
                {
                    noResult.Visible = false;
                    haveResult.Visible = true;
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<div id='power_journallist'>");
                    foreach (BookInfo info in list)
                    {
                        if (info == null)
                        {
                            continue;
                        }
                        sb.Append(GetJournalDetail(info));
                    }
                    sb.Append("</div>");
                    lt_list.Text = sb.ToString();
                }
                else
                {
                    BindDataNoResult();
                }

            }
        }

        /// <summary>
        /// 获取期刊信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetJournalDetail(BookInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = NormalFunction.ResetRedFlag(info.Name);
            string vpath = info.SYS_FLD_VIRTUALPATHTAG;
            string cpath = info.SYS_FLD_COVERPATH;

            string imgUrl = "ShowPic.aspx?ptype=0&vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(cpath);
            //点击超链接转到的期刊年期页面
            string hrefUrl = "/View/PowerBookDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + DataBaseType.BOOKTDATA.GetHashCode();
            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href='{2}' target='_blank'><img src='../view/ShowPic.aspx?ptype=0&vpath={1}&path={0}'/></a>", cpath, vpath, hrefUrl);
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.AppendFormat("<h2><a href='{1}' title='{2}' target='_blank'>{0}</a></h2>", NormalFunction.ReplaceRed(name), hrefUrl, fullName);
            //sb.AppendFormat("<p>年期：<a href='{2}' target='_blank' title='{1}'>{0}</a></p>", name, fullSource, sourcePage);
            sb.Append("</dd>");

            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 当未查询到任何记录时，在前台显示未查到记录
        /// </summary>
        private void BindDataNoResult()
        {
            noResult.Visible = true;
            haveResult.Visible = false;
        }
    }
}