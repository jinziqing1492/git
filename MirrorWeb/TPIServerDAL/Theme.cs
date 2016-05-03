using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

using DRMS.Model;
using DRMS.IDAL;
using CNKI.BaseFunction;
using TPI;

namespace DRMS.TPIServerDAL
{
    public class Theme : ITheme
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Theme"];

        private static string TABLE_NAME_KSPIDER = ConfigurationManager.AppSettings["EpmTheme"];

        private static string DataBaseInfo = ConfigurationManager.AppSettings["kspiderserver"];
        private static string KspiderLocal = ConfigurationManager.AppSettings["kspiderlocal"];

        #region IArticle 字段
        private const string PARM_ID = "ID";
        private const string PARM_THEMENAME = "THEMENAME";
        private const string PARM_PARENTID = "PARENTID";
        private const string PARM_SOURCECODE = "SOURCECODE";
        private const string PARM_ORDERNUM = "ORDERNUM";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_HASCHILD = "HASCHILD";
        private const string PARM_ZTCODE = "ZTCODE";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(ThemeInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();

            if (string.IsNullOrEmpty(item.ParentID))
            {
                return false;
            }
            item.ID = GetThemeId(TABLE_NAME, PARM_PARENTID, item.ParentID, PARM_ID);
            if (string.IsNullOrEmpty(item.ID))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(item.ID))
            {
                paramList.Add(PARM_ID);
                paramList.Add(item.ID);
            }
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
            if (!string.IsNullOrEmpty(item.Ordernum))
            {
                paramList.Add(PARM_ORDERNUM);
                paramList.Add(item.Ordernum);
            }
            if (!string.IsNullOrEmpty(item.remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.remark);
            }
            paramList.Add(PARM_HASCHILD);
            paramList.Add(item.HASCHILD.ToString());
            if (!string.IsNullOrEmpty(item.ZTCode))
            {
                paramList.Add(PARM_ZTCODE);
                paramList.Add(item.ZTCode);
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
        public bool Update(ThemeInfo item)
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
            //if (!string.IsNullOrEmpty(item.Ordernum))
            //{
                paramList.Add(PARM_ORDERNUM);
                paramList.Add(item.Ordernum);
            //}
            //if (!string.IsNullOrEmpty(item.remark))
            //{
                paramList.Add(PARM_REMARK);
                paramList.Add(item.remark);
            //}
            paramList.Add(PARM_HASCHILD);
            paramList.Add(item.HASCHILD.ToString());
            if (!string.IsNullOrEmpty(item.ZTCode))
            {
                paramList.Add(PARM_ZTCODE);
                paramList.Add(item.ZTCode);
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
        public ThemeInfo GetItem(string id)
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
                ThemeInfo entry = new ThemeInfo();
                #region 判断字段并赋值
                entry.ID = rs.GetValue(PARM_ID) ?? "";
                entry.ThemeName = rs.GetValue(PARM_THEMENAME) ?? "";
                entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                entry.Ordernum = rs.GetValue(PARM_ORDERNUM) ?? "";
                entry.remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.HASCHILD = StructTrans.TransNum( rs.GetValue(PARM_HASCHILD));
                entry.ZTCode = rs.GetValue(PARM_ZTCODE) ?? "";
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
        public List<ThemeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<ThemeInfo> entryList = new List<ThemeInfo>();
                ThemeInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ThemeInfo();
                    #region 判断字段并赋值
                    entry.ID = rs.GetValue(PARM_ID) ?? "";
                    entry.ThemeName = rs.GetValue(PARM_THEMENAME) ?? "";
                    entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                    entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                    entry.Ordernum = rs.GetValue(PARM_ORDERNUM) ?? "";
                    entry.remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.HASCHILD = StructTrans.TransNum(rs.GetValue(PARM_HASCHILD));
                    entry.ZTCode = rs.GetValue(PARM_ZTCODE) ?? "";
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
            //删除分类
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

        /// <summary>
        /// 从蜘蛛中获取新闻的分类
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        public List<ThemeInfo> GetListKspider(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            recordCount = 0;
            RecordSet rs = null;
            //判断是将表中所有字段取出还是只取常用的一部分
            if (KspiderLocal == "1")
            {
                rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME_KSPIDER, sqlWhere);
            }
            else
            {
                //获取链接
                Client client = ClientAnalyse.GetClient(DataBaseInfo);
                if (null == client)
                {
                    return null;
                }
                rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME_KSPIDER, sqlWhere, client);
            }
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
            //   rs.SetHitWordMarkFlag(RED_LEFT, RED_RIGHT);
            //  获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<ThemeInfo> entryList = new List<ThemeInfo>();
                ThemeInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ThemeInfo();
                    #region 判断字段并赋值
                    if (IsAll)
                    {
                        entry.ID = rs.GetValue(PARM_ID) ?? "";
                        entry.ThemeName = rs.GetValue(PARM_THEMENAME) ?? "";
                        entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                        entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                        entry.remark = rs.GetValue(PARM_REMARK) ?? "";
                        entry.ZTCode = rs.GetValue(PARM_ZTCODE) ?? "";
                        entry.HASCHILD = StructTrans.TransNum(rs.GetValue(PARM_HASCHILD) ?? "0");
                    }
                    else
                    {
                        //TODO:暂无，等开发时根据使用情况加上
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

        /// <summary>
        /// 获取待添加分类的ID
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="parentidcolumn">parentid字段的名称</param>
        /// <param name="parentidvalue">parentid字段的值</param>
        /// <param name="themeidcolumn">ID字段的名称</param>
        /// <returns></returns>
        public static string GetThemeId(string tablename, string parentidcolumn , string parentidvalue, string themeidcolumn)
        {
            if (string.IsNullOrEmpty(tablename))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(parentidcolumn))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(parentidvalue))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(themeidcolumn))
            {
                return string.Empty;
            }
            string mythemeid;
            string themesql = "SELECT * FROM " + tablename + " WHERE " + parentidcolumn + "='" + parentidvalue + "'";
            RecordSet rs = TPIHelper.GetRecordSet(themesql);
            if (null == rs)
            {
                return string.Empty;
            }
            if (parentidvalue == "0")
            {
                parentidvalue = "";
            }
            if (rs.GetCount() <= 0)
            {
                mythemeid = parentidvalue + "1.";
            }
            else
            {
                int maxid = 0;
                for (int i = 0; i < rs.GetCount(); i++)
                {
                    string themeid = rs.GetValue(themeidcolumn);
                    if (!string.IsNullOrEmpty(themeid))
                    {
                        int thisid = StructTrans.TransNum(Regex.Replace(themeid, @"^.*\.(?<num>[^\.]+)\.$", "${num}", RegexOptions.IgnoreCase));
                        if (thisid > maxid)
                        {
                            maxid = thisid;
                        }
                    }
                    if (!rs.MoveNext())
                    {
                        break;
                    }
                }
                if (maxid == 0)
                {
                    mythemeid = parentidvalue + "1.";
                }
                else
                {
                    maxid++;
                    mythemeid = parentidvalue + maxid.ToString() + ".";
                }
            }
            rs.Close();
            return mythemeid;
        }
    }
}
