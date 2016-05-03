using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using DRMS.BLL;
using CNKI.BaseFunction;
using DRMS.Model;
using System.Text;
using DRMS.MirrorWeb.Utility;

namespace DRMS.MirrorWeb.auditadmin
{
    public partial class ResViewList : System.Web.UI.Page
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
        #endregion

        private Mission _mission = new Mission();

        private string _resTypeFlag = "";
        private string _logicID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取逻辑库id
            _logicID = this.hdnLogicID.Value;
            //获取资源类型
            _resTypeFlag = this.hdnResTypeFlag.Value;

            SetProperty();
            if (!IsPostBack)
            {
                this._logicID = this.Request["lid"];
                this.hdnLogicID.Value = _logicID;
                _resTypeFlag = this.Request["rtf"];
                this.hdnResTypeFlag.Value = _resTypeFlag;

                InitData();
            }
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        protected void InitData()
        {
            BindMissionInfo();
            this.ltlCBookItemcount.Text = this.RecordCount.ToString();
            this.ltlCBookPagecount.Text = aspNetPager.PageCount.ToString();
            this.ltlCBookCurrentpage.Text = aspNetPager.CurrentPageIndex.ToString();
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
        private void BindMissionInfo()
        {
            SetSqlQueryCondition();
            //加载表头
            LoadTableHead();
            //加载数据
            LoadTableBody();
        }

        //加载表头
        private void LoadTableHead()
        {
            StringBuilder sbHead = new StringBuilder();
            sbHead.Append("<tr>");
            sbHead.Append("<th>序号</th>");
            DataBaseType dbt = (DataBaseType)StructTrans.TransNum(_resTypeFlag);
            switch (dbt)
            {
                case DataBaseType.BOOKTDATA:
                    sbHead.Append("<th>图书名称</th>");
                    sbHead.Append("<th>ISBN</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>责任编辑</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.CRITERION:
                    sbHead.Append("<th>名称</th>");
                    sbHead.Append("<th>标准号</th>");
                    sbHead.Append("<th>标准类型</th>");
                    sbHead.Append("<th>主编单位</th>");
                    sbHead.Append("<th>发布时间</th>");
                    sbHead.Append("<th>发布部门</th>");
                    break;
                case DataBaseType.REFERENCEBOOK:
                    sbHead.Append("<th>图书名称</th>");
                    sbHead.Append("<th>ISBN</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>责任编辑</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.JOURNAL:
                    sbHead.Append("<th>拼音刊名</th>");
                    sbHead.Append("<th>中文刊名</th>");
                    sbHead.Append("<th>年期</th>");
                    sbHead.Append("<th>期刊类型</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    sbHead.Append("<th>书名</th>");
                    sbHead.Append("<th>ISBN</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>责任编辑</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.YEARBOOK:
                    sbHead.Append("<th>书名</th>");
                    sbHead.Append("<th>ISBN</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>责任编辑</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.MAGAZINE:
                    sbHead.Append("<th>拼音刊名</th>");
                    sbHead.Append("<th>中文刊名</th>");
                    sbHead.Append("<th>年期</th>");
                    sbHead.Append("<th>期刊类型</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.NEWSPAPER:
                    sbHead.Append("<th>拼音刊名</th>");
                    sbHead.Append("<th>中文刊名</th>");
                    sbHead.Append("<th>年期</th>");
                    sbHead.Append("<th>期刊类型</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>出版单位</th>");
                    break;
                case DataBaseType.THESIS:
                    sbHead.Append("<th>中文题名</th>");
                    sbHead.Append("<th>研究方向</th>");
                    sbHead.Append("<th>指导教师</th>");
                    sbHead.Append("<th>发布年限</th>");
                    sbHead.Append("<th>姓名</th>");
                    sbHead.Append("<th>专业</th>");
                    break;
                case DataBaseType.VIDEODATA:
                    sbHead.Append("<th>名称</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>语种</th>");
                    sbHead.Append("<th>视频时长</th>");
                    sbHead.Append("<th>视频大小</th>");
                    break;
                case DataBaseType.AUDIODATA:
                    sbHead.Append("<th>名称</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>语种</th>");
                    sbHead.Append("<th>视频时长</th>");
                    sbHead.Append("<th>视频大小</th>");
                    break;
                case DataBaseType.PICDATA:
                    sbHead.Append("<th>名称</th>");
                    sbHead.Append("<th>作者</th>");
                    sbHead.Append("<th>出版时间</th>");
                    sbHead.Append("<th>语种</th>");
                    sbHead.Append("<th>图片类型</th>");
                    sbHead.Append("<th>视频大小</th>");
                    break;
                default:
                    break;
            }
            sbHead.Append("</tr>");
            this.LiteralTHead.Text = sbHead.ToString();
        }

        //加载数据
        private void LoadTableBody()
        {
            StringBuilder sbBody = new StringBuilder();
            int pageNo = aspNetPager.CurrentPageIndex;
            bool IsAll = true;
            DataBaseType dbt = (DataBaseType)StructTrans.TransNum(_resTypeFlag);
            switch (dbt)
            {
                case DataBaseType.BOOKTDATA:
                    Book b = new Book();
                    IList<BookInfo> mList = b.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (mList == null || mList.Count <= 0)
                    {
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
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + mList[i].Name + "</td>");
                        sbBody.Append("<td>" + mList[i].ISBN + "</td>");
                        sbBody.Append("<td>" + mList[i].Author + "</td>");
                        sbBody.Append("<td>" + mList[i].ExecutiveEditor + "</td>");
                        sbBody.Append("<td>" + mList[i].IssueDate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + mList[i].IssueDep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.CRITERION:
                    StdData sd = new StdData();
                    IList<StdDataInfo> sdList = sd.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (sdList == null || sdList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < sdList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + sdList[i].Name + "</td>");
                        sbBody.Append("<td>" + sdList[i].stdno + "</td>");
                        sbBody.Append("<td>" + sdList[i].stdtype + "</td>");
                        sbBody.Append("<td>" + sdList[i].ProposeDep + "</td>");
                        sbBody.Append("<td>" + sdList[i].Dateissued.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + sdList[i].IssuedDep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.REFERENCEBOOK:
                    ToolBook tb = new ToolBook();
                    IList<ToolBookInfo> tbList = tb.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (tbList == null || tbList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < tbList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + tbList[i].Name + "</td>");
                        sbBody.Append("<td>" + tbList[i].ISBN + "</td>");
                        sbBody.Append("<td>" + tbList[i].Author + "</td>");
                        sbBody.Append("<td>" + tbList[i].ExecutiveEditor + "</td>");
                        sbBody.Append("<td>" + tbList[i].IssueDate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + tbList[i].IssueDep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.JOURNAL:
                    JournalYear jy = new JournalYear();
                    IList<JournalYearInfo> jyList = jy.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (jyList == null || jyList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < jyList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + jyList[i].BASEID + "</td>");
                        sbBody.Append("<td>" + jyList[i].CNAME + "</td>");
                        sbBody.Append("<td>" + jyList[i].Yearissue + "</td>");
                        sbBody.Append("<td>" + jyList[i].Type + "</td>");
                        sbBody.Append("<td>" + jyList[i].Pubdate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + jyList[i].Pubdep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.CONFERENCEPAPER:
                    ConferencePaper cp = new ConferencePaper();
                    IList<ConferencePaperInfo> cpList = cp.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (cpList == null || cpList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < cpList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + cpList[i].Name + "</td>");
                        sbBody.Append("<td>" + cpList[i].ISBN + "</td>");
                        sbBody.Append("<td>" + cpList[i].Author + "</td>");
                        sbBody.Append("<td>" + cpList[i].ExecutiveEditor + "</td>");
                        sbBody.Append("<td>" + cpList[i].IssueDate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + cpList[i].IssueDep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.YEARBOOK:
                    YearBookYear yby = new YearBookYear();
                    IList<YearBookYearInfo> ybyList = yby.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (ybyList == null || ybyList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < ybyList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + ybyList[i].Name + "</td>");
                        sbBody.Append("<td>" + ybyList[i].ISBN + "</td>");
                        sbBody.Append("<td>" + ybyList[i].Author + "</td>");
                        sbBody.Append("<td>" + ybyList[i].ExecutiveEditor + "</td>");
                        sbBody.Append("<td>" + ybyList[i].IssueDate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + ybyList[i].IssueDep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.MAGAZINE:
                    MagazineYear my = new MagazineYear();
                    IList<MagazineYearInfo> myList = my.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (myList == null || myList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < myList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + myList[i].BASEID + "</td>");
                        sbBody.Append("<td>" + myList[i].CNAME + "</td>");
                        sbBody.Append("<td>" + myList[i].Yearissue + "</td>");
                        sbBody.Append("<td>" + myList[i].Type + "</td>");
                        sbBody.Append("<td>" + myList[i].Pubdate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + myList[i].Pubdep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.NEWSPAPER:
                    NewsPaperYear npy = new NewsPaperYear();
                    IList<NewsPaperYearInfo> npyList = npy.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (npyList == null || npyList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < npyList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + npyList[i].BASEID + "</td>");
                        sbBody.Append("<td>" + npyList[i].CNAME + "</td>");
                        sbBody.Append("<td>" + npyList[i].Yearissue + "</td>");
                        sbBody.Append("<td>" + npyList[i].Type + "</td>");
                        sbBody.Append("<td>" + npyList[i].Pubdate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + npyList[i].Pubdep + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.THESIS:
                    Thesis t = new Thesis();
                    IList<ThesisInfo> tList = t.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (tList == null || tList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < tList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + tList[i].Name + "</td>");
                        sbBody.Append("<td>" + tList[i].Instructor + "</td>");
                        sbBody.Append("<td>" + tList[i].ResearchField + "</td>");
                        sbBody.Append("<td>" + tList[i].ReleaseFixedYear + "</td>");
                        sbBody.Append("<td>" + tList[i].Author + "</td>");
                        sbBody.Append("<td>" + tList[i].Major + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.VIDEODATA:
                    Video v = new Video();
                    IList<VideoInfo> vList = v.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (vList == null || vList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < vList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + vList[i].Name + "</td>");
                        sbBody.Append("<td>" + vList[i].Author + "</td>");
                        sbBody.Append("<td>" + vList[i].IssueDate.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + vList[i].Language + "</td>");
                        sbBody.Append("<td>" + vList[i].videoTime + "</td>");
                        sbBody.Append("<td>" + vList[i].VideoSize + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.AUDIODATA:
                    Audio a = new Audio();
                    IList<AudioInfo> aList = a.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (aList == null || aList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < aList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + aList[i].Name + "</td>");
                        sbBody.Append("<td>" + aList[i].Author + "</td>");
                        sbBody.Append("<td>" + aList[i].DateIssued.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + aList[i].Language + "</td>");
                        sbBody.Append("<td>" + aList[i].AudioTime + "</td>");
                        sbBody.Append("<td>" + aList[i].AudioSize + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                case DataBaseType.PICDATA:
                    Pic p = new Pic();
                    IList<PicInfo> pList = p.GetList(SqlQueryCondition, pageNo, aspNetPager.PageSize, out recordCount, IsAll);
                    if (pList == null || pList.Count <= 0)
                    {
                        return;
                    }
                    aspNetPager.RecordCount = this.RecordCount;
                    if (pageNo > this.aspNetPager.PageCount)
                    {
                        pageNo = this.aspNetPager.PageCount;
                    }
                    //处理列表中需要显示的中文内容
                    for (int i = 0; i < pList.Count; i++)
                    {
                        int numCode = (i + 1) + (pageNo - 1) * aspNetPager.PageSize;
                        sbBody.Append("<tr>");
                        sbBody.Append("<td class=\"record-no\">" + numCode + "</td>");
                        sbBody.Append("<td>" + pList[i].Name + "</td>");
                        sbBody.Append("<td>" + pList[i].Author + "</td>");
                        sbBody.Append("<td>" + pList[i].Dateissued.ToString("yyyy-MM-dd") + "</td>");
                        sbBody.Append("<td>" + pList[i].Language + "</td>");
                        sbBody.Append("<td>" + pList[i].PicType + "</td>");
                        sbBody.Append("<td>" + pList[i].PicSize + "</td>");
                        sbBody.Append("</tr>");
                    }
                    break;
                default:
                    break;
            }
            this.LiteralTBody.Text = sbBody.ToString();
        }

        private string _role = Utility.Utility.GetRole();
        private string _username = Utility.Utility.GetUserName();
        /// <summary>
        /// 设置查询条件
        /// </summary>
        private void SetSqlQueryCondition()
        {
            this.SqlQueryCondition = "SYS_FLD_LDBID='?" + this.Request["lid"] + "'";
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
                    BindMissionInfo();
                }
                else
                {
                    Utility.Utility.AlertMessage("查找页面超出范围！");
                }
            }
            else
            {
                Utility.Utility.AlertMessage("请输入正确的页码格式！");
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
                BindMissionInfo();
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
                BindMissionInfo();
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
            BindMissionInfo();
        }

        /// <summary>
        /// 数据末页
        /// </summary>
        /// <returns></returns>
        protected void LastOne_Click(object sender, EventArgs e)
        {
            ltlCBookCurrentpage.Text = PageCount.ToString();
            aspNetPager.CurrentPageIndex = PageCount;
            BindMissionInfo();
        }
    }
}