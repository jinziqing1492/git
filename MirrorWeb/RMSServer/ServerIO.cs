using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;
using DRMS.BLL;
using DRMS.Model;

namespace DRMS.RMSServer
{
    /// <summary>
    /// 处理服务器端IO操作类
    /// </summary>
    class ServerIO
    {
        private Message _msg = null;

        public ServerIO(Message msg)
        {
            _msg = msg;
        }

        public void ServerHandle()
        {
            while (true)
            {
                try
                {
                    string strType = _msg.RecieveMessge();
                    if (string.IsNullOrEmpty(strType) || string.IsNullOrWhiteSpace(strType))
                    {
                        continue;
                    }
                    //_msg.WriteLog(strType);
                    int type = Convert.ToInt32(strType);
                    switch (type)
                    {
                        case 1:
                            //处理上传文件
                            UploadFile();
                            break;
                        case 2:
                            //处理下载文件
                            DownloadFile();
                            break;
                        case 3:
                            //处理删除文件
                            DeleteFile();
                            break;
                        case 4:
                            //获取服务器目录
                            GetServerDirectory();
                            break;
                        case 5:
                            //处理创建文件夹
                            CreateDirectory();
                            break;
                        case 6:
                            //处理删除文件夹
                            DeleteDirectory();
                            break;
                        case 7:
                            //处理文件夹是否存在
                            DirectoryExist();
                            break;
                        case 8:
                            //处理文件夹重命名
                            RenameDirectory();
                            break;
                        case 9:
                            //获取服务器各个硬盘盘符及其空闲空间
                            GetServerDisks();
                            break;
                        case 10:
                            //获取服务器各个硬盘盘符及其空闲空间
                            GetGrandSonDir();
                            break;
                        case 11:
                            //获取服务器中安装的服务端程序目录
                            GetServerInstallPath();
                            break;
                        case 12:
                            //获取服务器中安装的服务端文件是否存在
                            FileExist();
                            break;
                        case 13:
                            //获取指定目录下面所有文件
                            GetFileList();
                            break;
                        case 14:
                            //启动服务器端进程
                            StartServerProcess();
                            break;
                        case 15:
                            //获取文件的最后写入时间
                            GetFileLastWriteTime();
                            break;
                        case -1:
                            //断开连接
                            _msg.Close();
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _msg.WriteLog(ex.ToString());
                    //_msg.Close();
                }
            }
        }

        public void UploadFile()
        {
            string desPath = _msg.RecieveMessge();
            string fileName = desPath.Substring(desPath.LastIndexOf('\\') + 1);
            //_msg.WriteLog("收到上传请求，文件名：" + fileName);
            string parentDir = desPath.Substring(0, desPath.LastIndexOf('\\'));
            if (!Directory.Exists(parentDir))
            {
                Directory.CreateDirectory(parentDir);
            }
            //string desPath = parentDir.TrimEnd('\\') + "\\" + fileName;
            _msg.RecieveFile(desPath);
            _msg.SendByte(1);
            //_msg.Close();
        }

        public void FileExist()
        {
            string srcPath = _msg.RecieveMessge();
            if (File.Exists(srcPath))
            {
                _msg.SendByte(1);
            }
            else
            {
                _msg.SendByte(0);
            }
            //_msg.Close();
        }

        public void DownloadFile()
        {
            string srcPath = _msg.RecieveMessge();
            if (File.Exists(srcPath))
            {
                _msg.SendByte(1);
                //_msg.WriteLog("存在： " + srcPath);
                _msg.SendFile(srcPath);
            }
            else
            {
                //_msg.WriteLog("不存在： " + srcPath);
                _msg.SendByte(0);
            }
            //_msg.Close();
        }

        public void DeleteFile()
        {
            string filePath = _msg.RecieveMessge();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            //_msg.Close();
        }

        public void GetFileList()
        {
            string parentPath = _msg.RecieveMessge();
            if (Directory.Exists(parentPath))
            {
                try
                {
                    string[] subFiles = Directory.GetFiles(parentPath, "*", SearchOption.TopDirectoryOnly);
                    if (subFiles != null && subFiles.Length > 0)
                    {
                        StringBuilder strSub = new StringBuilder();
                        foreach (string sub in subFiles)
                        {
                            strSub.Append(sub);
                            strSub.Append("*");
                        }
                        string result = strSub.ToString().TrimEnd('*');
                        if (!string.IsNullOrEmpty(result))
                        {
                            //_msg.SendMessage("1");
                            _msg.SendMessage(result);
                            return;
                        }
                    }
                }
                catch { }
            }
            _msg.SendMessage("0");
        }

        public void GetServerDirectory()
        {
            string parentPath = _msg.RecieveMessge();
            if (Directory.Exists(parentPath))
            {
                try
                {
                    string[] subDirs = Directory.GetDirectories(parentPath, "*", SearchOption.TopDirectoryOnly);
                    if (subDirs != null && subDirs.Length > 0)
                    {
                        StringBuilder strSub = new StringBuilder();
                        foreach (string sub in subDirs)
                        {
                            if (sub.EndsWith("...."))
                            {
                                continue;
                            }
                            strSub.Append(sub);
                            strSub.Append("*");
                        }
                        string result = strSub.ToString().TrimEnd('*');
                        if (!string.IsNullOrEmpty(result))
                        {
                            _msg.SendMessage("1");
                            _msg.SendMessage(result);
                            return;
                        }
                    }
                }
                catch { }
            }
            _msg.SendMessage("0");
            //_msg.Close();
        }

        public void CreateDirectory()
        {
            string dirPath = _msg.RecieveMessge();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            _msg.SendByte(1);
            //_msg.Close();
        }

        public void DeleteDirectory()
        {
            string dirPath = _msg.RecieveMessge();
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
            _msg.SendByte(1);
            //_msg.Close();
        }

        public void RenameDirectory()
        {
            string dirPath = _msg.RecieveMessge();
            string newPath = _msg.RecieveMessge();
            if (Directory.Exists(dirPath))
            {
                Directory.Move(dirPath, newPath);
            }
            //_msg.Close();
        }

        public void DirectoryExist()
        {
            string dirPath = _msg.RecieveMessge();
            if (Directory.Exists(dirPath))
            {
                _msg.SendMessage("1");
            }
            else
            {
                _msg.SendMessage("0");
            }
            //_msg.Close();
        }

        public void GetFileLastWriteTime()
        {
            string filePath = _msg.RecieveMessge();
            if (!File.Exists(filePath))
            {
                _msg.SendMessage(DateTime.MinValue.ToString());
                return;
            }
            FileInfo fi = new FileInfo(filePath);
            string fiLWT = fi.LastWriteTime.ToString();
            _msg.SendMessage(fiLWT);
            //_msg.Close();
        }

        public void GetServerDisks()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<server />");
            XmlElement root = doc.DocumentElement;
            DriveInfo[] drivers = DriveInfo.GetDrives();
            foreach (DriveInfo di in drivers)
            {
                if (di.DriveType == DriveType.Fixed)
                {
                    XmlElement element = doc.CreateElement("driver");
                    element.SetAttribute("name", di.Name);
                    element.SetAttribute("size", di.AvailableFreeSpace.ToString());
                    root.AppendChild(element);
                }
            }
            _msg.SendMessage(doc.OuterXml);
        }

        public void GetGrandSonDir()
        {
            string grandPath = _msg.RecieveMessge();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<server />");
            XmlElement root = doc.DocumentElement;
            try
            {
                string[] childs = Directory.GetDirectories(grandPath, "*", SearchOption.TopDirectoryOnly);
                foreach (string di in childs)
                {
                    if (di.EndsWith("...."))
                    {
                        continue;
                    }
                    XmlElement element = doc.CreateElement("child");
                    element.SetAttribute("name", di);
                    try
                    {
                        string[] grandsons = Directory.GetDirectories(di, "*", SearchOption.TopDirectoryOnly);
                        foreach (string grand in grandsons)
                        {
                            XmlElement son = doc.CreateElement("grandson");
                            son.SetAttribute("name", grand);
                            element.AppendChild(son);
                        }
                    }
                    catch { }
                    root.AppendChild(element);
                }
                _msg.SendMessage(doc.OuterXml);
                return;
            }
            catch { }
            _msg.SendMessage("0");
        }

        public void GetServerInstallPath()
        {
            _msg.SendMessage(@"D:\ForTest");
        }

        public void StartServerProcess()
        {
            //while (System.IO.File.Exists(@"d:\debug.sleep"))
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
            //string _strCmd = "C:\\Users\\Administrator\\Desktop\\社科测试\\6387ba1f-1d5c-4fd6-b7cb-dac8e046f79b|C:\\Users\\Administrator\\Desktop\\社科服务测试\\6387ba1f-1d5c-4fd7dd-b7cb-dac8e046f79b|1|192.168.100.222|4567|DBOWN||admin";//m_lpCmdLine;

            string bookPath = _msg.RecieveMessge();
            string type = _msg.RecieveMessge();
            string user = _msg.RecieveMessge();
            string note = _msg.RecieveMessge();
            string missionID = _msg.RecieveMessge();
            string ServerIP = System.Configuration.ConfigurationManager.AppSettings["IP"];
            string ServerPort = System.Configuration.ConfigurationManager.AppSettings["Port"];
            string ServerUN = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            string ServerPwd = System.Configuration.ConfigurationManager.AppSettings["Password"];

            #region 根据任务id获取资源表名称
            DRMS.BLL.Mission _m = new DRMS.BLL.Mission();
            MissionInfo mi = _m.GetItem(missionID);
            if (mi == null)
            {
                _msg.SendByte(0);
                return;
            }
            int recordcount = 0;
            IList<DataBaseListInfo> lstDbli = (new DataBaseList()).GetList("DATABASETYPE='" + mi.ResType + "'", 1, 1, out recordcount, true);
            if (lstDbli == null || lstDbli.Count <= 0)
            {
                _msg.SendByte(0);
                return;
            }
            #endregion
            string tableName = lstDbli[0].TableName;

            string args = "\"" + bookPath + "|" + type + "|" + ServerIP + "|" + ServerPort + "|" + ServerUN + "|" + ServerPwd + "|" + user + "|" + tableName + "\"";
            string workderPath = GetRegistData("Worker");
            if (!string.IsNullOrEmpty(workderPath))
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.Arguments = args;
                    psi.FileName = workderPath;
                    psi.CreateNoWindow = true;
                    Process tool = Process.Start(psi);
                    if (tool != null)
                    {
                        tool.WaitForExit();
                    }
                    //更新任务状态
                    UpdateMission(bookPath, missionID, note, user, type);
                    _msg.SendByte(1);
                    return;
                }
                catch (Exception ex)
                {
                    _msg.SendByte(0);
                    //File.AppendAllText("\\rkserver.txt", ex.ToString());
                    return;
                }
            }
            _msg.SendByte(0);
        }

        //更新任务状态
        private void UpdateMission(string bookPath, string mid, string note, string user, string type)
        {
            Log _l = new Log();
            DRMS.BLL.Mission _m = new DRMS.BLL.Mission();
            string newDoi = ""; string oldDoi = "";
            bool bResult = true;
            if (type.Equals("0"))
            {
                //覆盖旧版本，只需要更新旧版数据就行
                newDoi = bookPath.Trim(new char[] { '\\', '|' }).Substring(bookPath.Trim('\\').LastIndexOf('\\') + 1);
            }
            else if (type.Equals("1"))
            {
                //添加新版本，需要更新新版数据,并且将审核任务置向新数据，因为审核的是新版本数据
                string[] paths = bookPath.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (paths.Length > 1)
                {
                    string oldPath = paths[0].Trim('\\');
                    string newPath = paths[1].Trim('\\');
                    newDoi = newPath.Substring(newPath.LastIndexOf('\\') + 1);
                    oldDoi = oldPath.Substring(oldPath.LastIndexOf('\\') + 1);
                }
            }

            MissionInfo mi = _m.GetItem(mid);
            if (bResult && mi != null)
            {
                if (type.Equals("1"))
                {
                    //更新旧版本的资源状态为 -1
                    bResult = UpdateResObjByDoi((DataBaseType)mi.ResType, oldDoi);
                    if (!bResult)
                    {
                        _l.Add((DataBaseType)mi.ResType, LogType.UPDATE, newDoi, "更新" + mi.ResTypeStr, "更新" + mi.ResTypeStr + "失败");
                        return;
                    }
                }

                mi.ResDOI = newDoi;
                mi.FinishDate = DateTime.Now;
                mi.WorkStatus = -1;
                bResult = _m.Update(mi);

                //添加任务到数据库--编辑待审核
                MissionInfo miNew = new MissionInfo();
                miNew.ResType = mi.ResType;
                miNew.ResDOI = newDoi;
                miNew.ResName = mi.ResName;
                miNew.ExecuteUser = user;
                miNew.CreateTime = DateTime.Now;
                miNew.Remark = note;
                miNew.SYS_FLD_CHECK_STATE = Convert.ToInt32(MissionStatusType.PENDINGPEDITING);
                bResult = bResult && _m.Add(miNew);

                try
                {
                    //添加操作日志记录
                    if (bResult)
                    {
                        _l.Add((DataBaseType)mi.ResType, LogType.UPDATE, newDoi, "更新" + mi.ResTypeStr, "更新" + mi.ResTypeStr + "成功");
                    }
                    else
                        _l.Add((DataBaseType)mi.ResType, LogType.UPDATE, newDoi, "更新" + mi.ResTypeStr, "更新" + mi.ResTypeStr + "失败");
                }
                catch { }
            }
        }

        /// <summary>
        /// 根据资源类型和doi更新资源状态
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="doi"></param>
        /// <returns></returns>
        public static bool UpdateResObjByDoi(DataBaseType dbtype, string doi)
        {
            bool bResult = true;
            switch (dbtype)
            {
                case DataBaseType.BOOKTDATA:
                    Book b = new Book();
                    BookInfo bi = b.GetItem(doi);
                    if (bi != null)
                    {
                        bi.SYS_FLD_CHECK_STATE = -1;
                        bResult = b.Update(bi);
                    }
                    break;
                case DataBaseType.CONFERENCEARTICLE:
                    ConferenceArticle conarticle = new ConferenceArticle();
                    ConferenceArticleInfo cai = conarticle.GetItem(doi);
                    if (cai != null)
                    {
                        cai.SYS_FLD_CHECK_STATE = -1;
                        bResult = conarticle.Update(cai);
                    }
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    ConferencePaper conferencepaper = new ConferencePaper();
                    ConferencePaperInfo cpi = conferencepaper.GetItem(doi);
                    if (cpi != null)
                    {
                        cpi.SYS_FLD_CHECK_STATE = -1;
                        bResult = conferencepaper.Update(cpi);
                    }
                    break;
                case DataBaseType.CRITERION:
                    StdData stddata = new StdData();
                    StdDataInfo sdi = stddata.GetItem(doi);
                    if (sdi != null)
                    {
                        sdi.SYS_FLD_CHECK_STATE = -1;
                        bResult = stddata.Update(sdi);
                    }
                    break;
                case DataBaseType.REFERENCEBOOK:
                    ToolBook toolbook = new ToolBook();
                    ToolBookInfo tbi = toolbook.GetItem(doi);
                    if (tbi != null)
                    {
                        tbi.SYS_FLD_CHECK_STATE = -1;
                        bResult = toolbook.Update(tbi);
                    }
                    break;
                case DataBaseType.LOGICALDATABASE:
                    LogicalDataBase ldb = new LogicalDataBase();
                    LogicalDataBaseInfo ldi = ldb.GetItem(doi);
                    if (ldi != null)
                    {
                        ldi.SYS_FLD_CHECK_STATE = -1;
                        bResult = ldb.Update(ldi);
                    }
                    break;
                case DataBaseType.AUDIODATA:
                    Audio audio = new Audio();
                    AudioInfo ai = audio.GetItem(doi);
                    if (ai != null)
                    {
                        ai.SYS_FLD_CHECK_STATE = -1;
                        bResult = audio.Update(ai);
                    }
                    break;
                case DataBaseType.JOURNALARTICLE:
                    JournalArticle jarticle = new JournalArticle();
                    JournalArticleInfo jai = jarticle.GetItem(doi);
                    if (jai != null)
                    {
                        jai.SYS_FLD_CHECK_STATE = -1;
                        bResult = jarticle.Update(jai);
                    }
                    break;
                case DataBaseType.MAGAZINEARTICLE:
                    MagazineArticle marticle = new MagazineArticle();
                    MagazineArticleInfo mai = marticle.GetItem(doi);
                    if (mai != null)
                    {
                        mai.SYS_FLD_CHECK_STATE = -1;
                        bResult = marticle.Update(mai);
                    }
                    break;
                case DataBaseType.NEWSPAPERARTICLE:
                    NewsPaperArticle narticle = new NewsPaperArticle();
                    NewsPaperArticleInfo npai = narticle.GetItem(doi);
                    if (npai != null)
                    {
                        npai.SYS_FLD_CHECK_STATE = -1;
                        bResult = narticle.Update(npai);
                    }
                    break;
                case DataBaseType.PICDATA:
                    Pic pic = new Pic();
                    PicInfo pi = pic.GetItem(doi);
                    if (pi != null)
                    {
                        pi.SYS_FLD_CHECK_STATE = -1;
                        bResult = pic.Update(pi);
                    }
                    break;
                case DataBaseType.VIDEODATA:
                    Video video = new Video();
                    VideoInfo vi = video.GetItem(doi);
                    if (vi != null)
                    {
                        vi.SYS_FLD_CHECK_STATE = -1;
                        bResult = video.Update(vi);
                    }
                    break;
                case DataBaseType.YEARBOOK:
                    YearBookYear yarticle = new YearBookYear();
                    YearBookYearInfo ybyi = yarticle.GetItem(doi);
                    if (ybyi != null)
                    {
                        ybyi.SYS_FLD_CHECK_STATE = -1;
                        bResult = yarticle.Update(ybyi);
                    }
                    break;
                case DataBaseType.CONTRACT:
                    Contract contract = new Contract();
                    ContractInfo ci = contract.GetItem(doi);
                    if (ci != null)
                    {
                        ci.SYS_FLD_CHECK_STATE = -1;
                        bResult = contract.Update(ci);
                    }
                    break;
                case DataBaseType.AUTHOR:
                    Author author = new Author();
                    AuthorInfo authi = author.GetItem(doi);
                    if (authi != null)
                    {
                        authi.SYS_FLD_CHECK_STATE = -1;
                        bResult = author.Update(authi);
                    }
                    break;
                case DataBaseType.ORG:
                    Org org = new Org();
                    OrgInfo oi = org.GetItem(doi);
                    if (oi != null)
                    {
                        oi.SYS_FLD_CHECK_STATE = -1;
                        bResult = org.Update(oi);
                    }
                    break;
                case DataBaseType.ORIGINALDATA:
                    OriginalData original = new OriginalData();
                    OriginalDataInfo odi = original.GetItem(doi);
                    if (odi != null)
                    {
                        odi.SYS_FLD_CHECK_STATE = -1;
                        bResult = original.Update(odi);
                    }
                    break;
                case DataBaseType.THESIS:
                    Thesis thesis = new Thesis();
                    ThesisInfo ti = thesis.GetItem(doi);
                    if (ti != null)
                    {
                        ti.SYS_FLD_CHECK_STATE = -1;
                        bResult = thesis.Update(ti);
                    }
                    break;
                case DataBaseType.JOURNALYEAR:
                    JournalYear journalyear = new JournalYear();
                    JournalYearInfo jyi = journalyear.GetItem(doi);
                    if (jyi != null)
                    {
                        jyi.SYS_FLD_CHECK_STATE = -1;
                        bResult = journalyear.Update(jyi);
                    }
                    break;
                case DataBaseType.NEWSPAPERYEAR:
                    NewsPaperYear newspaperyear = new NewsPaperYear();
                    NewsPaperYearInfo npyi = newspaperyear.GetItem(doi);
                    if (npyi != null)
                    {
                        npyi.SYS_FLD_CHECK_STATE = -1;
                        bResult = newspaperyear.Update(npyi);
                    }
                    break;
                case DataBaseType.MAGAZINEYEAR:
                    MagazineYear magazineyear = new MagazineYear();
                    MagazineYearInfo myi = magazineyear.GetItem(doi);
                    if (myi != null)
                    {
                        myi.SYS_FLD_CHECK_STATE = -1;
                        bResult = magazineyear.Update(myi);
                    }
                    break;
                default:
                    break;
            }
            return bResult;
        }

        private string GetRegistData(string name)
        {
            string rktoolpath = System.Configuration.ConfigurationManager.AppSettings["Worker"];
            return string.IsNullOrEmpty(rktoolpath) ? "" : rktoolpath;

            string registData = string.Empty;
            try
            {
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey ttkn = software.OpenSubKey("TTKN", true);
                RegistryKey ssap = ttkn.OpenSubKey("SSAP", true);
                if (ssap == null)
                    File.AppendAllText("D:\\同方-内容资源管理平台\\rkserver.txt",
                        "1:" + name + ":ssap为空:" + DateTime.Now.ToString() + Environment.NewLine);

                registData = ssap.GetValue(name).ToString();
            }
            catch (Exception ex)
            {
                File.AppendAllText("D:\\同方-内容资源管理平台\\rkserver.txt",
                    "读取worker值失败:" + ex.ToString() + DateTime.Now.ToString() + Environment.NewLine);
            }
            return registData;
        }
    }
}
