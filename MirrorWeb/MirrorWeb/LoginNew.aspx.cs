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
    public partial class LoginNew : System.Web.UI.Page
    {
        public string NewLogin = ConfigurationManager.AppSettings["NewLogin"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (NewLogin != "1")
                Response.Redirect("Login.aspx");
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
            UserLogon(account, password);//登录
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private void UserLogon(string account, string password)
        {
            Log logA = new Log();
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                this.message.Text = "用户名或密码不能为空！";
                return;
            }
            User userRepository = new User();
            UserInfo user = userRepository.UserLogin(account, password);
            if (user == null)
            {
                this.message.Text = "用户名或密码错误，请重新输入！";
                return;
            }


            string role = user.Role.ToString();// userRepository.GetUserRole(user);
            if (user.UserUnlockDate > DateTime.Now && !role.Contains("1"))
            {
                this.message.Text = "您的账号暂时不能使用，请" + user.UserUnlockDate.ToString("yyyy年MM月dd日") + "后重试";
                return;
            }
            if (user.UserLockDATE < DateTime.Now.AddDays(-1) && user.UserLockDATE != DateTime.MinValue && !role.Contains("1"))
            {
                this.message.Text = "您的账号已过期";
                return;
            }

            if (user.Flag == 1)
            {
                this.message.Text = "您的账号已被锁定，暂时无法登录，请联系管理员！";
                return;
            }
            if (user.Flag == 2)
            {
                this.message.Text = "您的账号已被禁用，暂时无法登录，请联系管理员！";
                return;
            }

            if (SingleLogin.isLogin(account) && user.Role.ToString().Contains("0"))
            {
                this.message.Text = "您的账号已在其它地点登录，请 " + SingleLogin.TimeOut + " 秒后重试";
                return;
            }

            //  登录成功
            userRepository.ResetTryNum(account);
            userRepository.AddLoginFlag(account);

            FormsAuthenticationTicket authTick = new FormsAuthenticationTicket(
                                                    1,
                                                    account,
                                                    DateTime.Now,
                                                    DateTime.Now.AddMinutes(30),
                                                    false,
                                                    role);
            string encryptedTicket = FormsAuthentication.Encrypt(authTick);
            this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));

            //根据不同的用户角色，跳转到不同的页面
            if (role.Contains("1"))
            {
                Redirect(account);
                return;
            }
            else if (role.Contains("0"))
            {
                UserRedirect(account);
                return;
            }
        }

        /// <summary>
        /// 系统管理员 跳转到管理员系统界面
        /// <param name="account">账号</param>
        /// </summary>
        private void Redirect(string account)
        {
            string url = FormsAuthentication.GetRedirectUrl(account, false);

            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            if (url.ToLower().IndexOf("/admin") != 0)
            {
                url = "/Admin/Default.aspx";
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
        /// 普通用户 跳转到普通用户界面
        /// </summary>
        /// <param name="account"></param>
        private void UserRedirect(string account)
        {

            string url = FormsAuthentication.GetRedirectUrl(account, false);

            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            if (url.ToLower().IndexOf("/view") != 0 && url.ToLower().IndexOf("/usercenter") != 0)
            {
                url = "/Index.aspx";
            }
            if (url.ToLower().Contains("dbthemenav.aspx"))
            {
                url = "/Index.aspx";
            }
            Log logA = new Log();
            logA.Add(DataBaseType.USERDATA, LogType.LOGNON, account, account + "登录前台", "用户登录前台");
            Response.Redirect(url);
        }

        protected void imgIplogin_Click(object sender, ImageClickEventArgs e)
        {
            Log logA = new Log();
            string ip = Request.UserHostAddress;
            uint intIp = Util.IP2Int(ip);
            User uerA = new BLL.User();
            int recordCount = 0;
            IList<UserInfo> list = uerA.GetList(" IPENDNUM >= " + intIp.ToString() + " AND IPSTARTNUM <= " + intIp.ToString() + " AND FLAG=0 AND ISORG=1", 1, 2, out recordCount);
            if (list != null && list.Count != 0)
            {
                UserInfo user = list[0];
                //判断是否到期
                if (user.UserUnlockDate > DateTime.Now)
                {
                    this.message.Text = "您的账号暂时不能使用，请" + user.UserUnlockDate.ToString("yyyy年MM月dd日") + "后重试";
                    return;
                }
                if (user.UserLockDATE < DateTime.Now.AddDays(-1) && user.UserLockDATE != DateTime.MinValue)
                {
                    this.message.Text = "您的账号已过期";
                    return;
                }

                uerA.AddLoginFlag(user.UserName);
                //现在保存的是他是个人用户还是机构用户
                FormsAuthenticationTicket authTick = new FormsAuthenticationTicket(
                                                        1,
                                                        user.UserName,
                                                        DateTime.Now,
                                                        DateTime.Now.AddMinutes(30),
                                                        false,
                                                        "0");
                string encryptedTicket = FormsAuthentication.Encrypt(authTick);
                this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
                string lastVisit = "";
                if (user.LASTVISIT != DateTime.MinValue)//用户中心会读取上次登录时间，由于用户表中没有登录时间字段，所以将该时间记录在日志表中
                {
                    lastVisit = user.LASTVISIT.ToString("yyyy-MM-dd");
                }
                logA.Add(DataBaseType.USERDATA, LogType.LOGNON, user.UserName, user.UserName + "登录网站", lastVisit);

                //登录成功，跳转页面
                Response.Redirect("/Index.aspx?issingle=false");//如果issingle=false则表明不需要让其不能登录
            }
            else
            {
                this.message.Text = "您的IP没有注册，或您的账号已经锁定或者禁用，请用其他方式登录！";
                return;
            }
        }
    }
}