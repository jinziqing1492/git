using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using DRMS.Model;
using CNKI.BaseFunction;
using DRMS.BLL;
using System.IO;
using System.Xml.Serialization;

namespace ImportEnglish
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string wenjianjia = textBox2.Text;
            if (string.IsNullOrEmpty(wenjianjia))
            {
                MessageBox.Show("文件路径不能为空");
                return;
            }

            button1.Enabled = false;
            string sqlWhere = textBox1.Text;
            int recordCount = 0;
            string docPath = "";
            //获取doc路径
            Config cbll = new Config();
            IList<ConfigInfo> cList = cbll.GetList("VIRTUALPATHTAG=1", 1, 1, out recordCount, true);
            if (cList != null)
            {
                docPath = cList[0].RootDir;
            }
            JournalYear bll = new JournalYear("english");
            IList<JournalYearInfo> list = bll.GetList(sqlWhere, 1, 10000, out recordCount, true);
            List<JournalArticleInfo> articleList = null;
            List<PicInfo> picList = null;
            List<JournalInfo> journalList = null;
            if (list != null)
            {
                JournalArticle articleBll = new JournalArticle("english");
                Pic picBll = new Pic();
                Journal journalBll = new Journal("english");
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
                    if (pList != null && pList.Count > 0)
                    {
                        picList.AddRange(pList);
                    }
                    //移动文件
                    string path = textBox2.Text + "/Journal/";
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
                    string path = textBox2.Text + "/xml/JournalYear.xml";
                    Serialize<JournalYearInfo>(list.ToList(), path);
                    path = textBox2.Text + "/xml/JournalArticle.xml";
                    Serialize<JournalArticleInfo>(articleList.ToList(), path);
                    path = textBox2.Text + "/xml/Pic.xml";
                    Serialize<PicInfo>(picList.ToList(), path);
                }
                if (journalList != null && journalList != null)
                {
                    string path = textBox2.Text + "/xml/Journal.xml";
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

        private void button2_Click(object sender, EventArgs e)
        {
            string sourcePath = textBox3.Text;
            string docPath = "";
            int recordCount = 0;
            //获取doc路径
            Config cbll = new Config();
            IList<ConfigInfo> cList = cbll.GetList("VIRTUALPATHTAG=1", 1, 1, out recordCount, true);
            if (cList != null)
            {
                docPath = cList[0].RootDir;
            }
            if (string.IsNullOrEmpty(docPath))
            {
                MessageBox.Show("获取doc路径失败");
                return;
            }

            //加载数据
            string yearPath = sourcePath + "/xml/JournalYear.xml";
            string articlePath = sourcePath + "/xml/JournalArticle.xml";
            string journalInfoPath = sourcePath + "/xml/Journal.xml";
            string picPath = sourcePath + "/xml/Pic.xml";
            if (!File.Exists(yearPath) || !File.Exists(articlePath)||!File.Exists(journalInfoPath)||!File.Exists(picPath))
            {
                MessageBox.Show("找不到XML文件");
                return;
            }
            //复制文件到新路径
            string journalPath = docPath + "/journal";
            string[] dirs = Directory.GetDirectories(sourcePath + "/Journal");
            if (dirs != null && dirs.Length > 0)
            {
                foreach (string dir in dirs)
                {
                    //判断是否存在dir
                    string extPath = docPath + "/" + dir.Substring(dir.LastIndexOf("/") + 1);
                    if (!Directory.Exists(extPath))
                    {
                        try
                        {
                            CopyDirectory(dir, journalPath);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); return; }
                    }
                }
            }
            //反序列化数据
            List<JournalYearInfo> yearList = DeSerialize<JournalYearInfo>(yearPath);
            List<JournalArticleInfo> articleList = DeSerialize<JournalArticleInfo>(articlePath);
            List<JournalInfo> journalList = DeSerialize<JournalInfo>(journalInfoPath);
            List<PicInfo> picList = DeSerialize<PicInfo>(picPath);
            if (yearList != null)
            {
                JournalYear bll = new JournalYear("english");
                foreach (JournalYearInfo info in yearList)
                {
                    //判断是否存在该数据
                    JournalYearInfo yINfo = bll.GetItem(info.SYS_FLD_DOI);
                    if (yINfo == null)
                    {
                        info.Sys_fld_Adddate = DateTime.Now;
                        bll.Add(info);
                    }
                }
            }
            if (articleList != null)
            {
                JournalArticle bll = new JournalArticle("english");
                foreach (JournalArticleInfo info in articleList)
                {
                    JournalArticleInfo yINfo = bll.GetItem(info.SYS_FLD_DOI);
                    if (yINfo == null)
                    {
                        info.ParentDoi = CNKI.BaseFunction.NormalFunction.ResetRedFlag(info.ParentDoi);
                        info.Sys_fld_Adddate = DateTime.Now;
                        bll.Add(info);
                    }
                }
            }
            if (journalList != null)
            {
                Journal bll = new Journal("english");
                foreach (JournalInfo info in journalList)
                {
                    JournalInfo jInfo = bll.GetItem(info.SYS_FLD_DOI);
                    if (jInfo == null)
                    {
                        info.Sys_fld_Adddate = DateTime.Now;
                        bll.Add(info);
                    }
                }
            }
            if (picList != null)
            {
                Pic bll = new Pic();
                foreach (PicInfo info in picList)
                {
                    PicInfo pINfo = bll.GetItem(info.SYS_FLD_DOI);
                    if (pINfo == null)
                    {
                        info.ParentDoi = CNKI.BaseFunction.NormalFunction.ResetRedFlag(info.ParentDoi);
                        info.Sys_fld_Adddate = DateTime.Now;
                        bll.Add(info);
                    }
                }
            }
            MessageBox.Show("导入成功");
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

        //选择期刊
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    string sourcePath = textBox4.Text;
        //    List<PicInfo> picList = DeSerialize<PicInfo>(sourcePath);
        //    if (picList != null)
        //    {
        //        Pic bll = new Pic();
        //        foreach (PicInfo info in picList)
        //        {
        //            PicInfo yINfo = bll.GetItem(info.SYS_FLD_DOI);
        //            if (yINfo == null)
        //            {
        //                yINfo.ParentDoi = CNKI.BaseFunction.NormalFunction.ResetRedFlag(yINfo.ParentDoi);
        //                yINfo.Sys_fld_Adddate = DateTime.Now;
        //                bll.Add(info);
        //            }
        //        }
        //    }
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    int recordCount = 0;
        //    JournalYear bll = new JournalYear("english");
        //    IList<JournalYearInfo> list = bll.GetList("", 1, 10000, out recordCount, true);
        //    if (list != null)
        //    {
        //        Pic pBll = new Pic();
        //        List<PicInfo> picList = new List<PicInfo>();
        //        foreach (JournalYearInfo info in list)
        //        {
        //            string sqlWhere = "PARENTDOI='" + info.SYS_FLD_DOI + "'";
        //            IList<PicInfo> aList = pBll.GetList(sqlWhere, 1, 100000, out recordCount, true);
        //            if (aList != null)
        //            {
        //                picList.AddRange(aList);
        //            }
        //        }
        //        //将期刊年表和文章表序列化
        //        if (picList != null)
        //        {
        //            string path = textBox5.Text + "/PicList.xml";
        //            Serialize<PicInfo>(picList.ToList(), path);
        //        }
        //    }
        //}
    }
}
