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
    /// <summary>
    /// 导出任务的列表实体
    /// </summary>
    public class ExportTaskList : IExportTaskList
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["exporttasklist"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_NAME = "NAME";
        private const string PARM_BOOKID = "BOOKID";
        private const string PARM_BOOKNAME = "BOOKNAME";
        private const string PARM_BOOKTYPE = "BOOKTYPE";
        private const string PARM_PRICE = "PRICE";
        private const string PARM_EXPORTTASKID = "EXPORTTASKID";
        private const string PARM_EXPORTTASKTYPE = "EXPORTTASKTYPE";
        private const string PARM_OPERATORDATE = "OPERATORDATE";
        private const string PARM_OPERATOR = "OPERATOR";
        private const string PARM_ORIGINALPRICE = "originalprice";
        private const string PARM_LDBID = "ldbid";


        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加导出任务的资源列表
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>        
        public bool Add(ExportTaskListInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.ID))
            {
                paramList.Add(PARM_ID);
                paramList.Add(item.ID);
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.BookId))
            {
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BookId);
            }
            if (!string.IsNullOrEmpty(item.BookName))
            {
                paramList.Add(PARM_BOOKNAME);
                paramList.Add(item.BookName);
            }
            paramList.Add(PARM_BOOKTYPE);
            paramList.Add(item.BookType.ToString());
            if (!string.IsNullOrEmpty(item.ExportTaskId))
            {
                paramList.Add(PARM_EXPORTTASKID);
                paramList.Add(item.ExportTaskId);
            }
            paramList.Add(PARM_EXPORTTASKTYPE);
            paramList.Add(item.ExportTaskType.ToString());
            if (item.OperatorDate != DateTime.MinValue)
            {
                paramList.Add(PARM_OPERATORDATE);
                paramList.Add(item.OperatorDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                paramList.Add(PARM_OPERATOR);
                paramList.Add(item.Operator);
            }

            if (!string.IsNullOrEmpty(item.LdbId))
            {
                paramList.Add(PARM_LDBID);
                paramList.Add(item.LdbId);
            }
            paramList.Add(PARM_PRICE);
            paramList.Add(item.Price.ToString("F"));
            paramList.Add(PARM_ORIGINALPRICE);
            paramList.Add(item.OriginalPrice.ToString("F"));

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
        /// 删除导出任务资源列表
        /// </summary>
        /// <param name="id">导出任务列表id</param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_ID, id);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 更新导出任务列表
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(ExportTaskListInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.ID))
            {
                paramList.Add(PARM_ID);
                paramList.Add(item.ID);
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.BookId))
            {
                paramList.Add(PARM_BOOKID);
                paramList.Add(item.BookId);
            }
            if (!string.IsNullOrEmpty(item.BookName))
            {
                paramList.Add(PARM_BOOKNAME);
                paramList.Add(item.BookName);
            }
            paramList.Add(PARM_BOOKTYPE);
            paramList.Add(item.BookType.ToString());

            if (!string.IsNullOrEmpty(item.ExportTaskId))
            {
                paramList.Add(PARM_EXPORTTASKID);
                paramList.Add(item.ExportTaskId);
            }
            paramList.Add(PARM_EXPORTTASKTYPE);
            paramList.Add(item.ExportTaskType.ToString());

            if (item.OperatorDate != DateTime.MinValue)
            {
                paramList.Add(PARM_OPERATORDATE);
                paramList.Add(item.OperatorDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                paramList.Add(PARM_OPERATOR);
                paramList.Add(item.Operator);
            }
            if (!string.IsNullOrEmpty(item.LdbId))
            {
                paramList.Add(PARM_LDBID);
                paramList.Add(item.LdbId);
            }
            paramList.Add(PARM_PRICE);
            paramList.Add(item.Price.ToString("F"));

            paramList.Add(PARM_ORIGINALPRICE);
            paramList.Add(item.OriginalPrice.ToString("F"));

            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_ID + "='" + item.ID + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据thname即导出任务列表id获得一条记录
        /// </summary>
        /// <param name="id">导出任务列表id</param>
        /// <returns></returns>
        public ExportTaskListInfo GetItem(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_ID, id);
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
                ExportTaskListInfo entry = new ExportTaskListInfo();
                #region 判断字段并赋值
                entry.ID = rs.GetValue(PARM_ID) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                entry.BookName = rs.GetValue(PARM_BOOKNAME) ?? "";
                entry.BookType = StructTrans.TransNum(rs.GetValue(PARM_BOOKTYPE));
                entry.ExportTaskId = rs.GetValue(PARM_EXPORTTASKID) ?? "";
                entry.ExportTaskType = StructTrans.TransNum(rs.GetValue(PARM_EXPORTTASKTYPE));
                entry.OperatorDate = StructTrans.TransDate(rs.GetValue(PARM_OPERATORDATE));
                entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                entry.OriginalPrice = StructTrans.TransFloat(rs.GetValue(PARM_ORIGINALPRICE));
                entry.Price = StructTrans.TransFloat(rs.GetValue(PARM_PRICE));
                entry.LdbId = rs.GetValue(PARM_LDBID) ?? "";

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
        /// 根据分页获得多条数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<ExportTaskListInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ExportTaskListInfo> entryList = new List<ExportTaskListInfo>();
                ExportTaskListInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ExportTaskListInfo();
                    #region 判断字段并赋值
                    entry.ID = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                    entry.BookName = rs.GetValue(PARM_BOOKNAME) ?? "";
                    entry.BookType = StructTrans.TransNum(rs.GetValue(PARM_BOOKTYPE));
                    entry.ExportTaskId = rs.GetValue(PARM_EXPORTTASKID) ?? "";
                    entry.ExportTaskType = StructTrans.TransNum(rs.GetValue(PARM_EXPORTTASKTYPE));
                    entry.OperatorDate = StructTrans.TransDate(rs.GetValue(PARM_OPERATORDATE));
                    entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                    entry.OriginalPrice = StructTrans.TransFloat(rs.GetValue(PARM_ORIGINALPRICE));
                    entry.Price = StructTrans.TransFloat(rs.GetValue(PARM_PRICE));
                    entry.LdbId = rs.GetValue(PARM_LDBID) ?? "";

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
        /// 按条件批量删除
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
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
