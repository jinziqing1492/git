using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class DatabaseMView : System.Web.UI.UserControl
    {
        LogicalDataBase _lddal = new LogicalDataBase();
        string href_URL = "../view/DataBaseList.aspx?dbtype=";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            bindData();
            getDisplayDbListFromXML();
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
                    str.AppendFormat(FormatA, "/view/LogicalDataBaseList.aspx?id=" + mylist[i].DbId, mylist[i].DbName);
                }
                //string role = Utility.Utility.GetRole();

                //if(role=="5")
                //{
                //    string FormatAdd = "<li><a href=\"{0}\"><img src=\"../images/TYdatabaseadd.gif\"><h5>{1}</h5></a></li>";
                //    str.AppendFormat(FormatAdd, "/auditadmin/AddAppDataBase.aspx","添加");
                //}
              ltllogdb.Text = str.ToString();
            }
        }

        /// <summary>
        /// 从配置文件中读取可展示的数据库信息
        /// </summary>
        /// <returns></returns>
        private void getDisplayDbListFromXML()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbMList");
            StringBuilder htmlAppender = new StringBuilder();
            if (mylist != null)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    string FormatA = "<li id={0} ><a href='{1}'><img src='../images/TYdatabaseIcon.png' /></a><h5>{2}</h5><p>共有<em>32</em>条数据</p></li>";
                    htmlAppender.AppendFormat(FormatA, mylist[i].Attributes["dtype"].Value, href_URL + mylist[i].Attributes["dtype"].Value, mylist[i].Attributes["dname"].Value);
                }

            }
            this.ltlbasedatabase.Text = htmlAppender.ToString();
        }
    }
}