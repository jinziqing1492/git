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
    public class Log : ILog
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Log"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_NAME = "NAME";
        private const string PARM_RESTYPE = "RESTYPE";
        private const string PARM_RESDOI = "RESDOI";
        private const string PARM_LOGTYPE = "LOGTYPE";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_ADDDATE = "ADDDATE";
        private const string PARM_USERNAME = "USERNAME";
        private const string PARM_USERTYPE = "USERTYPE";
        private const string PARM_IP = "IP";
        private const string PARM_IPNUM = "IPNUM";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(LogInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();

            if (!string.IsNullOrEmpty(item.Id))
            {
                paramList.Add(PARM_ID);
                paramList.Add(item.Id);
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            paramList.Add(PARM_RESTYPE);
            paramList.Add(item.ResType.ToString());
            if (!string.IsNullOrEmpty(item.ResDoi))
            {
                paramList.Add(PARM_RESDOI);
                paramList.Add(item.ResDoi);
            }
            paramList.Add(PARM_LOGTYPE);
            paramList.Add(item.LogType.ToString());
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (item.Adddate != DateTime.MinValue)
            {
                paramList.Add(PARM_ADDDATE);
                paramList.Add(item.Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.username))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.username);
            }
            paramList.Add(PARM_USERTYPE);
            paramList.Add(item.userType.ToString());
            if (!string.IsNullOrEmpty(item.Ip))
            {
                paramList.Add(PARM_IP);
                paramList.Add(item.Ip);
            }
            if (!string.IsNullOrEmpty(item.IpNum))
            {
                paramList.Add(PARM_IPNUM);
                paramList.Add(item.IpNum);
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
        public bool Update(LogInfo item)
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
            paramList.Add(PARM_RESTYPE);
            paramList.Add(item.ResType.ToString());
            if (!string.IsNullOrEmpty(item.ResDoi))
            {
                paramList.Add(PARM_RESDOI);
                paramList.Add(item.ResDoi);
            }
            paramList.Add(PARM_LOGTYPE);
            paramList.Add(item.LogType.ToString());
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (item.Adddate != DateTime.MinValue)
            {
                paramList.Add(PARM_ADDDATE);
                paramList.Add(item.Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.username))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.username);
            }
            paramList.Add(PARM_USERTYPE);
            paramList.Add(item.userType.ToString());
            if (!string.IsNullOrEmpty(item.Ip))
            {
                paramList.Add(PARM_IP);
                paramList.Add(item.Ip);
            }
            if (!string.IsNullOrEmpty(item.IpNum))
            {
                paramList.Add(PARM_IPNUM);
                paramList.Add(item.IpNum);
            }
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
        /// <param name="id"></param>
        /// <returns></returns>
        public LogInfo GetItem(string id)
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
                LogInfo entry = new LogInfo();
                #region 判断字段并赋值
                entry.Id = rs.GetValue(PARM_ID) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ResType = StructTrans.TransNum(rs.GetValue(PARM_RESTYPE));
                entry.ResDoi = rs.GetValue(PARM_RESDOI) ?? "";
                entry.LogType = StructTrans.TransNum(rs.GetValue(PARM_LOGTYPE));
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.Adddate = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                entry.username = rs.GetValue(PARM_USERNAME) ?? "";
                entry.userType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                entry.Ip = rs.GetValue(PARM_IP) ?? "";
                entry.IpNum = rs.GetValue(PARM_IPNUM) ?? "";
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
        public List<LogInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<LogInfo> entryList = new List<LogInfo>();
                LogInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new LogInfo();
                    #region 判断字段并赋值
                    entry.Id = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ResType = StructTrans.TransNum(rs.GetValue(PARM_RESTYPE));
                    entry.ResDoi = rs.GetValue(PARM_RESDOI) ?? "";
                    entry.LogType = StructTrans.TransNum(rs.GetValue(PARM_LOGTYPE));
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Adddate = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                    entry.username = rs.GetValue(PARM_USERNAME) ?? "";
                    entry.userType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                    entry.Ip = rs.GetValue(PARM_IP) ?? "";
                    entry.IpNum = rs.GetValue(PARM_IPNUM) ?? "";
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
        /// 获取热词
        /// </summary>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<LogInfo> GetHotWord(int pageCount)
        {
            pageCount = 10;
            string sqlWhere = "LOGTYPE=3 GROUP BY NAME ORDER BY COUNT(*) DESC";
            RecordSet rs = TPIHelper.GetRecordPartField(TABLE_NAME, sqlWhere, "COUNT(*),*");
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
            rs.Move(0);
            try
            {
                List<LogInfo> entryList = new List<LogInfo>();
                LogInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new LogInfo();
                    #region 判断字段并赋值
                    entry.Id = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ResType = StructTrans.TransNum(rs.GetValue(PARM_RESTYPE));
                    entry.ResDoi = rs.GetValue(PARM_RESDOI) ?? "";
                    entry.LogType = StructTrans.TransNum(rs.GetValue(PARM_LOGTYPE));
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Adddate = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                    entry.username = rs.GetValue(PARM_USERNAME) ?? "";
                    entry.userType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                    entry.Ip = rs.GetValue(PARM_IP) ?? "";
                    entry.IpNum = rs.GetValue(PARM_IPNUM) ?? "";
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
            //删除日志
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
