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
   public class ZTFlfCls
    {
       private static readonly IZTFlfCls ReZTFlfCls = SelectData.CreateZTFlfCls();

       /// <summary>
       /// 获取多条
       /// </summary>
       /// <param name="strwhere"></param>
       /// <param name="pageno"></param>
       /// <param name="pagecount"></param>
       /// <param name="textDatacount"></param>
       /// <returns></returns>
       public IList<ZTFlfClsInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
       {
           return ReZTFlfCls.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
       }

       /// <summary>
       /// 获取所有的根节点
       /// </summary>
       /// <returns></returns>
       public IList<ZTFlfClsInfo> GetRootZTFlfCls()
       {
           string sqlWhere = " SYS_FLD_CLASS_GRADE=1";
           int recordCount = 0;
           IList<ZTFlfClsInfo> lstTi = GetList(sqlWhere, 1, 1000, out recordCount, true);
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
       public IList<ZTFlfClsInfo> GetSubZTFlfCls(string parentId,int childGrade)
       {
           string sqlWhere = " SYS_FLD_SYS_CODE='" + parentId + "?' and  SYS_FLD_CLASS_GRADE="+childGrade;
           int recordCount = 0;
           IList<ZTFlfClsInfo> lstTi = GetList(sqlWhere, 1, 1000, out recordCount, true);
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
           return ReZTFlfCls.GetCount(strWhere);
       }

       public int GetChildCount(string id, int childGrade)
       {
           string sqlWhere = " SYS_FLD_SYS_CODE='" + id + "?' and  SYS_FLD_CLASS_GRADE=" + childGrade;
           return ReZTFlfCls.GetCount(sqlWhere);
       }
    }
}
