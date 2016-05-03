using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class Department
    {
        private static readonly IDepartment ReDepartment = SelectData.CreateDepartment();
                     
        
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public bool Add(DepartmentInfo dep)
        {
            if (null == dep)
                return false;
            try
            {
                return ReDepartment.Add(dep);
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Department"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Update(DepartmentInfo dep)
        {
            if (null == dep)
                return false;
            try
            {
                return ReDepartment.Update(dep);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            DepartmentInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReDepartment.Delete(id);
            
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                return false;
            }
            return ReDepartment.DeleteByWhere(strWhere);
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DepartmentInfo GetItem(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            try
            {
                return ReDepartment.GetItem(id);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="strwhere">符合规则的SQL串</param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="recordcount"></param>
        /// <returns></returns>
        public List<DepartmentInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReDepartment.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 获取所有的根节点
        /// </summary>
        /// <returns></returns>
        public IList<DepartmentInfo> GetRootDepartments()
        {
            string sqlWhere = " PARENTID=0 or PARENTID is null order by ORDERNUM asc";
            int recordCount = 0;
            IList<DepartmentInfo> lstDi = GetList(sqlWhere, 1, 1000, out recordCount, true);
            if (recordCount > 1000)
            {
                lstDi = GetList(sqlWhere, 1, recordCount, out recordCount, true);
            }
            return lstDi;
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <returns></returns>
        public IList<DepartmentInfo> GetSubThemes(string parentId)
        {
            string sqlWhere = " PARENTID='" + parentId + "' order by ORDERNUM asc";
            int recordCount = 0;
            IList<DepartmentInfo> lstDi = GetList(sqlWhere, 1, 1000, out recordCount, true);
            if (recordCount > 1000)
                lstDi = GetList(sqlWhere, 1, recordCount, out recordCount, true);
            return lstDi;
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReDepartment.GetCount(strWhere);
        }

        /// <summary>
        /// 根据条件获取子节点记录数
        /// </summary>
        public int GetChildCount(string id)
        {
            string sqlWhere = " PARENTID='" + id + "'";
            return ReDepartment.GetCount(sqlWhere);
        }
    }
}
