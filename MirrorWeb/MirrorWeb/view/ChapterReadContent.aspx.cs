using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text.RegularExpressions;

using Tool = CNKI.BaseFunction;
using DRMS.BLL;
using DRMS.Model;


namespace DRMS.MirrorWeb.view
{
    public partial class ChapterReadContent : System.Web.UI.Page
    {
        protected string ChapterDoi { get; set; }
        protected string Chapter_URL = "<a  href=\"/view/ChapterRead.aspx?doi={0}&chapterdoi={1}&type={2}\" target=\"_top\">{3}</a>";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doi = Request["doi"];
                string pID = Request["pID"];
                string pName = Request["parentName"];
                BindData(doi, pID, pName);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        //private void BindData(string doi, string pID, string parentName)
        //{
        //    ChapterDoi = doi;

        //    string cptContent = "";
        //    Chapter bll = new Chapter();
        //    ChapterInfo info = bll.GetItem(doi);
        //    string name = "";
        //    string content = "";
        //    if (info != null)
        //    {
        //        name = info.Title;// Tool.NormalFunction.SubString(info.Title, 15, "...");
        //        content =  info.Content;
        //        string pName = "";
        //        string fullpName = "";
        //        if (info.SYS_FLD_PARENTDOI != "0")
        //        {
        //            ChapterDoi = pID;//若其不为父节点，则需要根据其父节点ID进行购买

        //            fullpName = parentName + " -> ";
        //           // pName = Tool.NormalFunction.SubString(parentName, 15, "...") + " -> ";
        //            pName =parentName + " -> ";
        //        }
        //        string fullName = fullpName + info.Title;

        //        if (!string.IsNullOrEmpty(content))
        //        {
        //            cptContent = "<h4 title='" + fullName + "'>" + pName + name + "</h4><p>" + content + "</p>";
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(name))
        //            {
        //                cptContent = "<p>该章节没有预览信息!</p>";
        //            }
        //            else
        //            {
        //                cptContent = "<h4 title='" + fullName + "'>" + pName + name + "</h4><p>该章节没有预览信息!</p>";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        cptContent = "<p>该章节没有预览信息!</p>";
        //    }
        //    lt_content.Text = cptContent;
        //}


        /// <summary>
        /// 加载数据  这需要处理以后 需要将xml读取出来显示 为以后xml显示做基础
        /// </summary>
        private void BindData(string doi, string pID, string parentName)
        {
            ChapterDoi = doi;

            string cptContent = "";
            Chapter bll = new Chapter();
            ChapterInfo info = bll.GetItem(doi);
            string name = "";
            string content = "";
            if (info != null)
            {
                name = info.Title;
                content = info.SYS_FLD_PARAXML;// Tool.NormalFunction.SubString(info.Content, 300, "...");

                XmlDocument doc = new XmlDocument();
                //  content = Utility.XmlToHtml.ClearCDATA(content);
                try
                {
                    doc.LoadXml(content);
                    content = Utility.XmlToHtml.DealXml(doc, 0);
                }
                catch
                {
                    content = Tool.NormalFunction.SubString(info.Content, 300, "...");
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = "<p>" + content + "</p>";
                    }
                }

                string pName = "";
                if (info.SYS_FLD_ISPART == 0)
                {
                    ChapterDoi = pID;//若其不为父节点，则需要根据其父节点ID进行购买

                    pName = parentName;
                }
                else
                {
                    //if (info.SYS_FLD_ISPART == 1)
                    //{
                    //    BindOpt(doi);
                    //    BindPrice();
                    //}
                }

                if (!string.IsNullOrEmpty(content))
                {
                    string source = pName;// == "" ? "" : "<span>" + "<font color='gray'>来源</font>：" + string.Format(Chapter_URL,info.ParentDoi,info.SYS_FLD_PARENTDOI,info.doctype,pName) + "</span>";
                    if (!string.IsNullOrEmpty(pName))
                    {
                        source = pName == "" ? "" : "<span>" + "<font color='gray'>来源</font>：" + string.Format(Chapter_URL, info.ParentDoi, info.SYS_FLD_PARENTDOI, info.doctype, pName) + "</span>";
                    }
                    cptContent = "<h4>" + name + source + "</h4>" + Regex.Replace(content, "\r\n", "<br />", RegexOptions.IgnoreCase) + "";
                }
                else
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        cptContent = "<p>该章节没有预览信息!</p>";
                    }
                    else
                    {
                        string source = pName; //== "" ? "" : "<h5>" + "<font color='gray'>来源</font>：" + string.Format(Chapter_URL, info.ParentDoi, info.SYS_FLD_PARENTDOI, info.doctype, pName) + "</h5>";
                        if (!string.IsNullOrEmpty(pName))
                        {
                            source = pName == "" ? "" : "<span>" + "<font color='gray'>来源</font>：" + string.Format(Chapter_URL, info.ParentDoi, info.SYS_FLD_PARENTDOI, info.doctype, pName) + "</span>";
                        }
                        cptContent = "<h4>" + name + source + "</h4><p>该章节没有预览信息!</p>";
                    }
                }
            }
            else
            {
                cptContent = "<p>该章节没有预览信息!</p>";
            }
            lt_content.Text = cptContent;
        }
    }
}