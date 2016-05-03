using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

using DRMS.Model;

namespace ExportData
{
    public partial class SplitData : Form
    {
        public SplitData()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //将一部分文件分离出来
            string xmlpath = @"H:\英文资源\xml";
            string newxmlpath=@"H:\英文资源\xml";
            string filePath = @"H:\英文资源\Journal";
            string[] dirs = Directory.GetDirectories(filePath);
            List<string> fileList = new List<string>();
            foreach (string s in dirs)
            {
                fileList.Add(Path.GetFileNameWithoutExtension(s));
            }
            //反序列化数据
            List<JournalYearInfo> list = DeSerialize<JournalYearInfo>(xmlpath + "\\JournalYear.xml");
            List<JournalYearInfo> newList = new List<JournalYearInfo>();
            foreach (JournalYearInfo yearinfo in list)
            {
                if (fileList.Contains(yearinfo.SYS_FLD_DOI))
                {
                    newList.Add(yearinfo);
                }
            }
            List<JournalArticleInfo> articlelist = DeSerialize<JournalArticleInfo>(xmlpath + "\\JournalArticle.xml");
            List<JournalArticleInfo> newarticleList = new List<JournalArticleInfo>();
            foreach (JournalArticleInfo articleinfo in articlelist)
            {
                if (fileList.Contains(CNKI.BaseFunction.NormalFunction.ResetRedFlag(articleinfo.ParentDoi)))
                {
                    newarticleList.Add(articleinfo);
                }
            }
            List<PicInfo> piclist = DeSerialize<PicInfo>(xmlpath + "\\Pic.xml");
            List<PicInfo> picnewlist = new List<PicInfo>();
            foreach (PicInfo picinfo in piclist)
            {
                if (fileList.Contains(CNKI.BaseFunction.NormalFunction.ResetRedFlag(picinfo.ParentDoi)))
                {
                    picnewlist.Add(picinfo);
                }
            }

            //序列化
            Serialize<JournalYearInfo>(newList, newxmlpath + "\\JournalYear.xml");
            Serialize<JournalArticleInfo>(newarticleList, newxmlpath + "\\JournalArticle.xml");
            Serialize<PicInfo>(picnewlist, newxmlpath + "\\Pic.xml");
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
    }
}
