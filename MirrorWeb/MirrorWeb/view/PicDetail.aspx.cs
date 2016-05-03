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
    public partial class PicDetail : System.Web.UI.Page
    {
        protected string vpath { get; set; }
        protected string filename { get; set; }protected string userIP { get; set; }
        public string DataBaseName { get; set; }

        protected string KeyWordUrlFormat = "<a  href=\"/view/DBThemeNav.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";
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
                DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(type);
                string sql = mydbtype.GetHashCode().ToString();
                DataBaseName = EnumDescription.GetFieldText(mydbtype) + "&nbsp;>&nbsp;细览页";
                hidDoi.Value = bookdoi;
                hidPicDoi.Value = bookdoi;
                //userIP = Request.UserHostAddress;
                //hidIP.Value = userIP;
                BindPicDetail();
            }
        }

        private void BindPicDetail()
        {
            if (string.IsNullOrEmpty(hidPicDoi.Value))
            {
                BindDataNoResult();
                return;
            }
            else
            {
                PicInfo picInfo = new PicInfo();
                Pic picBll = new Pic();
                picInfo = picBll.GetItem(hidPicDoi.Value);
                if (picInfo == null) // || picInfo.IsOnline != 1是否考虑上架问题
                {
                    BindDataNoResult();
                    return;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    lt_Title.Text = picInfo.Name;
                    if (string.IsNullOrEmpty(picInfo.Name))
                    {
                        lt_Title.Text = "未命名";
                    }
                    string pictype = "";
                    string picDescript = "";
                    switch (picInfo.PicType)
                    {
                        case "0":
                            pictype = "本身资源图片";
                            picDescript = picInfo.Description;
                            break;
                        case "1":
                            pictype = "公式";
                            picDescript = "";
                            break;
                        case "2":
                            pictype = "图表";
                            picDescript = picInfo.Description;
                            break;
                        case "3":
                            pictype = "插图";
                            picDescript = picInfo.Description;
                            break;
                        case "4":
                            pictype = "符号";
                            picDescript = picInfo.Description;
                            break;
                        default:
                            pictype = "本身资源图片";
                            picDescript = picInfo.Description;
                            break;
                    }
                    BaseModel bi = Utility.FileManagementUtility.GetResObjByDoi((DataBaseType)picInfo.Sys_fld_ParentType, picInfo.ParentDoi);
                    string source = picInfo.Source;
                    if (bi != null)
                    {
                        if (!string.IsNullOrEmpty(bi.Name))
                        {
                            string format = "<a href=\"{0}\">{1}</a>";
                            string url = Utility.UtilMngrResource.GetResViewPageSite((DataBaseType)picInfo.Sys_fld_ParentType, picInfo.ParentDoi);
                            source = string.Format(format, url, bi.Name);
                        }
                    }
                    string picSize = "";
                    if (picInfo.PicSize < 1024)
                    {
                        picSize = picInfo.PicSize + "B";
                    }
                    else if (picInfo.PicSize >= 1024 && picInfo.PicSize < 1024 * 1024)
                    {
                        picSize = Math.Round((float)picInfo.PicSize / 1024, 1) + "KB";
                    }
                    else
                    {
                        picSize = picInfo.PicSize / (1024 * 1024) + "M";
                    }

                    sb.AppendFormat("<li><span>【图片来源】</span>{0}</li>", source);
                    //sb.AppendFormat("<li><span>【图片来源】</span>{0}</li>", picInfo.Source);
                    sb.AppendFormat("<li><span>【图片作者】</span>{0}</li>", picInfo.Author);                    
                    sb.AppendFormat("<li><span>【图片类型】</span>{0}</li>", pictype);
                    sb.AppendFormat("<li><span>【图片大小】</span>{0}</li>", picSize);
                    sb.AppendFormat("<li><span>【拍 摄 地】</span>{0}</li>", picInfo.Place);
                    sb.AppendFormat("<li><span>【拍摄时间】</span>{0}</li>", picInfo.ShootingTime);
                    sb.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(picInfo.Keywords, picInfo.PicType)));
                    lt_summary.Text = sb.ToString();
                    lt_Digest.Text = picDescript;

                    //图片显示控件属性初始化
                    string vpath = picInfo.SYS_FLD_VIRTUALPATHTAG;
                    string path = picInfo.SYS_FLD_FILEPATH;
                    this.ctrl_Zoom.ThumbImgSrc = "/View/ShowPic.aspx?ptype=0&vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(path);
                    this.ctrl_Zoom.BigImgSrc = "/View/ShowPic.aspx?vpath=" + vpath + "&path=" + HttpUtility.UrlEncode(path);
                    this.ctrl_Zoom.SearchImgSrc = "../images/magnifier.png";
                    this.ctrl_Zoom.ImgDescription = CNKI.BaseFunction.NormalFunction.ReplaceLabel(picInfo.Name);
                    this.ctrl_Zoom.NoImgSrc = "";

                    //StringBuilder sbdZoom = new StringBuilder();
                    //sbdZoom.Append("<div class=\"zoom_images_thumb\">");
                    //sbdZoom.AppendFormat("<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" onload=\"SetImgAutoSize(this);\" />", hidPicDoi.Value, picInfo.Name);                    
                    //sbdZoom.Append("</div>");
                    //sbdZoom.AppendFormat("<a href=\"#\" class=\"zoom_images_clicka\" onclick=\"seeBigPic('getpicdata.aspx?key={0}','{1}')\">点击查看大图</a>", hidPicDoi.Value, picInfo.Name);
                    //lt_zoom.Text = sbdZoom.ToString();

                    //绑定该书的其他图片
                    //BindOtherPic(picInfo);

                    noResult.Visible = false;
                    haveResult.Visible = true;

                    //记录日志
                    if (!IsPostBack)
                    {
                        Log logBll = new Log();
                        logBll.Add(DataBaseType.PICDATA, LogType.BROWSE, hidPicDoi.Value, picInfo.Name, "浏览图片");
                    }
                }
            }
        }

        /// <summary>
        /// 简介为空的时候 进行隐藏处理
        /// </summary>
        /// <returns></returns>
        private string PicDescNull()
        {
            string result = string.Empty;
            result = "<script >var tempdiv=$('sm').parent; if(tempdiv.length>0)tempdiv.hide();</script>";
            return result;
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
        /// 绑定本图片的来源，（即本书中的其他图片）
        /// </summary>
        private void BindOtherPic(PicInfo info)
        {
            //Pic picbll = new Pic();
            //int recordCount = 0;
            //string sqlConditon = "  (PARENTDOI='" + info.ParentDoi + "' AND SYS_SYSID>'" + info.SYS_SYSID + "')";
            //List<PicInfo> list = picbll.GetList(sqlConditon, 1, 6, out recordCount, true);
            //if (list != null && list.Count != 0)
            //{
            //    StringBuilder sbd = new StringBuilder();
            //    sbd.Append("<div id='search_illustration'>");
            //    foreach (PicInfo picinfo in list)
            //    {
            //        string link = string.Format("/Page/PicDetail.aspx?doi={0}", picinfo.SYS_FLD_DOI);
            //        string fulltitle = picinfo.Name;
            //        string vpath = picinfo.SYS_FLD_VIRTUALPATHTAG;
            //        string path = picinfo.SYS_FLD_FILEPATH;
            //        string imgsrc = "/Page/ShowPic.aspx?vpath=" + vpath + "&path=" + path;
            //        string title = Tool.NormalFunction.GetSubStrOther(fulltitle, 20, "...");
            //        //sbd.Append("<li>");
            //        ////sbd.AppendFormat("<a class=\"pica\" href=\"{0}\" title=\"{1}\"><img src=\"{2}\" alt=\"{1}\" onload=\"SetImgAutoSize(this);\" /></a>", link, fulltitle, imgsrc);
            //        //sbd.AppendFormat("<a class=\"picdesa\" href=\"{0}\" title=\"{2}\">{1}</a>", link, title,fulltitle);
            //        //sbd.Append("</li>");

            //        sbd.Append("<dl>");
            //        sbd.Append("<dt>");
            //        sbd.AppendFormat("<a href='{2}' target='_blank'><img src='../Page/ShowPic.aspx?vpath={1}&path={0}' onload=\"SetImgAutoSize(this);\"/></a>", path, vpath, link);
            //        sbd.Append("</dt>");
            //        sbd.Append("<dd>");
            //        sbd.AppendFormat("<p><a href='{1}' title='{2}' target='_blank'>{0}</a></p>", title, link, fulltitle);
            //        //sbd.AppendFormat("<p>来源：<a href='{2}' target='_blank' title='{1}'>{0}</a></p>", source, fullSource, sourcePage);
            //        sbd.Append("</dd>");
            //        sbd.Append("</dl>");
            //    }
            //    sbd.Append("</div>");
            //    lt_otherpic.Text = sbd.ToString();
           // }
        }

        /// <summary>
        /// 添加关键词的url
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private string GetKeyWordUrl(string keyWord, string type)
        {
            string activeIndex = "0";
            switch (type)
            {
                case "1": activeIndex = "6";
                    break;
                case "2": activeIndex = "7";
                    break;
                case "3": activeIndex = "8";
                    break;
            }
            if (activeIndex == "0")
            {
                return "";
            }
            string[] keywordStr = keyWord.Split(new string[] { ";", ",", "；", "，" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder str = new StringBuilder();
            foreach (string s in keywordStr)
            {
                str.AppendFormat(KeyWordUrlFormat, HttpUtility.UrlEncode(s), s, activeIndex);
                str.Append("  ");
            }
            return str.ToString();
        }
    }
}