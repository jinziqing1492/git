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

namespace ExportData
{
    public partial class delsource : Form
    {
        public delsource()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //删除文件夹
            string path = textBox1.Text;
            if (!string.IsNullOrEmpty(path))
            {
                eachFile(path);
                MessageBox.Show("操作完成");
            }
        }

        private void eachFile(string dirpath)
        {
            string[] dirs = Directory.GetDirectories(dirpath);
            if (dirs != null && dirs.Length > 0)
            {
                foreach (string s in dirs)
                {
                    string[] dirss = Directory.GetDirectories(s);
                    if (dirss != null && dirss.Length > 0)
                    {
                        foreach (string ss in dirss)
                        {
                            if (Path.GetFileNameWithoutExtension(ss).ToLower() == "source" || Path.GetFileNameWithoutExtension(ss).ToLower() == "chapter")
                            {
                                DelPdfAndRar(ss);
                            }
                        }
                    }
                }
            }
        }

        private void DelPdfAndRar(string path)
        {
            string[] files = Directory.GetFiles(path);
            if (files != null && files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Delete(s);
                }
            }
            Directory.Delete(path);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
