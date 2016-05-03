﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IJournalInfo
    {
        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="type"></param>
        void SetTableName(string type);

        /// <summary>
        /// 添加期刊
        /// </summary>
        /// <param name="item">期刊实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(JournalInfo Item);

        /// <summary>
        /// 删除期刊
        /// </summary>
        /// <param name="id">期刊的主键</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新期刊
        /// </summary>
        /// <param name="item">期刊实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(JournalInfo item);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">期刊主键</param>
        /// <returns>单个实体</returns>
        JournalInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<JournalInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">期刊的SYS_FLD_DOI</param>
        /// <param name="state">0未审核 -1审核通过</param>
        /// <returns></returns>
        bool SetState(string id, int state);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);
    }
}
