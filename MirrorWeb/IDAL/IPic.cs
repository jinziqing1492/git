﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IPic
    {
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="item">图片实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Add(PicInfo Item);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">图片的SYS_FLD_DOI</param>
        /// <returns>true 成功； false 失败</returns>
        bool Delete(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool DeleteByWhere(string strWhere);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="item">图片实体</param>
        /// <returns>true 成功； false 失败</returns>
        bool Update(PicInfo item);

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
        /// <param name="id">图片的SYS_FLD_DOI</param>
        /// <returns>单个实体</returns>
        PicInfo GetItem(string id);

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        List<PicInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        List<PicInfo> GetList_NoFirst(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll);

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">图片的SYS_FLD_DOI</param>
        /// <param name="state">0未审核 -1审核通过</param>
        /// <returns></returns>
        bool SetState(string id, int state);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool SetStateByWhere(string sqlWhere, int state);

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        int GetCount(string sqlWhere);

        /// <summary>
        /// 获取图片分组ID，图片详情页面使用，用于当前分组的图片显示完后，显示下一分组（或上一分组）的图片
        /// </summary>
        /// <param name="sqlwhere">查询限制条件</param>
        /// <param name="field">分组标识</param>
        /// <returns></returns>
        List<PicInfo> GetGroupID(string sqlwhere, string field);
    }
}
