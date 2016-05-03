using DRMS.DALFactory;
using DRMS.IDAL;
using DRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.BLL
{
    public class OwnerResourceType
    {
        private static readonly IOwnerResourceType Dal = SelectData.CreateOwnerResourceType();

        #region IArticle 字段
        public const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_BASEID = "BASEID";
        private const string PARM_DATATYPE = "DATATYPE";
        public const string PARM_NAME = "NAME";
        public const string PARM_SYS_SYSID = "SYS_SYSID";
        public const string PARM_DESCRIPT = "DESCRIPT";
        public const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        #endregion
        /// <summary>
        /// 添加
        /// <summary>
        /// <param name='item'></param>
        /// <returns></returns>
        public bool Add(OwnerResourceTypeInfo item)
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
        public bool Update(OwnerResourceTypeInfo item)
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
        public bool Update(OwnerResourceTypeInfo item, string[] fields)
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
        public OwnerResourceTypeInfo GetItem(string id)
        {
            return Dal.GetItem(id);
        }

        /// <summary>
        /// 获取
        /// <summary>
        /// <param name='id'></param>
        /// <returns></returns>
        public OwnerResourceTypeInfo GetItemByBaseID(string baseid)
        {
            return Dal.GetItemByBaseID(baseid);
        }

        /// <summary>
        /// 获取多条
        /// <summary>
        /// <param name='strwhere'></param>
        /// <param name='pageno'></param>
        /// <param name='pagecount'></param>
        /// <param name='textDatacount'></param>
        /// <returns></returns>
        public IList<OwnerResourceTypeInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool isRed = false)
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
        public IList<OwnerResourceTypeInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, string[] fields, bool isRed = false)
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
