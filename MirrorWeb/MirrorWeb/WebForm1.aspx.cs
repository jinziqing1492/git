using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRMS.BLL;
using DRMS.Model;


namespace DRMS.MirrorWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int recordCount1;
            picList = new DRMS.BLL.Pic().GetList("", 1, 10, out recordCount1, true);

            IList<ThemeInfo> mylist = null;
           // Theme reference = new Theme();
            DRMS.BLL.Theme reference = new BLL.Theme();
            int recordCount = 0;
            string sql = "";
            mylist = reference.GetListKspider(sql, 1, 1000, out recordCount, true);

            int i = 0;
            //LogicalDataBase bll = new LogicalDataBase();
            //bll.SetIsOnline("0718e04b-a775-4ef2-bd21-e5904b005098","1",DateTime.Now.ToString());
        }

        public IList<PicInfo> picList { get; set; }
    }
}