using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.view
{
    public partial class DatabaseContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request["type"];
                string keyWord = Request["keyword"] ?? "";
                keyWord = Server.UrlDecode(keyWord);
                string sqlConn = Request["queryConn"] ?? "";
                sqlConn = Server.UrlDecode(sqlConn);
                string classid = Request["classid"];
                string scode = Request["scode"];
                string searchWord = Request["searchword"] ?? "";
                searchWord = Server.UrlDecode(searchWord);
                string bookid = Request["bookid"];
                string power = Request["power"];
                string doctype = Request["doctype"];
                string selectValue = Request["selectValue"];
                string orderValue = Request["orderField"];
                string second = Request["second"];
                string owner = Request["owner"];
                string secondwhere = Request["secondwhere"];
                if (!string.IsNullOrEmpty(sqlConn))
                {
                    sqlConn = sqlConn.Replace("#CNKI_AND", "*");
                    sqlConn = sqlConn.Replace("#CNKI_OR", "+");
                    sqlConn = sqlConn.Replace("#CNKI_NOT", "-");
                }

                if (!string.IsNullOrEmpty(classid) && classid != "undefined")
                {
                    //if (classid.StartsWith(Start_Tag))
                    //{
                    //    classid = scode;
                    //}
                    sqlConn = string.IsNullOrEmpty(sqlConn) ? "SYS_FLD_CLASSFICATION='" + classid + "?'" : sqlConn +" AND SYS_FLD_CLASSFICATION='" + classid + "?'";
                }

                if (searchWord == "undefined")
                {
                    searchWord = "";
                }
                if (power == "undefined")
                {
                    power = "";
                }
                //绑定数据
                ctrl_Search.Type = type;
                ctrl_Search.KeyWord = keyWord;
                ctrl_Search.sqlEntry = sqlConn;
                ctrl_Search.SearchWord = searchWord;
                ctrl_Search.Bookid = bookid;
                ctrl_Search.PowerId = power;
                ctrl_Search.DocType = doctype;
                ctrl_Search.SelectValue = selectValue;
                ctrl_Search.OrderField = orderValue;
                ctrl_Search.SecondSearch = second == "1";
                ctrl_Search.SqlWhereCondition = secondwhere;
                ctrl_Search.OwnerSearch = owner == "1";
            }
        }
    }
}