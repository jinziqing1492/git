using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class OwnerBookList : System.Web.UI.Page
    {
        private string BaseID
        {
            get { return Request.QueryString["baseid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.OwnBookList.SqlQueryCondition = this.hidQueryCondition.Value;
            this.OwnBookList.SqlQueryCondition = "BASEID='" + BaseID + "' order by sys_fld_adddate desc";
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
                sqlQueryCondition = sqlQueryCondition + " AND BASEID='" + BaseID + "' order by sys_fld_adddate desc";
            }
            else
            {
                sqlQueryCondition = " AND BASEID='" + BaseID + "' order by sys_fld_adddate desc";
            }
            this.OwnBookList.PageNo = 1;
            this.OwnBookList.SqlQueryCondition = sqlQueryCondition;
            this.OwnBookList.InitData();
        }
    }
}