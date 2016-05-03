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
    public class DataBaseList : IDataBaseList
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["DataBaseList"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_TITLE = "TITLE";
        private const string PARM_TABLENAME = "TABLENAME";
        private const string PARM_DIRPREFIX = "DIRPREFIX";
        private const string PARM_DATABASETYPE = "DATABASETYPE";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(DataBaseListInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Title))
            {
                paramList.Add(PARM_TITLE);
                paramList.Add(item.Title);
            }
            if (!string.IsNullOrEmpty(item.TableName))
            {
                paramList.Add(PARM_TABLENAME);
                paramList.Add(item.TableName);
            }
            if (!string.IsNullOrEmpty(item.dirprefix))
            {
                paramList.Add(PARM_DIRPREFIX);
                paramList.Add(item.dirprefix);
            }
            paramList.Add(PARM_DATABASETYPE);
            paramList.Add(item.DatabaseType.ToString());
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
        public bool Update(DataBaseListInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Title))
            {
                paramList.Add(PARM_TITLE);
                paramList.Add(item.Title);
            }
            if (!string.IsNullOrEmpty(item.TableName))
            {
                paramList.Add(PARM_TABLENAME);
                paramList.Add(item.TableName);
            }
            if (!string.IsNullOrEmpty(item.dirprefix))
            {
                paramList.Add(PARM_DIRPREFIX);
                paramList.Add(item.dirprefix);
            }
            paramList.Add(PARM_DATABASETYPE);
            paramList.Add(item.DatabaseType.ToString());
            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_ID + "='" + item.Id + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id获得一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataBaseListInfo GetItem(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_ID, Id);
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
                DataBaseListInfo entry = new DataBaseListInfo();
                #region 判断字段并赋值
                entry.Id = StructTrans.TransNum(rs.GetValue(PARM_ID));
                entry.Title = rs.GetValue(PARM_TITLE) ?? "";
                entry.TableName = rs.GetValue(PARM_TABLENAME) ?? "";
                entry.dirprefix = rs.GetValue(PARM_DIRPREFIX) ?? "";
                entry.DatabaseType = StructTrans.TransNum(rs.GetValue(PARM_DATABASETYPE));
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
        public List<DataBaseListInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<DataBaseListInfo> entryList = new List<DataBaseListInfo>();
                DataBaseListInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new DataBaseListInfo();
                    #region 判断字段并赋值
                    entry.Id = StructTrans.TransNum(rs.GetValue(PARM_ID));
                    entry.Title = rs.GetValue(PARM_TITLE) ?? "";
                    entry.TableName = rs.GetValue(PARM_TABLENAME) ?? "";
                    entry.dirprefix = rs.GetValue(PARM_DIRPREFIX) ?? "";
                    entry.DatabaseType = StructTrans.TransNum(rs.GetValue(PARM_DATABASETYPE));
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
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件获得记录总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
