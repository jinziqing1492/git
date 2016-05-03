using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IExportTaskList
    {
        /// <summary>
        /// 添加导出任务的资源列表项
        /// </summary>
        /// <param name="item">导出任务的信息</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(ExportTaskListInfo item);

        /// <summary>
        /// 删除导出任务资源列表项
        /// </summary>
        /// <param name="id">导出任务id</param>
        /// <returns></returns>
        bool Delete(string id);

        /// <summary>
        /// 更新导出任务资源列表项
        /// </summary>
        /// <param name="item">导出任务的信息</param>
        /// <returns></returns>
        bool Update(ExportTaskListInfo item);

        /// <summary>
        /// 根据thname即导出任务id获得一条记录
        /// </summary>
        /// <param name="thname">导出任务的id</param>
        /// <returns></returns>
        ExportTaskListInfo GetItem(string thname);

        /// <summary>
        /// 根据分页获得多条数据实例
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<ExportTaskListInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);
    }
}
