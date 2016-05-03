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
    public partial class YearIssueDetail : System.Web.UI.Page
    {
        protected string mType { get; set; }
        public string filename { get; set; }
        public string DataBaseName { get; set; }
        public string NewsDetail = "/View/JournalDetail.aspx?doi={0}&type={1}";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string baseid = Request["baseid"];
                string type = Tool.NormalFunction.GetQueryString("type", "1");
                mType = type;                
                DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
                string sql = mydbtype.GetHashCode().ToString();
                DataBaseName = EnumDescription.GetFieldText(mydbtype) + "&nbsp;>&nbsp;按期阅读";
                //hidDoi.Value = baseid; 
                BindTitleandSummaryList(baseid, type);
                BindYearList(baseid, type);

            }

        }

        /// <summary>
        /// 绑定刊物标题和概要信息
        /// </summary>
        /// <param name="baseid">拼音刊名</param>
        /// <param name="type">刊物数据资源类型</param>
        private void BindTitleandSummaryList(string baseId, string type)
        {
            StringBuilder sb = new StringBuilder();
            int recordCount = 0;
            string title = "";
            string note = "";
            string notediv = "";
            string cpath = "";
            string vpath = "";
            
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            
            if (mydbtype == DataBaseType.NEWSPAPER)
            {
                NewsPaper npbll = new NewsPaper();
                NewsPaperInfo info = npbll.GetItem(baseId);                
                title = info.CNAME;
                title = title.Replace("\r\n", "");
                note = info.Note;
                notediv = Utility.Utility.DealNoteTitle(note);
                cpath = info.SYS_FLD_COVERPATH;
                vpath = info.SYS_FLD_VIRTUALPATHTAG;                                
                NewsPaperYear bll = new NewsPaperYear();
                IList<NewsPaperYearInfo> mylist = bll.GetList("baseid='" + baseId + "' and SYS_FLD_CHECK_STATE=-1 order by yearissue desc", 1, 1, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                    {
                        cpath = mylist[0].SYS_FLD_COVERPATH;
                        vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                    }
                }
                
                //报纸标题
                if (string.IsNullOrEmpty(notediv))
                {
                    lt_Title.Text = title;
                }
                else
                {
                    lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
                }
                //报纸简介
                lt_Digest.Text = info.Description;
                //报纸概要信息                
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【出版地址】 </span>{0}</li>", info.PubPlace);
                sb.AppendFormat("<li><span>【报纸类型】 </span>{0}</li>", Utility.Utility.GetTypeNamefromXml("newstype", info.Type));
                sb.AppendFormat("<li><span>【报纸  CN】 </span>{0}</li>", info.CN);
                sb.AppendFormat("<li><span>【报纸语种】 </span>{0}</li>", info.Language);
                sb.AppendFormat("<li><span>【建刊时间】 </span>{0}</li>", Utility.Utility.FormatDateTime(info.FoundDate));

            }
            else if (mydbtype == DataBaseType.JOURNAL)
            {
                Journal npbll = new Journal();
                JournalInfo info = npbll.GetItem(baseId);
                title = info.CNAME;
                title = title.Replace("\r\n", "");
                note = info.Note;
                notediv = Utility.Utility.DealNoteTitle(note);
                cpath = info.SYS_FLD_COVERPATH;
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                JournalYear bll = new JournalYear();
                IList<JournalYearInfo> mylist = bll.GetList("baseid='" + baseId + "' and SYS_FLD_CHECK_STATE=-1 order by yearissue desc", 1, 1, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                    {
                        cpath = mylist[0].SYS_FLD_COVERPATH;
                        vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                    }
                }

                //期刊标题
                if (string.IsNullOrEmpty(notediv))
                {
                    lt_Title.Text = title;
                }
                else
                {
                    lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
                }
                //期刊简介
                lt_Digest.Text = info.Description;
                //期刊概要信息                
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【出版地址】 </span>{0}</li>", info.PubPlace);
                sb.AppendFormat("<li><span>【期刊类型】 </span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【期刊  CN】 </span>{0}</li>", info.CN);
                sb.AppendFormat("<li><span>【期刊语种】 </span>{0}</li>", info.Language);
                sb.AppendFormat("<li><span>【建刊时间】 </span>{0}</li>", Utility.Utility.FormatDateTime(info.FoundDate));
            }
            else if (mydbtype == DataBaseType.MAGAZINE)
            {
                Magazine npbll = new Magazine();
                MagazineInfo info = npbll.GetItem(baseId);
                title = info.CNAME;
                title = title.Replace("\r\n", "");
                note = info.Note;
                notediv = Utility.Utility.DealNoteTitle(note);
                cpath = info.SYS_FLD_COVERPATH;
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                MagazineYear bll = new MagazineYear();
                IList<MagazineYearInfo> mylist = bll.GetList("baseid='" + baseId + "' and SYS_FLD_CHECK_STATE=-1 order by yearissue desc", 1, 1, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                    {
                        cpath = mylist[0].SYS_FLD_COVERPATH;
                        vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                    }
                }

                //杂志标题
                if (string.IsNullOrEmpty(notediv))
                {
                    lt_Title.Text = title;
                }
                else
                {
                    lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
                }
                //杂志简介
                lt_Digest.Text = info.Description;
                //杂志概要信息                
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【出版地址】 </span>{0}</li>", info.PubPlace);
                sb.AppendFormat("<li><span>【杂志类型】 </span>{0}</li>", Utility.Utility.GetTypeNamefromXml("magtype", info.Type));
                sb.AppendFormat("<li><span>【杂志  CN】 </span>{0}</li>", info.CN);
                sb.AppendFormat("<li><span>【杂志语种】 </span>{0}</li>", info.Language);
                sb.AppendFormat("<li><span>【建刊时间】 </span>{0}</li>", Utility.Utility.FormatDateTime(info.FoundDate));
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                string res = "english";
                Journal npbll = new Journal(res);
                JournalInfo info = npbll.GetItem(baseId);
                title = info.CNAME;
                title = title.Replace("\r\n", "");
                note = info.Note;
                notediv = Utility.Utility.DealNoteTitle(note);
                cpath = info.SYS_FLD_COVERPATH;
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList("baseid='" + baseId + "' and SYS_FLD_CHECK_STATE=-1 order by yearissue desc", 1, 1, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                    {
                        cpath = mylist[0].SYS_FLD_COVERPATH;
                        vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                    }
                }

                //期刊标题
                if (string.IsNullOrEmpty(notediv))
                {
                    lt_Title.Text = title;
                }
                else
                {
                    lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
                }
                //期刊简介
                lt_Digest.Text = info.Description;
                //期刊概要信息                
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【出版地址】 </span>{0}</li>", info.PubPlace);
                sb.AppendFormat("<li><span>【期刊类型】 </span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【CN】 </span>{0}</li>", info.CN);
                sb.AppendFormat("<li><span>【期刊语种】 </span>{0}</li>", info.Language);
                sb.AppendFormat("<li><span>【建刊时间】 </span>{0}</li>", Utility.Utility.FormatDateTime(info.FoundDate));
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                string res = "study";
                Journal npbll = new Journal(res);
                JournalInfo info = npbll.GetItem(baseId);
                title = info.CNAME;
                title = title.Replace("\r\n", "");
                note = info.Note;
                notediv = Utility.Utility.DealNoteTitle(note);
                cpath = info.SYS_FLD_COVERPATH;
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList("baseid='" + baseId + "' and SYS_FLD_CHECK_STATE=-1 order by yearissue desc", 1, 1, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                    {
                        cpath = mylist[0].SYS_FLD_COVERPATH;
                        vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                    }
                }

                //期刊标题
                if (string.IsNullOrEmpty(notediv))
                {
                    lt_Title.Text = title;
                }
                else
                {
                    lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
                }
                //期刊简介
                lt_Digest.Text = info.Description;
                //期刊概要信息                
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【出版地址】 </span>{0}</li>", info.PubPlace);
                sb.AppendFormat("<li><span>【期刊类型】 </span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【CN】 </span>{0}</li>", info.CN);
                sb.AppendFormat("<li><span>【期刊语种】 </span>{0}</li>", info.Language);
                sb.AppendFormat("<li><span>【建刊时间】 </span>{0}</li>", Utility.Utility.FormatDateTime(info.FoundDate));
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                string res = "owner";
                Journal npbll = new Journal(res);
                JournalInfo info = npbll.GetItem(baseId);
                title = info.CNAME;
                title = title.Replace("\r\n", "");
                note = info.Note;
                notediv = Utility.Utility.DealNoteTitle(note);
                cpath = info.SYS_FLD_COVERPATH;
                vpath = info.SYS_FLD_VIRTUALPATHTAG;
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList("baseid='" + baseId + "' and SYS_FLD_CHECK_STATE=-1 order by yearissue desc", 1, 1, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                    {
                        cpath = mylist[0].SYS_FLD_COVERPATH;
                        vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                    }
                }

                //期刊标题
                if (string.IsNullOrEmpty(notediv))
                {
                    lt_Title.Text = title;
                }
                else
                {
                    lt_Title.Text = title + "<img src='../images/notepic.png' class='notetitle'/>" + notediv;
                }
                //期刊简介
                lt_Digest.Text = info.Description;
                //期刊概要信息                
                sb.AppendFormat("<li><span class='sAuthor'>【主办单位】</span>{0}</li>", info.Hostdep);
                sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.Pubdep);
                sb.AppendFormat("<li><span>【出版地址】 </span>{0}</li>", info.PubPlace);
                sb.AppendFormat("<li><span>【期刊类型】 </span>{0}</li>", Utility.Utility.GetTypeNamefromXml("journaltype", info.Type));
                sb.AppendFormat("<li><span>【CN】 </span>{0}</li>", info.CN);
                sb.AppendFormat("<li><span>【期刊语种】 </span>{0}</li>", info.Language);
                sb.AppendFormat("<li><span>【建刊时间】 </span>{0}</li>", Utility.Utility.FormatDateTime(info.FoundDate));
            }
            else
            { }
            lt_summary.Text = sb.ToString();
            //图片显示控件属性初始化
            this.myctrl_Zoom.ThumbImgSrc = "/View/ShowPic.aspx?ptype=0&vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(cpath);
            this.myctrl_Zoom.BigImgSrc = "/View/ShowPic.aspx?vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(cpath);
            this.myctrl_Zoom.SearchImgSrc = "../images/magnifier.png";
            this.myctrl_Zoom.ImgDescription = CNKI.BaseFunction.NormalFunction.ReplaceLabel(title);
            this.myctrl_Zoom.NoImgSrc = "";
        }


        /// <summary>
        /// 绑定刊物年期数据
        /// </summary>
        /// <param name="baseid">拼音刊名</param>
        /// <param name="type">刊物数据资源类型</param>
        private void BindYearList(string baseId, string type)
        {            
            int recordCount = 0;            
            StringBuilder str = new StringBuilder();
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.NEWSPAPER)
            {
                //根据baseid获取报纸年和期数
                NewsPaperYear bll = new NewsPaperYear();
                IList<NewsPaperYearInfo> mylist = bll.GetList("baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1  group by year  order by year desc", 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    filename = mylist[0].CNAME;
                    lt_catalog.Text += "<div  >";
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        BindYearIssueList(baseId, mylist[i].YEAR.ToString(), type);
                    }
                    lt_catalog.Text += "</div>";
                }
                else
                    BindDataNoResult();               

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.NEWSPAPER, LogType.BROWSE, baseId, "报纸", "按年期浏览报纸");
                }
            }
            else if (mydbtype == DataBaseType.JOURNAL)
            {
                //根据baseid获取期刊年和期数
                JournalYear bll = new JournalYear();
                IList<JournalYearInfo> mylist = bll.GetList("baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1  group by year  order by year desc", 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    filename = mylist[0].CNAME;
                    lt_catalog.Text += "<div  >";
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        BindYearIssueList(baseId, mylist[i].YEAR.ToString(), type);
                    }
                    lt_catalog.Text += "</div>";
                }
                else
                    BindDataNoResult();

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.JOURNAL, LogType.BROWSE, baseId, "期刊", "按年期浏览期刊");
                }
            }
            else if (mydbtype == DataBaseType.MAGAZINE)
            {
                //根据baseid获取杂志年和期数
                MagazineYear bll = new MagazineYear();
                IList<MagazineYearInfo> mylist = bll.GetList("baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1  group by year  order by year desc", 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    filename = mylist[0].CNAME;
                    lt_catalog.Text += "<div  >";
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        BindYearIssueList(baseId, mylist[i].YEAR.ToString(), type);
                    }
                    lt_catalog.Text += "</div>";
                }
                else
                    BindDataNoResult();

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.MAGAZINE, LogType.BROWSE, baseId, "杂志", "按年期浏览杂志");
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                //根据baseid获取期刊年和期数
                string res = "english";
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList("baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1 group by year  order by year desc", 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    filename = mylist[0].CNAME;
                    lt_catalog.Text += "<div  >";
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        BindYearIssueList(baseId, mylist[i].YEAR.ToString(), type);
                    }
                    lt_catalog.Text += "</div>";
                }
                else
                    BindDataNoResult();

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.ENGLISHRES, LogType.BROWSE, baseId, "期刊", "按年期浏览期刊");
                }
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                //根据baseid获取期刊年和期数
                string res = "study";
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList("baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1 group by year  order by year desc", 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    filename = mylist[0].CNAME;
                    lt_catalog.Text += "<div  >";
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        BindYearIssueList(baseId, mylist[i].YEAR.ToString(), type);
                    }
                    lt_catalog.Text += "</div>";
                }
                else
                    BindDataNoResult();

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.STUDYRES, LogType.BROWSE, baseId, "期刊", "按年期浏览期刊");
                }
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                //根据baseid获取期刊年和期数
                string res = "owner";
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList("baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1 group by year  order by year desc", 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    filename = mylist[0].CNAME;
                    lt_catalog.Text += "<div  >";
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        BindYearIssueList(baseId, mylist[i].YEAR.ToString(), type);
                    }
                    lt_catalog.Text += "</div>";
                }
                else
                    BindDataNoResult();

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.OWNERRES, LogType.BROWSE, baseId, "自有资源", "按年期浏览期刊");
                }
            }
        }
        /// <summary>
        /// 绑定刊物年和其下的期数列表
        /// </summary>
        protected void BindYearIssueList(string baseId, string year,string type)
        {
            int recordCount = 0;                              
            StringBuilder strYear = new StringBuilder();
            string sql = "baseid=\"" + baseId + "\" and year=\"" + year + "\" and SYS_FLD_CHECK_STATE=-1 order by issue desc";
            if (string.IsNullOrEmpty(year))
            {
                sql = "baseid=\"" + baseId + "\" and SYS_FLD_CHECK_STATE=-1 order by yearissue desc";
            }
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);

            if (mydbtype == DataBaseType.NEWSPAPER)
            {
                NewsPaperYear bll = new NewsPaperYear();
                IList<NewsPaperYearInfo> mylist = bll.GetList(sql, 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    strYear.Append("<div >");
                    strYear.Append("<p>" + mylist[0].YEAR + "年</p><p>");
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        strYear.Append("<a href=\"" + string.Format(NewsDetail, mylist[i].SYS_FLD_DOI, DataBaseType.NEWSPAPER.GetHashCode()) + "\" target=\"_blank\" title=\"" + mylist[i].CNAME + mylist[i].YEAR + "年第" + mylist[i].ISSUE + "期\">[" + mylist[i].ISSUE + "期]</a>");
                        if (i != mylist.Count-1)
                            strYear.Append("    |    ");
                    }

                    strYear.Append("</p></div>");
                }
            }
            else if (mydbtype == DataBaseType.JOURNAL)
            {
                JournalYear bll = new JournalYear();
                IList<JournalYearInfo> mylist = bll.GetList(sql, 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    mylist = mylist.OrderByDescending(x => CNKI.BaseFunction.StructTrans.TransNum(x.ISSUE)).ToList();
                    strYear.Append("<div >");
                    strYear.Append("<p>" + mylist[0].YEAR + "年</p><p>");
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        strYear.Append("<a href=\"" + string.Format(NewsDetail, mylist[i].SYS_FLD_DOI, DataBaseType.JOURNAL.GetHashCode()) + "\" target=\"_blank\" title=\"" + mylist[i].CNAME + mylist[i].YEAR + "年第" + mylist[i].ISSUE + "期\">[" + mylist[i].ISSUE + "期]</a>");
                        if (i != mylist.Count - 1)
                            strYear.Append("    |    ");
                    }

                    strYear.Append("</p></div>");
                }
            }
            else if (mydbtype == DataBaseType.MAGAZINE)
            {
                MagazineYear bll = new MagazineYear();
                IList<MagazineYearInfo> mylist = bll.GetList(sql, 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    strYear.Append("<div >");
                    strYear.Append("<p>" + mylist[0].YEAR + "年</p><p>");
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        strYear.Append("<a href=\"" + string.Format(NewsDetail, mylist[i].SYS_FLD_DOI, DataBaseType.MAGAZINE.GetHashCode()) + "\" target=\"_blank\" title=\"" + mylist[i].CNAME + mylist[i].YEAR + "年第" + mylist[i].ISSUE + "期\">[" + mylist[i].ISSUE + "期]</a>");
                        if (i != mylist.Count - 1)
                            strYear.Append("    |    ");
                    }

                    strYear.Append("</p></div>");
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                string res = "english";
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList(sql, 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    mylist = mylist.OrderByDescending(x => CNKI.BaseFunction.StructTrans.TransNum(x.ISSUE)).ToList();
                    strYear.Append("<div >");
                    strYear.Append("<p>" + mylist[0].YEAR + "年</p><p>");
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        strYear.Append("<a href=\"" + string.Format(NewsDetail, mylist[i].SYS_FLD_DOI, DataBaseType.ENGLISHRES.GetHashCode()) + "&res=" + res + "\" target=\"_blank\" title=\"" + mylist[i].CNAME + mylist[i].YEAR + "年第" + mylist[i].ISSUE + "期\">[" + mylist[i].ISSUE + "期]</a>");
                        if (i != mylist.Count - 1)
                            strYear.Append("    |    ");
                    }

                    strYear.Append("</p></div>");
                }
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                string res = "study";
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList(sql, 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    strYear.Append("<div >");
                    strYear.Append("<p>" + mylist[0].YEAR + "年</p><p>");
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        strYear.Append("<a href=\"" + string.Format(NewsDetail, mylist[i].SYS_FLD_DOI, DataBaseType.STUDYRES.GetHashCode()) + "&res=" + res + "\" target=\"_blank\" title=\"" + mylist[i].CNAME + mylist[i].YEAR + "年第" + mylist[i].ISSUE + "期\">[" + mylist[i].ISSUE + "期]</a>");
                        if (i != mylist.Count - 1)
                            strYear.Append("    |    ");
                    }

                    strYear.Append("</p></div>");
                }
            }

            else if (mydbtype == DataBaseType.OWNERRES)
            {
                string res = "owner";
                JournalYear bll = new JournalYear(res);
                IList<JournalYearInfo> mylist = bll.GetList(sql, 1, 5000, out recordCount, false);
                if (mylist != null && mylist.Count > 0)
                {
                    strYear.Append("<div >");
                    strYear.Append("<p>" + mylist[0].YEAR + "年</p><p>");
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        strYear.Append("<a href=\"" + string.Format(NewsDetail, mylist[i].SYS_FLD_DOI, DataBaseType.OWNERRES.GetHashCode()) + "&res=" + res + "\" target=\"_blank\" title=\"" + mylist[i].CNAME + mylist[i].YEAR + "年第" + mylist[i].ISSUE + "期\">[" + mylist[i].ISSUE + "期]</a>");
                        if (i != mylist.Count - 1)
                            strYear.Append("    |    ");
                    }

                    strYear.Append("</p></div>");
                }
            }
            lt_catalog.Text += strYear.ToString();

        }

        /// <summary>
        /// 当未不存在任何记录时，在前台显示该资源不存在
        /// </summary>
        private void BindDataNoResult()
        {
            noResult.Visible = true;
            haveResult.Visible = false;
        }
    }
}