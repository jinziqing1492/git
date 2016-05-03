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
using DRMS.MirrorWeb.Utility;

namespace DRMS.MirrorWeb.view
{
    public partial class VideoDetail : System.Web.UI.Page
    {
        protected string mType { get; set; }
        protected string vpath { get; set; }
        protected string filename { get; set; }
        protected string VideoFileVirPath { get; set; }
        protected string VideoCoverVirPath { get; set; }
        protected string userIP { get; set; }
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
                DataBaseName = "视频&nbsp;>&nbsp;细览页";

                hidDoi.Value = bookdoi;
                hidPicDoi.Value = bookdoi;
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
                VideoInfo picInfo = new VideoInfo();
                Video picBll = new Video();
                picInfo = picBll.GetItem(hidPicDoi.Value);
                if (picInfo == null) // || picInfo.IsOnline != 1是否考虑上架问题
                {
                    BindDataNoResult();
                    return;
                }
                else
                {
                    StringBuilder sbdHtml = new StringBuilder();
                    lt_Title.Text = picInfo.Name;
                    if (string.IsNullOrEmpty(picInfo.Name))
                    {
                        lt_Title.Text = "未命名";
                    }

                    sbdHtml.AppendFormat("<li><span>【视频作者】</span>{0}</li>", picInfo.Author);
                    sbdHtml.AppendFormat("<li><span>【视频来源】</span>{0}</li>", picInfo.Source);
                    sbdHtml.AppendFormat("<li><span>【视频类型】</span>{0}</li>", picInfo.VideoType);
                    sbdHtml.AppendFormat("<li><span>【关 键 字】</span>{0}</li>", Tool.NormalFunction.ReplaceRed(GetKeyWordUrl(picInfo.Keywords, picInfo.VideoType)));
                    lt_summary.Text = sbdHtml.ToString();
                    lt_Digest.Text = picInfo.Description;

                    //图片显示控件属性初始化
                    string vpath = picInfo.SYS_FLD_VIRTUALPATHTAG;
                    string path = picInfo.SYS_FLD_FILEPATH;
                    VideoFileVirPath = FileManagementUtility.GetFileVirPathByResDoi(DataBaseType.VIDEODATA, hidPicDoi.Value);
                    VideoCoverVirPath = FileManagementUtility.GetCoverVirPathByResDoi(DataBaseType.VIDEODATA, hidPicDoi.Value);
                    noResult.Visible = false;
                    haveResult.Visible = true;

                    //记录日志
                    if (!IsPostBack)
                    {
                        Log logBll = new Log();
                        logBll.Add(DataBaseType.VIDEODATA, LogType.BROWSE, hidPicDoi.Value, picInfo.Name, "浏览视频");
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