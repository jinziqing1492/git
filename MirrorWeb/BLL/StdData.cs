using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class StdData
    {
        private static readonly IStdData ReStdData = SelectData.CreateStdData();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(StdDataInfo stddata)
        {
            if (null == stddata)
            {
                return false;
            }
            return ReStdData.Add(stddata);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">标准数据表的主键</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReStdData.SetIsOnline(id, isOnLine, dateTime);
            }
            else
            {
                return false;
            }
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

            //获取标准
            StdDataInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //删除图片
            Pic p = new Pic();
            bool IsSuccess = p.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }
            //删除附件
            Attachment atta = new Attachment();
            IsSuccess = atta.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }
            //删除术语、缩略语
            Terminology dal = new Terminology();
            IsSuccess = dal.DeleteByWhere("parenturlid='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReStdData.Delete(id);
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
            return ReStdData.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(StdDataInfo stddata)
        {
            if (null == stddata)
            {
                return false;
            }

            return ReStdData.Update(stddata);
        }

        /// <summary>
        /// 增加下载次数 
        /// </summary>
        /// <param name="id">doi</param>
        /// <returns></returns>
        public bool AddStaticDownload(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return ReStdData.AddStaticDownload(id);
        }

        /// <summary>
        /// 点击量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddHitCount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return ReStdData.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StdDataInfo GetItem(string id)
        {
            return ReStdData.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<StdDataInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReStdData.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">图书的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            StdDataInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
            int recordCount = 0;
            DRMS.IDAL.IPic p = new DRMS.TPIServerDAL.Pic();
            IList<PicInfo> lstPic = p.GetList("ParentDoi='" + id + "'", 0, 1000, out recordCount, true);
            if (recordCount > 1000)
                lstPic = p.GetList("ParentDoi='" + id + "'", 0, recordCount, out recordCount, true);
            for (int i = 0; i < recordCount; i++)
            {
                bool IsSuccess = p.SetState(lstPic[i].SYS_FLD_DOI, state);
                if (!IsSuccess)
                {
                    return false;
                }
            }
            //修改词条的状态
            recordCount = 0;
            DRMS.IDAL.ITerminology cpter = new DRMS.TPIServerDAL.Terminology();
            IList<PicInfo> lstTerminology = p.GetList("parenturlid='" + id + "'", 0, 1000, out recordCount, true);
            if (recordCount > 1000)
                lstPic = p.GetList("parenturlid='" + id + "'", 0, recordCount, out recordCount, true);
            for (int i = 0; i < recordCount; i++)
            {
                bool IsSuccess = cpter.SetState(lstTerminology[i].SYS_FLD_DOI, state);
                if (!IsSuccess)
                {
                    return false;
                }
            }

            return ReStdData.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReStdData.GetCount(strWhere);
        }
    }
}
