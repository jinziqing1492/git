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
    public class MagazineArticle
    {
        private static readonly IMagazineArticle ReMagazineArticle = SelectData.CreateMagazineArticle();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(MagazineArticleInfo magazinearticleinfo)
        {
            if (null == magazinearticleinfo)
            {
                return false;
            }
            return ReMagazineArticle.Add(magazinearticleinfo);
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

            MagazineArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }


            //删除图片
            Pic p = new Pic();
            bool IsSuccess = p.DeleteByWhere("SYS_FLD_ChapterDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReMagazineArticle.Delete(id);
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
            return ReMagazineArticle.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(MagazineArticleInfo magazinearticle)
        {
            if (null == magazinearticle)
            {
                return false;
            }

            return ReMagazineArticle.Update(magazinearticle);
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
            return ReMagazineArticle.AddStaticDownload(id);
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
            return ReMagazineArticle.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MagazineArticleInfo GetItem(string id)
        {
            return ReMagazineArticle.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<MagazineArticleInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReMagazineArticle.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">杂志的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id) || (state != 0 && state != -1))
            {
                return false;
            }
            MagazineArticleInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
            Pic picture = new Pic();
            int record;
            IList<PicInfo> list = picture.GetList("SYS_FLD_ChapterDoi='" + id + "'", 1, 1000, out record, false);
            foreach (PicInfo pictureinfo in list)
            {
                bool Flag = picture.SetState(pictureinfo.SYS_FLD_DOI, state);
                if (!Flag)
                {
                    return false;
                }
            }

            return ReMagazineArticle.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReMagazineArticle.GetCount(strWhere);
        }
    }
}
