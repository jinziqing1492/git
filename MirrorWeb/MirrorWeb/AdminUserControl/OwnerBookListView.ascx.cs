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
    public partial class OwnerBookListView : BasePage.AdminBaseControl
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

        /// <summary>
        /// 书籍操作对象
        /// </summary>
        private JournalYear bll = new JournalYear("owner");
        #endregion

        #region 事件

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetProperty();
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// Repeater控件 发生点击事件后的命令
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 分页控件点击事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void aspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.PageNo = e.NewPageIndex;
            InitData();
        }

        /// <summary>
        /// 设置每页显示多少条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 批量操作按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            BindOwnerBookList();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        internal void InitData()
        {
            SetSqlQueryCondition();
            BindOwnerBookList();
            BindRecordCountLabel();
        }

        /// <summary>
        /// 绑定书籍列表
        /// </summary>
        private void BindOwnerBookList()
        {
            IList<JournalYearInfo> OwnerBookList = bll.GetList(SqlQueryCondition, PageNo, PageSize, out recordCount, true);
            if (OwnerBookList == null || OwnerBookList.Count <= 0)
            {
                this.repEntryList.DataSource = new DataTable();
                this.repEntryList.DataBind();
                this.aspNetPager.RecordCount = 0;
                return;
            }
            this.aspNetPager.RecordCount = this.RecordCount;

            if (this.PageNo > this.PageCount)
            {
                this.PageNo = this.PageCount;
            }

            this.repEntryList.DataSource = OwnerBookList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 设定查询条件（这里需要修改字段）
        /// </summary>
        private void SetSqlQueryCondition()
        {
            if (string.IsNullOrWhiteSpace(this.SqlQueryCondition))
            {
                this.SqlQueryCondition = " order by sys_fld_adddate desc";
            }
            else
            {
                if (this.SqlQueryCondition.IndexOf("order by") < 0)
                {
                    this.SqlQueryCondition += " order by sys_fld_adddate desc";
                }
            }
            this.aspNetPager.CurrentPageIndex = this.PageNo;

        }

        /// <summary>
        /// 绑定记录结果标签
        /// </summary>
        private void BindRecordCountLabel()
        {
            string content = string.Format("为您检索到 {0} 个资源，共 {1} 页，当前是第 {2} 页。", this.RecordCount, this.PageCount, this.PageNo);
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
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        private bool BatchDelete(string keys)
        {
            if (string.IsNullOrWhiteSpace(keys))
            {
                return false;
            }
            bool result = false;
            IList<string> keyList = keys.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string sql = "";
            foreach (string key in keyList)
            {
                sql += "SYS_FLD_DOI='" + key + "' OR ";
            }
            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 4);
            }
            result = bll.DeleteByWhere(sql);
            return result;
        }

        #endregion
    }
}