using DRMS.BLL;
using DRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DRMS.MirrorWeb.ajax
{
    /// <summary>
    /// GetNavList 的摘要说明
    /// </summary>
    public class GetNavList : IHttpHandler
    {
        private IList<ThemeInfo> ThemeList { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            string themeList = GetTheme();
            context.Response.Write(themeList);
        }

        private string GetTheme()
        {
            if (ThemeList == null)
            {
                Theme bll = new Theme();
                int allCount = 0;
                ThemeList = bll.GetList("ORDER BY ORDERNUM", 1, 1000, out allCount, true);
                if (allCount > 1000)
                {
                    ThemeList = bll.GetList("ORDER BY ORDERNUM", 1, allCount, out allCount, true);
                }
            }
            if (ThemeList != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (ThemeInfo info in ThemeList)
                {
                    string id = info.ID;
                    string pID = info.ParentID;
                    string name = info.ThemeName;// Tool.NormalFunction.SubString(info.ThemeName, 12, "...");
                    string url = "/view/DBThemeNav.aspx?classid=" + id;
                    sb.Append("{");
                    sb.Append("\"id\":\"" + id + "\",");
                    sb.Append("\"pId\":\"" + pID + "\",");
                    if (pID != "0")
                    {
                        sb.Append("\"url\":\"" + url + "\",");
                        sb.Append("\"target\":\"_self\",");
                    }
                    sb.Append("\"name\":\"" + name + "\"");
                    sb.Append("},");
                }
                return sb.ToString().TrimEnd(',') + "]";
            }
            return "[]";
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