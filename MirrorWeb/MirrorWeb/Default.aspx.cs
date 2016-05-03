using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using DRMS.BLL;
using DRMS.Model;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb
{
    public partial class Default : BasePage.BasePage
    {
        public string NewLogin = ConfigurationManager.AppSettings["NewLogin"];
        protected string Nodes { get; set; }
        protected string ClassofElectricalfence = ConfigurationManager.AppSettings["ClassofElectricalfence"].ToString();
        protected string ClassofElectronics = ConfigurationManager.AppSettings["ClassofElectronics"].ToString();
        protected string ClassofStd = ConfigurationManager.AppSettings["ClassofStd"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTheme();
            if (!IsPostBack)
            {
                BindData();
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

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {            
            string key = Request.QueryString["searchword"];
            key = HttpUtility.UrlEncode(key);
            hdnSearchWord.Value = key;
        }

    }
}