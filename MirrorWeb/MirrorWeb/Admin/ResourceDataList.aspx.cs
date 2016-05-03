using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class ResourceDataList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DataList.SqlQueryCondition = this.hidQueryCondition.Value;
                this.DataList.SqlQueryCondition = " order by CreateTime desc";
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
                sqlQueryCondition = sqlQueryCondition + " order by CreateTime desc";
            }
            else
            {
                sqlQueryCondition = " order by CreateTime desc";
            }
            this.DataList.PageNo = 1;
            this.DataList.SqlQueryCondition = sqlQueryCondition;
            this.DataList.InitData();
        }
    }
}