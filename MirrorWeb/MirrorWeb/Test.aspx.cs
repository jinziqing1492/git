using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using DRMS.BLL;
using DRMS.Model;
using System.Xml.Serialization;

namespace DRMS.MirrorWeb
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JournalYear bll = new JournalYear();
                string sql = "baseid='ea-em' or baseid='a-ef' or baseid='af-am' or baseid='af-o' or baseid='o-em' or baseid='o-am'";
                int recordCount=0;
                bll.GetList(sql, 1, 100, out recordCount, true);

                //BindData();
                //BindData2();
                //DelFanHua();
                
            }
        }

        /// <summary>
        /// 删除泛华的资源
        /// </summary>
        private void DelFanHua()
        {
            int recordCount = 0;
            Journal bll = new Journal();
            string sql = "BASEID='AD' OR BASEID='NASA' OR BASEID='DE' OR BASEID='PB'";
            bll.DeleteByWhere(sql);
            JournalYear yearbll = new JournalYear();
            JournalArticle articleBll = new JournalArticle();
            IList<JournalYearInfo> list = yearbll.GetList(sql, 1, 4, out recordCount, true);
            if (list != null)
            {
                string docPath = Server.MapPath("doc");
                foreach (JournalYearInfo info in list)
                {
                    articleBll.DeleteByWhere("PARENTDOI='" + info.SYS_FLD_DOI + "'");
                    //删除文件
                    string filePath = docPath + "/journal/" + info.SYS_FLD_DOI;
                    Directory.Move(filePath, "I:\\mulu");
                }
            }
        }

        /// <summary>
        /// 将期刊里面的封面，如果没有自己的封面则取第一期的封面
        /// </summary>
        private void UpdateCover()
        {
            Journal bll = new Journal("");
            JournalYear yearBll = new JournalYear("");
            int recordCount = 0;
            IList<JournalInfo> list = bll.GetList("", 1, 100, out recordCount, true);
            if (list != null && list.Count > 0)
            {
                string docPath = Server.MapPath("doc");
                foreach (JournalInfo info in list)
                {
                    string filePath = docPath + info.SYS_FLD_COVERPATH;
                    if (!File.Exists(filePath))
                    {
                        IList<JournalYearInfo> yearList = yearBll.GetList("baseid='" + info.BASEID + "'", 1, 1, out recordCount, true);
                        if (yearList != null && yearList.Count > 0)
                        {
                            info.SYS_FLD_COVERPATH = yearList[0].SYS_FLD_COVERPATH;
                            info.SYS_FLD_VIRTUALPATHTAG = "1";
                            bll.Update(info);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData2()
        {
            Book bookBll = new Book();
            JournalYear journalBll = new JournalYear();
            JournalYear englishBll = new JournalYear("english");
            //判断三种资源，镜像库跟腾云的差别
            //将14.55的数据制作成xml 保存在桌面。之后要用
            int recordCount = 0;
            IList<BookInfo> bookList = bookBll.GetList("", 1, 100000, out recordCount, true);
            List<BookInfo> book2List = DeSerialize<BookInfo>("F:\\xml\\book.xml");
            List<BookInfo> newBookList = new List<BookInfo>();
            foreach (BookInfo info in bookList)
            {
                if (!book2List.Any(x => x.SYS_FLD_DOI == info.SYS_FLD_DOI))
                {
                    newBookList.Add(info);
                }
            }
            if (newBookList != null && newBookList.Count > 0)
            {
                Serialize<BookInfo>(newBookList.ToList(), "F:/xml/booknew.xml");
            }
            IList<JournalYearInfo> yearList = journalBll.GetList("", 1, 1000000, out recordCount, true);
            List<JournalYearInfo> year2List = DeSerialize<JournalYearInfo>("F:\\xml\\journal.xml");
            List<JournalYearInfo> newJournalList = new List<JournalYearInfo>();
            foreach (JournalYearInfo info in yearList)
            {
                if (!year2List.Any(x => x.SYS_FLD_DOI == info.SYS_FLD_DOI))
                {
                    newJournalList.Add(info);
                }
            }
            if (newJournalList != null && newJournalList.Count > 0)
            {
                Serialize<JournalYearInfo>(newJournalList.ToList(), "F:/xml/journalnew.xml");
            }
            IList<JournalYearInfo> englishList = englishBll.GetList("", 1, 100000, out recordCount, true);
            List<JournalYearInfo> english2List = DeSerialize<JournalYearInfo>("F:\\xml\\english.xml");
            List<JournalYearInfo> newEnglishList = new List<JournalYearInfo>();
            foreach (JournalYearInfo info in englishList)
            {
                if (!english2List.Any(x => x.SYS_FLD_DOI == info.SYS_FLD_DOI))
                {
                    newEnglishList.Add(info);
                }
            }
            if (newEnglishList != null && newEnglishList.Count > 0)
            {
                Serialize<JournalYearInfo>(newEnglishList.ToList(), "F:/xml/englishnew.xml");
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            Book bookBll = new Book();
            JournalYear journalBll = new JournalYear();
            JournalYear englishBll = new JournalYear("english");
            //判断三种资源，镜像库跟腾云的差别
            //将14.55的数据制作成xml 保存在桌面。之后要用
            int recordCount=0;
            IList<BookInfo> bookList = bookBll.GetList("", 1, 100000, out recordCount, true);
            if (bookList != null && bookList.Count > 0)
            {
                Serialize<BookInfo>(bookList.ToList(), "F:/xml/book.xml");
            }
            IList<JournalYearInfo> yearList = journalBll.GetList("", 1, 1000000, out recordCount, true);
            if (yearList != null && yearList.Count > 0)
            {
                Serialize<JournalYearInfo>(yearList.ToList(), "F:/xml/journal.xml");
            }
            IList<JournalYearInfo> englishList = englishBll.GetList("", 1, 100000, out recordCount, true);
            if (englishList != null && englishList.Count > 0)
            {
                Serialize<JournalYearInfo>(englishList.ToList(), "F:/xml/english.xml");
            }
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="filePath"></param>
        public static void Serialize<Type1>(List<Type1> o, string filePath)
        {
            try
            {
                //判断有没有文件夹，没有则新建
                string dirPath = Path.GetDirectoryName(filePath);
                dirPath= dirPath.Replace("\\","/");
                if (!File.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                XmlSerializer formatter = new XmlSerializer(typeof(List<Type1>));
                StreamWriter sw = new StreamWriter(filePath, false);
                formatter.Serialize(sw, o);
                sw.Flush();
                sw.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<ODataInfo> DeSerialize<ODataInfo>(string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<ODataInfo>));
                StreamReader sr = new StreamReader(filePath);
                List<ODataInfo> o = (List<ODataInfo>)formatter.Deserialize(sr);
                sr.Close();
                return o;
            }
            catch (Exception)
            {
            }
            return default(List<ODataInfo>);
        }
    }
}