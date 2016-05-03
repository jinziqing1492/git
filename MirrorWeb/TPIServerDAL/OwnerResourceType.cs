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
    public class OwnerResourceType : IOwnerResourceType
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["OwnerResourceType"];
        #region IArticle 字段
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_BASEID = "BASEID";
        private const string PARM_DATATYPE = "DATATYPE";
        private const string PARM_NAME = "NAME";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_DESCRIPT = "DESCRIPT";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_COVERPATH = "SYS_FLD_COVERPATH";
        
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        public bool Add(OwnerResourceTypeInfo item)
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
            paramList.Add(PARM_SYS_SYSID);
            paramList.Add(item.SYS_SYSID.ToString());
            if (!string.IsNullOrEmpty(item.DESCRIPT))
            {
                paramList.Add(PARM_DESCRIPT);
                paramList.Add(item.DESCRIPT);
            }
            if (item.SYS_FLD_ADDDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.SYS_FLD_ADDDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.BASEID))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            }
            paramList.Add(PARM_DATATYPE);
            paramList.Add(item.DATATYPE.ToString());
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
        public bool Update(OwnerResourceTypeInfo item)
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
            paramList.Add(PARM_SYS_SYSID);
            paramList.Add(item.SYS_SYSID.ToString());
            if (!string.IsNullOrEmpty(item.DESCRIPT))
            {
                paramList.Add(PARM_DESCRIPT);
                paramList.Add(item.DESCRIPT);
            }
            if (item.SYS_FLD_ADDDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.SYS_FLD_ADDDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.BASEID))
            {
                paramList.Add(PARM_BASEID);
                paramList.Add(item.BASEID);
            }
            paramList.Add(PARM_DATATYPE);
            paramList.Add(item.DATATYPE.ToString());
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
        public bool Update(OwnerResourceTypeInfo item, string[] fields)
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
                    case PARM_BASEID: paramList.Add(PARM_BASEID); paramList.Add(item.BASEID); break;
                    case PARM_DATATYPE: paramList.Add(PARM_DATATYPE); paramList.Add(item.DATATYPE.ToString()); break;
                    case PARM_NAME: paramList.Add(PARM_NAME); paramList.Add(item.NAME); break;
                    case PARM_SYS_SYSID: paramList.Add(PARM_SYS_SYSID); paramList.Add(item.SYS_SYSID.ToString()); break;
                    case PARM_DESCRIPT: paramList.Add(PARM_DESCRIPT); paramList.Add(item.DESCRIPT); break;
                    case PARM_SYS_FLD_ADDDATE: paramList.Add(PARM_SYS_FLD_ADDDATE); paramList.Add(item.SYS_FLD_ADDDATE.ToString("yyyy-MM-dd HH:mm:ss")); break;
                    case PARM_SYS_FLD_VIRTUALPATHTAG: paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG); paramList.Add(item.SYS_FLD_VIRTUALPATHTAG); break;
                    case PARM_SYS_FLD_COVERPATH: paramList.Add(PARM_SYS_FLD_COVERPATH); paramList.Add(item.SYS_FLD_COVERPATH); break;
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
        public OwnerResourceTypeInfo GetItem(string doi)
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
                OwnerResourceTypeInfo entry = new OwnerResourceTypeInfo();
                #region 判断字段并赋值
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                entry.DATATYPE = StructTrans.TransNum(rs.GetValue(PARM_DATATYPE));
                entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                entry.SYS_SYSID = StructTrans.TransNum(rs.GetValue(PARM_SYS_SYSID));
                entry.DESCRIPT = rs.GetValue(PARM_DESCRIPT) ?? "";
                entry.SYS_FLD_ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
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
        public OwnerResourceTypeInfo GetItemByBaseID(string baseid)
        {
            if (string.IsNullOrEmpty(baseid))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_BASEID, baseid);
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
                OwnerResourceTypeInfo entry = new OwnerResourceTypeInfo();
                #region 判断字段并赋值
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                entry.DATATYPE = StructTrans.TransNum(rs.GetValue(PARM_DATATYPE));
                entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                entry.SYS_SYSID = StructTrans.TransNum(rs.GetValue(PARM_SYS_SYSID));
                entry.DESCRIPT = rs.GetValue(PARM_DESCRIPT) ?? "";
                entry.SYS_FLD_ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
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
        public List<OwnerResourceTypeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool isRed = false)
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
                List<OwnerResourceTypeInfo> entryList = new List<OwnerResourceTypeInfo>();
                OwnerResourceTypeInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new OwnerResourceTypeInfo();
                    #region 判断字段并赋值
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.BASEID = rs.GetValue(PARM_BASEID) ?? "";
                    entry.DATATYPE = StructTrans.TransNum(rs.GetValue(PARM_DATATYPE));
                    entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                    entry.SYS_SYSID = StructTrans.TransNum(rs.GetValue(PARM_SYS_SYSID));
                    entry.DESCRIPT = rs.GetValue(PARM_DESCRIPT) ?? "";
                    entry.SYS_FLD_ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? "";
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
        public List<OwnerResourceTypeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, string[] fields, bool isRed = false)
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
                List<OwnerResourceTypeInfo> entryList = new List<OwnerResourceTypeInfo>();
                OwnerResourceTypeInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new OwnerResourceTypeInfo();
                    #region 判断字段并赋值
                    foreach (string s in fields)
                    {
                        switch (s)
                        {
                            case PARM_SYS_FLD_DOI: entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? ""; break;
                            case PARM_BASEID: entry.BASEID = rs.GetValue(PARM_BASEID) ?? ""; break;
                            case PARM_DATATYPE:entry.DATATYPE=StructTrans.TransNum(rs.GetValue(PARM_DATATYPE));break;
                            case PARM_NAME: entry.NAME = rs.GetValue(PARM_NAME) ?? ""; break;
                            case PARM_SYS_SYSID: entry.SYS_SYSID = StructTrans.TransNum(rs.GetValue(PARM_SYS_SYSID)); break;
                            case PARM_DESCRIPT: entry.DESCRIPT = rs.GetValue(PARM_DESCRIPT) ?? ""; break;
                            case PARM_SYS_FLD_ADDDATE: entry.SYS_FLD_ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE)); break;
                            case PARM_SYS_FLD_VIRTUALPATHTAG: entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? ""; break;
                            case PARM_SYS_FLD_COVERPATH: entry.SYS_FLD_COVERPATH = rs.GetValue(PARM_SYS_FLD_COVERPATH) ?? ""; break;
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
