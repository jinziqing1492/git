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
    public partial class BookListView : BasePage.AdminBaseControl
    {
        #region 字段,属性
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo
        {
            get { return this.aspNetPager.CurrentPageIndex; }
            set { this.aspNetPager.CurrentPageIndex = value; }
        }

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
        public string SqlQueryCondition
        {
            get { return this.hid_SqlQueryCondition.Value; }
            set { this.hid_SqlQueryCondition.Value = value; }
        }
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
            string bookdoi = e.CommandArgument.ToString();


            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(bookdoi))
            {
                result.Visible = true;
                message.Visible = false;
                result.Content = "操作失败！";
                result.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }

            bookdoi = CNKI.BaseFunction.NormalFunction.ResetRedFlag(bookdoi);
            switch (type)
            {
                case "openRead":
                    {
                        bool issuccess = false;
                        Book bll = new Book();
                        BookInfo info = bll.GetItem(bookdoi);
                        if (info != null)
                        {
                            info.ReadType = 2;
                            issuccess = bll.Update(info);
                        }
                        if (issuccess)
                        {
                            message.Content = "操作成功！";
                            BllLogObj.Add(DataBaseType.BOOKTDATA, LogType.UPDATE, bookdoi, bookdoi, "禁用业余时间阅读成功");
                            message.MessageType = AdminUserControl.NotificationType.Success;
                            BindBookList();
                        }
                        else
                        {
                            message.Content = "操作失败！";
                            BllLogObj.Add(DataBaseType.USERDATA, LogType.UPDATE, bookdoi, bookdoi, "禁用业余时间阅读失败");
                            message.MessageType = AdminUserControl.NotificationType.Error;
                        }
                    }
                    break;
                case "closeRead":
                    {
                        bool issuccess = false;
                        Book bll = new Book();
                        BookInfo info = bll.GetItem(bookdoi);
                        if (info != null)
                        {
                            info.ReadType = 1;
                            issuccess = bll.Update(info);
                        }
                        if (issuccess)
                        {
                            message.Content = "操作成功！";
                            BllLogObj.Add(DataBaseType.BOOKTDATA, LogType.UPDATE, bookdoi, bookdoi, "禁用业余时间阅读成功");
                            message.MessageType = AdminUserControl.NotificationType.Success;
                            BindBookList();
                        }
                        else
                        {
                            message.Content = "操作失败！";
                            BllLogObj.Add(DataBaseType.USERDATA, LogType.UPDATE, bookdoi, bookdoi, "禁用业余时间阅读失败");
                            message.MessageType = AdminUserControl.NotificationType.Error;
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
            BindBookList();
            BindRecordCountLabel();
        }

        /// <summary>
        /// 绑定用户列表
        /// </summary>
        private void BindBookList()
        {
            Book bll = new Book();
            IList<BookInfo> bookList = bll.GetList(SqlQueryCondition, PageNo, PageSize, out recordCount,true);
            if (bookList == null || bookList.Count <= 0)
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

            this.repEntryList.DataSource = bookList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 设定查询条件（这里需要修改字段）
        /// </summary>
        private void SetSqlQueryCondition()
        {
            if (string.IsNullOrWhiteSpace(this.SqlQueryCondition))
            {
                this.SqlQueryCondition = " order by adddate desc";
            }
            this.aspNetPager.CurrentPageIndex = this.PageNo;

        }

        /// <summary>
        /// 绑定记录结果标签
        /// </summary>
        private void BindRecordCountLabel()
        {
            string content = string.Format("为您检索到 {0} 个记录，共 {1} 页，当前是第 {2} 页。", this.RecordCount, this.PageCount, this.PageNo);
            this.message.Content = content;
            this.message.Visible = true;
        }

        /// <summary>
        /// 获取参数，设置属性
        /// </summary>
        private void SetProperty()
        {
            //this.PageNo = 1;
        }

        /// <summary>
        /// 获取是否业余时间阅读的链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected string GetOpeateLink(object input)
        {
            if (input != null)
            {
                if (input.ToString() == "2")
                {
                    return "<a href='javascript:void(0)' style='color:red;'>禁止</a>";
                }
                else
                {
                    return "<a href='javascript:void(0)'>允许</a>";
                }
            }
            return "";
        }

        #endregion

        /// <summary>
        /// 分页
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