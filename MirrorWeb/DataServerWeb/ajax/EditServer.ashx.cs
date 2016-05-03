using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Diagnostics;

using DRMS.BLL;
using DRMS.Model;
using DRMS.Web.Utility;

namespace DRMS.Web.ajax
{
    /// <summary>
    /// EditServer 的摘要说明
    /// </summary>
    public class EditServer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];

            switch (type)
            {
                case "-1":
                    //下载文件列表文件
                    TestFunc();
                    break;
                case "1":
                    //下载文件列表文件
                    DownloadFileList();
                    break;
                case "2":
                    //下载Xml文件
                    DownloadXmlFile();
                    break;
                case "3":
                    //下载图片文件
                    DownloadImageFile();
                    break;
                case "4":
                    //上传文件
                    UploadFile();
                    break;
                case "5":
                    //调用入库工具
                    StartEData();
                    break;
                case "6":
                    //删除文件
                    DeleteImageFile();
                    break;
                case "7":
                    //判断文件存在与否
                    XmlFileExist();
                    break;
                default:
                    break;
            }
        }

        public void TestFunc()
        {
            HttpContext.Current.Response.Write("Hello World!");
            HttpContext.Current.Response.End();
        }

        public void DownloadFileList()
        {
            //string bookdoi = HttpContext.Current.Request["bookdoi"];
            string mid = HttpContext.Current.Request["mid"];
            if (string.IsNullOrEmpty(mid))
            {
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.End();
                return;
            }

            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
            {
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.End();
                return;
            }
            string xmlpath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(xmlpath))
            {
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.End();
                return;
            }
            //获取图片路径，查询图片文件
            string dirpath = xmlpath.Substring(0, xmlpath.LastIndexOf('\\')).Trim('\\') + "\\Images";

            if (!Directory.Exists(dirpath))
            {
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.End();
                return;
            }
            string[] filelist = Directory.GetFiles(dirpath);
            StringBuilder sb = new StringBuilder();
            foreach (string filepath in filelist)
            {
                string filename = filepath.Substring(filepath.LastIndexOf('\\'));
                sb.Append(DesSecurity.DesEncrypt(filename) + "|");
            }

            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        public void XmlFileExist()
        {
            string mid = HttpContext.Current.Request["mid"];
            if (string.IsNullOrEmpty(mid))
                return;
            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
                return;
            string filepath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(filepath))
                return;
            if (!File.Exists(filepath))
                return;
            HttpContext.Current.Response.Write("1");
            HttpContext.Current.Response.End();
        }

        public void DownloadXmlFile()
        {
            string mid = HttpContext.Current.Request["mid"];
            if (string.IsNullOrEmpty(mid))
                return;
            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
                return;
            string filepath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(filepath))
                return;
            if (!File.Exists(filepath))
                return;
            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
        }

        public void DownloadImageFile()
        {
            string mid = HttpContext.Current.Request["mid"];
            string filename = DesSecurity.DesDecrypt(HttpContext.Current.Request["filename"]);
            if (string.IsNullOrEmpty(mid))
                return;
            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
                return;
            string xmlpath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(xmlpath))
                return;
            //查询文件
            string filepath = xmlpath.Substring(0, xmlpath.LastIndexOf('\\')).Trim('\\') + "\\Images\\" + filename;
            if (!File.Exists(filepath))
                return;
            HttpContext.Current.Response.WriteFile(filepath);
            HttpContext.Current.Response.End();
        }

        public void UploadFile()
        {
            string mid = HttpContext.Current.Request["mid"];
            string filename = DesSecurity.DesDecrypt(HttpContext.Current.Request["filename"]);
            if (string.IsNullOrEmpty(mid))
                return;
            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
                return;
            string xmlpath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(xmlpath))
                return;
            string bookPath = xmlpath.Substring(0, xmlpath.LastIndexOf('\\')).Trim('\\') + "\\";
            if (!Directory.Exists(bookPath))
            {
                Directory.CreateDirectory(bookPath);
            }
            string filepath = bookPath + mi.ResDOI + ".xml";
            if (!string.IsNullOrEmpty(filename))
            {
                filepath = bookPath + "Images\\" + filename;
                if (!Directory.Exists(bookPath + "Images\\"))
                {
                    Directory.CreateDirectory(bookPath + "Images\\");
                }
            }

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            foreach (string file in HttpContext.Current.Request.Files.AllKeys)
            {
                HttpPostedFile hpf = HttpContext.Current.Request.Files[file];
                hpf.SaveAs(filepath);
            }

            //using (FileStream fStream = File.Create(filepath))
            //{
            //    Stream inputStream = HttpContext.Current.Request.InputStream;
            //    int bufSize = 1024;
            //    int byteGet = 0;
            //    byte[] buf = new byte[bufSize];
            //    while ((byteGet = inputStream.Read(buf, 0, bufSize)) > 0)
            //    {
            //        fStream.Write(buf, 0, byteGet);
            //    }
            //    inputStream.Close();
            //}
        }

        #region 提交数据
        public void StartEData()
        {
            //string _strCmd = "C:\\Users\\Administrator\\Desktop\\社科测试\\6387ba1f-1d5c-4fd6-b7cb-dac8e046f79b|C:\\Users\\Administrator\\Desktop\\社科服务测试\\6387ba1f-1d5c-4fd7dd-b7cb-dac8e046f79b|1|192.168.100.222|4567|DBOWN||admin";//m_lpCmdLine;

            #region 处理参数
            string mid = HttpContext.Current.Request["mid"];
            string uploadtype = HttpContext.Current.Request["uploadtype"];
            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
                return;
            string xmlpath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(xmlpath))
                return;
            string olddoi = HttpContext.Current.Request["olddoi"];
            string bookPath = xmlpath.Substring(0, xmlpath.LastIndexOf('\\')).Trim('\\') + "|";
            if (uploadtype == "1")
            {
                string newdoi = HttpContext.Current.Request["newdoi"];
                bookPath += bookPath.Replace(olddoi, "Temp\\" + newdoi);
            }
            string user = HttpContext.Current.Request["user"];
            string note = DesSecurity.DesEncrypt(HttpContext.Current.Request["note"]);
            string ServerIP = System.Configuration.ConfigurationManager.AppSettings["IP"];
            string ServerPort = System.Configuration.ConfigurationManager.AppSettings["Port"];
            string ServerUN = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            string ServerPwd = System.Configuration.ConfigurationManager.AppSettings["Password"];
            #endregion

            #region 根据任务id获取资源表名称
            int recordcount = 0;
            IList<DataBaseListInfo> lstDbli = (new DataBaseList()).GetList("DATABASETYPE='" + mi.ResType + "'", 1, 1, out recordcount, true);
            if (lstDbli == null || lstDbli.Count <= 0)
                return;
            string tableName = lstDbli[0].TableName;
            #endregion

            string workderPath = System.Configuration.ConfigurationManager.AppSettings["Worker"];
            if (!string.IsNullOrEmpty(workderPath))
            {
                try
                {
                    string args = "\"" + bookPath + "|" + uploadtype + "|" + ServerIP + "|" + ServerPort + "|" + ServerUN + "|" + ServerPwd + "|" + user + "|" + tableName + "\"";
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.Arguments = args;
                    psi.FileName = workderPath;
                    psi.CreateNoWindow = true;
                    Process tool = Process.Start(psi);
                    if (tool != null)
                    {
                        tool.WaitForExit();
                    }

                    UpdateMission(bookPath, mid, note, user, uploadtype);

                    return;
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            else
            {
                return;
            }
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
                //产生新版本
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
        private static bool UpdateResObjByDoi(DataBaseType dbtype, string doi)
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
        #endregion

        public void DeleteImageFile()
        {
            string mid = HttpContext.Current.Request["mid"];
            if (string.IsNullOrEmpty(mid))
                return;
            //获取资源路径
            MissionInfo mi = (new Mission()).GetItem(mid);
            if (mi == null)
                return;
            string xmlpath = FileManagementUtility.GetXMLPathByResDoi((DataBaseType)mi.ResType, mi.ResDOI);
            if (string.IsNullOrEmpty(xmlpath))
                return;
            string dirpath = xmlpath.Substring(0, xmlpath.LastIndexOf('\\')).Trim('\\') + "\\Images\\";
            if (Directory.Exists(dirpath))
            {
                Directory.Delete(dirpath, true);
                //删除目录后，重新创建目录
                Directory.CreateDirectory(dirpath);
                HttpContext.Current.Response.Write("1");
                HttpContext.Current.Response.End();
                return;
            }
            else
                return;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}