using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.Admin
{
    public partial class OwnerTypeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.OwnList.SqlQueryCondition = " order by sys_fld_adddate";
            }
        }
    }
}