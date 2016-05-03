using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace DRMS.MirrorWeb
{
    public class Util
    {
        private static Regex RegPhone = new Regex(@"^(\d+-)*\d+$");
        private static Regex RegNumber = new Regex("^[0-9]+$");//包含0
        private static Regex RegNumberD = new Regex("^[1-9][0-9]*$");
        private static Regex RegEmail = new Regex(@"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegIP = new Regex(@"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        private static Regex RegUserName = new Regex(@"^[a-zA-Z][a-zA-Z0-9_]{1,15}$");
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
        /// 获取前台的用户名，如果是管理员则不获取
        /// </summary>
        /// <returns></returns>
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
            if (!ticket.UserData.Contains("1"))
            {
                return ticket.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 当前时间是否在工作时间
        /// </summary>
        /// <returns></returns>
        public static bool IsWorkTime()
        {
            XmlDocument xd = new XmlDocument();
            string path = HttpContext.Current.Server.MapPath("~/configuration/WorkTime.xml");
            xd.Load(path);
            try
            {
                //判断当前日期是否在工作时间（周几等）
                XmlNode node = xd.SelectSingleNode("//WorkDay");
                if (node != null)
                {
                    string weekText = node.InnerText;
                    string[] weeks = weekText.Split(',');
                    if (weeks != null && weeks.Length > 0)
                    {
                        string week = m_GetWeekNow();
                        if (!weeks.Contains(week))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                //判断当前时间是否在工作时间（几点）
                XmlNodeList nodelist = xd.SelectNodes("//WorkTime");
                if (nodelist != null && nodelist.Count > 0)
                {
                    foreach (XmlNode xnode in nodelist)
                    {
                        string timeText = xnode.InnerText;
                        string[] times = timeText.Split('-');
                        if (times != null && times.Length == 2)
                        {
                            string startTime = times[0];
                            string endTime = times[1];
                            TimeSpan sTime = DateTime.Parse(startTime).TimeOfDay;
                            TimeSpan eTime = DateTime.Parse(endTime).TimeOfDay;
                            if (DateTime.Now.TimeOfDay > sTime && DateTime.Now.TimeOfDay < eTime)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return true;
        }

        private static string m_GetWeekNow()
        {
            string strWeek = DateTime.Now.DayOfWeek.ToString();
            switch (strWeek)
            {
                case "Monday":
                    return "1";
                case "Tuesday":
                    return "2";
                case "Wednesday":
                    return "3";
                case "Thursday":
                    return "4";
                case "Friday":
                    return "5";
                case "Saturday":
                    return "6";
                case "Sunday":
                    return "7";
            }
            return "0";
        }


    }
}