using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

using DRMS.BLL;
using CNKI.BaseFunction;
using DRMS.Model;
using DRMS.MirrorWeb.Utility;


namespace DRMS.MirrorWeb.view
{
    public partial class MyApplyDownloadList : System.Web.UI.Page
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
        public string SqlQueryCondition { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public DRMS.MirrorWeb.Utility.Utility AltMessageUtility = new DRMS.MirrorWeb.Utility.Utility();
        #endregion

        string DownLoad_Format = "<a href=\"{0}\" >{1}</a>";
        string Page_Url = "/view/DownLoadFile.aspx?doi={0}&type={1}";

        private DownLoadApply _downloadApply = new DownLoadApply();

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
            //如果是0标识是要下载的 如果是1标识是全部
            string queryStarus = CNKI.BaseFunction.NormalFunction.GetQueryString("q", "0");
            string username = Utility.Utility.GetUserName();

            StringBuilder str = new StringBuilder();
            if (queryStarus == "0")
            {
                str.Append(" USERNAME=\"" + username + "\" and ISDOWNLOAD=0 and CHECKSTATUS=-1 order by APPLYDATE desc");
            }
            else
            {
                str.Append(" USERNAME=\"" + username + "\" order by applydate desc ");
            }
            if (string.IsNullOrWhiteSpace(this.SqlQueryCondition))
            {
                this.SqlQueryCondition = str.ToString();
            }
            else if (str.Length > 0)
            {
                this.SqlQueryCondition = str.ToString() + " and " + this.SqlQueryCondition;
            }
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
            IList<DownLoadApplyInfo> mList = _downloadApply.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
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
            //处理列表中需要显示的中文内容
            for (int i = 0; i < mList.Count; i++)
            {
               // mList[i].AttachmentName = "";// "<a href=\" BookView.aspx?doi=" + mList[i].SYS_FLD_DOI + "\" target=\"_blank\">" + mList[i].Name + "</a>";
                if (mList[i].CheckStatus == -1 && mList[i].IsDownload == 0)
                {
                    mList[i].OperateStr = string.Format(DownLoad_Format,string.Format(Page_Url,mList[i].ID,DataBaseType.DOWNLOADAPPLAY.GetHashCode()), "下载");
                }
                else
                {
                    if (mList[i].IsDownload == 1)
                    {
                        mList[i].OperateStr = "已下载";
                    }
                    else
                    {
                        mList[i].OperateStr = "无操作";
                    }
                }
                mList[i].CheckStatusStr = GetStatus(mList[i].CheckStatus);

            }
            this.repEntryList.Visible = true;
            this.repEntryList.DataSource = mList;
            this.repEntryList.DataBind();
        }

        /// <summary>
        /// 或取状态
        /// </summary>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        protected string GetStatus(int checkStatus)
        {
            string result = "";
            switch (checkStatus)
            {
                case 0:
                    {
                        result = "待审核";
                    }
                    break;
                case -1:
                    { 
                     result = "审核通过";
                    }
                    break;
                case -2:
                    {
                        result = "审核未通过";
                    }
                    break;
            }
            return result;
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
                case "Edit":
                    {
                        Response.RedirectPermanent("../auditadmin/AddBook.aspx?doi=" + doi);
                    }
                    break;

                case "Delete":
                    {
                        //删除图书
                        bool flag = _downloadApply.Delete(doi);
                        if (flag == true)
                        {
                            BindBookInfo();
                            DRMS.MirrorWeb.Utility.Utility.AlertMessageCloseWindow("删除成功");
                        }
                        else
                        {
                            DRMS.MirrorWeb.Utility.Utility.AlertMessageCloseWindow("删除失败");
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

        /// <summary>
        /// 批量删除图书
        /// </summary>
        /// <returns></returns>
        protected void DelBook_Click(object sender, EventArgs e)
        {

            string batchdel_bookdoi = "";
            //batchdel_bookdoi = Request.Form["hdnCheckedMIDs"];
            batchdel_bookdoi = hdnCheckedMIDs.Value.Trim();
            if (!string.IsNullOrEmpty(batchdel_bookdoi))
            {
                string[] batchDelIDs = batchdel_bookdoi.Split('|');
                bool flag = false;
                if (batchDelIDs != null)
                {
                    for (int i = 0; i < batchDelIDs.Length; i++)
                    {
                        //删除图书
                        flag = _downloadApply.Delete(batchDelIDs[i]);
                        flag = true;
                    }
                    if (flag == true)
                    {
                        InitData();
                        DRMS.MirrorWeb.Utility.Utility.AlertMessageCloseWindow("删除成功！");
                    }
                    else
                    {
                        DRMS.MirrorWeb.Utility.Utility.AlertMessageCloseWindow("删除失败！");
                    }

                }
            }
            else
            {
                DRMS.MirrorWeb.Utility.Utility.AlertMessageCloseWindow("请选择待删除的图书！");
            }
        }
    }
}