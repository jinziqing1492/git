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
    public class Org : IOrg
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Org"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_PLACE = "PLACE";
        private const string PARM_FOUNDDATE = "FOUNDDATE";
        private const string PARM_TEL1 = "TEL1";
        private const string PARM_TEL2 = "TEL2";
        private const string PARM_CONTRACTPERSON = "CONTRACTPERSON";
        private const string PARM_CONTRACTTEL = "CONTRACTTEL";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_PROVINCE = "PROVINCE";
        private const string PARM_CITY = "CITY";
        private const string PARM_ADDRESS = "ADDRESS";
        private const string PARM_EMAIL = "EMAIL";
        private const string PARM_WEBSITE = "WEBSITE";
        private const string PARM_DESCRIPTION = "DESCRIPTION";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(OrgInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.SYS_FLD_DOI))
            {
                paramList.Add(PARM_SYS_FLD_DOI);
                paramList.Add(item.SYS_FLD_DOI);
            }
            if (!string.IsNullOrEmpty(item.Name))
            {
                paramList.Add(PARM_NAME);
                paramList.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(item.Place))
            {
                paramList.Add(PARM_PLACE);
                paramList.Add(item.Place);
            }
            if (item.Founddate != DateTime.MinValue)
            {
                paramList.Add(PARM_FOUNDDATE);
                paramList.Add(item.Founddate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.TEL1))
            {
                paramList.Add(PARM_TEL1);
                paramList.Add(item.TEL1);
            }
            if (!string.IsNullOrEmpty(item.TEL2))
            {
                paramList.Add(PARM_TEL2);
                paramList.Add(item.TEL2);
            }
            if (!string.IsNullOrEmpty(item.Contractperson))
            {
                paramList.Add(PARM_CONTRACTPERSON);
                paramList.Add(item.Contractperson);
            }
            if (!string.IsNullOrEmpty(item.Contracttel))
            {
                paramList.Add(PARM_CONTRACTTEL);
                paramList.Add(item.Contracttel);
            }
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            }
            if (item.Founddate != DateTime.MinValue)
            {
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            {
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            }
            if (!string.IsNullOrEmpty(item.PROVINCE))
            {
                paramList.Add(PARM_PROVINCE);
                paramList.Add(item.PROVINCE);
            }
            if (!string.IsNullOrEmpty(item.CITY))
            {
                paramList.Add(PARM_CITY);
                paramList.Add(item.CITY);
            }
            if (!string.IsNullOrEmpty(item.ADDRESS))
            {
                paramList.Add(PARM_ADDRESS);
                paramList.Add(item.ADDRESS);
            }
            if (!string.IsNullOrEmpty(item.EMAIL))
            {
                paramList.Add(PARM_EMAIL);
                paramList.Add(item.EMAIL);
            }
            if (!string.IsNullOrEmpty(item.WEBSITE))
            {
                paramList.Add(PARM_WEBSITE);
                paramList.Add(item.WEBSITE);
            }
            if (!string.IsNullOrEmpty(item.DESCRIPTION))
            {
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.DESCRIPTION);
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
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
        /// 删除一条记录
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
        public bool Update(OrgInfo item)
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
            //if (!string.IsNullOrEmpty(item.Place))
            //{
                paramList.Add(PARM_PLACE);
                paramList.Add(item.Place);
            //}
            //if (item.Founddate != DateTime.MinValue)
            //{
                paramList.Add(PARM_FOUNDDATE);
                paramList.Add(item.Founddate.ToString("yyyy-MM-dd HH:mm:ss"));
            //}
            //if (!string.IsNullOrEmpty(item.TEL1))
            //{
                paramList.Add(PARM_TEL1);
                paramList.Add(item.TEL1);
            //}
            //if (!string.IsNullOrEmpty(item.TEL2))
            //{
                paramList.Add(PARM_TEL2);
                paramList.Add(item.TEL2);
            //}
            //if (!string.IsNullOrEmpty(item.Contractperson))
            //{
                paramList.Add(PARM_CONTRACTPERSON);
                paramList.Add(item.Contractperson);
            //}
            //if (!string.IsNullOrEmpty(item.Contracttel))
            //{
                paramList.Add(PARM_CONTRACTTEL);
                paramList.Add(item.Contracttel);
            //}
            //if (!string.IsNullOrEmpty(item.Remark))
            //{
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
            //}
            //if (item.Founddate != DateTime.MinValue)
            //{
                paramList.Add(PARM_SYS_FLD_ADDDATE);
                paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd"));
            //}
            //if (!string.IsNullOrEmpty(item.Sys_fld_Adduser))
            //{
                paramList.Add(PARM_SYS_FLD_ADDUSER);
                paramList.Add(item.Sys_fld_Adduser);
            //}
            //if (!string.IsNullOrEmpty(item.PROVINCE))
            //{
                paramList.Add(PARM_PROVINCE);
                paramList.Add(item.PROVINCE);
            //}
            //if (!string.IsNullOrEmpty(item.CITY))
            //{
                paramList.Add(PARM_CITY);
                paramList.Add(item.CITY);
            //}
            //if (!string.IsNullOrEmpty(item.ADDRESS))
            //{
                paramList.Add(PARM_ADDRESS);
                paramList.Add(item.ADDRESS);
            //}
            //if (!string.IsNullOrEmpty(item.EMAIL))
            //{
                paramList.Add(PARM_EMAIL);
                paramList.Add(item.EMAIL);
            //}
            //if (!string.IsNullOrEmpty(item.WEBSITE))
            //{
                paramList.Add(PARM_WEBSITE);
                paramList.Add(item.WEBSITE);
            //}
            //if (!string.IsNullOrEmpty(item.DESCRIPTION))
            //{
                paramList.Add(PARM_DESCRIPTION);
                paramList.Add(item.DESCRIPTION);
            //}
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());
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
        /// 根据id获得一条数据
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public OrgInfo GetItem(string doi)
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
                OrgInfo entry = new OrgInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Place = rs.GetValue(PARM_PLACE) ?? "";
                entry.Founddate = StructTrans.TransDate(rs.GetValue(PARM_FOUNDDATE));
                entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                entry.Contractperson = rs.GetValue(PARM_CONTRACTPERSON) ?? "";
                entry.Contracttel = rs.GetValue(PARM_CONTRACTTEL) ?? "";
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.PROVINCE = rs.GetValue(PARM_PROVINCE) ?? "";
                entry.CITY = rs.GetValue(PARM_CITY) ?? "";
                entry.ADDRESS = rs.GetValue(PARM_ADDRESS) ?? "";
                entry.EMAIL = rs.GetValue(PARM_EMAIL) ?? "";
                entry.WEBSITE = rs.GetValue(PARM_WEBSITE) ?? "";
                entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? "";
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
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
        public List<OrgInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<OrgInfo> entryList = new List<OrgInfo>();
                OrgInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new OrgInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Place = rs.GetValue(PARM_PLACE) ?? "";
                    entry.Founddate = StructTrans.TransDate(rs.GetValue(PARM_FOUNDDATE));
                    entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                    entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                    entry.Contractperson = rs.GetValue(PARM_CONTRACTPERSON) ?? "";
                    entry.Contracttel = rs.GetValue(PARM_CONTRACTTEL) ?? "";
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.PROVINCE = rs.GetValue(PARM_PROVINCE) ?? "";
                    entry.CITY = rs.GetValue(PARM_CITY) ?? "";
                    entry.ADDRESS = rs.GetValue(PARM_ADDRESS) ?? "";
                    entry.EMAIL = rs.GetValue(PARM_EMAIL) ?? "";
                    entry.WEBSITE = rs.GetValue(PARM_WEBSITE) ?? "";
                    entry.DESCRIPTION = rs.GetValue(PARM_DESCRIPTION) ?? "";
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
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
            //删除机构
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件记录条数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">机构的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改机构状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }
    }
}
