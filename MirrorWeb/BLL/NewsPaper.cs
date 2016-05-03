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
    public class NewsPaper
    {
        private static readonly INewsPaperInfo ReNewsPaperInfo = SelectData.CreateNewsPaperInfo();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(NewsPaperInfo newspaper)
        {
            if (null == newspaper)
            {
                return false;
            }
            return ReNewsPaperInfo.Add(newspaper);
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

            NewsPaperInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }


            //删除报纸文章文章
            NewsPaperArticle newspaperarticle = new NewsPaperArticle();
            bool IsSuccess = newspaperarticle.DeleteByWhere("BaseId='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            //删除报纸年信息
            NewsPaperYear newspaperyear = new NewsPaperYear();
            IsSuccess = newspaperyear.DeleteByWhere("BaseId='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReNewsPaperInfo.Delete(id);
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
            return ReNewsPaperInfo.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(NewsPaperInfo newspaper)
        {
            if (null == newspaper)
            {
                return false;
            }

            return ReNewsPaperInfo.Update(newspaper);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewsPaperInfo GetItem(string id)
        {
            return ReNewsPaperInfo.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<NewsPaperInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReNewsPaperInfo.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">报纸的BaseId</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            NewsPaperInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改文章的状态
            NewsPaperArticle article = new NewsPaperArticle();
            int record = 0;
            bool Flag = false;
            IList<NewsPaperArticleInfo> listart = article.GetList("BaseId='" + id + "'", 1, 1000, out record, false);
            foreach (NewsPaperArticleInfo articleinfo in listart)
            {
                Flag = article.SetState(articleinfo.SYS_FLD_DOI, state);
                if (!Flag)
                {
                    return false;
                }
            }

            //修改年表信息
            NewsPaperYear year = new NewsPaperYear();
            IList<NewsPaperYearInfo> listyear = year.GetList("BaseId='" + id + "'", 1, 1000, out record, false);
            foreach (NewsPaperYearInfo yearinfo in listyear)
            {
                Flag = year.SetState(yearinfo.SYS_FLD_DOI, state);
                if (!Flag)
                {
                    return false;
                }
            }

            return ReNewsPaperInfo.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReNewsPaperInfo.GetCount(strWhere);
        }
    }
}
