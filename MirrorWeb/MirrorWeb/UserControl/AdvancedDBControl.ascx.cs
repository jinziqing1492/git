using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class AdvancedDBControl : System.Web.UI.UserControl
    {
        public string Type { get; set; }

        protected int Counts { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDataBind();
            }
        }

        protected void InitDataBind()
        {
            NameValueCollection col = GetKindsFromConfig(Type);
            this.ltShow.Text = InitAdvString(col);
        }

        /// <summary>
        /// 构造高级检索模块
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private string InitAdvString(NameValueCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            for (int rank = 1; rank <= collection.Count; rank++)
            {
                if (rank == 1)
                {
                    sb.Append(string.Format(@"<tr id='txt_{0}'>
                                            <td width='10%' style='text-align:right;padding-right:25px;'>
                                                <a id='AddRow_A' href='javascript:void(0);'><img src='../images/01.gif' alt='添加条件'/></a>
                                                <a id='MinusRow_A' href='javascript:void(0);'><img src='../images/02.gif' alt='缩减条件'/></a></td>
                                            <td width='89%'>", rank));
                }
                else
                {
                    sb.Append(string.Format(@"<tr id='txt_{0}' style='display:none;'>
                                            <td width='10%' style='text-align:right;'>
                                                <select name='txt_{0}_logical' id='txt_{0}_logical'>
                                                    <option selected='selected' value='AND'>并且</option>
                                                    <option value='OR'>或者</option>
                                                    <option value='NOT'>不含</option>
                                                </select>
                                            </td>
                                            <td width='89%'>", rank));
                }
                sb.Append("<select name='txt_" + rank + "_sel' id='txt_" + rank + "_sel'>");
                string tpl = "<option value='{0}'>{1}</option>";
                string tplSel = "<option value='{0}' selected='selected'>{1}</option>";
                for (int i = 1; i <= collection.Count; i++)
                {
                    if (i == rank)
                    {
                        sb.Append(string.Format(tplSel, collection.GetKey(i - 1), collection.GetValues(i - 1)[0].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format(tpl, collection.GetKey(i - 1), collection.GetValues(i - 1)[0].ToString()));
                    }
                }
                sb.Append("</select>");
                sb.Append(string.Format(@"<input type='text' name='txt_{0}_value1' id='txt_{0}_value1' value='' class='adv_input' >
               
                                <select name='txt_{0}_relation' id='txt_{0}_relation' >
                                    <option selected='selected' value='#CNKI_AND'>并含</option>
                                    <option value='#CNKI_OR'>或含</option>
                                    <option value='#CNKI_NOT'>不含</option>
                                </select>
                                <input type='text' name='txt_{0}_value2' id='txt_{0}_value2' value='' class='adv_input'>
                                 <select name='txt_{0}_special' id='txt_{0}_special' >
                                    <option value='%'>模糊</option>
                                    <option selected='selected' value='='>精确</option>
                                </select> 
                            </td>
                        </tr>", rank));
            }

            sb.Append("</table>");
            return sb.ToString();
        }


        /// <summary>
        /// 从配置文件中获取高级检索参数
        /// </summary>
        /// <returns></returns>
        private NameValueCollection GetKindsFromConfig(string obj)
        {
            NameValueCollection collection = new NameValueCollection();
            string type = (string.IsNullOrEmpty(obj)) ? "1" : obj;

            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("../configuration/FieldList.xml"));
            XmlNodeList mylist = doc.SelectNodes("/item/field[@dtype='" + type + "']");
            if (mylist != null)
            {
                for (int i = 0; i < mylist.Count; i++)
                {
                    collection.Add(mylist[i].Attributes["fname"].Value, mylist[i].Attributes["fdname"].Value);
                }
            }
            Counts = collection.Count;
            return collection;
        }



    }
}