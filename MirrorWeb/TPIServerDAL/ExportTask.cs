using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using DRMS.Model;
using DRMS.IDAL;
using TPI;
using CNKI.BaseFunction;

namespace DRMS.TPIServerDAL
{
    public class ExportTask:IExportTask

    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["exporttask"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_NAME = "NAME";
        private const string PARM_TOUSER = "TOUSER";
        private const string PARM_EXPORTDATE = "EXPORTDATE";
        private const string PARM_CREATEDATE = "CREATEDATE";
        private const string PARM_CREATEUSER = "CREATEUSER";
        private const string PARM_TASKTYPE = "TASKTYPE";
        private const string PARM_TASKSTATUS = "TASKSTATUS";
        private const string PARM_PROPORTION = "PROPORTION";
        private const string PARM_ISDOWNLOAD = "ISDOWNLOAD";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_SELDATABASETYPE = "seldatabasetype";


        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ExportTaskInfo item)
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
            if (!string.IsNullOrEmpty(item.ToUser))
            {
                paramList.Add(PARM_TOUSER);
                paramList.Add(item.ToUser);
            }
            if (item.ExportDate != DateTime.MinValue)
            {
                paramList.Add(PARM_EXPORTDATE);
                paramList.Add(item.ExportDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.CreateDate != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATEDATE);
                paramList.Add(item.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.CreateUser))
            {
                paramList.Add(PARM_CREATEUSER);
                paramList.Add(item.CreateUser);
            }
            paramList.Add(PARM_TASKTYPE);
            paramList.Add(item.TaskType.ToString());
            paramList.Add(PARM_TASKSTATUS);
            paramList.Add(item.TaskStatus.ToString());
            if (!string.IsNullOrEmpty(item.Proportion))
            {
                paramList.Add(PARM_PROPORTION);
                paramList.Add(item.Proportion);
            }
            paramList.Add(PARM_ISDOWNLOAD);
            paramList.Add(item.IsDownload.ToString());
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
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }

            paramList.Add(PARM_SELDATABASETYPE);
            paramList.Add(item.SelDatabaseType.ToString());

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
        /// 单条删除
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
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(ExportTaskInfo item)
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
            if (!string.IsNullOrEmpty(item.ToUser))
            {
                paramList.Add(PARM_TOUSER);
                paramList.Add(item.ToUser);
            }
            if (item.ExportDate != DateTime.MinValue)
            {
                paramList.Add(PARM_EXPORTDATE);
                paramList.Add(item.ExportDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.CreateDate != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATEDATE);
                paramList.Add(item.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.CreateUser))
            {
                paramList.Add(PARM_CREATEUSER);
                paramList.Add(item.CreateUser);
            }
            paramList.Add(PARM_TASKTYPE);
            paramList.Add(item.TaskType.ToString());
            paramList.Add(PARM_TASKSTATUS);
            paramList.Add(item.TaskStatus.ToString());
            if (!string.IsNullOrEmpty(item.Proportion))
            {
                paramList.Add(PARM_PROPORTION);
                paramList.Add(item.Proportion);
            }
            paramList.Add(PARM_ISDOWNLOAD);
            paramList.Add(item.IsDownload.ToString());
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
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }

            paramList.Add(PARM_SELDATABASETYPE);
            paramList.Add(item.SelDatabaseType.ToString());

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
        /// 获取单条记录
        /// </summary>
        /// <param name="thname"></param>
        /// <returns></returns>
        public ExportTaskInfo GetItem(string thname)
        {
            if (string.IsNullOrEmpty(thname))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_ID, thname);
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
                ExportTaskInfo entry = new ExportTaskInfo();
                #region 判断字段并赋值
                entry.ID = rs.GetValue(PARM_ID) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ToUser = rs.GetValue(PARM_TOUSER) ?? "";
                entry.ExportDate = StructTrans.TransDate(rs.GetValue(PARM_EXPORTDATE));
                entry.CreateDate = StructTrans.TransDate(rs.GetValue(PARM_CREATEDATE));
                entry.CreateUser = rs.GetValue(PARM_CREATEUSER) ?? "";
                entry.TaskType = StructTrans.TransNum(rs.GetValue(PARM_TASKTYPE));
                entry.TaskStatus = StructTrans.TransNum(rs.GetValue(PARM_TASKSTATUS));
                entry.Proportion = rs.GetValue(PARM_PROPORTION) ?? "";
                entry.IsDownload = StructTrans.TransNum(rs.GetValue(PARM_ISDOWNLOAD));
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.SelDatabaseType = StructTrans.TransNum(rs.GetValue(PARM_SELDATABASETYPE));

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
        /// 获取数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<ExportTaskInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ExportTaskInfo> entryList = new List<ExportTaskInfo>();
                ExportTaskInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ExportTaskInfo();
                    #region 判断字段并赋值
                    entry.ID = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ToUser = rs.GetValue(PARM_TOUSER) ?? "";
                    entry.ExportDate = StructTrans.TransDate(rs.GetValue(PARM_EXPORTDATE));
                    entry.CreateDate = StructTrans.TransDate(rs.GetValue(PARM_CREATEDATE));
                    entry.CreateUser = rs.GetValue(PARM_CREATEUSER) ?? "";
                    entry.TaskType = StructTrans.TransNum(rs.GetValue(PARM_TASKTYPE));
                    entry.TaskStatus = StructTrans.TransNum(rs.GetValue(PARM_TASKSTATUS));
                    entry.Proportion = rs.GetValue(PARM_PROPORTION) ?? "";
                    entry.IsDownload = StructTrans.TransNum(rs.GetValue(PARM_ISDOWNLOAD));
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.SelDatabaseType = StructTrans.TransNum(rs.GetValue(PARM_SELDATABASETYPE));

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
        /// 获取记录数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }

        /// <summary>
        /// 
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
    }
}
