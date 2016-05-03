using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Configuration;
using DRMS.MirrorWeb.Utility;

using DRMS.BLL;
using DRMS.Model;
//using DRMS.Common;
using DRMS;


namespace AdminKNReader
{
    public partial class Reader : System.Web.UI.Page
    {
        protected string mPressID;
        protected string mBookID;
        protected string mReadMode;
        protected string mBoughtType;
        protected string mOrderId;
        protected string mUserName;
        protected string mPath;
        protected string mRightStr;
        string IsDistribute = ConfigurationManager.AppSettings["IsBookDistribute"];

        protected void Page_Load(object sender, EventArgs e)
        {
            mPressID = Request["p"];
            mBookID = Request["b"];

            if (!IsPostBack)
            {
                PageBind();
            }
        }

        private void PageBind()
        {
            string strHeight = Request["h"];
            string strWidth = Request["w"];
            Page.ClientScript.RegisterStartupScript(GetType(), "InitPage", string.Format("InitPage({0},{1});", strHeight, strWidth), true);
            InitRead();
        }


        /// <summary>
        /// 获取真实地址
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        private string GetRealFile(string doi)
        {
            string FilePath = string.Empty;


            if (mPressID == DataBaseType.BOOKTDATA.GetHashCode().ToString())
            {
                Book bookdal = new Book();
                BookInfo item = bookdal.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        //是通过分装系统对接
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }

                    }
                    else
                    {
                        //正常的读取没有drm的数据

                    }
                }
            }
            else if (mPressID == DataBaseType.REFERENCEBOOK.GetHashCode().ToString())
            {
                ToolBook bookdal = new ToolBook();
                ToolBookInfo item = bookdal.GetItem(doi);
                if (item != null)
                {
                    FilePath =FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        //是通过分装系统对接
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }

                    }
                    else
                    {
                        //正常的读取没有drm的数据

                    }
                }
            }
            else if (mPressID == DataBaseType.CRITERION.GetHashCode().ToString())
            {
                //标准
                StdData bookdal = new StdData();
                StdDataInfo item = bookdal.GetItem(doi);
                if (item != null)
                {
                    FilePath =FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        //是通过分装系统对接
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }

                    }
                    else
                    {
                        //正常的读取没有drm的数据

                    }
                }
            }
            else if (mPressID == DataBaseType.CONFERENCEPAPER.GetHashCode().ToString())
            {
                //论文集
                ConferencePaper paper = new ConferencePaper();
                ConferencePaperInfo item = paper.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.CONFERENCEARTICLE.GetHashCode().ToString())
            {
                //会议论文文章
                ConferenceArticle article = new ConferenceArticle();
                ConferenceArticleInfo item = article.GetItem(doi);
                if (item != null)
                {
                    FilePath =FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.BOOKCHAPTER.GetHashCode().ToString() || mPressID == DataBaseType.STDDATACHAPTER.GetHashCode().ToString())
            {
                //章节
                Chapter bll = new Chapter();
                ChapterInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.THESIS.GetHashCode().ToString())
            {
                //学位论文
                Thesis bll = new Thesis();
                ThesisInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.NEWSPAPER.GetHashCode().ToString() || mPressID == DataBaseType.NEWSPAPERYEAR.GetHashCode().ToString())
            {
                //报纸年表
                NewsPaperYear bll = new NewsPaperYear();
                NewsPaperYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.MAGAZINE.GetHashCode().ToString() || mPressID == DataBaseType.MAGAZINEYEAR.GetHashCode().ToString())
            {
                //杂志年表
                MagazineYear bll = new MagazineYear();
                MagazineYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.JOURNAL.GetHashCode().ToString() || mPressID == DataBaseType.JOURNALYEAR.GetHashCode().ToString())
            {
                //杂志年表
                JournalYear bll = new JournalYear();
                JournalYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.YEARBOOK.GetHashCode().ToString())
            {
                //杂志年表
                YearBookYear bll = new YearBookYear();
                YearBookYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.CONTRACT.GetHashCode().ToString())
            {
                //合同表
                Contract bll = new Contract();
                ContractInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath =FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    //if (IsDistribute == "1")
                    //{
                    //    using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                    //    {
                    //        mRightStr = sr.ReadToEnd();
                    //    }
                    //}
                    //else
                    //{
                    //    //正常的读取没有drm的数据
                    //}
                }
            }
            else if (mPressID == DataBaseType.ENGLISHRES.GetHashCode().ToString())
            {
                //杂志年表
                JournalYear bll = new JournalYear("english");
                JournalYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.STUDYRES.GetHashCode().ToString())
            {
                //杂志年表
                JournalYear bll = new JournalYear("study");
                JournalYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }
            else if (mPressID == DataBaseType.OWNERRES.GetHashCode().ToString())
            {
                //杂志年表
                JournalYear bll = new JournalYear("owner");
                JournalYearInfo item = bll.GetItem(doi);
                if (item != null)
                {
                    FilePath = FileManagementUtility.GetFilePath(item.SYS_FLD_VIRTUALPATHTAG, item.SYS_FLD_FILEPATH);
                    if (IsDistribute == "1")
                    {
                        using (StreamReader sr = new StreamReader(FilePath.Replace(".pdf", ".xml")))
                        {
                            mRightStr = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        //正常的读取没有drm的数据
                    }
                }
            }

            return FilePath;
        }


        /// <summary>
        /// 初始化阅读器
        /// </summary>
        private void InitRead()
        {
            //判断是否已经购买

            int iCount = 0;
            int iStart = 0;
            int iEnd = 0;

            string strPath = string.Empty;
            string strName = string.Empty;
            //UploadBLL ub = new UploadBLL();
            //ub.GetFilePath(mBookID, Convert.ToInt32(CBCM.Common.DBaseItem.图书资源).ToString(), out strName, out strPath);
            // strPath = Server.MapPath(strPath) + "\\book23-254A_l.pdf";



            strPath = GetRealFile(mBookID);

            mPath = strPath;
            if (!File.Exists(mPath))
            {
                Response.Write("<script>alert('未找到PDF文件!该文件不存在或已删除！');window.parent.close();</script>");
                return;
            }

            knbookLib.ReaderExWrap r = null;
            try
            {
                r = new knbookLib.ReaderExWrap();
                if (IsDistribute == "1")
                {
                    byte[] fileRight = System.Text.Encoding.Default.GetBytes(mRightStr);
                    r.Open(strPath, fileRight);
                }
                else
                {
                    r.Open(strPath, null);
                }
                iCount = r.GetPageCount();
                r.Close();
            }
            catch
            {
                return;
            }


            iStart = 1;
            iEnd = iCount;

            string strPage = Request["pg"];
            Page.ClientScript.RegisterStartupScript(GetType(), "InitPageDiv", string.Format("SetPageDiv({0},{1},'{2}');", iStart, iEnd, strPage), true);
            strPage = null;
        }
    }
}
