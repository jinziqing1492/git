using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.Model;
using DRMS.BLL;

namespace DRMS.MirrorWeb.view
{
    public partial class ChapterRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doi = Request["doi"];
                string mType = Request["type"];
                string chapterdoi = Request["chapterdoi"];

                BindData(doi, mType, chapterdoi);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="type"></param>
        private void BindData(string doi, string mType, string cptdoi)
        {
            ctrl_tree.BookDoi = doi;//设置树控件的属性
            ctrl_tree.SelectID = cptdoi;

            

            //显示该资源的摘要信息
            string abstractText = "";
            string title = "";
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(mType);
            
            if (mType == mydbtype.GetHashCode().ToString())//图书
            {
                Book bll = new Book();
                BookInfo info = bll.GetItem(doi);
                if (info != null)
                {
                    title = info.Name;
                    abstractText = info.Digest;
                    //判断本书是否可以在工作时间看
                    if (info.ReadType == 2)
                    {
                        //判断当前是否在工作日
                        if (Util.IsWorkTime())
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "myScript", "alert('该书工作时间不允许阅读。');history.go(-1);", true);
                            Response.Write("该书工作时间不允许阅读");
                            Response.End();
                        }
                    }
                }
            }
            else if (mType == mydbtype.GetHashCode().ToString())//标准
            {
                StdData bll = new StdData();
                StdDataInfo info = bll.GetItem(doi);
                if (info != null)
                {
                    title = info.Name;
                    abstractText = info.Digest;
                }
            }
            lt_title.Text = "<a href='/View/BookDetail.aspx?doi=" + doi + "'>" + title + "</a>";//绑定资源名称

            //又该参数则证明其是通过高级检索页面进来的需要特殊处理
        }
    }
}