﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IConferenceArticle
    {
        /// <summary>
        /// 添加会议文章列表
        /// </summary>
        /// <param name="item">会议文章列表的实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(ConferenceArticleInfo Item);

        /// <summary>
        /// 删除会议文章列表
        /// </summary>
        /// <param name="id">会议文章列表的SYS_FLD_DOI</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新会议文章列表
        /// </summary>
        /// <param name="item">会议文章列表实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(ConferenceArticleInfo item);

        /// <summary>
        /// 增加点击数1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool AddHitCount(string id);

        /// <summary>
        /// 增加下载次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool AddStaticDownload(string id);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">会议文章列表的SYS_FLD_DOI</param>
        /// <returns>单个实体</returns>
        ConferenceArticleInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<ConferenceArticleInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">会议文章列表的SYS_FLD_DOI</param>
        /// <param name="state">0未审核 -1审核通过</param>
        /// <returns></returns>
        bool SetState(string id, int state);
        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool SetStateByWhere(string strWhere, int state); 

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">会议文章列表的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        bool SetIsOnline(string id, string isOnLine, string dateTime);

        /// <summary>
        /// 对论文的逻辑库id进行更改
        /// </summary>
        /// <param name="ldbid">逻辑库id</param>
        /// <param name="doi">父类的doi</param>
        /// <returns></returns>
        bool UpdateLDBID(string ldbid ,string doi);
    }
}
