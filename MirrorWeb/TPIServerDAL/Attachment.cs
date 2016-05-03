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
    public class Attachment : IAttachment
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Attachment"];
        #region IArticle 字段
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_NAME = "NAME";
        private const string PARM_TYPE = "TYPE";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_FILETYPE = "SYS_FLD_FILETYPE";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_AUDITUSER = "AUDITUSER";
        private const string PARM_EDITTOOL = "EDITTOOL";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_SYS_FLD_CHAPTERDOI = "SYS_FLD_CHAPTERDOI";
        private const string PARM_DEPARTMENT = "DEPARTMENT";
        private const string PARM_SYS_FLD_PARENTTYPE = "sys_fld_parenttype";
        private const string PARM_PARENTNAME = "Parentname";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(AttachmentInfo item)
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
            if (!string.IsNullOrEmpty(item.PARENTDOI))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.PARENTDOI);
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.Type))
            {
                paramList.Add(PARM_TYPE);
                paramList.Add(item.Type);
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
            if (!string.IsNullOrEmpty(item.Sys_fld_filetype))
            {
                paramList.Add(PARM_SYS_FLD_FILETYPE);
                paramList.Add(item.Sys_fld_filetype);
            }
            if (item.Sys_fld_Adddate != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            {
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            }
            if (!string.IsNullOrEmpty(item.Audituser))
            {
                paramList.Add(PARM_AUDITUSER);
                paramList.Add(item.Audituser);
            }
            if (!string.IsNullOrEmpty(item.EditTool))
            {
                paramList.Add(PARM_EDITTOOL);
                paramList.Add(item.EditTool);
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (!string.IsNullOrEmpty(item.Parentname))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.Parentname);
            }
            paramList.Add(PARM_DEPARTMENT);
            paramList.Add(item.Department);

            if (!string.IsNullOrEmpty(item.Sys_fld_ChapterDoi))
            {
                paramList.Add(PARM_SYS_FLD_CHAPTERDOI);
                paramList.Add(item.Sys_fld_ChapterDoi);
            }

            paramList.Add(PARM_SYS_FLD_PARENTTYPE);
            paramList.Add(item.SYS_FLD_PARENTTYPE.ToString());


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
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(AttachmentInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();

            paramList.Add(PARM_PARENTDOI);
            paramList.Add(item.PARENTDOI);

            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.Parentname))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.Parentname);
            }
            paramList.Add(PARM_TYPE);
            paramList.Add(item.Type);

            paramList.Add(PARM_SYS_FLD_FILEPATH);
            paramList.Add(item.SYS_FLD_FILEPATH);

            paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
            paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);

            paramList.Add(PARM_SYS_FLD_FILETYPE);
            paramList.Add(item.Sys_fld_filetype);

            paramList.Add(PARM_SYS_FLD_ADDDATE);
            paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));


            paramList.Add(PARM_SYS_FLD_ADDUSER);
            paramList.Add(item.Sys_fld_Adduser);

            paramList.Add(PARM_AUDITUSER);
            paramList.Add(item.Audituser);

            paramList.Add(PARM_EDITTOOL);
            paramList.Add(item.EditTool);

            paramList.Add(PARM_REMARK);
            paramList.Add(item.Remark);

            paramList.Add(PARM_DEPARTMENT);
            paramList.Add(item.Department);

            paramList.Add(PARM_SYS_FLD_CHAPTERDOI);
            paramList.Add(item.Sys_fld_ChapterDoi);

            paramList.Add(PARM_SYS_FLD_PARENTTYPE);
            paramList.Add(item.SYS_FLD_PARENTTYPE.ToString());

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
        /// 得到一条记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public AttachmentInfo GetItem(string doi)
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
                AttachmentInfo entry = new AttachmentInfo();
                #region 判断字段并赋值
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.PARENTDOI = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Type = rs.GetValue(PARM_TYPE) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.Sys_fld_filetype = rs.GetValue(PARM_SYS_FLD_FILETYPE) ?? "";
                entry.SYS_SYSID = StructTrans.TransNum(rs.GetValue(PARM_SYS_SYSID));
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.Audituser = rs.GetValue(PARM_AUDITUSER) ?? "";
                entry.EditTool = rs.GetValue(PARM_EDITTOOL) ?? "";
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.Sys_fld_ChapterDoi = rs.GetValue(PARM_SYS_FLD_CHAPTERDOI) ?? "";
                entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";
                entry.SYS_FLD_PARENTTYPE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
                entry.Parentname = rs.GetValue(PARM_PARENTNAME) ?? "";
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
        /// 得到多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<AttachmentInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<AttachmentInfo> entryList = new List<AttachmentInfo>();
                AttachmentInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new AttachmentInfo();
                    #region 判断字段并赋值
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.PARENTDOI = rs.GetValue(PARM_PARENTDOI) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Type = rs.GetValue(PARM_TYPE) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.Sys_fld_filetype = rs.GetValue(PARM_SYS_FLD_FILETYPE) ?? "";
                    entry.SYS_SYSID = StructTrans.TransNum(rs.GetValue(PARM_SYS_SYSID));
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.Audituser = rs.GetValue(PARM_AUDITUSER) ?? "";
                    entry.EditTool = rs.GetValue(PARM_EDITTOOL) ?? "";
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Sys_fld_ChapterDoi = rs.GetValue(PARM_SYS_FLD_CHAPTERDOI) ?? "";
                    entry.Department = rs.GetValue(PARM_DEPARTMENT) ?? "";
                    entry.SYS_FLD_PARENTTYPE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_PARENTTYPE));
                    entry.Parentname = rs.GetValue(PARM_PARENTNAME) ?? "";
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
            //删除附录
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
