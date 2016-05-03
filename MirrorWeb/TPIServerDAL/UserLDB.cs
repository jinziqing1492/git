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
    public class UserLDB : IUserLDB
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["UserLDB"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_USERNAME = "USERNAME";
        private const string PARM_LDBID = "LDBID";
        private const string PARM_LDBNAME = "LDBNAME";
        private const string PARM_SARTDATE = "SARTDATE";
        private const string PARM_ENDDATE = "ENDDATE";
        private const string PARM_OPERATOR = "OPERATOR";
        private const string PARM_OPERATETIME = "OPERATETIME";
        private const string PARM_FLAG = "FLAG";
        private const string PARM_STATUS = "STATUS";
        private const string PARM_ONLINEDOI = "ONLINEDOI";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        public int GetMaxID()
        {
            string sql = "SELECT MAX(ID) FROM DPM_USERLDB GO";
            RecordSet rs = TPIHelper.GetRecordSet(sql);
            return Convert.ToInt32(rs.GetValue("MAX(ID)")) + 1;
        }
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(UserLDBInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            
            IList<string> paramList = new List<string>();
            paramList.Add(PARM_ID);
            //paramList.Add(GetMaxID().ToString());
            paramList.Add(item.Id);
            if (!string.IsNullOrEmpty(item.UserName))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.UserName);
            }
           
            if (!string.IsNullOrEmpty(item.LDBID))
            {
                paramList.Add(PARM_LDBID);
                paramList.Add(item.LDBID);
            }
            if (!string.IsNullOrEmpty(item.LDBName))
            {
                paramList.Add(PARM_LDBNAME);
                paramList.Add(item.LDBName);
            }
            if (item.SartDate != DateTime.MinValue)
            {
                paramList.Add(PARM_SARTDATE);
                paramList.Add(item.SartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.EndDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ENDDATE);
                paramList.Add(item.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
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
            paramList.Add(PARM_FLAG);
            paramList.Add(item.Flag.ToString());
            paramList.Add(PARM_STATUS);
            paramList.Add(item.Status.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
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
        public bool Update(UserLDBInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.UserName))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.UserName);
            }
            if (!string.IsNullOrEmpty(item.LDBID))
            {
                paramList.Add(PARM_LDBID);
                paramList.Add(item.LDBID);
            }
            if (!string.IsNullOrEmpty(item.LDBName))
            {
                paramList.Add(PARM_LDBNAME);
                paramList.Add(item.LDBName);
            }
            if (item.SartDate != DateTime.MinValue)
            {
                paramList.Add(PARM_SARTDATE);
                paramList.Add(item.SartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.EndDate != DateTime.MinValue)
            {
                paramList.Add(PARM_ENDDATE);
                paramList.Add(item.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
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
            paramList.Add(PARM_FLAG);
            paramList.Add(item.Flag.ToString());
            paramList.Add(PARM_STATUS);
            paramList.Add(item.Status.ToString());
            if (!string.IsNullOrEmpty(item.OnlineDoi))
            {
                paramList.Add(PARM_ONLINEDOI);
                paramList.Add(item.OnlineDoi);
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
        /// <param name="Id"></param>
        /// <returns></returns>
        public UserLDBInfo GetItem(string Id)
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
                UserLDBInfo entry = new UserLDBInfo();
                #region 判断字段并赋值
                entry.Id = rs.GetValue(PARM_ID) ?? "";
                entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                entry.LDBID = rs.GetValue(PARM_LDBID) ?? "";
                entry.LDBName = rs.GetValue(PARM_LDBNAME) ?? "";
                entry.SartDate = StructTrans.TransDate(rs.GetValue(PARM_SARTDATE));
                entry.EndDate = StructTrans.TransDate(rs.GetValue(PARM_ENDDATE));
                entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                entry.OperateTime = StructTrans.TransDate(rs.GetValue(PARM_OPERATETIME));
                entry.Flag = StructTrans.TransNum(rs.GetValue(PARM_FLAG));
                entry.Status = StructTrans.TransNum(rs.GetValue(PARM_STATUS));
                entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
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
        public List<UserLDBInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<UserLDBInfo> entryList = new List<UserLDBInfo>();
                UserLDBInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new UserLDBInfo();
                    #region 判断字段并赋值
                    entry.Id = rs.GetValue(PARM_ID) ?? "";
                    entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                    entry.LDBID = rs.GetValue(PARM_LDBID) ?? "";
                    entry.LDBName = rs.GetValue(PARM_LDBNAME) ?? "";
                    entry.SartDate = StructTrans.TransDate(rs.GetValue(PARM_SARTDATE));
                    entry.EndDate = StructTrans.TransDate(rs.GetValue(PARM_ENDDATE));
                    entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                    entry.OperateTime = StructTrans.TransDate(rs.GetValue(PARM_OPERATETIME));
                    entry.Flag = StructTrans.TransNum(rs.GetValue(PARM_FLAG));
                    entry.Status = StructTrans.TransNum(rs.GetValue(PARM_STATUS));
                    entry.OnlineDoi = rs.GetValue(PARM_ONLINEDOI) ?? "";
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
            //删除用户包库记录
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">用户包库记录的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改用户包库记录状态
            string strSet = "UPDATE " + TABLE_NAME + " SET FLAG=" + state + " WHERE ID='"+id+"'";
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">用户包库记录的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_DATE='{1}',ISONLINE='{2}' WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, dateTime, isOnLine, id);
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
