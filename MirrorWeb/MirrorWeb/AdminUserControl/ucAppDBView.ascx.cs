using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Xml;

using DRMS.Model;
using DRMS.BLL;
using DRMS.MirrorWeb.Utility;

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class ucAppDBView : System.Web.UI.UserControl
    {
        protected LogicalDataBase _ldb = new LogicalDataBase();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["view"] == "1")
            {
                this.LiteralEditDB.Text = "<div class=\"TYCRTextTitle_arTabTitle EditDBInfoDiv\">"
                    + "<div class=\"TYCRTextTitle_left fl\"></div>"
                    + "<div class=\"TYCRTextTitle_center fl\">编辑库</div>"
                    + "<div class=\"TYCRTextTitle_right fl\"></div>"
                    + "</div>";
            }

            if (!this.IsPostBack)
            {
                string ldbID = this.Request["id"];
                if (!string.IsNullOrEmpty(ldbID))
                {
                    //存储DOI
                    this.hdnLogicDBID.Value = ldbID;
                    LogicalDataBaseInfo ldbi = _ldb.GetItem(ldbID);
                    if (ldbi == null)
                    {
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "加载失败", "库信息加载失败！");
                        return;
                    }
                    this.DBNAME.InnerText = ldbi.DbName;
                    this.DBTYPE.InnerText = ldbi.Dbtype == 1 ? "专题库" : "产品库";
                    this.DBDESCRIPTION.InnerText = ldbi.DbDescription;
                    this.REMARK.InnerText = ldbi.Remark;

                    string coversrc = FileManagementUtility.GetCoverVirPathByResDoi(DataBaseType.LOGICALDATABASE, ldbi.DbId);
                    if (string.IsNullOrEmpty(coversrc))
                        coversrc = "../images/zanwu.jpg";
                    //封面图片
                    this.bookcover_img.Attributes["src"] = coversrc;
                }
            }
        }
    }
}