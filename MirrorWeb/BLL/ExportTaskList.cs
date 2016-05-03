using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class ExportTaskList
    {
        private static readonly IExportTaskList ReExportTaskList = SelectData.CreateExportTaskList();
        /// <summary>
        ///  添加数据导出任务的资源列表
        /// </summary>
        /// <param name="exporttasklist">数据导出任务的资源列表</param>
        /// <returns></returns>
        public bool Add(ExportTaskListInfo exporttasklist)
        {
            if (null == exporttasklist)
            {
                return false;
            }
            return ReExportTaskList.Add(exporttasklist);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">导出任务id</param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            //ExportTaskListInfo info = GetItem(id);
            //if (info == null)
            //{
            //    return false;
            //}

            return ReExportTaskList.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                return false;
            }
            return ReExportTaskList.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="exporttasklist"></param>
        /// <returns></returns>
        public bool Update(ExportTaskListInfo exporttasklist)
        {
            if (null == exporttasklist)
            {
                return false;
            }

            return ReExportTaskList.Update(exporttasklist);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">导出任务id</param>
        /// <returns></returns>
        public ExportTaskListInfo GetItem(string id)
        {
            return ReExportTaskList.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<ExportTaskListInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReExportTaskList.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReExportTaskList.GetCount(strWhere);
        }

    }
}
