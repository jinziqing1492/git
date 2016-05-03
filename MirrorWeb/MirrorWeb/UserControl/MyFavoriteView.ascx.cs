using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using DRMS.BLL;
using DRMS.Model;
using CNKI.BaseFunction;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class MyFavoriteView : System.Web.UI.UserControl
    {
        #region 字段属性
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return this.aspNetPager.PageSize; }
            set { this.aspNetPager.PageSize = value; }
        }

        private int recordCount;
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return this.aspNetPager.PageCount; }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SqlQueryCondition {
            get { return this.hid_sql.Value; }
            set { this.hid_sql.Value = value; }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public DRMS.MirrorWeb.Utility.Utility AltMessageUtility = new DRMS.MirrorWeb.Utility.Utility();
        #endregion

        private FavoriteData bll = new FavoriteData();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetProperty();
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        protected void InitData()
        {
            SetSqlQueryCondition();
            BindBookInfo();
            this.ltlCBookItemcount.Text = this.RecordCount.ToString();
            this.ltlCBookPagecount.Text = aspNetPager.PageCount.ToString();
            this.ltlCBookCurrentpage.Text = aspNetPager.CurrentPageIndex.ToString();
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        private void SetSqlQueryCondition()
        {
            //筛选该用户的收藏
            SqlQueryCondition = "OPERATOR='" + HttpContext.Current.User.Identity.Name + "' ORDER BY OPERATORDATE DESC";
        }

        /// <summary>
        /// 获取参数，设置属性
        /// </summary>
        private void SetProperty()
        {
            this.PageNo = StructTrans.TransNum(NormalFunction.GetQueryString("page", ""));
        }

        /// <summary>
        /// 绑定图书数据
        /// </summary>
        private void BindBookInfo()
        {
            int pageNo = aspNetPager.CurrentPageIndex;

            bool IsAll = true;
            IList<FavoriteDataInfo> mList = bll.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
            if (mList == null || mList.Count <= 0)
            {
                this.repEntryList.Visible = false;
                return;
            }
            aspNetPager.RecordCount = this.RecordCount;
            if (pageNo > this.aspNetPager.PageCount)
            {
                pageNo = this.aspNetPager.PageCount;
            }
            this.repEntryList.Visible = true;
            this.repEntryList.DataSource = mList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        protected string GetResourceName(object resType)
        {
            XmlNodeList mylist = Utility.Utility.getDisplayDbListFromConfig("BaseDbViewList");
            foreach (XmlNode node in mylist)
            {
                if (node.Attributes["dtype"].Value == resType.ToString())
                {
                    return node.Attributes["dname"].Value.Replace("库", "");
                }
            }
            return "";
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected string FormatDate(object dt)
        {
            if (dt != null)
            {
                DateTime newDate = StructTrans.TransDate(dt.ToString());
                if (newDate != DateTime.MinValue)
                {
                    return newDate.ToString("yyyy-MM-dd HH:mm");
                }
            }
            return "";
        }

        /// <summary>
        /// 获取详情页地址
        /// </summary>
        /// <returns></returns>
        protected string GetDetailPage(object resType,object doi)
        {
            string detailPage = "";
            if (resType.ToString() == "1")//图书
            {
                detailPage = "/View/BookDetail.aspx?doi=" + doi;
            }
            else if(resType.ToString()=="4")//期刊
            {
                detailPage = "/View/JournalDetail.aspx?doi=" + doi + "&type" + resType;
            }
            return detailPage;
        }

        /// <summary>
        /// repeater的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string type = e.CommandName;
            string doi = e.CommandArgument.ToString();
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(doi))
            {
                return;
            }
            switch (type)
            {
                case "Delete":
                    {
                        //删除收藏
                        bool result = bll.Delete(doi);
                        if (result == true)
                        {
                            BindBookInfo();
                            DRMS.MirrorWeb.Utility.Utility.AlertMessage("删除成功");
                        }
                        else
                        {
                            DRMS.MirrorWeb.Utility.Utility.AlertMessage("删除失败");
                        }

                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 数据页面跳转
        /// </summary>
        /// <returns></returns>
        protected void GoTo_Click(object sender, ImageClickEventArgs e)
        {
            Match mc = Regex.Match(PageNum.Text, @"^\d*$");
            if (mc.Success)
            {
                int Num;
                int.TryParse(PageNum.Text, out Num);
                if (Num > 0 && Num <= PageCount)
                {
                    ltlCBookCurrentpage.Text = Num.ToString();
                    aspNetPager.CurrentPageIndex = Num;
                    BindBookInfo();
                }
                else
                {
                    Response.Write("<script>alert('查找页面超出范围')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('请输入正确的页码格式')</script>");
            }
        }

        /// <summary>
        /// 数据后一页
        /// </summary>
        /// <returns></returns>
        protected void NextOne_Click(object sender, ImageClickEventArgs e)
        {
            int Present;
            int.TryParse(ltlCBookCurrentpage.Text, out Present);
            Present = Present + 1;
            if (Present <= PageCount)
            {
                ltlCBookCurrentpage.Text = Present.ToString();
                aspNetPager.CurrentPageIndex = Present;
                BindBookInfo();
            }
        }

        /// <summary>
        /// 数据前一页
        /// </summary>
        /// <returns></returns>
        protected void PreviewOne_Click(object sender, ImageClickEventArgs e)
        {
            int Present;
            int.TryParse(ltlCBookCurrentpage.Text, out Present);
            Present = Present - 1;
            if (Present > 0)
            {
                ltlCBookCurrentpage.Text = Present.ToString();
                aspNetPager.CurrentPageIndex = Present;
                BindBookInfo();
            }
        }

        /// <summary>
        /// 数据首页
        /// </summary>
        /// <returns></returns>
        protected void FirstOne_Click(object sender, EventArgs e)
        {
            ltlCBookCurrentpage.Text = "1";
            aspNetPager.CurrentPageIndex = 1;
            BindBookInfo();
        }

        /// <summary>
        /// 数据末页
        /// </summary>
        /// <returns></returns>
        protected void LastOne_Click(object sender, EventArgs e)
        {
            ltlCBookCurrentpage.Text = PageCount.ToString();
            aspNetPager.CurrentPageIndex = PageCount;
            BindBookInfo();
        }
    }
}