using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.BLL;
using DRMS.Model;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class SupportDetail : System.Web.UI.Page
    {
        protected string DataBaseName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int type = StructTrans.TransNum(Request.QueryString["dbtype"]);
            this.hdnResDoi.Value = Request.QueryString["doi"];

            string uvPath = "";

            switch (type)
            {
                case 13:
                    uvPath = "../AdminUserControl/ucContractView.ascx";
                    DataBaseName = "合同库";
                    break;
                case 14:
                    uvPath = "../AdminUserControl/ucAuthorView.ascx";
                    DataBaseName = "作者库";
                    break;
                case 15:
                    uvPath = "../AdminUserControl/ucOrganView.ascx";
                    DataBaseName = "机构库";
                    break;
                case 28:
                    uvPath = "../AdminUserControl/ucOriginalClassView.ascx";
                    DataBaseName = "原始资料库";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(uvPath))
            {
                System.Web.UI.UserControl uc = new System.Web.UI.UserControl();//声明一个用户自定义控件对象
                uc = (System.Web.UI.UserControl)Page.LoadControl(uvPath);//通过相对路径实例化自定义控件
                PanelUCContent.Controls.Clear();//清空容器控件
                PanelUCContent.Controls.Add(uc);//加载控件
            }
        }
    }
}