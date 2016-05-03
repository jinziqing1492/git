using CNKI.BaseFunction;
using DRMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TPI;
using DRMS.IDAL;

namespace DRMS.TPIServerDAL
{
    public class ResourceType:IResourceType
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["ResourceType"];
        #region IArticle 字段
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_NAME = "NAME";
        private const string PARM_PARENTID = "PARENTID";
        private const string PARM_COVERPATH = "COVERPATH";
        private const string PARM_CREATETIME = "CREATETIME";
        private const string PARM_REMARK = "REMARK";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        public bool Add(ResourceTypeInfo item)
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
            if (!string.IsNullOrEmpty(item.NAME))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.NAME);
            }
            if (!string.IsNullOrEmpty(item.PARENTID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(item.PARENTID);
            }
            if (!string.IsNullOrEmpty(item.COVERPATH))
            {
                paramList.Add(PARM_COVERPATH);
                paramList.Add(item.COVERPATH);
            }
            if (item.CREATETIME != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATETIME);
                paramList.Add(item.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
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
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, id);
            return TPIHelper.ExecSql(sqlDelete);
        }
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrWhiteSpace(strWhere))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }
        public bool Update(ResourceTypeInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.NAME))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.NAME);
            }
            if (!string.IsNullOrEmpty(item.PARENTID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(item.PARENTID);
            }
            if (!string.IsNullOrEmpty(item.COVERPATH))
            {
                paramList.Add(PARM_COVERPATH);
                paramList.Add(item.COVERPATH);
            }
            if (item.CREATETIME != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATETIME);
                paramList.Add(item.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
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
        public bool Update(ResourceTypeInfo item, string[] fields)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            foreach (string field in fields)
            {
                switch (field)
                {
                    case PARM_NAME: paramList.Add(PARM_NAME); paramList.Add(item.NAME); break;
                    case PARM_PARENTID: paramList.Add(PARM_PARENTID); paramList.Add(item.PARENTID); break;
                    case PARM_COVERPATH: paramList.Add(PARM_COVERPATH); paramList.Add(item.COVERPATH); break;
                    case PARM_CREATETIME: paramList.Add(PARM_CREATETIME); paramList.Add(item.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss")); break;
                    case PARM_REMARK: paramList.Add(PARM_REMARK); paramList.Add(item.Remark); break;
                }
            }
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
        public ResourceTypeInfo GetItem(string doi)
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
                ResourceTypeInfo entry = new ResourceTypeInfo();
                #region 判断字段并赋值
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                entry.PARENTID = rs.GetValue(PARM_PARENTID) ?? "";
                entry.COVERPATH = rs.GetValue(PARM_COVERPATH) ?? "";
                entry.CREATETIME = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME));
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
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
        public List<ResourceTypeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool isRed = false)
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
            //获取总得记录数
            recordCount = rs.GetCount();
            if (isRed)
                rs.SetHitWordMarkFlag(RED_LEFT, RED_RIGHT);
            //获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<ResourceTypeInfo> entryList = new List<ResourceTypeInfo>();
                ResourceTypeInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ResourceTypeInfo();
                    #region 判断字段并赋值
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                    entry.PARENTID = rs.GetValue(PARM_PARENTID) ?? "";
                    entry.COVERPATH = rs.GetValue(PARM_COVERPATH) ?? "";
                    entry.CREATETIME = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME));
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
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
        public List<ResourceTypeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, string[] fields, bool isRed = false)
        {
            recordCount = 0;
            if (fields == null || fields.Length == 0)
            {
                return null;
            }
            string fieldStr = "";
            foreach (string field in fields)
            {
                fieldStr += field + ",";
            }
            fieldStr = fieldStr.Remove(fieldStr.Length - 1);
            RecordSet rs = TPIHelper.GetRecordPartField(TABLE_NAME, sqlWhere, fieldStr);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            //获取总得记录数
            recordCount = rs.GetCount();
            if (isRed)
                rs.SetHitWordMarkFlag(RED_LEFT, RED_RIGHT);
            //获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<ResourceTypeInfo> entryList = new List<ResourceTypeInfo>();
                ResourceTypeInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ResourceTypeInfo();
                    #region 判断字段并赋值
                    foreach (string s in fields)
                    {
                        switch (s)
                        {
                            case PARM_SYS_FLD_DOI: entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? ""; break;
                            case PARM_NAME: entry.NAME = rs.GetValue(PARM_NAME) ?? ""; break;
                            case PARM_PARENTID: entry.PARENTID = rs.GetValue(PARM_PARENTID) ?? ""; break;
                            case PARM_COVERPATH: entry.COVERPATH = rs.GetValue(PARM_COVERPATH) ?? ""; break;
                            case PARM_CREATETIME: entry.CREATETIME = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME)); break;
                            case PARM_REMARK: entry.Remark = rs.GetValue(PARM_REMARK) ?? ""; break;
                        }
                    }
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

    }
}
