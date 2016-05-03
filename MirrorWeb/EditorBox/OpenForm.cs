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
    public partial class OpenForm : Form
    {
        public OpenForm()
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
        private void OpenForm_Load(object sender, EventArgs e)
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
            if (this.checkBox_delxml.Checked)
            {
                DialogResult dr =
                    MessageBox.Show("确定删除本机上此图书当前xml文件，下载新的xml文件？", "", MessageBoxButtons.OKCancel);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    string clientFolder = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\" + Workor.BookID;
                    string xmlClientPath = clientFolder + "\\" + Workor.BookID + ".xml";
                    if (File.Exists(xmlClientPath))
                        File.Delete(xmlClientPath);
                }
            }
            this.buttonOpen.Enabled = false;
            //打开操作
            Thread thDownload = new Thread(new ThreadStart(this.Download));
            thDownload.IsBackground = false;
            thDownload.Start();
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
        public void Download()
        {
            //while (System.IO.File.Exists(@"d:\debug.sleep"))
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
            try
            {
                this.Invoke(this.delShowMsg, "准备下载文件。。。");
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { 0 });

                #region 统计文件个数，及获取Images文件列表
                //获取文件列表
                Stream stream = _wc.OpenRead("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                    + "/Ajax/EditServer.ashx?type=1&mid=" + Workor.MissionID);
                StreamReader reader = new StreamReader(stream);
                //获取文件列表文本
                string text = reader.ReadToEnd();
                stream.Close();

                int index = 0;
                int total = 1;
                string[] filelist = text.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (filelist != null && filelist.Length > 0)
                {
                    total += filelist.Length;
                }
                this.Invoke(this.delUpdateCount, index, total);
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Maximum = value; }), new object[] { total });
                #endregion

                //在客户端准备存放目录
                string clientFolder = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\" + Workor.BookID;
                string xmlClientPath = clientFolder + "\\" + Workor.BookID + ".xml";
                string clientImagesFolder = clientFolder + "\\Images";
                if (!Directory.Exists(clientFolder) || !Directory.Exists(clientImagesFolder))
                {
                    Directory.CreateDirectory(clientImagesFolder);
                }

                #region 下载xml主文件
                this.Invoke(this.delShowMsg, "正在下载XML主文件。。。");
                if (!File.Exists(xmlClientPath))
                {
                    //判断文件存在与否
                    stream = _wc.OpenRead("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                        + "/Ajax/EditServer.ashx?type=7&mid=" + Workor.MissionID);
                    reader = new StreamReader(stream);
                    //获取文件列表文本
                    text = reader.ReadToEnd();
                    stream.Close();
                    if (text != "1")
                    {
                        MessageBox.Show("XML主文件不存在，程序退出！");
                        Invoke(new CloseFormHandler(() => this.Close()));
                        return;
                    }

                    _wc.DownloadFile("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                        + "/Ajax/EditServer.ashx?type=2&mid=" + Workor.MissionID, xmlClientPath);
                }
                this.Invoke(this.delUpdateCount, ++index, total);
                this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { index });
                #endregion

                #region 处理xml文件头部
                //文件下载完成后，替换xml文件头
                if (File.Exists(xmlClientPath))
                {
                    string xmlfile = File.ReadAllText(xmlClientPath, Encoding.UTF8);
                    xmlfile = xmlfile.Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\" \"book.dtd\"><book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\"\r\n \"book.dtd\">\r\n<book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<!DOCTYPE book PUBLIC \"-//CTDW//BOOK DTD 1.0//EN\"\r\n \"book.dtd\">\r\n<?Pub Inc?>\r\n<book>",
                        "<book xmlns=\"http://docbook.org/ns/docbook\">").Replace(
                        "<info xmlns=\"http://docbook.org/ns/docbook\">", "<info>").Replace(
                        "<chapterinfo xmlns=\"http://docbook.org/ns/docbook\">", "<chapterinfo>");
                    Regex reg = new Regex("\r\n>");
                    xmlfile = reg.Replace(xmlfile, ">");
                    reg = new Regex("\n>");
                    xmlfile = reg.Replace(xmlfile, ">");
                    File.WriteAllText(xmlClientPath, xmlfile, Encoding.UTF8);
                }
                #endregion

                #region 下载Images文件
                bool bExistsFail = false;
                if (filelist != null && filelist.Length > 0)
                {
                    foreach (string fileName in filelist)
                    {
                        if (string.IsNullOrEmpty(fileName))
                        {
                            this.Invoke(this.delUpdateCount, ++index, total);
                            continue;
                        }
                        string fileClientPath = clientImagesFolder + "\\" + DesSecurity.DesDecrypt(fileName);
                        this.Invoke(this.delShowMsg, "正在下载Images文件。。。");// + DesSecurity.DesDecrypt(fileName).Substring(0, 20) + "。。。");
                        if (!File.Exists(fileClientPath))
                        {
                            _wc.DownloadFile("http://" + Workor.ServerIP + ":" + Workor.ServerPort
                                + "/Ajax/EditServer.ashx?type=3&mid=" + Workor.MissionID + "&filename=" + fileName, fileClientPath);
                            if (!File.Exists(fileClientPath))
                            {
                                bExistsFail = true;
                            }
                        }
                        this.Invoke(this.delUpdateCount, ++index, total);
                        this.Invoke(new SetPgbValue(value => { this.progressBar1.Value = value; }), new object[] { index });
                    }
                }
                if (bExistsFail)
                {
                    MessageBox.Show("部分图片文件下载失败！");
                    return;
                }
                #endregion

                #region 启动编辑器
                try
                {
                    this.Invoke(this.delShowMsg, "下载完毕，打开编辑器");
                    string editorPath = Workor.XEditorPath;
                    if (string.IsNullOrEmpty(editorPath))
                    {
                        MessageBox.Show("编辑器启动失败，请确认本机已安装此编辑器！");
                        Invoke(new CloseFormHandler(() => this.Close()));
                        return;
                    }
                    Process.Start(editorPath, "\"" + xmlClientPath + "\"");
                    Thread.Sleep(1000);
                    Invoke(new CloseFormHandler(() => this.Close()));
                }
                catch
                {
                    MessageBox.Show("编辑器启动过程发生意外，程序退出！");
                    Invoke(new CloseFormHandler(() => this.Close()));
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                string errorpath = Application.StartupPath.TrimEnd('\\') + "\\ErrorLog.txt";
                File.AppendAllText(errorpath, ex.ToString() + Environment.NewLine);
                MessageBox.Show("文件下载过程发生意外，程序退出！");
                Invoke(new CloseFormHandler(() => this.Close()));
                return;
            }
        }

        private void label_Close_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
