using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
   public interface IZTFlfCls
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
       List<ZTFlfClsInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

       /// <summary>
       /// 根据条件获得记录条数
       /// </summary>
       /// <param name="sqlWhere"></param>
       /// <returns></returns>
       int GetCount(string sqlWhere);
       
    }
}
