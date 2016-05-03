using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using DRMS.Model;
using DRMS.BLL;
using System.Configuration;

namespace DRMS.MirrorWeb
{
    public partial class Login : System.Web.UI.Page
    {
        public string NewLogin = ConfigurationManager.AppSettings["NewLogin"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (NewLogin == "1")
                Response.Redirect("LoginNew.aspx");
        }

        /// <summary>
        /// 登陆成功 根据权限跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imglogin_Click(object sender, ImageClickEventArgs e)
        {
            string account = tbxusername.Text.Trim();
            string password = tbxpwd.Text.Trim();
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            bool result = UserLogon(account, password);
            if (result)
            {
                Redirect(account);
            }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool UserLogon(string account, string password)
        {
            Log logA = new Log();
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                this.message.Text = "用户名或密码不能为空！";
                return false;
            }
            User userRepository = new User();
            UserInfo user = userRepository.UserLogin(account, password);
            if (user == null)
            {
                int tryCount = userRepository.GetTryNum(account);
                if (tryCount >= 5)
                {
                    this.message.Text = "您已经5次尝试登录不成功，该账号已经被锁定！";
                    userRepository.SetFlag(account, 0);
                    return false;
                }

                userRepository.AddTryNum(account);
                this.message.Text = "您输入的用户名或者密码不正确，请重新输入！";
                return false;
            }
            //if (user.Role ==0) // 普通用户没有权限登录
            //{
            //    this.message.Text = "您没有权限，无法登录！";
            //    return false;
            //}
            if (user.Flag == 1)
            {
                this.message.Text = "您的账号已被锁定，暂时无法登录，请联系管理员！";
                return false;
            }
            if (user.Flag == 2)
            {
                this.message.Text = "您的账号已被禁用，暂时无法登录，请联系管理员！";
                return false;
            }
            //if (user.Version == 0)
            //{
            //    this.message.InnerHtml = "您的帐号处于待审核状态，暂时无法登录！";
            //    return false;
            //}            
            //  登录成功
            userRepository.ResetTryNum(account);
            userRepository.AddLoginFlag(account);

            string role = user.Role.ToString();// userRepository.GetUserRole(user);
            FormsAuthenticationTicket authTick = new FormsAuthenticationTicket(
                                                    1,
                                                    account,
                                                    DateTime.Now,
                                                    DateTime.Now.AddMinutes(30),
                                                    false,
                                                    role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTick);
            this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
            //logA.Add(DataBaseType.USERDATA, LogType.LOGNON, account, account + "登录后台", "用户登录后台");
            return true;
        }

        /// <summary>
        /// 跳转
        /// <param name="account">账号</param>
        /// </summary>
        private void Redirect(string account)
        {
            string url = FormsAuthentication.GetRedirectUrl(account, false);
           
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            string flag = this.Request["flag"] ?? "";
            if (url.IndexOf("flag=active") <= 0)
            {
                string separator = url.Contains('?') ? "&" : "?";
                url += separator + "flag=active";
            }
            Log logA = new Log();
            logA.Add(DataBaseType.USERDATA, LogType.LOGNON, account, account + "登录后台", "用户登录后台");
            Response.Redirect(url);
        }
    }
}