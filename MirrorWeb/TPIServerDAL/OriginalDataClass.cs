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
    public class OriginalDataClass : IOriginalDataClass
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["OriginalDataClass"];
        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_THEMENAME = "THEMENAME";
        private const string PARM_PARENTID = "PARENTID";
        private const string PARM_SOURCECODE = "SOURCECODE";
        private const string PARM_FILEFORMAT = "FILEFORMAT";
        private const string PARM_REMARK = "REMARK";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(OriginalDataClassInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.ThemeName))
            {
                paramList.Add(PARM_THEMENAME);
                paramList.Add(item.ThemeName);
            }
            if (!string.IsNullOrEmpty(item.ParentID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(item.ParentID);
            }
            if (!string.IsNullOrEmpty(item.SourceCode))
            {
                paramList.Add(PARM_SOURCECODE);
                paramList.Add(item.SourceCode);
            }
            if (!string.IsNullOrEmpty(item.FileFormat))
            {
                paramList.Add(PARM_FILEFORMAT);
                paramList.Add(item.FileFormat);
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
        public bool Update(OriginalDataClassInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.ThemeName))
            {
                paramList.Add(PARM_THEMENAME);
                paramList.Add(item.ThemeName);
            }
            //if (!string.IsNullOrEmpty(item.ParentID))
            //{
                paramList.Add(PARM_PARENTID);
                paramList.Add(item.ParentID);
            //}
            //if (!string.IsNullOrEmpty(item.SourceCode))
            //{
                paramList.Add(PARM_SOURCECODE);
                paramList.Add(item.SourceCode);
            //}
            //if (!string.IsNullOrEmpty(item.FileFormat))
            //{
                paramList.Add(PARM_FILEFORMAT);
                paramList.Add(item.FileFormat);
            //}
            //if (!string.IsNullOrEmpty(item.Remark))
            //{
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            //}
            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_ID + "='" + item.id + "'", paramList);
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
        public OriginalDataClassInfo GetItem(string id)
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
                OriginalDataClassInfo entry = new OriginalDataClassInfo();
                #region 判断字段并赋值
                entry.id = rs.GetValue(PARM_ID) ?? "";
                entry.ThemeName = rs.GetValue(PARM_THEMENAME) ?? "";
                entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                entry.FileFormat = rs.GetValue(PARM_FILEFORMAT) ?? "";
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

        /// <summary>
        /// 根据分页获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<OriginalDataClassInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<OriginalDataClassInfo> entryList = new List<OriginalDataClassInfo>();
                OriginalDataClassInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new OriginalDataClassInfo();
                    #region 判断字段并赋值
                    entry.id = rs.GetValue(PARM_ID) ?? "";
                    entry.ThemeName = rs.GetValue(PARM_THEMENAME) ?? "";
                    entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                    entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                    entry.FileFormat = rs.GetValue(PARM_FILEFORMAT) ?? "";
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
            //删除原始资料库分类
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
