using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserLogout();
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void UserLogout()
        {
            Log logA = new Log();
            string userName = HttpContext.Current.User.Identity.Name;

            //清除登录信息中的缓存  用于单点登录
            SingleLogin.ClearCache();

            string url = "Default.aspx";
            string page = "/Login.aspx";
            bool isUserCenter = false;
            if (Request.QueryString["url"] != null)
            {
                logA.Add(DataBaseType.USERDATA, LogType.LOGOUT, userName, userName + "退出", "用户退出");
                page = CNKI.BaseFunction.NormalFunction.GetQueryString("page", "");// Request.QueryString["page"].ToString();
                url = CNKI.BaseFunction.NormalFunction.GetQueryString("url", "");//Request.QueryString["url"].ToString();
                url = url == "web" ? "Default.aspx" : "Login.aspx";
                isUserCenter = page.Contains("UserCenter");
            }
            FormsAuthentication.SignOut();
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //  authCookie.Domain = "example.com";
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);

            if (url == "Default.aspx" && !isUserCenter)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "myScript", "goto('" + page + "')", true);
            }
            else
            {
                Response.Redirect("~/" + url);
            }

        }
    }
}