using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using DRMS.Model;
using System.Web.Security;

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class HeadView : System.Web.UI.UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断用户登录信息是否已经过期，已经过期后将其踢出系统。如果没有过期则绑定前台用户
                if (!SingleLogin.isReLogin())
                {
                    BindUser();
                }
                else
                {
                    FormsAuthentication.SignOut();
                    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(authCookie);
                }
            }
            else
            {
                BindUser();
            }
        }

        private void BindUser()
        {
            RoleName = "您好:<a href='/UserCenter/UserCenter.aspx'>" + Utility.Utility.GetUserName() + "</a>"
                + "<a href='/UserCenter/UserCenter.aspx' style='color:#fea500'>进入用户中心</a>";
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            string MissionFormat = "<li><a id=\"Mission\" href=\"{0}\"><img alt=\"\" src=\"../images/NAV_Mytask.png\" /><h3>我的任务</h3></a></li>";
            string ResMFormat = "<li><a id=\"DataManagement\" href=\"{0}\"><img alt=\"\" src=\"../images/NAV_data.png\" /><h3>数据管理</h3></a></li>";
            string SysAdmin = "<li><a id=\"SysManagement\" href=\"{0}\"><img alt=\"\" src=\"../images/NAV_Sys.png\" /><h3>系统管理</h3></a></li>";

            string Role = Utility.Utility.GetRole();
            UserRole myrole = (UserRole)CNKI.BaseFunction.StructTrans.TransNum(Role);
            switch (myrole)
            {
                case UserRole.ASSIGNTASKUSER:
                case UserRole.AUDITCOLLECTUSER:
                case UserRole.AUDITPROCESSUSER:
                case UserRole.COLLECTUSER:
                case UserRole.PROCESSUSER:
                    ltlmytask.Text = string.Format(MissionFormat, TaskUrl());
                    break;
                case UserRole.RESADMIN:
                    ltldataM.Text = string.Format(ResMFormat, TaskUrl());
                    break;
                case UserRole.ADMIN:
                    ltldataM.Text = string.Format(SysAdmin, TaskUrl());
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// 获取任务 列表地址
        /// </summary>
        /// <returns></returns>
        protected string TaskUrl()
        {
            string Url = "";
            string Role = Utility.Utility.GetRole();
            UserRole myrole = (UserRole)CNKI.BaseFunction.StructTrans.TransNum(Role);
            switch (myrole)
            {
                case UserRole.COLLECTUSER:
                    Url = "../auditadmin/ResourceType.aspx";
                    break; 
                case UserRole.AUDITCOLLECTUSER:
                    Url = "../auditadmin/ExaminingRes.aspx?resEx=0&at=c";
                    break;
                case UserRole.PROCESSUSER:
                    Url = "../auditadmin/ProcessTask.aspx?scf=0&at=p";
                    break;
                case UserRole.AUDITPROCESSUSER:
                    Url = "../auditadmin/ExaminingRes.aspx?resEx=0&at=p";
                    break;
                case UserRole.ASSIGNTASKUSER:
                    Url = "../auditadmin/AssignTask.aspx?tmst=ppp";
                    break;
                case UserRole.RESADMIN:
                    Url = "../auditadmin/BaseDataBase.aspx";
                    break;
                case UserRole.ADMIN:
                    Url = "../sysadmin/SysAdminDefault.aspx";
                    break;
                default:
                    break;
            }
            return Url;
        }

    }
}
