using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

using DRMS.Model;
using DRMS.BLL;
using Tool=CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class EntryDetail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doi = Tool.NormalFunction.GetQueryString("doi", "0");
                BindDetail(doi);
            }
        }

        /// <summary>
        /// 根据当前词条doi绑定子词条导航控件
        /// </summary>
        /// <param name="currentEntryDoi">当前词条doi</param>
        /// <returns></returns>
        private void BindDetail(string currentEntryDoi)
        {
            ctrl_tree.YearIssueDoi = currentEntryDoi;//设置树控件的属性
            ctrl_tree.SelectID = currentEntryDoi;
            ctrl_tree.dbtybe = "22";
        }
    }
}