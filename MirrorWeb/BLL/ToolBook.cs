using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

using DRMS.DALFactory;
using DRMS.IDAL;
using DRMS.Model;
using CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class ToolBook
    {
        private static readonly IToolBook ReToolBook = SelectData.CreateToolBook();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(ToolBookInfo toolbook)
        {
            if (null == toolbook)
            {
                return false;
            }
            return ReToolBook.Add(toolbook);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">工具书的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            ToolBookInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReToolBook.SetIsOnline(id, isOnLine, dateTime);
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

            ToolBookInfo info = GetItem(id);
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
            //删除工具书词条
            Terminology dal = new Terminology();
            string strWhere1 = "parenturlid='" + id + "'";
            //获取词条，用以删除词条内容记录
            int record = 0;
            IList<TerminologyInfo> termlist = dal.GetList(strWhere1, 1, 1000, out record, false);
            Subterminology subterm = new Subterminology();
            if (termlist != null)
            {
                foreach (TerminologyInfo termitem in termlist)
                {
                    IsSuccess = subterm.DeleteByWhere("EntryDoi='" + termitem.SYS_FLD_DOI + "'");
                    if (!IsSuccess)
                    {
                        return false;
                    }
                }
            }
            IsSuccess = dal.DeleteByWhere(strWhere1);
            if (!IsSuccess)
            {
                return false;
            }

            return ReToolBook.Delete(id);
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
            return ReToolBook.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(ToolBookInfo toolbook)
        {
            if (null == toolbook)
            {
                return false;
            }

            return ReToolBook.Update(toolbook);
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
            return ReToolBook.AddStaticDownload(id);
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
            return ReToolBook.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ToolBookInfo GetItem(string id)
        {
            return ReToolBook.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<ToolBookInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReToolBook.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">工具书的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            ToolBookInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
           // int recordCount = 0;
            DRMS.IDAL.IPic p = new DRMS.TPIServerDAL.Pic();
            //IList<PicInfo> lstPic = p.GetList("ParentDoi='" + id + "'", 0, 1000, out recordCount, true);
            //if (recordCount > 1000)
            //    lstPic = p.GetList("ParentDoi='" + id + "'", 0, recordCount, out recordCount, true);
            //for (int i = 0; i < recordCount; i++)
            //{
            //    bool IsSuccess = p.SetState(lstPic[i].SYS_FLD_DOI, state);
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}
            bool IsSuccess = p.SetStateByWhere("ParentDoi='" + id + "'", state);
            if (!IsSuccess)
            {
                return false;
            }

            //修改词条的状态
           // recordCount = 0;
            DRMS.IDAL.ITerminology cpter = new DRMS.TPIServerDAL.Terminology();
            //IList<PicInfo> lstTerminology = p.GetList("parenturlid='" + id + "'", 0, 1000, out recordCount, true);
            //if (recordCount > 1000)
            //    lstPic = p.GetList("parenturlid='" + id + "'", 0, recordCount, out recordCount, true);
            //for (int i = 0; i < recordCount; i++)
            //{
            //    bool IsSuccess = cpter.SetState(lstTerminology[i].SYS_FLD_DOI, state);
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}
            IsSuccess = cpter.SetStateByWhere("parenturlid='" + id + "'", state);
            if (!IsSuccess)
            {
                return false;
            }
            return ReToolBook.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReToolBook.GetCount(strWhere);
        }
    }
}
