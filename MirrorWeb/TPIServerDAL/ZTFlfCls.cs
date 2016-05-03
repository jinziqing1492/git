using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using DRMS.Model;
using DRMS.IDAL;
using CNKI.BaseFunction;
using TPI;

namespace DRMS.TPIServerDAL
{
   public class ZTFlfCls:IZTFlfCls
    {
       private static string TABLE_NAME = ConfigurationManager.AppSettings["ZTFlfCls"];
        #region IArticle 字段
        public const string SYS_FLD_CLASS_NAME="SYS_FLD_CLASS_NAME"; //中图名称
        public const string SYS_FLD_CLASS_CODE="SYS_FLD_CLASS_CODE"; //中图编号（A A1 A11 A19）
        public const string SYS_FLD_CLASS_GRADE = "SYS_FLD_CLASS_GRADE"; //分类级别
        public const string SYS_FLD_CHILD_FLAG = "SYS_FLD_CHILD_FLAG"; //有无子级 有1 无0
        public const string SYS_FLD_SYS_CODE = "SYS_FLD_SYS_CODE"; //中图主键
        public const string SYS_FLD_PARENT_CODE = "SYS_FLD_PARENT_CODE"; //
        public const string SYS_FLD_PREVSILIBING_CODE = "SYS_FLD_PREVSILIBING_CODE"; //
        public const string SYS_FLD_NEXTSILIBING_CODE = "SYS_FLD_NEXTSILIBING_CODE"; //
        public const string SYS_FLD_CHILD_SORTSN = "SYS_FLD_CHILD_SORTSN"; //
        public const string SYS_FLD_SYSID = "SYS_FLD_SYSID"; //
        public const string SYS_FLD_RECORD_COUNT = "SYS_FLD_RECORD_COUNT"; //

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 得到多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<ZTFlfClsInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            recordCount = 0;
            RecordSet rs = TPIHelper.GetRecordSetByCondition(TABLE_NAME, sqlWhere);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            //  获取总得记录数
            recordCount = rs.GetCount();
            rs.SetHitWordMarkFlag(RED_LEFT, RED_RIGHT);
            //  获取分页操作的记录的区间
            IList<int> paginationInterval = Pagination.GetPageStartToEnd(ref pageNo, pageCount, recordCount);
            rs.Move(paginationInterval[0]);
            try
            {
                List<ZTFlfClsInfo> entryList = new List<ZTFlfClsInfo>();
                ZTFlfClsInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new ZTFlfClsInfo();
                    #region 判断字段并赋值

                    entry.SYS_FLD_CHILD_FLAG = StructTrans.TransNum(rs.GetValue(SYS_FLD_CHILD_FLAG));
                    entry.SYS_FLD_CHILD_SORTSN = rs.GetValue(SYS_FLD_CHILD_SORTSN) ?? "";
                    entry.SYS_FLD_CLASS_CODE = rs.GetValue(SYS_FLD_CLASS_CODE) ?? "";
                    entry.SYS_FLD_CLASS_GRADE = StructTrans.TransNum(rs.GetValue(SYS_FLD_CHILD_FLAG));
                    entry.SYS_FLD_CLASS_NAME = rs.GetValue(SYS_FLD_CLASS_NAME) ?? "";
                    entry.SYS_FLD_NEXTSILIBING_CODE = rs.GetValue(SYS_FLD_NEXTSILIBING_CODE) ?? "";
                    entry.SYS_FLD_PARENT_CODE = rs.GetValue(SYS_FLD_PARENT_CODE) ?? "";
                    entry.SYS_FLD_PREVSILIBING_CODE = rs.GetValue(SYS_FLD_PREVSILIBING_CODE) ?? "";
                    entry.SYS_FLD_RECORD_COUNT = rs.GetValue(SYS_FLD_RECORD_COUNT) ?? "";
                    entry.SYS_FLD_SYS_CODE = rs.GetValue(SYS_FLD_SYS_CODE) ?? "";
                    entry.SYS_FLD_SYSID = StructTrans.TransNum(rs.GetValue(SYS_FLD_SYSID));

                    #endregion
                    entryList.Add(entry);
                    if (!rs.MoveNext())
                    {
                        break;
                    }
                }
                return entryList;
            }
            catch
            {
                return null;
            }
            finally
            {
                rs.Close();
            }
        }

        /// <summary>
        /// 根据条件获得记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }

    }
}
