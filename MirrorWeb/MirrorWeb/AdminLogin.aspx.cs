using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using DRMS.Model;
using DRMS.BLL;

namespace DRMS.MirrorWeb
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断url 若是从个人中心过来的页面则跳转到前台的登陆
            string path = Request["ReturnUrl"];
            if (!string.IsNullOrEmpty(path) && path.ToLower().IndexOf("/usercenter") == 0)
            {
                Response.Redirect("Login.aspx?ReturnUrl=" + path + "");
            }
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogon_Click(object sender, EventArgs e)
        {
            string account = tbxAccount.Value.Trim();
            string password = tbxPassword.Value.Trim();
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            bool result = UserLogon(account, password);
            if (result)
            {
                Redirect(account);
            }
        }

        /// <summary>
        /// 杂凑加密字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        private string HashPassword(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            string retHash = "";
            System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            byte[] combined = encoder.GetBytes(input);
            hash.ComputeHash(combined);
            retHash = Convert.ToBase64String(hash.Hash);
            return retHash;
        }


        /// <summary>
        /// 跳转
        /// <param name="account">账号</param>
        /// </summary>
        private void Redirect(string account)
        {
            string url = FormsAuthentication.GetRedirectUrl(account, true);
            string path = Request["ReturnUrl"];
            if (!string.IsNullOrEmpty(path))
            {
                url = Server.UrlDecode(path);
            }
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
            if (user.Role == "0") // 普通用户没有权限登录
            {
                this.message.Text = "您没有权限，无法登录！";
                return false;
            }
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
            //  登录成功
            userRepository.ResetTryNum(account);
            userRepository.AddLoginFlag(account);

            string role = userRepository.GetUserRole(user);
            FormsAuthenticationTicket authTick = new FormsAuthenticationTicket(
                                                    1,
                                                    account,
                                                    DateTime.Now,
                                                    DateTime.Now.AddMinutes(120),
                                                    false,
                                                    role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTick);
            this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
            return true;
        }

        protected void btnLogon_Click(object sender, ImageClickEventArgs e)
        {
            string account = tbxAccount.Value.Trim();
            string password = tbxPassword.Value.Trim();
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            bool result = UserLogon(account, password);
            if (result)
            {
                Redirect(account);
            }
        }
    }
}