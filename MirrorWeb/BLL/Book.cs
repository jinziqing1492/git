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
using DRMS.TPIServerDAL;

namespace DRMS.BLL
{
    public class Book
    {

        private static readonly IBook ReBook = SelectData.CreateBook();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(BookInfo book)
        {
            if (null == book)
            {
                return false;
            }
            return ReBook.Add(book);
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">图书的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            BookInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReBook.SetIsOnline(id, isOnLine, dateTime);
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

            //获取图书 判断是图书还是工具书
            BookInfo info = GetItem(id);
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
            //删除章节
            Chapter cpter = new Chapter();
            IsSuccess = cpter.DeleteByWhere("ParentDoi='" + id + "'");
            if (!IsSuccess)
            {
                return false;
            }

            return ReBook.Delete(id);
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
            return ReBook.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(BookInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReBook.Update(book);
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
            return ReBook.AddStaticDownload(id);
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
            return ReBook.AddHitCount(id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookInfo GetItem(string id)
        {
            return ReBook.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<BookInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReBook.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
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
            BookInfo info = GetItem(id);
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
            //修改章节的状态
            //recordCount = 0;
            DRMS.IDAL.IChapter cpter = new DRMS.TPIServerDAL.Chapter();
            //IList<ChapterInfo> lstChapter = cpter.GetList("ParentDoi='" + id + "'", 0, 1000, out recordCount, true);
            //if (recordCount > 1000)
            //    lstPic = p.GetList("ParentDoi='" + id + "'", 0, recordCount, out recordCount, true);
            //for (int i = 0; i < recordCount; i++)
            //{
            //    bool IsSuccess = cpter.SetState(lstChapter[i].SYS_FLD_DOI, state);
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}
            IsSuccess = cpter.SetStateByWhere("ParentDoi='" + id + "'", state);
            return ReBook.SetState(id, state);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReBook.GetCount(strWhere);
        }

        /// <summary>
        /// 切词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetWord(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return ReBook.GetWord(input);
        }
        
       /// <summary>
       /// 根据字段列表获取数据
       /// </summary>
       /// <param name="Fields">全取数据可以为null</param>
       /// <param name="tableName"></param>
       /// <param name="sqlWhere"></param>
       /// <param name="pageNo"></param>
       /// <param name="pageCount"></param>
       /// <param name="recordCount"></param>
       /// <returns></returns>
        public List<List<string>> GetDataByFieldList(List<string> Fields, string tableName, string sqlWhere, int pageNo, int pageCount, out int recordCount)
        {
            return ReBook.GetDataByFieldList(Fields, tableName, sqlWhere, pageNo, pageCount, out recordCount);
        }
    }
}
