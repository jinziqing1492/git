using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

using DRMS.BLL;
using DRMS.Model;


namespace DRMS.MirrorWeb.ajax
{
    /// <summary>
    /// ApplyDownLoadHandler 的摘要说明
    /// </summary>
    public class ApplyDownLoadHandler : IHttpHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString.Get("type");//0是附件1是主文件 主文件的时候 doi为编码后的路径
            string doi = context.Request.QueryString.Get("doi");
            string name = context.Request.QueryString.Get("name");
            string bookname = context.Request.QueryString.Get("bookname");
            string username = Utility.Utility.GetUserName();
            if (string.IsNullOrEmpty(username))
            {
                ApplyDownLoadResult resultItem = new ApplyDownLoadResult();
                resultItem.Message = "已经申请,不能重复申请，请耐心等待！";
                resultItem.Result = "0";
                JavaScriptSerializer json = new JavaScriptSerializer();
                context.Response.Write(json.Serialize(resultItem));
            }
            else
            {
                context.Response.Write(AddApplyDownLoad(doi, username, name, CNKI.BaseFunction.StructTrans.TransNum(type),bookname));
            }
            // "Hello World");
        }
        /// <summary>
        /// 从附件表里判断
        /// </summary>
        /// <returns></returns>
        protected string AddApplyDownLoad(string doi, string username,string name,int attachmenttype,string bookName)
        {
            //先判断是否可以申请下载改资源
            DownLoadApply downdal = new DownLoadApply();
            int recordcount = 0;
            ApplyDownLoadResult resultItem = new ApplyDownLoadResult();

            IList<DownLoadApplyInfo> mylist = downdal.GetList(" attachmentid=\"" + doi + "\" and username=\"" + username + "\" and CheckStatus=0 ", 1, 1, out recordcount, false);
            if (mylist == null || mylist.Count == 0)
            {
                //说明该附件没有待审核的情况 可以添加一条记录
                DownLoadApplyInfo item = new DownLoadApplyInfo();
                item.ID = CNKI.BaseFunction.RandomId.Get();
                item.UserName = username;
                item.AttachmentID = doi;//如果是路径那么就是图书的doi
                item.AttachmentType = attachmenttype;
                item.AttachmentName =name;
                item.ApplyDate = DateTime.Now;
                if (attachmenttype == 0)
                {

                    Attachment attBll = new Attachment();
                    AttachmentInfo attInfo = attBll.GetItem(doi);
                    if (attInfo != null)
                    {
                        item.Description = bookName + " 的 " + attInfo.Type + " " + attInfo.Name;
                    }

                }
                else
                {
                    item.Description = bookName + " 的pdf主文件 " ;
                }
                if (downdal.Add(item))
                {
                    resultItem.Message = "已经成功申请，请耐心等待，如果审核通过会在首页快捷方式里有提示！";
                    resultItem.Result = "1";
                }
                else
                {
                    resultItem.Message = "申请失败，请稍后再试！";
                    resultItem.Result = "0";
                }
            

            }
            else
            {
                //说明已经申请过了 不能再申请 请耐心等待
                resultItem.Message = "已经申请,不能重复申请，请耐心等待！";
                resultItem.Result = "0";
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(resultItem);
        }
        /// <summary>
        /// 返回到前台的结果类。
        /// </summary>
        [Serializable]
        public class ApplyDownLoadResult
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}