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
    /// <summary>
    /// 下载申请实体
    /// </summary>
    public class DownLoadApply:IDownLoadApply
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["downloadapply"];

        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_USERNAME = "USERNAME";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_ATTACHMENTID = "ATTACHMENTID";
        private const string PARM_ATTACHMENTNAME = "ATTACHMENTNAME";
        private const string PARM_AUDITUSER = "AUDITUSER";
        private const string PARM_CHECKSTATUS = "CHECKSTATUS";
        private const string PARM_CHECKDESCRIPTION = "CHECKDESCRIPTION";
        private const string PARM_APPLYDATE = "APPLYDATE";
        private const string PARM_CHECKDATE = "CHECKDATE";
        private const string PARM_ISDOWNLOAD = "ISDOWNLOAD";
        private const string PARM_ATTACHMENTTYPE = "attachmenttype";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";

        #endregion
        /// <summary>
        /// 添加下载申请
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(DownLoadApplyInfo item)
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
            if (!string.IsNullOrEmpty(item.UserName))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.UserName);
            }
            if (!string.IsNullOrEmpty(item.Description))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.Description);
            }
            if (!string.IsNullOrEmpty(item.AttachmentID))
            {
                paramList.Add(PARM_ATTACHMENTID);
                paramList.Add(item.AttachmentID);
            }
            if (!string.IsNullOrEmpty(item.AttachmentName))
            {
                paramList.Add(PARM_ATTACHMENTNAME);
                paramList.Add(item.AttachmentName);
            }
            if (!string.IsNullOrEmpty(item.AuditUser))
            {
                paramList.Add(PARM_AUDITUSER);
                paramList.Add(item.AuditUser);
            }
            paramList.Add(PARM_CHECKSTATUS);
            paramList.Add(item.CheckStatus.ToString());
            if (!string.IsNullOrEmpty(item.CheckDescription))
            {
                paramList.Add(PARM_CHECKDESCRIPTION);
                paramList.Add(item.CheckDescription);
            }
            if (item.ApplyDate != DateTime.MinValue)
            {
                paramList.Add(PARM_APPLYDATE);
                paramList.Add(item.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.CheckDate != DateTime.MinValue)
            {
                paramList.Add(PARM_CHECKDATE);
                paramList.Add(item.CheckDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_ISDOWNLOAD);
            paramList.Add(item.IsDownload.ToString());

            paramList.Add(PARM_ATTACHMENTTYPE);
            paramList.Add(item.AttachmentType.ToString());

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
        /// 删除下载申请
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
        /// 更新申请
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(DownLoadApplyInfo item)
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
            if (!string.IsNullOrEmpty(item.UserName))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.UserName);
            }
            if (!string.IsNullOrEmpty(item.Description))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.Description);
            }
            if (!string.IsNullOrEmpty(item.AttachmentID))
            {
                paramList.Add(PARM_ATTACHMENTID);
                paramList.Add(item.AttachmentID);
            }
            if (!string.IsNullOrEmpty(item.AttachmentName))
            {
                paramList.Add(PARM_ATTACHMENTNAME);
                paramList.Add(item.AttachmentName);
            }
            if (!string.IsNullOrEmpty(item.AuditUser))
            {
                paramList.Add(PARM_AUDITUSER);
                paramList.Add(item.AuditUser);
            }
            paramList.Add(PARM_CHECKSTATUS);
            paramList.Add(item.CheckStatus.ToString());
            if (!string.IsNullOrEmpty(item.CheckDescription))
            {
                paramList.Add(PARM_CHECKDESCRIPTION);
                paramList.Add(item.CheckDescription);
            }
            if (item.ApplyDate != DateTime.MinValue)
            {
                paramList.Add(PARM_APPLYDATE);
                paramList.Add(item.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.CheckDate != DateTime.MinValue)
            {
                paramList.Add(PARM_CHECKDATE);
                paramList.Add(item.CheckDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_ISDOWNLOAD);
            paramList.Add(item.IsDownload.ToString());

            paramList.Add(PARM_ATTACHMENTTYPE);
            paramList.Add(item.AttachmentType.ToString());

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
        /// 获取申请
        /// </summary>
        /// <param name="thname"></param>
        /// <returns></returns>
        public DownLoadApplyInfo GetItem(string thname)
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
                DownLoadApplyInfo entry = new DownLoadApplyInfo();
                #region 判断字段并赋值
                entry.ID = rs.GetValue(PARM_ID) ?? "";
                entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.AttachmentID = rs.GetValue(PARM_ATTACHMENTID) ?? "";
                entry.AttachmentName = rs.GetValue(PARM_ATTACHMENTNAME) ?? "";
                entry.AuditUser = rs.GetValue(PARM_AUDITUSER) ?? "";
                entry.CheckStatus = StructTrans.TransNum(rs.GetValue(PARM_CHECKSTATUS));
                entry.CheckDescription = rs.GetValue(PARM_CHECKDESCRIPTION) ?? "";
                entry.ApplyDate = StructTrans.TransDate(rs.GetValue(PARM_APPLYDATE));
                entry.CheckDate = StructTrans.TransDate(rs.GetValue(PARM_CHECKDATE));
                entry.IsDownload = StructTrans.TransNum(rs.GetValue(PARM_ISDOWNLOAD));
                entry.AttachmentType = StructTrans.TransNum(rs.GetValue(PARM_ATTACHMENTTYPE));

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
        /// 获取列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<DownLoadApplyInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<DownLoadApplyInfo> entryList = new List<DownLoadApplyInfo>();
                DownLoadApplyInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new DownLoadApplyInfo();
                    #region 判断字段并赋值
                    entry.ID = rs.GetValue(PARM_ID) ?? "";
                    entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                    entry.Description = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.AttachmentID = rs.GetValue(PARM_ATTACHMENTID) ?? "";
                    entry.AttachmentName = rs.GetValue(PARM_ATTACHMENTNAME) ?? "";
                    entry.AuditUser = rs.GetValue(PARM_AUDITUSER) ?? "";
                    entry.CheckStatus = StructTrans.TransNum(rs.GetValue(PARM_CHECKSTATUS));
                    entry.CheckDescription = rs.GetValue(PARM_CHECKDESCRIPTION) ?? "";
                    entry.ApplyDate = StructTrans.TransDate(rs.GetValue(PARM_APPLYDATE));
                    entry.CheckDate = StructTrans.TransDate(rs.GetValue(PARM_CHECKDATE));
                    entry.IsDownload = StructTrans.TransNum(rs.GetValue(PARM_ISDOWNLOAD));
                    entry.AttachmentType = StructTrans.TransNum(rs.GetValue(PARM_ATTACHMENTTYPE));

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
        /// 按条件删除
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

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
