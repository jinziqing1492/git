using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace DRMS.EditorBox
{
    public partial class SaveForm : Form
    {
        public SaveForm()
        {
            InitializeComponent();
        }

        #region 窗体移动
        //定义一个布尔变量，作为窗体拖动事件的开关。
        bool mIsStartDrag = false;
        //定义一个‘点’的变量，接收鼠标的点位置。
        Point mMousePonit;
        /// <summary>
        /// 窗体移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            //考虑是否鼠标左键按下，如果按下则开始做以下的事情。
            if (e.Button == MouseButtons.Left)
            {
                //给mousePonit定义为当前的鼠标位置坐标。
                mMousePonit = new Point(-e.X, -e.Y);
                //设置变量b为布尔真值。
                mIsStartDrag = true;
            }
        }
        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            //如果获取b为真的时候，开始执行下面的语句。
            if (mIsStartDrag)
            {
                //定义一个‘点’变量，为组件的鼠标光标位置
                Point p = Control.MousePosition;
                //平移mousePonit为p变量。
                p.Offset(mMousePonit);
                //控件的位置，为p位置。
                this.Location = p;
            }
        }
        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            mIsStartDrag = false;
        }
        #endregion

        WebClient _wc = new WebClient();
        private void SaveForm_Load(object sender, EventArgs e)
        {
            //webClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usernameUpload"].ToString(),
            //    ConfigurationManager.AppSettings["passwordUpload"].ToString()
            //    );
            _wc.Credentials = CredentialCache.DefaultCredentials;

            delShowMsg = new DelShowMessage(ShowMessage);
            delUpdateCount = new DelUpdateCount(UpdateCount);

        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            this.buttonSubmit.Enabled = false;
            this.textBox1.Enabled = false;
            switch (Workor.WorkType)
            {
                case "1":
                    //覆盖操作
                    Thread thCover_Old = new Thread(new ThreadStart(this.Cover_Old));
                    thCover_Old.IsBackground = false;
                    thCover_Old.Start();
                    break;
                case "2":
                    //再版操作
                    Thread thUpload_New = new Thread(new ThreadStart(this.Upload_New));
                    thUpload_New.IsBackground = false;
                    thUpload_New.Start();
                    break;
                default:
                    Invoke(new CloseFormHandler(() => this.Close()));
                    break;
            }
        }

        public delegate void DelShowMessage(string message);
        public DelShowMessage delShowMsg = null;
        public void ShowMessage(string message)
        {
            this.lblMsg.Text = message;
        }

        public delegate void SetPgbValue(int value);
        public SetPgbValue setPgbValue = null;

        public delegate void DelUpdateCount(int done, int total);
        public DelUpdateCount delUpdateCount = null;
        public void UpdateCount(int done, int total)
        {
            this.lblCount.Text = "[ " + done + "/" + total + " ]";
        }

        public delegate void CloseFormHandler();

        //string _strCmd = "C:\\Users\\Administrator\\Desktop\\社科测试\\6387ba1f-1d5c-4fd6-b7cb-dac8e046f79b|C:\\Users\\Administrator\\Desktop\\社科服务测试\\6387ba1f-1d5c-4fd7dd-b7cb-dac8e046f79b|1|192.168.100.222|4567|DBOWN||admin";//m_lpCmdLine;
        public void Cover_Old()
        {
            //while (System.IO.File.Exists(@"d:\debug.sleep"))
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
            try
            {
                this.Invoke(this.delShowMsg, "正在保存编辑数据。。。");
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { 0 });

                string clientFolder = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\" + Workor.BookID;
                string xmlClientPath = clientFolder + "\\" + Workor.BookID + ".xml";
                string xmlClientCopy = clientFolder + "\\" + Workor.BookID + "_copy.xml";
                string clientImagesPath = clientFolder + "\\Images";
                if (!File.Exists(xmlClientPath))
                {
                    MessageBox.Show("程序未找到XML文件！");
                    Invoke(new CloseFormHandler(() => this.Close()));
                    return;
                }
                File.Copy(xmlClientPath, xmlClientCopy, true);

                //文件上传前，替换xml文件头
                if (File.Exists(xmlClientCopy))
                {
                    string xmlfile = File.ReadAllText(xmlClientCopy, Encoding.UTF8);
                    xmlfile = xmlfile.Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\"\r\n \"book.dtd\">\r\n<book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\"\r\n \"book.dtd\">\r\n<?Pub Inc?>\r\n<book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\" \"book.dtd\"><book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<info xmlns=\"http://docbook.org/ns/docbook\">", "<info>").Replace(
                        "<chapterinfo xmlns=\"http://docbook.org/ns/docbook\">", "<chapterinfo>");
                    Regex reg = new Regex("\r\n>");
                    xmlfile = reg.Replace(xmlfile, ">");
                    reg = new Regex("\n>");
                    xmlfile = reg.Replace(xmlfile, ">");
                    File.WriteAllText(xmlClientCopy, xmlfile, Encoding.UTF8);
                }

                //删除Images图片文件
                _wc.OpenRead("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                    + "/Ajax/EditServer.ashx?type=6&mid=" + Workor.MissionID);
                int index = 0;
                int total = 1;
                string[] filelist = Directory.GetFiles(clientImagesPath);
                if (filelist != null && filelist.Length > 0)
                {
                    total += filelist.Length;
                }
                this.Invoke(this.delUpdateCount, index, total);
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Maximum = value; }), new object[] { total });

                this.Invoke(this.delShowMsg, "正在上传XML文件。。。");
                _wc.UploadFile("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                    + "/Ajax/EditServer.ashx?type=4&mid=" + Workor.MissionID, WebRequestMethods.Http.Post, xmlClientCopy);
                this.Invoke(this.delUpdateCount, ++index, total);
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { index });
                if (filelist != null && filelist.Length > 0)
                {
                    foreach (string filePath in filelist)
                    {
                        string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        this.Invoke(this.delShowMsg, "正在上传Images文件。。。");// + fileName);
                        _wc.UploadFile("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                            + "/Ajax/EditServer.ashx?type=4&mid=" + Workor.MissionID + "&filename=" + DesSecurity.DesEncrypt(fileName), WebRequestMethods.Http.Post, filePath);
                        this.Invoke(this.delUpdateCount, ++index, total);
                        this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { index });
                    }
                }

                //文件上传完成后，调起入库工具
                this.Invoke(this.delShowMsg, "正在进行数据入库。。。");
                _wc.OpenRead("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                    + "/Ajax/EditServer.ashx?type=5&uploadtype=0&olddoi=" + Workor.BookID + "&user=" + Workor.WorkUser + "&note=" + DesSecurity.DesEncrypt(Workor.Note) + "&mid=" + Workor.MissionID);
                //sc.StartServerProcess(serverParent, "1", Common.WorkUser, Common.Note, Common.MissionID);
                File.Delete(xmlClientCopy);
                CloseEditor(clientFolder);
            }
            catch
            {
                MessageBox.Show("文件上传过程发生意外，程序退出！");
                Invoke(new CloseFormHandler(() => this.Close()));
            }
        }

        public void Upload_New()
        {
            //while (File.Exists(@"E:\debug.sleep"))
            //{
            //    Thread.Sleep(1000);
            //}
            try
            {
                this.Invoke(this.delShowMsg, "正在保存编辑数据。。。");
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { 0 });

                string clientFolder = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\" + Workor.BookID;
                string xmlClientPath = clientFolder + "\\" + Workor.BookID + ".xml";
                string xmlClientCopy = clientFolder + "\\" + Workor.BookID + "_copy.xml";
                string clientImagesPath = clientFolder + "\\images";
                if (!File.Exists(xmlClientPath))
                {
                    MessageBox.Show("程序未找到XML文件！");
                    Invoke(new CloseFormHandler(() => this.Close()));
                    return;
                }
                File.Copy(xmlClientPath, xmlClientCopy, true);

                #region 处理xml文件头部
                //文件上传前，替换xml文件头
                if (File.Exists(xmlClientCopy))
                {
                    //<!--Arbortext, Inc., 1988-2012, v.4002-->       <?Pub Caret 7?>
                    string xmlfile = File.ReadAllText(xmlClientCopy, Encoding.UTF8);
                    xmlfile = xmlfile.Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\"\r\n \"book.dtd\">\r\n<book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\"\r\n \"book.dtd\">\r\n<?Pub Inc?>\r\n<book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\" \"book.dtd\"><book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<info xmlns=\"http://docbook.org/ns/docbook\">", "<info>").Replace(
                        "<chapterinfo xmlns=\"http://docbook.org/ns/docbook\">", "<chapterinfo>");
                    Regex reg = new Regex("\r\n>");
                    xmlfile = reg.Replace(xmlfile, ">");
                    reg = new Regex("\n>");
                    xmlfile = reg.Replace(xmlfile, ">");
                    File.WriteAllText(xmlClientCopy, xmlfile, Encoding.UTF8);
                }
                #endregion

                //生成新版本的doi
                string doi_new = Guid.NewGuid().ToString();
                int index = 0;
                int total = 1;
                string[] filelist = Directory.GetFiles(clientImagesPath);
                if (filelist != null && filelist.Length > 0)
                {
                    total += filelist.Length;
                }
                this.Invoke(this.delUpdateCount, index, total);
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Maximum = value; }), new object[] { total });
                this.Invoke(this.delShowMsg, "正在上传XML文件。。。");
                _wc.UploadFile("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                    + "/Ajax/EditServer.ashx?type=4&mid=" + Workor.MissionID, WebRequestMethods.Http.Post, xmlClientCopy);
                this.Invoke(this.delUpdateCount, ++index, total);
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { index });
                if (filelist != null && filelist.Length > 0)
                {
                    foreach (string filePath in filelist)
                    {
                        string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        this.Invoke(this.delShowMsg, "正在上传Images文件。。。");// + fileName);
                        _wc.UploadFile("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                            + "/Ajax/EditServer.ashx?type=4&mid=" + Workor.MissionID + "&filename=" + DesSecurity.DesEncrypt(fileName), WebRequestMethods.Http.Post, filePath);
                        this.Invoke(this.delUpdateCount, ++index, total);
                        this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { index });
                    }
                }
                //文件上传完成后，调起入库工具
                this.Invoke(this.delShowMsg, "正在进行数据入库。。。");
                _wc.OpenRead("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                    + "/Ajax/EditServer.ashx?type=5&uploadtype=1&olddoi=" + Workor.BookID + "&newdoi=" + doi_new + "&user=" + Workor.WorkUser
                    + "&note=" + DesSecurity.DesEncrypt(Workor.Note) + "&mid=" + Workor.MissionID);
                //sc.StartServerProcess(serverParent + "|" + Common.BookPath, "2", Common.WorkUser, Common.Note, Common.MissionID);
                File.Delete(xmlClientCopy);
                CloseEditor(clientFolder);
            }
            catch
            {
                MessageBox.Show("文件上传过程发生意外，程序退出！");
                Invoke(new CloseFormHandler(() => this.Close()));
            }
        }

        public void CloseEditor(string clientFolder)
        {
            try
            {
                this.Invoke(this.delShowMsg, "保存完毕，退出编辑器");
                Process[] editors = Process.GetProcessesByName("xmleditor");
                if (editors != null)
                {
                    foreach (Process p in editors)
                    {
                        p.Kill();
                        //Thread.Sleep(1000);
                    }
                }
                //最后清除掉客户端的临时数据
                //if (Directory.Exists(clientFolder)) 
                //{
                //    Directory.Delete(clientFolder, true);
                //}
                //清除配置信息
                string configPath = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\book.xml";
                if (File.Exists(configPath))
                    File.Delete(configPath);
                Thread.Sleep(1000);
                Invoke(new CloseFormHandler(() => this.Close()));
            }
            catch
            {
                MessageBox.Show("编辑器退出过程发生意外，程序退出！");
                Invoke(new CloseFormHandler(() => this.Close()));
            }
        }

        private void label_Close_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
