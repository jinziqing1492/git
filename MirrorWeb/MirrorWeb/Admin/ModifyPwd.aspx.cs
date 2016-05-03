using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class ModifyPwd : System.Web.UI.Page
    {
        BLL.User bllUserObj = new BLL.User();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string pwd = this.txtPwd.Text.Trim();
            string rePwd = this.txtRePwd.Text.Trim();
            string origPwd = this.txtOrigPwd.Text.Trim();
            string uname = HttpContext.Current.User.Identity.Name.Trim();//用户名

            //-----验证-----
            if (string.IsNullOrEmpty(origPwd))
            {
                message.Visible = true;
                message.Content = "请输入旧密码";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            if (string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(rePwd))
            {
                message.Visible = true;
                message.Content = "请输入新密码";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }

            if (!pwd.Equals(rePwd))
            {
                message.Visible = true;
                message.Content = "密码不相同，请重新输入";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            if (pwd.Equals(origPwd))
            {
                message.Visible = true;
                message.Content = "新密码与旧密码不能相同";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            if (rePwd.Length > 10 || rePwd.Length < 6)
            {
                message.Visible = true;
                message.Content = "密码格式错误";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            //-----验证-----
            //-----验证旧密码-----
            string MD5OrigPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(origPwd, "MD5");
            if (bllUserObj.UserLogin(uname, MD5OrigPwd) == null)
            {
                message.Visible = true;
                message.Content = "您输入的旧密码错误";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            //-----验证旧密码-----
            //-----更改密码-----
            string MD5Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(rePwd, "MD5");
            bool res = false;
            try
            {
                res = bllUserObj.ModifyPwd(uname, MD5Pwd);
            }
            catch (Exception)
            {
                res = false;
            }
            if (!res)
            {
                message.Visible = true;
                message.Content = "修改失败";
                message.MessageType = AdminUserControl.NotificationType.Error;
            }
            else
            {
                message.Visible = true;
                message.Content = "修改成功";
                message.MessageType = AdminUserControl.NotificationType.Success;
            }
            //-----更改密码-----
        }
    }
}