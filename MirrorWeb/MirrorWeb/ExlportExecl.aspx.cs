using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRMS.BLL;
using DRMS.Model;
using System.Data;

namespace DRMS.MirrorWeb
{
    public partial class ExlportExecl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Book bll = new Book();
            int allCount = 0;
            IList<BookInfo> list = bll.GetList("", 1, 2000, out allCount, true);

            //生成datatable
            DataTable dt = new DataTable("南网execl");
            dt.Columns.Add("图书ID");
            dt.Columns.Add("书名");
            dt.Columns.Add("作者");
            dt.Columns.Add("ISBN");
            dt.Columns.Add("出版单位");
            dt.Columns.Add("关键字");
            dt.Columns.Add("纸书定价");
            dt.Columns.Add("封面虚拟路径");
            dt.Columns.Add("出版时间");
            dt.Columns.Add("图书分类");
            //dt.Columns.Add("图书简介");

            foreach (BookInfo info in list)
            {
                string s_issueDate = "";
                try
                {
                    s_issueDate = info.IssueDate.ToString("yyyy年MM月");
                }
                catch 
                {
                    s_issueDate = "";
                }
                DataRow drNew = dt.NewRow();
                drNew[0] = info.SYS_FLD_DOI;
                drNew[1] = info.Name;
                drNew[2] = info.Author;
                drNew[3] = info.ISBN;
                drNew[4] = info.IssueDep;
                drNew[5] = info.Keywords;
                drNew[6] = info.Price;
                drNew[7] = info.SYS_FLD_COVERPATH;
                drNew[8] = s_issueDate;
                drNew[9] = info.SYS_FLD_CLASSFICATION;
                //drNew[7] = info.Digest;
                dt.Rows.Add(drNew);
            }

            string path = "C:\\ex\\图书字段.xls";
            DRMS.MirrorWeb.Utility.MyExcelUtls.DataTable2Sheet(path,dt);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Theme bll = new Theme();
            int allCount = 0;
            IList<ThemeInfo> list = bll.GetList("", 1, 300, out allCount, true);

            //生成datatable
            DataTable dt = new DataTable("电力分类");
            dt.Columns.Add("分类ID");
            dt.Columns.Add("分类名称");
            dt.Columns.Add("所属的父分类的ID号");

            foreach (ThemeInfo info in list)
            {                
                DataRow drNew = dt.NewRow();
                drNew[0] = info.ID;
                drNew[1] = info.ThemeName;
                drNew[2] = info.ParentID;
                dt.Rows.Add(drNew);
            }

            string path = "C:\\ex\\分类.xls";
            DRMS.MirrorWeb.Utility.MyExcelUtls.DataTable2Sheet(path, dt);
        }
    }
}