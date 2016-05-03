using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IDepartment
    {
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        bool Add(DepartmentInfo dep);

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        bool Update(DepartmentInfo dep);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DepartmentInfo GetItem(string id);        

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="recordcount"></param>
        /// <returns></returns>
        List<DepartmentInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);
    }
}
