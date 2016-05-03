using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

namespace DRMS.MirrorWeb.view
{
    public partial class UserMoreBaseDBList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getDisplayDbListFromXML();
        }

        /// <summary>
        /// 从配置文件中读取可展示的数据库信息
        /// </summary>
        /// <returns></returns>
        private void getDisplayDbListFromXML()
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            StringBuilder htmlAppender = new StringBuilder();
            if (mylist != null)
            {
                for (int i = 0; i < mylist.Count; i++)
                {

                    if (i == 0)
                        htmlAppender.AppendFormat("<li id='{0}-m' class='TYCchoice_pitchOn'><img src='../images/TYdatabaseIcon.png' /><h5>{1}</h5><p>共有<em>0</em>条数据</p></li>", mylist[i].Attributes["dtype"].Value, mylist[i].Attributes["dname"].Value);
                    else
                        htmlAppender.AppendFormat("<li id='{0}-m' ><img src='../images/TYdatabaseIcon.png' /><h5>{1}</h5><p>共有<em>0</em>条数据</p></li>", mylist[i].Attributes["dtype"].Value, mylist[i].Attributes["dname"].Value);
                }
                //for (int i = 0; i < mylist.Count; i++)
                //{

                //    if (i == 0)
                //        htmlAppender.AppendFormat("<li id='{0}' class='TYCchoice_pitchOn'><img src='../images/TYdatabaseIcon.png' /><h5>{1}</h5></li>", mylist[i].Attributes["dtype"].Value, mylist[i].Attributes["dname"].Value);
                //    else
                //        htmlAppender.AppendFormat("<li id='{0}' ><img src='../images/TYdatabaseIcon.png' /><h5>{1}</h5></li>", mylist[i].Attributes["dtype"].Value, mylist[i].Attributes["dname"].Value);
                //}

            }
            this.ltlbasedatabase.Text = htmlAppender.ToString();

        }
    }
}