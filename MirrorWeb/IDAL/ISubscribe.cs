﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface ISubscribe
    {
        /// <summary>
        /// 添加订阅信息
        /// </summary>
        /// <param name="item">订阅信息实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(SubscribeInfo Item);

        /// <summary>
        /// 删除订阅信息
        /// </summary>
        /// <param name="id">订阅信息的主体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新订阅信息
        /// </summary>
        /// <param name="item">订阅信息实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(SubscribeInfo item);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">订阅信息的主体</param>
        /// <returns>单个实体</returns>
        SubscribeInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<SubscribeInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);
    }
}
