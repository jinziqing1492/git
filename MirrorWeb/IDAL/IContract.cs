﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IContract
    {
        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="item">合同实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(ContractInfo Item);

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="id">合同的SYS_FLD_DOI</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="item">合同实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(ContractInfo item);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">合同的SYS_FLD_DOI</param>
        /// <returns>单个实体</returns>
        ContractInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<ContractInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);
    }
}