using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text.RegularExpressions;

using Tool = CNKI.BaseFunction;
using DRMS.Model;
using DRMS.BLL;

namespace DRMS.MirrorWeb.Admin
{
    public partial class UserItem : System.Web.UI.Page
    {
        #region 属性

        /// <summary>
        /// 该记录的ID
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否是编辑模式
        /// </summary>
        public bool EditMode
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ItemId);
            }
        }

        /// <summary>
        /// 操作标签
        /// </summary>
        protected string OperateLabel { get { return EditMode ? "编辑用户" : "添加用户"; } }

        /// <summary>
        /// 日志
        /// </summary>
        private Log logA = new Log();

        #endregion

        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            SetProperty();
            if (!IsPostBack)
            {
                InitData();
            }
        }


        /// <summary>
        /// 检查用户名是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnCheckUserName_Click(object sender, EventArgs e)
        {
            if (!CheckExsit())
            {
                return;
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidInput() == false)
            {
                message.MessageType = AdminUserControl.NotificationType.Error;
                message.Content = ErrorMessage;
                message.Visible = true;
                return;
            }
            if (EditMode == false)
            {
                if (CheckExsit())
                {
                    AddeUser();
                }
            }
            else
            {
                UpdateUser();
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 配置属性
        /// </summary>
        private void SetProperty()
        {
            this.ItemId = Tool.NormalFunction.GetQueryString("UserName", "");
        }

        /// <summary>
        /// 绑定实体
        /// </summary>
        private void BindLogical()
        {
            User userA = new User();
            UserInfo item = userA.GetUser(this.ItemId);
            if (null == item)
            {
                message.MessageType = AdminUserControl.NotificationType.Error;
                message.Content = "读取用户失败，请重新从列表页进入修改！";
                message.Visible = true;
                return;
            }
            this.tbxUserName.Text = item.UserName;
            this.tbxUserName.ReadOnly = true;
            this.tbxRemark.Text = item.REMARK ?? string.Empty;
            this.tbxRealName.Text = item.RealName ?? string.Empty;
            this.tbxPhone1.Text = item.TEL1 ?? string.Empty;
            this.tbxMaxOnline.Text = item.MaxOnlineCount.ToString() ?? string.Empty;
            this.tbxIpStart.Text = item.IPstart ?? string.Empty;
            this.tbxIpEnd.Text = item.Ipend ?? string.Empty;
            this.tbxEmail.Text = item.EMail ?? string.Empty;
            this.tbxUnit.Text = item.TOKEN ?? string.Empty;
            this.tbxStartDate.Text = FormatDate(item.UserUnlockDate);
            this.tbxEndDate.Text = FormatDate(item.UserLockDATE);

            ddlOrg.SelectedValue = item.IsOrg.ToString();
            ddlRole.SelectedValue = item.Role;
            ddlStatus.SelectedValue = item.Flag.ToString();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            if (EditMode)
            {
                BindLogical();
            }
        }

        /// <summary>
        /// 从页面中获取该基本信息实体
        /// </summary>
        /// <returns></returns>
        private UserInfo GetItemFormControl(UserInfo item)
        {
            item.EMail = this.tbxEmail.Text.Trim();
            // IP要转化为数字
            item.Ipend = this.tbxIpEnd.Text.Trim();
            if (!string.IsNullOrEmpty(this.tbxIpEnd.Text.Trim()))
            {
                item.Ipendnum = Util.IP2Int(this.tbxIpEnd.Text.Trim()).ToString();
            }
            item.IPstart = this.tbxIpStart.Text.Trim();
            if (!string.IsNullOrEmpty(this.tbxIpStart.Text.Trim()))
            {
                item.IpstartNum = Util.IP2Int(this.tbxIpStart.Text.Trim()).ToString();
            }
            item.IsOrg = Tool.StructTrans.TransNum(ddlOrg.SelectedValue);
            item.MaxOnlineCount = Tool.StructTrans.TransNum(this.tbxMaxOnline.Text.Trim());
            item.RealName = this.tbxRealName.Text.Trim();
            item.REMARK = this.tbxRemark.Text.Trim();
            item.Role = (this.ddlRole.SelectedIndex == 0 ? "0" : this.ddlRole.SelectedValue);
            item.TEL1 = this.tbxPhone1.Text.Trim();
            item.Flag = Tool.StructTrans.TransNum(this.ddlStatus.SelectedValue);
            item.TOKEN = tbxUnit.Text.Trim();
            item.UserUnlockDate = Tool.StructTrans.TransDate(this.tbxStartDate.Text);
            item.UserLockDATE = Tool.StructTrans.TransDate(this.tbxEndDate.Text);
            return item;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="item"></param>
        private void UpdateUser()
        {
            User UserA = new User();
            UserInfo item = UserA.GetUser(ItemId);
            if (null == item)
            {
                message.MessageType = AdminUserControl.NotificationType.Error;
                message.Content = "读取用户失败，请重新从列表页进入修改！";
                message.Visible = true;
                return;
            }
            item = GetItemFormControl(item);
            bool result = UserA.ModifyUser(item, ItemId);
            if (result)
            {
                message.Content = "修改用户成功";
                message.MessageType = AdminUserControl.NotificationType.Success;
                message.Visible = true;
                logA.Add(DataBaseType.USERDATA, LogType.UPDATE, this.ItemId, item.UserName, "修改用户成功");

            }
            else
            {
                message.Content = "修改用户失败！";
                message.MessageType = AdminUserControl.NotificationType.Error;
                message.Visible = true;
                logA.Add(DataBaseType.USERDATA, LogType.UPDATE, this.ItemId, item.UserName, "修改用户失败");
                return;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        private void AddeUser()
        {
            User userA = new User();
            UserInfo item = new UserInfo();
            item.ADDDATE = DateTime.Now;
            item.UserName = this.tbxUserName.Text.Trim();
            item = GetItemFormControl(item);
            string defaultpwd = System.Configuration.ConfigurationManager.AppSettings["DefaultPWD"];
            item.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(defaultpwd, "MD5");
            bool result = userA.AddUser(item);
            if (result)
            {
                message.Content = "添加用户成功";
                message.MessageType = AdminUserControl.NotificationType.Success;
                message.Visible = true;
                logA.Add(DataBaseType.USERDATA, LogType.ADD, this.ItemId, item.UserName, "添加用户成功");
            }
            else
            {
                message.Content = "添加用户失败！";
                message.MessageType = AdminUserControl.NotificationType.Error;
                message.Visible = false;
                logA.Add(DataBaseType.USERDATA, LogType.ADD, this.ItemId, item.UserName, "添加用户失败");
                return;
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        /// <returns>true 成功；false 失败</returns>
        private bool ValidInput()
        {
            if (string.IsNullOrWhiteSpace(this.tbxUserName.Text.Trim()))
            {
                ErrorMessage = "用户名不能为空！";
                return false;
            }
            if (!string.IsNullOrEmpty(this.tbxPhone1.Text.Trim()))
            {
                if (!Util.IsPhpne(this.tbxPhone1.Text.Trim()))
                {
                    ErrorMessage += "电话1的格式不正确！";
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(this.tbxEmail.Text.Trim()))
            {
                if (!Util.IsEmail(this.tbxEmail.Text.Trim()))
                {
                    ErrorMessage += "Email的格式不正确！";
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(this.tbxMaxOnline.Text.Trim()))
            {
                if (!Util.IsNumber(this.tbxMaxOnline.Text.Trim()))
                {
                    ErrorMessage += "最大在线数应该为数字！";
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(this.tbxIpStart.Text.Trim()))
            {
                if (!Util.IsIP(this.tbxIpStart.Text.Trim()))
                {
                    ErrorMessage += "开始IP的格式不正确！";
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(this.tbxIpEnd.Text.Trim()))
            {
                if (!Util.IsIP(this.tbxIpEnd.Text.Trim()))
                {
                    ErrorMessage += "结束IP的格式不正确！";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 查看新增用户名是否已经存在
        /// </summary>
        /// <returns></returns>
        private bool CheckExsit()
        {
            User userA = new User();
            bool isOK = false;
            if (!string.IsNullOrEmpty(this.tbxUserName.Text.Trim()))
            {
                message.Visible = true;
                if (userA.GetUser(this.tbxUserName.Text.Trim()) != null)
                {
                    message.Content = "用户名已经存在！";
                    message.MessageType = AdminUserControl.NotificationType.Error;
                    isOK = false;

                }
                else
                {
                    message.Content = "该用户名可用！";
                    message.MessageType = AdminUserControl.NotificationType.Success;
                    isOK = true;
                }
            }
            return isOK;
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string FormatDate(DateTime dt)
        {
            if (dt != DateTime.MinValue)
            {
                return dt.ToString("yyyy-MM-dd");
            }
            return "";
        }

        #endregion
    }
}