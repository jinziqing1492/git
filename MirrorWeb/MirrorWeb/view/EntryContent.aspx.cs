using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

using DRMS.Model;
using DRMS.BLL;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class EntryContent : System.Web.UI.Page
    {

        protected string userNameLogin = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doi = Tool.NormalFunction.GetQueryString("doi", "0");
                BindDetail(doi);
            }
        }

        /// <summary>
        /// 词条详细内容
        /// </summary>
        protected void BindDetail(string doi)
        {
            string title = "";
            string strXml = "";
            string vpath = "";
            string path = "";
            string source = "";
            string keyword = string.Empty;

            Terminology bll = new Terminology();
            TerminologyInfo info = bll.GetItem(doi);
            if (info == null)
            {
                BindDataNoResult();
                return;
            }

            //为变量赋值
            vpath = info.SYS_FLD_VIRTUALPATHTAG;
            path = info.SYS_FLD_COVERPATH;
            //filename = info.SYS_FLD_FILEPATH;
            title = info.Name;
            source = info.ParentName;//来源的名称
            strXml = info.SYS_FLD_PARAXML;
            string parentDoi = info.ParentDOI;

            string strFormat = "<a href=\"BookDetail.aspx?doi={0}&type=3\" target=\"_top\">{1}</a>";

            //绑定概要信息
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<li><span>【英文名称】</span>{0}</li>", info.ENName.Trim().Length == 0 ? "未知" : info.ENName);
            sb.AppendFormat("<li><span>【词条作者】</span>{0}</li>", info.Author.Trim().Length == 0 ? "未知" : info.Author);
            sb.AppendFormat("<li><span>【知识来源】 </span>{0}</li>", source.Length == 0 ? "未知" : string.Format(strFormat, parentDoi, source));
            //keyword = DealKeyWord(info.Smartstr);
            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
            //    sb.AppendFormat("<li><span>【关 键 词】</span>{0}</li>", keyword);
            //}

            lt_summary.Text = sb.ToString();

            //记录日志
            if (!IsPostBack)
            {
                Log logBll = new Log();
                logBll.Add(DataBaseType.ENTRYDATA, LogType.BROWSE, doi, title, "浏览词条");
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


            if (info.Sys_fld_HasPartContent == 0)
            {
                BindContent(strXml);
            }
            else
            {
                //绑定内容
                BindCataLog(strXml);
            }

            //显示数据
            haveResult.Visible = true;
            noResult.Visible = false;
        }

        /// <summary>
        /// 绑定正文
        /// </summary>
        /// <param name="content"></param>
        private void BindContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            //Methods method = new Methods();
            //判断，如果没登陆，则显示一部分
            if (string.IsNullOrEmpty(userNameLogin))
            {
                lt_Digest.Text = xmlParaTohtml(content, 1);
                BindPic(content, false);
            }
            else
            {
                //显示全部
                lt_Digest.Text = xmlParaTohtml(content, 0);
            }
        }

        /// <summary>
        /// 绑定XML
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
            catch(Exception ex)
            {
                lt_Digest.Text = "转换xml出错 详细信息为：" + ex.Message;
                return;
            }

            //Methods method = new Methods();
            //判断，如果没登陆，则显示一部分
            if (string.IsNullOrEmpty(userNameLogin))
            {
                lt_Digest.Text = xmlTOhtml(xd);
                //lt_Digest.Text = xmlPartTohtml(xd);
                //BindPic(strXml, true);
            }
            else
            {
                //显示全部
                lt_Digest.Text = xmlTOhtml(xd);
            }


        }

        /// <summary>
        /// 绑定未购买时显示的图片
        /// </summary>
        /// <param name="strXml">xml字符串</param>
        /// <param name="hasPart">是否包含partcontent</param>
        private void BindPic(string strXml, bool hasPart)
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
            XmlNode entry = xd.SelectSingleNode("entry");
            if (entry == null)
            {
                return;
            }
            List<string> imgList = new List<string>();
            XmlNode content = entry.SelectSingleNode("content");
            if (content == null)
            {
                return;
            }
            if (!hasPart)
            {
                XmlNodeList para = content.SelectNodes("para");
                StringBuilder paratext = new StringBuilder();

                if (para != null && para.Count != 0)
                {
                    int showPara = para.Count;
                    for (int i = 0; i < showPara; i++)
                    {
                        foreach (XmlNode node in para[i].ChildNodes)
                        {
                            if (node.Name == "mediaobject")
                            {
                                imgList.Add(GoodPic(para[i]));
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < content.ChildNodes.Count; i++)
                {
                    if (content.ChildNodes[i].Name == "para")
                    {
                        foreach (XmlNode node in content.ChildNodes[i].ChildNodes)
                        {
                            if (node.Name == "mediaobject")
                            {
                                imgList.Add(GoodPic(content.ChildNodes[i]));
                            }
                        }
                    }
                    else
                    {
                        if (content.ChildNodes[i].Name == "partcontent")
                        {
                            XmlNodeList paras = content.ChildNodes[i].SelectNodes("para");
                            if (paras != null && paras.Count != 0)
                            {
                                for (int j = 0; j < paras.Count; j++)
                                {
                                    foreach (XmlNode node in paras[j].ChildNodes)
                                    {
                                        if (node.Name == "mediaobject")
                                        {
                                            imgList.Add(GoodPic(paras[j]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //根据imgList中的数据，生成html
            if (imgList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<h2 style='margin-top:20px;'>图片精选</h2><div class='spic'>");
                for (int i = 0; i < imgList.Count && i < 4; i++)
                {
                    sb.Append("<a href='javascript:void(0)'>");
                    sb.Append(imgList[i]);
                    sb.Append("</a>");
                }
                sb.Append("</div>");
                lt_Pic.Text = sb.ToString();
            }
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
        /// 根据XML拼接为HTML，全部显示
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string xmlTOhtml(XmlDocument xml)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                XmlNode entry = xml.SelectSingleNode("entry");
                XmlNode content = entry.SelectSingleNode("content");
                for (int i = 0; i < content.ChildNodes.Count; i++)
                {
                    if (content.ChildNodes[i].Name == "para")
                    {
                        str.AppendFormat("<p class=\"para\">{0}</p>", DealAllObject(content.ChildNodes[i]));

                    }
                    else
                    {
                        if (content.ChildNodes[i].Name == "partcontent")
                        {
                            if (content.ChildNodes[i].FirstChild != null)
                            {
                                str.AppendFormat("<p class=\"sepcail\">{0}</p>", content.ChildNodes[i].FirstChild.InnerText);
                            }
                            //XmlNodeList paras = content.ChildNodes[i].SelectNodes("para");
                            //if (paras != null && paras.Count != 0)
                            //{
                            //    for (int j = 0; j < paras.Count; j++)
                            //    {
                            //        str.AppendFormat("<p class=\"para\">{0}</p>", DealAllObject(paras[j]));
                            //    }
                            //}
                            XmlNodeList paras = content.ChildNodes[i].ChildNodes;
                            if (paras != null && paras.Count != 0)
                            {

                                for (int j = 1; j < paras.Count; j++)
                                {
                                    if (paras[j].Name == "itemizedlist")
                                    {
                                        StringBuilder strList = new StringBuilder();
                                        strList.Append("<ul class=\"ullist\">");
                                        strList.Append(paras[j].Attributes["role"].InnerText);
                                        if (paras[j].ChildNodes.Count > 0)
                                        {
                                            for (int z = 0; z < paras[j].ChildNodes.Count; z++)
                                            {
                                                strList.Append("<li class=\"lilist\">");
                                                strList.Append(paras[j].ChildNodes[z].Attributes["role"].InnerText + "  " + Utility.XmlToHtml.DealAllObject(paras[j].ChildNodes[z]));
                                                strList.Append("</li>");
                                            }
                                        }

                                        strList.Append("</ul>");
                                        str.Append(strList);
                                    }
                                    else
                                    {
                                        str.AppendFormat("<p class=\"para\">{0}</p>", DealAllObject(paras[j]));
                                    }
                                }

                            }
                        }
                    }
                }

            }
            //  }
            catch(Exception ex)
            {
                lt_Digest.Text = "转换xml出错 详细信息为：" + ex.Message;
            }
            return str.ToString();
        }

        /// <summary>
        /// 根据XML拼接为HTML，部分显示
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string xmlPartTohtml(XmlDocument xml)
        {
            //string htmlStr = "";
            StringBuilder str = new StringBuilder();

            try
            {
                XmlNode entry = xml.SelectSingleNode("entry");
                XmlNode content = entry.SelectSingleNode("content");

                bool partContent = false;
                for (int i = 0; i < content.ChildNodes.Count; i++)
                {
                    if (content.ChildNodes[i].Name == "para")
                    {
                        if (!partContent)
                        {
                            str.AppendFormat("<p class=\"para\">{0}</p>", DealPic(content.ChildNodes[i]));
                            partContent = true;
                            break;
                        }
                    }
                    else
                    {

                        if (content.ChildNodes[i].Name == "partcontent")
                        {
                            if (content.ChildNodes[i].FirstChild != null)
                            {
                                str.AppendFormat("<p class=\"sepcail\">{0}</p>", content.ChildNodes[i].FirstChild.InnerText);
                            }
                            XmlNodeList paras = content.ChildNodes[i].SelectNodes("para");
                            if (paras != null && paras.Count != 0)
                            {
                                for (int j = 0; j < paras.Count; j++)
                                {
                                    if (!partContent)
                                    {
                                        str.AppendFormat("<p class=\"para\">{0}</p>", DealAllObject(paras[j]));
                                        partContent = true;
                                        break;
                                    }
                                }
                            }
                            if (partContent)
                            {
                                break;
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                str.Append(ex.Message);
            }

            return str.ToString();
        }

        /// <summary>
        /// 出去空白
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetStrNoEmpty(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", "");
                //str = str.Replace("+", "");
                return str;
            }
        }


        /// <summary>
        /// xml段落转换成html
        /// </summary>
        /// <param name="xmlPart">如果为0就是全部 </param>
        /// <param name="paraCount"></param>
        /// <returns></returns>
        private string xmlParaTohtml(string xmlPart, int paraCount)
        {
            if (string.IsNullOrEmpty(xmlPart))
            {
                return string.Empty;
            }
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(xmlPart);
            }
            catch(Exception ex)
            {
                lt_Digest.Text = "转换xml出错 详细信息为：" + ex.Message;
            }
            XmlNode entry = xd.SelectSingleNode("entry");
            if (entry == null)
            {
                return string.Empty;
            }
            XmlNode content = entry.SelectSingleNode("content");
            if (content == null)
            {
                return string.Empty;
            }
            XmlNodeList para = content.SelectNodes("para");
            StringBuilder paratext = new StringBuilder();

            if (para != null && para.Count != 0)
            {
                int showPara = para.Count;
                if (paraCount > 0)
                {
                    showPara = paraCount;
                }
                for (int i = 0; i < showPara; i++)
                {
                    paratext.AppendFormat("<p>{0}</p>", DealAllObject(para[i]));
                }
            }
            return paratext.ToString();
        }

        /// <summary>
        /// 处理段落里的图片
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string DealPic(XmlNode node)
        {
            if (node == null)
            {
                return string.Empty;
            }
            //  XmlNodeList mylist = node.SelectNodes("mediaobject");
            string picFormat = "<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" onload=\"SetImgAutoSize(this, 600, 400);\"/>";
            //if (mylist == null || mylist.Count == 0)
            //{
            //    return node.InnerText;
            //}
            //for (int i = 0; i < 1; i++)
            //{
            XmlNode pic = node.SelectSingleNode("imageobject/imagedata");
            string tempresult = string.Empty;
            if (pic != null)
            {
                XmlNode pictitle = pic.SelectSingleNode("title");
                string title = pictitle == null ? string.Empty : pictitle.InnerText;
                // title = title.Replace("<", "").Replace(">", "");
                string key = string.Empty;
                if (pic.Attributes["fileref"] != null)
                {
                    key = HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(pic.Attributes["fileref"].Value));
                }
                //if (string.IsNullOrWhiteSpace(key))
                //{
                //    node.InnerXml = string.Empty;
                //}
                //else
                //{
                //    node.InnerXml = string.Format(picFormat, key, title);
                //}
                tempresult = string.Format(picFormat, key, Utility.XmlToHtml.DealNoClearCDATA(title));
            }
            //   }
            if (!string.IsNullOrEmpty(tempresult))
            {
                tempresult = tempresult.Replace("<mediaobject>", "");
                tempresult = tempresult.Replace("</mediaobject>", "");
            }
            return tempresult;
        }
        /// <summary>
        /// 处理段落里的图片
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string GoodPic(XmlNode node)
        {
            if (node == null)
            {
                return string.Empty;
            }
            XmlNodeList mylist = node.SelectNodes("mediaobject");
            string picFormat = "<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" onclick=\"seeBigPic('getpicdata.aspx?key={0}','{1}')\" onload=\"SetImgAutoSize(this,155,155,true)\"/>";
            if (mylist == null || mylist.Count == 0)
            {
                return node.InnerText;
            }
            for (int i = 0; i < mylist.Count; i++)
            {
                XmlNode pic = mylist[i].SelectSingleNode("imageobject/imagedata");
                XmlNode pictitle = pic.SelectSingleNode("title");
                string title = pictitle == null ? string.Empty : pictitle.InnerText;
                string key = string.Empty;
                if (pic.Attributes["fileref"] != null)
                {
                    key = HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(pic.Attributes["fileref"].Value));
                }
                if (string.IsNullOrWhiteSpace(key))
                {
                    mylist[i].ParentNode.InnerXml = string.Empty;
                }
                else
                {
                    mylist[i].ParentNode.InnerXml = string.Format(picFormat, key, title);
                }
            }

            return node.InnerXml;
        }

        /// <summary>
        /// 处理锚点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string DealAnchord(XmlNode node)
        {
            string result = string.Empty;
            string anchorid = string.Empty;
            if (node.Attributes["anchorid"] != null)
            {
                anchorid = node.Attributes["anchorid"].Value;
            }
            if (!string.IsNullOrWhiteSpace(anchorid))
            {
                result = "<span id=\"" + anchorid + "\" >" + node.InnerText + "</span>";
            }
            return result;
        }

        /// <summary>
        /// 处理段落里的节点内容
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string DealAllObject(XmlNode node)
        {

            StringBuilder strNote = new StringBuilder();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                switch (node.ChildNodes[i].Name)
                {
                    case "anchor":
                        {
                            //处理注释
                            strNote.Append(DealAnchord(node.ChildNodes[i]));
                        }
                        break;
                    case "mediaobject":
                        {
                            //处理图片
                            strNote.Append(DealPic(node.ChildNodes[i]));
                        }
                        break;
                    case "subscript":
                        {
                            //处理下标
                            strNote.Append("<sub>" + node.ChildNodes[i].InnerText + "</sub>");
                        }
                        break;
                    case "superscript":
                        {
                            //处理上标
                            strNote.Append("<sup>" + node.ChildNodes[i].InnerText + "</sup>");
                        }
                        break;
                    case "note":
                        {
                            //处理注释
                            if (node.ChildNodes[i].HasChildNodes)
                            {
                                strNote.Append("<img src='../images/notepic.png' class='notetitle'/>");
                                strNote.Append("<span class=\"divnote\">");
                                for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                                {
                                    strNote.AppendFormat("<span class=\"notepara\">{0}</span>", DealPic(node.ChildNodes[i].ChildNodes[j]));
                                }
                                strNote.Append("</span>");
                            }
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-section":
                        {
                            strNote.Append(Utility.XmlToHtml.DealCdataStr(node.ChildNodes[i].InnerText));
                        }
                        break;
                }
            }

            return strNote.ToString();

        }


        /// <summary>
        /// 处理关键词
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private string DealKeyWord(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return string.Empty;
            }
            string result = string.Empty;
            string[] keyarr = keyword.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in keyarr)
            {
                if (s.IndexOf(",") > 0)
                {
                    string[] temparr = s.Split(',');
                    if (temparr.Length == 2)
                    {
                        result = result + "<a href=\"#" + temparr[1] + "\" class=\"keyworda\" targetid=\"" + temparr[1] + "\">" + temparr[0] + "</a>  ";
                    }
                }
            }

            return result;
        }
    }
}