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
    public class ResourceData:IResourceData
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["ResourceData"];
        #region IArticle 字段
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_NAME = "NAME";
        private const string PARM_CODE = "CODE";
        private const string PARM_AUTHOR = "AUTHOR";
        private const string PARM_PUBDEP = "PUBDEP";
        private const string PARM_PUBDATE = "PUBDATE";
        private const string PARM_PRICE = "PRICE";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_FILEPATH = "FILEPATH";
        private const string PARM_COVERPATH = "COVERPATH";
        private const string PARM_CATALOGPATH = "CATALOGPATH";
        private const string PARM_CREATETIME = "CREATETIME";
        private const string PARM_STATUS = "STATUS";
        private const string PARM_PARENTID = "PARENTID";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        public bool Add(ResourceDataInfo item)
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
            if (!string.IsNullOrEmpty(item.CODE))
            {
                paramList.Add(PARM_CODE);
                paramList.Add(item.CODE);
            }
            if (!string.IsNullOrEmpty(item.AUTHOR))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.AUTHOR);
            }
            if (!string.IsNullOrEmpty(item.PUBDEP))
            {
                paramList.Add(PARM_PUBDEP);
                paramList.Add(item.PUBDEP);
            }
            if (item.PUBDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.PUBDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.PRICE))
            {
                paramList.Add(PARM_PRICE);
                paramList.Add(item.PRICE);
            }
            if (!string.IsNullOrEmpty(item.DESCRIPTION))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.DESCRIPTION);
            }
            if (!string.IsNullOrEmpty(item.FILEPATH))
            {
                paramList.Add(PARM_FILEPATH);
                paramList.Add(item.FILEPATH);
            }
            if (!string.IsNullOrEmpty(item.COVERPATH))
            {
                paramList.Add(PARM_COVERPATH);
                paramList.Add(item.COVERPATH);
            }
            if (!string.IsNullOrEmpty(item.CATALOGPATH))
            {
                paramList.Add(PARM_CATALOGPATH);
                paramList.Add(item.CATALOGPATH);
            }
            if (item.CREATETIME != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATETIME);
                paramList.Add(item.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_STATUS);
            paramList.Add(item.STATUS.ToString());
            if (!string.IsNullOrEmpty(item.PARENTID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(item.PARENTID);
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
        public bool Update(ResourceDataInfo item)
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
            if (!string.IsNullOrEmpty(item.CODE))
            {
                paramList.Add(PARM_CODE);
                paramList.Add(item.CODE);
            }
            if (!string.IsNullOrEmpty(item.AUTHOR))
            {
                paramList.Add(PARM_AUTHOR);
                paramList.Add(item.AUTHOR);
            }
            if (!string.IsNullOrEmpty(item.PUBDEP))
            {
                paramList.Add(PARM_PUBDEP);
                paramList.Add(item.PUBDEP);
            }
            if (item.PUBDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_PUBDATE);
                paramList.Add(item.PUBDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.PRICE))
            {
                paramList.Add(PARM_PRICE);
                paramList.Add(item.PRICE);
            }
            if (!string.IsNullOrEmpty(item.DESCRIPTION))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.DESCRIPTION);
            }
            if (!string.IsNullOrEmpty(item.FILEPATH))
            {
                paramList.Add(PARM_FILEPATH);
                paramList.Add(item.FILEPATH);
            }
            if (!string.IsNullOrEmpty(item.COVERPATH))
            {
                paramList.Add(PARM_COVERPATH);
                paramList.Add(item.COVERPATH);
            }
            if (!string.IsNullOrEmpty(item.CATALOGPATH))
            {
                paramList.Add(PARM_CATALOGPATH);
                paramList.Add(item.CATALOGPATH);
            }
            if (item.CREATETIME != DateTime.MinValue)
            {
                paramList.Add(PARM_CREATETIME);
                paramList.Add(item.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            paramList.Add(PARM_STATUS);
            paramList.Add(item.STATUS.ToString());
            if (!string.IsNullOrEmpty(item.PARENTID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(item.PARENTID);
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
        public bool Update(ResourceDataInfo item, string[] fields)
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
                    case PARM_CODE: paramList.Add(PARM_CODE); paramList.Add(item.CODE); break;
                    case PARM_AUTHOR: paramList.Add(PARM_AUTHOR); paramList.Add(item.AUTHOR); break;
                    case PARM_PUBDEP: paramList.Add(PARM_PUBDEP); paramList.Add(item.PUBDEP); break;
                    case PARM_PUBDATE: paramList.Add(PARM_PUBDATE); paramList.Add(item.PUBDATE.ToString("yyyy-MM-dd HH:mm:ss")); break;
                    case PARM_PRICE: paramList.Add(PARM_PRICE); paramList.Add(item.PRICE); break;
                    case PARM_DESCRIPTION: paramList.Add(PARM_DESCRIPTION); paramList.Add(item.DESCRIPTION); break;
                    case PARM_FILEPATH: paramList.Add(PARM_FILEPATH); paramList.Add(item.FILEPATH); break;
                    case PARM_COVERPATH: paramList.Add(PARM_COVERPATH); paramList.Add(item.COVERPATH); break;
                    case PARM_CATALOGPATH: paramList.Add(PARM_CATALOGPATH); paramList.Add(item.CATALOGPATH); break;
                    case PARM_CREATETIME: paramList.Add(PARM_CREATETIME); paramList.Add(item.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss")); break;
                    case PARM_STATUS: paramList.Add(PARM_STATUS); paramList.Add(item.STATUS.ToString()); break;
                    case PARM_PARENTID: paramList.Add(PARM_PARENTID); paramList.Add(item.PARENTID); break;
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
        public ResourceDataInfo GetItem(string doi)
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
                ResourceDataInfo entry = new ResourceDataInfo();
                #region 判断字段并赋值
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                entry.CODE = rs.GetValue(PARM_CODE) ?? "";
                entry.AUTHOR = rs.GetValue(PARM_AUTHOR) ?? "";
                entry.PUBDEP = rs.GetValue(PARM_PUBDEP) ?? "";
                entry.PUBDATE = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                entry.PRICE = rs.GetValue(PARM_PRICE) ?? "";
                entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.FILEPATH = rs.GetValue(PARM_FILEPATH) ?? "";
                entry.COVERPATH = rs.GetValue(PARM_COVERPATH) ?? "";
                entry.CATALOGPATH = rs.GetValue(PARM_CATALOGPATH) ?? "";
                entry.CREATETIME = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME));
                entry.STATUS = StructTrans.TransNum(rs.GetValue(PARM_STATUS));
                entry.PARENTID = rs.GetValue(PARM_PARENTID) ?? "";
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
        public List<ResourceDataInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool isRed = false)
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
                List<ResourceDataInfo> entryList = new List<ResourceDataInfo>();
                ResourceDataInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ResourceDataInfo();
                    #region 判断字段并赋值
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.NAME = rs.GetValue(PARM_NAME) ?? "";
                    entry.CODE = rs.GetValue(PARM_CODE) ?? "";
                    entry.AUTHOR = rs.GetValue(PARM_AUTHOR) ?? "";
                    entry.PUBDEP = rs.GetValue(PARM_PUBDEP) ?? "";
                    entry.PUBDATE = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE));
                    entry.PRICE = rs.GetValue(PARM_PRICE) ?? "";
                    entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.FILEPATH = rs.GetValue(PARM_FILEPATH) ?? "";
                    entry.COVERPATH = rs.GetValue(PARM_COVERPATH) ?? "";
                    entry.CATALOGPATH = rs.GetValue(PARM_CATALOGPATH) ?? "";
                    entry.CREATETIME = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME));
                    entry.STATUS = StructTrans.TransNum(rs.GetValue(PARM_STATUS));
                    entry.PARENTID = rs.GetValue(PARM_PARENTID) ?? "";
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
        public List<ResourceDataInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, string[] fields, bool isRed = false)
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
                List<ResourceDataInfo> entryList = new List<ResourceDataInfo>();
                ResourceDataInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ResourceDataInfo();
                    #region 判断字段并赋值
                    foreach (string s in fields)
                    {
                        switch (s)
                        {
                            case PARM_SYS_FLD_DOI: entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? ""; break;
                            case PARM_NAME: entry.NAME = rs.GetValue(PARM_NAME) ?? ""; break;
                            case PARM_CODE: entry.CODE = rs.GetValue(PARM_CODE) ?? ""; break;
                            case PARM_AUTHOR: entry.AUTHOR = rs.GetValue(PARM_AUTHOR) ?? ""; break;
                            case PARM_PUBDEP: entry.PUBDEP = rs.GetValue(PARM_PUBDEP) ?? ""; break;
                            case PARM_PUBDATE: entry.PUBDATE = StructTrans.TransDate(rs.GetValue(PARM_PUBDATE)); break;
                            case PARM_PRICE: entry.PRICE = rs.GetValue(PARM_PRICE) ?? ""; break;
                            case PARM_DESCRIPTION: entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? ""; break;
                            case PARM_FILEPATH: entry.FILEPATH = rs.GetValue(PARM_FILEPATH) ?? ""; break;
                            case PARM_COVERPATH: entry.COVERPATH = rs.GetValue(PARM_COVERPATH) ?? ""; break;
                            case PARM_CATALOGPATH: entry.CATALOGPATH = rs.GetValue(PARM_CATALOGPATH) ?? ""; break;
                            case PARM_CREATETIME: entry.CREATETIME = StructTrans.TransDate(rs.GetValue(PARM_CREATETIME)); break;
                            case PARM_STATUS: entry.STATUS = StructTrans.TransNum(rs.GetValue(PARM_STATUS)); break;
                            case PARM_PARENTID: entry.PARENTID = rs.GetValue(PARM_PARENTID) ?? ""; break;
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
