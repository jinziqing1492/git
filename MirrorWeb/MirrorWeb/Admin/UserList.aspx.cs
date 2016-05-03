using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UsersList.SqlQueryCondition = this.hidQueryCondition.Value;
            if (!IsPostBack)
            {
                this.UsersList.SqlQueryCondition = " order by adddate desc";
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
                sqlQueryCondition = sqlQueryCondition + " order by adddate desc";
            }
            else
            {
                sqlQueryCondition = " order by adddate desc";
            }
            this.UsersList.PageNo = 1;
            this.UsersList.SqlQueryCondition = sqlQueryCondition;
            this.UsersList.InitData();
        }
    }
}