using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using DRMS.BLL;
using DRMS.Model;
using System.Xml.Serialization;

namespace ExportData
{
    public partial class Form1 : Form
    {
        private string FilePath = @"D:\document\泛华\2015-11-06更新包（2）";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string wenjianjia = FilePath;
            if (string.IsNullOrEmpty(wenjianjia))
            {
                MessageBox.Show("文件路径不能为空");
                return;
            }

            button1.Enabled = false;
            string sqlWhere = "SYS_FLD_ADDDATE>'2015-11-01'";
            int recordCount = 0;
            string docPath = "";
            //获取doc路径
            Config cbll = new Config();
            IList<ConfigInfo> cList = cbll.GetList("VIRTUALPATHTAG=1", 1, 1, out recordCount, true);
            if (cList != null)
            {
                docPath = cList[0].RootDir;
            }
            JournalYear bll = new JournalYear("owner");
            IList<JournalYearInfo> list = bll.GetList(sqlWhere, 1, 10000, out recordCount, true);
            List<JournalArticleInfo> articleList = null;
            List<PicInfo> picList = null;
            List<JournalInfo> journalList = null;
            if (list != null)
            {
                JournalArticle articleBll = new JournalArticle("owner");
                Pic picBll = new Pic();
                Journal journalBll = new Journal("owner");
                journalList = new List<JournalInfo>();
                IList<JournalInfo> jList = journalBll.GetList("", 1, 1000, out recordCount, true);
                if (jList != null && jList.Count > 0)
                {
                    journalList.AddRange(jList);
                }

                articleList = new List<JournalArticleInfo>();
                picList = new List<PicInfo>();
                foreach (JournalYearInfo info in list)
                {
                    sqlWhere = "PARENTDOI='" + info.SYS_FLD_DOI + "'";
                    IList<JournalArticleInfo> aList = articleBll.GetList(sqlWhere, 1, 100000, out recordCount, true);
                    if (aList != null && aList.Count > 0)
                    {
                        articleList.AddRange(aList);
                    }
                    //导出图片
                    IList<PicInfo> pList = picBll.GetList(sqlWhere, 1, 100000, out recordCount, true);
                    if (pList != null && aList.Count > 0)
                    {
                        picList.AddRange(pList);
                    }
                    //移动文件
                    string path = FilePath + "/Journal/";
                    string filePath = docPath + "/" + info.SYS_FLD_FILEPATH;
                    string dirPath = Path.GetDirectoryName(filePath);
                    try
                    {
                        //判断是否存在dirpath
                        if (Directory.Exists(dirPath))
                        {
                            CopyDirectory(dirPath, path);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); button1.Enabled = true; return; }
                }
                //将期刊年表和文章表序列化
                if (articleList != null && picList != null)
                {
                    string path = FilePath + "/xml/JournalYear.xml";
                    Serialize<JournalYearInfo>(list.ToList(), path);
                    path = FilePath + "/xml/JournalArticle.xml";
                    Serialize<JournalArticleInfo>(articleList.ToList(), path);
                    path = FilePath + "/xml/Pic.xml";
                    Serialize<PicInfo>(picList.ToList(), path);
                }
                if (journalList != null && journalList != null)
                {
                    string path = FilePath + "/xml/Journal.xml";
                    Serialize<JournalInfo>(journalList, path);
                }
            }
            button1.Enabled = true;
            MessageBox.Show("导出完成");
        }

        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="srcdir"></param>
        /// <param name="desdir"></param>
        private void CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = desdir + "\\" + folderName;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {

                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;


                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }


                    File.Copy(file, srcfileName);
                }
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
