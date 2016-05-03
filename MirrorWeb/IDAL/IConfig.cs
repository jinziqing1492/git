using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IConfig
    {
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="item">配置实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(ConfigInfo Item);

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="id">配置的主键</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="item">配置实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(ConfigInfo item);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">配置的主键</param>
        /// <returns>单个实体</returns>
        ConfigInfo GetItem(string id);

        /// <summary>
        /// 根据虚拟路径标示获取单个实体
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        ConfigInfo GetItemByTag(string tag);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<ConfigInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);
    }
}
