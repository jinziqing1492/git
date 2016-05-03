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
    public class UEBookList : IUEBookList
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["UEBookList"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_NAME = "NAME";
        private const string PARM_BOOKID = "BOOKID";
        private const string PARM_BOOKNAME = "BOOKNAME";
        private const string PARM_BOOKTYPE = "BOOKTYPE";
        private const string PARM_OPERATORDATE = "OPERATORDATE";
        private const string PARM_OPERATOR = "OPERATOR";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(UEBookListInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
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
        /// <param name="id"></param>
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
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(UEBookListInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
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
        /// 根据id获得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UEBookListInfo GetItem(string id)
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
                UEBookListInfo entry = new UEBookListInfo();
                #region 判断字段并赋值
                entry.ID = rs.GetValue(PARM_ID) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                entry.BookName = rs.GetValue(PARM_BOOKNAME) ?? "";
                entry.BookType = StructTrans.TransNum(rs.GetValue(PARM_BOOKTYPE));
                entry.OperatorDate = StructTrans.TransDate(rs.GetValue(PARM_OPERATORDATE));
                entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
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
        /// 根据分页获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<UEBookListInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<UEBookListInfo> entryList = new List<UEBookListInfo>();
                UEBookListInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new UEBookListInfo();
                    #region 判断字段并赋值
                    entry.ID = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.BookId = rs.GetValue(PARM_BOOKID) ?? "";
                    entry.BookName = rs.GetValue(PARM_BOOKNAME) ?? "";
                    entry.BookType = StructTrans.TransNum(rs.GetValue(PARM_BOOKTYPE));
                    entry.OperatorDate = StructTrans.TransDate(rs.GetValue(PARM_OPERATORDATE));
                    entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
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
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrWhiteSpace(strWhere))
            {
                return false;
            }
            //删除u盘电子书
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件获得记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
