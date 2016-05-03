using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class OwnerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.OwnList.SqlQueryCondition = this.hidQueryCondition.Value;
            if (!IsPostBack)
            {
                this.OwnList.SqlQueryCondition = " order by sys_sysid desc";
            }
        }

        /// <summary>
        /// 检索按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlQueryCondition = this.hidQueryCondition.Value;
            if (!string.IsNullOrEmpty(sqlQueryCondition))
            {
                sqlQueryCondition = sqlQueryCondition + " order by sys_sysid desc";
            }
            else
            {
                sqlQueryCondition = " order by sys_sysid desc";
            }
            this.OwnList.PageNo = 1;
            this.OwnList.SqlQueryCondition = sqlQueryCondition;
            this.OwnList.InitData();
        }
    }
}