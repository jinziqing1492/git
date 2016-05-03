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
using System.Threading;

using DRMS.BLL;
using DRMS.Model;
using CNKI.BaseFunction;

namespace CalculatePicSize
{
    public partial class Form1 : Form
    {
        private Thread thread = null;  //线程

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            button1.Enabled = false;
            thread = new Thread(Calculate);
            thread.Start();
        }

        /// <summary>
        /// 计算软件大小
        /// </summary>
        private void Calculate()
        {

            //获取doc路径
            textBox1.Text = "开始计算" + Environment.NewLine;
            string docPath = "";
            Config cbll = new Config();
            int recordCount = 0;
            IList<ConfigInfo> cList = cbll.GetList("VIRTUALPATHTAG=1", 1, 1, out recordCount, true);
            if (cList != null)
            {
                docPath = cList[0].RootDir;
            }
            if (string.IsNullOrEmpty(docPath))
            {
                MessageBox.Show("没有获取到虚拟路径标示");
            }
            //每页一百条数据进行循环操作
            Pic pbll = new Pic();
            int pageIndex = 1;
            while (true)
            {
                IList<PicInfo> pList = pbll.GetList("", pageIndex, 100, out recordCount, true);
                textBox1.Text += "当前正在操作第" + (pageIndex - 1) * 100 + "到第" + pageIndex * 100 + "条数据  共" + recordCount + "条数据" + Environment.NewLine;
                if (pList == null || pageIndex * 100 > recordCount)
                {
                    break;
                }

                //循环计算图片大小并添加到数据库
                foreach (PicInfo pInfo in pList)
                {
                    //获取图片的地址
                    if (pInfo.SYS_FLD_VIRTUALPATHTAG == "1" && !string.IsNullOrEmpty(pInfo.SYS_FLD_FILEPATH) && pInfo.PicSize == 0)
                    {
                        string fileName = docPath + pInfo.SYS_FLD_FILEPATH;
                        FileInfo fInfo = new FileInfo(fileName);
                        if (!fInfo.Exists)
                        {
                            continue;
                        }
                        long picSize = fInfo.Length;
                        if (picSize > 0)
                        {
                            pInfo.ParentDoi = NormalFunction.ResetRedFlag(pInfo.ParentDoi);
                            pInfo.PicSize = (int)picSize;
                            pbll.Update(pInfo);
                        }
                    }
                }
                pageIndex++;

                //设置进度条
                int provalue = pageIndex * 100 * 100 / recordCount;
                progressBar1.Value = provalue > 100 ? 100 : provalue;
            }
            MessageBox.Show("操作完成");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread.Abort();
            button1.Enabled = true;
        }
    }
}
