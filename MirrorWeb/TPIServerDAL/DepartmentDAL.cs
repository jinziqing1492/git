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
    /// <summary>
    /// 部门
    /// </summary>
    public class DepartmentDAL : IDepartment
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Department"].ToString();
        #region IArticle 字段
        private const string PARM_ID = "Id";
        private const string PARM_NAME = "NAME";
        private const string PARM_PARENTID = "PARENTID";
        private const string PARM_SOURCECODE = "SOURCECODE";
        private const string PARM_ORDERNUM = "ORDERNUM";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_HASCHILD = "HASCHILD";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
                
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public bool Add(DepartmentInfo dep)
        {
            if (dep == null)
            {
                return false;
            }

            //生成部门id
            if (string.IsNullOrEmpty(dep.ParentID))
            {
                return false;
            }
            dep.Id = GetDepartmentId(TABLE_NAME, PARM_PARENTID, dep.ParentID, PARM_ID);
            if (string.IsNullOrEmpty(dep.Id))
            {
                return false;
            }

            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(dep.Id))
            {
                paramList.Add(PARM_ID);
                paramList.Add(dep.Id);
            }

            if (!string.IsNullOrEmpty(dep.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(dep.Name);
            }
            if (!string.IsNullOrEmpty(dep.ParentID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(dep.ParentID);
            }
            if (!string.IsNullOrEmpty(dep.SourceCode))
            {
                paramList.Add(PARM_SOURCECODE);
                paramList.Add(dep.SourceCode);
            }
            if (!string.IsNullOrEmpty(dep.Ordernum))
            {
                paramList.Add(PARM_ORDERNUM);
                paramList.Add(dep.Ordernum);
            }
            if (!string.IsNullOrEmpty(dep.remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(dep.remark);
            }
            paramList.Add(PARM_HASCHILD);
            paramList.Add(dep.HASCHILD.ToString());
            
            #endregion

            return TPIHelper.Insert(TABLE_NAME, paramList);
        }
        /// <summary>
        /// 删除部门
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
        /// 更新部门信息
        /// </summary>
        /// <param name="Department"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Update(DepartmentInfo dep)
        {
            if (dep == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(dep.Id))
            {
                paramList.Add(PARM_ID);
                paramList.Add(dep.Id);
            }

            if (!string.IsNullOrEmpty(dep.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(dep.Name);
            }
            if (!string.IsNullOrEmpty(dep.ParentID))
            {
                paramList.Add(PARM_PARENTID);
                paramList.Add(dep.ParentID);
            }
            if (!string.IsNullOrEmpty(dep.SourceCode))
            {
                paramList.Add(PARM_SOURCECODE);
                paramList.Add(dep.SourceCode);
            }
            if (!string.IsNullOrEmpty(dep.Ordernum))
            {
                paramList.Add(PARM_ORDERNUM);
                paramList.Add(dep.Ordernum);
            }
            if (!string.IsNullOrEmpty(dep.remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(dep.remark);
            }
            paramList.Add(PARM_HASCHILD);
            paramList.Add(dep.HASCHILD.ToString());

            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_ID + "='" + dep.Id + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取单个部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DepartmentInfo GetItem(string id)
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
                DepartmentInfo entry = new DepartmentInfo();
                #region 判断字段并赋值
                entry.Id = rs.GetValue(PARM_ID) ?? "";
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                entry.Ordernum = rs.GetValue(PARM_ORDERNUM) ?? "";
                entry.remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.HASCHILD = StructTrans.TransNum( rs.GetValue(PARM_HASCHILD));
                
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
        /// 获取多个部门信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="recordcount"></param>
        /// <returns></returns>
        public List<DepartmentInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<DepartmentInfo> entryList = new List<DepartmentInfo>();
                DepartmentInfo entry;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new DepartmentInfo();
                    #region
                    entry.Id = rs.GetValue(PARM_ID) ?? "";
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.ParentID = rs.GetValue(PARM_PARENTID) ?? "";
                    entry.SourceCode = rs.GetValue(PARM_SOURCECODE) ?? "";
                    entry.Ordernum = rs.GetValue(PARM_ORDERNUM) ?? "";
                    entry.remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.HASCHILD = StructTrans.TransNum(rs.GetValue(PARM_HASCHILD));

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
            //删除部门信息
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
        /// 获取待添加分类的ID
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="parentidcolumn">parentid字段的名称</param>
        /// <param name="parentidvalue">parentid字段的值</param>
        /// <param name="DepartmentIdcolumn">ID字段的名称</param>
        /// <returns></returns>
        public static string GetDepartmentId(string tablename, string parentidcolumn, string parentidvalue, string DepartmentIdcolumn)
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
            if (string.IsNullOrEmpty(DepartmentIdcolumn))
            {
                return string.Empty;
            }
            string mydepartmentid;
            string departmentsql = "SELECT * FROM " + tablename + " WHERE " + parentidcolumn + "='" + parentidvalue + "'";
            RecordSet rs = TPIHelper.GetRecordSet(departmentsql);
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
                mydepartmentid = parentidvalue + "1.";
            }
            else
            {
                int maxid = 0;
                for (int i = 0; i < rs.GetCount(); i++)
                {
                    string departmentid = rs.GetValue(DepartmentIdcolumn);
                    if (!string.IsNullOrEmpty(departmentid))
                    {
                        int thisid = StructTrans.TransNum(Regex.Replace(departmentid, @"^.*\.(?<num>[^\.]+)\.$", "${num}", RegexOptions.IgnoreCase));
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
                    mydepartmentid = parentidvalue + "1.";
                }
                else
                {
                    maxid++;
                    mydepartmentid = parentidvalue + maxid.ToString() + ".";
                }
            }
            rs.Close();
            return mydepartmentid;
        }

    }
}
