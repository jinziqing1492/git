using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DRMS.BLL;
using DRMS.Model;

namespace ImportEnglish
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //加载英文资源列表
            JournalYear bll = new JournalYear();
            string sql = "SYS_FLD_CHECK_STATE = -1";
            int recordCount = 0;
            IList<JournalYearInfo> list = bll.GetList(sql, 1, 1000, out recordCount, false);
            if (list != null && list.Count > 0)
            {
                int order = 0;
                foreach (JournalYearInfo info in list)
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[order].Cells[0].Value = info.SYS_FLD_DOI;
                    dataGridView1.Rows[order].Cells[1].Value = info.BASEID;
                    dataGridView1.Rows[order].Cells[2].Value = info.CNAME;
                    dataGridView1.Rows[order].Cells[3].Value = info.Yearissue;
                    order++;
                }
            }
        }
    }
}
