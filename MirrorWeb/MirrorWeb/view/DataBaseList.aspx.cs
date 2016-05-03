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
    public partial class DataBaseList : System.Web.UI.Page
    {
        public string DataBaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string dbtype = NormalFunction.GetQueryString("dbtype", "1");

            if (dbtype == "1")
            {
                this.book_radio.Visible = true;
                this.chapter_radio.Visible = true;
            }
            else
            {
                this.book_radio.Visible = false;
                this.chapter_radio.Visible = false;
            }
            if (dbtype == "2" || dbtype == "17")
            {
                this.std_radio.Visible = true;
                this.stdchapter_radio.Visible = true;
            }
            else
            {
                this.std_radio.Visible = false;
                this.stdchapter_radio.Visible = false;
            }
            if (dbtype == "5" || dbtype == "19")
            {
                this.Conference_radio.Visible = true;
                this.article_radio.Visible = true;
            }
            else
            {
                this.Conference_radio.Visible = false;
                this.article_radio.Visible = false;
            }
            if (dbtype == "3" || dbtype == "22")
            {
                this.toolbook_radio.Visible = true;
                this.entry_radio.Visible = true;
            }
            else
            {
                this.toolbook_radio.Visible = false;
                this.entry_radio.Visible = false;
            }
            if (dbtype == "4" || dbtype == "18")
            {
                this.journal_radio.Visible = true;
                this.journal_article_radio.Visible = true;
            }
            else
            {
                this.journal_radio.Visible = false;
                this.journal_article_radio.Visible = false;
            }

            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(dbtype);
            string sql = mydbtype.GetHashCode().ToString();
            DataBaseName = EnumDescription.GetFieldText(mydbtype);
            hdnQueryCon.Value = sql;
            string key = Request.QueryString["searchword"];
            hdnSearchWord.Value = key;
        }
    }
}