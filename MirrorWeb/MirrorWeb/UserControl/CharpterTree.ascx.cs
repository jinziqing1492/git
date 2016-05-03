using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;

using DRMS.BLL;
using DRMS.Model;
using Tool = CNKI.BaseFunction;


namespace DRMS.MirrorWeb.UserControl
{
    public partial class CharpterTree : System.Web.UI.UserControl
    {
        public string BookDoi { get; set; }

        protected string Nodes { get; set; }

        public string SelectID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            if (!string.IsNullOrEmpty(BookDoi))
            {
                string sql = "PARENTDOI='" + BookDoi + "'";
                Chapter bll = new Chapter();
                int allCount = 0;
                IList<ChapterInfo> cptList = bll.GetList(sql, 1, 1000, out allCount, false);
                if (allCount > 1000)
                {
                    cptList = bll.GetList(sql, 1, allCount, out allCount, false);
                }
                List<ChapterInfo> list = new List<ChapterInfo>();
                if (cptList == null || cptList.Count == 0)
                {
                    Nodes = "[{name:\"未找到数据\"}]";
                    return;
                }

                //将节下面的节去掉
                foreach (ChapterInfo info in cptList)
                {
                    if (info.SYS_FLD_ISPART != 0)
                    {
                        list.Add(info);
                    }
                    else
                    {
                        ChapterInfo item = cptList.Where(x => x.SYS_FLD_DOI == info.SYS_FLD_PARENTDOI).FirstOrDefault();
                        if (item != null)// && item.SYS_FLD_ISPART != 0
                        {
                            list.Add(info);
                        }
                    }
                }
                //list = cptList.ToList();
                if (list != null && list.Count > 0)
                {
                    //判断SelectID的值是否为空 若不为空 给其赋值为列表中的第一个值
                    if (string.IsNullOrEmpty(SelectID))
                    {
                        SelectID = list[0].SYS_FLD_DOI;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("[");
                    foreach (ChapterInfo info in list)
                    {
                        string fullName = info.Title;
                        string name = Utility.Utility.ClearTitle(info.Title);
                        name = Tool.NormalFunction.GetSubStrOther(name, 30, "...");

                        string ispart = info.SYS_FLD_ISPART.ToString();
                        // name = Utility.Utility.ClearTitle(name);
                        fullName = Utility.Utility.ClearTitle(fullName);

                        sb.Append("{");
                        sb.Append("id:\"" + info.SYS_FLD_DOI.ToUpper() + "\",");
                        sb.Append("pId:\"" + info.SYS_FLD_PARENTDOI.ToUpper() + "\",");
                        if (info.SYS_FLD_PARENTDOI == "0")
                        {
                            string vpath = info.SYS_FLD_VIRTUALPATHTAG;
                            string filename = info.SYS_FLD_FILEPATH;
                            string path = Utility.FileManagementUtility.GetFilePath(vpath, filename);
                            if (File.Exists(path))
                            {
                                sb.Append("isParent:true,");
                            }
                        }
                        sb.Append("name:\"" + name + "\",");
                        sb.Append("fullName:\"" + fullName + "\",");
                        sb.Append("ispart:\"" + ispart + "\",");
                        sb.Append("open:true");
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                    Nodes = sb.ToString();
                }
                else
                {
                    Nodes = "[{name:\"未找到数据\"}]";
                }
            }
        }
    }
}