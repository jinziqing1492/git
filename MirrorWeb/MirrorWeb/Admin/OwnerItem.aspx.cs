using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.Admin
{
    public partial class OwnerItem : System.Web.UI.Page
    {
        /// <summary>
        /// 判断是否为编辑
        /// </summary>
        private bool IsEdit
        {
            get { return !string.IsNullOrEmpty(Request.QueryString["baseid"]); }
        }

        Journal bll = new Journal("owner");
        OwnerResourceType typeBll = new OwnerResourceType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
                string baseid = Request.QueryString["baseid"];
                if (IsEdit)
                {
                    BindData(baseid);
                }
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            //绑定下拉框
            int recordCount=0;
            IList<OwnerResourceTypeInfo> list = typeBll.GetList("", 1, 100, out recordCount);
            if (list != null && list.Count > 0)
            {
                foreach(OwnerResourceTypeInfo info in list)
                {
                    ddl_Type.Items.Add(new ListItem()
                    {
                        Text = info.NAME,
                        Value = info.BASEID
                    });
                }
            }
        }

        /// <summary>
        /// 绑定数据 修改时才会起作用
        /// </summary>
        private void BindData(string baseid)
        {
            //BASEID不能修改
            tbxCodeName.Enabled = false;

            JournalInfo info = bll.GetItem(baseid);
            if (info != null)
            {
                tbxCodeName.Text = baseid;
                tbxName.Text = info.CNAME;
                tbxRemark.Text = info.Description;
                ddl_Type.SelectedValue = info.SYS_FLD_CLASSFICATION;
            }
            else //未获取到数据
            {
                tbxCodeName.Text = baseid;
                message.Visible = true;
                message.Content = "未找到数据，请稍后重试。";
                message.MessageType = AdminUserControl.NotificationType.Error;
            }
        }

        /// <summary>
        /// 添加或修改自有资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string baseid = tbxCodeName.Text;
            string name = tbxName.Text;
            string remark = tbxRemark.Text;
            string ownerType = ddl_Type.SelectedValue;
            //判断是否为空
            if (string.IsNullOrEmpty(baseid) || string.IsNullOrEmpty(name) || ownerType == "0")
            {
                message.Visible = true;
                message.Content = "必填项不能为空。";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            //判断是否是标准  如果是标准则需要建立到其它的表中
            JournalInfo info = bll.GetItem(baseid);
            if (!IsEdit)
            {
                //判断BaseID是否重复
                if (info != null)
                {
                    message.Visible = true;
                    message.Content = "该标示已存在。";
                    message.MessageType = AdminUserControl.NotificationType.Error;
                    return;
                }
                else
                {
                    info = new JournalInfo();
                }
            }
            if (info == null)
            {
                message.Visible = true;
                message.Content = "未获取到资源 操作失败。";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            info.BASEID = baseid;
            info.CNAME = name;
            info.Description = remark;
            info.SYS_FLD_CLASSFICATION = ownerType;
            info.SYS_FLD_CHECK_STATE = -1;


            //若为编辑则使用Update方法 创建则使用Add方法
            bool isSuccess = IsEdit ? bll.Update(info) : bll.Add(info);
            if (!isSuccess)
            {
                message.Visible = true;
                message.Content = "操作失败。";
                message.MessageType = AdminUserControl.NotificationType.Error;
            }
            else
            {
                //跳转到列表页
                Response.Redirect("OwnerList.aspx");
            }
        }

        /// <summary>
        /// 验证标示是否已存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnCheckCode_Click(object sender, EventArgs e)
        {
            string code = tbxCodeName.Text;
            JournalInfo info = bll.GetItem(code);
            if (info == null)
            {
                message.Visible = true;
                message.Content = "该标示可用";
                message.MessageType = AdminUserControl.NotificationType.Success;
            }
            else
            {
                message.Visible = true;
                message.Content = "该标示已存在。";
                message.MessageType = AdminUserControl.NotificationType.Error;
            }
        }
    }
}