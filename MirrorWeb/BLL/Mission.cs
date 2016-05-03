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
    public class Mission
    {
        private static readonly IMission ReMission = SelectData.CreateMission();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(MissionInfo mission)
        {
            if (null == mission)
            {
                return false;
            }
            return ReMission.Add(mission);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            MissionInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReMission.Delete(id);
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
            return ReMission.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(MissionInfo mission)
        {
            if (null == mission)
            {
                return false;
            }

            return ReMission.Update(mission);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MissionInfo GetItem(string id)
        {
            return ReMission.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<MissionInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReMission.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">任务的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id) || (state != 0 && state != -1))
            {
                return false;
            }
            MissionInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReMission.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReMission.GetCount(strWhere);
        }

        /// <summary>
        /// 设置任务状态
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool SetWorkState(string doi, int state)
        {
            return ReMission.SetWorkState(doi, state);
        }

         /// <summary>
        /// 设置任务状态和完成情况
        /// </summary>
        /// <param name="doi"></param>
        /// <param name="state"></param>
        /// <param name="finishstate"></param>
        /// <returns></returns>
        public bool SetWorkState(string doi, int state, int finishstate)
        {
            return ReMission.SetWorkState(doi, state, finishstate);
        }
    }
}
