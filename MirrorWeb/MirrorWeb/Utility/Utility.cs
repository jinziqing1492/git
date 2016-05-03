using System;
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


namespace DRMS.MirrorWeb.Utility
{
    public class Utility
    {


        private static DrmRightModelInfo myDrmModel;
        public static List<IPScopeInfo> myIpList;

        static Utility()
        {
            GetIpScopeList();
        }

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

        /// <summary>
        /// 从xml配置文件中获取类型对应的名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeNamefromXml(string restype, string type)
        {
            string result = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(@"../configuration/listItem.xml"));
            XmlNode root = null;
            string currItem = "";
            switch (restype)
            {
                case "restype":
                    {
                        currItem = "restype";
                    }
                    break;
                case "usertype":
                    {
                        currItem = "usertype";
                    }
                    break;
                case "journaltype":
                    {
                        currItem = "journaltype";
                    }
                    break;
                case "newstype":
                    {
                        currItem = "newstype";
                    }
                    break;
                case "magtype":
                    {
                        currItem = "magtype";
                    }
                    break;
                case "stdtype":
                    {
                        currItem = "stdtype";
                    }
                    break;
                case "videotype":
                    {
                        currItem = "videotype";
                    }
                    break;
                case "videoformat":
                    {
                        currItem = "videoformat";
                    }
                    break;
                case "picprecision":
                    {
                        currItem = "picprecision";
                    }
                    break;
                case "pictype":
                    {
                        currItem = "pictype";
                    }
                    break;
                default:
                    break;
            }
            root = doc.SelectSingleNode(@"listitem/" + currItem + "/item[@value='" + type + "']");
            if (root != null)
            {
                result = root.Attributes["name"].Value;
            }
            return result;
        }

        /// <summary>
        /// 获取Xpath_items下包含的节点信息列表
        /// </summary>
        /// <param name="Xpath_items"></param>
        /// <returns></returns>
        public static XmlNodeList getDisplayDbListFromConfig(string Xpath_items)
        {
            string itemname = (string.IsNullOrEmpty(Xpath_items)) ? "1" : Xpath_items;
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(@"~/configuration/DisplayDbConfig.xml"));
            XmlNodeList mylist = doc.SelectNodes("/DbList/" + itemname + "/item");
            return mylist;
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
        /// 获取当前最大终端数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxTerminalCount()
        {
            DrmRightModelInfo item = GetCurrentDrmPolicy();
            if (item != null)
            {
                return item.TerminalCount;
            }
            return 0;
        }

        /// <summary>
        /// 获取当前的drm策略
        /// </summary>
        /// <returns></returns>
        public static DrmRightModelInfo GetCurrentDrmPolicy()
        {
            if (myDrmModel != null)
            {
                return myDrmModel;
            }
            else
            {
                myDrmModel = new DrmRightModelInfo();
                XmlDocument reader = new XmlDocument();
                try
                {
                    string menuStr = "configuration/DrmConfig.xml";
                    reader.Load(HttpContext.Current.Server.MapPath("~/") + menuStr);
                    XmlNode xm = reader.SelectSingleNode("/config");
                    XmlNode xmc = xm.SelectSingleNode("Type");
                    if (xmc != null)
                    {
                        myDrmModel.Type = CNKI.BaseFunction.StructTrans.TransNum(xmc.InnerText);
                    }
                    xmc = xm.SelectSingleNode("TimeLimit");
                    if (xmc != null)
                    {
                        myDrmModel.TimeLimit = (xmc.InnerText == "0") ? false : true;
                    }
                    xmc = xm.SelectSingleNode("TimeUnit");
                    if (xmc != null)
                    {
                        myDrmModel.TimeUnit = xmc.InnerText;
                    }
                    xmc = xm.SelectSingleNode("TimeLength");
                    if (xmc != null)
                    {
                        myDrmModel.TimeLength = CNKI.BaseFunction.StructTrans.TransNum(xmc.InnerText);
                    }
                    xmc = xm.SelectSingleNode("CopyLimit");
                    if (xmc != null)
                    {
                        myDrmModel.CopyLimit = (xmc.InnerText == "0") ? false : true;
                    }
                    xmc = xm.SelectSingleNode("PrintLimit");
                    if (xmc != null)
                    {
                        myDrmModel.PrintLimit = (xmc.InnerText == "0") ? false : true;
                    }
                    xmc = xm.SelectSingleNode("CopyTextLimit");
                    if (xmc != null)
                    {
                        myDrmModel.CopyTextLimit = (xmc.InnerText == "0") ? false : true;
                    }

                    //xmc = xm.SelectSingleNode("CopyCharLimit");
                    //if (xmc != null)
                    //{
                    //    myDrmModel.CopyCharLimit = (xmc.InnerText == "0") ? false : true;
                    //}

                    xmc = xm.SelectSingleNode("CopyCharCount");
                    if (xmc != null)
                    {
                        myDrmModel.CopyCharCount = CNKI.BaseFunction.StructTrans.TransNum(xmc.InnerText);
                        if (myDrmModel.CopyCharCount <= 0)
                        {
                            myDrmModel.CopyCharLimit = false;
                        }
                        else
                        {
                            myDrmModel.CopyCharLimit = true;
                        }
                    }
                    xmc = xm.SelectSingleNode("TerminalCount");
                    if (xmc != null)
                    {
                        myDrmModel.TerminalCount = CNKI.BaseFunction.StructTrans.TransNum(xmc.InnerText);
                    }
                }
                catch
                {
                    return null;
                }
                return myDrmModel;
            }
        }

        /// <summary>
        /// 获取drm结束时间
        /// </summary>
        /// <param name="timeType"></param>
        /// <param name="timeLength"></param>
        /// <returns></returns>
        public static string GetDrmEndTime(string timeType, int timeLength)
        {
            string result = string.Empty;
            switch (timeType)
            {
                case "y":
                    result = DateTime.Now.AddYears(timeLength).ToString("yyyy-MM-01");
                    break;
                case "m":
                    result = DateTime.Now.AddMonths(timeLength).ToString("yyyy-MM-01");
                    break;
                case "d":
                    result = DateTime.Now.AddDays(timeLength).ToString("yyyy-MM-01");
                    break;
            }
            return result;
        }

        /// <summary>
        /// 设置drm策略
        /// </summary>
        /// <returns></returns>
        public static bool SetDrmPolicy(DrmRightModelInfo item)
        {
            if (null == item)
            {
                return false;
            }
            XmlDocument reader = new XmlDocument();
            try
            {

                string menuStr = "configuration/DrmConfig.xml";
                reader.Load(HttpContext.Current.Server.MapPath("~/") + menuStr);
                XmlNode xm = reader.SelectSingleNode("/config");
                XmlNode xmc = xm.SelectSingleNode("Type");
                if (xmc != null)
                {
                    xmc.InnerText = myDrmModel.Type.ToString();
                }
                XmlNode xmc2 = xm.SelectSingleNode("TimeLimit");
                if (xmc2 != null)
                {
                    xmc2.InnerText = (myDrmModel.TimeLimit == true) ? "1" : "0"; //myDrmModel.TimeLimit.ToString();
                }
                XmlNode xmc3 = xm.SelectSingleNode("TimeUnit");
                if (xmc3 != null)
                {
                    xmc3.InnerText = myDrmModel.TimeUnit;
                }
                XmlNode xmc4 = xm.SelectSingleNode("TimeLength");
                if (xmc4 != null)
                {
                    xmc4.InnerText = myDrmModel.TimeLength.ToString();
                }
                XmlNode xmc5 = xm.SelectSingleNode("CopyLimit");
                if (xmc5 != null)
                {
                    xmc5.InnerText = (myDrmModel.CopyLimit == true) ? "1" : "0";
                }
                XmlNode xmc6 = xm.SelectSingleNode("PrintLimit");
                if (xmc6 != null)
                {
                    xmc6.InnerText = (myDrmModel.PrintLimit == true) ? "1" : "0";
                }
                XmlNode xmc7 = xm.SelectSingleNode("TerminalCount");
                if (xmc7 != null)
                {
                    xmc7.InnerText = myDrmModel.TerminalCount.ToString();
                }

                XmlNode xmc8 = xm.SelectSingleNode("CopyCharCount");
                if (xmc8 != null)
                {
                    xmc8.InnerText = myDrmModel.CopyCharCount.ToString();
                }
                reader.Save(HttpContext.Current.Server.MapPath("~/") + menuStr);
                myDrmModel = null;//清空drm以便及时更新drm策略
                return true;
            }
            catch
            {
                return false;
            }
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
        /// 获取ip的范围列表
        /// </summary>
        public static void GetIpScopeList()
        {
            myIpList = new List<IPScopeInfo>();
            XmlDocument reader = new XmlDocument();
            try
            {
                string menuStr = "configuration/IPConfig.xml";
                reader.Load(HttpContext.Current.Server.MapPath("~/") + menuStr);
                XmlNode xm = reader.SelectSingleNode("/item");
                XmlNodeList mynodeList = xm.SelectNodes("ip");

                if (mynodeList != null)
                {

                    foreach (XmlNode item in mynodeList)
                    {
                        IPScopeInfo myip = new IPScopeInfo();
                        myip.IpStart = IP2Int(item.Attributes["startip"].Value);
                        myip.IpEnd = IP2Int(item.Attributes["endip"].Value);
                        myIpList.Add(myip);
                    }
                }
            }
            catch(Exception e)
            {
            }
        }

        /// <summary>
        /// 规范日期格式（yyyy-MM-dd）
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        public static string FormatDateTime(object input)
        {
            string result = string.Empty;
            if (input != null)
            {
                DateTime dt = CNKI.BaseFunction.StructTrans.TransDate(input.ToString());
                if (dt != DateTime.MinValue)
                {
                    result = dt.ToString("yyyy-MM-dd");
                }
            }
            return result;
        }

        /// <summary>
        ///规范日期显示格式（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDateTimeDetail(object obj)
        {
            string result = string.Empty;
            if (obj != null)
            {
                DateTime dt = CNKI.BaseFunction.StructTrans.TransDate(obj.ToString());
                if (dt != DateTime.MinValue)
                {
                    result = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            return result;
        }

        /// <summary>
        /// 替换标红
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        public static string ReplaceRed(object input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            return CNKI.BaseFunction.NormalFunction.ReplaceRed(input.ToString());
        }

        /// <summary>
        /// 去掉首字母
        /// </summary>
        /// <returns></returns>
        public static string ReplaceBeginStr(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            return input.TrimStart(';');
        }
    }
}
