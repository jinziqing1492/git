using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DRMS.BLL;
using DRMS.Model;

namespace DRMS.MirrorWeb.ajax
{
    /// <summary>
    /// FavoriteData 的摘要说明
    /// </summary>
    public class FavoriteData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string doi = context.Request["doi"];
            string type = context.Request["type"];
            string name = context.Request["name"];

            string uName = context.User.Identity.Name;

            string result = AddFavorite(doi, type, name,uName);

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string AddFavorite(string doi,string type,string name,string userName)
        {
            DRMS.BLL.FavoriteData bll = new BLL.FavoriteData();
            
            //判断是否已经收藏
            string sql = "DOI='" + doi + "'";
            int recordCount=0;
            IList<FavoriteDataInfo> list = bll.GetList(sql, 1, 10, out recordCount, false);
            if (recordCount != 0)
            {
                return "1";//已经存在，可以直接返回收藏成功!
            }

            FavoriteDataInfo info = new FavoriteDataInfo();
            info.ID = CNKI.BaseFunction.RandomId.Get();
            info.Name = name;
            info.Operator = userName;
            info.OperatorDate = DateTime.Now;
            info.Remark = "收藏资源";
            info.BookType = CNKI.BaseFunction.StructTrans.TransNum(type);
            info.DOI = doi;

            bool result = bll.Add(info);
            if (result)
                return "1";
            else
                return "0";
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