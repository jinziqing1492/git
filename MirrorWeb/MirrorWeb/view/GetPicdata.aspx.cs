using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using DRMS.BLL;
using DRMS.Model;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.view
{
    public partial class GetPicdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            databind();
        }

        private void databind()
        {
            string key = NormalFunction.GetQueryString("key", "");
            string ptype = NormalFunction.GetQueryString("ptype", "1");
            //string virtulpath = NormalFunction.GetQueryString("vpath", "");
            //string picurl = NormalFunction.GetQueryString("path", "");


            if (string.IsNullOrWhiteSpace(key))
            {
                Server.Transfer("/images/grey.gif");
                return;
            }

            Pic picBll = new Pic();
            PicInfo item = picBll.GetItem(key);
            // string ptype = NormalFunction.GetQueryString("ptype", "1");
            string virtulpath = string.Empty;
            string picurl = string.Empty;
            if (item != null)
            {
                picurl = item.SYS_FLD_FILEPATH;
                virtulpath = item.SYS_FLD_VIRTUALPATHTAG;
            }

            if (string.IsNullOrEmpty(virtulpath) && string.IsNullOrEmpty(picurl))
            {
                Server.Transfer("/images/grey.gif");
                return;
            }
            virtulpath = Config.GetVirtalPath(virtulpath);
            if (string.IsNullOrWhiteSpace(virtulpath))
            {
                //需要跳转到没找到的一个图片页面
                Server.Transfer("/images/grey.gif");
                return;
            }
            else
            {
                if (ptype == "0")
                {
                    //缩略图
                    string small_pic = Path.GetFileNameWithoutExtension(picurl) + "_small" + Path.GetExtension(picurl);
                    // picurl = small_pic;
                    if (File.Exists(Server.MapPath("~/" + virtulpath) + "\\" + small_pic))
                    {
                        picurl = small_pic;
                    }
                    else
                    {
                        picurl = Server.MapPath("~/" + virtulpath) + "\\" + picurl;
                    }
                }
                else
                {
                    picurl = Server.MapPath("~/" + virtulpath) + "\\" + picurl;
                }
            }

            if (File.Exists(picurl))
            {
                //图片存在
                Response.WriteFile(picurl);
                Response.End();
                return;
            }
            else
            {
                Server.Transfer("/images/grey.gif");
                return;
            }
        }
    }
}