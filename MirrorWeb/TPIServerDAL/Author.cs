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
    public class Author : IAuthor
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["Author"];
        #region IArticle 字段
        private const string PARM_NAME = "NAME";
        private const string PARM_SEX = "SEX";
        private const string PARM_OTHERNAME = "OTHERNAME";
        private const string PARM_BIRTHDAY = "BIRTHDAY";
        private const string PARM_HOMETOWN = "HOMETOWN";
        private const string PARM_ORGID = "ORGID";
        private const string PARM_ORGNAME = "ORGNAME";
        private const string PARM_TEL1 = "TEL1";
        private const string PARM_TEL2 = "TEL2";
        private const string PARM_EMAIL = "EMAIL";
        private const string PARM_DIGEST = "DIGEST";
        private const string PARM_SYS_FLD_FILEPATH = "SYS_FLD_FILEPATH";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_SYS_FLD_DOI = "SYS_FLD_DOI";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_SYS_FLD_ADDDATE = "SYS_FLD_ADDDATE";
        private const string PARM_SYS_FLD_ADDUSER = "SYS_FLD_ADDUSER";
        private const string PARM_IDNUM = "IDNUM";
        private const string PARM_COUNTRY = "COUNTRY";
        private const string PARM_WRITENAME = "WRITENAME";
        private const string PARM_POSITION = "POSITION";
        private const string PARM_ACADEMICTITLE = "ACADEMICTITLE";
        private const string PARM_ADDRESSNOW = "ADDRESSNOW";
        private const string PARM_MAINWORK = "MAINWORK";
        private const string PARM_SYS_FLD_CHECK_STATE = "SYS_FLD_CHECK_STATE";

        private const string PARM_FAX = "fax";
        private const string PARM_POSTCODE = "postcode";
        private const string PARM_BANKDEPOSIT = "bankdeposit";
        private const string PARM_ACCOUNTNUM = "accountnum";
        private const string PARM_OTHERCONTRACT = "othercontact";


        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion
        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(AuthorInfo item)
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
            paramList.Add(PARM_SEX);
            paramList.Add(item.Sex.ToString());
            if (!string.IsNullOrEmpty(item.Othername))
            {
                paramList.Add(PARM_OTHERNAME);
                paramList.Add(item.Othername);
            }
            if (item.Birthday != DateTime.MinValue)
            {
                paramList.Add(PARM_BIRTHDAY);
                paramList.Add(item.Birthday.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.Hometown))
            {
                paramList.Add(PARM_HOMETOWN);
                paramList.Add(item.Hometown);
            }
            if (!string.IsNullOrEmpty(item.OrgID))
            {
                paramList.Add(PARM_ORGID);
                paramList.Add(item.OrgID);
            }
            if (!string.IsNullOrEmpty(item.OrgName))
            {
                paramList.Add(PARM_ORGNAME);
                paramList.Add(item.OrgName);
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
            if (!string.IsNullOrEmpty(item.EMail))
            {
                paramList.Add(PARM_EMAIL);
                paramList.Add(item.EMail);
            }
            if (!string.IsNullOrEmpty(item.Digest))
            {
                paramList.Add(PARM_DIGEST);
                paramList.Add(item.Digest);
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
            if (!string.IsNullOrEmpty(item.Remark))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.Remark);
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
            if (!string.IsNullOrEmpty(item.IDNUM))
            {
                paramList.Add(PARM_IDNUM);
                paramList.Add(item.IDNUM);
            }
            if (!string.IsNullOrEmpty(item.COUNTRY))
            {
                paramList.Add(PARM_COUNTRY);
                paramList.Add(item.COUNTRY);
            }
            if (!string.IsNullOrEmpty(item.WRITENAME))
            {
                paramList.Add(PARM_WRITENAME);
                paramList.Add(item.WRITENAME);
            }
            if (!string.IsNullOrEmpty(item.POSITION))
            {
                paramList.Add(PARM_POSITION);
                paramList.Add(item.POSITION);
            }
            if (!string.IsNullOrEmpty(item.ACADEMICTITLE))
            {
                paramList.Add(PARM_ACADEMICTITLE);
                paramList.Add(item.ACADEMICTITLE);
            }
            if (!string.IsNullOrEmpty(item.ADDRESSNOW))
            {
                paramList.Add(PARM_ADDRESSNOW);
                paramList.Add(item.ADDRESSNOW);
            }
            if (!string.IsNullOrEmpty(item.Mainwork))
            {
                paramList.Add(PARM_MAINWORK);
                paramList.Add(item.Mainwork);
            }
            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());

            if (!string.IsNullOrEmpty(item.Fax))
            {
                paramList.Add(PARM_FAX);
                paramList.Add(item.Fax);
            }

            if (!string.IsNullOrEmpty(item.PostCode))
            {
                paramList.Add(PARM_POSTCODE);
                paramList.Add(item.PostCode);
            }

            if (!string.IsNullOrEmpty(item.BankDeposit))
            {
                paramList.Add(PARM_BANKDEPOSIT);
                paramList.Add(item.BankDeposit);
            }

            if (!string.IsNullOrEmpty(item.AccountNum))
            {
                paramList.Add(PARM_ACCOUNTNUM);
                paramList.Add(item.AccountNum);
            }


            if (!string.IsNullOrEmpty(item.Othercontact))
            {
                paramList.Add(PARM_OTHERCONTRACT);
                paramList.Add(item.Othercontact);
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
        /// 修改记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(AuthorInfo item)
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

            paramList.Add(PARM_SEX);
            paramList.Add(item.Sex.ToString());

            paramList.Add(PARM_OTHERNAME);
            paramList.Add(item.Othername);

            paramList.Add(PARM_BIRTHDAY);
            paramList.Add(item.Birthday.ToString("yyyy-MM-dd HH:mm:ss"));

            paramList.Add(PARM_HOMETOWN);
            paramList.Add(item.Hometown);

            paramList.Add(PARM_ORGID);
            paramList.Add(item.OrgID);

            paramList.Add(PARM_ORGNAME);
            paramList.Add(item.OrgName);

            paramList.Add(PARM_TEL1);
            paramList.Add(item.TEL1);

            paramList.Add(PARM_TEL2);
            paramList.Add(item.TEL2);

            paramList.Add(PARM_EMAIL);
            paramList.Add(item.EMail);

            paramList.Add(PARM_DIGEST);
            paramList.Add(item.Digest);

            paramList.Add(PARM_SYS_FLD_FILEPATH);
            paramList.Add(item.SYS_FLD_FILEPATH);

            paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
            paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);

            paramList.Add(PARM_REMARK);
            paramList.Add(item.Remark);

            paramList.Add(PARM_SYS_FLD_ADDDATE);
            paramList.Add(item.Sys_fld_Adddate.ToString("yyyy-MM-dd HH:mm:ss"));

            paramList.Add(PARM_SYS_FLD_ADDUSER);
            paramList.Add(item.Sys_fld_Adduser);

            paramList.Add(PARM_IDNUM);
            paramList.Add(item.IDNUM);

            paramList.Add(PARM_COUNTRY);
            paramList.Add(item.COUNTRY);

            paramList.Add(PARM_WRITENAME);
            paramList.Add(item.WRITENAME);

            paramList.Add(PARM_POSITION);
            paramList.Add(item.POSITION);

            paramList.Add(PARM_ACADEMICTITLE);
            paramList.Add(item.ACADEMICTITLE);

            paramList.Add(PARM_ADDRESSNOW);
            paramList.Add(item.ADDRESSNOW);

            paramList.Add(PARM_MAINWORK);
            paramList.Add(item.Mainwork);

            paramList.Add(PARM_SYS_FLD_CHECK_STATE);
            paramList.Add(item.SYS_FLD_CHECK_STATE.ToString());


            paramList.Add(PARM_FAX);
            paramList.Add(item.Fax);

            paramList.Add(PARM_POSTCODE);
            paramList.Add(item.PostCode);

            paramList.Add(PARM_BANKDEPOSIT);
            paramList.Add(item.BankDeposit);

            paramList.Add(PARM_ACCOUNTNUM);
            paramList.Add(item.AccountNum);

            paramList.Add(PARM_OTHERCONTRACT);
            paramList.Add(item.Othercontact);



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
        /// 得到一条记录
        /// </summary>
        /// <param name="doi"></param>
        /// <returns></returns>
        public AuthorInfo GetItem(string doi)
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
                AuthorInfo entry = new AuthorInfo();
                #region 判断字段并赋值
                entry.Name = rs.GetValue(PARM_NAME) ?? "";
                entry.Sex = StructTrans.TransNum(rs.GetValue(PARM_SEX));
                entry.Othername = rs.GetValue(PARM_OTHERNAME) ?? "";
                entry.Birthday = StructTrans.TransDate(rs.GetValue(PARM_BIRTHDAY));
                entry.Hometown = rs.GetValue(PARM_HOMETOWN) ?? "";
                entry.OrgID = rs.GetValue(PARM_ORGID) ?? "";
                entry.OrgName = rs.GetValue(PARM_ORGNAME) ?? "";
                entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                entry.EMail = rs.GetValue(PARM_EMAIL) ?? "";
                entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                entry.IDNUM = rs.GetValue(PARM_IDNUM) ?? "";
                entry.COUNTRY = rs.GetValue(PARM_COUNTRY) ?? "";
                entry.WRITENAME = rs.GetValue(PARM_WRITENAME) ?? "";
                entry.POSITION = rs.GetValue(PARM_POSITION) ?? "";
                entry.ACADEMICTITLE = rs.GetValue(PARM_ACADEMICTITLE) ?? "";
                entry.ADDRESSNOW = rs.GetValue(PARM_ADDRESSNOW) ?? "";
                entry.Mainwork = rs.GetValue(PARM_MAINWORK) ?? "";
                entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));

                entry.PostCode = rs.GetValue(PARM_POSTCODE) ?? "";
                entry.Othercontact = rs.GetValue(PARM_OTHERCONTRACT) ?? "";
                entry.Fax = rs.GetValue(PARM_FAX) ?? "";
                entry.AccountNum = rs.GetValue(PARM_ACCOUNTNUM) ?? "";
                entry.BankDeposit = rs.GetValue(PARM_BANKDEPOSIT) ?? "";

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
        /// 得到多条记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <param name="IsAll"></param>
        /// <returns></returns>
        public List<AuthorInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount, bool IsAll)
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
                List<AuthorInfo> entryList = new List<AuthorInfo>();
                AuthorInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new AuthorInfo();
                    #region 判断字段并赋值
                    entry.Name = rs.GetValue(PARM_NAME) ?? "";
                    entry.Sex = StructTrans.TransNum(rs.GetValue(PARM_SEX));
                    entry.Othername = rs.GetValue(PARM_OTHERNAME) ?? "";
                    entry.Birthday = StructTrans.TransDate(rs.GetValue(PARM_BIRTHDAY));
                    entry.Hometown = rs.GetValue(PARM_HOMETOWN) ?? "";
                    entry.OrgID = rs.GetValue(PARM_ORGID) ?? "";
                    entry.OrgName = rs.GetValue(PARM_ORGNAME) ?? "";
                    entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                    entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                    entry.EMail = rs.GetValue(PARM_EMAIL) ?? "";
                    entry.Digest = rs.GetValue(PARM_DIGEST) ?? "";
                    entry.SYS_FLD_FILEPATH = rs.GetValue(PARM_SYS_FLD_FILEPATH) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.SYS_FLD_DOI = rs.GetValue(PARM_SYS_FLD_DOI) ?? "";
                    entry.Remark = rs.GetValue(PARM_REMARK) ?? "";
                    entry.Sys_fld_Adddate = StructTrans.TransDate(rs.GetValue(PARM_SYS_FLD_ADDDATE));
                    entry.Sys_fld_Adduser = rs.GetValue(PARM_SYS_FLD_ADDUSER) ?? "";
                    entry.IDNUM = rs.GetValue(PARM_IDNUM) ?? "";
                    entry.COUNTRY = rs.GetValue(PARM_COUNTRY) ?? "";
                    entry.WRITENAME = rs.GetValue(PARM_WRITENAME) ?? "";
                    entry.POSITION = rs.GetValue(PARM_POSITION) ?? "";
                    entry.ACADEMICTITLE = rs.GetValue(PARM_ACADEMICTITLE) ?? "";
                    entry.ADDRESSNOW = rs.GetValue(PARM_ADDRESSNOW) ?? "";
                    entry.Mainwork = rs.GetValue(PARM_MAINWORK) ?? "";
                    entry.SYS_FLD_CHECK_STATE = StructTrans.TransNum(rs.GetValue(PARM_SYS_FLD_CHECK_STATE));

                    entry.PostCode = rs.GetValue(PARM_POSTCODE) ?? "";
                    entry.Othercontact = rs.GetValue(PARM_OTHERCONTRACT) ?? "";
                    entry.Fax = rs.GetValue(PARM_FAX) ?? "";
                    entry.AccountNum = rs.GetValue(PARM_ACCOUNTNUM) ?? "";
                    entry.BankDeposit = rs.GetValue(PARM_BANKDEPOSIT) ?? "";
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
            //删除作者
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} ", TABLE_NAME, strWhere);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string sqlWhere)
        {
            return TPIHelper.GetRecordsCount(TABLE_NAME, sqlWhere);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="id">音频的SYS_FLD_DOI</param>
        /// <param name="state">0为未审批 -1为审批通过</param>
        /// <returns></returns>
        public bool SetState(string id, int state)
        {
            //修改作者状态
            string strSet = string.Format("UPDATE {0} SET SYS_FLD_CHECK_STATE={1} WHERE SYS_FLD_DOI='{3}'", TABLE_NAME, state, id);
            return TPIHelper.ExecSql(strSet);
        }
    }
}
