using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml;


namespace DRMS.RMSClient
{
    public class SocketClient
    {
        private string _serverIP;
        private int _serverPort;
        private Message _msg;

        private string _error;
        public string ErrorMsg
        {
            get { return _error; }
        }

        public SocketClient(string ip, int port)
        {
            _serverIP = ip;
            _serverPort = port;
        }

        public bool Connect()
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(_serverIP), _serverPort);
                if (client.Connected)
                {
                    _msg = new Message(client.Client);
                    return true;
                }
                client.Close();
            }
            catch
            {
            }
            return false;
        }

        public void DisConnect()
        {
            if (_msg != null)
            {
                _msg.SendMessage("-1");
                _msg.Close();
            }
        }

        public bool UploadFile(string filePath, string fileName, string parentDir)
        {
            try
            {
                _msg.SendMessage("1");
                //_msg.SendMessage(fileName);
                //_msg.SendMessage(parentDir);
                _msg.SendMessage(parentDir.TrimEnd('\\') + "\\" + fileName);
                _msg.SendFile(filePath);
                if (_msg.RecieveByte())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return false;
        }

        public bool DownloadFile(string filePath, string srcPath)
        {
            try
            {
                _msg.SendMessage("2");
                _msg.SendMessage(srcPath);
                bool exsit = _msg.RecieveByte();
                if (exsit)
                {
                    _msg.RecieveFile(filePath);
                    return true;
                }
                else
                {
                    _error = "服务器上不存在该文件";
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return false;
        }

        public bool FileExist(string filePath)
        {
            try
            {
                _msg.SendMessage("12");
                _msg.SendMessage(filePath);
                bool exsit = _msg.RecieveByte();
                if (exsit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _msg.WriteLog(ex.ToString());
                return false;
            }
        }

        public bool DeleteFile(string srcPath)
        {
            _msg.SendMessage("3");
            _msg.SendMessage(srcPath);
            return true;
        }

        public string[] GetFileList(string parentPath)
        {
            string[] dirList = null;
            _msg.SendMessage("13");
            _msg.SendMessage(parentPath);
            string list = _msg.RecieveMessge();
            if (list.Equals("0"))
            {
                dirList = null;
            }
            else
            {
                dirList = list.Split('*');
            }
            return dirList;
        }

        public bool CreateDirectory(string dirPath)
        {
            _msg.SendMessage("5");
            _msg.SendMessage(dirPath);
            if (_msg.RecieveByte())
            {
                return true;
            }
            return false;
        }

        public bool DeleteDirectory(string dirName)
        {
            _msg.SendMessage("6");
            _msg.SendMessage(dirName);
            if (_msg.RecieveByte())
            {
                return true;
            }
            return true;
        }

        public string[] GetServerSubDir(string parentPath)
        {
            string[] dirList = null;
            _msg.SendMessage("4");
            _msg.SendMessage(parentPath);
            string exist = _msg.RecieveMessge();
            if (exist.Equals("1"))
            {
                string list = _msg.RecieveMessge();
                dirList = list.Split('*');
            }
            return dirList;
        }

        public XmlDocument GetServerDisks()
        {
            _msg.SendMessage("9");
            string disks = _msg.RecieveMessge();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(disks);
            return doc;
        }

        public XmlDocument GetGrandSonDir(string path)
        {
            _msg.SendMessage("10");
            _msg.SendMessage(path);
            string disks = _msg.RecieveMessge();
            if (!disks.Equals("0"))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml(disks);
                    return doc;
                }
                catch
                {
                }
            }
            return null;
        }

        public string GetServerInstallPath()
        {
            _msg.SendMessage("11");
            string path = _msg.RecieveMessge();
            return path;
        }

        public bool DirectoryExist(string dirPath)
        {
            _msg.SendMessage("7");
            _msg.SendMessage(dirPath);
            string exist = _msg.RecieveMessge();
            if (exist == "1")
                return true;
            return false;
        }

        public DateTime GetFileLastWriteTime(string filePath)
        {
            _msg.SendMessage("15");
            _msg.SendMessage(filePath);
            string lwt = _msg.RecieveMessge();
            try
            {
                return Convert.ToDateTime(lwt);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 通过通信启动服务器端入库程序
        /// 服务器IP端口号之类的将在服务器端配置好，因为入库程序是手动部署在服务器端的。
        /// </summary>
        /// <param name="bookid">需要入库的图书Doi（如果是生成新版，ＩＤ格式为: newdoi_olddoi）</param>
        /// <param name="type">入库方式（0 覆盖旧版，1 生成新版）</param>
        /// <param name="updateUser">添加或者更新的用户名,用于更新【更新用户】或者【添加用户】字段</param>
        /// <returns></returns>
        public bool StartServerProcess(string doi, string type, string user, string note, string missionID)
        {
            _msg.SendMessage("14");
            _msg.SendMessage(doi);
            _msg.SendMessage(type);
            _msg.SendMessage(user);
            if (string.IsNullOrEmpty(note))
            {
                note = "无";
            }
            _msg.SendMessage(note);
            _msg.SendMessage(missionID);
            return _msg.RecieveByte();
        }
    }
}
