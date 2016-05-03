using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DRMS.Model;

namespace DRMS.MirrorWeb.Admin
{
    public partial class LogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.logList.SqlQueryCondition = this.hidQueryCondition.Value;
            if (!IsPostBack)
            {
                BindType();
            }
        }
        /// <summary>
        /// 检索按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlQueryCondition = this.hidQueryCondition.Value;
            this.logList.PageNo = 1;
            this.logList.SqlQueryCondition = sqlQueryCondition;
            this.logList.InitData();
        }
        protected void BindType()
        {
            Type databasetype = typeof(DataBaseType);
            foreach (int myCode in Enum.GetValues(databasetype))
            {
                string strName = EnumDescription.GetFieldText(Enum.Parse(databasetype, myCode.ToString()));//获取名称
                string strVaule = myCode.ToString();//获取值
                ListItem tempitem = new ListItem();
                tempitem.Value = strVaule;
                tempitem.Text = strName;
                selResType.Items.Add(tempitem);//添加到DropDownList控件
            }

            Type logtype = typeof(LogType);
            foreach (int myCode in Enum.GetValues(logtype))
            {
                string strName = EnumDescription.GetFieldText(Enum.Parse(logtype, myCode.ToString()));//获取名称
                string strVaule = myCode.ToString();//获取值
                ListItem tempitem = new ListItem();
                tempitem.Value = strVaule;
                tempitem.Text = strName;
                selLogType.Items.Add(tempitem);//添加到DropDownList控件
            }
        }
    }
}