using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DRMS.MirrorWeb
{
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// 程序启动的时候需要验证一下系统是否注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (HttpContext.Current.Request.CurrentExecutionFilePathExtension == ".aspx")
            //{
            //    if (HttpContext.Current.Request.Path.ToLower() != "/regsvr.aspx")
            //    {
            //        if (DateTime.Now >= Verification.GetOutDate())
            //        {
            //            CNKI.BaseFunction.NormalFunction.Redirect("/Regsvr.aspx");
            //            return;
            //        }
            //    }
            //}
            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SessionId";
                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch { }

            //此处是身份验证
            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;
                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }
            }
            catch { }
        }
        private void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (null == cookie)
            {
                cookie = new HttpCookie(cookie_name);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);//重新设定请求中的cookie值，将服务器端的session值赋值给它
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex1 = Server.GetLastError();
            if (ex1 is HttpRequestValidationException)
            {
                Response.Write("请您输入合法字符串。");
                Server.ClearError();//清除异常
            }
            // 在出现未处理的错误时运行的代码 
            Exception ex = Server.GetLastError().GetBaseException();
            StringBuilder str = new StringBuilder();
            str.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            str.Append("\r\n.客户信息：");


            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            str.Append("\r\n\tIp:" + ip);
            str.Append("\r\n\t浏览器:" + Request.Browser.Browser.ToString());
            str.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
            str.Append("\r\n\t操作系统:" + Request.Browser.Platform.ToString());
            str.Append("\r\n.错误信息：");
            str.Append("\r\n\t页面：" + Request.Url.ToString());
            str.Append("\r\n\t错误信息：" + ex.Message);
            str.Append("\r\n\t错误源：" + ex.Source);
            str.Append("\r\n\t异常方法：" + ex.TargetSite);
            str.Append("\r\n\t堆栈信息：" + ex.StackTrace);
            str.Append("\r\n--------------------------------------------------------------------------------------------------");
            //创建路径 
            string upLoadPath = Server.MapPath("~/log/");
            if (!System.IO.Directory.Exists(upLoadPath))
            {
                System.IO.Directory.CreateDirectory(upLoadPath);
            }
            //创建文件 写入错误 
            System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", str.ToString(), System.Text.Encoding.UTF8);
            //处理完及时清理异常 
            Server.ClearError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User == null || HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                return;
            }
            FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
            if (id == null)
            {
                return;
            }
            // 取得FormsAuthenticationTicket对象
            FormsAuthenticationTicket ticket = id.Ticket;
            //  取得UserData字段数据
            string userData = ticket.UserData;
            //  如果有多个角色，可以用逗号分隔
            string[] roles = string.IsNullOrWhiteSpace(userData) ? null : userData.Split(',');
            // 赋予该用户新的身份
            HttpContext.Current.User = new GenericPrincipal(id, roles);
        }
    }
}