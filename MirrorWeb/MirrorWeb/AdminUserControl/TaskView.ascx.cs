using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using DRMS.BLL;
using DRMS.Model; 

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class TaskView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        /// <summary>
        /// 根据角色去划分 1 收集人员 2 收集审核人员 3 加工人员 4 加工审核 5 资源管理员 7 任务分配管理员
        /// </summary>
        protected void BindData()
        { 
        /**
         * 1、收集人员 待收集 完成未完成的收集 并创建任务
         * 2、收集审核员 就是查看任务表 查出符合条件的任务记录 待审核的任务 （如果通过 分配给 加工和审核的用户）
         * 3、加工人员 登陆后就查看 符合加工中的任务列表
         * 4、加工审核 加工审核人员 就查分给自己的加工完成 待审核的任务表里的 任务项 如果合格就入库发布状态
         * 5、系统资源管理员  所有的都能查看，并且还可以将某些资源打回进行 二次编辑
         * 7、任务分配管理员主要负责分配任务
         **/
            string role = Utility.Utility.GetRole();
            string username =Utility.Utility.GetUserName();
            StringBuilder str = new StringBuilder();
            switch (role)
            {
                case "0":
                    {
                        str.Append(GetDownLoadApply(username, role));
                    }
                    break;
                case "1":
                    {
                        str.Append(GetCollectTask(username, role));
                    }
                    break;
                case "2":
                    {
                        str.Append(GetAuditCollectTask(username, role));
                    }
                    break;
                case "3":
                    {
                        str.Append(GetProcessTask(username, role));
                        str.Append(GetSecProcessTask(username, role));
                    }
                    break;
                case "4":
                    {
                        str.Append(GetAuditProcessTask(username, role));
                        str.Append(GeAutidtSecProcessTask(username, role));
                    }
                    break;
                case "5":
                    {
                        //str.Append(GetCollectTask(username, role));
                        //str.Append(GetAuditCollectTask(username, role));
                        //str.Append(GetProcessTask(username, role));
                        //str.Append(GetAuditProcessTask(username, role));
                        //str.Append(GetAssignTask(username, role));
                        str.Append(GetAuidtDownLoadApply(username, role));
                    }
                    break;
                case "7":
                    {
                        str.Append(GetAssignTask(username, role));
                    }
                    break;
            }
            ltlTask.Text = str.ToString();

        }

        /// <summary>
        /// 获取待审核的资源
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetAuidtDownLoadApply(string username, string role)
        {
            //这还可以加一个 选择资源的链接 添加资源
            string FormatStrA = "<li><a href=\"../auditadmin/AuditDownLoadApply.aspx\"><img src=\"../images/TYpic1.png\"/><p>资源下载审批</p><span class=\"TaskNum\">{0}</span> </a></li>";

            string strWhere = " IsDownload=0 and CHECKSTATUS=\"0\"";
            DownLoadApply misstiom = new DownLoadApply();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStrA, "");
            }
            else
            {
                return string.Format(FormatStrA, count);
            }
        }

        /// <summary>
        /// 获取需要加工的任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetSecProcessTask(string username, string role)
        {
            string FormatStr = "<li><a href=\"/auditadmin/ProcessTask.aspx?scf=0&at=e\"><img src=\"../images/TYpic5.png\"/><p>待二次编辑的任务</p><span class=\"TaskNum\">{0}</span></a> </li>";

            string strWhere = "SENDUSER=\"" + username + "\" workstatus=0 and SYS_FLD_CHECK_STATE=\"4\" ";

            if (role == "5")
            {
                //查看所有人的带加工任务
                strWhere = " workstatus=0 and SYS_FLD_CHECK_STATE=\"4\"";
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStr, "");
            }
            else
            {
                return string.Format(FormatStr, count);
            }
        }


        /// <summary>
        /// 获取需要加工的任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GeAutidtSecProcessTask(string username, string role)
        {
            string FormatStr = "<li><a href=\"/auditadmin/ExaminingRes.aspx?resEx=0&at=e\"><img src=\"../images/TYpic2.png\"/><p>编辑待审核的任务</p><span class=\"TaskNum\">{0}</span></a> </li>";

            string strWhere = "SENDUSER=\"" + username + "\" workstatus=0 and SYS_FLD_CHECK_STATE=\"5\" and isbat=1";

            if (role == "5")
            {
                //查看所有人的带加工任务
                strWhere = " workstatus=0 and SYS_FLD_CHECK_STATE=\"5\" and isbat=1";
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStr, "");
            }
            else
            {
                return string.Format(FormatStr, count);
            }
        }

        /// <summary>
        /// 获取需要加工审核的任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetAuditProcessTask(string username, string role)
        {
            string FormatStr = "<li><a href=\"/auditadmin/ExaminingRes.aspx?resEx=0&at=p\"><img src=\"../images/TYpic3.png\"/><p>待审核的资源（加工后）</p><span class=\"TaskNum\">{0}</span></a> </li>";

            string strWhere = "SENDUSER=\"" + username + "\" workstatus=0 and SYS_FLD_CHECK_STATE=\"3\" ";

            if (role == "5")
            {
                //查看所有人的带加工任务
                strWhere = " workstatus=0 and SYS_FLD_CHECK_STATE=\"3\" ";
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStr, "");
            }
            else
            {
                return string.Format(FormatStr, count);
            }
        }

        /// <summary>
        /// 获取需要加工的任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string  GetProcessTask(string username, string role)
        {
            string FormatStr = "<li><a href=\"/auditadmin/ProcessTask.aspx?scf=0&at=p\"><img src=\"../images/TYpic2.png\"/><p>待加工资源</p><span class=\"TaskNum\">{0}</span></a> </li>";

            string strWhere = "SENDUSER=\"" + username + "\" workstatus=0 and SYS_FLD_CHECK_STATE=\"2\" ";

            if (role == "5")
            {
                //查看所有人的带加工任务
                strWhere = " workstatus=0 and SYS_FLD_CHECK_STATE=\"2\" "; 
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStr, "");
            }
            else
            {
                return string.Format(FormatStr, count);
            }
        }


        /// <summary>
        /// 获取收集完成 待审核的任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetAuditCollectTask(string username, string role)
        {
            string FormatStr = "<li><img src=\"../images/TYpic1.png\"/><p>待审核资源</p><span class=\"TaskNum\">{0}</span> </li>";
            string FormatStrA = "<li><a href=\"/auditadmin/ExaminingRes.aspx?resEx=0&at=c\"><img src=\"../images/TYpic1.png\"/><p>待审核资源</p><span class=\"TaskNum\">{0}</span> <a></li>";
            string strWhere = " SENDUSER=\"" + username + "\" and  workstatus=0 and SYS_FLD_CHECK_STATE=\"1\" ";

            if (role == "5")
            {
                strWhere = " workstatus=0  and SYS_FLD_CHECK_STATE=\"1\" ";
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStr, "");
            }
            else
            {
                return string.Format(FormatStrA, count);
            }
        }

        /// <summary>
        /// 获得未完成的收集任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetCollectTask(string username, string role)
        {
            //这还可以加一个 选择资源的链接 添加资源
            string FormatStr = "<li><img src=\"../images/TYpic1.png\"/><p>待收集资源</p><span class=\"TaskNum\">{0}</span> </li>";
            string FormatStrA = "<li><a href=\"../auditadmin/ChooseMyRes.aspx\"><img src=\"../images/TYpic1.png\"/><p>待收集资源</p><span class=\"TaskNum\">{0}</span> </a></li>";
            string FormatAddrec = "<li><a href=\"../auditadmin/ResourceType.aspx\"><img src=\"../images/addrec.png\" /><p>添加资源</p></a></li>";
            string strWhere = " EXECUTEUSER=\"" + username + "\" and workstatus=0 and SYS_FLD_CHECK_STATE=\"0\"";

            if (role == "5")
            {
                strWhere = " workstatus=0 and SYS_FLD_CHECK_STATE=\"0\"";
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return FormatAddrec + string.Format(FormatStr, "");
            }
            else
            {
                return FormatAddrec + string.Format(FormatStrA, count);
            }

        }

        /// <summary>
        /// 获得待分配的任务
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetAssignTask(string username, string role)
        {
            //这还可以加一个 选择资源的链接 添加资源
            string FormatStr = "<li><img src=\"../images/assigntask.png\"/><p>待分配的任务</p><span class=\"TaskNum\">{0}</span> </li>";
            string FormatStrA = "<li><a href=\"../auditadmin/AssignTask.aspx?tmst=ppp\"><img src=\"../images/assigntask.png\"/><p>待分配的任务</p><span class=\"TaskNum\">{0}</span> </a></li>";
            // string FormatAddrec = "<li><a href=\"../auditadmin/ResourceType.aspx\"><img src=\"../images/addrec.png\" /><p>添加资源</p></a></li>";
            string strWhere = "workstatus=0 and FINISHSTATUS=\"0\" and ISSENDED=0 and SYS_FLD_CHECK_STATE>0 ";

            if (role == "7")
            {
                strWhere = " workstatus=0 and FINISHSTATUS=\"0\" and ISSENDED=0 and SYS_FLD_CHECK_STATE>0 ";
            }
            Mission misstiom = new Mission();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStr, "");
            }
            else
            {
                return string.Format(FormatStrA, count);
            }
        }

        /// <summary>
        /// 获得用户下载申请个数
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetDownLoadApply(string username, string role)
        {
            //这还可以加一个 选择资源的链接 添加资源
            string FormatStrA = "<li><a href=\"../view/ApplyDownLoadList.aspx?q=0\"><img src=\"../images/TYpic1.png\"/><p>我申请下载的资源</p><span class=\"TaskNum\">{0}</span> </a></li>";

            string strWhere = " USERNAME=\"" + username + "\" and IsDownload=0 and CHECKSTATUS=\"-1\"";


            DownLoadApply misstiom = new DownLoadApply();
            int count = misstiom.GetCount(strWhere);
            if (count == 0)
            {
                return string.Format(FormatStrA, "");
            }
            else
            {
                return string.Format(FormatStrA, count);
            }

        }
    }
}