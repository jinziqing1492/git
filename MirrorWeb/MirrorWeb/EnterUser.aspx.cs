using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb
{
    public partial class EnterUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //验证是否必要的内容全部都输入完成了

            //获取Excel文件中的数据
            List<ExcelDataInfo> dataList = GetExcelData();
            if (dataList == null || dataList.Count == 0)
            {
                //MessageBox.Show("获取数据失败");
                return;
            }
            //MessageBox.Show("共获取到了" + dataList.Count + "条数据");

            //将Excel中的数据添加到数据库中
            if (dataList != null && dataList.Count > 0)
            {
                foreach (ExcelDataInfo info in dataList)
                {
                    //向数据库中添加用户
                    AddUser(info);
                }
            }

            ////验证是否成功
            //List<ExcelDataInfo> errorList = CheckData(dataList);
            //if (errorList != null && errorList.Count > 0)
            //{
            //    MessageBox.Show("导入用户时出现错误，出错条数：" + errorList.Count);
            //}
            //else
            //{
            //    MessageBox.Show("导入用户成功");
            //}
        }
        /// <summary>
        /// 从Excel中读取数据
        /// </summary>
        private List<ExcelDataInfo> GetExcelData()
        {
            List<ExcelDataInfo> dataList = null;

            string filePath = TextBox1.Text;
           
            StreamReader reader = null;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                reader = new StreamReader(fs, Encoding.Default);
                //Console.WriteLine(reader.ReadToEnd());
                List<string> lines = new List<string>();
                string line = "";
                line = reader.ReadLine();
                while (line != "" && line != null)
                {
                    lines.Add(line);
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
                if (lines != null && lines.Count > 0)
                {
                    dataList = new List<ExcelDataInfo>();
                    foreach (string s in lines)
                    {
                        string user = "";
                        string name = "";
                        string dpt = "";
                        string[] fields = s.Split('|');
                        if (fields != null && fields.Length == 3)
                        {
                            user = fields[0];
                            name = fields[1];
                            dpt = fields[2];
                        }
                        if (!string.IsNullOrEmpty(user))
                        {
                            dataList.Add(new ExcelDataInfo()
                            {
                                Code = user,
                                Name = name,
                                Dpt = dpt
                            });
                        }
                    }
                }
            }
            catch { throw; }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return dataList;
        }

        /// <summary>
        /// 新建用户
        /// <param name="sql">需要执行的sql语句</param>
        /// </summary>
        private void AddUser(ExcelDataInfo info)
        {
            //检查用户名是否重复
            BLL.User bll = new BLL.User();
            UserInfo item = bll.GetUser(info.Code);
            if (item != null)
            {
                return;
            }

            item = new UserInfo();
            item.UserName = info.Code;
            item.Password = "670B14728AD9902AECBA32E22FA4F6BD";
            item.RealName = info.Name;
            item.ADDDATE = DateTime.Now;
            item.Role = "0";
            item.TOKEN = info.Dpt;

            //添加到数据库中
            //string password = "670B14728AD9902AECBA32E22FA4F6BD";
            //sql = string.Format("INSERT INTO {0}(USERNAME,PASSWORD,REALNAME,ADDDATE,ROLE,TOKEN) VALUES('{1}','{2}','{3}','{4}',{5},'{6}')",
            //    Table_Name, info.Code, password, info.Name, DateTime.Now, "0", info.Dpt);
            //TPIHelper.ExecSql(sql);
            bll.AddUser(item);
        }
    }
    public class ExcelDataInfo
    {
        public string Code { get; set; }//员工编号
        public string Dpt { get; set; }//部门名称
        public string Name { get; set; }//姓名
    }
}