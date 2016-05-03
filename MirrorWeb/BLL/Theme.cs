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
    public class Theme
    {

        private static readonly ITheme ReTheme = SelectData.CreateTheme();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(ThemeInfo theme)
        {
            if (null == theme)
            {
                return false;
            }
            return ReTheme.Add(theme);
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

            ThemeInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReTheme.Delete(id);
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
            return ReTheme.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(ThemeInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReTheme.Update(book);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ThemeInfo GetItem(string id)
        {
            return ReTheme.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<ThemeInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReTheme.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }
        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public List<ThemeInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount)
        {
            return ReTheme.GetList(strwhere, pageno, pagecount, out recordcount, false);
        }
        /// <summary>
        /// 获取所有的根节点
        /// </summary>
        /// <returns></returns>
        public IList<ThemeInfo> GetRootThemes()
        {
            string sqlWhere = " PARENTID=0 or PARENTID is null order by ORDERNUM asc";
            int recordCount = 0;
            IList<ThemeInfo> lstTi = GetList(sqlWhere, 1, 1000, out recordCount, true);
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
        public IList<ThemeInfo> GetSubThemes(string parentId)
        {
            string sqlWhere = " PARENTID='" + parentId + "' order by ORDERNUM asc";
            int recordCount = 0;
            IList<ThemeInfo> lstTi = GetList(sqlWhere, 1, 1000, out recordCount, true);
            if (recordCount > 1000)
                lstTi = GetList(sqlWhere, 1, recordCount, out recordCount, true);
            return lstTi;
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReTheme.GetCount(strWhere);
        }

        /// <summary>
        /// 从蜘蛛中获取新闻的分类
        /// </summary>
        /// <param name="sqlWhere">SQL语句的查询条件</param>
        /// <param name="pageNo">分页查询的页码（第一页为1）</param>
        /// <param name="pageCount">每页的记录数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <param name="IsAll">是否获取所有字段的数据</param>
        /// <returns>实体列表</returns>
        public List<ThemeInfo> GetListKspider(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            return ReTheme.GetListKspider(sqlWhere, pageNo, pageCount, out recordCount, IsAll);
        }
        /// <summary>
        /// 根据条件获取子节点记录数
        /// </summary>
        public int GetChildCount(string id)
        {
            string sqlWhere = " PARENTID='" + id + "'";
            return ReTheme.GetCount(sqlWhere);
        }
    }
}
