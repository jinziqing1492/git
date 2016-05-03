using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DRMS.MirrorWeb.ajax
{
    /// <summary>
    /// SetUserCache 的摘要说明
    /// </summary>
    public class SetUserCache : IHttpHandler, IReadOnlySessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            //更新缓存
            SingleLogin.SetCache();

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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