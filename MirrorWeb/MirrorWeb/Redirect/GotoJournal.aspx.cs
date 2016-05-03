using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.Redirect
{
    public partial class GotoJournal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            string doi = Request.QueryString["doi"];
            if (type == "article")
            {
                GotoArticle(doi);
            }
            else
            {
                GotoJournalInfo(doi);
            }
        }

        /// <summary>
        /// 跳转到期刊文章
        /// </summary>
        /// <param name="doi"></param>
        private void GotoArticle(string doi)
        {
            //判断文章为期刊还是自有资源
            DataBaseType restype = DataBaseType.OWNERRES;
            JournalArticle bll = new JournalArticle("english");
            JournalArticleInfo info = bll.GetItem(doi);
            if (info != null)//说明该文章为期刊资源
            {
                restype = DataBaseType.ENGLISHRES;
            }
            else
            {
                bll = new JournalArticle("owner");
                info = bll.GetItem(doi);
                if (info != null)
                {
                    restype = DataBaseType.OWNERRES;
                }
                else
                {
                    restype = DataBaseType.JOURNAL;
                }
            }

            //跳转到自有资源的文章列表
            string url = "/view/ArticleDetail.aspx?type=" + restype.GetHashCode() + "&doi=" + doi;
            Response.Redirect(url);
        }

        /// <summary>
        /// 跳转到期刊
        /// </summary>
        /// <param name="doi"></param>
        private void GotoJournalInfo(string doi)
        {
            //判断文章为期刊还是自有资源
            DataBaseType restype = DataBaseType.OWNERRES;
            JournalYear bll = new JournalYear("owner");
            JournalYearInfo info = bll.GetItem(doi);
            if (info == null)//说明该文章为期刊资源
            {
                restype = DataBaseType.JOURNALYEAR;
            }
            //跳转到自有资源的文章列表
            string url = "/view/JournalDetail.aspx?type=" + restype.GetHashCode() + "&doi=" + doi;
            Response.Redirect(url);
        }
    }
}