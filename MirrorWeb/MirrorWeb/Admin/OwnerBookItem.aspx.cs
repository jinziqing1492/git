using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.Admin
{
    public partial class OwnerBookItem : System.Web.UI.Page
    {
        #region 属性，字段

        /// <summary>
        /// 判断是否为编辑
        /// </summary>
        private bool IsEdit
        {
            get { return !string.IsNullOrEmpty(Request.QueryString["doi"]); }
        }

        /// <summary>
        /// BASEID
        /// </summary>
        private string BaseID
        {
            get { return Request.QueryString["baseid"]; }
        }

        private string Doi
        {
            get { return Request.QueryString["doi"]; }
        }

        /// <summary>
        /// 业务逻辑库对象
        /// </summary>
        JournalYear bll = new JournalYear("owner");
        Journal jBll = new Journal("owner");
        #endregion

        #region 事件

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doi = Request.QueryString["doi"];
                if (IsEdit)
                {
                    BindData(doi);
                }
            }
        }

        /// <summary>
        /// 添加或修改书籍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string name = tbxName.Text;
            //判断是否为空
            if (string.IsNullOrEmpty(name))
            {
                message.Visible = true;
                message.Content = "必填项不能为空。";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }

            JournalYearInfo info = bll.GetItem(Doi);
            if (!IsEdit)
            {
                //判断BaseID是否重复
                if (info != null)
                {
                    message.Visible = true;
                    message.Content = "书籍名称已存在，请更换后重试。";
                    message.MessageType = AdminUserControl.NotificationType.Error;
                    return;
                }
                else
                {
                    info = new JournalYearInfo();
                    info.BASEID = BaseID;
                    info.SYS_FLD_DOI = Guid.NewGuid().ToString();
                }
            }
            if (info == null)
            {
                message.Visible = true;
                message.Content = "未获取到资源 操作失败。";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            info.ISSUE = name;
            info.YEAR = DateTime.Now.Year;
            info.Sys_fld_Adddate = DateTime.Now;
            info.SYS_FLD_CHECK_STATE = -1;
            info.CNAME = "";
            int recordCount=0;
            IList<JournalInfo> jList = jBll.GetList("baseid='" + BaseID + "'", 1, 1, out recordCount, false);
            if (jList != null && jList.Count > 0)
            {
                info.CNAME = jList[0].CNAME;
            }
            if (file_cover.HasFile)
            {
                //上传封面
                string errorMessage = "";
                bool result = UploadCover(info, ref errorMessage);
                if (!result)
                {
                    message.Visible = true;
                    message.Content = errorMessage;
                    message.MessageType = AdminUserControl.NotificationType.Error;
                    return;
                }
            }
            if (file_pdf.HasFile)
            {
                //上传pdf
                string errorMessage = "";
                bool result = UploadFile(info, ref errorMessage);
                if (!result)
                {
                    message.Visible = true;
                    message.Content = errorMessage;
                    message.MessageType = AdminUserControl.NotificationType.Error;
                    return;
                }
            }

            //若为编辑则使用Update方法 创建则使用Add方法
            bool isSuccess = IsEdit ? bll.Update(info) : bll.Add(info);
            if (!isSuccess)
            {
                message.Visible = true;
                message.Content = "操作失败。";
                message.MessageType = AdminUserControl.NotificationType.Error;
            }
            else
            {
                //跳转到列表页
                Response.Redirect("OwnerBookList.aspx?baseid=" + BaseID);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定数据 修改时才会起作用
        /// </summary>
        private void BindData(string doi)
        {
            JournalYearInfo info = bll.GetItem(doi);
            if (info != null)
            {
                tbxName.Text = info.ISSUE;
                if (!string.IsNullOrEmpty(info.SYS_FLD_COVERPATH) && !string.IsNullOrEmpty(info.SYS_FLD_VIRTUALPATHTAG))
                {
                    lt_cover.Text = "<img src='../view/ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + info.SYS_FLD_COVERPATH + "' />";
                }
                if (!string.IsNullOrEmpty(info.SYS_FLD_FILEPATH) && !string.IsNullOrEmpty(info.SYS_FLD_VIRTUALPATHTAG))
                {
                    string fileName = Config.GetVirtalPath("1");
                    string pdfPath = "../" + fileName + "/" + info.SYS_FLD_FILEPATH;
                    lt_pdf.Text = "<a href='" + pdfPath + "' target='_blank'>查看文件</a>";
                }
            }
            else //未获取到数据
            {
                message.Visible = true;
                message.Content = "未找到数据，请稍后重试。";
                message.MessageType = AdminUserControl.NotificationType.Error;
            }
        }

        /// <summary>
        /// 上传封面
        /// </summary>
        private bool UploadCover(JournalYearInfo info,ref string message)
        {
            //判断是否符合要求
            string ext = Path.GetExtension(file_cover.FileName);
            if (ext != ".png" && ext != ".jpg" && ext != ".gif")
            {
                message = "图片格式只能为 .png .jpg .gif";
                return false;
            }
            int length = file_cover.PostedFile.ContentLength;
            if (length > 1 * 1024 * 1024)
            {
                message = "图片大小必须小于1M";
                return false;
            }
            //上传图片
            string virpath = Server.MapPath("~/" + Config.GetVirtalPath("1"));
            string path = "\\journal\\" + info.SYS_FLD_DOI + "\\" + info.SYS_FLD_DOI + ext;
            string dirPath = Path.GetDirectoryName(virpath + path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            file_cover.PostedFile.SaveAs(virpath + path);
            if (File.Exists(virpath + path))
            {
                info.SYS_FLD_VIRTUALPATHTAG = "1";
                info.SYS_FLD_COVERPATH = path;
                return true;
            }
            else
            {
                message = "上传图片失败";
                return false;
            }
        }

        /// <summary>
        /// 上传封面
        /// </summary>
        private bool UploadFile(JournalYearInfo info,ref string message)
        {
            //判断是否符合要求
            string ext = Path.GetExtension(file_pdf.FileName);
            if (ext != ".pdf")
            {
                message = "文件格式只能为 .pdf";
                return false;
            }
            int length = file_pdf.PostedFile.ContentLength;
            if (length > 200 * 1024 * 1024)
            {
                message = "文件大小必须小于200M";
                return false;
            }
            //上传文件
            string virpath = Server.MapPath("~/" + Config.GetVirtalPath("1"));
            string path = "\\journal\\" + info.SYS_FLD_DOI + "\\" + info.SYS_FLD_DOI + ext;
            string dirPath = Path.GetDirectoryName(virpath + path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            file_pdf.PostedFile.SaveAs(virpath + path);
            if (File.Exists(virpath + path))
            {
                info.SYS_FLD_VIRTUALPATHTAG = "1";
                info.SYS_FLD_FILEPATH = path;
                return true;
            }
            else
            {
                message = "上传文件失败";
                return false;
            }
        }

        #endregion
    }
}