using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class BookList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BooksList.SqlQueryCondition = this.hidQueryCondition.Value;
                this.BooksList.SqlQueryCondition = " order by sys_fld_adddate desc";
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
                sqlQueryCondition = sqlQueryCondition + " order by sys_fld_adddate desc";
            }
            else
            {
                sqlQueryCondition = " order by sys_fld_adddate desc";
            }
            this.BooksList.PageNo = 1;
            this.BooksList.SqlQueryCondition = sqlQueryCondition;
            this.BooksList.InitData();
        }
    }
}