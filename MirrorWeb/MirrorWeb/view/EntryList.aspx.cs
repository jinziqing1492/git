using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.Model;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class EntryList : System.Web.UI.Page
    {
        public string BookID { get; set; }

        public string DataBaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BookID = CNKI.BaseFunction.NormalFunction.GetQueryString("bookid", "");
            if (!IsPostBack)
            {
                BindData();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            string dbtype = NormalFunction.GetQueryString("dbtype",DataBaseType.ENTRYDATA.GetHashCode().ToString());
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(dbtype);
            string sql = mydbtype.GetHashCode().ToString();
            DataBaseName = EnumDescription.GetFieldText(mydbtype);
            hdnQueryCon.Value = sql;
            string key = Request.QueryString["searchword"];
            hdnSearchWord.Value = key;
        }
    }
}