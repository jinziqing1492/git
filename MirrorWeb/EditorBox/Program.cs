using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace DRMS.EditorBox
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //while (File.Exists(@"D:\debug.sleep"))
            //{
            //    Thread.Sleep(1000);
            //}

            if (args == null || args.Length != 1)
            {
                MessageBox.Show("传入参数有误（参数长度）！");
                return;
            }

            try
            {
                //编辑后，编辑器提交数据
                if (args[0].Length != 1)
                {
                    string argsStr = args[0].Substring(args[0].Trim('/').LastIndexOf('/') + 1);
                    string[] paramArr = DesSecurity.DesDecrypt(argsStr).Split('|');
                    if (paramArr == null || paramArr.Length < 7 || paramArr.Length > 9)
                    {
                        MessageBox.Show("传入参数（长度）有误！");
                        return;
                    }
                    //获取启动的工具类型，1加工 2编辑
                    string toolType = paramArr[0];
                    if (toolType == "1")
                    {
                        StartTMCC(paramArr);
                        return;
                    }
                    //else if (toolType == "2")
                    //{
                    //    Application.EnableVisualStyles();
                    //    Application.SetCompatibleTextRenderingDefault(false);

                    //    StartEDITOpen(paramArr);
                    //}
                    else
                    {
                        MessageBox.Show("传入参数（工具类型参数）有误！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("传入参数有误！");
                    return;
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);

                    //StartEDITSave(args);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new OpenForm());
        }

        static void StartTMCC(string[] paramArr)
        {
            //args = "1|" + svrIp + "|" + svrPort + "|" + svrUser + "|" + svrPwd + "|" + Utility.Utility.GetUserName() + "|" + passwordSys + "|" + mi.ID;
            if (string.IsNullOrEmpty(paramArr[0]) || string.IsNullOrEmpty(paramArr[1]) || string.IsNullOrEmpty(paramArr[2]) || string.IsNullOrEmpty(paramArr[3]) ||
                string.IsNullOrEmpty(paramArr[5]) || string.IsNullOrEmpty(paramArr[6]) || string.IsNullOrEmpty(paramArr[7]) || string.IsNullOrEmpty(paramArr[8]))
            {
                throw new Exception("传入参数有误（有必须值为空）！");
            }

            string paramTcmm = paramArr[1] + "|" + paramArr[2] + "|" + paramArr[3] + "|" + paramArr[4] + "|" + paramArr[5] + "|" + paramArr[6] + "|" + paramArr[7];
            //读取加工工具在注册表中的路径 HKEY_CURRENT_USER\\Software\\TTOD\\数字出版资源管理平台-资源碎片化系统\\CMD\\EXEPATH
            //string tmccPath = RegeditEditor.GetRegData(RegKeyType.HKEY_CURRENT_USER, "Software/TTOD/数字出版资源管理平台-资源碎片化系统/CMD", "EXEPATH");
            string tmccPath = AppDomain.CurrentDomain.BaseDirectory.Trim('\\');
            switch (paramArr[8])
            {
                case "8":
                    //读取调用的工具，默认调用报纸工具
                    string toolflag = "0";
                    try
                    {
                        toolflag = ConfigurationManager.AppSettings["toolflag"];
                    }
                    catch
                    {
                        toolflag = "0";
                    }
                    if (toolflag == "0")
                    {
                        try
                        {
                            string exepath = ConfigurationManager.AppSettings["paper"];
                            if (File.Exists(exepath))
                            {
                                tmccPath = exepath;
                            }
                            else
                            {
                                tmccPath += "\\paper\\MagazineProcess.exe";
                            }
                        }
                        catch
                        {
                            tmccPath += "\\paper\\MagazineProcess.exe";
                        }
                    }
                    else
                    {
                        try
                        {
                            string exepath = ConfigurationManager.AppSettings["cbxmarker"];
                            if (File.Exists(exepath))
                            {
                                tmccPath = exepath;
                            }
                            else
                            {
                                tmccPath += "\\cbxmarker\\CBXMarkerU.exe";
                            }
                        }
                        catch
                        {
                            tmccPath += "\\cbxmarker\\CBXMarkerU.exe";
                        }
                        paramTcmm = "\"" + paramTcmm + "\"";
                    }
                    break;
                default:
                    try
                    {
                        string exepath = ConfigurationManager.AppSettings["cbxmarker"];
                        if (File.Exists(exepath))
                        {
                            tmccPath = exepath;
                        }
                        else
                        {
                            tmccPath += "\\cbxmarker\\CBXMarkerU.exe";
                        }
                    }
                    catch
                    {
                        tmccPath += "\\cbxmarker\\CBXMarkerU.exe";
                    }
                    paramTcmm = "\"" + paramTcmm + "\"";
                    //paramTcmm = Encoding.GetEncoding("gb2312").GetString(Encoding.UTF8.GetBytes(paramTcmm));
                    break;
            }
            if (File.Exists(tmccPath))
            {
                //Process.Start(tmccPath, "\"" + paramTcmm + "\"");
                Process.Start(tmccPath, paramTcmm);
            }
            else
            {
                MessageBox.Show("本机尚未安装可用程序！");
            }
            Thread.Sleep(1000);
        }

        static void StartEDITOpen(string[] paramArr)
        {
            //args[0]为操作类型 0为打开 1为保存
            //args = "2|0|" + serverip + "|" + serverport + "|" + resdoi + "|" + user + "|" + mid;
            if (string.IsNullOrEmpty(paramArr[0]) || string.IsNullOrEmpty(paramArr[1]) || string.IsNullOrEmpty(paramArr[2]) || string.IsNullOrEmpty(paramArr[3]) ||
                string.IsNullOrEmpty(paramArr[4]) || string.IsNullOrEmpty(paramArr[5]) || string.IsNullOrEmpty(paramArr[6]))
            {
                throw new Exception("传入参数有误（有必须值为空）！");
            }

            string configPath = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\book.xml";
            Workor.WorkType = paramArr[1];
            if (Workor.WorkType.Equals("0"))
            {
                if (File.Exists(configPath))
                    File.Delete(configPath);
                //打开操作，记录传入的参数
                Workor.ServerIP = DesSecurity.DesDecrypt(paramArr[2]);
                Workor.ServerPort = DesSecurity.DesDecrypt(paramArr[3]);
                Workor.BookID = DesSecurity.DesDecrypt(paramArr[4]);
                Workor.WorkUser = DesSecurity.DesDecrypt(paramArr[5]);
                Workor.MissionID = DesSecurity.DesDecrypt(paramArr[6]);

                if (string.IsNullOrEmpty(Workor.ServerIP) || string.IsNullOrEmpty(Workor.ServerPort) || string.IsNullOrEmpty(Workor.BookID) ||
                    string.IsNullOrEmpty(Workor.WorkUser) || string.IsNullOrEmpty(Workor.MissionID))
                {
                    throw new Exception("传入参数有误（有必须值为空）！");
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<book />");
                XmlElement root = doc.DocumentElement;
                root.SetAttribute("ServerIP", Workor.ServerIP);
                root.SetAttribute("ServerPort", Workor.ServerPort);
                root.SetAttribute("BookID", Workor.BookID);
                root.SetAttribute("WorkUser", Workor.WorkUser);
                root.SetAttribute("MissionID", Workor.MissionID);
                doc.Save(configPath);

                Application.Run(new OpenForm());
            }
            else
            {
                throw new Exception("传入参数（打开过程）有误！");
            }
        }

        static void StartEDITSave(string[] args)
        {
            string configPath = Application.StartupPath.TrimEnd('\\') + "\\xmleditor\\book.xml";

            Workor.WorkType = args[0];
            if (Workor.WorkType.Equals("1") || Workor.WorkType.Equals("2"))
            {
                //找到配置文件，加载服务器信息
                if (File.Exists(configPath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(configPath);
                    XmlElement root = doc.DocumentElement;
                    Workor.ServerIP = root.GetAttribute("ServerIP");
                    Workor.ServerPort = root.GetAttribute("ServerPort");
                    Workor.BookID = root.GetAttribute("BookID");
                    Workor.WorkUser = root.GetAttribute("WorkUser");
                    Workor.MissionID = root.GetAttribute("MissionID");

                    if (string.IsNullOrEmpty(Workor.ServerIP) || string.IsNullOrEmpty(Workor.ServerPort) || string.IsNullOrEmpty(Workor.BookID) ||
                        string.IsNullOrEmpty(Workor.WorkUser) || string.IsNullOrEmpty(Workor.MissionID))
                    {
                        throw new Exception("配置文件有误（有必须值为空）！");
                    }

                    Application.Run(new SaveForm());
                }
                else
                {
                    throw new Exception("工具必须的配置文件丢失！");
                }
            }
            else
            {
                throw new Exception("传入参数（保存过程）有误！");
            }
        }
    }
}
