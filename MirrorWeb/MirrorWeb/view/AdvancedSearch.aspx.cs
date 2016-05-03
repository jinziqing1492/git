using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using System.Configuration;

using DRMS.Model;
using DRMS.BLL;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class AdvancedSearch : BasePage.BasePage
    {
        protected string Nodes { get; set; }
        protected string ClassofElectricalfence = ConfigurationManager.AppSettings["ClassofElectricalfence"].ToString();
        protected string ClassofElectronics = ConfigurationManager.AppSettings["ClassofElectronics"].ToString();
        protected string ClassofStd = ConfigurationManager.AppSettings["ClassofStd"].ToString();
        protected string KeyWordUrlFormat = "<a  href=\"../Default.aspx?searchword={0}\" target=\"_blank\" >{1}</a>";

        protected void Page_Load(object sender, EventArgs e)
        {
            BindTheme();
            if (!IsPostBack)
            {

            }
        }
        //绑定分类列表
        private void BindTheme()
        {
            Theme bll = new Theme();
            int allCount = 0;
            IList<ThemeInfo> list = bll.GetList("", 1, 1000, out allCount, true);
            if (allCount > 1000)
            {
                list = bll.GetList("", 1, allCount, out allCount, true);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (list != null && list.Count > 0)
            {
                foreach (ThemeInfo info in list)
                {
                    string id = info.ID;
                    string pID = info.ParentID;
                    string name = Tool.NormalFunction.SubString(info.ThemeName, 12, "...");
                    sb.Append("{");
                    sb.Append("id:\"" + id + "\",");
                    sb.Append("pId:\"" + pID + "\",");
                    sb.Append("name:\"" + name + "\"");
                    if (pID == "0")
                    {
                        sb.Append(",open:\"true\"");
                    }
                    sb.Append("},");
                }
            }
            //绑定所有数据
            sb.Append("{id:\"\",pId:\"\",name:\"所有资源\"}");
            sb.Append("]");
            Nodes = sb.ToString();
        }

        protected void QueryBtn_Click(object sender, EventArgs e)
        {
            string queryCon = "";
            //获取检索条件
            if (!String.IsNullOrEmpty(this.bTitle.Text.Trim()))
                queryCon += (String.IsNullOrEmpty(queryCon) ? "" :" AND" )+ "TITLE='" +  this.bTitle.Text.Trim() + "' ";
            if (!String.IsNullOrEmpty(this.bISBN.Text.Trim()))
                queryCon += (String.IsNullOrEmpty(queryCon) ? "" : " AND") + " ISBN='?" + this.bISBN.Text.Trim() + "' ";
            if (!String.IsNullOrEmpty(this.bAuthor.Text.Trim()))
                queryCon += (String.IsNullOrEmpty(queryCon) ? "" : " AND") + " AUTHOR='?" + this.bAuthor.Text.Trim() + "' ";
            if (!String.IsNullOrEmpty(this.bKeyWord.Text.Trim()))
                queryCon += (String.IsNullOrEmpty(queryCon) ? "" : " AND") + " KEYWORDS='?" + this.bKeyWord.Text.Trim() + "' ";
            if (!String.IsNullOrEmpty(this.bPubDep.Text.Trim()))
                queryCon += (String.IsNullOrEmpty(queryCon) ? "" :" AND") + " ISSUEDEP='?" + this.bPubDep.Text.Trim() + "' ";

            hdnQueryCon.Value = HttpUtility.UrlEncode(queryCon);
            BindTheme();

        }
    }
}