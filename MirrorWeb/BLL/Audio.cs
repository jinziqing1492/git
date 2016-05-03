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
   public class Audio
    {
        private static readonly IAudio ReAudio = SelectData.CreateAudio();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(AudioInfo audio)
        {
            if (null == audio)
            {
                return false;
            }
            return ReAudio.Add(audio);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">音频的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReAudio.SetIsOnline(id, isOnLine, dateTime);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            AudioInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }
         
            return ReAudio.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere">删除条件</param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                return false;
            }
            return ReAudio.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(AudioInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReAudio.Update(book);
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
            return ReAudio.AddStaticDownload(id);
        }

        /// <summary>
        /// 点击量
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public bool AddHitCount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return ReAudio.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public AudioInfo GetItem(string id)
        {
            return ReAudio.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="recordcount">返回的总记录数</param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public IList<AudioInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReAudio.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">音频的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            AudioInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }
           
            return ReAudio.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReAudio.GetCount(strWhere);
        }
    }
}
