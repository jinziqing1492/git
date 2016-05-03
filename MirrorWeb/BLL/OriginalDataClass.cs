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
    public class OriginalDataClass
    {
        private static readonly IOriginalDataClass ReOriginalDataClass = SelectData.CreateOriginalDataClass();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(OriginalDataClassInfo originaldataclass)
        {
            if (null == originaldataclass)
            {
                return false;
            }
            return ReOriginalDataClass.Add(originaldataclass);
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

            OriginalDataClassInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReOriginalDataClass.Delete(id);
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
            return ReOriginalDataClass.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(OriginalDataClassInfo originaldataclass)
        {
            if (null == originaldataclass)
            {
                return false;
            }

            return ReOriginalDataClass.Update(originaldataclass);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OriginalDataClassInfo GetItem(string id)
        {
            return ReOriginalDataClass.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<OriginalDataClassInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReOriginalDataClass.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReOriginalDataClass.GetCount(strWhere);
        }

        /// <summary>
        /// 获取所有的根节点
        /// </summary>
        /// <returns></returns>
        public IList<OriginalDataClassInfo> GetRootThemes()
        {
            string sqlWhere = " PARENTID=0 or PARENTID is null";
            int recordCount = 0;
            IList<OriginalDataClassInfo> lstTi = GetList(sqlWhere, 1, 1000, out recordCount, true);
            if (recordCount > 1000)
            {
                lstTi = GetList(sqlWhere, 1, recordCount, out recordCount, true);
            }
            return lstTi;
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <returns></returns>
        public IList<OriginalDataClassInfo> GetSubThemes(string parentId)
        {
            string sqlWhere = " PARENTID='" + parentId + "' ";
            int recordCount = 0;
            IList<OriginalDataClassInfo> lstTi = GetList(sqlWhere, 1, 1000, out recordCount, true);
            if (recordCount > 1000)
            {
                lstTi = GetList(sqlWhere, 1, recordCount, out recordCount, true);
            }
            return lstTi;
        }

        /// <summary>
        /// 获取还在节点的个数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetChildCount(string id)
        {
            string sqlWhere = " PARENTID='" + id + "'";
            return ReOriginalDataClass.GetCount(sqlWhere);
        }
    }
}
