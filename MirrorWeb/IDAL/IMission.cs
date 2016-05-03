using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IMission
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="item">任务实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(MissionInfo Item);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id">任务的主键</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="item">任务实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(MissionInfo item);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">任务的主键</param>
        /// <returns>单个实体</returns>
        MissionInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<MissionInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">任务的主键</param>
        /// <param name="state">0未审核 -1审核通过</param>
        /// <returns></returns>
        bool SetState(string id, int state);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);

        /// <summary>
        /// 设置工作状态
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state">0是正在处理-1处理完成</param>
        /// <returns></returns>
        bool SetWorkState(string doi, int state);

        /// <summary>
        /// 设置工作状态
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state">0是正在处理-1处理完成</param>
        /// <param name="finishstate">0正常1驳回</param>
        /// <returns></returns>
        bool SetWorkState(string doi, int state,int finishstate);
    }
}
