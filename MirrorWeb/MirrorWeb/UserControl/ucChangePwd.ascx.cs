using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using DRMS.BLL;
using CNKI.BaseFunction;
using DRMS.Model;
using DRMS.MirrorWeb.Utility;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class ucChangePwd : System.Web.UI.UserControl
    {

        #region 字段属性

        protected Log _l = new Log();        
        private User _user = new User();
        private string _username = Utility.Utility.GetUserName();

        #endregion        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CurrentPassword.Text = "";
                //NewPassword.Text = "";
                //ConfirmNewPassword.Text = "";
            }
            
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            //FailureText.Visible = false;
            UserInfo item = _user.GetUser(_username);
            if (null == item)
            {
                return;
            }
            item = GetItemFormControl(item);
            bool result = _user.ModifyUser(item, _username);
            if (result)
            {
                _l.Add(DataBaseType.USERDATA, LogType.UPDATE, this._username, item.UserName, "修改密码成功");
                Utility.Utility.AlertMessage("修改密码成功！");
            }
            else
            {
                _l.Add(DataBaseType.USERDATA, LogType.UPDATE, this._username, this._username, "修改密码失败");
                //FailureText.Visible = true;
                Utility.Utility.AlertMessage("修改密码失败！");
                return;
            }  
            
        }
        /// <summary>
        /// 从页面中获取该基本信息实体
        /// </summary>
        /// <returns></returns>
        private UserInfo GetItemFormControl(UserInfo item)
        {
            string newPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(this.NewPassword.Text.Trim(), "MD5");
            string curPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(this.CurrentPassword.Text.Trim(), "MD5");
            if (curPwd == item.Password)
            {
                item.Password = newPwd;
               
            }
            else
            {                
                item = null;
                Utility.Utility.AlertMessage("原密码填写不正确！");
            }
            return item;
        }


        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            CurrentPassword.Text = "";
            NewPassword.Text = "";
            ConfirmNewPassword.Text = "";
            //return;
        }

    }
}