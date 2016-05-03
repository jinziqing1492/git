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
    public class Config : IConfig
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Config"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_ROOTDIR = "ROOTDIR";
        private const string PARM_VIRTUALPATHTAG = "VIRTUALPATHTAG";
        private const string PARM_VIRTUALPATHNAME = "VIRTUALPATHNAME";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ConfigInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.RootDir))
            {
                paramList.Add(PARM_ROOTDIR);
                paramList.Add(item.RootDir);
            }
            if (!string.IsNullOrEmpty(item.VirtualPathTag))
            {
                paramList.Add(PARM_VIRTUALPATHTAG);
                paramList.Add(item.VirtualPathTag);
            }
            if (!string.IsNullOrEmpty(item.VirtualPathName))
            {
                paramList.Add(PARM_VIRTUALPATHNAME);
                paramList.Add(item.VirtualPathName);
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
        public bool Update(ConfigInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.RootDir))
            {
                paramList.Add(PARM_ROOTDIR);
                paramList.Add(item.RootDir);
            }
            if (!string.IsNullOrEmpty(item.VirtualPathTag))
            {
                paramList.Add(PARM_VIRTUALPATHTAG);
                paramList.Add(item.VirtualPathTag);
            }
            if (!string.IsNullOrEmpty(item.VirtualPathName))
            {
                paramList.Add(PARM_VIRTUALPATHNAME);
                paramList.Add(item.VirtualPathName);
            }
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
        public ConfigInfo GetItem(string id)
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
                ConfigInfo entry = new ConfigInfo();
                #region 判断字段并赋值
                entry.ID = StructTrans.TransNum(rs.GetValue(PARM_ID));
                entry.RootDir = rs.GetValue(PARM_ROOTDIR) ?? "";
                entry.VirtualPathTag = rs.GetValue(PARM_VIRTUALPATHTAG) ?? "";
                entry.VirtualPathName = rs.GetValue(PARM_VIRTUALPATHNAME) ?? "";
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
        /// 根据虚拟路径标示获取单个实体
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ConfigInfo GetItemByTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_VIRTUALPATHTAG, tag);
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
                ConfigInfo entry = new ConfigInfo();
                #region 判断字段并赋值
                entry.ID = StructTrans.TransNum(rs.GetValue(PARM_ID));
                entry.RootDir = rs.GetValue(PARM_ROOTDIR) ?? "";
                entry.VirtualPathTag = rs.GetValue(PARM_VIRTUALPATHTAG) ?? "";
                entry.VirtualPathName = rs.GetValue(PARM_VIRTUALPATHNAME) ?? "";
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
        public List<ConfigInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ConfigInfo> entryList = new List<ConfigInfo>();
                ConfigInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ConfigInfo();
                    #region 判断字段并赋值
                    entry.ID = StructTrans.TransNum(rs.GetValue(PARM_ID));
                    entry.RootDir = rs.GetValue(PARM_ROOTDIR) ?? "";
                    entry.VirtualPathTag = rs.GetValue(PARM_VIRTUALPATHTAG) ?? "";
                    entry.VirtualPathName = rs.GetValue(PARM_VIRTUALPATHNAME) ?? "";
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
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件得到记录的总数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
