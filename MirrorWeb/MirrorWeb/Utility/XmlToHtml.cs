using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace DRMS.MirrorWeb.Utility
{
    /// <summary>
    /// 处理将现有的xml 章节 内容处理成可以展示的html
    /// </summary>
    public abstract class XmlToHtml
    {
        /// <summary>
        /// 处理带有note的title
        /// </summary>
        /// <param name="note"></param>
        /// <param name="partTitle">note前的内容</param>
        /// <returns>note转换成的div</returns>
        public static string DealNoteTitle(string note)
        {

            //将note处理成一个 title 加一个 
            string partTitle = string.Empty;
            StringBuilder strNote = new StringBuilder();

            XmlDocument doc = new XmlDocument();
            if (note.IndexOf("{") > 0)
            {
                note = DealCdataStr(note);
            }
            doc.LoadXml("<book>" + note + "</book>");
            XmlNode node = doc.SelectSingleNode("book");
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                switch (node.ChildNodes[i].Name)
                {
                    case "note":
                        {
                            //处理注释
                            if (node.ChildNodes[i].HasChildNodes)
                            {
                                strNote.Append("<img src='../images/notepic.png' class='notetitle'/>");
                                strNote.Append("<span class=\"divnote\">");
                                for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                                {
                                    strNote.AppendFormat("<span class=\"notepara\">{0}</span>", DealAllObject(node.ChildNodes[i].ChildNodes[j]));
                                }
                                strNote.Append("</span>");
                            }
                        }
                        break;
                    case "mediaobject":
                        {
                            //处理图片
                            strNote.Append(DealTitlePic(node.ChildNodes[i]));
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
                    case "italic":
                        {
                            //处理斜体
                            strNote.Append("<i>" + node.ChildNodes[i].InnerText + "</i>");
                        }
                        break;
                    case "bold":
                        {
                            //处理黑体
                            strNote.Append("<b>" + node.ChildNodes[i].InnerText + "</b>");
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
                            strNote.Append(DealNoClearCDATA(DealCdataStr(node.ChildNodes[i].InnerText)));
                        }
                        break;
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
            string picFormat = "<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" {2} onload=\"SetImgAutoSize(this, 600, 400);\" /><span style=\"width: 100%; display: block;\">{1}</span>";
            string inlinepicFormat = "<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" {2} onload=\"SetImgAutoSize(this, 600, 400);\"/>";
            string tableFormat = "<span style=\"width: 100%; display: block;\">{1}</span><img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" {2} onload=\"SetImgAutoSize(this, 600, 400);\" />{3}";
            string paraDesc = "<span style=\"width: 100%; display: block;\" class=\"tablenote\">{0}</span>";
            string paraPicDesc = "<span style=\"width: 100%; display: block;\" class=\"picnote\">{0}</span>";
            #region 注释
            //if (mylist == null || mylist.Count == 0)
            //{
            //    return node.InnerText;
            //}
            //for (int i = 0; i < mylist.Count; i++)
            //{
            //    XmlNode pic = mylist[i].SelectSingleNode("imageobject/imagedata");
            //    XmlNode pictitle = pic.SelectSingleNode("title");
            //    string title = pictitle == null ? string.Empty : pictitle.InnerText;
            //    string key = string.Empty;
            //    if (pic.Attributes["fileref"] != null)
            //    {
            //        key = HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(pic.Attributes["fileref"].Value));
            //    }
            //    if (string.IsNullOrWhiteSpace(key))
            //    {
            //        if (mylist[i].ParentNode != null)
            //        {
            //            mylist[i].ParentNode.InnerXml = string.Empty;
            //        }
            //    }
            //    else
            //    {
            //        if (mylist[i].ParentNode != null)
            //        {
            //            mylist[i].ParentNode.InnerXml = string.Format(picFormat, key, title);
            //        }
            //    }
            //}
            //return node.InnerXml;
            #endregion
            XmlNode pic = node.SelectSingleNode("imageobject/imagedata");
            string tempresult = string.Empty;
            if (pic != null)
            {
                XmlNode pictitle = pic.SelectSingleNode("title");
                string title = pictitle == null ? string.Empty : pictitle.InnerText;
                string key = string.Empty;
                string role = pic.Attributes["role"] == null ? string.Empty : pic.Attributes["role"].Value;
                string roleClass = string.Empty;

                if (pic.Attributes["fileref"] != null)
                {
                    key = HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(pic.Attributes["fileref"].Value));
                }
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "3")
                    {
                        //说明是 行内图
                        roleClass = "class=\"inlinepic\"";
                    }
                }
                if (role == "3")
                {
                    tempresult = string.Format(inlinepicFormat, key, DealNoClearCDATA(title), roleClass);
                }
                if (role == "2")
                {
                    //表格
                    StringBuilder parastr = new StringBuilder();
                    XmlNodeList picdesclist = pic.SelectNodes("picdesc/para");
                    if (picdesclist != null)
                    {
                        for (int i = 0; i < picdesclist.Count; i++)
                        {
                            parastr.AppendFormat(paraDesc, picdesclist[i].InnerText);
                        }
                    }
                    tempresult = string.Format(tableFormat, key, DealNoClearCDATA(title), roleClass, parastr.ToString());
                }
                else
                {
                    if (role == "0")
                    {
                        StringBuilder parastr = new StringBuilder();
                        XmlNodeList picdesclist = pic.SelectNodes("picdesc/para");
                        if (picdesclist != null)
                        {
                            for (int i = 0; i < picdesclist.Count; i++)
                            {
                                parastr.AppendFormat(paraPicDesc, picdesclist[i].InnerText);
                            }
                        }
                        tempresult = string.Format(picFormat, key, DealNoClearCDATA(title), roleClass) + parastr.ToString();
                    }
                    else
                    {
                        tempresult = string.Format(picFormat, key, DealNoClearCDATA(title), roleClass);
                    }

                }
            }
            if (!string.IsNullOrEmpty(tempresult))
            {
                tempresult = tempresult.Replace("<mediaobject>", "");
                tempresult = tempresult.Replace("</mediaobject>", "");
            }
            return tempresult;
        }

        /// <summary>
        /// 处理标题里的图片
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string DealTitlePic(XmlNode node)
        {
            if (node == null)
            {
                return string.Empty;
            }
            XmlNodeList mylist = node.SelectNodes("mediaobject");
            string picFormat = "<img src=\"getpicdata.aspx?key={0}\" title=\"{1}\" height=\"21px\"></img>";
            //if (mylist == null || mylist.Count == 0)
            //{
            //    return node.InnerText;
            //}
            //for (int i = 0; i < mylist.Count; i++)
            //{
            //    XmlNode pic = mylist[i].SelectSingleNode("imageobject/imagedata");
            //    XmlNode pictitle = pic.SelectSingleNode("title");
            //    string title = pictitle == null ? string.Empty : pictitle.InnerText;
            //    string key = string.Empty;
            //    if (pic.Attributes["fileref"] != null)
            //    {
            //        key = HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(pic.Attributes["fileref"].Value));
            //    }
            //    if (string.IsNullOrWhiteSpace(key))
            //    {
            //        if (mylist[i].ParentNode != null)
            //        {
            //            mylist[i].ParentNode.InnerXml = string.Empty;
            //        }
            //    }
            //    else
            //    {
            //        if (mylist[i].ParentNode != null)
            //        {
            //            mylist[i].ParentNode.InnerXml = string.Format(picFormat, key, title);
            //        }
            //    }
            //}
            //return node.InnerXml;
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
                tempresult = string.Format(picFormat, key, DealNoClearCDATA(title));
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
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ClearCDATA(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            input = Regex.Replace(input, @"<!\[CDATA\[|\]\]>", "", RegexOptions.IgnoreCase);
            return input;
        }
        /// <summary>
        /// 处理段落里的节点内容
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string DealAllObject(XmlNode node)
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
                    case "italic":
                        {
                            //处理斜体
                            strNote.Append("<i>" + node.ChildNodes[i].InnerText + "</i>");
                        }
                        break;
                    case "bold":
                        {
                            //处理黑体
                            strNote.Append("<b>" + node.ChildNodes[i].InnerText + "</b>");
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
                                    strNote.AppendFormat("<span class=\"notepara\">{0}</span>", DealAllObject(node.ChildNodes[i].ChildNodes[j]));
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
                            strNote.Append(DealNoClearCDATA(node.ChildNodes[i].InnerText));
                        }
                        break;
                }
            }

            return strNote.ToString();

        }

        /// <summary>
        /// 处理篇
        /// </summary>
        /// <param name="node"></param>
        /// <param name="showParaCount">显示段落的数目</param>
        /// <returns></returns>
        public static string DealXml(XmlNode node, int showParaCount, bool displayTitle = true)
        {
            StringBuilder strNote = new StringBuilder();
            int paraCount = 0;
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                switch (node.ChildNodes[i].Name)
                {
                    case "title":
                        {
                            //处理注释
                            if (displayTitle)
                            {
                                strNote.Append("<p>" + DealNoteTitle(node.ChildNodes[i].InnerXml) + "</p>");
                            }
                        }
                        break;
                    case "subtitle":
                        {
                            strNote.Append("<p>" + DealNoteTitle(node.ChildNodes[i].InnerXml) + "</p>");
                        }
                        break;
                    case "etitle":
                        {
                            //处理注释
                            if (displayTitle)
                            {
                                strNote.Append("<p>" + DealNoteTitle(node.ChildNodes[i].InnerXml) + "</p>");
                            }
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
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "chapter":
                        {
                            //处理章
                            strNote.Append(DealXml(node.ChildNodes[i], showParaCount));
                        }
                        break;
                    case "part":
                        {
                            //处理章
                            strNote.Append(DealXml(node.ChildNodes[i], showParaCount));
                        }
                        break;
                    case "section":
                        {
                            //处理节
                            strNote.Append(DealXml(node.ChildNodes[i], showParaCount));
                        }
                        break;
                    case "para":
                        {
                            //处理段落
                            strNote.Append("<p>" + DealAllObject(node.ChildNodes[i]) + "</p>");
                            paraCount++;
                        }
                        break;
                    case "article":
                        {
                            //处文章
                            strNote.Append(DealXml(node.ChildNodes[i], showParaCount, displayTitle));
                        }
                        break;
                    case "info":
                        {
                            //处里基本信息
                            strNote.Append(DealInfo(node.ChildNodes[i]));
                        }
                        break;
                    case "bibliography":
                        {
                            //处理参考文献
                            strNote.Append(DealBibliography(node.ChildNodes[i]));
                        }
                        break;
                    case "exerciseset":
                        {
                            //处理习题集
                            strNote.Append(DealExerciseset(node.ChildNodes[i]));
                        }
                        break;
                    case "exerciseentry":
                        {
                            //处理习题集
                            //  strNote.Append(DealExerciseset(node.ChildNodes[i]));
                            strNote.Append(DealExercise(node.ChildNodes[i]));
                        }
                        break;
                    case "theorem":
                        {
                            //处理习题集
                            strNote.Append(DealTheorem(node.ChildNodes[i]));
                        }
                        break;
                    case "clause":
                        {
                            //条文说明
                            strNote.Append("<p class=\"clause\">显示条文说明</p>");
                            strNote.Append("<div class=\"clausediv\">");
                            strNote.Append(DealXml(node.ChildNodes[i], showParaCount));
                            strNote.Append("</div>");
                        }
                        break;
                    case "interpretation":
                        {
                            //条文解读
                            strNote.Append("<p class=\"interpretation\">显示条文解读</p>");
                            strNote.Append("<div class=\"interpretationdiv\">");
                            strNote.Append(DealXml(node.ChildNodes[i], showParaCount));
                            strNote.Append("</div>");
                        }
                        break;
                }
                if (showParaCount > 0 && paraCount >= showParaCount)
                {
                    break;
                }
            }
            return strNote.ToString();
        }

        /// <summary>
        /// 处理cdata内容
        /// </summary>
        /// <param name="cdatastr"></param>
        /// <returns></returns>
        public static string DealCdataStr(string cdatastr)
        {

            string partTitle = string.Empty;
            StringBuilder strNote = new StringBuilder();

            //XmlDocument doc = new XmlDocument();

            //doc.LoadXml("<book>" + cdatastr + "</book>");
            //XmlNode node = doc.SelectSingleNode("book");
            //for (int i = 0; i < node.ChildNodes.Count; i++)
            //{
            //    switch (node.ChildNodes[i].Name)
            //    {
            //        case "anchor":
            //            {
            //                //处理注释
            //                strNote.Append(DealAnchord(node.ChildNodes[i]));
            //            }
            //            break;
            //        case "mediaobject":
            //            {
            //                //处理图片
            //                strNote.Append(DealPic(node.ChildNodes[i].ParentNode));
            //            }
            //            break;
            //        case "subscript":
            //            {
            //                //处理下标
            //                strNote.Append("<sub>" + node.ChildNodes[i].InnerText + "</sub>");
            //            }
            //            break;
            //        case "superscript":
            //            {
            //                //处理上标
            //                strNote.Append("<sup>" + node.ChildNodes[i].InnerText + "</sup>");
            //            }
            //            break;
            //        case "#text":
            //            {
            //                //处理文本
            //                strNote.Append(node.ChildNodes[i].InnerText);
            //            }
            //            break;
            //    }
            //}
            //cdatastr = cdatastr.Replace("subscript", "sub");
            //cdatastr = cdatastr.Replace("superscript", "sup");
            cdatastr = Utility.ReplacePicUrlForBook(cdatastr);
            if (cdatastr.IndexOf("<note>") > 0)
            {
                cdatastr = DealNoteTitle(cdatastr);
            }
            return cdatastr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static string DealNoClearCDATA(string xmlstr)
        {
            //将note处理成一个 title 加一个 
            string partTitle = string.Empty;
            StringBuilder strNote = new StringBuilder();

            XmlDocument doc = new XmlDocument();
            if (xmlstr.IndexOf("{") > 0)
            {
                xmlstr = DealCdataStr(xmlstr);
            }
            try
            {
                doc.LoadXml("<book>" + xmlstr + "</book>");
            }
            catch
            {
                xmlstr = xmlstr.Replace("&", "&amp;");
                xmlstr = xmlstr.Replace("<", "&lt;");
                xmlstr = xmlstr.Replace(">", "&gt;");

                doc.LoadXml("<book>" + xmlstr + "</book>");
            }
            XmlNode node = doc.SelectSingleNode("book");
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
                    case "note":
                        {
                            //处理注释
                            if (node.ChildNodes[i].HasChildNodes)
                            {
                                strNote.Append("<img src='../images/notepic.png' class='notetitle'/>");
                                strNote.Append("<span class=\"divnote\">");
                                for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                                {
                                    strNote.AppendFormat("<span class=\"notepara\">{0}</span>", DealAllObject(node.ChildNodes[i].ChildNodes[j]));
                                }
                                strNote.Append("</span>");
                            }
                        }
                        break;
                    case "mediaobject":
                        {
                            //处理图片
                            strNote.Append(DealTitlePic(node.ChildNodes[i]));
                        }
                        break;
                    case "italic":
                        {
                            //处理斜体
                            strNote.Append("<i>" + node.ChildNodes[i].InnerText + "</i>");
                        }
                        break;
                    case "bold":
                        {
                            //处理黑体
                            strNote.Append("<b>" + node.ChildNodes[i].InnerText + "</b>");
                        }
                        break;
                    case "sub":
                        {
                            //处理黑体
                            strNote.Append("<sub>" + node.ChildNodes[i].InnerText + "</sub>");
                        }
                        break;
                    case "sup":
                        {
                            //处理黑体
                            strNote.Append("<sup>" + node.ChildNodes[i].InnerText + "</sup>");
                        }
                        break;
                    case "subscript":
                        {
                            //处理黑体
                            strNote.Append("<sub>" + node.ChildNodes[i].InnerText + "</sub>");
                        }
                        break;
                    case "superscript":
                        {
                            //处理黑体
                            strNote.Append("<sup>" + node.ChildNodes[i].InnerText + "</sup>");
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
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "img":
                        {
                            strNote.Append(node.ChildNodes[i].OuterXml);
                        }
                        break;
                    case "span":
                        {
                            strNote.Append(node.ChildNodes[i].OuterXml);
                        }
                        break;
                }
            }

            return strNote.ToString();
        }

        /// <summary>
        /// 处理段落
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static string DealPara(string xmlstr)
        {
            //将note处理成一个 title 加一个 
            string partTitle = string.Empty;
            StringBuilder strNote = new StringBuilder();

            XmlDocument doc = new XmlDocument();
            if (xmlstr.IndexOf("{") > 0)
            {
                xmlstr = DealCdataStr(xmlstr);
            }
            doc.LoadXml("<book>" + xmlstr + "</book>");
            XmlNode node = doc.SelectSingleNode("book");
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
                    case "mediaobject":
                        {
                            //处理图片
                            strNote.Append(DealTitlePic(node.ChildNodes[i]));
                        }
                        break;
                    case "italic":
                        {
                            //处理斜体
                            strNote.Append("<i>" + node.ChildNodes[i].InnerText + "</i>");
                        }
                        break;
                    case "bold":
                        {
                            //处理黑体
                            strNote.Append("<b>" + node.ChildNodes[i].InnerText + "</b>");
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "para":
                        {
                            //处理文本
                            strNote.Append("<p>" + DealAllObject(node.ChildNodes[i]) + "</p>");
                        }
                        break;
                }
            }

            return strNote.ToString();
        }

        /// <summary>
        /// 处理锚点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealAnchord(XmlNode node)
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
        /// 处理info节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealInfo(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                switch (node.ChildNodes[i].Name)
                {

                    case "authorgroup":
                        {
                            //处理作者
                            strNote.Append(DealAuthorGroup(node.ChildNodes[i]));
                        }
                        break;
                    case "keywordset":
                        {
                            //处理图片
                            strNote.Append(DealKeywordSet(node.ChildNodes[i]));
                        }
                        break;
                    case "abstract":
                        {
                            strNote.Append(DealAbstract(node.ChildNodes[i]));
                        }
                        break;
                    case "superscript":
                        {
                            //处理上标
                            strNote.Append("<sup>" + node.ChildNodes[i].InnerText + "</sup>");
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;

                }

            }
            return strNote.ToString();
        }

        /// <summary>
        /// 处理参考文献
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealBibliography(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                switch (node.ChildNodes[i].Name)
                {

                    case "title":
                        {
                            //处理图片
                            strNote.Append("<p>" + node.ChildNodes[i].InnerText + "</p>");
                        }
                        break;
                    case "biblioentry":
                        {
                            //处理图片
                            strNote.Append(DealBiblioentry(node.ChildNodes[i]));
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                }
            }
            return strNote.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static string DealBiblioentry(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                switch (node.ChildNodes[i].Name)
                {

                    case "biblioset":
                        {
                            //处理图片
                            XmlNode allstrnode = node.ChildNodes[i].SelectSingleNode("allinfoStr");
                            if (allstrnode != null)
                            {
                                strNote.Append("<p>" + allstrnode.InnerText + "</p>");
                            }
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                }

            }
            return strNote.ToString();
        }

        /// <summary>
        /// 处理关键词集
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealKeywordSet(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            string roleValue = node.Attributes["role"].Value;

            switch (roleValue)
            {
                case "zh-Hans":
                    strNote.Append("<p> 关键词：" + DealKeyword(node) + "</p>");
                    break;
                case "en":
                    strNote.Append("<p> Keywords：" + DealKeyword(node) + "</p>");
                    break;
            }
            return strNote.ToString();
        }
        /// <summary>
        /// 处理关键词
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealKeyword(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                //处理文本
                if (i == 0)
                {
                    strNote.Append(node.ChildNodes[i].InnerText);
                }
                else
                {
                    strNote.Append(";" + node.ChildNodes[i].InnerText);
                }
            }
            return strNote.ToString();
        }
        /// <summary>
        /// 处理用户组 这个 处理的 还不全
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string DealAuthorGroup(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            Dictionary<string, StringBuilder> mydic = new Dictionary<string, StringBuilder>();

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {

                for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                {
                    XmlNode tempEntry = node.ChildNodes[i].ChildNodes[j];
                    string roleValue = string.Empty;

                    switch (tempEntry.Name)
                    {

                        case "personname":
                            {
                                roleValue = tempEntry.Attributes["role"] == null ? "" : tempEntry.Attributes["role"].Value;
                                if (mydic.ContainsKey(roleValue))
                                {
                                    //说明存在
                                    mydic[roleValue].Append(";&nbsp;&nbsp;" + DealNoClearCDATA(tempEntry.InnerXml));
                                }
                                else
                                {
                                    //说明不存在
                                    // mydic[roleValue].Append(new StringBuilder().Append(tempEntry.InnerXml));
                                    mydic.Add(roleValue, new StringBuilder().Append(DealNoClearCDATA(tempEntry.InnerXml)));

                                }
                                ////处理图片
                                //strNote.Append(node.ChildNodes[i].InnerText);
                            }
                            break;
                        case "personblurb":
                            {
                                ////处理图片
                                //strNote.Append(node.ChildNodes[i].InnerText);
                                roleValue = tempEntry.Attributes["role"] == null ? "" : tempEntry.Attributes["role"].Value;
                                if (string.IsNullOrEmpty(roleValue))
                                {
                                    roleValue = tempEntry.PreviousSibling.Attributes["role"] == null ? "" : tempEntry.PreviousSibling.Attributes["role"].Value;
                                }
                                if (mydic.ContainsKey(roleValue))
                                {
                                    //说明存在

                                    strNote = new StringBuilder();
                                    strNote.Append("<img src='../images/notepic.png' class='notetitle'/>");
                                    strNote.Append("<span class=\"divnote\">");
                                    for (int z = 0; z < tempEntry.ChildNodes.Count; z++)
                                    {
                                        strNote.AppendFormat("<span class=\"notepara\">{0}</span>", DealAllObject(tempEntry.ChildNodes[z]));
                                    }
                                    strNote.Append("</span>");
                                    // mydic[roleValue].Append(";");
                                    mydic[roleValue].Append(strNote);
                                }
                                else
                                {
                                    //说明不存在
                                    strNote = new StringBuilder();
                                    strNote.Append("<img src='../images/notepic.png' class='notetitle'/>");
                                    strNote.Append("<span class=\"divnote\">");
                                    for (int z = 0; z < tempEntry.ChildNodes.Count; z++)
                                    {
                                        strNote.AppendFormat("<span class=\"notepara\">{0}</span>", DealAllObject(tempEntry.ChildNodes[z]));
                                    }
                                    strNote.Append("</span>");

                                    mydic.Add(roleValue, strNote);

                                }
                            }
                            break;
                    }
                }

            }

            strNote = new StringBuilder();
            bool isHasHead = false;
            foreach (string role in mydic.Keys)
            {
                isHasHead = false;

                if (mydic[role].Length == 0)
                {
                    continue;
                }
                switch (role)
                {
                    case "zh-Hans":
                        {
                            strNote.Append("<p>作者: ");
                            isHasHead = true;
                        }
                        break;
                    case "en":
                        {
                            strNote.Append("<p>Author: ");
                            isHasHead = true;
                        }
                        break;
                    case "":
                        {
                            strNote.Append("<p>作者: ");
                            isHasHead = true;
                        }
                        break;
                }
                if (isHasHead)
                {
                    strNote.Append(mydic[role]);
                    strNote.Append("</p>");
                }
            }

            return strNote.ToString();
        }
        /// <summary>
        /// 处理责任方式
        /// </summary>
        /// <returns></returns>
        public static string DealAuthorLiabilityForm(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            XmlNodeList mylist = node.SelectNodes("/authorgroup/author");

            for (int i = 0; i < mylist.Count; i++)
            {
                string role = mylist[i].Attributes["role"].Value;
                if (i == 0)
                {
                    strNote.Append(role + ":" + mylist[i].FirstChild.InnerText);
                }
                else
                {
                    strNote.Append(";" + role + ":" + mylist[i].FirstChild.InnerText);
                }
            }
            return strNote.ToString();
        }
        /// <summary>
        /// 处理关键词
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealAbstract(XmlNode node)
        {
            //处理段落
            string strAbstractName = "";
            if (node.ParentNode != null)
            {
                if (node.Name == "abstract")
                {
                    string roleValue = node.Attributes["role"].Value;
                    switch (roleValue)
                    {
                        case "zh-Hans":
                            strAbstractName = "摘要：";
                            break;
                        case "en":
                            strAbstractName = "Abstract：";
                            break;
                    }
                }
            }

            //  strNote.Append("<p>" + strAbstractName + DealAllObject(node.ChildNodes[i]) + "</p>");

            StringBuilder strNote = new StringBuilder();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                //处理文本
                if (i == 0)
                {
                    strNote.Append("<p>" + strAbstractName + DealAllObject(node.ChildNodes[i]) + "</p>");
                }
                else
                {
                    strNote.Append("<p>" + DealAllObject(node.ChildNodes[i]) + "</p>");
                }
            }
            return strNote.ToString();
        }

        /// <summary>
        /// 处理习题集
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealExerciseset(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            //string roleValue = node.Attributes["role"].Value;

            //switch (roleValue)
            //{
            //    case "zh-Hans":
            //        strNote.Append("<p> 关键词：" + DealKeyword(node) + "</p>");
            //        break;
            //    case "en":
            //        strNote.Append("<p> Keywords：" + DealKeyword(node) + "</p>");
            //        break;
            //}
            foreach (XmlNode item in node.ChildNodes)
            {
                strNote.Append(DealExercise(item));
            }

            return strNote.ToString();
        }
        /// <summary>
        /// 处理习题
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealExercise(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            int childcount = node.ChildNodes.Count;

            //这单独处理一下 需要组合的几个内容
            string year = string.Empty;
            string nonum = string.Empty;
            string recommendscore = string.Empty;
            string difficulty = string.Empty;
            string source = string.Empty;
            string role = string.Empty;

            if (node.Attributes["role"] != null)
            {
                role = DRMS.MirrorWeb.Utility.Utility.GetTypeNamefromXml("exercisetype", node.Attributes["role"].Value);
            }

            if (node.SelectSingleNode("nonum") != null)
            {
                nonum = node.SelectSingleNode("nonum").InnerText;
            }
            if (node.SelectSingleNode("year") != null)
            {
                year = node.SelectSingleNode("year").InnerText;
            }
            if (node.SelectSingleNode("recommendscore") != null)
            {
                recommendscore = node.SelectSingleNode("recommendscore").InnerText;
            }

            if (node.SelectSingleNode("difficulty") != null)
            {
                difficulty = DRMS.MirrorWeb.Utility.Utility.GetTypeNamefromXml("difficulty", node.SelectSingleNode("difficulty").InnerText);
            }

            if (node.SelectSingleNode("source") != null)
            {
                source = node.SelectSingleNode("source").InnerText;
            }
            //string formatspan = "<span>{0}</span>";
            //if (!string.IsNullOrEmpty(year) || !string.IsNullOrEmpty(source))
            //{
            //    strNote.Append("<p>");
            //    strNote.Append(string.Format(formatspan, role) + "：");

            //    strNote.Append((string.IsNullOrEmpty(year) == true ? "" : string.Format(formatspan, year+"年 ")));
            //    strNote.Append(string.IsNullOrEmpty(source) == true ? "" : string.Format(formatspan, source));

            //    strNote.Append("</p>");
            //}

            //if (!string.IsNullOrEmpty(recommendscore) || !string.IsNullOrEmpty(difficulty))
            //{
            //    strNote.Append("<p>" + (string.IsNullOrEmpty(recommendscore) == true ? "" : string.Format(formatspan, "推荐分数：" + recommendscore + " ")));
            //    strNote.Append(string.IsNullOrEmpty(difficulty) == true ? "" : string.Format(formatspan, "难度系数：" + difficulty));
            //    strNote.Append("</p>");
            //}
            int knowledgenum = 0;
            for (int i = 0; i < childcount; i++)
            {
                switch (node.ChildNodes[i].Name)
                {

                    case "authorgroup":
                        {
                            // strNote.Append(DealAuthorGroup(node.ChildNodes[i]));
                        }
                        break;
                    case "question":
                        {
                            bool isfirst = true;
                            //问题
                            foreach (XmlNode item in node.ChildNodes[i].ChildNodes)
                            {
                                if (isfirst)
                                {
                                    strNote.Append("<p>" + nonum + " " + DealPara(item.InnerText) + "</p>");
                                }
                                else
                                {
                                    strNote.Append("<p>" + DealPara(item.InnerText) + "</p>");
                                }
                            }
                            //for (int j = 0; j < childcount; j++)
                            //{
                            //    switch (node.ChildNodes[j].Name)
                            //    {
                            //        case "choiceitemlist":
                            //            {
                            //                //选择项列表
                            //                strNote.Append(DealChoiceItem(node.ChildNodes[j]));
                            //            }
                            //            break;
                            //    }
                            //}

                            XmlNodeList mylist = node.SelectNodes("choiceitemlist");
                            for (int j = 0; j < mylist.Count; j++)
                            {
                                strNote.Append(DealChoiceItem(mylist[j]));
                            }
                            XmlNode choice = node.SelectSingleNode("choiceanswer");
                            if (choice != null)
                            {
                                string issequence = string.Empty;
                                string sequenceName = string.Empty;

                                if (choice.Attributes["issequence"] != null)
                                {
                                    issequence = choice.Attributes["issequence"].Value;
                                }
                                if (issequence == "0")
                                {
                                    //无序
                                    sequenceName = "无序";
                                }
                                if (issequence == "1")
                                {
                                    //有序
                                    sequenceName = "有序";
                                }
                                if (!string.IsNullOrWhiteSpace(choice.InnerText))
                                {
                                    //选择题答案
                                    strNote.Append("<p>" + sequenceName + " 答案:" + choice.InnerText + "</p>");
                                }
                            }
                        }
                        break;
                    case "answer":
                        {

                            bool isfirst = true;
                            //答案
                            foreach (XmlNode item in node.ChildNodes[i].ChildNodes)
                            {
                                if (isfirst)
                                {
                                    isfirst = false;
                                    strNote.Append("<p><b>答案解析：</b></p>");
                                }
                                strNote.Append("<p>" + "" + DealAllObject(item) + "</p>");
                            }
                        }
                        break;
                    case "knowledgepoint":
                        {
                            //知识点
                            //foreach (XmlNode item in node.ChildNodes[i].ChildNodes)
                            //{
                            //    strNote.Append("<p>" + DealAllObject(item) + "</p>");
                            //}
                            if (knowledgenum == 0 && node.ChildNodes[i] != null && !string.IsNullOrEmpty(node.ChildNodes[i].InnerText))
                            {
                                strNote.Append("<p><b>考察的知识点：</b></p>");
                                knowledgenum++;
                            }

                            strNote.Append(DealKnowledgePoint(node.ChildNodes[i]));
                        }
                        break;
                    case "exerciseentry":
                        {

                            strNote.Append(DealExercise(node.ChildNodes[i]));
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                }
            }



            return strNote.ToString();
        }

        /// <summary>
        /// 处理选项
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealChoiceItem(XmlNode node)
        {

            StringBuilder strNote = new StringBuilder();

            string nonum = string.Empty;
            //if (node.SelectSingleNode("nonum")!=null)
            //{
            //    nonum = node.SelectSingleNode("nonum").InnerText;
            //}
            foreach (XmlNode item in node.ChildNodes)
            {
                //strNote.Append(DealExercise(item));
                switch (item.Name)
                {
                    case "nonum":
                        {
                            nonum = item.InnerText;
                        }
                        break;
                    case "choiceitem":
                        {

                            strNote.Append("<p>" + nonum + " " + DealPara(item.InnerText) + "</p>");
                        }
                        break;
                    case "answer":
                        {
                            //if (!string.IsNullOrEmpty(item.InnerText))
                            //{
                            //    strNote.Append("<p>" + nonum + "考察知识点：" + DealPara(item.InnerText) + "</p>");
                            //}
                        }
                        break;
                    case "knowledgepoint":
                        {
                            if (!string.IsNullOrEmpty(item.InnerText))
                            {
                                strNote.Append("<p><b>考察知识点：</b>" + nonum + " " + "</p>");
                                //知识点
                                //foreach (XmlNode knpoint in item.ChildNodes)
                                //{
                                strNote.Append(DealKnowledgePoint(item));
                                //}
                            }
                        }
                        break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.InnerText);
                        }
                        break;
                }
            }

            return strNote.ToString();
        }

        /// <summary>
        /// 处理选项
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DealKnowledgePoint(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            string nonum = string.Empty;
            //if (node.SelectSingleNode("pointnum") != null)
            //{
            //    nonum = node.SelectSingleNode("pointnum").InnerText;
            //}
            bool isfirst = true;
            foreach (XmlNode item in node.ChildNodes)
            {
                //strNote.Append(DealExercise(item));
                switch (item.Name)
                {
                    case "pointnum":
                        {
                            nonum = item.InnerText;
                        }
                        break;
                    case "pointcontent":
                        {
                            //问题
                            foreach (XmlNode knitem in item.ChildNodes)
                            {
                                if (!string.IsNullOrEmpty(knitem.InnerText))
                                {
                                    if (isfirst)
                                    {
                                        strNote.Append("<p>" + nonum + " " + DealPara(knitem.InnerText) + "</p>");
                                        isfirst = false;
                                    }
                                    else
                                    {
                                        strNote.Append("<p>" + DealPara(knitem.InnerText) + "</p>");
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            return strNote.ToString();
        }


        private static string DealTheorem(XmlNode node)
        {
            StringBuilder strNote = new StringBuilder();
            int childcount = node.ChildNodes.Count;

            //这单独处理一下 需要组合的几个内容

            string nomum = string.Empty;
            string role = string.Empty;

            if (node.Attributes["role"] != null)
            {
                //role = "";需要修改
            }

            if (node.SelectSingleNode("nonum") != null)
            {
                nomum = node.SelectSingleNode("nonum").InnerText;
            }

            for (int i = 0; i < childcount; i++)
            {
                switch (node.ChildNodes[i].Name)
                {
                    case "name":
                        {
                            strNote.Append("<p>" + nomum + "  " + DealPara(node.ChildNodes[i].InnerText) + "</p>");
                        }
                        break;
                    case "content":
                        {
                            strNote.Append("<p><b>定理内容：</b></p>");
                            strNote.Append("<p>" + DealPara(node.ChildNodes[i].InnerText) + "</p>");
                        }
                        break;
                    case "procedure":
                        {
                            strNote.Append("<p><b>证明过程：</b></p>");
                            strNote.Append("<p>" + DealPara(node.ChildNodes[i].InnerText) + "</p>");
                        }
                        break;
                    case "keywordset":
                        {
                            //选择项列表
                            // strNote.Append(DealKeywordSet(node.ChildNodes[i]));
                        }
                        break;
                    case "exerciseset":
                        {
                            if (!string.IsNullOrEmpty(node.ChildNodes[i].InnerText))
                            {
                                strNote.Append("<p><b>定理带的习题：</b></p>");
                            }
                            strNote.Append(DealExerciseset(node.ChildNodes[i]));
                        }
                        break;
                    case "classification":
                        {
                            // strNote.Append("<p>" + "分类体系：" + Utility.GetAllName(node.ChildNodes[i].InnerText) + "</p>");
                        }
                        break;

                    //case "exerciseentry":
                    //    {
                    //        strNote.Append(DealExercise(node.ChildNodes[i]));
                    //    }
                    //    break;
                    case "#text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                    case "#cdata-text":
                        {
                            //处理文本
                            strNote.Append(node.ChildNodes[i].InnerText);
                        }
                        break;
                }
            }



            return strNote.ToString();
        }

    }
}