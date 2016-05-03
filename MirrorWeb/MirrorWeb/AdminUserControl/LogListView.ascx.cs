using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class LogListView : BasePage.AdminBaseControl
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

        protected void RepeaterItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string type = e.CommandName;
            string id = e.CommandArgument.ToString();

            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(id))
            {
                message.Content = "操作失败！";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            switch (type)
            {
                case "Delete":
                    DRMS.BLL.Log bll = new DRMS.BLL.Log();
                    bool isSuccess = bll.Delete(id);
                    if (isSuccess)
                    {
                        InitData();
                        message.Content = "删除成功！";
                        message.MessageType = AdminUserControl.NotificationType.Success;
                    }
                    else
                    {
                        message.Content = "删除失败！";
                        message.MessageType = AdminUserControl.NotificationType.Error;
                    }
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
            Log LogControl = new Log();
            IList<LogInfo> LogList = LogControl.GetList(SqlQueryCondition, PageNo, PageSize, out recordCount, true);
            if (LogList == null || LogList.Count <= 0)
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

            this.repEntryList.DataSource = LogList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 设定查询条件（这里需要修改字段）
        /// </summary>
        private void SetSqlQueryCondition()
        {
            if (string.IsNullOrWhiteSpace(this.SqlQueryCondition))
            {
                this.SqlQueryCondition = " order by date";
            }
            else
            {
                if (this.SqlQueryCondition.IndexOf("order by") < 0)
                {
                    this.SqlQueryCondition += " order by date";
                }
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
        /// 数据类型
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        protected string ResType(object input)
        {
            string resType = "未知";
            if (input == null)
            {
                return string.Empty;
            }
            Type logtype = typeof(DataBaseType);
            foreach (int myCode in Enum.GetValues(logtype))
            {
                string strName = EnumDescription.GetFieldText(Enum.Parse(logtype, myCode.ToString()));//获取名称
                string strVaule = myCode.ToString();//获取值
                if (input.ToString().Equals(strVaule))
                {
                    resType = strName;
                    break;
                }
            }
            return resType;
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        protected string LogType(object input)
        {
            string logType = "aaa";
            if (input == null)
            {
                return string.Empty;
            }
            Type logtype = typeof(LogType);
            foreach (int myCode in Enum.GetValues(logtype))
            {
                string strName = EnumDescription.GetFieldText(Enum.Parse(logtype, myCode.ToString()));//获取名称
                string strVaule = myCode.ToString();//获取值
                if (RemoveRed(input.ToString()).Equals(strVaule))
                {
                    logType = strName;
                    break;
                }
            }
            if (logType == "aaa")
            {
                logType = input.ToString();
            }
            return logType;
        }

        /// <summary>
        /// 获取参数，设置属性
        /// </summary>
        private void SetProperty()
        {
            //this.PageNo = Tool.StructTrans.TransNum(Tool.NormalFunction.GetQueryString("page", "1"));
            this.PageNo = 1;
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
            InitData();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string index = ddlPageSize.SelectedValue;
            if (!string.IsNullOrEmpty(index))
            {
                switch (index)
                {
                    case "10":
                        PageSize = 10;
                        break;
                    case "20":
                        PageSize = 20;
                        break;
                    case "50":
                        PageSize = 50;
                        break;
                }
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
            message.Content = actionName;
            message.Content += result ? "成功" : "失败";
            message.MessageType = result ? DRMS.MirrorWeb.AdminUserControl.NotificationType.Success : DRMS.MirrorWeb.AdminUserControl.NotificationType.Error;

            SetSqlQueryCondition();
            BindUserList();
        }
        private bool BatchDelete(string keys)
        {
            if (string.IsNullOrWhiteSpace(keys))
            {
                return false;
            }
            bool result = false;
            IList<string> keyList = keys.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Log bll = new DRMS.BLL.Log();
            string sql = "";
            foreach (string key in keyList)
            {
                sql += "ID='" + key + "' OR ";
            }
            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 4);
            }
            result = bll.DeleteByWhere(sql);
            return result;
        }
    }
}