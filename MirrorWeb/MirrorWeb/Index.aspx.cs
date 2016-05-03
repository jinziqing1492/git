using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;

using DRMS.BLL;
using DRMS.Model;
using Tool = CNKI.BaseFunction;
using System.Web.Security;

namespace DRMS.MirrorWeb
{
    public partial class Index : BasePage.BasePage
    {
        protected string Nodes { get; set; }

        protected bool IsDisplayOwner { get; set; }

        private IList<ThemeInfo> ThemeList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        
        private void BindData()
        {
            BindConfig();
            BindTheme();
            BindHotWords();
            BindOwner();
        }

        /// <summary>
        /// 绑定自有资源的隐藏显示
        /// </summary>
        private void BindOwner()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            if (mylist != null && mylist.Count > 0)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    int tableft = 94 * i;
                    string dbname = mylist[i].Attributes["dname"].Value.Replace("库", "");
                    if (dbname == "泛华资源")
                    {
                        IsDisplayOwner = true;
                    }
                }
            }
        }

        private void BindConfig()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            StringBuilder htmlAppender = new StringBuilder();
            if (mylist != null&&mylist.Count>0)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    int tableft = 94 * i;
                    string dbname = mylist[i].Attributes["dname"].Value.Replace("库", "");
                    if (i == 0)
                    {
                        htmlAppender.AppendFormat("<a href='#' field='{0}' class='Actived first' style='left:" + tableft + "px;z-index:" + (100 - i) + "'>{1}</a> ", mylist[i].Attributes["dtype"].Value, dbname);
                    }
                    else
                    {
                        htmlAppender.AppendFormat("<a href='#' field='{0}' style='left:" + tableft + "px;z-index:" + (100 - i) + "'>{1}</a> ", mylist[i].Attributes["dtype"].Value, dbname);
                    }
                }
            }
            //lt_config.Text = htmlAppender.ToString();
        }
        private void BindTheme()
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
                int order = 0;
                sb.Append("[");
                foreach (ThemeInfo info in ThemeList)
                {
                    string id = info.ID;
                    string pID = info.ParentID;
                    string name = info.ThemeName;// Tool.NormalFunction.SubString(info.ThemeName, 12, "...");
                    string url = "/view/DBThemeNav.aspx?classid=" + id;
                    sb.Append("{");
                    sb.Append("id:\"" + id + "\",");
                    sb.Append("pId:\"" + pID + "\",");
                    if (pID != "0")
                    {
                        sb.Append("url:\"" + url + "\",");
                        sb.Append("target:\"_self\",");
                    }
                    sb.Append("name:\"" + name + "\"");
                    sb.Append("},");
                }
                Nodes = sb.ToString().TrimEnd(',') + "]";
            }

        }
        private void BindWords()
        {
            //绑定搜索框下的搜索词
            XmlDocument xd = new XmlDocument();
            string xmlPath = "~/configuration/SearchWord.xml";
            string realPath = Server.MapPath(xmlPath);
            xd.Load(realPath);
            XmlNodeList list = xd.SelectNodes("//Word");
            if (list != null && list.Count > 0)
            {
                string words = "";
                foreach (XmlNode node in list)
                {
                    words += "<a href='/view/DBThemeNav.aspx?txt=" + Server.UrlEncode(node.InnerText) + "'>" + node.InnerText + "</a>|";
                }
                lt_words.Text = words.TrimEnd('|');
            }
 
        }
        private void BindHotWords()
        {
            Log bll = new Log();
            IList<LogInfo> list = bll.GetHotWord(10);
            if (list != null && list.Count > 0)
            {
                string words = "";
                int num = 0;
                foreach (LogInfo info in list)
                {
                    if (info.Name.Length >= 2 && info.Name.Length <= 6 && num < 5)
                    {
                        words += "<a href='/view/DBThemeNav.aspx?txt=" + Server.UrlEncode(info.Name) + "'>" + info.Name + "</a>|";
                        num++;
                    }
                }
                lt_words.Text = words.TrimEnd('|');
            }
        }
        private void SetUser()
        {
            FormsAuthenticationTicket authTick = new FormsAuthenticationTicket(
                                                    1,
                                                    "user",
                                                    DateTime.Now,
                                                    DateTime.Now.AddMinutes(30),
                                                    false,
                                                    "0");
            string encryptedTicket = FormsAuthentication.Encrypt(authTick);
            this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
        }
    }
}