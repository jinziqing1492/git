using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

namespace DRMS.MirrorWeb.view
{
    public partial class AdvancedDBContent : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdvancedDBControl1.Type = (string.IsNullOrEmpty(Request["type"])) ? "1" : Request["type"];
            }
        }

       
    }
}