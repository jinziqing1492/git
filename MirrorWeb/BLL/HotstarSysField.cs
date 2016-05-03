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
    public class HotstarSysField
    {
        private static readonly IHotstarSysField ReHotstarSysField = SelectData.CreateHotstarSysField();
        
        /// <summary>
        /// 按分页获取记录列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public IList<HotstarSysFieldInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            return ReHotstarSysField.GetList(sqlWhere, pageNo, pageCount, out recordCount, IsAll);
        }


        public IList<HotstarSysFieldInfo> GetListPartField(string sqlWhere, string fields, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            return ReHotstarSysField.GetListPartField(sqlWhere, fields, pageNo, pageCount, out recordCount, IsAll);
        }
    }
}
