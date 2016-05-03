using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;

using DRMS.BLL;
using DRMS.Model;
using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class ArticleTree : System.Web.UI.UserControl
    {
        /// <summary>
        /// 当前浏览文章所属刊物年期doi，若为词条则是当前词条doi
        /// </summary>        
        public string YearIssueDoi { get; set; }

        /// <summary>
        /// 树控件节点，报纸：两级-->版面（首版、2版...）+文章标题；期刊、杂志两级；年鉴若干
        /// </summary> 
        protected string Nodes { get; set; }

        /// <summary>
        /// 当前浏览文章的doi
        /// </summary>
        public string SelectID { get; set; }

        /// <summary>
        /// 当前浏览文章的数据资源类型
        /// </summary>
        public string dbtybe { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 加载树控件各级节点
        /// </summary>
        private void BindData()
        {
            DataBaseType mydbtype = (DataBaseType)CNKI.BaseFunction.StructTrans.TransNum(dbtybe);
            if (mydbtype == DataBaseType.NEWSPAPER || mydbtype == DataBaseType.NEWSPAPERYEAR)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    NewsPaperArticle bll = new NewsPaperArticle();
                    int allCount = 0;
                    IList<NewsPaperArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的报纸文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }
                    
                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (NewsPaperArticleInfo info in npaList)
                        {
                            string fullName = Tool.NormalFunction.ReplaceLabel(info.Title);
                            string name = Tool.NormalFunction.GetSubStrOther(Tool.NormalFunction.ReplaceLabel(info.Title), 30, "...");
                            //string ispart = info.SYS_FLD_CHECK_STATE.ToString();
                            name = name.Replace("\r", "");
                            sb.Append("{");

                            if (info.SYS_FLD_parentdoi == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_parentdoi.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            //sb.Append("ispart:\"" + ispart + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.JOURNAL || mydbtype == DataBaseType.JOURNALYEAR)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    JournalArticle bll = new JournalArticle();
                    int allCount = 0;
                    IList<JournalArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }
                    
                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (JournalArticleInfo info in npaList)
                        {
                            string fullName = Tool.NormalFunction.ReplaceLabel(info.Name);
                            string name = Tool.NormalFunction.GetSubStrOther(Tool.NormalFunction.ReplaceLabel(info.Name), 30, "...");
                            //将英文引文的引号替换为中文引号
                            fullName = fullName.Replace('"', ' ');
                            name = name.Replace('"', ' ');
                             
                            name = name.Replace("\r", "");
                            sb.Append("{");
                            if (info.SYS_FLD_PARENTDOI == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_PARENTDOI.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("dbtype:\"" + dbtybe + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.MAGAZINE || mydbtype == DataBaseType.MAGAZINEYEAR)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    MagazineArticle bll = new MagazineArticle();
                    int allCount = 0;
                    IList<MagazineArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }
                    
                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (MagazineArticleInfo info in npaList)
                        {
                            string fullName = Tool.NormalFunction.ReplaceLabel(info.Title);
                            string name = Tool.NormalFunction.GetSubStrOther(Tool.NormalFunction.ReplaceLabel(info.Title), 30, "...");
                            //string ispart = info.SYS_FLD_CHECK_STATE.ToString();
                            name = name.Replace("\r", "");
                            sb.Append("{");

                            if (info.SYS_FLD_parentdoi == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_parentdoi.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            //sb.Append("ispart:\"" + ispart + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.YEARBOOK)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    YearBookArticle bll = new YearBookArticle();
                    int allCount = 0;
                    IList<YearBookArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }
                    
                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (YearBookArticleInfo info in npaList)
                        {
                            string fullName = Tool.NormalFunction.ReplaceLabel(info.Title);
                            string name = Tool.NormalFunction.GetSubStrOther(Tool.NormalFunction.ReplaceLabel(info.Title), 30, "...");
                            //string ispart = info.SYS_FLD_CHECK_STATE.ToString();
                            name = name.Replace("\r", "");
                            sb.Append("{");

                            if (info.SYS_FLD_PARENTDOI == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_PARENTDOI.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            //sb.Append("ispart:\"" + ispart + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.ENTRYDATA)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "SYS_FLD_PARENTDOI='" + YearIssueDoi + "'";
                    string psql = "SYS_FLD_DOI='" + YearIssueDoi + "'";
                    Terminology bll = new Terminology();
                    int allCount = 0;
                    IList<TerminologyInfo> tiList = bll.GetList(psql, 1, 1, out allCount, false);//当前词条信息                    
                    if (tiList == null || tiList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }
                    else
                    {
                        IList<TerminologyInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//当前词条的子词条
                        if (allCount > 1000)
                        {
                            npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                        }
                        if (npaList == null || npaList.Count == 0)
                        {
                            //判断SelectID的值是否为空，若为空则其赋值为当前查看的词条
                            if (string.IsNullOrEmpty(SelectID))
                            {
                                SelectID = YearIssueDoi;
                            }
                            StringBuilder sb = new StringBuilder();
                            sb.Append("[");
                            string fullName = Tool.NormalFunction.ReplaceLabel(tiList[0].Name);
                            string name = Tool.NormalFunction.GetSubStrOther(Tool.NormalFunction.ReplaceLabel(tiList[0].Name), 30, "...");
                            name = name.Replace("\r", "");
                            sb.Append("{");
                            sb.Append("id:\"" + tiList[0].SYS_FLD_DOI.ToUpper() + "\",");
                            sb.Append("pId:\"" + tiList[0].SYS_FLD_DOI.ToUpper() + "\",");
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("]");
                            Nodes = sb.ToString();
                            return;
                        }

                        if (npaList != null && npaList.Count > 0)
                        {
                            //判断SelectID的值是否为空，若为空则其赋值为当前查看的词条
                            if (string.IsNullOrEmpty(SelectID))
                            {
                                SelectID = YearIssueDoi;
                            }

                            StringBuilder sb = new StringBuilder();
                            sb.Append("[");
                            string fullName = tiList[0].Name;
                            string name = Tool.NormalFunction.GetSubStrOther(tiList[0].Name, 30, "...");
                            name = name.Replace("\r", "");
                            sb.Append("{");
                            sb.Append("id:\"" + tiList[0].SYS_FLD_DOI.ToUpper() + "\",");
                            sb.Append("pId:\"" + tiList[0].ParentDOI.ToUpper() + "\",");
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                            foreach (TerminologyInfo info in npaList)
                            {
                                 fullName = info.Name;
                                 name = Tool.NormalFunction.GetSubStrOther(info.Name, 30, "...");
                                name = name.Replace("\r", "");
                                sb.Append("{");
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.Sys_fld_Parentdoi.ToUpper() + "\",");
                                //}
                                sb.Append("name:\"" + name + "\",");
                                sb.Append("fullName:\"" + fullName + "\",");
                                sb.Append("open:true");
                                sb.Append("},");
                            }
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append("]");
                            Nodes = sb.ToString();
                        }
                        else
                        {
                            Nodes = "[{name:\"未找到数据\"}]";
                        }
                    }
                }
            }
            else if (mydbtype == DataBaseType.CONFERENCEPAPER || mydbtype == DataBaseType.CONFERENCEARTICLE)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    ConferenceArticle bll = new ConferenceArticle();
                    int allCount = 0;
                    IList<ConferenceArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }

                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (ConferenceArticleInfo info in npaList)
                        {
                            string fullName = Tool.NormalFunction.ReplaceLabel(info.Name);
                            string name = Tool.NormalFunction.GetSubStrOther(Tool.NormalFunction.ReplaceLabel(info.Name), 30, "...");
                            //string ispart = info.SYS_FLD_CHECK_STATE.ToString();
                            name = name.Replace("\r", "");
                            sb.Append("{");

                            //if (info.SYS_FLD_PARENTDOI == "0")
                            //{
                            //    sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                            //    sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                            //    string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                            //    string filename = info.SYS_FLD_FILEPATH;
                            //    string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                            //    if (File.Exists(path))
                            //    {
                            //        sb.Append("isParent:true,");
                            //    }
                            //}
                            //else
                            //{
                            sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                            sb.Append("pId:\"" + info.ParentDoi.ToUpper() + "\",");
                            //}
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            //sb.Append("ispart:\"" + ispart + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.ENGLISHRES)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    JournalArticle bll = new JournalArticle("english");
                    int allCount = 0;
                    IList<JournalArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }

                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (JournalArticleInfo info in npaList)
                        {
                            string fullName = info.Name;
                            string name = Tool.NormalFunction.GetSubStrOther(info.Name, 30, "...");
                            name = name.Replace("\r", "");
                            sb.Append("{");
                            if (info.SYS_FLD_PARENTDOI == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_PARENTDOI.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("dbtype:\"" + dbtybe + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.STUDYRES)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    JournalArticle bll = new JournalArticle("study");
                    int allCount = 0;
                    IList<JournalArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }

                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (JournalArticleInfo info in npaList)
                        {
                            string fullName = info.Name;
                            string name = Tool.NormalFunction.GetSubStrOther(info.Name, 30, "...");
                            name = name.Replace("\r", "");
                            sb.Append("{");
                            if (info.SYS_FLD_PARENTDOI == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_PARENTDOI.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("dbtype:\"" + dbtybe + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }
            else if (mydbtype == DataBaseType.OWNERRES)
            {
                if (!string.IsNullOrEmpty(YearIssueDoi))
                {
                    string sql = "PARENTDOI='" + YearIssueDoi + "'";
                    JournalArticle bll = new JournalArticle("owner");
                    int allCount = 0;
                    IList<JournalArticleInfo> npaList = bll.GetList(sql, 1, 1000, out allCount, false);//父节点下的文章
                    if (allCount > 1000)
                    {
                        npaList = bll.GetList(sql, 1, allCount, out allCount, false);
                    }

                    if (npaList == null || npaList.Count == 0)
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                        return;
                    }

                    if (npaList != null && npaList.Count > 0)
                    {
                        //判断SelectID的值是否为空 若为空 给其赋值为列表中的第一个值
                        if (string.IsNullOrEmpty(SelectID))
                        {
                            SelectID = npaList[0].SYS_FLD_DOI;
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("[");
                        foreach (JournalArticleInfo info in npaList)
                        {
                            string fullName = info.Name;
                            string name = Tool.NormalFunction.GetSubStrOther(info.Name, 30, "...");
                            name = name.Replace("\r", "");
                            sb.Append("{");
                            if (info.SYS_FLD_PARENTDOI == "0")
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + YearIssueDoi.ToUpper() + "\",");
                                string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                                string filename = info.SYS_FLD_FILEPATH;
                                string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);//获取真实路径
                                if (File.Exists(path))
                                {
                                    sb.Append("isParent:true,");
                                }
                            }
                            else
                            {
                                sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                                sb.Append("pId:\"" + info.SYS_FLD_PARENTDOI.ToUpper() + "\",");
                            }
                            sb.Append("name:\"" + name + "\",");
                            sb.Append("dbtype:\"" + dbtybe + "\",");
                            sb.Append("fullName:\"" + fullName + "\",");
                            sb.Append("open:true");
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                        Nodes = sb.ToString();
                    }
                    else
                    {
                        Nodes = "[{name:\"未找到数据\"}]";
                    }
                }
            }

        }
    }
}