using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.Model;
using CNKI.BaseFunction;
using DRMS.BLL;

namespace DRMS.MirrorWeb.view
{
    public partial class LogicalDataBaseList : System.Web.UI.Page
    {
        public string DataBaseName { get; set; }
        protected LogicalDataBase _ldb = new LogicalDataBase();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ldbID = this.Request["id"];
                if (!string.IsNullOrEmpty(ldbID))
                {
                    //存储DOI
                    LogicalDataBaseInfo ldbi = _ldb.GetItem(ldbID);
                    if (ldbi == null)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "加载失败", "库信息加载失败！");
                        return;
                    }
                    this.DataBaseName = ldbi.DbName;
                }
            }
        }
    }
}