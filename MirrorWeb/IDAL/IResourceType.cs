using DRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.IDAL
{
    public interface IResourceType
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name='item'>实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(ResourceTypeInfo Item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name='id'>id</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name='strWhere'>strWhere</param>
        /// <returns>true 成功； false 失败</returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name='item'>实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(ResourceTypeInfo item);

        /// <summary>
        /// 更新记录 可以指定字段
        /// </summary>
        /// <param name='item'>实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(ResourceTypeInfo item, string[] fields);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name='id'>id</param>
        /// <returns>true 成功； false 失败</returns>
        ResourceTypeInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name='sqlWhere'>语句的查询条件</param>
        /// <param name='pageNo'>分页查询的页码（第一页为1）<param>
        /// <param name='pageCount'>每页的记录数</param>
        /// <param name='recordCount'>返回的总记录数</param>
        /// <param name='IsAll'>是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<ResourceTypeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool isRed = false);

        /// <summary>
        /// 获取实体列表 可指定字段
        /// </summary>
        /// <param name='sqlWhere'>语句的查询条件</param>
        /// <param name='pageNo'>分页查询的页码（第一页为1）<param>
        /// <param name='pageCount'>每页的记录数</param>
        /// <param name='recordCount'>返回的总记录数</param>
        /// <param name='IsAll'>是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<ResourceTypeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, string[] fields, bool isRed = false);


    }
}
