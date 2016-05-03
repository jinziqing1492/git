using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using System.Xml;

using DRMS.BLL;
using DRMS.Model;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class AdvancedDBThemeNav : System.Web.UI.Page
    {
        protected string Nodes { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTheme();
                getDisplayDbListFromXML();
            }
        }
        //绑定分类列表
        private void BindTheme()
        {
            Theme bll = new Theme();
            int allCount = 0;
            IList<ThemeInfo> list = bll.GetList(" order by ORDERNUM", 1, 1000, out allCount, true);
            if (allCount > 1000)
            {
                list = bll.GetList(" order by ORDERNUM", 1, allCount, out allCount, true);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (list != null && list.Count > 0)
            {
                foreach (ThemeInfo info in list)
                {
                    string id = info.ID;
                    string pID = info.ParentID;
                    string name = Tool.NormalFunction.GetSubStrOther(info.ThemeName, 30, "...");
                    sb.Append("{");
                    sb.Append("id:\"" + id + "\",");
                    sb.Append("pId:\"" + pID + "\",");
                    sb.Append("name:\"" + name + "\"");
                    sb.Append("},");
                }
            }
            //绑定所有数据
            sb.Append("{id:\"\",pId:\"\",name:\"所有资源\"}");
            sb.Append("]");
            Nodes = sb.ToString();
        }

        /// <summary>
        /// 从配置文件中读取可展示的数据库信息
        /// </summary>
        private void getDisplayDbListFromXML()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbList");
            StringBuilder htmlAppender = new StringBuilder();
            if (mylist != null)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    string dbname = mylist[i].Attributes["dname"].Value.Replace("库", "");
                    htmlAppender.AppendFormat("<li id='{0}'><a href='#'>{1}</a></li> ", mylist[i].Attributes["dtype"].Value, dbname);
                }
            }
            this.ltlbasedatabase.Text = htmlAppender.ToString();
        }

    }
}