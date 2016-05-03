using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRMS.BLL;
using DRMS.Model;


namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class ResourceDataListView:BasePage.AdminBaseControl
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
            string doi = e.CommandArgument.ToString();


            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(doi))
            {
                result.Visible = true;
                message.Visible = false;
                result.Content = "操作失败！";
                result.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }

            doi = CNKI.BaseFunction.NormalFunction.ResetRedFlag(doi);
            switch (type)
            {
                case "Delete":
                    {
                        ResourceData bll = new ResourceData();
                        bool issuccess = bll.Delete(doi);
                        if (issuccess)
                        {
                            message.Content = "操作成功！";
                            BllLogObj.Add(DataBaseType.RESOURCETYPE, LogType.DELETE, doi, doi, "删除资源");
                            message.MessageType = AdminUserControl.NotificationType.Success;
                            BindResourceTypeList();
                        }
                        else
                        {
                            message.Content = "操作失败！";
                            BllLogObj.Add(DataBaseType.RESOURCETYPE, LogType.DELETE, doi, doi, "删除资源");
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
            BindResourceTypeList();
            BindRecordCountLabel();
        }

        /// <summary>
        /// 绑定用户列表
        /// </summary>
        private void BindResourceTypeList()
        {
            ResourceData bll = new ResourceData();
            IList<ResourceDataInfo> typeList = bll.GetList(SqlQueryCondition, PageNo, PageSize, out recordCount, true);
            if (typeList == null || typeList.Count <= 0)
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

            this.repEntryList.DataSource = typeList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 设定查询条件（这里需要修改字段）
        /// </summary>
        private void SetSqlQueryCondition()
        {
            if (string.IsNullOrWhiteSpace(this.SqlQueryCondition))
            {
                this.SqlQueryCondition = " order by createtime desc";
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