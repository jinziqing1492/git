using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using DRMS.Model;
using DRMS.BLL;


namespace DRMS.MirrorWeb.view
{
    public partial class DownLoadFile : System.Web.UI.Page
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doi = CNKI.BaseFunction.NormalFunction.GetQueryString("doi", "");
                string type = CNKI.BaseFunction.NormalFunction.GetQueryString("type", "");
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
                case (int)DataBaseType.DOWNLOADAPPLAY:
                    {
                        DownLoadApply downloadappdll = new DownLoadApply();
                        DownLoadApplyInfo downloadItem = downloadappdll.GetItem(doi);
                        if (downloadItem != null)
                        {
                            BindAttData(downloadItem);
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
            //if (string.IsNullOrEmpty(OTime))
            //{
            //    OTime = DateTime.Now.ToString("yyyy-MM-01");// DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            //}
            ////结束时间 时间为空 默认一年时间结束
            //if (string.IsNullOrEmpty(STime))
            //{
            //    STime = DateTime.Now.AddYears(1).ToString("yyyy-MM-01");// Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) + 1) + "-" + DateTime.Now.Month.ToString() + "-01";
            //}
            //Cshu = "3";
            string pdfFile = GetRealFile(FilePath, virtualTag);
            if (!string.IsNullOrEmpty(pdfFile))
            {
                PDFDrm(pdfFile, title);
            }
        }
        /// <summary>
        /// 绑定下载的资源
        /// </summary>
        /// <param name="item"></param>
        public void BindAttData(DownLoadApplyInfo item)
        {
            DownLoadApply downloadbll = new DownLoadApply();

            string strFileName = string.Empty;
            string RealFileName = string.Empty;

            if (item.IsDownload == 1)
            {
                Utility.Utility.AlertMessage("已经下载过了，不能下载，如果还想下载请再提交申请！");
                return;
            }
            if (item.AttachmentType == 1)
            {
                strFileName = item.AttachmentID;
                RealFileName = item.AttachmentName + ".pdf";
            }
            else
            {
                Attachment attbll = new Attachment();
                AttachmentInfo attitem = attbll.GetItem(item.AttachmentID);
                if(attitem!=null)
                {
                    strFileName = GetRealFile(attitem.SYS_FLD_FILEPATH, attitem.SYS_FLD_VIRTUALPATHTAG);
                }
                RealFileName = item.AttachmentName;
            }

            if (File.Exists(strFileName))
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
                    //输出全文流
                    while (DataLength > 0 && Response.IsClientConnected)
                    {
                        int lengthRead = teb_streams.Read(bRet, 0, 10240);//读取的大小 Response.OutputStream.Write(buffer, 0, lengthRead); 
                        Response.OutputStream.Write(bRet, 0, lengthRead);
                        Response.Flush();
                        DataLength = DataLength - lengthRead;
                    }
                    //将生成的权限信息附加在输出流之后
                    // Response.OutputStream.Write(bFileRight, 0, bFileRight.Length);
                    // Response.Flush();

                    //设置下载状态
                    item.IsDownload = 1;
                    downloadbll.Update(item);
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
                Utility.Utility.AlertMessage("文件不存在！");
            }
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
            string RealFileName = title + ".pdf";
          //  byte[] bFileRight = null;
            string username = Utility.Utility.GetUserName();
            //try
            //{
            //    //下载需要和用户名绑定 如果是服务器模式 我可以限制打开次数 如果是证书模式 需要根据用户名限制打开模式
            //    bFileRight = UpdateRigth(strBookID, username, File.ReadAllBytes(strFileName).Length, strFileName);
            //}
            //catch (Exception e)
            //{
            //    //Alert(string.Format("alert('下载失败：{0}！');", e.Message));               
            //}//下载
            if (File.Exists(strFileName))
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
                    //输出全文流
                    while (DataLength > 0 && Response.IsClientConnected)
                    {
                        int lengthRead = teb_streams.Read(bRet, 0, 10240);//读取的大小 Response.OutputStream.Write(buffer, 0, lengthRead); 
                        Response.OutputStream.Write(bRet, 0, lengthRead);
                        Response.Flush();
                        DataLength = DataLength - lengthRead;
                    }
                    //将生成的权限信息附加在输出流之后
                   // Response.OutputStream.Write(bFileRight, 0, bFileRight.Length);
                   // Response.Flush();

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
        /// 获取真实路径文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="virtualTag"></param>
        /// <returns></returns>
        public string GetRealFile(string FilePath, string virtualTag)
        {
            string virtulpath = Config.GetVirtalPath((CNKI.BaseFunction.StructTrans.TransNum(virtualTag) * 100).ToString());

            virtulpath = Config.GetVirtalPath(virtualTag);

            string result = string.Empty;
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
            return result;
        }

    }
}