﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using System.Web.UI;

using DRMS.Model;
using CNKI.BaseFunction;
using DRMS.BLL;


namespace DRMS.Web.Utility
{
    public class Utility
    {

        private static List<ThemeInfo> myThemeList = null;

        /// <summary>
        /// 获取当前用户角色
        /// </summary>
        /// <returns>角色</returns>
        public static string GetRole()
        {
            FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
            if (id == null || id.IsAuthenticated == false)
            {
                return "";
            }
            // 取得FormsAuthenticationTicket对象
            FormsAuthenticationTicket ticket = id.Ticket;
            if (ticket == null)
            {
                return "";
            }
            //  取得UserData字段数据
            return ticket.UserData;
        }

        /// <summary>
        /// 获取用户所在部门编号
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public static string GetDepartment()
        {
            string currDepartment = "";
            return currDepartment;
        }

        /// <summary>
        /// 根据角色获取角色的名称
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public static string GetRoleName(int roleType)
        {
            UserRole role = (UserRole)roleType;
            return EnumDescription.GetFieldText(role);
        }

        /// <summary>
        /// 获取当前登录的用户名
        /// </summary>
        /// <returns>用户名</returns>
        public static string GetUserName()
        {
            FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
            if (id == null)
            {
                return "";
            }
            // 取得FormsAuthenticationTicket对象
            FormsAuthenticationTicket ticket = id.Ticket;

            if (ticket == null)
            {
                return "";
            }
            //此处角色与后台进行了区分
            //if (ticket.UserData == "0" || ticket.UserData == "1")
            //{
            //  取得UserData字段数据
            return ticket.Name;
            //}
            //else
            //{
            //    return string.Empty;
            //}

        }

        /// <summary> /// 生成缩略图
        /// <param name=""originalImagePath"">源图路径（物理路径）</param>
        /// <param name=""thumbnailPath"">缩略图路径（物理路径）</param>
        /// <param name=""width"">缩略图宽度</param>
        /// <param name=""height"">缩略图高度</param>
        /// <param name=""mode"">生成缩略图的方式</param>  
        /// </summary>
        public static string MakeThumbnail(Stream originalImagestream, string thumbnailPath, int width, int height, string mode, string format)
        {
            switch (format)
            {
                case ".gif": break;
                case ".jpg": break;
                case ".jpeg": break;
                case ".bmp": break;
                case ".png": break;
                default:
                    return string.Empty;
            }
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalImagestream);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode.ToLower())
            {
                case "hw"://指定高宽缩放（可能变形）                
                    break;
                case "w"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "h"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height; break;
                case "cut"://指定高宽裁减（不变形）                
                    //if((double)originalImage.Width/(double)originalImage.Height > (double)towidth/(double)toheight)
                    //{
                    //    oh = originalImage.Height;
                    //    ow = originalImage.Height*towidth/toheight;
                    //    y = 0;
                    //    x = (originalImage.Width - ow)/2;
                    //}
                    //else
                    //{
                    //    ow = originalImage.Width;
                    //    oh = originalImage.Width*height/towidth;
                    //    x = 0;
                    //    y = (originalImage.Height - oh)/2;
                    //}

                    //需要判断一下如果这个图片没有超过所需要的大小那么就不要缩放了

                    //说明宽比高长 按宽缩放
                    if (originalImage.Width >= originalImage.Height)
                    {
                        if (ow < towidth)
                        {
                            towidth = ow;
                            toheight = oh;
                        }
                        else
                        {
                            toheight = towidth * oh / ow;
                        }
                    }
                    else
                    {         //说明高比宽长 按高缩放
                        // if(originalImage.Height>originalImage.Width)
                        if (oh < toheight)
                        {
                            towidth = ow;
                            toheight = oh;
                        }
                        else
                        {
                            towidth = toheight * ow / oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);



            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

            try
            {

                byte[] photo = null;
                //以jpg格式保存缩略图
                MemoryStream mystream = new MemoryStream();
                switch (format)
                {
                    case ".gif": bitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case ".jpeg": bitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".jpg": bitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".bmp": bitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    case ".png": bitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Png); break;
                    default: break;
                }
                //  bitmap.Save(mystream, originalImage.RawFormat);
                BinaryReader binread = new BinaryReader(mystream);
                mystream.Position = 0;
                photo = binread.ReadBytes((int)mystream.Length);
                binread.Close();
                mystream.Dispose();
                return StructTrans.TransStringFromByte(photo);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {

                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /*
        * 格式化文件大小，显示KB/MB/GB
        * @param size
        * @return String
        */
        public static String FormatSize(long size)
        {
            long SIZE_KB = 1024;
            long SIZE_MB = SIZE_KB * 1024;
            long SIZE_GB = SIZE_MB * 1024;

            if (size < SIZE_KB)
            {
                return String.Format("{0} B", (int)size);
            }
            else if (size < SIZE_MB)
            {
                return String.Format("{0:f2} KB", (float)size / SIZE_KB);
            }
            else if (size < SIZE_GB)
            {
                return String.Format("{0:f2} MB", (float)size / SIZE_MB);
            }
            else
            {
                return String.Format("{0:f2} GB", (float)size / SIZE_GB);
            }
        }



        /// <summary>
        /// 将xml 通过xslt转换后 以字符串形式输出
        /// </summary>
        /// <param name="xml">XmlDocument对象</param>
        /// <param name="filePath">xslt文件路径</param>
        /// <returns>经过转换后的字符串</returns>
        public static string XmlToString(XmlDocument xml, string filePath)
        {
            StringBuilder sb = new StringBuilder();//需要输出的字符串类
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;//编写xml声明
                settings.ConformanceLevel = ConformanceLevel.Fragment;//设置xml文件符合的一致性级别
                settings.CloseOutput = false;//settings关闭时不关闭基础流

                TextWriter tw = new StringWriter(sb);
                XmlWriter xw = XmlWriter.Create(tw, settings);
                XslCompiledTransform xst = new XslCompiledTransform();
                //加载格式化文件
                string str = HttpContext.Current.Request.ApplicationPath + "/" + filePath;
                xst.Load(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/" + filePath));
                xst.Transform(xml, null, xw);//把转换的结果输出到sb中
            }
            catch { return ""; }
            return sb.ToString();
        }
        /// <summary>
        ///显示信息
        /// </summary>
        /// <param name="aspNetPager"></param>
        /// <returns></returns>
        public static string GetPageMsg(Wuqi.Webdiyer.AspNetPager aspNetPager)
        {
            return "共为您搜索到 <b>" + aspNetPager.RecordCount + "</b> 条 当前显示第 <b>" + aspNetPager.CurrentPageIndex + "</b> 页 共 " + aspNetPager.PageCount + " 页 显示" + aspNetPager.StartRecordIndex + "-" + aspNetPager.EndRecordIndex + "条";
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
            title = Regex.Replace(title, @"[\r\n]", "", RegexOptions.IgnoreCase);
            return title;
        }


        /// <summary>
        /// 搜索关键词会被 ##LEFT## 和##RIGHT##包裹，且显示时会被替换为标红的font标签，所以在计算长度时不能计算这部分的长度
        /// </summary>
        /// <param name="title">截取的字符串</param>
        /// <param name="count">截取的长度</param>
        /// <param name="endStr">后缀</param>
        /// <returns>截取后的字符串</returns>
        public static string SubTitle(string title, int count, string endStr)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return string.Empty;
            }
            string subTitle = "";
            if (title.ToUpper().Contains("##LEFT##") && title.ToUpper().Contains("##RIGHT##"))
            {
                int leftIndex = title.ToUpper().IndexOf("##LEFT##");
                int rightIndex = title.ToUpper().IndexOf("##RIGHT##");

                string t0 = title.Substring(0, leftIndex);
                string t1 = title.Substring(leftIndex + 8, rightIndex - leftIndex - 8);
                string t2 = title.Substring(rightIndex + 9);

                title = t0 + t1 + t2;

                if (!string.IsNullOrEmpty(t0) && t0.Length > count)
                {
                    return NormalFunction.SubString(t0, count, endStr);
                }
                if ((t0 + t1).Length > count)
                {
                    return t0 + "##LEFT##" + NormalFunction.SubString(t1, count - t0.Length, endStr) + "##RIGHT##";
                }
                if (title.Length > count)
                {
                    return t0 + "##LEFT##" + t1 + "##RIGHT##" + NormalFunction.SubString(t2, count - t0.Length - t1.Length, endStr); ;
                }
                else
                {
                    return t0 + "##LEFT##" + t1 + "##RIGHT##" + t2;
                }
            }
            else
            {
                subTitle = NormalFunction.SubString(title, count, "...");
            }

            return subTitle;
        }


        /// <summary> 
        /// 警告消息框
        /// </summary> 
        /// <param name="message">提示信息,例子："不能为空!"</param> 
        public static void AlertMessage(string message)
        {
            Page page = System.Web.HttpContext.Current.Handler as Page;
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript'>alert('" + message.ToString() + "');</script>");
        }

        /// <summary> 
        /// 警告消息框
        /// </summary> 
        /// <param name="message">提示信息,例子："不能为空!"</param> 
        public static void AlertMessageCloseWindow(string message)
        {
            Page page = System.Web.HttpContext.Current.Handler as Page;
            page.ClientScript.RegisterStartupScript(page.GetType(), "messageCloseWindow", "<script language='javascript'>alert('" + message.ToString() + "');window.close();</script>");
        }

        /// <summary>
        ///加载分类
        /// </summary>
        private static void LoadTheme()
        {
            if (myThemeList == null)
            {
                Theme myTheme = new Theme();
                int recordCount = 0;
                myThemeList = myTheme.GetList("", 1, 1000, out recordCount);
                if (myThemeList != null)
                {
                    if (recordCount > 1000)
                    {
                        List<ThemeInfo> lastAll = myTheme.GetList("", 1, recordCount, out recordCount);
                        if (lastAll != null)
                        {
                            myThemeList.Clear();
                            myThemeList.AddRange(lastAll);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 获取分类id 根据给定的分类id查找合适的返回
        /// </summary>
        /// <param name="sourceThemeId"></param>
        /// <returns></returns>
        private static string GetThemeMapId(string sourceThemeId)
        {
            if (myThemeList != null)
            {
                List<ThemeInfo> myresult = myThemeList.Where(item =>
                {
                    return FindBySouceCode(item, sourceThemeId);
                }).ToList();
                string result = string.Empty;
                if (myresult != null)
                {
                    bool isFirst = true;
                    foreach (ThemeInfo item in myresult)
                    {
                        if (isFirst)
                        {
                            result = item.ID;
                            isFirst = false;
                        }
                        else
                        {
                            result = result + ";" + item.ID;
                        }
                    }
                }
                return result;
            }
            else
            {
                return sourceThemeId;
            }
        }
        /// <summary>
        /// 获取分类id 根据给定的分类id查找合适的返回
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        private static ThemeInfo GetThemeName(string themeId)
        {
            

            if (myThemeList != null)
            {
                List<ThemeInfo> myresult = myThemeList.Where(item =>
                {
                    return FindById(item, themeId);
                }).ToList();
                string result = string.Empty;
                if (myresult != null&& myresult.Count >0)
                {
                    //bool isFirst = true;
                    //foreach (ThemeInfo item in myresult)
                    //{
                    //    if (isFirst)
                    //    {
                    //        result = item.ID;
                    //        isFirst = false;
                    //    }
                    //    else
                    //    {
                    //        result = result + ";" + item.ID;
                    //    }
                    //}
                    return myresult[0];
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 比较用的函数 也许有变化
        /// </summary>
        /// <param name="themeItem"></param>
        /// <param name="sourceThemeId"></param>
        /// <returns></returns>
        private static bool FindBySouceCode(ThemeInfo themeItem, string sourceThemeId)
        {
            if (themeItem == null || string.IsNullOrWhiteSpace(themeItem.SourceCode) || string.IsNullOrWhiteSpace(sourceThemeId))
            {
                return false;
            }
            return themeItem.SourceCode.ToLower() == sourceThemeId.ToLower();
        }
        /// <summary>
        /// 比较用的函数 也许有变化
        /// </summary>
        /// <param name="themeItem"></param>
        /// <param name="sourceThemeId"></param>
        /// <returns></returns>
        private static bool FindById(ThemeInfo themeItem, string sourceThemeId)
        {
            if (themeItem == null || string.IsNullOrWhiteSpace(themeItem.SourceCode) || string.IsNullOrWhiteSpace(sourceThemeId))
            {
                return false;
            }
            return themeItem.ID.ToLower() == sourceThemeId.ToLower();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="themeid"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private static string GetAllName(string themeid, string prefix)
        {
            if (themeid == "0")
            {
                return prefix;
            }
            else
            {
                ThemeInfo item = GetThemeName(themeid);
                string themename = string.Empty;
                string result = string.Empty;
                if (item != null)
                {
                    themename = item.ThemeName;
                    if (!string.IsNullOrEmpty(prefix))
                    {
                        prefix = themename + " > " + prefix;
                    }
                    else
                    {
                        prefix = themename;
                    }
                    result = GetAllName(item.ParentID, prefix);
                }
                return result;
            }
        }

        /// <summary>
        /// 获取分类的完整路径 根据给定的多个分类
        /// </summary>
        /// <param name="themeIdlist"></param>
        /// <returns></returns>
        public static string GetAllName(string themeIdlist)
        {
            if(string.IsNullOrWhiteSpace(themeIdlist))
            {
                return string.Empty;
            }
            string[] list = themeIdlist.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            string result=string.Empty;
            if(list.Length>0)
            {
                LoadTheme();
                bool isfirst = true;
                string tempstr = string.Empty;

                foreach (string str in list)
                {
                    tempstr = GetAllName(str, "");
                    if (!string.IsNullOrWhiteSpace(tempstr))
                    {
                        if (isfirst)
                        {
                            result = tempstr;
                            isfirst = false;
                        }
                        else
                        {
                            result = result + ";" + tempstr;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据数据类型生成资源的链接
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="doi"></param>
        /// <returns></returns>
        public static string GetResHref(int dbtype, string doi)
        {
            DataBaseType db = (DataBaseType)dbtype;
            string typestr = string.Empty;
            string view_Page = "MyResView.aspx?rest={0}&doi={1}";
            switch (db)
            {
                case DataBaseType.BOOKTDATA:
                    {
                        typestr = "book";
                    }
                    break;
                case DataBaseType.CRITERION:
                    { 
                        typestr = "std";
                    }
                    break;
                case DataBaseType.REFERENCEBOOK:
                    {
                        typestr = "toolbook";
                    }
                    break;
                case DataBaseType.JOURNAL:
                    {
                        typestr = "baseid";
                    }
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    {
                        typestr = "conference";
                    }
                    break;
                case DataBaseType.YEARBOOK:
                    {
                        typestr = "year";
                    }
                    break;
                case DataBaseType.MAGAZINE:
                    {
                        typestr = "magaz";
                    }
                    break;
                case DataBaseType.NEWSPAPER:
                    {
                        typestr = "newspaper";
                    }
                    break;
                case DataBaseType.THESIS:
                    {
                        typestr = "thesis";
                    }
                    break;
                case DataBaseType.VIDEODATA:
                    {
                       typestr= "video";
                    }
                    break;
                case DataBaseType.AUDIODATA:
                    {
                        typestr = "audio";
                    }
                    break;
                case DataBaseType.PICDATA:
                    {
                        typestr = "pic";
                    }
                    break;
                default:
                    break;

            }
            return string.Format(view_Page, typestr, doi);
        }

    }
}
