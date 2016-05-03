using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using DRMS.Model;
using DRMS.IDAL;
using CNKI.BaseFunction;
using TPI;
namespace DRMS.TPIServerDAL
{
    public class ControlCss:IControlCss
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["ControlCss"];
        #region 字段
        private const string PARM_ID = "ID";
        private const string PARM_NAME = "NAME";
        private const string PARM_CSSPARAM = "CSSPARAM";
        private const string PARM_TYPE = "TYPE";
        private const string PARM_CSSNAME = "CSSNAME";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ControlCssInfo item)
        {
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.ID))
            {
                paramList.Add(PARM_ID);
                paramList.Add(item.ID);
            }
            if (!string.IsNullOrEmpty(item.CssName))
            {
                paramList.Add(PARM_CSSNAME);
                paramList.Add(item.CssName);
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
            if (!string.IsNullOrEmpty(item.CssParam))
            {
                paramList.Add(PARM_CSSPARAM);
                paramList.Add(item.CssParam);
            }
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
        public bool Update(ControlCssInfo item)
        {
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.ID))
            {
                paramList.Add(PARM_ID);
                paramList.Add(item.ID);
            }
            if (!string.IsNullOrEmpty(item.CssName))
            {
                paramList.Add(PARM_CSSNAME);
                paramList.Add(item.CssName);
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
            if (!string.IsNullOrEmpty(item.CssParam))
            {
                paramList.Add(PARM_CSSPARAM);
                paramList.Add(item.CssParam);
            }
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
        public ControlCssInfo GetItem(string id)
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
                ControlCssInfo entry = new ControlCssInfo();
                #region 判断字段并赋值
                entry.ID = rs.GetValue(PARM_ID) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.CssName = rs.GetValue(PARM_CSSNAME) ?? "";
                entry.CssParam = rs.GetValue(PARM_CSSPARAM) ?? "";
                entry.Type = rs.GetValue(PARM_TYPE) ?? "";
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
        public List<ControlCssInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ControlCssInfo> entryList = new List<ControlCssInfo>();
                ControlCssInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ControlCssInfo();
                    #region 判断字段并赋值
                    entry.ID = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.CssName = rs.GetValue(PARM_CSSNAME) ?? "";
                    entry.CssParam = rs.GetValue(PARM_CSSPARAM) ?? "";
                    entry.Type = rs.GetValue(PARM_TYPE) ?? "";

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
        /// 根据条件获得记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
