using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.IO;

using DRMS.BLL;
using DRMS.Model;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.Utility
{
    public abstract class UtilMngSys
    {        
            private static Regex RegPhone = new Regex(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");
            private static Regex RegNumber = new Regex("^[0-9]+$");//包含0
            private static Regex RegNumberD = new Regex("^[1-9][0-9]*$");
            private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
            private static Regex RegIP = new Regex(@"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            private static Regex RegUserName = new Regex(@"^[a-zA-Z][a-zA-Z0-9_]{1,15}$");

            /// <summary>
            /// 获取最大试读的页数
            /// </summary>
            /// <returns></returns>
            public static string GetTryReadCount()
            {
                return System.Configuration.ConfigurationManager.AppSettings["tryreadPage"];
            }

            /// <summary>
            /// 手机和座机电话格式
            /// </summary>
            /// <param name="strPhone"></param>
            /// <returns></returns>
            public static bool IsPhpne(string strPhone)
            {
                return RegPhone.Match(strPhone).Success;

            }

            /// <summary>
            /// 是否是数字包含0
            /// </summary>
            /// <param name="strNumber"></param>
            /// <returns></returns>
            public static bool IsNumber(string strNumber)
            {
                return RegNumber.Match(strNumber).Success;
            }

            /// <summary>
            /// 是否是数字，不包含0
            /// </summary>
            /// <param name="strNumber"></param>
            /// <returns></returns>
            public static bool IsNuberExcept0(string strNumber)
            {
                return RegNumberD.Match(strNumber).Success;
            }

            /// <summary>
            /// 是否是IP
            /// </summary>
            /// <param name="strNumber"></param>
            /// <returns></returns>
            public static bool IsIP(string strIP)
            {
                return RegIP.Match(strIP).Success;
            }

            /// <summary>
            /// 是否是email
            /// </summary>
            /// <param name="strNumber"></param>
            /// <returns></returns>
            public static bool IsEmail(string strEmail)
            {
                return RegEmail.Match(strEmail).Success;
            }

            /// <summary>
            /// 用户名，8-16位，以字母开头，由字母数字和下划线组成
            /// </summary>
            /// <param name="strNumber"></param>
            /// <returns></returns>
            public static bool IsUserName(string strUserName)
            {
                return RegUserName.Match(strUserName).Success;
            }

            /// <summary>
            /// 数字转换成ip
            /// </summary>
            /// <param name="ipCode"></param>
            /// <returns></returns>
            public static string Int2IP(UInt32 ipCode)
            {
                byte a = (byte)((ipCode & 0xFF000000) >> 0x18);
                byte b = (byte)((ipCode & 0x00FF0000) >> 0x10);
                byte c = (byte)((ipCode & 0x0000FF00) >> 0x8);
                byte d = (byte)(ipCode & 0x000000FF);
                string ipStr = String.Format("{0}.{1}.{2}.{3}", a, b, c, d);
                return ipStr;
            }
            /// <summary>
            /// ip转换成数字
            /// </summary>
            /// <param name="ipStr"></param>
            /// <returns></returns>
            public static UInt32 IP2Int(string ipStr)
            {
                try
                {
                    if (ipStr.ToLower().Equals("localhost"))
                    {
                        ipStr = "127.0.0.1";
                    }
                    string[] ip = ipStr.Split('.');
                    uint ipCode = 0xFFFFFF00 | byte.Parse(ip[3]);
                    ipCode = ipCode & 0xFFFF00FF | (uint.Parse(ip[2]) << 0x8);
                    ipCode = ipCode & 0xFF00FFFF | (uint.Parse(ip[1]) << 0x10);
                    ipCode = ipCode & 0x00FFFFFF | (uint.Parse(ip[0]) << 0x18);
                    return ipCode;
                }
                catch
                {
                    return 0;
                }
            }


            /// <summary>
            /// 替换采集数据中正文的图片路径
            /// </summary>
            /// <param name="html"></param>
            /// <returns></returns>
            public static string ReplacePicUrl(string html)
            {
                return Regex.Replace(html, @"\{(?<id>[^\{\}\.]*)(?<type>\.[^\{\}\.]*)\}", new MatchEvaluator(picurl), RegexOptions.IgnoreCase);
            }

            /// <summary>
            /// 替换具体的图片路径
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            private static string picurl(Match m)
            {
                string filetype = m.Groups["type"].Value.ToLower();
                if (filetype == ".jpg" || filetype == ".gif" || filetype == ".jpeg" || filetype == ".png" || filetype == ".bmp")
                {
                    return "<img src=\"Showpic.aspx?key=" + m.Groups["id"].Value + "&type=" + m.Groups["type"].Value + "\" border=\"0\">";
                }
                else
                {
                    return "<a href=\"Showpic.aspx?key=" + m.Groups["id"].Value + "&type=" + m.Groups["type"].Value + "\" target=\"blank\" class=\"more\">文件下载</a>";
                }
            }

            /// <summary>
            /// 根据字符串和类型，创建xml
            /// </summary>
            /// <param name="doiStr">字符串，‘；’分开</param>
            /// <param name="type">类型</param>
            /// <returns></returns>
            public static string CreatXML(string doiStr, string type)
            {
                XDocument docnew = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                XElement itemlist = new XElement("ItemList", new XAttribute("type", type));

                if (doiStr.Contains(";")) // 批量
                {
                    IList<string> keyList = doiStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < keyList.Count; i++)
                    {
                        XElement item = new XElement("Item", new XAttribute("Guid", keyList[i]));
                        itemlist.Add(item);
                    }
                    docnew.Add(itemlist);
                }
                else // 单个
                {
                    XElement item = new XElement("Item", new XAttribute("Guid", doiStr));
                    itemlist.Add(item);
                    docnew.Add(itemlist);
                }
                return docnew.ToString();
            }
            /// <summary>
            /// 根据资源类型获取其细览页地址
            /// </summary>
            /// <returns></returns>
            public static string GetDetailPage(DataBaseType dbType)
            {
                string detailUrl = "";
                switch (dbType)
                {
                    case DataBaseType.BOOKTDATA:
                    case DataBaseType.REFERENCEBOOK:
                        detailUrl = "/Page/BookDetail.aspx?doi={0}";
                        break;
                    case DataBaseType.CRITERION:
                        detailUrl = "/Page/BookDetail.aspx?doi={0}&type=1";
                        break;
                    case DataBaseType.ENTRYDATA:
                        detailUrl = "/Page/ReferencesubDetail.aspx?doi={0}";
                        break;
                    case DataBaseType.BOOKCHAPTER:
                        detailUrl = "/Page/CharpterReade.aspx?doi={0}&type=9";
                        break;
                    case DataBaseType.STDDATACHAPTER:
                        detailUrl = "/Page/CharpterReade.aspx?doi={0}&type=10";
                        break;
                    case DataBaseType.CONFERENCEPAPER:
                        detailUrl = "/Page/ConferencePaperDetail.aspx?doi={0}&type=5";
                        break;
                    case DataBaseType.CONFERENCEARTICLE:
                        detailUrl = "/Page/ArticleDetail.aspx?doi={0}&type=19";
                        break;
                }
                return detailUrl;
            }

            /// <summary>
            /// 清理特殊标记
            /// </summary>
            /// <param name="title"></param>
            /// <returns></returns>
            public static string ClearTitle(string title)
            {
                if (string.IsNullOrEmpty(title))
                {
                    return string.Empty;
                }
                title = NormalFunction.ResetRedFlag(title);
                title = NormalFunction.ReplaceLabel(title);
                title = ReplacePicUrlToBlank(title);
                return title;
            }

            /// <summary>
            /// 处理带有note的title
            /// </summary>
            /// <param name="note"></param>
            /// <param name="partTitle">note前的内容</param>
            /// <returns>note转换成的div</returns>
            public static string DealNoteTitle(string note)
            {
                if (string.IsNullOrEmpty(note))
                {
                    return "";
                }

                //将note处理成一个 title 加一个 
                StringBuilder strNote = new StringBuilder();

                XmlDocument doc = new XmlDocument();

                doc.LoadXml("<book>" + note + "</book>");
                XmlNode node = doc.SelectSingleNode("book");
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes[i].Name == "note")
                    {
                        //处理注释
                        if (node.ChildNodes[i].HasChildNodes)
                        {
                            strNote.Append("<span class=\"divnote\">");
                            for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                            {
                                strNote.AppendFormat("<p class=\"notepara\">{0}</p>", DealPic(node.ChildNodes[i].ChildNodes[j]));
                            }
                            strNote.Append("</span>");
                        }
                    }
                }

                return strNote.ToString();
            }

            /// <summary>
            /// 处理段落里的图片
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public static string DealPic(XmlNode node)
            {
                if (node == null)
                {
                    return string.Empty;
                }
                XmlNodeList mylist = node.SelectNodes("mediaobject");
                string picFormat = "<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" onload=\"SetImgAutoSize(this, 600, 400);\"></img>";
                if (mylist == null || mylist.Count == 0)
                {
                    return CNKI.BaseFunction.NormalFunction.ReplaceRed(node.InnerText);
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
            /// 处理上下脚标
            /// </summary>
            /// <param name="content"></param>
            /// <returns></returns>
            public static string DealFootScript(string content)
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    return string.Empty;
                }
                string result = string.Empty;
                content = content.Replace("subscript", "sub");
                content = content.Replace("superscript", "sup");
                return content;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="html"></param>
            /// <returns></returns>
            public static string ReplacePicUrlToBlank(string html)
            {
                if (string.IsNullOrEmpty(html))
                {
                    return string.Empty;
                }
                return Regex.Replace(html, @"\{(?<id>[^\{\}\.]*)(?<type>\.[^\{\}\.]*)\}", "", RegexOptions.IgnoreCase);
            }
            /// <summary>
            /// 替换采集数据中正文的图片路径
            /// </summary>
            /// <param name="html"></param>
            /// <returns></returns>
            public static string ReplacePicUrlForBook(string html)
            {
                return Regex.Replace(html, @"\{(?<id>[^\{\}\.]*)(?<type>\.[^\{\}\.]*)\}", new MatchEvaluator(picurlforbook), RegexOptions.IgnoreCase);
            }

            /// <summary>
            /// 替换具体的图片路径
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            private static string picurlforbook(Match m)
            {
                string filetype = m.Groups["type"].Value.ToLower();
                if (filetype == ".jpg" || filetype == ".gif" || filetype == ".jpeg" || filetype == ".png" || filetype == ".bmp")
                {
                    return "<img src=\"getpicdata.aspx?key=" + m.Groups["id"].Value + "&type=" + m.Groups["type"].Value + "\" border=\"0\" height=\"21px\">";
                }
                else
                {
                    return "<a href=\"Showpic.aspx?key=" + m.Groups["id"].Value + "&type=" + m.Groups["type"].Value + "\" target=\"blank\" class=\"more\">文件下载</a>";
                }
            }
            /// <summary>
            /// 切词
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string GetWord(string str)
            {
                return Book.GetWord(str);
            }


        
    }
}