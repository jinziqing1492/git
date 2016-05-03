using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class ResourceTypeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TypeList.SqlQueryCondition = this.hidQueryCondition.Value;
                this.TypeList.SqlQueryCondition = " order by sys_fld_adddate desc";
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
                sqlQueryCondition = sqlQueryCondition + " order by createtime desc";
            }
            else
            {
                sqlQueryCondition = " order by sys_fld_adddate desc";
            }
            this.TypeList.PageNo = 1;
            this.TypeList.SqlQueryCondition = sqlQueryCondition;
            this.TypeList.InitData();
        }
    }
}