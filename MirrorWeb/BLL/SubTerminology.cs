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
    public class Subterminology
    {
        private static readonly ISubTerminology ReSubterminology = SelectData.CreateSubTerminology();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(SubterminologyInfo Subterminology)
        {
            if (null == Subterminology)
            {
                return false;
            }
            return ReSubterminology.Add(Subterminology);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">术语部分的主键</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReSubterminology.SetIsOnline(id, isOnLine, dateTime);
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


            SubterminologyInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReSubterminology.Delete(id);
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
            return ReSubterminology.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(SubterminologyInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReSubterminology.Update(book);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubterminologyInfo GetItem(string id)
        {
            return ReSubterminology.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<SubterminologyInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReSubterminology.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReSubterminology.GetCount(strWhere);
        }
    }
}
