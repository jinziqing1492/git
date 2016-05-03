using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;


using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.view
{
    public partial class DownLoadDrmFile : BasePage.BasePage
    {

        public string OTime;
        public string STime;
        public string Cshu = "";
        private string ProductName = ConfigurationManager.AppSettings["DrmProductName"];
        string IsDistribute = ConfigurationManager.AppSettings["IsBookDistribute"];//1是分装 0是未分装
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string doi = CNKI.BaseFunction.NormalFunction.GetQueryString("doi", "");
            string type = CNKI.BaseFunction.NormalFunction.GetQueryString("type", "");
            if (!IsPostBack)
            {
                GetData(doi, type);
            }
        }

        /// <summary>
        /// 根据不同的数据类型提取数据 并进行相应的参数设置
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="type"></param>
        protected void GetData(string doi, string type)
        {
            int intType = CNKI.BaseFunction.StructTrans.TransNum(type);
            switch (intType)
            {
                case (int)DataBaseType.BOOKTDATA:
                case (int)DataBaseType.REFERENCEBOOK:
                    {
                        Book bookdll = new Book();
                        BookInfo bookItem = bookdll.GetItem(doi);//这个地方可以 简略的提出几项 提高效率
                        if (bookItem != null)
                        {
                            BindData(bookItem.SYS_FLD_FILEPATH, bookItem.SYS_FLD_VIRTUALPATHTAG, bookItem.Name);
                        }
                    }
                    break;
                case (int)DataBaseType.CRITERION:
                    {
                        StdData bookdll = new StdData();
                        StdDataInfo bookItem = bookdll.GetItem(doi);//这个地方可以 简略的提出几项 提高效率
                        if (bookItem != null)
                        {
                            BindData(bookItem.SYS_FLD_FILEPATH, bookItem.SYS_FLD_VIRTUALPATHTAG, bookItem.Name);
                        }
                    }
                    break;
                case (int)DataBaseType.CONFERENCEPAPER:
                    {
                        ConferencePaper bookdll = new ConferencePaper();
                        ConferencePaperInfo bookItem = bookdll.GetItem(doi);//这个地方可以 简略的提出几项 提高效率
                        if (bookItem != null)
                        {
                            BindData(bookItem.SYS_FLD_FILEPATH, bookItem.SYS_FLD_VIRTUALPATHTAG, bookItem.Name);
                        }
                    }
                    break;
                case (int)DataBaseType.CONFERENCEARTICLE:
                    {
                        ConferenceArticle bookdll = new ConferenceArticle();
                        ConferenceArticleInfo bookItem = bookdll.GetItem(doi);//这个地方可以 简略的提出几项 提高效率
                        if (bookItem != null)
                        {
                            BindData(bookItem.SYS_FLD_FILEPATH, bookItem.SYS_FLD_VIRTUALPATHTAG, bookItem.Name);
                        }
                    }
                    break;
                case (int)DataBaseType.BOOKCHAPTER:
                case (int)DataBaseType.STDDATACHAPTER:
                    {
                        Chapter bookdll = new Chapter();
                        ChapterInfo bookItem = bookdll.GetItem(doi);//这个地方可以 简略的提出几项 提高效率
                        if (bookItem != null)
                        {
                            BindData(bookItem.SYS_FLD_FILEPATH, bookItem.SYS_FLD_VIRTUALPATHTAG, bookItem.Title);
                        }
                    }
                    break;
                default:

                    break;
            }
            //debug
            // BindData("", "", "");
        }

        /// <summary>
        /// 绑定下载数据 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="virtualTag"></param>
        public void BindData(string FilePath, string virtualTag, string title)
        {
            //需要从接口读出 开始和结束时间

            //设置起始结束时间 和终端数
            //开始时间   时间为空 已系统当前时间开始
            if (string.IsNullOrEmpty(OTime))
            {
                OTime = DateTime.Now.ToString("yyyy-MM-01");// DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            }
            //结束时间 时间为空 默认一年时间结束
            if (string.IsNullOrEmpty(STime))
            {
                STime = DateTime.Now.AddYears(1).ToString("yyyy-MM-01");// Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) + 1) + "-" + DateTime.Now.Month.ToString() + "-01";
            }
            Cshu = "3";
            string pdfFile = GetRealFile(FilePath, virtualTag);
            if (!string.IsNullOrEmpty(pdfFile))
            {
                PDFDrm(pdfFile, title);
            }
        }

        /// <summary>
        /// 获取真实路径文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="virtualTag"></param>
        /// <returns></returns>
        public string GetRealFile(string FilePath, string virtualTag)
        {
            string virtulpath = Config.GetVirtalPath((CNKI.BaseFunction.StructTrans.TransNum(virtualTag) * 100).ToString());
            if (IsDistribute == "1")
            {
                virtulpath = Config.GetVirtalPath(virtualTag);
            }
            string result = string.Empty;
            ////debug
            //result = @"E:\1.pdf";
            //return result;
            //if (string.IsNullOrWhiteSpace(virtulpath))
            //{
            //    return result;
            //}


            string fullFilePath = Server.MapPath("~/" + virtulpath);// +"\\" + FilePath;

            if (FilePath.StartsWith("\\"))
            {
                fullFilePath = fullFilePath + FilePath;
            }
            else
            {
                fullFilePath = fullFilePath + "\\" + FilePath; ;
            }
            if (File.Exists(fullFilePath))
            {
                result = fullFilePath;
            }
            else
                Response.Write("<script>alert('未找到PDF文件!该文件不存在或已删除！');window.parent.close();</script>");
            //return string.Empty;
            // string preDrmVirtual = ConfigurationManager.AppSettings["PreDrmFile"].ToString();
            // string fullFilePath = Server.MapPath("~/" + preDrmVirtual) + "\\" + FilePath;
            //if (File.Exists(fullFilePath))
            //{
            //    result = fullFilePath;
            //}

            return result;
        }

        /// <summary>
        /// 加密图书下载 
        /// </summary>
        /// <param name="PdfName">真实的pdf路径</param>
        /// <param name="title">原始文件名</param>
        public void PDFDrm(string PdfName, string title)
        {
            string StrFLID = string.Empty;
            string strBookID = string.Empty;
            string strFileName = string.Empty;
            string fileGuid = string.Empty;
            strBookID = "R100001";
            strFileName = PdfName;
            string RealFileName = title + ".caj";
            byte[] bFileRight = null;
            string username = "zx";// Utility.Utility.GetUserName();
            try
            {
                //下载需要和用户名绑定 如果是服务器模式 我可以限制打开次数 如果是证书模式 需要根据用户名限制打开模式
                bFileRight = UpdateRigth(strBookID, username, File.ReadAllBytes(strFileName).Length, strFileName);
            }
            catch (Exception e)
            {
                //Alert(string.Format("alert('下载失败：{0}！');", e.Message));               
            }
            //下载
            if (bFileRight != null)
            {
                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(RealFileName, System.Text.Encoding.UTF8));
                //  Response.AddHeader("Content-Length", (DataLength + rightBytes.Length).ToString());
                byte[] bRet = new byte[10240];
                FileStream teb_streams = null;
                try
                {
                    teb_streams = File.Open(strFileName, FileMode.Open, FileAccess.Read);
                    long DataLength = teb_streams.Length;
                 //   Response.AddHeader("Content-Length", (DataLength + bFileRight.Length).ToString());
                    //输出全文流
                    while (DataLength > 0 && Response.IsClientConnected)
                    {
                        int lengthRead = teb_streams.Read(bRet, 0, 10240);//读取的大小 Response.OutputStream.Write(buffer, 0, lengthRead); 
                        Response.OutputStream.Write(bRet, 0, lengthRead);
                        Response.Flush();
                        DataLength = DataLength - lengthRead;
                    }
                    //将生成的权限信息附加在输出流之后
                    Response.OutputStream.Write(bFileRight, 0, bFileRight.Length);
                    Response.Flush();

                    Response.End();
                }
                catch
                {

                }
                finally
                {
                    if (teb_streams != null)
                    {
                        teb_streams.Close();
                    }
                }

            }
            else
            {
                // Alert("下载失败：数据加密错误！");
            }
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="UserName"></param>
        /// <param name="DataLength">pdf文件长度</param>
        /// <param name="FileName">pdf路径</param>
        /// <returns></returns>
        private byte[] UpdateRigth(string BookID, string UserName, int DataLength, string FileName)
        {
            //DRMLib.IRightMngr rmObj = new DRMLib.BookRightMngr("SqlConnectionStringDRM");
            DRMLib.RightModel drmModel = new DRMLib.RightModel();
            //DRMLib.RightModel drmModel = null;
            // 生成证书 
            int errorCode = 0;
            string DrmServer = ConfigurationManager.AppSettings["DRMServer"].ToString();
            //生成了文件的唯一标识
            string fileID = DRMLib.FileEncrypt.CreateCertBySale(UserName, Path.GetFileName(FileName), "4f04044f5acfda18239a75fc98b53c5c", ProductName, out errorCode, DrmServer);

            #region  加密模版
            //// 类型 0 服务器加密：1 证书加密
            //drmModel.Type = 0;
            //// 是否使用时间限制
            //drmModel.TimeLimit = true;
            //// 0起始时间 结束时间 1打开天数
            ////if (rbTimeArea.Checked)
            ////{
            //drmModel.TimeLimitType = 0;
            //drmModel.TimeStart = OTime;// "2012-01-12";
            //drmModel.TimeEnd = STime;// "2012-11-12";
            ////}
            ////else
            ////{
            ////    drmModel.TimeLimitType = 1;
            ////    drmModel.Days = txtTimeDay.Text.Trim();
            ////}

            //drmModel.AddLabLimit = false; //
            //drmModel.CopyLimit = false;  //复制
            //drmModel.ModifyLabLimit = true;
            //drmModel.PrintLimit = false;  // 打印


            //// 是否打开次数限制
            ////if (Cshu == "")
            ////{ 
            //drmModel.OpenTimeLimit = false;
            ////}
            ////else
            ////{
            ////    drmModel.OpenTimeLimit = true;
            ////drmModel.OpenTime = Cshu;
            ////}
            //// 打开次数
            ////if (drmModel.OpenTimeLimit)
            ////    drmModel.OpenTime = "0";
            ////else
            ////    drmModel.OpenTime = "0";                 
            drmModel = GetDrmModel();
            #endregion
            //StrISBN = StrISBN + "_PDF.xml"; //"E:\\新建\\978-7-80764-574-0_PDF.xml"  加密文件xml 路径

            //获取对应的预处理的xml权限文件
            byte[] bRight = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(FileName), Path.GetFileNameWithoutExtension(FileName) + ".xml"));
            try
            {
                return DRMLib.FileEncrypt.EncryptFile(drmModel, bRight, Path.GetFileName(FileName), fileID, DataLength, UserName, UserName, ProductName, DrmServer);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 设置drm模板
        /// </summary>
        /// <returns></returns>
        private DRMLib.RightModel GetDrmModel()
        {
            DrmRightModelInfo myDrmPolicy = Utility.Utility.GetCurrentDrmPolicy();
            if (null != myDrmPolicy)
            {
                DRMLib.RightModel item = new DRMLib.RightModel();
                item.Type = myDrmPolicy.Type; // 类型 0 服务器加密：1 证书加密
                item.PrintLimit = myDrmPolicy.PrintLimit;
                item.CopyLimit = myDrmPolicy.CopyLimit;
                item.TimeLimit = myDrmPolicy.TimeLimit; // 是否使用时间限制 
                item.ModifyLabLimit = true;
                item.AddLabLimit = false;
                item.OpenTimeLimit = false;//打开次数限制


                item.CopyCharLimit = myDrmPolicy.CopyCharLimit;

                item.CopyCharCount = myDrmPolicy.CopyCharCount;   //复制字数限制
                item.CopyTextLimit = myDrmPolicy.CopyTextLimit;

                item.PrintPageLimit = true;
                item.PrintPageCount = 3;   //打印页数限制

                if (item.TimeLimit && item.Type == 0)
                {
                    item.TimeLimitType = 0;
                    item.TimeStart = DateTime.Now.ToString("yyyy-MM-01");
                    item.TimeEnd = Utility.Utility.GetDrmEndTime(myDrmPolicy.TimeUnit, myDrmPolicy.TimeLength);
                }
                return item;
            }
            else
            {
                return null;
            }
        }

    }
}