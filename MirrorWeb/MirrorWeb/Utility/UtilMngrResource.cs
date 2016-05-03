using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRMS.BLL;
using DRMS.Model;
using System.Text;
using System.IO;
using System.Xml;

namespace DRMS.MirrorWeb.Utility
{
    public static class UtilMngrResource
    {
        /// <summary>
        /// 加载基础资源库资源记录的文件信息
        /// </summary>
        /// <param name="dbt"></param>
        /// <param name="resDoi"></param>
        /// <returns></returns>
        public static string ReadFileJson(DataBaseType dbt, string resDoi)
        {
            string resname = "";
            switch (dbt)
            {
                case DataBaseType.BOOKTDATA:
                    Book b = new Book();
                    BookInfo bi = b.GetItem(resDoi);
                    if (bi != null)
                    {
                        resname = bi.Name;
                    }
                    break;
                case DataBaseType.REFERENCEBOOK:
                    ToolBook toolbook = new ToolBook();
                    ToolBookInfo toolbookinfo = toolbook.GetItem(resDoi);
                    if (toolbookinfo != null)
                    {
                        resname = toolbookinfo.Name;
                    }
                    break;
                case DataBaseType.JOURNALARTICLE:
                    JournalArticle jarticle = new JournalArticle();
                    JournalArticleInfo jarticleinfo = jarticle.GetItem(resDoi);
                    if (jarticleinfo != null)
                    {
                        resname = jarticleinfo.Name;
                    }
                    break;
                case DataBaseType.CONFERENCEARTICLE:
                    ConferenceArticle carticle = new ConferenceArticle();
                    ConferenceArticleInfo carticleinfo = carticle.GetItem(resDoi);
                    if (carticleinfo != null)
                    {
                        resname = carticleinfo.Name;
                    }
                    break;
                case DataBaseType.CRITERION:
                    StdData std = new StdData();
                    StdDataInfo stdinfo = std.GetItem(resDoi);
                    if (stdinfo != null)
                    {
                        resname = stdinfo.Name;
                    }
                    break;
                case DataBaseType.YEARBOOK:
                    YearBookYear yarticle = new YearBookYear();
                    YearBookYearInfo yarticleinfo = yarticle.GetItem(resDoi);
                    if (yarticleinfo != null)
                    {
                        resname = yarticleinfo.Name;
                    }
                    break;
                case DataBaseType.MAGAZINEARTICLE:
                    MagazineArticle marticle = new MagazineArticle();
                    MagazineArticleInfo marticleinfo = marticle.GetItem(resDoi);
                    if (marticleinfo != null)
                    {
                        resname = marticleinfo.Title;
                    }
                    break;
                case DataBaseType.NEWSPAPERARTICLE:
                    NewsPaperArticle narticle = new NewsPaperArticle();
                    NewsPaperArticleInfo narticleinfo = narticle.GetItem(resDoi);
                    if (narticleinfo != null)
                    {
                        resname = narticleinfo.Title;
                    }
                    break;
                case DataBaseType.PICDATA:
                    Pic p = new Pic();
                    PicInfo pinfo = p.GetItem(resDoi);
                    if (pinfo != null)
                    {
                        resname = pinfo.Name;
                    }
                    break;
                case DataBaseType.AUDIODATA:
                    Audio audio = new Audio();
                    AudioInfo audioinfo = audio.GetItem(resDoi);
                    if (audioinfo != null)
                    {
                        resname = audioinfo.Name;
                    }
                    break;
                case DataBaseType.VIDEODATA:
                    Video video = new Video();
                    VideoInfo videoinfo = video.GetItem(resDoi);
                    if (videoinfo != null)
                    {
                        resname = videoinfo.Name;
                    }
                    break;
                case DataBaseType.CONTRACT:
                    Contract contract = new Contract();
                    ContractInfo contractinfo = contract.GetItem(resDoi);
                    if (contractinfo != null)
                    {
                        resname = contractinfo.CONTRACTNAME;
                    }
                    break;
                case DataBaseType.AUTHOR:
                    Author author = new Author();
                    AuthorInfo authorinfo = author.GetItem(resDoi);
                    if (authorinfo != null)
                    {
                        resname = authorinfo.Name;
                    }
                    break;
                case DataBaseType.LOGICALDATABASE:
                    LogicalDataBase ldb = new LogicalDataBase();
                    LogicalDataBaseInfo ldbi = ldb.GetItem(resDoi);
                    if (ldbi != null)
                    {
                        resname = ldbi.DbName;
                    }
                    break;
                case DataBaseType.ORG:
                    Org org = new Org();
                    OrgInfo orginfo = org.GetItem(resDoi);
                    if (orginfo != null)
                    {
                        resname = orginfo.Name;
                    }
                    break;
                case DataBaseType.ORIGINALDATA:
                    OriginalData originaldata = new OriginalData();
                    OriginalDataInfo originalinfo = originaldata.GetItem(resDoi);
                    if (originalinfo != null)
                    {
                        resname = originalinfo.Name;
                    }
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    ConferencePaper conferencepaper = new ConferencePaper();
                    ConferencePaperInfo conferencepaperinfo = conferencepaper.GetItem(resDoi);
                    if (conferencepaperinfo != null)
                    {
                        resname = conferencepaperinfo.ConferenceName;
                    }
                    break;
                case DataBaseType.JOURNALYEAR:
                    JournalYear journalyear = new JournalYear();
                    JournalYearInfo journalyearinfo = journalyear.GetItem(resDoi);
                    if (journalyearinfo != null)
                    {
                        resname = journalyearinfo.CNAME;
                    }
                    break;
                case DataBaseType.THESIS:
                    Thesis thesis = new Thesis();
                    ThesisInfo thesisinfo = thesis.GetItem(resDoi);
                    if (thesisinfo != null)
                    {
                        resname = thesisinfo.Name;
                    }
                    break;
                case DataBaseType.MAGAZINEYEAR:
                    MagazineYear magazine = new MagazineYear();
                    MagazineYearInfo magazineinfo = magazine.GetItem(resDoi);
                    if (magazineinfo != null)
                    {
                        resname = magazineinfo.CNAME;
                    }
                    break;
                case DataBaseType.NEWSPAPERYEAR:
                    NewsPaperYear newspaper = new NewsPaperYear();
                    NewsPaperYearInfo newspaperinfo = newspaper.GetItem(resDoi);
                    if (newspaperinfo != null)
                    {
                        resname = newspaperinfo.CNAME;
                    }
                    break;
                default:
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            //主文件
            string mainfpath = FileManagementUtility.GetFilePathByResDoi(dbt, resDoi);
            if (!String.IsNullOrEmpty(mainfpath) && File.Exists(mainfpath))
            {
                FileInfo fiMainf = new FileInfo(mainfpath);
                string mainfSize = Utility.FormatSize(fiMainf.Length);
                sb.Append("\"mainf\":\"['" + resname + fiMainf.Extension + "','" + mainfSize + "']\",");
            }
            //封面
            string coverpath = FileManagementUtility.GetCoverPathByResDoi(dbt, resDoi);
            if (!String.IsNullOrEmpty(coverpath) && File.Exists(coverpath))
            {
                FileInfo fiCover = new FileInfo(coverpath);
                string coverSize = Utility.FormatSize(fiCover.Length);
                sb.Append("\"cover\":\"['" + resname + fiCover.Extension + "','" + coverSize + "']\",");
            }
            //附件
            int recordcount = 0;
            Attachment a = new Attachment();
            IList<AttachmentInfo> lstAi = a.GetList("PARENTDOI='" + resDoi + "'", 1, 1000, out recordcount, true);
            if (recordcount > 1000)
                lstAi = a.GetList("", 1, recordcount, out recordcount, true);
            if (lstAi != null && lstAi.Count > 0)
            {
                sb.Append("\"attach\":\"[");
                foreach (AttachmentInfo item in lstAi)
                {
                    string attachpath = FileManagementUtility.GetAttachFilePath(item.SYS_FLD_DOI);
                    if (!String.IsNullOrEmpty(attachpath) && File.Exists(attachpath))
                    {
                        FileInfo fiAttach = new FileInfo(attachpath);
                        string attachSize = Utility.FormatSize(fiAttach.Length);
                        sb.Append("'" + item.Name + "','" + item.SYS_FLD_DOI + "','" + attachSize + "',");
                    }
                }
                if (sb.ToString().EndsWith(","))
                    sb.Remove(sb.Length - 1, 1);
                sb.Append("]\"");
            }
            if (sb.ToString().EndsWith(","))
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        public static string GetResViewPageSite(DataBaseType dbt)
        {
            string pageSite = "";
            switch (dbt)
            {
                case DataBaseType.BOOKTDATA:
                    pageSite = "../auditadmin/BookView.aspx";
                    break;
                case DataBaseType.CRITERION:
                    pageSite = "../auditadmin/StdDataView.aspx";
                    break;
                case DataBaseType.CONFERENCEARTICLE:
                    pageSite = "../auditadmin/ConferenceArticleView.aspx";
                    break;
                case DataBaseType.REFERENCEBOOK:
                    pageSite = "../auditadmin/ToolBookView.aspx";
                    break;
                default:
                    break;
            }
            return pageSite;
        }

        public static string GetAttachType(string attachCode)
        {
            string attachType = "";
            switch (attachCode)
            {
                case "1":
                    attachType = "高精度pdf文件";
                    break;
                case "2":
                    attachType = "封面排版文件";
                    break;
                case "3":
                    attachType = "正文排版文件";
                    break;
                case "4":
                    attachType = "高清扫描文件";
                    break;
                case "5":
                    attachType = "课件";
                    break;
                case "6":
                    attachType = "其他";
                    break;
                default:
                    break;
            }
            return attachType;
        }

        /// <summary>
        /// 处理分类信息，用于显示
        /// </summary>
        /// <param name="classfi"></param>
        /// <returns></returns>
        public static string DealWithClassfication(string classfi)
        {
            if (string.IsNullOrEmpty(classfi))
                return "";
            string[] claArr = classfi.Split(';');
            if (claArr == null || claArr.Length <= 0)
                return "";
            StringBuilder sb = new StringBuilder();
            Theme t = new Theme();
            foreach (string item in claArr)
            {
                ThemeInfo ti = t.GetItem(item);
                if (ti == null)
                    continue;
                sb.Append(ti.ThemeName + "(" + ti.SourceCode + ")|");
            }
            if (sb.ToString().EndsWith("|"))
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 从数据库中infoxml内容读取作者信息
        /// </summary>
        /// <param name="xmlinfo"></param>
        /// <param name="touchuan"></param>
        /// <param name="authors"></param>
        public static string GetAuthorGroup(string xmlinfo, string touchuan)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(xmlinfo))
                return "[{}]";
            //当前数据库中info信息的旧的xml
            XmlDocument docInfoXmlData = new XmlDocument();
            docInfoXmlData.LoadXml(xmlinfo);
            XmlNodeList xnAuthorList = docInfoXmlData.SelectNodes("/" + touchuan + "/authorgroup/author");
            if (xnAuthorList == null || xnAuthorList.Count <= 0)
                return "[{}]";
            sb.Append("[");
            //循环读取每个author
            foreach (XmlNode item in xnAuthorList)
            {
                string zzfs = (item as XmlElement).GetAttribute("role");
                //if (zzfs == "责任编辑")// || zzfs == "责任部门" || zzfs == "项目经理" || zzfs == "项目统筹")
                //    continue;
                string personOrorg = "p";
                XmlNode xnAuthorname = (item as XmlElement).SelectSingleNode("personname");
                if (xnAuthorname == null || string.IsNullOrEmpty(xnAuthorname.InnerText))
                    personOrorg = "o";
                XmlNode xnOrgname = (item as XmlElement).SelectSingleNode("orgname");
                if (personOrorg == "o" && xnOrgname == null)
                    continue;
                XmlNode xnAffiliationname = (item as XmlElement).SelectSingleNode("affiliation");
                XmlNode xnEmailname = (item as XmlElement).SelectSingleNode("email");
                XmlNode xnAddressname = (item as XmlElement).SelectSingleNode("address");
                XmlNode xnPersonblurb = (item as XmlElement).SelectSingleNode("personblurb");
                XmlNode xnOrgblurb = (item as XmlElement).SelectSingleNode("orgblurb");
                string pname = xnAuthorname == null ? "" : xnAuthorname.InnerText;
                string orgname = xnOrgname == null ? "" : xnOrgname.InnerText;
                string affiliation = xnAffiliationname == null ? "" : xnAffiliationname.InnerText;
                string email = xnEmailname == null ? "" : xnEmailname.InnerText;
                string address = xnAddressname == null ? "" : xnAddressname.InnerText;
                string pblurb = xnPersonblurb == null ? "" : xnPersonblurb.InnerXml;
                string orgblurb = xnOrgblurb == null ? "" : xnOrgblurb.InnerXml;

                sb.Append("{");
                sb.Append("\"zzfs\":\"" + HttpContext.Current.Server.UrlEncode(zzfs) + "\",");
                sb.Append("\"auttype\":\"" + HttpContext.Current.Server.UrlEncode((personOrorg == "o" ? "1" : "0")) + "\",");
                sb.Append("\"name\":\"" + HttpContext.Current.Server.UrlEncode((personOrorg == "o" ? orgname : pname)) + "\",");
                sb.Append("\"deptname\":\"" + HttpContext.Current.Server.UrlEncode(affiliation) + "\",");
                sb.Append("\"email\":\"" + HttpContext.Current.Server.UrlEncode(email) + "\",");
                sb.Append("\"address\":\"" + HttpContext.Current.Server.UrlEncode(address) + "\",");
                sb.Append("\"blurbEdit\":\"" + HttpContext.Current.Server.UrlEncode((personOrorg == "o" ? DealWithDegistParaToN(orgblurb) : DealWithDegistParaToN(pblurb))) + "\"");
                sb.Append("},");
            }
            if (sb.ToString().EndsWith(","))
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 从数据库中infoxml内容读取关键词信息
        /// </summary>
        /// <param name="xmlinfo"></param>
        /// <param name="touchuan"></param>
        /// <param name="authors"></param>
        public static string GetKeywords(string xmlinfo, string touchuan)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(xmlinfo))
                return "{}";
            //当前数据库中info信息的旧的xml
            XmlDocument docInfoXmlData = new XmlDocument();
            docInfoXmlData.LoadXml(xmlinfo);
            XmlNodeList xnKeywordsList = docInfoXmlData.SelectNodes("/" + touchuan + "/keywordset");
            if (xnKeywordsList == null || xnKeywordsList.Count <= 0)
                return "{}";
            sb.Append("{");
            //循环读取每个关键词
            foreach (XmlNode item in xnKeywordsList)
            {
                string lang = (item as XmlElement).GetAttribute("lang");
                XmlNodeList xnKeywordList = (item as XmlElement).SelectNodes("keyword");
                if (xnKeywordList == null || xnKeywordList.Count <= 0)
                    continue;
                string keywords = "";
                foreach (XmlNode itemK in xnKeywordList)
                {
                    if (!string.IsNullOrEmpty(itemK == null ? "" : itemK.InnerText))
                        keywords += (itemK == null ? "" : itemK.InnerText) + ";";
                }
                if (string.IsNullOrEmpty(keywords))
                    continue;
                sb.Append("\"" + lang.Replace("-", "") + "\":\"" + HttpContext.Current.Server.UrlEncode(keywords) + "\",");
            }
            if (sb.ToString().EndsWith(","))
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 从数据库中infoxml内容读取内容简介信息
        /// </summary>
        /// <param name="xmlinfo"></param>
        /// <param name="touchuan"></param>
        /// <param name="authors"></param>
        public static string GetDegist(string xmlinfo, string touchuan)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(xmlinfo))
                return "{}";
            //当前数据库中info信息的旧的xml
            XmlDocument docInfoXmlData = new XmlDocument();
            docInfoXmlData.LoadXml(xmlinfo);
            XmlNodeList xnDegistsList = docInfoXmlData.SelectNodes("/" + touchuan + "/abstract");
            if (xnDegistsList == null || xnDegistsList.Count <= 0)
                return "{}";
            sb.Append("{");
            //循环读取每个内容简介
            foreach (XmlNode item in xnDegistsList)
            {
                string lang = (item as XmlElement).GetAttribute("lang");
                string degist = item == null ? "" : item.InnerText;
                if (string.IsNullOrEmpty(degist))
                    continue;
                sb.Append("\"" + lang.Replace("-", "") + "\":\"" + HttpContext.Current.Server.UrlEncode(DealWithDegistParaToN(degist)) + "\",");
            }
            if (sb.ToString().EndsWith(","))
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
        //处理简介等内容，将xml字段或文件中的简介内容中的para处理成换行
        public static string DealWithDegistParaToN(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            value = value.TrimEnd('\n');
            value = value.Replace("<![CDATA[", "");
            value = value.Replace("]]>", "");
            value = value.Replace("<para>", "");
            value = value.Replace("</para>", "\n");
            return value;
        }

        //在xml文档中查找xpath对应节点，如果不存在则创建
        public static XmlNode CreateXmlNodeByXpath(XmlDocument xmlDoc, XmlNode xnNode, string xpath, string nsUrl)
        {
            // /book/info/series/SeriesName
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            if (!string.IsNullOrEmpty(nsUrl))
            {
                manager.AddNamespace("myXmlns", nsUrl);

                if (!xpath.StartsWith("/") && !xpath.StartsWith("myXmlns:"))
                    xpath = "myXmlns:" + xpath;
            }
            XmlNode xn = null;
            if (xnNode != null)
            {
                if (string.IsNullOrEmpty(nsUrl))
                    xn = xnNode.SelectSingleNode(xpath);
                else
                    xn = xnNode.SelectSingleNode(xpath.Replace("/", "/myXmlns:"), manager);
            }
            else
            {
                if (string.IsNullOrEmpty(nsUrl))
                    xn = xmlDoc.SelectSingleNode(xpath);
                else
                    xn = xmlDoc.SelectSingleNode(xpath.Replace("/", "/myXmlns:"), manager);
            }
            if (xn != null)
                return xn;
            string parentXpath = xpath; XmlNode xnParent = xnNode;
            if (xpath.Contains('/'))
            {
                parentXpath = xpath.Substring(0, xpath.LastIndexOf('/'));
                xnParent = CreateXmlNodeByXpath(xmlDoc, xnNode, parentXpath, nsUrl);
            }
            try
            {
                if (string.IsNullOrEmpty(nsUrl))
                {
                    if (xpath.Contains('['))
                    {
                        xn = xmlDoc.CreateElement(xpath.Substring(xpath.LastIndexOf('/') + 1, xpath.IndexOf('[') - xpath.LastIndexOf('/') - 1));
                        (xn as XmlElement).SetAttribute("lang", xpath.Substring(xpath.IndexOf('\'') + 1, xpath.LastIndexOf('\'') - xpath.IndexOf('\'')));
                    }
                    else
                        xn = xmlDoc.CreateElement(xpath.Substring(xpath.LastIndexOf('/') + 1));
                }
                else
                    xn = xmlDoc.CreateElement(xpath.Substring(xpath.LastIndexOf('/') + 1), nsUrl);
                xnParent.AppendChild(xn);
            }
            catch { }
            return xn;
        }
        //获取xml路径
        public static string GetBookXmlPath(string doi)
        {
            Book b = new Book();
            BookInfo bi = b.GetItem(doi);
            if (bi == null)
                return "";
            string virpathAtt = "~\\" + Config.GetVirtalPath(bi.SYS_FLD_VIRTUALPATHTAG) + bi.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取TOOLBOOK的XML路径
        public static string GetToolBookXmlPath(string doi)
        {
            ToolBookInfo info = (new ToolBook()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取STDDATA的XML路径
        public static string GetStdXmlPath(string doi)
        {
            StdDataInfo info = (new StdData()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取JOURNAL的XML路径
        public static string GetJournalXmlPath(string doi)
        {
            JournalYearInfo info = (new JournalYear()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取CONFERENCE的XML路径
        public static string GetConferenceXmlPath(string doi)
        {
            ConferencePaperInfo info = (new ConferencePaper()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取CONFERENCE的XML路径
        public static string GetYearBookXmlPath(string doi)
        {
            YearBookYearInfo info = (new YearBookYear()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取MAGAZINE的XML路径
        public static string GetMagazineXmlPath(string doi)
        {
            MagazineYearInfo info = (new MagazineYear()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取NEWSPAPER的XML路径
        public static string GetNewsPaperXmlPath(string doi)
        {
            NewsPaperYearInfo info = (new NewsPaperYear()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取THESIS的XML路径
        public static string GetThesisXmlPath(string doi)
        {
            ThesisInfo info = (new Thesis()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取PIC的XML路径
        public static string GetPicXmlPath(string doi)
        {
            PicInfo info = (new Pic()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取AUDIO的XML路径
        public static string GetAudioXmlPath(string doi)
        {
            AudioInfo info = (new Audio()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        //获取PIC的XML路径
        public static string GetVideoXmlPath(string doi)
        {
            VideoInfo info = (new Video()).GetItem(doi);
            if (info == null)
            {
                return "";
            }
            string virpathAtt = "~\\" + Config.GetVirtalPath(info.SYS_FLD_VIRTUALPATHTAG) + info.SYS_FLD_XMLPATH;
            string filepathAtt = HttpContext.Current.Server.MapPath(virpathAtt);
            return filepathAtt;
        }

        /// <summary>
        /// 按照xpath获取 结果字符串
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetValueFromXmlByPath(string xmlinfo, string xpath)
        {
            if (string.IsNullOrWhiteSpace(xmlinfo))
            {
                return string.Empty;
            }
            //当前数据库中info信息的旧的xml
            XmlDocument docInfoXmlData = new XmlDocument();
            try
            {
                docInfoXmlData.LoadXml(xmlinfo);
            }
            catch
            {

            }
            XmlNodeList mylist = docInfoXmlData.SelectNodes(xpath);
            if (mylist == null || mylist.Count <= 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            //循环读取每个内容简介
            foreach (XmlNode item in mylist)
            {
                if (isFirst)
                {
                    sb.Append(item.InnerText);
                    isFirst = false;
                }
                else
                {
                    sb.Append(item.InnerText + ";");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 根据给定的xml获取作者
        /// </summary>
        /// <param name="xmlinfo"></param>
        /// <returns></returns>
        public static string GetAuthorFromBookInfo(string xmlinfo)
        {
            string author = string.Empty;
            author = GetValueFromXmlByPath(xmlinfo, "info/authorgroup/author[@role='著' or @role='编著' or @role='合著' or @role='原著']/personname");

            if (string.IsNullOrEmpty(author))
            {
                author = GetValueFromXmlByPath(xmlinfo, "info/authorgroup/author[@role='主编' or @role='合编' or @role='汇编']/personname");
            }
            return author;
        }

        /// <summary>
        /// 获取责任编辑
        /// </summary>
        /// <param name="xmlinfo"></param>
        /// <returns></returns>
        public static string GetExeCutiveEditorFromBookInfo(string xmlinfo)
        {

            return GetValueFromXmlByPath(xmlinfo, "info/authorgroup/author[@role='责任编辑' or @role='本期责任编辑']/personname");
        }

        /// <summary>
        /// 获取资源的细览页
        /// </summary>
        /// <param name="dbt"></param>
        /// <returns></returns>
        public static string GetResViewPageSite(DataBaseType dbt, string doi)
        {
            string pageSite = "";
            switch (dbt)
            {
                case DataBaseType.BOOKTDATA:
                case DataBaseType.CRITERION:
                case DataBaseType.REFERENCEBOOK:
                case DataBaseType.THESIS:
                    {
                        pageSite = "/view/BookDetail.aspx?doi=" + doi + "&type=" + dbt.GetHashCode().ToString();
                        break;
                    }
                case DataBaseType.JOURNALARTICLE:
                case DataBaseType.MAGAZINEARTICLE:
                case DataBaseType.NEWSPAPERARTICLE:
                    {
                        pageSite = "/view/JournalDetail.aspx?doi=" + doi + "&type=" + dbt.GetHashCode().ToString();
                        break;
                    }
                case DataBaseType.CONFERENCEARTICLE:
                    {
                        pageSite = "/view/ConferenceArticleView.aspx?doi=" + doi + "&type=" + dbt.GetHashCode().ToString();
                        break;
                    }
                case DataBaseType.CONFERENCEPAPER:
                    {
                        pageSite = "/view/ConferencePaperDetail.aspx?doi=" + doi + "&type=" + dbt.GetHashCode().ToString();
                        break;
                    }
                case DataBaseType.JOURNAL:
                    {
                        JournalYear englishBll = new JournalYear("english");
                        JournalYearInfo enINfo = englishBll.GetItem(doi);
                        if (enINfo != null)
                        {
                            pageSite = "/view/JournalDetail.aspx?doi=" + doi + "&type=" + DataBaseType.ENGLISHRES.GetHashCode().ToString();
                        }
                        else
                        {
                            pageSite = "/view/JournalDetail.aspx?doi=" + doi + "&type=" + dbt.GetHashCode().ToString();
                        }
                        break;
                    }
                default:
                    break;
            }
            return pageSite;
        }

    }
}
