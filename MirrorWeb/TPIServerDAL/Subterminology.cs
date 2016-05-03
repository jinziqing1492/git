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
    public class Subterminology : ISubTerminology
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Subterminology"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_ISONLINE = "ISONLINE";
        private const string PARM_SYS_FLD_PARTXML = "SYS_FLD_PARTXML";
        private const string PARM_ENTRYDOI = "ENTRYDOI";
        private const string PARM_ENTRYNAME = "ENTRYNAME";
        private const string PARM_PARENTDOI = "PARENTDOI";
        private const string PARM_PARENTNAME = "PARENTNAME";
        private const string PARM_PARENTATTRPATH = "PARENTATTRPATH";
        private const string PARM_SYS_FLD_PARAXML_U = "SYS_FLD_PARAXML_U";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(SubterminologyInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            }
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.isonline.ToString());
            if (!string.IsNullOrEmpty(item.Sys_fld_partXml))
            {
                paramList.Add(PARM_SYS_FLD_PARTXML);
                paramList.Add(item.Sys_fld_partXml);
            }
            if (!string.IsNullOrEmpty(item.EntryDoi))
            {
                paramList.Add(PARM_ENTRYDOI);
                paramList.Add(item.EntryDoi);
            }
            if (!string.IsNullOrEmpty(item.EntryName))
            {
                paramList.Add(PARM_ENTRYNAME);
                paramList.Add(item.EntryName);
            }
            if (!string.IsNullOrEmpty(item.Parentdoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.Parentdoi);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (!string.IsNullOrEmpty(item.parentAttrPath))
            {
                paramList.Add(PARM_PARENTATTRPATH);
                paramList.Add(item.parentAttrPath);
            }
            #endregion
            try
            {
                return TPIHelper.Insert(TABLE_NAME, paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public bool Delete(string doi)
        {
            if (string.IsNullOrWhiteSpace(doi))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(SubterminologyInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            paramList.Add(PARM_ISONLINE);
            paramList.Add(item.isonline.ToString());
            if (!string.IsNullOrEmpty(item.Sys_fld_partXml))
            {
                paramList.Add(PARM_SYS_FLD_PARTXML);
                paramList.Add(item.Sys_fld_partXml);
            }
            if (!string.IsNullOrEmpty(item.EntryDoi))
            {
                paramList.Add(PARM_ENTRYDOI);
                paramList.Add(item.EntryDoi);
            }
            if (!string.IsNullOrEmpty(item.EntryName))
            {
                paramList.Add(PARM_ENTRYNAME);
                paramList.Add(item.EntryName);
            }
            if (!string.IsNullOrEmpty(item.Parentdoi))
            {
                paramList.Add(PARM_PARENTDOI);
                paramList.Add(item.Parentdoi);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (!string.IsNullOrEmpty(item.parentAttrPath))
            {
                paramList.Add(PARM_PARENTATTRPATH);
                paramList.Add(item.parentAttrPath);
            }
            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_SYS_FLD_DOI + "='" + item.SYS_FLD_DOI + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据id获得一条记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public SubterminologyInfo GetItem(string doi)
        {
            if (string.IsNullOrEmpty(doi))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, doi);
            RecordSet rs = TPIHelper.GetRecordSet(sqlQuery);
            if (rs == null)
            {
                return null;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return null;
            }
            try
            {
                SubterminologyInfo entry = new SubterminologyInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.isonline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
               // entry.Sys_fld_partXml = rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "";
                entry.EntryDoi = rs.GetValue(PARM_ENTRYDOI) ?? "";
                entry.EntryName = rs.GetValue(PARM_ENTRYNAME) ?? "";
                entry.Parentdoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                entry.parentAttrPath = rs.GetValue(PARM_PARENTATTRPATH) ?? "";
               // entry.Sys_fld_partXml_U = rs.GetValue(PARM_SYS_FLD_PARAXML_U) ?? "";
                entry.Sys_fld_partXml = string.IsNullOrEmpty(rs.GetValue(PARM_SYS_FLD_PARAXML_U)) ? (rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "") : rs.GetValue(PARM_SYS_FLD_PARAXML_U);

                #endregion
                return entry;
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
        /// 根据分页获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<SubterminologyInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<SubterminologyInfo> entryList = new List<SubterminologyInfo>();
                SubterminologyInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new SubterminologyInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.isonline = StructTrans.TransNum(rs.GetValue(PARM_ISONLINE));
                    entry.Sys_fld_partXml = rs.GetValue(PARM_SYS_FLD_PARTXML) ?? "";
                    entry.EntryDoi = rs.GetValue(PARM_ENTRYDOI) ?? "";
                    entry.EntryName = rs.GetValue(PARM_ENTRYNAME) ?? "";
                    entry.Parentdoi = rs.GetValue(PARM_PARENTDOI) ?? "";
                    entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                    entry.parentAttrPath = rs.GetValue(PARM_PARENTATTRPATH) ?? "";
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
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrWhiteSpace(strWhere))
            {
                return false;
            }
            
            ////删除词条内容
            //int record = 0;
            //IList<SubterminologyInfo> termlist = GetList(strWhere, 1, 1000, out record, false);
            //foreach (SubterminologyInfo info in termlist)
            //{
            //    //删除图片
            //    Pic p = new Pic();
            //    bool IsSuccess = p.DeleteByWhere("ParentDoi='" + info.SYS_FLD_DOI + "'");
            //    if (!IsSuccess)
            //    {
            //        return false;
            //    }
            //}

            //删除词条里的部分内容
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
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
        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id">词条里的部分内容的SYS_FLD_DOI</param>
        /// <param name="isOnLine">0为下架状态，1为上架状态</param>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public bool SetIsOnline(string id, string isOnLine, string dateTime)
        {
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_DATE='{1}',ISONLINE='{2}' WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, dateTime, isOnLine, id);
            return TPIHelper.ExecSql(strSet);
        }
    }
}
