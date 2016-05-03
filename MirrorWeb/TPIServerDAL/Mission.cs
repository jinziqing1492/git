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
    public class Mission : IMission
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Mission"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_NAME = "NAME";
        private const string PARM_RESTYPE = "RESTYPE";
        private const string PARM_RESDOI = "RESDOI";
        private const string PARM_RESNAME = "RESNAME";
        private const string PARM_EXECUTEUSER = "EXECUTEUSER";
        private const string PARM_SENDUSER = "SENDUSER";
        private const string PARM_DEADLINE = "DEADLINE";
        private const string PARM_FINISHDATE = "FINISHDATE";
        private const string PARM_CREATETIME = "CREATETIME";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_OPERATOR = "OPERATOR";
        private const string PARM_WORKSTATUS = "WORKSTATUS";
        private const string PARM_AUDITREMARK = "AUDITREMARK";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";
        private const string PARM_ISSENDED = "ISSENDED";
        private const string PARM_REJECT = "REJECT";
        private const string PARM_FINISHSTATUS = "FINISHSTATUS";
        private const string PARM_ISBAT = "ISBAT";
        private const string PARM_DEPARTMENT = "DEPARTMENT";
        private const string PARM_DEPARTMENTNAME = "DEPARTMENTNAME";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(MissionInfo item)
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
            if (!string.IsNullOrEmpty(item.ResDOI))
            {
                paramList.Add(PARM_RESDOI);
                paramList.Add(item.ResDOI);
            }
            if (!string.IsNullOrEmpty(item.ResName))
            {
                paramList.Add(PARM_RESNAME);
                paramList.Add(item.ResName);
            }
            if (!string.IsNullOrEmpty(item.ExecuteUser))
            {
                paramList.Add(PARM_EXECUTEUSER);
                paramList.Add(item.ExecuteUser);
            }
            if (!string.IsNullOrEmpty(item.SendUser))
            {
                paramList.Add(PARM_SENDUSER);
                paramList.Add(item.SendUser);
            }
            if (item.DeadLine != DateTime.MinValue)
            {
                paramList.Add(PARM_DEADLINE);
                paramList.Add(item.DeadLine.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.FinishDate != DateTime.MinValue)
            {
                paramList.Add(PARM_FINISHDATE);
                paramList.Add(item.FinishDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.CreateTime != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATETIME);
                paramList.Add(item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                paramList.Add(PARM_OPERATOR);
                paramList.Add(item.Operator);
            }
            //if (!string.IsNullOrEmpty(item.WorkStatus)) 
            //{
                paramList.Add(PARM_WORKSTATUS);
                paramList.Add(item.WorkStatus.ToString());
           // }
            if (!string.IsNullOrEmpty(item.AuditRemark))
            {
                paramList.Add(PARM_AUDITREMARK);
                paramList.Add(item.AuditRemark);
            }
            if (!string.IsNullOrEmpty(item.Department))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
            }
            if (!string.IsNullOrEmpty(item.DepartmentName))
            {
                paramList.Add(PARM_DEPARTMENTNAME);
                paramList.Add(item.DepartmentName);
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());


            paramList.Add(PARM_ISSENDED);
            paramList.Add(item.isSended.ToString());

            paramList.Add(PARM_REJECT);
            paramList.Add(item.Reject);

            paramList.Add(PARM_FINISHSTATUS);
            paramList.Add(item.FinishStatus.ToString());


            paramList.Add(PARM_ISBAT);
            paramList.Add(item.IsBat.ToString());

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
        public bool Update(MissionInfo item)
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
            if (!string.IsNullOrEmpty(item.ResDOI))
            {
                paramList.Add(PARM_RESDOI);
                paramList.Add(item.ResDOI);
            }
            if (!string.IsNullOrEmpty(item.ResName))
            {
                paramList.Add(PARM_RESNAME);
                paramList.Add(item.ResName);
            }
            if (!string.IsNullOrEmpty(item.ExecuteUser))
            {
                paramList.Add(PARM_EXECUTEUSER);
                paramList.Add(item.ExecuteUser);
            }
            if (!string.IsNullOrEmpty(item.SendUser))
            {
                paramList.Add(PARM_SENDUSER);
                paramList.Add(item.SendUser);
            }
            if (item.DeadLine != DateTime.MinValue)
            {
                paramList.Add(PARM_DEADLINE);
                paramList.Add(item.DeadLine.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.FinishDate != DateTime.MinValue)
            {
                paramList.Add(PARM_FINISHDATE);
                paramList.Add(item.FinishDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.CreateTime != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATETIME);
                paramList.Add(item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                paramList.Add(PARM_OPERATOR);
                paramList.Add(item.Operator);
            }
            //if (!string.IsNullOrEmpty(item.WorkStatus))
            //{
                paramList.Add(PARM_WORKSTATUS);
                paramList.Add(item.WorkStatus.ToString());
           // }
            if (!string.IsNullOrEmpty(item.AuditRemark))
            {
                paramList.Add(PARM_AUDITREMARK);
                paramList.Add(item.AuditRemark);
            }
            if (!string.IsNullOrEmpty(item.Department))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.Department);
            }
            if (!string.IsNullOrEmpty(item.DepartmentName))
            {
                paramList.Add(PARM_DEPARTMENTNAME);
                paramList.Add(item.DepartmentName);
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());

            paramList.Add(PARM_ISSENDED);
            paramList.Add(item.isSended.ToString());

            paramList.Add(PARM_REJECT);
            paramList.Add(item.Reject);

            paramList.Add(PARM_FINISHSTATUS);
            paramList.Add(item.FinishStatus.ToString());


            paramList.Add(PARM_ISBAT);
            paramList.Add(item.IsBat.ToString());

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
        public MissionInfo GetItem(string id)
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
                MissionInfo entry = new MissionInfo();
                #region 判断字段并赋值
                entry.ID = StructTrans.TransNum(rs.GetValue(PARM_ID));
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ResType = StructTrans.TransNum(rs.GetValue(PARM_RESTYPE));
                entry.ResDOI = NormalFunction.ResetRedFlag(rs.GetValue(PARM_RESDOI)) ?? "";
                entry.ResName = rs.GetValue(PARM_RESNAME) ?? "";
                entry.ExecuteUser = rs.GetValue(PARM_EXECUTEUSER) ?? "";
                entry.SendUser = rs.GetValue(PARM_SENDUSER) ?? "";
                entry.DeadLine = StructTrans.TransDate(rs.GetValue(PARM_DEADLINE));
                entry.FinishDate = StructTrans.TransDate(rs.GetValue(PARM_FINISHDATE));
                entry.CreateTime = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME));
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                entry.WorkStatus = StructTrans.TransNum(rs.GetValue(PARM_WORKSTATUS));
                entry.AuditRemark = rs.GetValue(PARM_AUDITREMARK) ?? "";
                entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";
                entry.DepartmentName = rs.GetValue(PARM_DEPARTMENTNAME) ?? "";
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                entry.isSended = StructTrans.TransNum(rs.GetValue(PARM_ISSENDED));
                entry.FinishStatus = StructTrans.TransNum(rs.GetValue(PARM_FINISHSTATUS));
                entry.Reject = rs.GetValue(PARM_REJECT);
                entry.IsBat = StructTrans.TransNum(rs.GetValue(PARM_ISBAT));

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
        public List<MissionInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<MissionInfo> entryList = new List<MissionInfo>();
                MissionInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new MissionInfo();
                    #region 判断字段并赋值
                    entry.ID = StructTrans.TransNum(rs.GetValue(PARM_ID));
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ResType = StructTrans.TransNum(rs.GetValue(PARM_RESTYPE));
                    entry.ResDOI = NormalFunction.ResetRedFlag(rs.GetValue(PARM_RESDOI)) ?? "";
                    entry.ResName = rs.GetValue(PARM_RESNAME) ?? "";
                    entry.ExecuteUser = rs.GetValue(PARM_EXECUTEUSER) ?? "";
                    entry.SendUser = rs.GetValue(PARM_SENDUSER) ?? "";
                    entry.DeadLine = StructTrans.TransDate(rs.GetValue(PARM_DEADLINE));
                    entry.FinishDate = StructTrans.TransDate(rs.GetValue(PARM_FINISHDATE));
                    entry.CreateTime = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME));
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Operator = rs.GetValue(PARM_OPERATOR) ?? "";
                    entry.WorkStatus = StructTrans.TransNum(rs.GetValue(PARM_WORKSTATUS));
                    entry.AuditRemark = rs.GetValue(PARM_AUDITREMARK) ?? "";
                    entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";
                    entry.DepartmentName = rs.GetValue(PARM_DEPARTMENTNAME) ?? "";
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                    entry.isSended = StructTrans.TransNum(rs.GetValue(PARM_ISSENDED));
                    entry.FinishStatus = StructTrans.TransNum(rs.GetValue(PARM_FINISHSTATUS));
                    entry.Reject = rs.GetValue(PARM_REJECT);
                    entry.IsBat = StructTrans.TransNum(rs.GetValue(PARM_ISBAT));

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
            //删除任务
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">任务的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改任务状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }

        /// <summary>
        /// 设置任务状态
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetWorkState(string doi, int state)
        {
            //修改任务状态
            string strSet = string.Format("UPDATE {0} SET PARM_WORKSTATUS={1} WHERE SYS_FLD_DOI='{2}'", TABLE_NAME, state, doi);
            return TPIHelper.ExecSql(strSet);
        }
        /// <summary>
        /// 设置任务状态和完成情况
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state"></param>
        /// <param name="finishstate"></param>
        /// <returns></returns>
        public bool SetWorkState(string doi, int state, int finishstate)
        {
            //修改任务状态
            string strSet = string.Format("UPDATE {0} SET PARM_WORKSTATUS={1},PARM_FINISHDATE={2} WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, state, finishstate, doi);
            return TPIHelper.ExecSql(strSet);
        }
    }
}
