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
    public class OriginalData : IOriginalData
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["OriginalData"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_DESCIPTION = "DESCIPTION";
        private const string PARM_PARENTNAME = "PARENTNAME";
        private const string PARM_ORGINALRESTYPE = "ORGINALRESTYPE";
        private const string PARM_SYS_FLD_CLASSFICATION = "SYS_FLD_CLASSFICATION";
        private const string PARM_SYS_FLD_CLASSNAME = "SYS_FLD_CLASSNAME";
        private const string PARM_SYS_FLD_READUSER = "SYS_FLD_READUSER";
        private const string PARM_SYS_FLD_DOWNLOADUSER = "SYS_FLD_DOWNLOADUSER";
        private const string PARM_SYS_FLD_VERSION = "SYS_FLD_VERSION";
        private const string PARM_SYS_FLD_GROUPID = "SYS_FLD_GROUPID";
        private const string PARM_SYS_FLD_STATUS = "SYS_FLD_STATUS";
        private const string PARM_KEYWORDS = "KEYWORDS";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_SYS_SYSID = "SYS_SYSID";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        private const string PARM_FILETYPE = "FILETYPE";
        private const string PARM_SOURCE = "SOURCE";

        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(OriginalDataInfo item)
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
            if (!string.IsNullOrEmpty(item.Desciption))
            {
                paramList.Add(PARM_DESCIPTION);
                paramList.Add(item.Desciption);
            }
            if (!string.IsNullOrEmpty(item.ParentName))
            {
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            }
            if (!string.IsNullOrEmpty(item.OrginalresType))
            {
                paramList.Add(PARM_ORGINALRESTYPE);
                paramList.Add(item.OrginalresType);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            {
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSNAME))
            {
                paramList.Add(PARM_SYS_FLD_CLASSNAME);
                paramList.Add(item.SYS_FLD_CLASSNAME);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_READUSER))
            {
                paramList.Add(PARM_SYS_FLD_READUSER);
                paramList.Add(item.SYS_FLD_READUSER);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOWNLOADUSER))
            {
                paramList.Add(PARM_SYS_FLD_DOWNLOADUSER);
                paramList.Add(item.SYS_FLD_DOWNLOADUSER);
            }
            paramList.Add(PARM_SYS_FLD_VERSION);
            paramList.Add(item.SYS_FLD_VERSION.ToString());
            if (!string.IsNullOrEmpty(item.SYS_FLD_GROUPID))
            {
                paramList.Add(PARM_SYS_FLD_GROUPID);
                paramList.Add(item.SYS_FLD_GROUPID);
            }
            paramList.Add(PARM_SYS_FLD_STATUS);
            paramList.Add(item.SYS_FLD_STATUS.ToString());
            if (!string.IsNullOrEmpty(item.KeyWords))
            {
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.KeyWords);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_FILEPATH))
            {
                paramList.Add(PARM_SYS_FLD_FILEPATH);
                paramList.Add(item.SYS_FLD_FILEPATH);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            }
            if (item.Sys_fld_Adddate != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            {
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            }
            if(!string.IsNullOrEmpty(item.FileType))
            {
                paramList.Add(PARM_FILETYPE);
                paramList.Add(item.FileType);
            }
            if(!string.IsNullOrEmpty(item.Source))
            {
                paramList.Add(PARM_SOURCE);
                paramList.Add(item.Source);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_SYS_FLD_DOI, id);
            return TPIHelper.ExecSql(sqlDelete);
        }
        public bool Update(OriginalDataInfo item)
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
            //if (!string.IsNullOrEmpty(item.Desciption))
            //{
                paramList.Add(PARM_DESCIPTION);
                paramList.Add(item.Desciption);
            //}
            //if (!string.IsNullOrEmpty(item.ParentName))
            //{
                paramList.Add(PARM_PARENTNAME);
                paramList.Add(item.ParentName);
            //}
            //if (!string.IsNullOrEmpty(item.OrginalresType))
            //{
                paramList.Add(PARM_ORGINALRESTYPE);
                paramList.Add(item.OrginalresType);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSFICATION))
            //{
                paramList.Add(PARM_SYS_FLD_CLASSFICATION);
                paramList.Add(item.SYS_FLD_CLASSFICATION);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_CLASSNAME))
            //{
                paramList.Add(PARM_SYS_FLD_CLASSNAME);
                paramList.Add(item.SYS_FLD_CLASSNAME);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_READUSER))
            //{
                paramList.Add(PARM_SYS_FLD_READUSER);
                paramList.Add(item.SYS_FLD_READUSER);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_DOWNLOADUSER))
            //{
                paramList.Add(PARM_SYS_FLD_DOWNLOADUSER);
                paramList.Add(item.SYS_FLD_DOWNLOADUSER);
            //}
            paramList.Add(PARM_SYS_FLD_VERSION);
            paramList.Add(item.SYS_FLD_VERSION.ToString());
            //if (!string.IsNullOrEmpty(item.SYS_FLD_GROUPID))
            //{
                paramList.Add(PARM_SYS_FLD_GROUPID);
                paramList.Add(item.SYS_FLD_GROUPID);
            //}
            paramList.Add(PARM_SYS_FLD_STATUS);
            paramList.Add(item.SYS_FLD_STATUS.ToString());
            //if (!string.IsNullOrEmpty(item.KeyWords))
            //{
                paramList.Add(PARM_KEYWORDS);
                paramList.Add(item.KeyWords);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_FILEPATH))
            //{
                paramList.Add(PARM_SYS_FLD_FILEPATH);
                paramList.Add(item.SYS_FLD_FILEPATH);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            //{
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            //{
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            //}
            //if (!string.IsNullOrEmpty(item.SYS_SYSID))
            //{
                paramList.Add(PARM_SYS_SYSID);
                paramList.Add(item.SYS_SYSID);
            //}
            //if (item.Sys_fld_Adddate != DateTime.MinValue)
            //{
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            //{
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            //}
                //if(!string.IsNullOrEmpty(item.FileType))
                //{
                    paramList.Add(PARM_FILETYPE);
                    paramList.Add(item.FileType);
               // }
                //if(!string.IsNullOrEmpty(item.Source))
                //{
                    paramList.Add(PARM_SOURCE);
                    paramList.Add(item.Source);
                //}
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
        public OriginalDataInfo GetItem(string doi)
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
                OriginalDataInfo entry = new OriginalDataInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Desciption = rs.GetValue(PARM_DESCIPTION) ?? "";
                entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                entry.OrginalresType = rs.GetValue(PARM_ORGINALRESTYPE) ?? "";
                entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                entry.SYS_FLD_CLASSNAME = rs.GetValue(PARM_SYS_FLD_CLASSNAME) ?? "";
                entry.SYS_FLD_READUSER = rs.GetValue(PARM_SYS_FLD_READUSER) ?? "";
                entry.SYS_FLD_DOWNLOADUSER = rs.GetValue(PARM_SYS_FLD_DOWNLOADUSER) ?? "";
                entry.SYS_FLD_VERSION = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_VERSION));
                entry.SYS_FLD_GROUPID = rs.GetValue(PARM_SYS_FLD_GROUPID) ?? "";
                entry.SYS_FLD_STATUS = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_STATUS));
                entry.KeyWords = rs.GetValue(PARM_KEYWORDS) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.FileType = rs.GetValue(PARM_FILETYPE) ?? "";
                entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
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
        public List<OriginalDataInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<OriginalDataInfo> entryList = new List<OriginalDataInfo>();
                OriginalDataInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new OriginalDataInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Desciption = rs.GetValue(PARM_DESCIPTION) ?? "";
                    entry.ParentName = rs.GetValue(PARM_PARENTNAME) ?? "";
                    entry.OrginalresType = rs.GetValue(PARM_ORGINALRESTYPE) ?? "";
                    entry.SYS_FLD_CLASSFICATION = rs.GetValue(PARM_SYS_FLD_CLASSFICATION) ?? "";
                    entry.SYS_FLD_CLASSNAME = rs.GetValue(PARM_SYS_FLD_CLASSNAME) ?? "";
                    entry.SYS_FLD_READUSER = rs.GetValue(PARM_SYS_FLD_READUSER) ?? "";
                    entry.SYS_FLD_DOWNLOADUSER = rs.GetValue(PARM_SYS_FLD_DOWNLOADUSER) ?? "";
                    entry.SYS_FLD_VERSION = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_VERSION));
                    entry.SYS_FLD_GROUPID = rs.GetValue(PARM_SYS_FLD_GROUPID) ?? "";
                    entry.SYS_FLD_STATUS = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_STATUS));
                    entry.KeyWords = rs.GetValue(PARM_KEYWORDS) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.SYS_SYSID = rs.GetValue(PARM_SYS_SYSID) ?? "";
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.FileType = rs.GetValue(PARM_FILETYPE) ?? "";
                    entry.Source = rs.GetValue(PARM_SOURCE) ?? "";
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
            //删除原始资料
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件获得多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
    }
}
