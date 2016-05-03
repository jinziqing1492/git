using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

using DRMS.Model;
using DRMS.BLL;
using DRMS.MirrorWeb.Utility;

namespace DRMS.MirrorWeb.view
{
    public partial class UserMoreAppDBList : System.Web.UI.Page
    {
        LogicalDataBase _lddal = new LogicalDataBase();
        Log _l = new Log();
        int recordcount = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //逻辑库需要动态的提取
                bindData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void bindData()
        {
            int recordcount = 0;
            //获取发布状态的逻辑库
            IList<LogicalDataBaseInfo> mylist = _lddal.GetList("SYS_FLD_CHECK_STATE=-1", 1, 100, out recordcount, false);
            if (mylist != null && mylist.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                string FormatA = "<li><a href=\"{0}\"><img src=\"../images/TYdatabaseIcon.png\"><h5>{1}</h5></a></li>";
                for (int i = 0; i < mylist.Count; i++)
                {
                    str.AppendFormat(FormatA, "LogicalDataBaseList.aspx?id=" + mylist[i].DbId, mylist[i].DbName);
                }
                ltlldlist.Text = str.ToString();
            }
        }
    }
}