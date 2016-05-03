using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.BLL;
using DRMS.Model;
using System.IO;

namespace DRMS.MirrorWeb.Admin
{
    public partial class OwnerTypeItem : System.Web.UI.Page
    {
        /// <summary>
        /// 判断是否为编辑
        /// </summary>
        private bool IsEdit
        {
            get { return !string.IsNullOrEmpty(Doi); }
        }
        private string Doi
        {
            get { return Request.QueryString["doi"]; }
        }

        OwnerResourceType bll = new OwnerResourceType();

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
        /// 绑定数据 修改时才会起作用
        /// </summary>
        private void BindData(string doi)
        {
            OwnerResourceTypeInfo info = bll.GetItem(doi);
            if (info != null)
            {
                tbxName.Text = info.NAME;
                tbxRemark.Text = info.DESCRIPT;
                if (!string.IsNullOrEmpty(info.SYS_FLD_COVERPATH) && !string.IsNullOrEmpty(info.SYS_FLD_VIRTUALPATHTAG))
                {
                    lt_cover.Text = "<img src='../view/ShowPic.aspx?vpath=" + info.SYS_FLD_VIRTUALPATHTAG + "&path=" + info.SYS_FLD_COVERPATH + "' />";
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
        /// 添加或修改自有资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string name = tbxName.Text;
            string remark = tbxRemark.Text;
            //判断是否为空
            if (string.IsNullOrEmpty(name))
            {
                message.Visible = true;
                message.Content = "必填项不能为空。";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }

            OwnerResourceTypeInfo info = null;
            if (!IsEdit)
            {
                //判断BaseID是否重复
                int recordCount = 0;
                bll.GetList("NAME='" + name + "'", 1, 1, out recordCount);
                if (recordCount > 0)
                {
                    message.Visible = true;
                    message.Content = "该名称已存在。";
                    message.MessageType = AdminUserControl.NotificationType.Error;
                    return;
                }
                else
                {
                    info = new OwnerResourceTypeInfo();
                    info.SYS_FLD_DOI = Guid.NewGuid().ToString();
                    info.BASEID = Utility.Chinese2Spell.ConvertToAllSpell(name).ToUpper();
                    info.DATATYPE = 1;
                }
            }
            else
            {
                info = bll.GetItem(Doi);
            }
            if (info == null)
            {
                message.Visible = true;
                message.Content = "未获取到资源 操作失败。";
                message.MessageType = AdminUserControl.NotificationType.Error;
                return;
            }
            info.NAME = name;
            info.DESCRIPT = remark;
            info.SYS_FLD_ADDDATE = DateTime.Now;
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
                Response.Redirect("OwnerTypeList.aspx");
            }
        }

        /// <summary>
        /// 上传封面
        /// </summary>
        private bool UploadCover(OwnerResourceTypeInfo info, ref string message)
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
    }
}