using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using DRMS.Model;
using DRMS.IDAL;
using CNKI.BaseFunction;
using TPI;

namespace DRMS.TPIServerDAL
{
    public class Contract : IContract
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Contract"];
        #region IArticle 字段
        private const string PARM_CONTRACTNAME = "CONTRACTNAME";
        private const string PARM_CONTRACTNO = "CONTRACTNO";
        private const string PARM_BEGINDATETIME = "BEGINDATETIME";
        private const string PARM_ENDDATETIME = "ENDDATETIME";
        private const string PARM_SIGNDATE = "SIGNDATE";
        private const string PARM_BOOKID = "BOOKID";
        private const string PARM_PARTA = "PARTA";
        private const string PARM_POSTCODE = "POSTCODE";
        private const string PARM_PHONENUM = "PHONENUM";
        private const string PARM_ADDRESS = "ADDRESS";
        private const string PARM_WORKDEP = "WORKDEP";
        private const string PARM_PARTB = "PARTB";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_EXECUTIVEDEPT = "EXECUTIVEDEPT";
        private const string PARM_LICENSEAREA = "LICENSEAREA";
        private const string PARM_LICENSELANGUAGE = "LICENSELANGUAGE";
        private const string PARM_LICENSEMEDIA = "LICENSEMEDIA";
        private const string PARM_SIGNEDRIGHT = "SIGNEDRIGHT";
        private const string PARM_ISSOLE = "ISSOLE";
        private const string PARM_ISHAVEEFILE = "ISHAVEEFILE";
        private const string PARM_PROPORTION = "PROPORTION";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_AUTHOR = "author";
        private const string PARM_AGENT = "agent";
        private const string PARM_FILENO = "fileno";
        private const string PARM_SELECTEDTOPICNUM = "SelectedTopicNum";
        private const string PARM_ISBNSTR = "ISBNSTR";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ContractInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            }
            if (!string.IsNullOrEmpty(item.ISBNStr))
            {
                paramList.Add(PARM_ISBNSTR);
                paramList.Add(item.ISBNStr);
            }
            if (!string.IsNullOrEmpty(item.CONTRACTNAME))
            {
                paramList.Add(PARM_CONTRACTNAME);
                paramList.Add(item.CONTRACTNAME);
            }
            if (!string.IsNullOrEmpty(item.CONTRACTNO))
            {
                paramList.Add(PARM_CONTRACTNO);
                paramList.Add(item.CONTRACTNO);
            }
            if (item.BEGINDATETIME != DateTime.MinValue)
            {
                paramList.Add(PARM_BEGINDATETIME);
                paramList.Add(item.BEGINDATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.ENDDATETIME != DateTime.MinValue)
            {
                paramList.Add(PARM_ENDDATETIME);
                paramList.Add(item.ENDDATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.SIGNDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SIGNDATE);
                paramList.Add(item.SIGNDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.BOOKID))
            {
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BOOKID);
            }
            if (!string.IsNullOrEmpty(item.Parta))
            {
                paramList.Add(PARM_PARTA);
                paramList.Add(item.Parta);
            }
            if (!string.IsNullOrEmpty(item.Postcode))
            {
                paramList.Add(PARM_POSTCODE);
                paramList.Add(item.Postcode);
            }
            if (!string.IsNullOrEmpty(item.Phonenum))
            {
                paramList.Add(PARM_PHONENUM);
                paramList.Add(item.Phonenum);
            }
            if (!string.IsNullOrEmpty(item.Address))
            {
                paramList.Add(PARM_ADDRESS);
                paramList.Add(item.Address);
            }
            if (!string.IsNullOrEmpty(item.Workdep))
            {
                paramList.Add(PARM_WORKDEP);
                paramList.Add(item.Workdep);
            }
            if (!string.IsNullOrEmpty(item.Partb))
            {
                paramList.Add(PARM_PARTB);
                paramList.Add(item.Partb);
            }
            if (!string.IsNullOrEmpty(item.DESCRIPTION))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.DESCRIPTION);
            }
            if (!string.IsNullOrEmpty(item.EXECUTIVEDEPT))
            {
                paramList.Add(PARM_EXECUTIVEDEPT);
                paramList.Add(item.EXECUTIVEDEPT);
            }
            if (!string.IsNullOrEmpty(item.LICENSEAREA))
            {
                paramList.Add(PARM_LICENSEAREA);
                paramList.Add(item.LICENSEAREA);
            }
            if (!string.IsNullOrEmpty(item.LICENSELANGUAGE))
            {
                paramList.Add(PARM_LICENSELANGUAGE);
                paramList.Add(item.LICENSELANGUAGE);
            }
            if (!string.IsNullOrEmpty(item.LICENSEMEDIA))
            {
                paramList.Add(PARM_LICENSEMEDIA);
                paramList.Add(item.LICENSEMEDIA);
            }
            if (!string.IsNullOrEmpty(item.signedRight))
            {
                paramList.Add(PARM_SIGNEDRIGHT);
                paramList.Add(item.signedRight);
            }
            paramList.Add(PARM_ISSOLE);
            paramList.Add(item.Issole.ToString());
            if (!string.IsNullOrEmpty(item.IshaveEfile))
            {
                paramList.Add(PARM_ISHAVEEFILE);
                paramList.Add(item.IshaveEfile);
            }
            if (!string.IsNullOrEmpty(item.Proportion))
            {
                paramList.Add(PARM_PROPORTION);
                paramList.Add(item.Proportion);
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_FILEPATH))
            {
                paramList.Add(PARM_SYS_FLD_FILEPATH);
                paramList.Add(item.SYS_FLD_FILEPATH);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.Author))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.Author);
            }
            if (!string.IsNullOrEmpty(item.Agent))
            {
                paramList.Add(PARM_AGENT);
                paramList.Add(item.Agent);
            }

            if (!string.IsNullOrEmpty(item.FileNO))
            {
                paramList.Add(PARM_FILENO);
                paramList.Add(item.FileNO);
            }

            if (!string.IsNullOrEmpty(item.SelectedTopicNum))
            {
                paramList.Add(PARM_SELECTEDTOPICNUM);
                paramList.Add(item.SelectedTopicNum);
            }

            #endregion
            try
            {
                return TPIHelper.Insert(TABLE_NAME, paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public bool Delete(string doi)
        {
            if (string.IsNullOrWhiteSpace(doi))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 跟新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(ContractInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.CONTRACTNAME))
            {
                paramList.Add(PARM_CONTRACTNAME);
                paramList.Add(item.CONTRACTNAME);
            }
            //if (!string.IsNullOrEmpty(item.ISBNStr))
            //{
            paramList.Add(PARM_ISBNSTR);
            paramList.Add(item.ISBNStr);
            //}
            //if (!string.IsNullOrEmpty(item.CONTRACTNO))
            //{
            paramList.Add(PARM_CONTRACTNO);
            paramList.Add(item.CONTRACTNO);
            //}
            //if (item.BEGINDATETIME != DateTime.MinValue)
            //{
            paramList.Add(PARM_BEGINDATETIME);
            paramList.Add(item.BEGINDATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (item.ENDDATETIME != DateTime.MinValue)
            //{
            paramList.Add(PARM_ENDDATETIME);
            paramList.Add(item.ENDDATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (item.SIGNDATE != DateTime.MinValue)
            //{
            paramList.Add(PARM_SIGNDATE);
            paramList.Add(item.SIGNDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.BOOKID))
            //{
            paramList.Add(PARM_BOOKID);
            paramList.Add(item.BOOKID);
            //}
            //if (!string.IsNullOrEmpty(item.Parta))
            //{
            paramList.Add(PARM_PARTA);
            paramList.Add(item.Parta);
            //}
            //if (!string.IsNullOrEmpty(item.Postcode))
            //{
            paramList.Add(PARM_POSTCODE);
            paramList.Add(item.Postcode);
            //}
            //if (!string.IsNullOrEmpty(item.Phonenum))
            //{
            paramList.Add(PARM_PHONENUM);
            paramList.Add(item.Phonenum);
            //}
            //if (!string.IsNullOrEmpty(item.Address))
            //{
            paramList.Add(PARM_ADDRESS);
            paramList.Add(item.Address);
            //}
            //if (!string.IsNullOrEmpty(item.Workdep))
            //{
            paramList.Add(PARM_WORKDEP);
            paramList.Add(item.Workdep);
            //}
            //if (!string.IsNullOrEmpty(item.Partb))
            //{
            paramList.Add(PARM_PARTB);
            paramList.Add(item.Partb);
            //}
            //if (!string.IsNullOrEmpty(item.DESCRIPTION))
            //{
            paramList.Add(PARM_DESCRIPTION);
            paramList.Add(item.DESCRIPTION);
            //}
            //if (!string.IsNullOrEmpty(item.EXECUTIVEDEPT))
            //{
            paramList.Add(PARM_EXECUTIVEDEPT);
            paramList.Add(item.EXECUTIVEDEPT);
            //}
            //if (!string.IsNullOrEmpty(item.LICENSEAREA))
            //{
            paramList.Add(PARM_LICENSEAREA);
            paramList.Add(item.LICENSEAREA);
            //}
            //if (!string.IsNullOrEmpty(item.LICENSELANGUAGE))
            //{
            paramList.Add(PARM_LICENSELANGUAGE);
            paramList.Add(item.LICENSELANGUAGE);
            //}
            //if (!string.IsNullOrEmpty(item.LICENSEMEDIA))
            //{
            paramList.Add(PARM_LICENSEMEDIA);
            paramList.Add(item.LICENSEMEDIA);
            //}
            //if (!string.IsNullOrEmpty(item.signedRight))
            //{
            paramList.Add(PARM_SIGNEDRIGHT);
            paramList.Add(item.signedRight);
            // }
            paramList.Add(PARM_ISSOLE);
            paramList.Add(item.Issole.ToString());
            //if (!string.IsNullOrEmpty(item.IshaveEfile))
            //{
            paramList.Add(PARM_ISHAVEEFILE);
            paramList.Add(item.IshaveEfile);
            //}
            //if (!string.IsNullOrEmpty(item.Proportion))
            //{
            paramList.Add(PARM_PROPORTION);
            paramList.Add(item.Proportion);
            //}
            //if (!string.IsNullOrEmpty(item.Remark))
            //{
            paramList.Add(PARM_REMARK);
            paramList.Add(item.Remark);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_FILEPATH))
            //{
            paramList.Add(PARM_SYS_FLD_FILEPATH);
            paramList.Add(item.SYS_FLD_FILEPATH);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            //{
            paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
            paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            //}
            //if (!string.IsNullOrEmpty(item.Author))
            //{
            paramList.Add(PARM_AUTHOR);
            paramList.Add(item.Author);
            //}
            //if (!string.IsNullOrEmpty(item.Agent))
            //{
            paramList.Add(PARM_AGENT);
            paramList.Add(item.Agent);
            //}

            //if (!string.IsNullOrEmpty(item.FileNO))
            //{
            paramList.Add(PARM_FILENO);
            paramList.Add(item.FileNO);
            //}

            //if (!string.IsNullOrEmpty(item.SelectedTopicNum))
            //{
            paramList.Add(PARM_SELECTEDTOPICNUM);
            paramList.Add(item.SelectedTopicNum);
            // }


            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_SYS_FLD_DOI + "='" + item.SYS_FLD_DOI + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id获得一条记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public ContractInfo GetItem(string doi)
        {
            if (string.IsNullOrEmpty(doi))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
            RecordSet rs = TPIHelper.GetRecordSet(sqlQuery);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            try
            {
                ContractInfo entry = new ContractInfo();
                #region 判断字段并赋值
                entry.CONTRACTNAME = rs.GetValue(PARM_CONTRACTNAME) ?? "";
                entry.CONTRACTNO = rs.GetValue(PARM_CONTRACTNO) ?? "";
                entry.BEGINDATETIME = StructTrans.TransDate(rs.GetValue(PARM_BEGINDATETIME));
                entry.ENDDATETIME = StructTrans.TransDate(rs.GetValue(PARM_ENDDATETIME));
                entry.SIGNDATE = StructTrans.TransDate(rs.GetValue(PARM_SIGNDATE));
                entry.BOOKID = rs.GetValue(PARM_BOOKID) ?? "";
                entry.Parta = rs.GetValue(PARM_PARTA) ?? "";
                entry.Postcode = rs.GetValue(PARM_POSTCODE) ?? "";
                entry.Phonenum = rs.GetValue(PARM_PHONENUM) ?? "";
                entry.Address = rs.GetValue(PARM_ADDRESS) ?? "";
                entry.Workdep = rs.GetValue(PARM_WORKDEP) ?? "";
                entry.Partb = rs.GetValue(PARM_PARTB) ?? "";
                entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.EXECUTIVEDEPT = rs.GetValue(PARM_EXECUTIVEDEPT) ?? "";
                entry.LICENSEAREA = rs.GetValue(PARM_LICENSEAREA) ?? "";
                entry.LICENSELANGUAGE = rs.GetValue(PARM_LICENSELANGUAGE) ?? "";
                entry.LICENSEMEDIA = rs.GetValue(PARM_LICENSEMEDIA) ?? "";
                entry.signedRight = rs.GetValue(PARM_SIGNEDRIGHT) ?? "";
                entry.Issole = StructTrans.TransNum(rs.GetValue(PARM_ISSOLE));
                entry.IshaveEfile = rs.GetValue(PARM_ISHAVEEFILE) ?? "";
                entry.Proportion = rs.GetValue(PARM_PROPORTION) ?? "";
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.Agent = rs.GetValue(PARM_AGENT) ?? "";
                entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.SelectedTopicNum = rs.GetValue(PARM_SELECTEDTOPICNUM) ?? "";
                entry.FileNO = rs.GetValue(PARM_FILENO) ?? "";
                entry.ISBNStr = rs.GetValue(PARM_ISBNSTR) ?? "";

                #endregion
                return entry;
            }
            catch
            {
                return null;
            }
            finally
            {
                rs.Close();
            }
        }

        /// <summary>
        /// 根据分页信息获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<ContractInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            recordCount = 0;
            RecordSet rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, sqlWhere);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            //  获取总得记录数
            recordCount = rs.GetCount();
            rs.SetHitWordMarkFlag(RED_LEFT, RED_RIGHT);
            //  获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<ContractInfo> entryList = new List<ContractInfo>();
                ContractInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ContractInfo();
                    #region 判断字段并赋值
                    entry.CONTRACTNAME = rs.GetValue(PARM_CONTRACTNAME) ?? "";
                    entry.CONTRACTNO = rs.GetValue(PARM_CONTRACTNO) ?? "";
                    entry.BEGINDATETIME = StructTrans.TransDate(rs.GetValue(PARM_BEGINDATETIME));
                    entry.ENDDATETIME = StructTrans.TransDate(rs.GetValue(PARM_ENDDATETIME));
                    entry.SIGNDATE = StructTrans.TransDate(rs.GetValue(PARM_SIGNDATE));
                    entry.BOOKID = rs.GetValue(PARM_BOOKID) ?? "";
                    entry.Parta = rs.GetValue(PARM_PARTA) ?? "";
                    entry.Postcode = rs.GetValue(PARM_POSTCODE) ?? "";
                    entry.Phonenum = rs.GetValue(PARM_PHONENUM) ?? "";
                    entry.Address = rs.GetValue(PARM_ADDRESS) ?? "";
                    entry.Workdep = rs.GetValue(PARM_WORKDEP) ?? "";
                    entry.Partb = rs.GetValue(PARM_PARTB) ?? "";
                    entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.EXECUTIVEDEPT = rs.GetValue(PARM_EXECUTIVEDEPT) ?? "";
                    entry.LICENSEAREA = rs.GetValue(PARM_LICENSEAREA) ?? "";
                    entry.LICENSELANGUAGE = rs.GetValue(PARM_LICENSELANGUAGE) ?? "";
                    entry.LICENSEMEDIA = rs.GetValue(PARM_LICENSEMEDIA) ?? "";
                    entry.signedRight = rs.GetValue(PARM_SIGNEDRIGHT) ?? "";
                    entry.Issole = StructTrans.TransNum(rs.GetValue(PARM_ISSOLE));
                    entry.IshaveEfile = rs.GetValue(PARM_ISHAVEEFILE) ?? "";
                    entry.Proportion = rs.GetValue(PARM_PROPORTION) ?? "";
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.Agent = rs.GetValue(PARM_AGENT) ?? "";
                    entry.Author = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.ISBNStr = rs.GetValue(PARM_ISBNSTR) ?? "";

                    #endregion
                    entryList.Add(entry);
                    if (!rs.MoveNext())
                    {
                        break;
                    }
                }
                return entryList;
            }
            catch
            {
                return null;
            }
            finally
            {
                rs.Close();
            }
        }

        /// <summary>
        /// 批量删除信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrWhiteSpace(strWhere))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件得到记录总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
