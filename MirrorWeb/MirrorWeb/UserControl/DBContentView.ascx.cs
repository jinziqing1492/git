using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Xml;

using DRMS.Model;
using DRMS.BLL;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class DBContentView : System.Web.UI.UserControl
    {

        private string StrWhere { get; set; }
        public string Type { get; set; }
        public string KeyWord { get; set; }
        public string SearchWord { get; set; }
        public string DocType { get; set; }
        public string SelectValue
        {
            get { return hid_selectValue.Value; }
            set { hid_selectValue.Value = value; }
        }
        public string OrderField
        {
            get { return hid_orderField.Value; }
            set { hid_orderField.Value = value; }
        }
        public string SqlWhereCondition
        {
            get { return hid_where.Value; }
            set { hid_where.Value = value; }
        }
        /// <summary>
        /// 是否只搜索内部资源
        /// </summary>
        public bool OwnerSearch
        {
            get { return hid_owner.Value == "true"; }
            set { hid_owner.Value = value ? "true" : "false"; }
        }
        /// <summary>
        /// 是否为二次检索
        /// </summary>
        public bool SecondSearch
        {
            get { return hid_second.Value == "true"; }
            set { hid_second.Value = value ? "true" : "false"; }
        }

        public string sqlEntry { get; set; }//查询条目时，可能需要传入where条件，进行比较复杂的查询
        public string PowerId { get; set; }
        protected string KeyWordUrlFormat = "<a  href=\"/view/DBThemeNav.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";
        protected string newKeyWordUrlFormat = "<a  href=\"../Default.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";
        public string Bookid { get; set; }
        public bool NoResult { get; set; }//判断是否查询到结果，如果未查到结果则不记录日志
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Type;
                string keyWord = KeyWord;
                string searchword = SearchWord;
                string bookid = Bookid;
                hid_type.Value = type;
                hid_keyWord.Value = keyWord;
                hid_sql.Value = sqlEntry;
                hid_search.Value = searchword;
                Hid_bookid.Value = bookid;
                Hid_powerid.Value = PowerId;
                //绑定数据
                BindData(type, keyWord, searchword);

                //添加日志
                if (!NoResult)
                {
                    Log bll = new Log();
                    if (!string.IsNullOrEmpty(keyWord))
                    {
                        //bll.Add(DataBaseType.AllDATA, LogType.SEARCH, "", keyWord, "查询");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        private void BindData(string type, string keyWord, string searchword)
        {
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);

            switch (mydbtype)
            {
                case DataBaseType.BOOKTDATA:
                    {
                        BindBook(keyWord, searchword);
                    }
                    break;
                case DataBaseType.REFERENCEBOOK:
                    {
                        BindToolBook(keyWord, searchword);
                    }
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    {
                        //会议论文集
                        BindConferencePaperData(keyWord, searchword);
                    }
                    break;
                case DataBaseType.CRITERION:
                    {
                        BindStdData(keyWord, searchword);
                    }
                    break;
                case DataBaseType.ENTRYDATA:
                    {
                        BindEntry(keyWord, Hid_bookid.Value);
                    }
                    break;
                case DataBaseType.BOOKCHAPTER:
                case DataBaseType.STDDATACHAPTER:
                    {
                        BindChapter(keyWord, searchword);
                    }
                    break;
                case DataBaseType.CONFERENCEARTICLE:
                    {
                        BindConferenceArticle(keyWord, searchword);
                    }
                    break;
                case DataBaseType.JOURNAL:
                    {
                        BindJournal(keyWord, searchword);
                    }
                    break;
                case DataBaseType.JOURNALARTICLE:
                    {
                        BindJournalArticle(keyWord, searchword);
                    }
                    break;
                case DataBaseType.YEARBOOK:
                    {
                        BindYearBook(keyWord, searchword);
                    }
                    break;
                case DataBaseType.MAGAZINE:
                    {
                        BindMagazine(keyWord, searchword);
                    }
                    break;
                case DataBaseType.NEWSPAPER:
                    {
                        BindNewsPaper(keyWord, searchword);
                    }
                    break;
                case DataBaseType.PICDATA:
                    {
                        BindPic(keyWord, searchword, "6");
                    }
                    break;
                case DataBaseType.THESIS:
                    {
                        BindThesis(keyWord, searchword);
                    }
                    break;
                case DataBaseType.AUDIODATA:
                    {
                        BindAudio(keyWord, searchword);
                    }
                    break;
                case DataBaseType.VIDEODATA:
                    {
                        BindVideo(keyWord, searchword);
                    }
                    break;
                case DataBaseType.CONTRACT:
                    {
                        BindContractData(keyWord);
                    }
                    break;
                case DataBaseType.AUTHOR:
                    {
                        BindAuthorData(keyWord);
                    }
                    break;
                case DataBaseType.ORG:
                    {
                        BindOrganData(keyWord);
                    }
                    break;
                case DataBaseType.ORIGINALDATA:
                    {
                        BindOriginalData(keyWord, searchword);
                    }
                    break;
                case DataBaseType.ENGLISHRES:
                    {
                        //BindJournalArticle(keyWord, searchword, "english");
                        BindJournal(keyWord, searchword, "english");
                    }
                    break;
                case DataBaseType.ENGLISHARTICLE:
                    {
                        BindJournalArticle(keyWord, searchword, "english");
                    }
                    break;
                case DataBaseType.STUDYRES:
                    {
                        BindJournalArticle(keyWord, searchword, "study");
                        //BindJournal(keyWord, searchword, "study");
                    }
                    break;
                case DataBaseType.OWNERRES:
                    {
                        if (!hid_sql.Value.ToUpper().Contains("SYS_FLD_CLASSFICATION"))
                        {
                            aspNetPager.PageSize = 12;
                            BindOwnerResourceType(keyWord, searchword);
                        }
                        else
                        {
                            BindJournal(keyWord, searchword, "owner");
                        }
                    }
                    break;
                //case "12": BindCJFD(keyWord);
                //    break;
                //case "8": BindPic(keyWord, "1");
                //    break;
                //case "10": BindPic(keyWord, "2");
                //    break;
                //case "13": BindPic(keyWord, "3");
                //    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 绑定音频
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="Isbook"></param>
        private void BindAudio(string keyWord, string searchword)
        {
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }

            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_FLD_CHECK_DATE";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            Audio bll = new Audio();
            IList<AudioInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (AudioInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetAudioDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取音频信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetAudioDetail(AudioInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string keyword = info.Keywords;
            string fullName = SubLinkTitle(info.Name);
            string pubDate = info.DateIssued == DateTime.MinValue ? "" : info.DateIssued.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Description, 120, "...");
            string hrefUrl = "/View/AudioDetail.aspx?doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            //sb.AppendFormat("<p>名称：<a href=\"{0}\" target=\"_blank\">{1}</a></p>", hrefUrl, NormalFunction.ReplaceRed(pubPlace));
            sb.Append("<p>");
            sb.AppendFormat("音频作者：<strong>{0}</strong>", info.Author);
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;关键字:<strong>{0}</strong>", NormalFunction.ReplaceRed(GetKeyWordUrl(keyword)));
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("音频来源：<strong>{0}</strong>", info.Source);
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;音频大小：<strong>{0}</strong>", info.AudioSize);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("出版时间：<strong>{0}</strong>", pubDate);
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;语种：<strong>{0}</strong>", info.Language);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("音频简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }
        /// <summary>
        /// 绑定视频
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="Isbook"></param>
        private void BindVideo(string keyWord, string searchword)
        {
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_FLD_CHECK_DATE";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            Video bll = new Video();
            IList<VideoInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div class='searmod'>");
                foreach (VideoInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetVideoDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }
        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetVideoDetail(VideoInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);
            string keyword = info.Keywords;
            string pubDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Description, 120, "...");
            string hrefUrl = "/View/VideoDetail.aspx?doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt class=\"noplay\">");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img class=\"detail_img\" src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("<img class=\"play_img\" src=\"../images/viplay.gif\" alt=\"\" /></a>");
            sb.Append("</dt>");
            sb.Append("<dd class=\"spe1\">");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</dd>");
            sb.Append("<dd class=\"spe2\">");
            sb.AppendFormat("视频作者：<strong>{0}</strong><em>|</em>", info.Author);
            sb.AppendFormat("关键字：<strong>{0}</strong>", NormalFunction.ReplaceRed(GetKeyWordUrl(keyword)));
            sb.Append("</dd>");
            sb.Append("<dd class=\"spe2\">");
            sb.AppendFormat("视频来源：<strong>{0}</strong><em>|</em>", info.Source);
            sb.AppendFormat("发布时间：<strong>{0}</strong>", pubDate);
            sb.Append("</dd>");
            sb.Append("<dd class=\"spe2\">");
            sb.AppendFormat("视频简介：<strong>{0}</strong>", absStr);
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定学位论文
        /// </summary>
        /// <param name="keyWord"></param>
        protected void BindThesis(string keyWord, string searchword)
        {

            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_FLD_CHECK_DATE";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            Thesis bll = new Thesis();
            IList<ThesisInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (ThesisInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetThesisDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取学位论文信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetThesisDetail(ThesisInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string searchword = info.Keywords;
            string pubPlace = info.Academy + info.DepartmentName;
            string subject = info.Subject + info.Major;
            string instructor = info.Instructor;
            //string language = info.Language;
            string pages = info.Keywords;
            string degreeyear = info.DegreeYear;
            string pubDate = info.PaperSubmissionDate == DateTime.MinValue ? "" : info.PaperSubmissionDate.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Abstract, 120, "...");
            string hrefUrl = "/View/BookDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + DataBaseType.THESIS.GetHashCode();//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>所属院系：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(pubPlace), NormalFunction.ReplaceRed(GetKeyWordUrl(searchword)));
            sb.Append("<p>");
            sb.AppendFormat("作者导师：<strong>{0}</strong>", NormalFunction.ReplaceRed(instructor));
            sb.AppendFormat("主修专业：<strong>{0}</strong>", subject);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("学位年度：<strong>{0}</strong>", degreeyear);
            sb.AppendFormat("提交时间：<strong>{0}</strong>", pubDate);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("论文简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="pictype"></param>
        private void BindPic(string keyWord, string searchword, string pictype)
        {
            string sqlName = "";
            if (pictype == "1")
            {
                aspNetPager.PageSize = 4;
            }
            else if (pictype == "2")
            {
                aspNetPager.PageSize = 6;
                sqlName = " And (name=* not name is null)";
            }
            else if (pictype == "3")
            {
                aspNetPager.PageSize = 9;
                sqlName = " And (name=* not name is null)";
            }
            else
            {
                aspNetPager.PageSize = 6;
                sqlName = " And (name=* not name is null)";
            }
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                if (pictype == "6")
                {
                    //让全部的类型都显示出来
                    StrWhere = "pictype=* " + sqlName;
                }
                else
                {
                    StrWhere = "pictype=" + pictype + sqlName;
                }
            }
            else
            {
                if (pictype == "6")
                {
                    StrWhere = hid_sql.Value + " AND pictype=*" + sqlName;
                }
                else
                {
                    StrWhere = hid_sql.Value + " AND pictype=" + pictype + sqlName;
                }
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND NAME='" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY SYS_FLD_CHECK_DATE desc";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            Pic bll = new Pic();
            IList<PicInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                string idName = pictype == "1" ? "search_formula" : (pictype == "2" ? "search_pic" : "search_illustration");
                sb.Append("<div id='" + idName + "'>");
                foreach (PicInfo info in list)
                {
                    sb.Append(GetPicDetail(info, pictype));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 绑定图片
        /// </summary>
        /// <param name="info"></param>
        private string GetPicDetail(PicInfo info, string pictype)
        {
            string doi = info.SYS_FLD_DOI;
            string path = info.SYS_FLD_FILEPATH;
            string vpath = info.SYS_FLD_VIRTUALPATHTAG;
            string title =  NormalFunction.ReplaceRed(info.Name);
            string fullName =  Utility.Utility.ClearTitle(info.Name);
            string source = info.Source;
            string fullSource = info.Source;
            string detailPage = "/view/PicDetail.aspx?doi=" + doi;
            string sourcePage = "";

            //获取source
            if (info.Sys_fld_ParentType == 1 || info.Sys_fld_ParentType == 3)
            {
                Book bll = new Book();
                BookInfo item = bll.GetItem(info.ParentDoi);
                if (item != null)
                {
                    source = NormalFunction.SubString(item.Name, 16, "...");
                    fullSource = item.Name;
                    sourcePage = "/view/BookDetail.aspx?doi=" + info.ParentDoi + "&type=1";
                }
            }
            else if (info.Sys_fld_ParentType == 2)
            {
                StdData bll = new StdData();
                StdDataInfo item = bll.GetItem(info.ParentDoi);
                if (item != null)
                {
                    source = NormalFunction.SubString(item.Name, 16, "...");
                    fullSource = item.Name;
                    sourcePage = "/view/BookDetail.aspx?doi=" + info.ParentDoi + "&type=2";
                }
            }
            else if (info.Sys_fld_ParentType == 5)
            {
                ConferenceArticle bll = new ConferenceArticle();
                ConferenceArticleInfo item = bll.GetItem(info.ParentDoi);
                if (item != null)
                {
                    source = NormalFunction.SubString(item.Name, 16, "...");
                    fullSource = item.Name;
                    //sourcePage = "/view/ConferenceArticleDetail.aspx?doi=" + info.ParentDoi;
                    sourcePage = "/view/ArticleDetail.aspx?type=" + DataBaseType.CONFERENCEPAPER.GetHashCode().ToString() + "&doi=" + info.ParentDoi;

                }
            }
            else if (info.Sys_fld_ParentType == 4)
            {
                JournalYear bll = new JournalYear();
                JournalYearInfo item = bll.GetItem(info.ParentDoi);
                if (item != null)
                {
                    //source = NormalFunction.SubString(item.Name, 16, "...");
                    source = item.CNAME + item.YEAR + "年" + "第" + item.ISSUE + "期";
                    fullSource = item.Name;
                    sourcePage = "/view/JournalDetail.aspx?doi=" + info.ParentDoi + "&type=" + DataBaseType.JOURNALYEAR.GetHashCode().ToString();
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href='{2}' target='_blank'><img src='../view/ShowPic.aspx?vpath={1}&path={0}&ptype=0' onload=\"SetImgAutoSize(this);\"/></a>", path, vpath, detailPage);
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.AppendFormat("<h2><a href='{1}' title='{2}' target='_blank'>{0}</a></h2>", NormalFunction.ReplaceRed(title), detailPage, fullName);
            if (sourcePage == "")
                sb.AppendFormat("<p>图片来源：{0}</p>", source);
            else
                sb.AppendFormat("<p>图片来源：<a href='{2}' target='_blank' title='{1}'>{0}</a></p>", source, fullSource, sourcePage);
            sb.Append("</dd>");

            sb.Append("</dl>");

            return sb.ToString();
        }
        /// <summary>
        /// 绑定报纸资源列表信息
        /// </summary>
        /// <param name="keyWord"></param>
        protected void BindNewsPaper(string keyWord, string searchword)
        {
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " order by  YEARISSUE desc";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            NewsPaper bll = new NewsPaper();
            IList<NewsPaperInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (NewsPaperInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetNewsPaperDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }
        /// <summary>
        /// 获取报纸资源信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetNewsPaperDetail(NewsPaperInfo info)
        {
            string name = Utility.Utility.SubTitle(info.CNAME, 25, "...");
            string fullName = SubLinkTitle(info.CNAME);
            string Pubdep = info.Pubdep;
            string Hostdep = info.Hostdep;
            string type = Utility.Utility.GetTypeNamefromXml("newstype", info.Type);
            string cn = info.CN;
            string pubPlace = info.PubPlace;
            string foundDate = Utility.Utility.FormatDateTime(info.FoundDate);
            string updateDate = Utility.Utility.FormatDateTimeDetail(info.SYS_FLD_CHECK_DATE);
            string absStr = NormalFunction.SubString(info.Description, 120, "...");
            string vpath = info.SYS_FLD_VIRTUALPATHTAG;
            string cpath = info.SYS_FLD_VIRTUALPATHTAG;
            int recordCount = 0;
            NewsPaperYear bll = new NewsPaperYear();
            IList<NewsPaperYearInfo> mylist = bll.GetList("baseid=" + info.BASEID + " order by yearissue desc", 1, 1, out recordCount, false);
            if (mylist != null && mylist.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                {
                    cpath = mylist[0].SYS_FLD_COVERPATH;
                    vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                }
            }

            string imgUrl = "ShowPic.aspx?vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(cpath);
            //string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            //string hrefUrl = "/View/JournalDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + DataBaseType.NEWSPAPER.GetHashCode();//点击超链接转到的页面
            string hrefUrl = "/View/YearIssueDetail.aspx?baseid=" + info.BASEID + "&type=" + DataBaseType.NEWSPAPER.GetHashCode();//点击超链接转到的报纸年期页面
            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            //sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(Pubdep), NormalFunction.ReplaceRed(GetKeyWordUrl(searchkeywords)));
            sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(Pubdep));
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;出版地：<strong>{0}</strong>", NormalFunction.ReplaceRed(pubPlace));
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("主办单位：<strong>{0}</strong>", NormalFunction.ReplaceRed(Hostdep));
            sb.AppendFormat("报纸类型：<strong>{0}</strong>", type);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("C&nbsp;N&nbsp;刊号：<strong>{0}</strong>", cn);
            sb.AppendFormat("建刊时间：<strong>{0}</strong>", foundDate);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("报纸简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }
        /// <summary>
        /// 绑定杂志
        /// </summary>
        /// <param name="keyWord"></param>
        protected void BindMagazine(string keyWord, string searchword)
        {
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_FLD_CHECK_DATE desc";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            Magazine bll = new Magazine();
            IList<MagazineInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (MagazineInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetMagazineDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }
        /// <summary>
        /// 获取杂志信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetMagazineDetail(MagazineInfo info)
        {
            string name = Utility.Utility.SubTitle(info.CNAME, 25, "...");
            string fullName = SubLinkTitle(info.CNAME);
            string Pubdep = info.Pubdep;
            string Hostdep = info.Hostdep;
            string type = Utility.Utility.GetTypeNamefromXml("magtype", info.Type);
            string cn = info.CN;
            string pubPlace = info.PubPlace;
            string foundDate = Utility.Utility.FormatDateTime(info.FoundDate);
            string updateDate = Utility.Utility.FormatDateTimeDetail(info.SYS_FLD_CHECK_DATE);
            string absStr = NormalFunction.SubString(info.Description, 120, "...");
            string vpath = info.SYS_FLD_VIRTUALPATHTAG;
            string cpath = info.SYS_FLD_VIRTUALPATHTAG;
            int recordCount = 0;
            MagazineYear bll = new MagazineYear();
            IList<MagazineYearInfo> mylist = bll.GetList("baseid=" + info.BASEID + " order by yearissue desc", 1, 1, out recordCount, false);
            if (mylist != null && mylist.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                {
                    cpath = mylist[0].SYS_FLD_COVERPATH;
                    vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                }
            }

            string imgUrl = "ShowPic.aspx?vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(cpath);
            //点击超链接转到的杂志年期页面
            string hrefUrl = "/View/YearIssueDetail.aspx?baseid=" + info.BASEID + "&type=" + DataBaseType.MAGAZINE.GetHashCode();
            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(Pubdep));
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;出版地：<strong>{0}</strong>", NormalFunction.ReplaceRed(pubPlace));
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("主办单位：<strong>{0}</strong>", NormalFunction.ReplaceRed(Hostdep));
            sb.AppendFormat("杂志类型：<strong>{0}</strong>", type);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("C&nbsp;N&nbsp;刊号：<strong>{0}</strong>", cn);
            sb.AppendFormat("建刊时间：<strong>{0}</strong>", foundDate);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("杂志简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定年鉴
        /// </summary>
        /// <param name="keyWord"></param>
        protected void BindYearBook(string keyWord, string searchword)
        {
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_FLD_CHECK_DATE desc";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            YearBookYear bll = new YearBookYear();
            IList<YearBookYearInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (YearBookYearInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetYearBookDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }
        /// <summary>
        /// 获取年鉴信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetYearBookDetail(YearBookYearInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string searchword = info.Keywords;
            string pubDep = info.IssueDep;
            string Isbn = info.ISBN;
            string liabilityDesc = info.LiabilityDesc;
            string language = info.Language;
            string pages = info.MaxPageNO;
            string updateDate = info.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd");
            string pubDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Digest, 120, "...");
            string hrefUrl = "/view/JournalDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + DataBaseType.YEARBOOK.GetHashCode();//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(pubDep), NormalFunction.ReplaceRed(GetKeyWordUrl(searchword)));
            sb.Append("<p>");
            sb.AppendFormat("责任说明：<strong>{0}</strong>", NormalFunction.ReplaceRed(liabilityDesc));
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年鉴页数：<strong>{0}</strong>", pages);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("ISBN编号：<strong>{0}</strong>", Isbn);
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;出版时间：<strong>{0}</strong>", pubDate);
            sb.AppendFormat("</p>");
            sb.Append("<p class=\"list\">");
            sb.AppendFormat("年鉴简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定期刊列表
        /// </summary>
        /// <param name="keyWord"></param>
        protected void BindJournal(string keyWord, string searchword, string res = "")
        {
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY SYS_SYSID DESC";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            Journal bll = new Journal(res);
            IList<JournalInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (JournalInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetJournalDetail(info, res));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 绑定自有资源类别
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="searchword"></param>
        protected void BindOwnerResourceType(string keyWord, string searchword)
        {
            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (StrWhere == null || !StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY SYS_FLD_ADDDATE desc";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            OwnerResourceType bll = new OwnerResourceType();
            IList<OwnerResourceTypeInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search_ownresource'>");
                foreach (OwnerResourceTypeInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetOwnResourceTypeDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }
        /// <summary>
        /// 获取期刊信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetOwnResourceTypeDetail(OwnerResourceTypeInfo info)
        {

            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH) + "&ptype=0";
            //点击超链接转到的期刊年期页面
            string queryConn = "SYS_FLD_CLASSFICATION='" + info.BASEID + "'";
            string hrefUrl = "/View/DatabaseContent.aspx?type=" + DataBaseType.OWNERRES.GetHashCode() + "&queryConn=" + queryConn;

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            sb.AppendFormat("<a href=\"{0}\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(info.NAME));
            sb.Append("</h2>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取期刊信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetJournalDetail(JournalInfo info, string res = "")
        {
            string name = Utility.Utility.SubTitle(info.CNAME, 25, "...");
            string fullName = SubLinkTitle(info.CNAME);
            string Pubdep = info.Pubdep;
            string Hostdep = info.Hostdep;
            string type = Utility.Utility.GetTypeNamefromXml("journaltype", info.Type);
            string cn = SubLinkTitle(info.CN);
            string pubPlace = info.PubPlace;
            string foundDate = Utility.Utility.FormatDateTime(info.FoundDate);
            string updateDate = Utility.Utility.FormatDateTimeDetail(info.SYS_FLD_CHECK_DATE);
            string absStr = NormalFunction.SubString(info.Description, 120, "...");
            string vpath = info.SYS_FLD_VIRTUALPATHTAG;
            string cpath = info.SYS_FLD_COVERPATH;

            int recordCount = 0;
            JournalYear bll = new JournalYear(res);
            IList<JournalYearInfo> mylist = bll.GetList("baseid='" + info.BASEID + "' order by yearissue desc", 1, 1, out recordCount, false);
            if (mylist != null && mylist.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_COVERPATH) && !string.IsNullOrWhiteSpace(mylist[0].SYS_FLD_VIRTUALPATHTAG))
                {
                    cpath = mylist[0].SYS_FLD_COVERPATH;
                    vpath = mylist[0].SYS_FLD_VIRTUALPATHTAG;
                }
            }

            DataBaseType dtype = DataBaseType.JOURNAL;
            if (res == "english")
            {
                dtype = DataBaseType.ENGLISHRES;
            }
            else if (res == "study")
            {
                dtype = DataBaseType.STUDYRES;
            }
            else if (res == "owner")
            {
                dtype = DataBaseType.OWNERRES;
            }

            string imgUrl = "ShowPic.aspx?vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(cpath) + "&ptype=0";
            //点击超链接转到的期刊年期页面
            string hrefUrl = "/View/YearIssueDetail.aspx?baseid=" + info.BASEID + "&type=" + dtype.GetHashCode();
            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            if (res == "owner")
            {
                sb.Append("<p>");
                sb.AppendFormat("简介：<strong>{0}</strong>", absStr);
                sb.Append("</p>");
            }
            else
            {
                sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(Pubdep));
                sb.AppendFormat("&nbsp;&nbsp;&nbsp;出版地：<strong>{0}</strong>", NormalFunction.ReplaceRed(pubPlace));
                sb.Append("</p>");
                sb.Append("<p>");
                sb.AppendFormat("主办单位：<strong>{0}</strong>", NormalFunction.ReplaceRed(Hostdep));
                sb.AppendFormat("期刊类型：<strong>{0}</strong>", type);
                sb.Append("</p>");
                sb.Append("<p>");
                sb.AppendFormat("C&nbsp;N&nbsp;刊号：<strong>{0}</strong>", cn);
                sb.AppendFormat("建刊时间：<strong>{0}</strong>", foundDate);
                sb.Append("</p>");
                sb.Append("<p>");
                sb.AppendFormat("期刊简介：<strong>{0}</strong>", absStr);
                sb.Append("</p>");
            }
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定期刊文章
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindJournalArticle(string keyWord, string searchword,string res="")
        {
            aspNetPager.PageSize = 6;
            StrWhere = "";
            //判断是根据关键词查询还是根据比较复杂的条件
            if (!string.IsNullOrEmpty(hid_sql.Value))
            {
                //StrWhere = "  ";
                StrWhere = hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + "   ";
            }
            //else
            //{
            //    StrWhere = hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + "   ";
            //}

            if (!string.IsNullOrEmpty(keyWord))
            {
                if (keyWord.StartsWith("@big@"))
                {
                    keyWord = keyWord.Replace("\"", "");
                    keyWord = keyWord.Replace("@big@", "");
                    string[] keywordarry = keyWord.Split(new string[] { "。", "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    string contentstr = string.Empty;
                    bool isfirst = true;
                    foreach (string item in keywordarry)
                    {
                        if (isfirst)
                        {
                            isfirst = false;
                            contentstr = " FULLTEXT=\"" + item + "\"";
                        }
                        else
                        {
                            contentstr += " or FULLTEXT=\"" + item + "\"";
                        }
                    }

                    if (string.IsNullOrEmpty(StrWhere))
                        StrWhere += "(" + contentstr + ")";
                    else
                        StrWhere += " and (" + contentstr + ")";
                }
                else
                {
                    string keywordSql = "";
                    if (string.IsNullOrEmpty(SelectValue) || SelectValue == "all")
                    {
                        keywordSql = "(NAME='" + keyWord + "' OR FULLTEXT=\"" + keyWord + "\" )";
                    }
                    else if (SelectValue == "title")
                    {
                        keywordSql = "NAME='" + keyWord + "'";
                    }
                    else if (SelectValue == "content")
                    {
                        keywordSql = "FULLTEXT='" + keyWord + "'";
                    }

                    if (string.IsNullOrEmpty(StrWhere))
                        StrWhere = keywordSql;
                    else
                        StrWhere += " AND " + keywordSql;
                }
            }

            if (!string.IsNullOrEmpty(searchword))
            {
                if (string.IsNullOrEmpty(StrWhere))
                    StrWhere += " KEYWORDS='?" + searchword + "'";
                else
                    StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //判断是否为只搜索内部资源
            if (string.IsNullOrEmpty(res) || res == "journal")
            {
                if (OwnerSearch)
                {
                    res = "owner";
                }
                else
                {
                    res = "ownerandjournalarticle";
                }
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY SYS_SYSID DESC";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            JournalArticle bll = new JournalArticle(res);
            IList<JournalArticleInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search_entry'>");
                foreach (JournalArticleInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetJournalArticleDetail(info, res));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取期刊文章
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetJournalArticleDetail(JournalArticleInfo info,string res="")
        {
            DataBaseType dtype = DataBaseType.JOURNALYEAR;
            if (res == "english")
            {
                dtype = DataBaseType.ENGLISHRES;
            }
            else if (res == "study")
            {
                dtype = DataBaseType.STUDYRES;
            }
            else if (res == "owner")
            {
                dtype = DataBaseType.OWNERRES;
            }

            string SourceUrl = "/Redirect/GotoJournal.aspx?type=journal&doi=" + info.ParentDoi;
            string specialTextUrl = "/Redirect/GotoJournal.aspx?type=article&doi=" + HttpUtility.UrlEncode(info.SYS_FLD_DOI);
            string fullTitle = info.Name;
            string title = fullTitle;

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            title = NormalFunction.ReplaceRed(title);
            title = Utility.Utility.ReplacePicUrlForBook(title);
            string content = NormalFunction.GetSubStrOther(NormalFunction.ReplaceLabel(info.SYS_FLD_ABSTRACT), 300, "...");
            content = NormalFunction.ReplaceRed(content);
            string jname = "";
            JournalYear bll = new JournalYear(res);
            JournalYearInfo jyi = bll.GetItem(info.ParentDoi);
            if (jyi != null)
                jname = jyi.CNAME + CNKI.BaseFunction.NormalFunction.ReplaceRed(info.Year) + "年" + "第" + CNKI.BaseFunction.NormalFunction.ReplaceRed(info.Issue) + "期";
            string wordsnum = info.FullText.Length.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt class=\"article_title\">");

            if (string.IsNullOrEmpty(notediv))
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", specialTextUrl, title, Utility.Utility.ClearTitle(fullTitle));
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{2}", specialTextUrl, title, notediv);
            }

            sb.Append("</dt>");
            sb.Append("<dd class=\"article_content\">");
            sb.AppendFormat("<p class=\"summary\">{0}</p>", content);
            sb.Append("<p class=\"detail\">");
            sb.AppendFormat("来源：<a target=\"_blank\" href=\"{1}\">{0}</a>", jname, SourceUrl);
            sb.AppendFormat("&nbsp|&nbsp字数： {0}字", wordsnum);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定图书  将工具书和图书整合起来
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="Isbook"></param>
        private void BindBook(string keyWord, string searchword)
        {

            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }

            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                if (string.IsNullOrEmpty(SelectValue) || SelectValue == "all")
                {
                    StrWhere += " AND (TITLE='" + keyWord + "' OR AUTHOR='?" + keyWord + "')";
                }
                else if (SelectValue == "title")
                {
                    StrWhere += " AND TITLE='" + keyWord + "'";
                }
                else if (SelectValue == "author")
                {
                    StrWhere += " AND AUTHOR='?" + keyWord + "'";
                }
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch&&!string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY SYS_FLD_CHECK_DATE desc";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            Book bll = new Book();
            IList<BookInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (BookInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetBookDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 绑定工具书
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="Isbook"></param>
        private void BindToolBook(string keyWord, string searchword)
        {

            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1  ";
            }

            //判断查询条件是否为空
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND TITLE='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + keyWord + "'";
            }


            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;
            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_FLD_CHECK_DATE desc";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            //获取列表信息
            ToolBook bll = new ToolBook();
            IList<ToolBookInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);
            //绑定页面信息

            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (ToolBookInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetToolBookDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 绑定标准
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindStdData(string keyWord, string searchword)
        {
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1 ";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1 ";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND NAME='" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_SYSID DESC";
            }
            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            StdData bll = new StdData();
            IList<StdDataInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (StdDataInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetStdDataDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 绑定词条
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindEntry(string keyWord, string bookid)
        {
            aspNetPager.PageSize = 6;
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                //StrWhere = "SYS_FLD_CHECK_STATE=-1 AND METATYPE=2 and parentdoi=\"" + bookid + "\" ";
                StrWhere = "METATYPE=2 ";
            }
            else
            {
                //StrWhere = hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + " AND SYS_FLD_CHECK_STATE=-1 AND METATYPE=2 and parentdoi=\"" + bookid + "\" ";
                StrWhere = hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + " AND METATYPE=2 ";
            }
            if (!string.IsNullOrEmpty(bookid))
            {
                StrWhere += " and parentdoi=\"" + bookid + "\" ";
            }

            //if (!string.IsNullOrEmpty(keyWord))
            //{
            //    StrWhere += " AND METATYPE=2 AND smartstr='" + keyWord + "'";
            //}
            if (!string.IsNullOrEmpty(keyWord))
            {
                if (!StrWhere.Contains(" METATYPE=2 "))
                {
                    StrWhere += " AND METATYPE=2 ";
                }
                if (keyWord.StartsWith("@big@"))
                {
                    keyWord = keyWord.Replace("\"", "");
                    keyWord = keyWord.Replace("@big@", "");
                    string[] keywordarry = keyWord.Split(new string[] { "。", "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    string contentstr = string.Empty;
                    bool isfirst = true;
                    foreach (string item in keywordarry)
                    {
                        if (isfirst)
                        {
                            isfirst = false;
                            contentstr = " smartstr=\"?" + item + "\"";
                        }
                        else
                        {
                            contentstr += " or smartstr=\"?" + item + "\"";
                        }
                    }
                    StrWhere += " and (" + contentstr + ")";
                }
                else
                    StrWhere += " and smartstr='?" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY SYS_FLD_CHECK_DATE DESC";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            Terminology bll = new Terminology();
            IList<TerminologyInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search_entry'>");
                foreach (TerminologyInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetEntryDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }


        /// <summary>
        /// 绑定论文集中的文章
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindConferenceArticle(string keyWord, string searchword)
        {
            aspNetPager.PageSize = 6;
            StrWhere = "";
            //判断是根据关键词查询还是根据比较复杂的条件
            if (!string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + "   ";
            }
            //else
            //{
            //    StrWhere = hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + "   ";
            //}

            if (!string.IsNullOrEmpty(keyWord))
            {
                if (keyWord.StartsWith("@big@"))
                {
                    keyWord = keyWord.Replace("\"", "");
                    keyWord = keyWord.Replace("@big@", "");
                    string[] keywordarry = keyWord.Split(new string[] { "。", "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    string contentstr = string.Empty;
                    bool isfirst = true;
                    foreach (string item in keywordarry)
                    {
                        if (isfirst)
                        {
                            isfirst = false;
                            contentstr = " FULLTEXT=\"" + item + "\"";
                        }
                        else
                        {
                            contentstr += " or FULLTEXT=\"" + item + "\"";
                        }
                    }
                    if (string.IsNullOrEmpty(StrWhere))
                        StrWhere += "(" + contentstr + ")";
                    else
                        StrWhere += " and (" + contentstr + ")";
                }
                else
                {
                    //StrWhere += " AND TITLE='" + keyWord + "' or FULLTEXT=\"" + keyWord + "\"";
                    if (string.IsNullOrEmpty(StrWhere))
                        StrWhere += " NAME='" + keyWord + "' or FULLTEXT=\"" + keyWord + "\"";
                    else
                        StrWhere += " AND NAME='" + keyWord + "' or FULLTEXT=\"" + keyWord + "\"";
                }
            }

            if (!string.IsNullOrEmpty(searchword))
            {
                //StrWhere += " AND KEYWORD='?" + searchword + "'";
                if (string.IsNullOrEmpty(StrWhere))
                    StrWhere += " KEYWORDS='?" + searchword + "'";
                else
                    StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY SYS_SYSID DESC";
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            ConferenceArticle bll = new ConferenceArticle();
            IList<ConferenceArticleInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search_entry'>");
                foreach (ConferenceArticleInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetConferenceArticleDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取论文集中的文章
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetConferenceArticleDetail(ConferenceArticleInfo info)
        {
            string hrefUrl = "/view/ConferencePaperDetail.aspx?doi=" + info.ParentDoi;
            //string specialTextUrl = "/view/ConferenceArticleDetail.aspx?doi=" + HttpUtility.UrlEncode(info.SYS_FLD_DOI);
            string specialTextUrl = "/view/ArticleDetail.aspx?type=" + DataBaseType.CONFERENCEPAPER.GetHashCode().ToString() + "&doi=" + HttpUtility.UrlEncode(info.SYS_FLD_DOI);
            string fullTitle = info.Name;
            string title = fullTitle;

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            title = NormalFunction.ReplaceRed(title);
            title = Utility.Utility.ReplacePicUrlForBook(title);

            string content = NormalFunction.GetSubStrOther(info.SYS_FLD_ABSTRACT, 300, "...");
            content = NormalFunction.ReplaceRed(content);

            string pName = info.ParentName;
            if (string.IsNullOrEmpty(pName))
            {
                ConferencePaper bll = new ConferencePaper();
                ConferencePaperInfo cpi = bll.GetItem(info.ParentDoi);
                if(cpi!=null)
                    pName = cpi.Name;
            }
            string words = info.FullText.Length.ToString();
            string searchword = info.KeyWord;

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt class=\"article_title\">");

            if (string.IsNullOrEmpty(notediv))
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", specialTextUrl, title, Utility.Utility.ClearTitle(fullTitle));
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{2}", specialTextUrl, title, notediv);
            }


            sb.Append("</dt>");
            sb.Append("<dd class=\"article_content\">");
            sb.AppendFormat("<p class=\"summary\">{0}</p>", content);
            sb.Append("<p class=\"detail\">");
            sb.AppendFormat("来源：<a target=\"_blank\" href=\"{1}\">{0}</a>", pName, hrefUrl);
            sb.AppendFormat("&nbsp|&nbsp字数： {0}字", words);
            sb.Append("</p>");
            sb.AppendFormat("<p>关键字：<strong>{0}</strong></p>", NormalFunction.ReplaceRed(GetKeyWordUrl(searchword)));
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 章节信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private void BindChapter(string keyWord, string searchword)
        {
            aspNetPager.PageSize = 6;
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "  (SYS_FLD_ISPART=0) ";
            }
            else
            {
                StrWhere = "(" + hid_sql.Value.Replace("FULLTEXT='*", "FULLTEXT='") + ") AND  (SYS_FLD_ISPART=0) ";
            }


            if (!string.IsNullOrEmpty(keyWord))
            {
                if (keyWord.StartsWith("@big@"))
                {
                    keyWord = keyWord.Replace("\"", "");
                    keyWord = keyWord.Replace("@big@", "");
                    string[] keywordarry = keyWord.Split(new string[] { "。", "、", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);
                    string contentstr = string.Empty;
                    bool isfirst = true;
                    foreach (string item in keywordarry)
                    {
                        if (isfirst)
                        {
                            isfirst = false;
                            contentstr = " content=\"" + item + "\"";
                        }
                        else
                        {
                            contentstr += " or content=\"" + item + "\"";
                        }
                    }
                    StrWhere += " and (" + contentstr + ")";
                }
                else
                {
                    if (string.IsNullOrEmpty(SelectValue) || SelectValue == "all")
                    {
                        StrWhere += " AND ( TITLE='" + keyWord + "' or content=\"" + keyWord + "\" )";
                    }
                    else if (SelectValue == "title")
                    {
                        StrWhere += " AND TITLE='" + keyWord + "'";
                    }
                    else if (SelectValue == "content")
                    {
                        StrWhere += " AND CONTENT='" + keyWord + "'";
                    }
                }
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORD='?" + searchword + "'";
            }
            if (!string.IsNullOrEmpty(DocType))
            {
                StrWhere += " AND DOCTYPE='" + DocType + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                if (string.IsNullOrEmpty(OrderField))
                {
                    StrWhere += " ORDER BY date";
                }
                else
                {
                    StrWhere += " ORDER BY " + OrderField + "";
                }
            }

            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            Chapter bll = new Chapter();
            IList<ChapterInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, true);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search_entry'>");
                foreach (ChapterInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetChapterDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取章节概览
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetChapterDetail(ChapterInfo info)
        {
            string hrefUrl = "/view/BookDetail.aspx?doi=" + info.ParentDoi;
            //阅读该章节的页面
            string readUrl = "../view/ChapterRead.aspx?doi=" + info.ParentDoi + "&chapterdoi=" + HttpUtility.UrlEncode(info.SYS_FLD_DOI) + "&type=" + info.doctype;
            string fullTitle = info.Title;
            string title = fullTitle;
            string searchword = info.keyword;

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            title = NormalFunction.ReplaceRed(title);
            title = Utility.Utility.ReplacePicUrlForBook(title);

            string content = NormalFunction.GetSubStrOther(info.SYS_FLD_ABSTRACT, 300, "...");
            content = NormalFunction.ReplaceRed(NormalFunction.ReplaceLabel(content));
           // content = Utility.Utility.ClearTitle(content);


            string toolBook = CNKI.BaseFunction.NormalFunction.ReplaceRed(info.ParentName);
            string words = info.Content.Length.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt class=\"article_title\">");
            //判断有没有注释
            if (string.IsNullOrEmpty(notediv))
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", readUrl, title, Utility.Utility.ClearTitle(fullTitle));
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{2}", readUrl, title, notediv);
            }

            sb.Append("</dt>");
            sb.Append("<dd class=\"article_content\">");
            sb.AppendFormat("<p class=\"summary\">{0}</p>", content);
            sb.Append("<p class=\"detail\">");
            sb.AppendFormat("来源：<a target=\"_blank\" href=\"{1}\">{0}</a>", toolBook, hrefUrl);
            sb.AppendFormat("&nbsp|&nbsp字数： {0}字", words);
            sb.Append("</p>");
            sb.AppendFormat("<p>关键字：<strong>{0}</strong></p>", NormalFunction.ReplaceRed(GetKeyWordUrl(searchword)));
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 获取工具书的具体信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetToolBookDetail(ToolBookInfo info)
        {

            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string pubPlace = info.IssueDep;
            string Isbn = info.ISBN;
            string author = info.Author;
            string language = info.Language;
            string pages = info.MaxPageNO;
            string keywords = info.Keywords;
            string printnum = info.PrintNUM.ToString();
            string updateDate = info.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd");
            string pubDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Digest, 120, "...");
            string hrefUrl = "/view/BookDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + DataBaseType.REFERENCEBOOK.GetHashCode();//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(pubPlace), NormalFunction.ReplaceRed(GetKeyWordUrl(keywords)));
            sb.Append("<p>");
            sb.AppendFormat("主编人员：<strong>{0}</strong>", NormalFunction.ReplaceRed(author));
            sb.AppendFormat("语种：<strong>{0}</strong>", GetLanguageText(language));
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("ISBN：<strong>{0}</strong>", Isbn);
            sb.AppendFormat("页数：<strong>{0}</strong>", pages);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("出版时间：<strong>{0}</strong>", pubDate);
            sb.AppendFormat("版次：<strong>{0}</strong>", printnum);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("工具书简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }



        /// <summary>
        /// 获取图书的具体信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetBookDetail(BookInfo info)
        {
            //info.Name=NormalFunction.ReplaceRed(info.Name);
            //string name = Utility.Utility.SubTitle(info.Name, 45, "...");
            string name = NormalFunction.ReplaceRed(info.Name);
            string fullName = SubLinkTitle(info.Name);

            string pubPlace = NormalFunction.ReplaceRed(info.IssueDep);
            string Isbn = NormalFunction.ReplaceRed(info.ISBN);
            string author = Utility.UtilMngrResource.GetAuthorFromBookInfo(info.Sys_fld_BookInfo);
            string price = NormalFunction.ReplaceRed(info.Price);
            string language = info.Language;
            string pages = info.MaxPageNO;
            string printnum = info.PrintNUM;
            string searchkeywords = NormalFunction.ReplaceRed(info.Keywords);
            string updateDate = info.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd");
            string pubDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.GetSubStrOther(info.Digest, 120, "...");
            //string hrefUrl = "/View/BookDetail.aspx?doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面
            string hrefUrl="";//点击超链接转到的页面
            if (Hid_powerid.Value=="1")
            {
                hrefUrl = "/View/PowerBookDetail.aspx?doi=" + info.SYS_FLD_DOI;
            }
            else
                hrefUrl = "/View/BookDetail.aspx?doi=" + info.SYS_FLD_DOI;
            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(pubPlace), GetKeyWordUrl(searchkeywords));
            //sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(pubPlace), searchkeywords);
            sb.Append("<p>");
            sb.AppendFormat("图书作者：<strong>{0}</strong>", NormalFunction.ReplaceRed(author));
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;语种：<strong>{0}</strong>", GetLanguageText(language));            
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("ISBN：<strong>{0}</strong>", Isbn);
            sb.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;纸书定价：<strong>{0}</strong>", price);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("图书简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }
        /// <summary>
        /// 获取标准数据的详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetStdDataDetail(StdDataInfo info)
        {
            //初始化需要用到的变量
            string name = NormalFunction.ReplaceRed(info.Name);// Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string searchword = info.Keywords;
            string draftDep = info.DraftDep;
            string draftPerson = info.DraftPerson;
            //string updateDate = info.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd");
            string pubDate = Utility.Utility.FormatDateTime(info.Dateissued);
            string language = info.Language;
            string stdno = info.stdno;
            string imgUrl = "ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Digest, 120, "...");
            string hrefUrl = "/view/BookDetail.aspx?doi=" + info.SYS_FLD_DOI + "&type=" + DataBaseType.CRITERION.GetHashCode();

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            //绑定数据
            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}"
                    , hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>起草单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(draftDep), NormalFunction.ReplaceRed(GetKeyWordUrl(searchword)));
            sb.Append("<p>");
            sb.AppendFormat("出版时间：<strong>{0}</strong>", pubDate);
            sb.AppendFormat("标准号：<strong>{0}</strong>", stdno);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("所属语种：<strong>{0}</strong>", language);
            sb.AppendFormat("起草人：<strong>{0}</strong>", draftPerson);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("标准简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }
        /// <summary>
        /// 获取词条 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetEntryDetail(TerminologyInfo info)
        {
            string hrefUrl = "/view/BookDetail.aspx?type=3&doi=" + info.ParentDOI; ;
            string specialTextUrl = "/view/EntryDetail.aspx?doi=" + info.SYS_FLD_DOI + "&name=" + HttpUtility.UrlEncode(NormalFunction.ResetRedFlag(info.Name));
            string fullTitle = info.Name + "  " + info.ENName;
            string title = Utility.Utility.SubTitle(fullTitle, 39, "...");
            fullTitle = SubLinkTitle(fullTitle);

            string pubDate = info.Pubdate == DateTime.MinValue ? "" : info.Pubdate.ToString("yyyy-MM-dd");
            string content = info.Content.Replace("<![CDATA[", "").Replace("]]>", "");
            content = NormalFunction.GetSubStrOther(NormalFunction.ReplaceLabel(content), 300, "...");
            string toolBookName = info.ParentName;//词条所在的工具书名称
            string words = info.Content.Length.ToString();

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);
            if (string.IsNullOrEmpty(toolBookName))
            {
                //获取词条所在的工具书名称
                ToolBook bll = new ToolBook();
                ToolBookInfo item = bll.GetItem(info.ParentDOI);
                if (item != null)
                {
                    toolBookName = item.Name;
                    //toolBook = NormalFunction.SubString(item.Name, 20, "...");
                }
                else
                {
                    toolBookName = "无";
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt class=\"article_title\">");
            sb.AppendFormat("<span class=\"date gray\">{0}</span>", pubDate);
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", specialTextUrl, NormalFunction.ReplaceRed(title), fullTitle);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a><img src='../images/notepic.png' class='notetitle'/>{3}"
                    , specialTextUrl, NormalFunction.ReplaceRed(title), fullTitle, notediv);
            }

            sb.Append("</dt>");
            sb.Append("<dd class=\"article_content\">");
            sb.AppendFormat("<p class=\"summary\">{0}</p>", NormalFunction.ReplaceRed(content));
            sb.Append("<p class=\"detail\">");
            sb.AppendFormat("来源：<a target=\"_blank\" href=\"{1}\">{0}</a>", toolBookName, hrefUrl);
            sb.AppendFormat("&nbsp|&nbsp字数： {0}字", words);
            sb.AppendFormat("<a href='{0}' class='readAll' target='_blank'>阅读全文>></a>", specialTextUrl);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }
        /// <summary>
        /// 绑定论文集
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindConferencePaperData(string keyWord, string searchword)
        {
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1";
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND NAME='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }


            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY ConferenceName";
            }
            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            ConferencePaper bll = new ConferencePaper();
            IList<ConferencePaperInfo> list = bll.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (ConferencePaperInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetConferencePaperDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }
        /// <summary>
        /// 绑定论文集
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="Isbook"></param>
        private string GetConferencePaperDetail(ConferencePaperInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string keyword = info.Keywords;
            string pubPlace = info.IssueDep;
            string Isbn = info.ISBN;
            string author = info.Author;
            string language = info.Language;
            string pages = info.MaxPageNO;
            string updateDate = info.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd");
            string pubDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");
            string imgUrl = "ShowPic.aspx?ptype=0&vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_COVERPATH);
            string absStr = NormalFunction.SubString(info.Digest, 120, "...");
            string hrefUrl = "/view/ConferencePaperDetail.aspx?doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a></a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>出版单位：<a href=\"{0}\" target=\"_blank\">{1}</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;关键字：<strong>{2}</strong></p>", hrefUrl, NormalFunction.ReplaceRed(pubPlace), NormalFunction.ReplaceRed(GetKeyWordUrl(keyword)));
            sb.Append("<p>");            
            sb.AppendFormat("出版时间：<strong>{0}</strong>", pubDate);
            sb.AppendFormat("图书作者：<strong>{0}</strong>", NormalFunction.ReplaceRed(author));
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("ISBN：<strong>{0}</strong>", Isbn);
            sb.AppendFormat("语种：<strong>{0}</strong>", language);            
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendFormat("论文集简介：<strong>{0}</strong>", absStr);
            sb.Append("</p>");
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定合同内容
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindContractData(string keyWord)
        {
            StrWhere = "";
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " CONTRACTNAME='" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY CONTRACTNAME";
            }
            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            Contract contract = new Contract();
            IList<ContractInfo> list = contract.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (ContractInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetContractDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 绑定合同详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetContractDetail(ContractInfo info)
        {
            Book _book = new Book();
            string name = Utility.Utility.SubTitle(info.CONTRACTNAME, 25, "...");
            string fullName = SubLinkTitle(info.CONTRACTNAME);

            string contractNo = info.CONTRACTNO;
            string beginDate = info.BEGINDATETIME.ToString("yyyy-MM-dd");
            string endDate = info.ENDDATETIME.ToString("yyyy-MM-dd");
            string bookName = "";
            if (!string.IsNullOrEmpty(info.BOOKID))
            {
                StringBuilder result = new StringBuilder();
                string[] Id = info.BOOKID.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in Id)
                {
                    BookInfo book = _book.GetItem(item);
                    if (book != null)
                    {
                        result.Append(book.Name);
                        result.Append(";");
                    }
                }
                bookName = result.ToString();
            }
            string partA=info.Parta;
            string partB=info.Partb;
            string description=info.DESCRIPTION;
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_FILEPATH);
            string hrefUrl = "/view/SupportDetail.aspx?dbtype=13&&doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a></a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>合同：<a href=\"{0}\" target=\"_blank\">{1}</a></p>", hrefUrl, NormalFunction.ReplaceRed(name));
            sb.Append("<p>");
            sb.AppendFormat("签订图书：<strong>{0}</strong>", NormalFunction.ReplaceRed(bookName));
            sb.Append("</p><p>");
            sb.AppendFormat("合同编号：<strong>{0}</strong>", contractNo);
            sb.Append("</p><p>");
            sb.AppendFormat("原著作权人：<strong>{0}</strong>", partA);
            sb.AppendFormat("合同乙方：<strong>{0}</strong>", partB);
            sb.Append("</p><p>");
            sb.AppendFormat("开始时间：<strong>{0}</strong>", beginDate);
            sb.AppendFormat("结束时间：<strong>{0}</strong>", endDate);
            sb.Append("</p>");
            sb.AppendFormat("<p class=\"desc\">{0}</p>", NormalFunction.ReplaceRed(description));
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定作者信息
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindAuthorData(string keyWord)
        {
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1";
            }

            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND Name='" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY Name";
            }
            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            Author author = new Author();
            IList<AuthorInfo> list = author.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (AuthorInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetAuthorDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取作者详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetAuthorDetail(AuthorInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string sex = "";
            switch (info.Sex)
            {
                case 0:
                    sex = "未知";
                    break;
                case 1:
                    sex = "男";
                    break;
                case 2:
                    sex = "女";
                    break;
                default:
                    break;
            }
            string othername = info.Othername;
            string birthday = info.Birthday.ToString("yyyy-MM-dd");
            string hometown = info.Hometown;
            string mainwork = info.Mainwork;
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_FILEPATH);
            string absStr = NormalFunction.SubString(info.Digest, 120, "...");
            string hrefUrl = "/view/SupportDetail.aspx?dbtype=14&&doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a></a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>姓名：<a href=\"{0}\" target=\"_blank\">{1}</a></p>", hrefUrl, NormalFunction.ReplaceRed(name));
            sb.Append("<p>");
            sb.AppendFormat("性别：<strong>{0}</strong>", sex);
            sb.AppendFormat("出生年月：<strong>{0}</strong>", birthday);
            sb.Append("</p><p>");
            sb.AppendFormat("籍贯：<strong>{0}</strong>", hometown);
            sb.AppendFormat("曾用名：<strong>{0}</strong>", othername);
            sb.Append("</p><p>");
            sb.AppendFormat("主要作品：<strong>{0}</strong>", mainwork);
            sb.Append("</p>");
            sb.AppendFormat("<p class=\"desc\">{0}</p>", NormalFunction.ReplaceRed(absStr));
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定机构信息
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindOrganData(string keyWord)
        {
            //判断是根据关键词查询还是根据比较复杂的条件
            if (string.IsNullOrEmpty(hid_sql.Value))
            {
                StrWhere = "SYS_FLD_CHECK_STATE=-1";
            }
            else
            {
                StrWhere = hid_sql.Value + " AND SYS_FLD_CHECK_STATE=-1";
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " AND Name='" + keyWord + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY Name";
            }
            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            Org org = new Org();
            IList<OrgInfo> list = org.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (OrgInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetOrganDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取机构详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetOrganDetail(OrgInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);

            string pubPlace = info.Place;
            string foundDate = info.Founddate.ToString("yyyy-MM-dd");
            string contractPerson = info.Contractperson;
            string contractTel = info.Contracttel;
            string imgUrl = "ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + HttpUtility.UrlEncode(info.SYS_FLD_FILEPATH);
            string absStr = NormalFunction.SubString(info.DESCRIPTION, 120, "...");
            string hrefUrl = "/view/SupportDetail.aspx?dbtype=15&&doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dt>");
            sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", hrefUrl, imgUrl, "");
            sb.Append("</dt>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a></a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>出版：<a href=\"{0}\" target=\"_blank\">{1}</a></p>", hrefUrl, NormalFunction.ReplaceRed(name));
            sb.Append("<p>");
            sb.AppendFormat("组织地点：<strong>{0}</strong>", NormalFunction.ReplaceRed(pubPlace));
            sb.AppendFormat("成立日期：<strong>{0}</strong>", foundDate);
            sb.Append("</p><p>");
            sb.AppendFormat("联系人：<strong>{0}</strong>", contractPerson);
            sb.AppendFormat("联系电话：<strong>{0}</strong>", contractTel);
            sb.Append("</p>");
            sb.AppendFormat("<p class=\"desc\">{0}</p>", NormalFunction.ReplaceRed(absStr));
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 绑定原始数据信息
        /// </summary>
        /// <param name="keyWord"></param>
        private void BindOriginalData(string keyWord, string searchword)
        {
            StrWhere = "";
            if (!string.IsNullOrEmpty(keyWord))
            {
                StrWhere += " Name='" + keyWord + "'";
            }
            if (!string.IsNullOrEmpty(searchword))
            {
                StrWhere += " AND KEYWORDS='?" + searchword + "'";
            }

            //判断是否为二次检索，如果是需要加上上次的条件
            if (SecondSearch && !string.IsNullOrEmpty(SqlWhereCondition))
            {
                StrWhere = SqlWhereCondition + " AND " + StrWhere;
            }
            //记录查询条件 用于二次检索
            SqlWhereCondition = StrWhere;

            if (!StrWhere.ToUpper().Contains("ORDER BY"))
            {
                StrWhere += " ORDER BY Name";
            }
            int pageNo = aspNetPager.CurrentPageIndex;
            int allCount = 0;

            OriginalData orig = new OriginalData();
            IList<OriginalDataInfo> list = orig.GetList(StrWhere, pageNo, aspNetPager.PageSize, out allCount, false);

            //绑定页面信息
            if (list != null && list.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                //绑定分页控件
                aspNetPager.RecordCount = allCount;
                ltlmessage.Text = Utility.Utility.GetPageMsg(aspNetPager);
                sb.Append("<div id='search-result'>");
                foreach (OriginalDataInfo info in list)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    sb.Append(GetOriginalDataDetail(info));
                }
                sb.Append("</div>");
                lt_list.Text = sb.ToString();
            }
            else
            {
                BindDataNoResult();
            }
        }

        /// <summary>
        /// 获取原始资料详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetOriginalDataDetail(OriginalDataInfo info)
        {
            string name = Utility.Utility.SubTitle(info.Name, 25, "...");
            string fullName = SubLinkTitle(info.Name);
            Book _book = new Book();

            string parentName = "";
            if (!string.IsNullOrEmpty(info.ParentName))
            {
                StringBuilder result = new StringBuilder();
                string[] Id = info.ParentName.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in Id)
                {
                    BookInfo book = _book.GetItem(item);
                    result.Append(book.Name);
                    result.Append(";");
                }
                parentName = result.ToString();
            }
            string classname = info.SYS_FLD_CLASSNAME;
            string keyword = info.KeyWords;
            string absStr = NormalFunction.SubString(info.Desciption, 120, "...");
            string hrefUrl = "/view/SupportDetail.aspx?dbtype=28&&doi=" + info.SYS_FLD_DOI;//点击超链接转到的页面

            //处理注释
            string notediv = Utility.Utility.DealNoteTitle(info.Note);

            StringBuilder sb = new StringBuilder();
            sb.Append("<dl>");
            sb.Append("<dd>");
            sb.Append("<h2>");
            if (notediv == "")
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a>", hrefUrl, NormalFunction.ReplaceRed(name), fullName);
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\" title='{2}' target=\"_blank\">{1}</a></a><img src='../images/notepic.png' class='notetitle'/>{3}",
                    hrefUrl, NormalFunction.ReplaceRed(name), fullName, notediv);
            }
            sb.Append("</h2>");
            sb.AppendFormat("<p>资源名称：<a href=\"{0}\" target=\"_blank\">{1}</a></p>", hrefUrl, NormalFunction.ReplaceRed(name));
            sb.Append("<p>");
            sb.AppendFormat("所属图书：<strong>{0}</strong>", NormalFunction.ReplaceRed(parentName));
            sb.AppendFormat("分类名称：<strong>{0}</strong>", classname);
            sb.Append("</p><p>");
            sb.AppendFormat("关键词：<strong>{0}</strong>", NormalFunction.ReplaceRed(GetKeyWordUrl(keyword)));
            sb.Append("</p>");
            sb.AppendFormat("<p class=\"desc\">{0}</p>", NormalFunction.ReplaceRed(absStr));
            sb.Append("</dd>");
            sb.Append("</dl>");
            return sb.ToString();
        }

        /// <summary>
        /// 当未查询到任何记录时，在前台显示未查到记录
        /// </summary>
        private void BindDataNoResult()
        {
            NoResult = true;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div style=' margin:15px auto; width:300px; height:50px; text-align:center; color:Red; padding-top:20px; border:1px solid #aed0ea;'>
                                <p style=' margin:auto;'>暂无相关记录，请重新选择</p>
                            </div>");
            lt_list.Text = sb.ToString();
        }

        /// <summary>
        /// 在a标签的title属性中的值不应该出现##LEFT## 和##RIGHT## 所以需要移除
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private string SubLinkTitle(string title)
        {
            //string subTitle = "";
            //subTitle = title.Replace("##LEFT##", "");
            //subTitle = subTitle.Replace("##RIGHT##", "");
            //return subTitle;
            return CNKI.BaseFunction.NormalFunction.ResetRedFlag(title);

        }

        /// <summary>
        /// 获取语种的中文字符
        /// </summary>
        /// <returns></returns>
        private string GetLanguageText(string langageCode)
        {
            if (string.IsNullOrEmpty(langageCode))
            {
                return string.Empty;
            }
            //string lcode = ";zh-hans;zh-hant;en;ja;ko-kr;ru;fr;de;";
            //string[] ldisplay = { "中文(简体)", "中文(繁体)", "英语", "日语", "韩文", "俄语", "法语", "德语" };
            Hashtable has = new Hashtable();
            has.Add("zh-hans", "中文(简体)");
            has.Add("zh-hant", "中文(简体)");
            has.Add("en", "英语");
            has.Add("ja", "日语");
            has.Add("ko-kr", "韩文");
            has.Add("ru", "俄语");
            has.Add("fr", "法语");
            has.Add("de", "德语");


            string[] larr = langageCode.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            string result = langageCode;
            bool first = true;
            foreach (string s in larr)
            {
                if (first)
                {
                    if (has.ContainsKey(s.ToLower()))
                    {
                        result = has[s.ToLower()].ToString();
                        first = false;
                    }
                }
                else
                {
                    if (has.ContainsKey(s.ToLower()))
                    {
                        result += ";" + has[s.ToLower()].ToString();
                        //first = false;
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void aspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            aspNetPager.CurrentPageIndex = e.NewPageIndex;
            //type 
            string type = hid_type.Value;
            string keyWord = hid_keyWord.Value;
            string searchWord = hid_search.Value;
            //绑定数据
            BindData(type, keyWord, searchWord);
        }

        /// <summary>
        /// 添加关键词的url
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private string GetKeyWordUrl(string keyWord)
        {
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(keyWord))
            {
                string[] keywordStr = keyWord.Split(new string[] { "；", "，", ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in keywordStr)
                {
                    //str.AppendFormat(KeyWordUrlFormat, HttpUtility.UrlEncode(s), s);
                    str.AppendFormat(newKeyWordUrlFormat, HttpUtility.UrlEncode(s), s);
                    str.Append(" ");
                }
            }
            return str.ToString();
        }
    }
}