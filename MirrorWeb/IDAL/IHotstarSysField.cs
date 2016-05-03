using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IHotstarSysField
    {
        /// <summary>
        /// 按分页获取数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        List<HotstarSysFieldInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 按分页获取数据，查询表中部分字段信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="fields"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        List<HotstarSysFieldInfo> GetListPartField(string sqlWhere, string fields, int pageNo, int pageCount, out int recordCount, bool IsAll);
    }
}
