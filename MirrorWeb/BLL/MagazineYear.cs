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
    public class MagazineYear
    {
        private static readonly IMagazineYearInfo ReMagazineYearInfo = SelectData.CreateMagazineYearInfo();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(MagazineYearInfo magazineyearinfo)
        {
            if (null == magazineyearinfo)
            {
                return false;
            }
            return ReMagazineYearInfo.Add(magazineyearinfo);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">杂志年信息的的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            MagazineYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReMagazineYearInfo.SetIsOnline(id, isOnLine, dateTime);
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

            MagazineYearInfo info = GetItem(id);
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
            Attachment attach = new Attachment();
            IsSuccess = attach.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            //删除文章
            MagazineArticle magazinearticle = new MagazineArticle();
            IsSuccess = magazinearticle.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReMagazineYearInfo.Delete(id);
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
            return ReMagazineYearInfo.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(MagazineYearInfo magazineyearinfo)
        {
            if (null == magazineyearinfo)
            {
                return false;
            }

            return ReMagazineYearInfo.Update(magazineyearinfo);
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
            return ReMagazineYearInfo.AddStaticDownload(id);
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
            return ReMagazineYearInfo.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MagazineYearInfo GetItem(string id)
        {
            return ReMagazineYearInfo.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<MagazineYearInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReMagazineYearInfo.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">杂志年信息的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            MagazineYearInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            //修改图片的状态
            Pic picture = new Pic();
            int record = 0;
            bool Flag = false;
            IList<PicInfo> listpic = picture.GetList("ParentDoi='" + id + "'", 1, 1000, out record, false);
            if (listpic != null)
            {
                foreach (PicInfo pictureinfo in listpic)
                {
                    Flag = picture.SetState(pictureinfo.SYS_FLD_DOI, state);
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

            return ReMagazineYearInfo.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReMagazineYearInfo.GetCount(strWhere);
        }
    }
}
