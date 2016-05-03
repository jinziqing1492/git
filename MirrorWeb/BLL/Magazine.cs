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
    public class Magazine
    {
        private static readonly IMagazineInfo ReMagazineInfo = SelectData.CreateMagazineInfo();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(MagazineInfo magazineinfo)
        {
            if (null == magazineinfo)
            {
                return false;
            }
            return ReMagazineInfo.Add(magazineinfo);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">杂志的BASEID</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return ReMagazineInfo.SetIsOnline(id, isOnLine, dateTime);
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

            MagazineInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //删除杂志文章
            MagazineArticle magazinearticle = new MagazineArticle();
            bool IsSuccess = magazinearticle.DeleteByWhere("BaseId='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            //删除杂志年信息
            MagazineYear magazineyear = new MagazineYear();
            IsSuccess = magazineyear.DeleteByWhere("BaseId='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReMagazineInfo.Delete(id);
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
            return ReMagazineInfo.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(MagazineInfo magazineinfo)
        {
            if (null == magazineinfo)
            {
                return false;
            }

            return ReMagazineInfo.Update(magazineinfo);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MagazineInfo GetItem(string id)
        {
            return ReMagazineInfo.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<MagazineInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReMagazineInfo.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">杂志的baseid</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            MagazineInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改杂志年信息状态
            MagazineYear year = new MagazineYear();
            int record = 0;
            bool Flag = false;
            IList<MagazineYearInfo> listyear = year.GetList("BaseId='" + id + "'", 1, 1000, out record, false);
            if (listyear != null)
            {
                foreach (MagazineYearInfo yearinfo in listyear)
                {
                    Flag = year.SetState(yearinfo.SYS_FLD_DOI, state);
                    if (!Flag)
                    {
                        return false;
                    }
                }
            }
            //修改文章状态
            MagazineArticle article = new MagazineArticle();
            record = 0;
            IList<MagazineArticleInfo> listart = article.GetList("ParentDoi='" + id + "'", 1, 1000, out record, false);
            if (listart != null)
            {
                foreach (MagazineArticleInfo articleinfo in listart)
                {
                    Flag = article.SetState(articleinfo.SYS_FLD_DOI, state);
                    if (!Flag)
                    {
                        return false;
                    }
                }
            }
            return ReMagazineInfo.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReMagazineInfo.GetCount(strWhere);
        }
    }
}
