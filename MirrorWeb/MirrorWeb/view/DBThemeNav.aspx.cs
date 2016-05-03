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
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class DBThemeNav : System.Web.UI.Page
    {
        protected string Nodes { get; set; }
        protected bool IsHaveOwner { get; set; }
        protected bool IsDisplayOwner { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTheme();
                getDisplayDbListFromXML();
            }
            BindOwner();
        }

        /// <summary>
        /// 绑定自有资源的隐藏显示
        /// </summary>
        private void BindOwner()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            if (mylist != null && mylist.Count > 0)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    int tableft = 94 * i;
                    string dbname = mylist[i].Attributes["dname"].Value.Replace("库", "");
                    if (dbname == "泛华资源")
                    {
                        IsDisplayOwner = true;
                    }
                }
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
                    string name = info.ThemeName;// Tool.NormalFunction.SubString(info.ThemeName, 12, "...");
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
        /// <returns></returns>
        private void getDisplayDbListFromXML()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            StringBuilder htmlAppender = new StringBuilder();
            StringBuilder divTabsAppender = new StringBuilder();
            if (mylist != null)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    string dbname = mylist[i].Attributes["dname"].Value.Replace("库", "");
                    string FormatA = "<li style='padding: 0px;'><a field='{0}' href='{1}'>{2}</a></li>";
                    htmlAppender.AppendFormat(FormatA, mylist[i].Attributes["dtype"].Value, "#tabs-" + mylist[i].Attributes["dtype"].Value, dbname);
                    divTabsAppender.AppendFormat("<div id='{0}' class='search-result-content'></div>", "tabs-" + mylist[i].Attributes["dtype"].Value);
                }

            }
            this.ltlDivTabs.Text = divTabsAppender.ToString();
            this.ltlbasedatabaseview.Text = htmlAppender.ToString();
        }
    }
}