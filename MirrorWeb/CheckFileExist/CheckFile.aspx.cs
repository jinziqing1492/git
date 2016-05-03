using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

using DRMS.BLL;
using DRMS.Model;

namespace CheckFileExist
{
    public partial class CheckFile : System.Web.UI.Page
    {
        private IList<ErrorMsg> ErrorList { get; set; }//错误记录

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindData();
            }
        }
        private void BindData()
        {
            Book bll = new Book();
            int recordCount=0;
            IList<BookInfo> list = bll.GetList("", 1, 1, out recordCount, true);
            if (recordCount > 1)
            {
                list = bll.GetList("", 1, recordCount, out recordCount, true);
            }
            if (list != null && list.Count > 0)
            {
                ErrorList = new List<ErrorMsg>();
                foreach (BookInfo info in list)
                {
                    string filePath = Server.MapPath("~/doc") + info.SYS_FLD_FILEPATH;
                    string coverPath = Server.MapPath("~/doc") + info.SYS_FLD_COVERPATH;
                    //判断文件和封面是否存在
                    bool isFile = File.Exists(filePath);
                    bool isCover = File.Exists(coverPath);
                    if (!isFile || !isCover)
                    {
                        ErrorMsg msg = new ErrorMsg();
                        msg.Sys_Fld_Doi = info.SYS_FLD_DOI;
                        msg.Title = info.Name;
                        msg.IsCover = isCover;
                        msg.IsFile = isFile;
                        ErrorList.Add(msg);
                    }
                }
            }
            if (ErrorList != null && ErrorList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                int order = 1;
                foreach (ErrorMsg info in ErrorList)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + order + "</td>");
                    sb.Append("<td>" + info.Sys_Fld_Doi + "</td>");
                    sb.Append("<td>" + info.Title + "</td>");
                    sb.Append("<td>" + (info.IsFile ? "是" : "否") + "</td>");
                    sb.Append("<td>" + (info.IsCover ? "是" : "否") + "</td>");
                    sb.Append("</tr>");

                    order++;
                }
                lt_list.Text = sb.ToString();
            }
        }

        protected void btn_Check_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
    public class ErrorMsg
    {
        public string Sys_Fld_Doi { get; set; }
        public string Title { get; set; }
        public bool IsFile { get; set; }
        public bool IsCover { get; set; }
    }
}