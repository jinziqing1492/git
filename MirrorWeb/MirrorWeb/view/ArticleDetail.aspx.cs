using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.Model;
using DRMS.BLL;

namespace DRMS.MirrorWeb.view
{
    public partial class ArticleDetail : System.Web.UI.Page
    {
        /// <summary>
        /// 当前浏览文章的数据资源类型
        /// </summary>
        public string dbtybe { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string curr_articledoi = Request["doi"];
                string mType = Request["type"];
                hdnQueryCon.Value = mType;
                this.dbtybe = mType;
                string yearissue_doi = getYearIssueDoi(curr_articledoi, mType);
                if (string.IsNullOrEmpty(yearissue_doi))
                    yearissue_doi = Request["yissuedoi"];
                BindData(yearissue_doi, curr_articledoi, mType);
            }

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="yearIssueDoi">刊物年期doi</param>
        /// <param name="currentArticleDoi">文章doi</param>
        /// <param name="type">数据类型</param>
        private void BindData(string yearIssueDoi, string currentArticleDoi, string type)
        {
            ctrl_tree.YearIssueDoi = yearIssueDoi;//设置树控件的属性
            ctrl_tree.SelectID = currentArticleDoi;
            ctrl_tree.dbtybe = type;

            //显示该资源的来源信息 
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.NEWSPAPER || mydbtype == DataBaseType.NEWSPAPERYEAR)
            {
                NewsPaperYear bll = new NewsPaperYear();
                NewsPaperYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = info.CNAME + info.YEAR + "年" + info.ISSUE + "期";//绑定资源来源信息
                }
            }
            else if (mydbtype == DataBaseType.JOURNAL || mydbtype == DataBaseType.JOURNALYEAR)
            {
                JournalYear bll = new JournalYear();
                JournalYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = "<a href='/View/JournalDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + (int)DataBaseType.JOURNAL + "'>" + info.CNAME + info.YEAR + "年" + info.ISSUE + "期</a>";//绑定资源来源信息
                }
            }
            else if (mydbtype == DataBaseType.MAGAZINE || mydbtype == DataBaseType.MAGAZINEYEAR)
            {
                MagazineYear bll = new MagazineYear();
                MagazineYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = info.CNAME + info.YEAR + "年" + info.ISSUE + "期";//绑定资源来源信息
                }
            }
            else if (mydbtype == DataBaseType.YEARBOOK)
            {
                YearBookYear bll = new YearBookYear();
                YearBookYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    //lt_title.Text = info.Name + articleInfo.Year + "年";//绑定资源来源信息
                    lt_title.Text = info.Name ;//绑定资源来源信息
                }
            }
            else if (mydbtype == DataBaseType.CONFERENCEPAPER || mydbtype == DataBaseType.CONFERENCEARTICLE)
            {
                ConferencePaper bll = new ConferencePaper();
                ConferencePaperInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = info.Name;//绑定资源来源信息
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                JournalYear bll = new JournalYear("english");
                JournalYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = info.CNAME + info.YEAR + "年" + info.ISSUE + "期";
                }
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                JournalYear bll = new JournalYear("study");
                JournalYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = info.CNAME + info.YEAR + "年" + info.ISSUE + "期";
                }
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                JournalYear bll = new JournalYear("owner");
                JournalYearInfo info = bll.GetItem(yearIssueDoi);
                if (info != null)
                {
                    lt_title.Text = info.CNAME + info.YEAR + "年" + info.ISSUE + "期";
                }
            }
        }

        /// <summary>
        /// 获取当前阅读文章所属刊物年期doi
        /// </summary>        
        /// <param name="currentdoi">文章doi</param>
        private string getYearIssueDoi(string articledoi, string type)
        {
            string yearissuedoi = "";
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.NEWSPAPER || mydbtype == DataBaseType.NEWSPAPERYEAR)
            {
                NewsPaperArticle bll = new NewsPaperArticle();
                NewsPaperArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi; 
            }
            else if (mydbtype == DataBaseType.JOURNAL || mydbtype == DataBaseType.JOURNALYEAR)
            {
                JournalArticle bll = new JournalArticle();
                JournalArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            else if (mydbtype == DataBaseType.MAGAZINE || mydbtype == DataBaseType.MAGAZINEYEAR)
            {
                MagazineArticle bll = new MagazineArticle();
                MagazineArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            else if (mydbtype == DataBaseType.YEARBOOK)
            {
                YearBookArticle bll = new YearBookArticle();
                YearBookArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            else if (mydbtype == DataBaseType.CONFERENCEPAPER || mydbtype == DataBaseType.CONFERENCEARTICLE)
            {
                ConferenceArticle bll = new ConferenceArticle();
                ConferenceArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                JournalArticle bll = new JournalArticle("english");
                JournalArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                JournalArticle bll = new JournalArticle("study");
                JournalArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                JournalArticle bll = new JournalArticle("owner");
                JournalArticleInfo articleInfo = bll.GetItem(articledoi);
                if (articleInfo != null)
                    yearissuedoi = articleInfo.ParentDoi;
            }
            return yearissuedoi;
        }

    }
}