using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.MirrorWeb.Utility;

namespace AdminKNReader
{
    public partial class _Default : System.Web.UI.Page
    {
        public string mBookid = string.Empty;
        public string mType = string.Empty;
        public string page = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                mBookid = Request.QueryString["doi"];
                mType = Request.QueryString["type"];
                page = Request.QueryString["page"];

                if(string.IsNullOrWhiteSpace(page))
                {
                    page = "1";
                }
                if (string.IsNullOrEmpty(mBookid))
                {
                    Utility.AlertMessageCloseWindow("没有找到PDF文件!");
                }
            }
                
        }
    }
}
