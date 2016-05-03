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
    public class HotstarSysField : IHotstarSysField
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["HOTSTAR_SYS_FIELD"];
        #region IArticle 字段
        private const string PARM_TABLE_NAME = "TABLE_NAME";
        private const string PARM_COLUMN_NAME = "COLUMN_NAME";
        private const string PARM_FIELD_TYPE = "FIELD_TYPE";
        private const string PARM_COLUMN_SIZE = "COLUMN_SIZE";
        private const string PARM_COLUMN_DEF = "COLUMN_DEF";
        private const string PARM_FIELD_CHECK = "FIELD_CHECK";
        private const string PARM_FIELD_INDEXTYPE = "FIELD_INDEXTYPE";
        private const string PARM_FIELD_ALIASNAME = "FIELD_ALIASNAME";
        private const string PARM_FIELD_DISPNAME = "FIELD_DISPNAME";
        private const string PARM_TYPE_NAME = "TYPE_NAME";
        private const string PARM_NUM_PREC_RADIX = "NUM_PREC_RADIX";
        private const string PARM_REMARKS = "REMARKS";
        private const string PARM_CHAR_OCTET_LENGTH = "CHAR_OCTET_LENGTH";
        private const string PARM_ORDINAL_POSITION = "ORDINAL_POSITION";
        private const string PARM_IS_NULLABLE = "IS_NULLABLE";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        
        /// <summary>
        /// 按分页获取数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<HotstarSysFieldInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<HotstarSysFieldInfo> entryList = new List<HotstarSysFieldInfo>();
                HotstarSysFieldInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new HotstarSysFieldInfo();
                    #region 判断字段并赋值
                    entry.Table_Name = rs.GetValue(PARM_TABLE_NAME) ?? "";
                    entry.Column_Name = rs.GetValue(PARM_COLUMN_NAME) ?? "";
                    entry.Field_Type = StructTrans.TransNum(rs.GetValue(PARM_FIELD_TYPE));
                    entry.Column_Size = StructTrans.TransNum(rs.GetValue(PARM_COLUMN_SIZE));
                    entry.Column_Def = rs.GetValue(PARM_COLUMN_DEF) ?? "";
                    entry.Field_Check = rs.GetValue(PARM_FIELD_CHECK) ?? "";
                    entry.Field_IndexType = StructTrans.TransNum(rs.GetValue(PARM_FIELD_INDEXTYPE));
                    entry.Field_AliasName = rs.GetValue(PARM_FIELD_ALIASNAME) ?? "";
                    entry.Field_DispName = rs.GetValue(PARM_FIELD_DISPNAME) ?? "";
                    entry.Type_Name = rs.GetValue(PARM_TYPE_NAME) ?? "";
                    entry.Num_Prec_radix = StructTrans.TransNum(rs.GetValue(PARM_NUM_PREC_RADIX));
                    entry.Remarks = rs.GetValue(PARM_REMARKS) ?? "";
                    entry.Char_Octet_Length = StructTrans.TransNum(rs.GetValue(PARM_CHAR_OCTET_LENGTH));
                    entry.Ordinal_Position = StructTrans.TransNum(rs.GetValue(PARM_ORDINAL_POSITION));
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
        /// 按分页获取数据，查询表中部分字段信息
        /// </summary>
        /// <param name="sqlWhere">where后的条件</param>
        /// <param name="fields">字段拼接的字符串</param>
        /// <param name="pageNo">页数</param>
        /// <param name="pageCount">每页数量</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="IsAll">是否获取所有</param>
        /// <returns></returns>
        public List<HotstarSysFieldInfo> GetListPartField(string sqlWhere,string fields, int pageNo, int pageCount, out int recordCount, bool IsAll)
        {
            recordCount = 0;
            RecordSet rs = TPIHelper.GetRecordPartField(TABLE_NAME, sqlWhere,fields);
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
                List<HotstarSysFieldInfo> entryList = new List<HotstarSysFieldInfo>();
                HotstarSysFieldInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new HotstarSysFieldInfo();
                    #region 判断字段并赋值
                    entry.Table_Name = rs.GetValue(PARM_TABLE_NAME) ?? "";
                    entry.Column_Name = rs.GetValue(PARM_COLUMN_NAME) ?? "";
                    entry.Field_Type = StructTrans.TransNum(rs.GetValue(PARM_FIELD_TYPE));
                    entry.Column_Size = StructTrans.TransNum(rs.GetValue(PARM_COLUMN_SIZE));
                    entry.Column_Def = rs.GetValue(PARM_COLUMN_DEF) ?? "";
                    entry.Field_Check = rs.GetValue(PARM_FIELD_CHECK) ?? "";
                    entry.Field_IndexType = StructTrans.TransNum(rs.GetValue(PARM_FIELD_INDEXTYPE));
                    entry.Field_AliasName = rs.GetValue(PARM_FIELD_ALIASNAME) ?? "";
                    entry.Field_DispName = rs.GetValue(PARM_FIELD_DISPNAME) ?? "";
                    entry.Type_Name = rs.GetValue(PARM_TYPE_NAME) ?? "";
                    entry.Num_Prec_radix = StructTrans.TransNum(rs.GetValue(PARM_NUM_PREC_RADIX));
                    entry.Remarks = rs.GetValue(PARM_REMARKS) ?? "";
                    entry.Char_Octet_Length = StructTrans.TransNum(rs.GetValue(PARM_CHAR_OCTET_LENGTH));
                    entry.Ordinal_Position = StructTrans.TransNum(rs.GetValue(PARM_ORDINAL_POSITION));
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

    }
}
