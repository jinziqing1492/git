using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Data;
using System.Collections;
using System.Configuration;

namespace AdminKNReader.request
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TebReadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            context.Response.Cache.SetExpires(DateTime.Now.AddHours(0.25));

            context.Response.ContentType = "image/gif";
            string strPage = context.Request["page"];
            string strBookID = context.Request["b"];
            string strPressID = context.Request["p"];
            string mmpath = context.Request["cc"];

            string strPath = DRMS.MirrorWeb.Utility.FileManagementUtility.GetFilePathByResDoi((DRMS.Model.DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(strPressID), strBookID);
            if (string.IsNullOrWhiteSpace(strPath))
            {
                context.Response.End();
                return;
            }
            knbookLib.ReaderExWrap ReaderExObj = GetReaderEx(strBookID, strPath);

            Int16 strpage = Convert.ToInt16(strPage);
            object lockobj = new object();
            lock (lockobj)
            {
                try
                {
                    int imgWidth = 1050;
                    int imgHeight = 1400;
                    if (HttpContext.Current.Request.Browser.Type.ToUpper() == "IE6")
                    {
                        imgWidth = 623;
                        imgHeight = 880;
                    }

                    byte[] bRet = (byte[])ReaderExObj.GetPageMirror1(strpage, imgWidth, imgHeight, 100);
                    if (bRet != null)
                    {
                        context.Response.Clear();
                        context.Response.OutputStream.Write(bRet, 0, bRet.Length);
                        bRet = null;
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message);
                    WriteLog(DateTime.Now, "bookId:" + strBookID + ";page:" + strpage + " ;errormsg:" + ex.Message);
                }
                finally
                {
                    ReaderExObj.Close();
                }
            }
            context.Response.End();
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logDate"></param>
        /// <param name="result"></param>
        public void WriteLog(DateTime logDate, string result)
        {
            string filePath = HttpContext.Current.Server.MapPath("/");// Application.StartupPath;
            if (!Directory.Exists(filePath + "\\log"))
            {
                Directory.CreateDirectory(filePath + "\\log");
            }
            string logFilePath = filePath + "\\log" + "\\" + logDate.ToString("yyyy-MM-dd") + ".txt";
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine(string.Format("{0}\t{1}", logDate.ToString("yyyy-MM-dd HH:mm:ss"), result));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        private knbookLib.ReaderExWrap GetReaderEx(string BookID, string Path)
        {
            //Hashtable htHandler = null;
            //if (HttpContext.Current.Application["ReaderHandler"] == null)
            //{
            //    htHandler = new Hashtable();
            //    HttpContext.Current.Application["ReaderHandler"] = htHandler;
            //}
            //else
            //{
            //    htHandler = (Hashtable)HttpContext.Current.Application["ReaderHandler"];
            //}

            knbookLib.ReaderExWrap r = null;

            //if (htHandler[BookID] != null)
            //{
            //    r = (knbookLib.ReaderExWrap)htHandler[BookID];
            //}
            //else
            //{
            r = new knbookLib.ReaderExWrap();
            //lock (htHandler.SyncRoot)
            //{
            //    if (htHandler[BookID] == null)
            //    {
            //        htHandler.Add(BookID, r);
            //    }
            //}
            r.Open(Path, null);
            //  }

            return r;
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
