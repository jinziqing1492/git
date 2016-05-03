using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Tool = CNKI.BaseFunction;
using DRMS.BLL;
using DRMS.Model;
using System.Web.Security;

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class UserListView : BasePage.AdminBaseControl
    {
        #region 字段,属性
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return this.aspNetPager.PageSize; }
            set { this.aspNetPager.PageSize = value; }
        }

        private int recordCount;
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return this.aspNetPager.PageCount; }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SqlQueryCondition { get; set; }
        /// <summary>
        /// 日志对象
        /// </summary>
        private Log BllLogObj = new Log();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SetProperty();
            if (!IsPostBack)
            {
                InitData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userNames = this.hidIDList.Value.Trim();
            string action = this.ddlAction.SelectedValue.Trim();
            if (string.IsNullOrEmpty(userNames) || string.IsNullOrEmpty(action))
            {
                return;
            }
            string actionName = this.ddlAction.SelectedItem.Text;
            bool result = false;
            switch (action)
            {
                case "batchDelete":
                    result = BatchDelete(userNames);
                    break;
                default:
                    break;
            }
            message.Visible = true;
            message.Content = actionName;
            message.Content += result ? "成功" : "失败";
            message.MessageType = result ? DRMS.MirrorWeb.AdminUserControl.NotificationType.Success : DRMS.MirrorWeb.AdminUserControl.NotificationType.Error;

            SetSqlQueryCondition();
            BindUserList();
        }

        protected void RepeaterItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string type = e.CommandName;
            string uName = e.CommandArgument.ToString();


            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(uName))
            {
                result.Visible = true;
                message.Visible = false;
                result.Content = "操作失败！";
                result.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }

            uName = CNKI.BaseFunction.NormalFunction.ResetRedFlag(uName);
            switch (type)
            {
                case "Edit": Response.RedirectPermanent("UserItem.aspx?UserName=" + uName);
                    break;
                case "Delete":
                    {
                        result.Visible = true;
                        User user = new User();
                        if (user.Delete(uName))
                        {
                            result.Content = "删除成功！";
                            BllLogObj.Add(DataBaseType.USERDATA, LogType.DELETE, uName, uName, "删除用户成功;无DOI字段，使用USERNAME作为主键");
                            result.MessageType = AdminUserControl.NotificationType.Success;
                        }
                        else
                        {
                            result.Content = "删除失败！";
                            BllLogObj.Add(DataBaseType.USERDATA, LogType.DELETE, uName, uName, "删除用户失败;无DOI字段，使用USERNAME作为主键");
                            result.MessageType = AdminUserControl.NotificationType.Error;
                        }
                        InitData();
                    }
                    break;
                case "ResetPwd":
                    {
                        string defaultpwd = System.Configuration.ConfigurationManager.AppSettings["DefaultPWD"];
                        string md5str = FormsAuthentication.HashPasswordForStoringInConfigFile(defaultpwd, "MD5");
                        result.Visible = true;
                        User user = new User();
                        if (user.ModifyPwd(uName, md5str))
                        {
                            result.Content = "重置密码成功！";
                            BllLogObj.Add(DataBaseType.USERDATA, LogType.UPDATE, uName, uName, "删除用户成功;无DOI字段，使用USERNAME作为主键");
                            result.MessageType = AdminUserControl.NotificationType.Success;
                        }
                        else
                        {
                            result.Content = "重置密码失败！";
                            BllLogObj.Add(DataBaseType.USERDATA, LogType.UPDATE, uName, uName, "删除用户失败;无DOI字段，使用USERNAME作为主键");
                            result.MessageType = AdminUserControl.NotificationType.Error;
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        internal void InitData()
        {
            SetSqlQueryCondition();
            BindUserList();
            BindRecordCountLabel();
        }

        /// <summary>
        /// 绑定用户列表
        /// </summary>
        private void BindUserList()
        {
            User UserControl = new User();
            IList<UserInfo> userList = UserControl.GetList(SqlQueryCondition, PageNo, PageSize, out recordCount);
            if (userList == null || userList.Count <= 0)
            {
                // this.aspNetPager.Visible = false;
                this.repEntryList.DataSource = new DataTable();
                this.repEntryList.DataBind();
                this.aspNetPager.RecordCount = 0;
                return;
            }
            //this.aspNetPager.Visible = true;
            this.aspNetPager.RecordCount = this.RecordCount;

            if (this.PageNo > this.PageCount)
            {
                this.PageNo = this.PageCount;
            }

            this.repEntryList.DataSource = userList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 设定查询条件（这里需要修改字段）
        /// </summary>
        private void SetSqlQueryCondition()
        {
            if (string.IsNullOrWhiteSpace(this.SqlQueryCondition))
            {
                this.SqlQueryCondition = " order by adddate desc";//1 和2 为院校和厂商
            }
            this.aspNetPager.CurrentPageIndex = this.PageNo;

        }

        /// <summary>
        /// 绑定记录结果标签
        /// </summary>
        private void BindRecordCountLabel()
        {
            string content = string.Format("为您检索到 {0} 个用户，共 {1} 页，当前是第 {2} 页。", this.RecordCount, this.PageCount, this.PageNo);
            this.message.Content = content;
            this.message.Visible = true;
        }

        /// <summary>
        /// 获取参数，设置属性
        /// </summary>
        private void SetProperty()
        {
            this.PageNo = 1;
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="keys">唯一标识字符串（以;隔开）</param>
        /// <returns>成功 返回true；失败返回false</returns>
        private bool BatchDelete(string keys)
        {
            if (string.IsNullOrWhiteSpace(keys))
            {
                return false;
            }
            bool result = true;
            IList<string> keyList = keys.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            User userRepository = new User();
            foreach (var item in keyList)
            {
                result = result && userRepository.Delete(item);
                bool res = result ? BllLogObj.Add(DataBaseType.USERDATA, LogType.DELETE, item, item, "删除用户成功;无DOI字段，使用USERNAME作为主键") : BllLogObj.Add(DataBaseType.USERDATA, LogType.DELETE, item, item, "删除用户失败;无DOI字段，使用USERNAME作为主键");
            }
            return result;
        }

        /// <summary>
        /// 获得角色名称
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        protected string GetRoleName(string roleid)
        {
            string res = string.Empty;
            switch (roleid)
            {
                case "0":
                    res = "普通用户";
                    break;
                case "1":
                    res = "资源管理员";
                    break;
                case "2":
                    res = "系统管理员";
                    break;
                default:
                    break;
            }
            return res;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void aspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.PageNo = e.NewPageIndex;
            result.Visible = false;
            InitData();
        }
    }
}