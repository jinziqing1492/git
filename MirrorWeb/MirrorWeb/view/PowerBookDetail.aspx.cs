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
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class PowerBookDetail : BasePage.BasePage
    {
        protected string mType { get; set; }
        protected string vpath { get; set; }
        protected string filename { get; set; }
        protected string ClassofElectricalfence = ConfigurationManager.AppSettings["ClassofElectricalfence"].ToString();
        protected string ClassofElectronics = ConfigurationManager.AppSettings["ClassofElectronics"].ToString();
        protected string ClassofStd = ConfigurationManager.AppSettings["ClassofStd"].ToString();
        protected string userIP { get; set; }

        protected string KeyWordUrlFormat = "<a  href=\"../Default.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";
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
                string type = Tool.NormalFunction.GetQueryString("type", "1");
                mType = type;
                DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
                string sql = mydbtype.GetHashCode().ToString();
                hidDoi.Value = bookdoi;
                BindData(bookdoi, type);

            }
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
            string digest = "";
            string strXml = "";
            string path = "";
            string note = "";
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
            if (mydbtype == DataBaseType.CRITERION)
            {
                StdData bll = new StdData();
                StdDataInfo info = bll.GetItem(doi);
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
                title = info.Name;
                digest = info.Digest;
                strXml = info.SYS_FLD_CATALOG;
                note = info.Note;

                string dateissued = info.Dateissued == DateTime.MinValue ? "" : info.Dateissued.ToString("yyyy-MM-dd");
                string dateimplate = info.DateImplement == DateTime.MinValue ? "" : info.DateImplement.ToString("yyyy-MM-dd");

                string charpterUrl = "CharpterReade.aspx?doi=" + doi + "&type=10";

                //绑定概要信息
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<li><span class='sAuthor'>【批准部门】</span>{0}</li>", info.ApproveDep);
                sb.AppendFormat("<li><span>【主编单位】 </span>{0}</li>", info.ProposeDep);
                sb.AppendFormat("<li><span>【主持机构】 </span>{0}</li>", info.HostInstitution);
                sb.AppendFormat("<li><span>【解释单位】 </span>{0}</li>", info.ExplainDep);
                sb.AppendFormat("<li><span>【发布时间】</span>{0}</li>", dateissued);
                sb.AppendFormat("<li><span>【实施时间】</span>{0}</li>", dateimplate);
                sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));                
                lt_summary.Text = sb.ToString();

                //绑定价格信息
                BindPrice(mydbtype.GetHashCode().ToString(), doi, title);

                //记录日志
                if (!IsPostBack)
                {
                    Log logBll = new Log();
                    logBll.Add(DataBaseType.CRITERION, LogType.BROWSE, doi, title, "浏览标准");
                }

            }
            else
            {

                if (mydbtype == DataBaseType.BOOKTDATA)
                {
                    //根据doi 获取图书实体
                    Book bll = new Book();
                    BookInfo info = bll.GetItem(doi);
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
                    digest = info.Digest;
                    strXml = info.SYS_FLD_CATALOG;
                    filePath = info.SYS_FLD_FILEPATH;
                    note = info.Note;
                    //string resouceType = info.SYS_FLD_BOOKTYPE == 1 ? "2" : "0";
                    string issueDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");

                    //绑定图书的概要信息
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<li><span class='sAuthor'>【图书作者】</span>{0}</li>", info.Author);
                    sb.AppendFormat("<li><span>【ＩＳＢＮ】</span>{0}</li>", info.ISBN);
                    sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.IssueDep);
                    sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", issueDate);
                    sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                    //sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(info.Keywords));
                    sb.AppendFormat("<li><span>【分类体系】</span>{0}</li>", Utility.UtilMngrResource.DealWithClassfication(info.SYS_FLD_CLASSFICATION));

                    lt_summary.Text = sb.ToString();

                    //绑定价格信息
                    BindPrice(mydbtype.GetHashCode().ToString(), doi, title);
                    bll.AddHitCount(doi);

                    //记录日志
                    if (!IsPostBack)
                    {
                        Log logBll = new Log();
                        logBll.Add(DataBaseType.BOOKTDATA, LogType.BROWSE, doi, title, "浏览图书");
                    }

                }
                else
                {
                    if (mydbtype == DataBaseType.REFERENCEBOOK)
                    {
                        //根据doi 获取实体
                        ToolBook bll = new ToolBook();
                        ToolBookInfo info = bll.GetItem(doi);
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
                        digest = info.Digest;
                        strXml = info.SYS_FLD_CATALOG;
                        filePath = info.SYS_FLD_FILEPATH;
                        note = info.Note;
                        string issueDate = info.IssueDate == DateTime.MinValue ? "" : info.IssueDate.ToString("yyyy-MM-dd");

                        //绑定概要信息
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<li><span class='sAuthor'>【主 编 人】</span>{0}</li>", info.Author);
                        sb.AppendFormat("<li><span>【ＩＳＢＮ】</span>{0}</li>", info.ISBN);
                        sb.AppendFormat("<li><span>【出版单位】 </span>{0}</li>", info.IssueDep);
                        sb.AppendFormat("<li><span>【出版时间】</span>{0}</li>", issueDate);
                        sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                        sb.AppendFormat("<li><span>【分类体系】</span>{0}</li>", Utility.UtilMngrResource.DealWithClassfication(info.SYS_FLD_CLASSFICATION));

                        lt_summary.Text = sb.ToString();

                        //绑定价格信息
                        BindPrice(mydbtype.GetHashCode().ToString(), doi, title);

                        //记录日志
                        if (!IsPostBack)
                        {
                            Log logBll = new Log();
                            logBll.Add(DataBaseType.REFERENCEBOOK, LogType.BROWSE, doi, title, "浏览工具书");
                        }

                    }
                    else
                    {
                        if (mydbtype == DataBaseType.THESIS)
                        {
                            //根据doi 获取图书实体
                            Thesis bll = new Thesis();
                            ThesisInfo info = bll.GetItem(doi);
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
                            digest = info.Abstract;
                            strXml = info.SYS_FLD_CATALOG;
                            filePath = info.SYS_FLD_FILEPATH;
                            note = info.Note;
                            string issueDate = info.PaperSubmissionDate == DateTime.MinValue ? "" : info.PaperSubmissionDate.ToString("yyyy-MM-dd");

                            //绑定概要信息
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("<li><span class='sAuthor'>【论文作者】</span>{0}</li>", info.Author);
                            sb.AppendFormat("<li><span>【作者导师】</span>{0}</li>", info.Instructor);
                            sb.AppendFormat("<li><span>【所属院系】 </span>{0}</li>", info.Academy + info.DepartmentName);
                            sb.AppendFormat("<li><span>【所属专业】</span>{0}</li>", info.Subject + info.Major);
                            sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(info.Keywords)));
                            lt_summary.Text = sb.ToString();

                            //绑定价格信息
                            BindPrice(mydbtype.GetHashCode().ToString(), doi, title);

                            //记录日志
                            if (!IsPostBack)
                            {
                                Log logBll = new Log();
                                logBll.Add(DataBaseType.THESIS, LogType.BROWSE, doi, title, "浏览学位论文");
                            }
                        }
                    }

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
            lt_Digest.Text = digest;

            //绑定目录
            BindCataLog(strXml);

            //string realPath = Utility.FileManagementUtility.GetFilePath(vpath, filePath);
            //BindAttachment(doi, type, title, realPath);

            //显示数据
            haveResult.Visible = true;
            noResult.Visible = false;
        }


        /// <summary>
        /// 绑定目录
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
        }

        /// <summary>
        /// 绑定价格信息 并根据各种条件生成不同的操作按钮
        /// </summary>
        /// <param name="resouceType"></param>
        /// <param name="doi"></param>
        /// <param name="title"></param>
        private void BindPrice(string resouceType, string doi, string title)
        {
            //string charpterUrl = "ChapterRead.aspx?doi=" + doi + "&type=" + resouceType;
            //string entryUrl = "EntryList.aspx?bookid=" + doi;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='btn-book'>");
            sb.Append("<a href='/AdminknReader/default.aspx?doi=" + doi + "&type=" + resouceType + "' class='btn-online'></a>");
            sb.Append("<a href='/View/DownLoadDrmFile.aspx?doi=" + doi + "&type=" + resouceType + "' class='btn-download'></a>");
            //if (resouceType == DataBaseType.BOOKTDATA.GetHashCode().ToString())
            //{
            //    sb.Append("<a href='" + charpterUrl + "' class='btn-chapterbuy'></a>");
            //}
            //else if (resouceType == DataBaseType.REFERENCEBOOK.GetHashCode().ToString())
            //{
            //    sb.Append("<a href='" + entryUrl + "' class='btn-entryview'></a>");
            //}
            //else
            //{
            //    sb.Append("<a href='" + charpterUrl + "' class='btn-chapterbuy'></a>");
            //}
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
            string[] keywordStr = keyWord.Split(new string[] { "；", "，", ";", "," }, StringSplitOptions.RemoveEmptyEntries);
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