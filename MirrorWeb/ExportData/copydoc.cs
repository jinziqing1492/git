using DRMS.BLL;
using DRMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ExportData
{
    public partial class copydoc : Form
    {
        public copydoc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //反序列化数据
            Book bookBll = new Book();
            JournalYear journalBll = new JournalYear();
            JournalYear englishBll = new JournalYear("english");
            //List<BookInfo> book2List = DeSerialize<BookInfo>("F:\\xml\\booknew.xml");
            //if (book2List != null && book2List.Count > 0)
            //{
            //    foreach (BookInfo info in book2List)
            //    {
            //        string srcName = "F:/doc/book/" + info.SYS_FLD_DOI;
            //        string dirName = "F:/newdoc/book/" + info.SYS_FLD_DOI;
            //        //Directory.Move(srcName, dirName);
            //    }
            //}
            //MessageBox.Show("1");
            //List<JournalYearInfo> jounal2List = DeSerialize<JournalYearInfo>("F:\\xml\\journalnew.xml");
            //MessageBox.Show("2");
            //if (jounal2List != null && jounal2List.Count > 0)
            //{
            //    foreach (JournalYearInfo info in jounal2List)
            //    {
            //        string srcName = "F:/doc/journal/" + info.SYS_FLD_DOI;
            //        if (!Directory.Exists(srcName))
            //        {
            //            MessageBox.Show(srcName);
            //            return;
            //        }
            //        string dirName = "F:/newdoc/journal/" + info.SYS_FLD_DOI;
            //        Directory.Move(srcName, dirName);
            //    }
            //}
            List<JournalYearInfo> english2List = DeSerialize<JournalYearInfo>("F:\\xml\\englishnew.xml");
            if (english2List != null && english2List.Count > 0)
            {
                foreach (JournalYearInfo info in english2List)
                {
                    string srcName = "F:/doc/journal/" + info.SYS_FLD_DOI;
                    if (Directory.Exists(srcName))
                    {
                        string dirName = "F:/newdoc/journal/" + info.SYS_FLD_DOI;
                        Directory.Move(srcName, dirName);
                    }
                }
            }
            MessageBox.Show("");
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
    }
}
