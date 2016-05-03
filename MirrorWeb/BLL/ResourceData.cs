using DRMS.DALFactory;
using DRMS.IDAL;
using DRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.BLL
{
    public class ResourceData
    {
        private static readonly IResourceData Dal = SelectData.CreateResourceData();

        #region IArticle 字段
        public const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        public const string PARM_NAME = "NAME";
        public const string PARM_CODE = "CODE";
        public const string PARM_AUTHOR = "AUTHOR";
        public const string PARM_PUBDEP = "PUBDEP";
        public const string PARM_PUBDATE = "PUBDATE";
        public const string PARM_PRICE = "PRICE";
        public const string PARM_DESCRIPTION = "DESCRIPTION";
        public const string PARM_FILEPATH = "FILEPATH";
        public const string PARM_COVERPATH = "COVERPATH";
        public const string PARM_CATALOGPATH = "CATALOGPATH";
        public const string PARM_CREATETIME = "CREATETIME";
        public const string PARM_STATUS = "STATUS";
        public const string PARM_PARENTID = "PARENTID";
        #endregion
        /// <summary>
        /// 添加
        /// <summary>
        /// <param name='item'></param>
        /// <returns></returns>
        public bool Add(ResourceDataInfo item)
        {
            if (null == item)
            {
                return false;
            }
            return Dal.Add(item);
        }

        /// <summary>
        /// 删除
        /// <summary>
        /// <param name='id'></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return Dal.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// <summary>
        /// <param name='strWhere'></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                return false;
            }
            return Dal.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// <summary>
        /// <param name='item'></param>
        /// <returns></returns>
        public bool Update(ResourceDataInfo item)
        {
            if (null == item)
            {
                return false;
            }
            return Dal.Update(item);
        }

        /// <summary>
        /// 更新记录 可指定参数
        /// <summary>
        /// <param name='item'></param>
        /// <returns></returns>
        public bool Update(ResourceDataInfo item, string[] fields)
        {
            if (null == item || fields == null || fields.Length == 0)
            {
                return false;
            }
            return Dal.Update(item, fields);
        }

        /// <summary>
        /// 获取
        /// <summary>
        /// <param name='id'></param>
        /// <returns></returns>
        public ResourceDataInfo GetItem(string id)
        {
            return Dal.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// <summary>
        /// <param name='strwhere'></param>
        /// <param name='pageno'></param>
        /// <param name='pagecount'></param>
        /// <param name='textDatacount'></param>
        /// <returns></returns>
        public IList<ResourceDataInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool isRed = false)
        {
            return Dal.GetList(strwhere, pageno, pagecount, out recordcount, isRed);
        }

        /// <summary>
        /// 获取多条 可指定字段
        /// <summary>
        /// <param name='strwhere'></param>
        /// <param name='pageno'></param>
        /// <param name='pagecount'></param>
        /// <param name='fields'></param>
        /// <returns></returns>
        public IList<ResourceDataInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, string[] fields, bool isRed = false)
        {
            if (fields == null || fields.Length == 0)
            {
                recordcount = 0;
                return null;
            }
            return Dal.GetList(strwhere, pageno, pagecount, out recordcount, fields, isRed);
        }
    }
}
