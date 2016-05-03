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
    public class LogicalDataBase : ILogicalDataBase
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["LogicalDataBase"];
        #region IArticle 字段
        private const string PARM_DBID = "DBID";
        private const string PARM_DBNAME = "DBNAME";
        private const string PARM_DBDESCRIPTION = "DBDESCRIPTION";
        private const string PARM_PARENTDBID = "PARENTDBID";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_DBTYPE = "DBTYPE";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_ONLINEDOI = "ONLINEDOI";
        private const string PARM_ONLINEDATE = "ONLINEDATE";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_COVERPATH = "SYS_FLD_COVERPATH";
        private const string PARM_DBTAG = "DBTAG";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";
        private const string PARM_SYS_FLD_CHECK_DATE = "SYS_FLD_CHECK_DATE";
        private const string PARM_SYS_FLD_CHECK_USERNAME = "SYS_FLD_CHECK_USERNAME";
        private const string PARM_OPERATOR = "OPERATOR";
        private const string PARM_OPERATETIME = "OPERATETIME";
        private const string PARM_SYS_FLD_ICOPATH = "SYS_FLD_ICOPATH";
        private const string PARM_SYS_FLD_CLASSFICATION = "SYS_FLD_CLASSFICATION";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(LogicalDataBaseInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.DbId))
            {
                paramList.Add(PARM_DBID);
                paramList.Add(item.DbId);
            }
            if (!string.IsNullOrEmpty(item.DbName))
            {
                paramList.Add(PARM_DBNAME);
                paramList.Add(item.DbName);
            }
            if (!string.IsNullOrEmpty(item.DbDescription))
            {
                paramList.Add(PARM_DBDESCRIPTION);
                paramList.Add(item.DbDescription);
            }
            if (!string.IsNullOrEmpty(item.ParentDbId))
            {
                paramList.Add(PARM_PARENTDBID);
                paramList.Add(item.ParentDbId);
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            paramList.Add(PARM_DBTYPE);
            paramList.Add(item.Dbtype.ToString());
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.isonline.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
            }
            if (item.OnlineDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ONLINEDATE);
                paramList.Add(item.OnlineDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_COVERPATH))
            {
                paramList.Add(PARM_SYS_FLD_COVERPATH);
                paramList.Add(item.SYS_FLD_COVERPATH);
            }
            if (!string.IsNullOrEmpty(item.DbTag))
            {
                paramList.Add(PARM_DBTAG);
                paramList.Add(item.DbTag);
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
            if (item.SYS_FLD_CHECK_DATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_CHECK_DATE);
                paramList.Add(item.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CHECK_USERNAME))
            {
                paramList.Add(PARM_SYS_FLD_CHECK_USERNAME);
                paramList.Add(item.SYS_FLD_CHECK_USERNAME);
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                paramList.Add(PARM_OPERATOR);
                paramList.Add(item.Operator);
            }
            if (item.OperateTime != DateTime.MinValue)
            {
                paramList.Add(PARM_OPERATETIME);
                paramList.Add(item.OperateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_ICOPATH))
            {
                paramList.Add(PARM_SYS_FLD_ICOPATH);
                paramList.Add(item.SYS_FLD_ICOPATH);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            {
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
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
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_DBID, id);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(LogicalDataBaseInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.DbId))
            {
                paramList.Add(PARM_DBID);
                paramList.Add(item.DbId);
            }
            if (!string.IsNullOrEmpty(item.DbName))
            {
                paramList.Add(PARM_DBNAME);
                paramList.Add(item.DbName);
            }
            if (!string.IsNullOrEmpty(item.DbDescription))
            {
                paramList.Add(PARM_DBDESCRIPTION);
                paramList.Add(item.DbDescription);
            }
            if (!string.IsNullOrEmpty(item.ParentDbId))
            {
                paramList.Add(PARM_PARENTDBID);
                paramList.Add(item.ParentDbId);
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            paramList.Add(PARM_DBTYPE);
            paramList.Add(item.Dbtype.ToString());
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.isonline.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
            }
            if (item.OnlineDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ONLINEDATE);
                paramList.Add(item.OnlineDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_COVERPATH))
            {
                paramList.Add(PARM_SYS_FLD_COVERPATH);
                paramList.Add(item.SYS_FLD_COVERPATH);
            }
            if (!string.IsNullOrEmpty(item.DbTag))
            {
                paramList.Add(PARM_DBTAG);
                paramList.Add(item.DbTag);
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
            if (item.SYS_FLD_CHECK_DATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_CHECK_DATE);
                paramList.Add(item.SYS_FLD_CHECK_DATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CHECK_USERNAME))
            {
                paramList.Add(PARM_SYS_FLD_CHECK_USERNAME);
                paramList.Add(item.SYS_FLD_CHECK_USERNAME);
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                paramList.Add(PARM_OPERATOR);
                paramList.Add(item.Operator);
            }
            if (item.OperateTime != DateTime.MinValue)
            {
                paramList.Add(PARM_OPERATETIME);
                paramList.Add(item.OperateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_ICOPATH))
            {
                paramList.Add(PARM_SYS_FLD_ICOPATH);
                paramList.Add(item.SYS_FLD_ICOPATH);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            {
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            }
            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_DBID + "='" + item.DbId + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id获得一条记录
        /// </summary>
        /// <param name="DbId"></param>
        /// <returns></returns>
        public LogicalDataBaseInfo GetItem(string DbId)
        {
            if (string.IsNullOrEmpty(DbId))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_DBID, DbId);
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
                LogicalDataBaseInfo entry = new LogicalDataBaseInfo();
                #region 判断字段并赋值
                entry.DbId = rs.GetValue(PARM_DBID) ?? "";
                entry.DbName = rs.GetValue(PARM_DBNAME) ?? "";
                entry.DbDescription = rs.GetValue(PARM_DBDESCRIPTION) ?? "";
                entry.ParentDbId = rs.GetValue(PARM_PARENTDBID) ?? "";
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.Dbtype = StructTrans.TransNum(rs.GetValue(PARM_DBTYPE));
                entry.isonline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                entry.OnlineDate = StructTrans.TransDate(rs.GetValue(PARM_ONLINEDATE));
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                entry.DbTag = rs.GetValue(PARM_DBTAG) ?? "";
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                entry.OperateTime = StructTrans.TransDate(rs.GetValue(PARM_OPERATETIME));
                entry.SYS_FLD_ICOPATH=rs.GetValue(PARM_SYS_FLD_ICOPATH)??"";
                entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
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
        public List<LogicalDataBaseInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<LogicalDataBaseInfo> entryList = new List<LogicalDataBaseInfo>();
                LogicalDataBaseInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new LogicalDataBaseInfo();
                    #region 判断字段并赋值
                    entry.DbId = rs.GetValue(PARM_DBID) ?? "";
                    entry.DbName = rs.GetValue(PARM_DBNAME) ?? "";
                    entry.DbDescription = rs.GetValue(PARM_DBDESCRIPTION) ?? "";
                    entry.ParentDbId = rs.GetValue(PARM_PARENTDBID) ?? "";
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Dbtype = StructTrans.TransNum(rs.GetValue(PARM_DBTYPE));
                    entry.isonline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
                    entry.OnlineDate = StructTrans.TransDate(rs.GetValue(PARM_ONLINEDATE));
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
                    entry.DbTag = rs.GetValue(PARM_DBTAG) ?? "";
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                    entry.SYS_FLD_CHECK_DATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_CHECK_DATE));
                    entry.SYS_FLD_CHECK_USERNAME = rs.GetValue(PARM_SYS_FLD_CHECK_USERNAME) ?? "";
                    entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                    entry.OperateTime = StructTrans.TransDate(rs.GetValue(PARM_OPERATETIME));
                    entry.SYS_FLD_ICOPATH = rs.GetValue(PARM_SYS_FLD_ICOPATH) ?? "";
                    entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
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
            //删除逻辑数据库
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">逻辑数据库的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改逻辑数据库状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">逻辑数据库的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            string strSet = string.Format("UPDATE {0} SET OPERATETIME='{1}',ISONLINE='{2}' WHERE DBID='{3}'", TABLE_NAME, dateTime, isOnLine, id);
            return TPIHelper.ExecSql(strSet);
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
