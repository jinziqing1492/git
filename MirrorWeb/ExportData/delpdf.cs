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

namespace ExportData
{
    public partial class delpdf : Form
    {
        public delpdf()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //删除掉pdf和rar
            DelPdfAndRar(@"D:\document\泛华导出文件\Journal");
        }
        private void DelPdfAndRar(string path)
        {
            string[] files = Directory.GetFiles(path);
            if (files != null && files.Length > 0)
            {
                foreach (string s in files)
                {
                    if (Path.GetExtension(s)==".pdf"||Path.GetExtension(s)==".rar")
                    {
                        File.Delete(s);
                    }
                }
            }
            string[] dirs = Directory.GetDirectories(path);
            if (dirs != null && dirs.Length > 0)
            {
                foreach (string s in dirs)
                {
                    DelPdfAndRar(s);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
