using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

using DRMS.Model;
using DRMS.BLL;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class DataBaseListNavView : System.Web.UI.UserControl
    {
        //这种具有资源管理员权限的页面 需要统一加权限限制
        string Page_URL = "../view/LogicalDataBaseList.aspx?id=";
        string href_URL = "../view/DataBaseList.aspx?dbtype=";
        protected void Page_Load(object sender, EventArgs e)
        {
            getDisplayDbListFromXML();
        }

        /// <summary>
        /// 从配置文件中读取可展示的数据库信息
        /// </summary>
        /// <returns></returns>
        private void getDisplayDbListFromXML()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            StringBuilder htmlAppender = new StringBuilder();
            if (mylist != null)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    string FormatA = "<li><a id='{0}' href='{1}'>{2}</a></li>";
                    htmlAppender.AppendFormat(FormatA, mylist[i].Attributes["dtype"].Value, href_URL + mylist[i].Attributes["dtype"].Value, mylist[i].Attributes["dname"].Value);
                }

            }
            this.ltlbasedatabaseview.Text = htmlAppender.ToString();
        }

    }
}