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
    public class User : IUser
    {
        private static string TABLE_NAME = ConfigurationManager.AppSettings["User"];
        #region IArticle 字段
        private const string PARM_USERNAME = "USERNAME";
        private const string PARM_PASSWORD = "PASSWORD";
        private const string PARM_ROLE = "ROLE";
        private const string PARM_RIGHTSTR = "RIGHTSTR";
        private const string PARM_ISORG = "ISORG";
        private const string PARM_ONLINECOUNT = "ONLINECOUNT";
        private const string PARM_MAXONLINECOUNT = "MAXONLINECOUNT";
        private const string PARM_DEPARTMENT = "DEPARTMENT";
        private const string PARM_REALNAME = "REALNAME";
        private const string PARM_TEL1 = "TEL1";
        private const string PARM_TEL2 = "TEL2";
        private const string PARM_EMAIL = "EMAIL";
        private const string PARM_LEVEL = "LEVEL";
        private const string PARM_LASTVISIT = "LASTVISIT";
        private const string PARM_ADDDATE = "ADDDATE";
        private const string PARM_USERLOCKDATE = "USERLOCKDATE";
        private const string PARM_USERUNLOCKDATE = "USERUNLOCKDATE";
        private const string PARM_LASTEDITPASS = "LASTEDITPASS";
        private const string PARM_IPSTART = "IPSTART";
        private const string PARM_IPEND = "IPEND";
        private const string PARM_IPSTARTNUM = "IPSTARTNUM";
        private const string PARM_IPENDNUM = "IPENDNUM";
        private const string PARM_FLAG = "FLAG";
        private const string PARM_TRYNUM = "TRYNUM";
        private const string PARM_SHOWFLAG = "SHOWFLAG";
        private const string PARM_GROUPID = "GROUPID";
        private const string PARM_REMARK = "REMARK";
        private const string PARM_LOGINMODE = "LOGINMODE";
        private const string PARM_ICCARDNUM = "ICCARDNUM";
        private const string PARM_ICCARDEMAIL = "ICCARDEMAIL";
        private const string PARM_USERTYPE = "USERTYPE";
        private const string PARM_USERINTEREST = "USERINTEREST";
        private const string PARM_TERMINALCOUNT = "TERMINALCOUNT";
        private const string PARM_PWDQUESTION = "PWDQUESTION";
        private const string PARM_PWDANSWER = "PWDANSWER";

        private const string PARM_WORKUNIT = "WORKUNIT";
        private const string PARM_SYS_FLD_HEADPIC = "SYS_FLD_HEADPIC";
        private const string PARM_SYS_FLD_VIRTUALPATHTAG = "SYS_FLD_VIRTUALPATHTAG";
        private const string PARM_TOKEN = "TOKEN";

        private const string RED_LEFT = "##LEFT##";
        private const string RED_RIGHT = "##RIGHT##";
        #endregion

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddUser(UserInfo item)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.UserName))
            {
                paramList.Add(PARM_USERNAME);
                paramList.Add(item.UserName);
            }
            if (!string.IsNullOrEmpty(item.Password))
            {
                paramList.Add(PARM_PASSWORD);
                paramList.Add(item.Password);
            }
            if (!string.IsNullOrEmpty(item.Role))
            {
                paramList.Add(PARM_ROLE);
                paramList.Add(item.Role);
            }
            if (!string.IsNullOrEmpty(item.Rightstr))
            {
                paramList.Add(PARM_RIGHTSTR);
                paramList.Add(item.Rightstr);
            }
            paramList.Add(PARM_ISORG);
            paramList.Add(item.IsOrg.ToString());
            paramList.Add(PARM_ONLINECOUNT);
            paramList.Add(item.OnlineCount.ToString());
            paramList.Add(PARM_MAXONLINECOUNT);
            paramList.Add(item.MaxOnlineCount.ToString());
            if (!string.IsNullOrEmpty(item.DEPARTMENT))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.DEPARTMENT);
            }
            if (!string.IsNullOrEmpty(item.RealName))
            {
                paramList.Add(PARM_REALNAME);
                paramList.Add(item.RealName);
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
            paramList.Add(PARM_LEVEL);
            paramList.Add(item.LEVEL.ToString());
            if (item.LASTVISIT != DateTime.MinValue)
            {
                paramList.Add(PARM_LASTVISIT);
                paramList.Add(item.LASTVISIT.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.ADDDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_ADDDATE);
                paramList.Add(item.ADDDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.UserLockDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_USERLOCKDATE);
                paramList.Add(item.UserLockDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.UserUnlockDate != DateTime.MinValue)
            {
                paramList.Add(PARM_USERUNLOCKDATE);
                paramList.Add(item.UserUnlockDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.LASTEDITPASS != DateTime.MinValue)
            {
                paramList.Add(PARM_LASTEDITPASS);
                paramList.Add(item.LASTEDITPASS.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.IPstart))
            {
                paramList.Add(PARM_IPSTART);
                paramList.Add(item.IPstart);
            }
            if (!string.IsNullOrEmpty(item.Ipend))
            {
                paramList.Add(PARM_IPEND);
                paramList.Add(item.Ipend);
            }
            if (!string.IsNullOrEmpty(item.IpstartNum))
            {
                paramList.Add(PARM_IPSTARTNUM);
                paramList.Add(item.IpstartNum);
            }
            if (!string.IsNullOrEmpty(item.Ipendnum))
            {
                paramList.Add(PARM_IPENDNUM);
                paramList.Add(item.Ipendnum);
            }
            paramList.Add(PARM_FLAG);
            paramList.Add(item.Flag.ToString());
            paramList.Add(PARM_TRYNUM);
            paramList.Add(item.TRYNUM.ToString());
            paramList.Add(PARM_SHOWFLAG);
            paramList.Add(item.SHOWFLAG.ToString());
            if (!string.IsNullOrEmpty(item.GroupID))
            {
                paramList.Add(PARM_GROUPID);
                paramList.Add(item.GroupID);
            }
            if (!string.IsNullOrEmpty(item.REMARK))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.REMARK);
            }
            paramList.Add(PARM_LOGINMODE);
            paramList.Add(item.LOGINMODE.ToString());
            if (!string.IsNullOrEmpty(item.ICCardNum))
            {
                paramList.Add(PARM_ICCARDNUM);
                paramList.Add(item.ICCardNum);
            }
            if (!string.IsNullOrEmpty(item.ICCardEmail))
            {
                paramList.Add(PARM_ICCARDEMAIL);
                paramList.Add(item.ICCardEmail);
            }
            paramList.Add(PARM_USERTYPE);
            paramList.Add(item.UserType.ToString());
            if (!string.IsNullOrEmpty(item.UserInterest))
            {
                paramList.Add(PARM_USERINTEREST);
                paramList.Add(item.UserInterest);
            }
            paramList.Add(PARM_TERMINALCOUNT);
            paramList.Add(item.TERMINALCOUNT.ToString());
            if (!string.IsNullOrEmpty(item.PwdQuestion))
            {
                paramList.Add(PARM_PWDQUESTION);
                paramList.Add(item.PwdQuestion);
            }
            if (!string.IsNullOrEmpty(item.pwdAnswer))
            {
                paramList.Add(PARM_PWDANSWER);
                paramList.Add(item.pwdAnswer);
            }
            if (!string.IsNullOrEmpty(item.workUnit))
            {
                paramList.Add(PARM_WORKUNIT);
                paramList.Add(item.workUnit);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_HEADPIC))
            {
                paramList.Add(PARM_SYS_FLD_HEADPIC);
                paramList.Add(item.SYS_FLD_HEADPIC);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.TOKEN))
            {
                paramList.Add(PARM_TOKEN);
                paramList.Add(item.TOKEN);
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
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DelUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            string sqlDelete = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_USERNAME, username);
            return TPIHelper.ExecSql(sqlDelete);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ModifyUser(UserInfo item, string username)
        {
            if (item == null)
            {
                return false;
            }
            #region 赋值
            IList<string> paramList = new List<string>();
            if (!string.IsNullOrEmpty(item.Password))
            {
                paramList.Add(PARM_PASSWORD);
                paramList.Add(item.Password);
            }
            if (!string.IsNullOrEmpty(item.Role))
            {
                paramList.Add(PARM_ROLE);
                paramList.Add(item.Role);
            }
            if (!string.IsNullOrEmpty(item.Rightstr))
            {
                paramList.Add(PARM_RIGHTSTR);
                paramList.Add(item.Rightstr);
            }
            paramList.Add(PARM_ISORG);
            paramList.Add(item.IsOrg.ToString());
            paramList.Add(PARM_ONLINECOUNT);
            paramList.Add(item.OnlineCount.ToString());
            paramList.Add(PARM_MAXONLINECOUNT);
            paramList.Add(item.MaxOnlineCount.ToString());
            if (!string.IsNullOrEmpty(item.DEPARTMENT))
            {
                paramList.Add(PARM_DEPARTMENT);
                paramList.Add(item.DEPARTMENT);
            }
            if (!string.IsNullOrEmpty(item.RealName))
            {
                paramList.Add(PARM_REALNAME);
                paramList.Add(item.RealName);
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
            paramList.Add(PARM_LEVEL);
            paramList.Add(item.LEVEL.ToString());
            if (item.LASTVISIT != DateTime.MinValue)
            {
                paramList.Add(PARM_LASTVISIT);
                paramList.Add(item.LASTVISIT.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.ADDDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_ADDDATE);
                paramList.Add(item.ADDDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.UserLockDATE != DateTime.MinValue)
            {
                paramList.Add(PARM_USERLOCKDATE);
                paramList.Add(item.UserLockDATE.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.UserUnlockDate != DateTime.MinValue)
            {
                paramList.Add(PARM_USERUNLOCKDATE);
                paramList.Add(item.UserUnlockDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (item.LASTEDITPASS != DateTime.MinValue)
            {
                paramList.Add(PARM_LASTEDITPASS);
                paramList.Add(item.LASTEDITPASS.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (!string.IsNullOrEmpty(item.IPstart))
            {
                paramList.Add(PARM_IPSTART);
                paramList.Add(item.IPstart);
            }
            if (!string.IsNullOrEmpty(item.Ipend))
            {
                paramList.Add(PARM_IPEND);
                paramList.Add(item.Ipend);
            }
            if (!string.IsNullOrEmpty(item.IpstartNum))
            {
                paramList.Add(PARM_IPSTARTNUM);
                paramList.Add(item.IpstartNum);
            }
            if (!string.IsNullOrEmpty(item.Ipendnum))
            {
                paramList.Add(PARM_IPENDNUM);
                paramList.Add(item.Ipendnum);
            }
            paramList.Add(PARM_FLAG);
            paramList.Add(item.Flag.ToString());
            paramList.Add(PARM_TRYNUM);
            paramList.Add(item.TRYNUM.ToString());
            paramList.Add(PARM_SHOWFLAG);
            paramList.Add(item.SHOWFLAG.ToString());
            if (!string.IsNullOrEmpty(item.GroupID))
            {
                paramList.Add(PARM_GROUPID);
                paramList.Add(item.GroupID);
            }
            if (!string.IsNullOrEmpty(item.REMARK))
            {
                paramList.Add(PARM_REMARK);
                paramList.Add(item.REMARK);
            }
            paramList.Add(PARM_LOGINMODE);
            paramList.Add(item.LOGINMODE.ToString());
            if (!string.IsNullOrEmpty(item.ICCardNum))
            {
                paramList.Add(PARM_ICCARDNUM);
                paramList.Add(item.ICCardNum);
            }
            if (!string.IsNullOrEmpty(item.ICCardEmail))
            {
                paramList.Add(PARM_ICCARDEMAIL);
                paramList.Add(item.ICCardEmail);
            }
            paramList.Add(PARM_USERTYPE);
            paramList.Add(item.UserType.ToString());
            if (!string.IsNullOrEmpty(item.UserInterest))
            {
                paramList.Add(PARM_USERINTEREST);
                paramList.Add(item.UserInterest);
            }
            paramList.Add(PARM_TERMINALCOUNT);
            paramList.Add(item.TERMINALCOUNT.ToString());
            if (!string.IsNullOrEmpty(item.PwdQuestion))
            {
                paramList.Add(PARM_PWDQUESTION);
                paramList.Add(item.PwdQuestion);
            }
            if (!string.IsNullOrEmpty(item.pwdAnswer))
            {
                paramList.Add(PARM_PWDANSWER);
                paramList.Add(item.pwdAnswer);
            }
            if (!string.IsNullOrEmpty(item.workUnit))
            {
                paramList.Add(PARM_WORKUNIT);
                paramList.Add(item.workUnit);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_HEADPIC))
            {
                paramList.Add(PARM_SYS_FLD_HEADPIC);
                paramList.Add(item.SYS_FLD_HEADPIC);
            }
            if (!string.IsNullOrEmpty(item.SYS_FLD_VIRTUALPATHTAG))
            {
                paramList.Add(PARM_SYS_FLD_VIRTUALPATHTAG);
                paramList.Add(item.SYS_FLD_VIRTUALPATHTAG);
            }
            if (!string.IsNullOrEmpty(item.TOKEN))
            {
                paramList.Add(PARM_TOKEN);
                paramList.Add(item.TOKEN);
            }
            #endregion
            try
            {
                return TPIHelper.Update(TABLE_NAME, PARM_USERNAME + "='" + username + "'", paramList);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名获得一条记录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo GetItem(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_USERNAME, username);
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
                UserInfo entry = new UserInfo();
                #region 判断字段并赋值
                entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                entry.Password = rs.GetValue(PARM_PASSWORD) ?? "";
                entry.Role = rs.GetValue(PARM_ROLE);
                entry.Rightstr = rs.GetValue(PARM_RIGHTSTR) ?? "";
                entry.IsOrg = StructTrans.TransNum(rs.GetValue(PARM_ISORG));
                entry.OnlineCount = StructTrans.TransNum(rs.GetValue(PARM_ONLINECOUNT));
                entry.MaxOnlineCount = StructTrans.TransNum(rs.GetValue(PARM_MAXONLINECOUNT));
                entry.DEPARTMENT = rs.GetValue(PARM_DEPARTMENT) ?? "";
                entry.RealName = rs.GetValue(PARM_REALNAME) ?? "";
                entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                entry.EMail = rs.GetValue(PARM_EMAIL) ?? "";
                entry.LEVEL = StructTrans.TransNum(rs.GetValue(PARM_LEVEL));
                entry.LASTVISIT = StructTrans.TransDate(rs.GetValue(PARM_LASTVISIT));
                entry.ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                entry.UserLockDATE = StructTrans.TransDate(rs.GetValue(PARM_USERLOCKDATE));
                entry.UserUnlockDate = StructTrans.TransDate(rs.GetValue(PARM_USERUNLOCKDATE));
                entry.LASTEDITPASS = StructTrans.TransDate(rs.GetValue(PARM_LASTEDITPASS));
                entry.IPstart = rs.GetValue(PARM_IPSTART) ?? "";
                entry.Ipend = rs.GetValue(PARM_IPEND) ?? "";
                entry.IpstartNum = rs.GetValue(PARM_IPSTARTNUM) ?? "";
                entry.Ipendnum = rs.GetValue(PARM_IPENDNUM) ?? "";
                entry.Flag = StructTrans.TransNum(rs.GetValue(PARM_FLAG));
                entry.TRYNUM = StructTrans.TransNum(rs.GetValue(PARM_TRYNUM));
                entry.SHOWFLAG = StructTrans.TransNum(rs.GetValue(PARM_SHOWFLAG));
                entry.GroupID = rs.GetValue(PARM_GROUPID) ?? "";
                entry.REMARK = rs.GetValue(PARM_REMARK) ?? "";
                entry.LOGINMODE = StructTrans.TransNum(rs.GetValue(PARM_LOGINMODE));
                entry.ICCardNum = rs.GetValue(PARM_ICCARDNUM) ?? "";
                entry.ICCardEmail = rs.GetValue(PARM_ICCARDEMAIL) ?? "";
                entry.UserType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                entry.UserInterest = rs.GetValue(PARM_USERINTEREST) ?? "";
                entry.TERMINALCOUNT = StructTrans.TransNum(rs.GetValue(PARM_TERMINALCOUNT));
                entry.PwdQuestion = rs.GetValue(PARM_PWDQUESTION) ?? "";
                entry.pwdAnswer = rs.GetValue(PARM_PWDANSWER) ?? "";
                entry.workUnit = rs.GetValue(PARM_WORKUNIT) ?? "";
                entry.SYS_FLD_HEADPIC = rs.GetValue(PARM_SYS_FLD_HEADPIC) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.TOKEN = rs.GetValue(PARM_TOKEN) ?? "";
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
        /// 根据用户名获得一条记录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo GetUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", TABLE_NAME, PARM_USERNAME, username);
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
                UserInfo entry = new UserInfo();
                #region 判断字段并赋值
                entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                entry.Password = rs.GetValue(PARM_PASSWORD) ?? "";
                entry.Role = rs.GetValue(PARM_ROLE);
                entry.Rightstr = rs.GetValue(PARM_RIGHTSTR) ?? "";
                entry.IsOrg = StructTrans.TransNum(rs.GetValue(PARM_ISORG));
                entry.OnlineCount = StructTrans.TransNum(rs.GetValue(PARM_ONLINECOUNT));
                entry.MaxOnlineCount = StructTrans.TransNum(rs.GetValue(PARM_MAXONLINECOUNT));
                entry.DEPARTMENT = rs.GetValue(PARM_DEPARTMENT) ?? "";
                entry.RealName = rs.GetValue(PARM_REALNAME) ?? "";
                entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                entry.EMail = rs.GetValue(PARM_EMAIL) ?? "";
                entry.LEVEL = StructTrans.TransNum(rs.GetValue(PARM_LEVEL));
                entry.LASTVISIT = StructTrans.TransDate(rs.GetValue(PARM_LASTVISIT));
                entry.ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                entry.UserLockDATE = StructTrans.TransDate(rs.GetValue(PARM_USERLOCKDATE));
                entry.UserUnlockDate = StructTrans.TransDate(rs.GetValue(PARM_USERUNLOCKDATE));
                entry.LASTEDITPASS = StructTrans.TransDate(rs.GetValue(PARM_LASTEDITPASS));
                entry.IPstart = rs.GetValue(PARM_IPSTART) ?? "";
                entry.Ipend = rs.GetValue(PARM_IPEND) ?? "";
                entry.IpstartNum = rs.GetValue(PARM_IPSTARTNUM) ?? "";
                entry.Ipendnum = rs.GetValue(PARM_IPENDNUM) ?? "";
                entry.Flag = StructTrans.TransNum(rs.GetValue(PARM_FLAG));
                entry.TRYNUM = StructTrans.TransNum(rs.GetValue(PARM_TRYNUM));
                entry.SHOWFLAG = StructTrans.TransNum(rs.GetValue(PARM_SHOWFLAG));
                entry.GroupID = rs.GetValue(PARM_GROUPID) ?? "";
                entry.REMARK = rs.GetValue(PARM_REMARK) ?? "";
                entry.LOGINMODE = StructTrans.TransNum(rs.GetValue(PARM_LOGINMODE));
                entry.ICCardNum = rs.GetValue(PARM_ICCARDNUM) ?? "";
                entry.ICCardEmail = rs.GetValue(PARM_ICCARDEMAIL) ?? "";
                entry.UserType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                entry.UserInterest = rs.GetValue(PARM_USERINTEREST) ?? "";
                entry.TERMINALCOUNT = StructTrans.TransNum(rs.GetValue(PARM_TERMINALCOUNT));
                entry.PwdQuestion = rs.GetValue(PARM_PWDQUESTION) ?? "";
                entry.pwdAnswer = rs.GetValue(PARM_PWDANSWER) ?? "";
                entry.workUnit = rs.GetValue(PARM_WORKUNIT) ?? "";
                entry.SYS_FLD_HEADPIC = rs.GetValue(PARM_SYS_FLD_HEADPIC) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.TOKEN = rs.GetValue(PARM_TOKEN) ?? "";
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
        /// <returns></returns>
        public List<UserInfo> GetList(string sqlWhere, int pageNo, int pageCount, out int recordCount)
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
                List<UserInfo> entryList = new List<UserInfo>();
                UserInfo entry = null;
                for (int i = 0; i < pageCount; i++)
                {
                    entry = new UserInfo();
                    #region 判断字段并赋值
                    entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                    entry.Password = rs.GetValue(PARM_PASSWORD) ?? "";
                    entry.Role = rs.GetValue(PARM_ROLE);
                    entry.Rightstr = rs.GetValue(PARM_RIGHTSTR) ?? "";
                    entry.IsOrg = StructTrans.TransNum(rs.GetValue(PARM_ISORG));
                    entry.OnlineCount = StructTrans.TransNum(rs.GetValue(PARM_ONLINECOUNT));
                    entry.MaxOnlineCount = StructTrans.TransNum(rs.GetValue(PARM_MAXONLINECOUNT));
                    entry.DEPARTMENT = rs.GetValue(PARM_DEPARTMENT) ?? "";
                    entry.RealName = rs.GetValue(PARM_REALNAME) ?? "";
                    entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                    entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                    entry.EMail = rs.GetValue(PARM_EMAIL) ?? "";
                    entry.LEVEL = StructTrans.TransNum(rs.GetValue(PARM_LEVEL));
                    entry.LASTVISIT = StructTrans.TransDate(rs.GetValue(PARM_LASTVISIT));
                    entry.ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                    entry.UserLockDATE = StructTrans.TransDate(rs.GetValue(PARM_USERLOCKDATE));
                    entry.UserUnlockDate = StructTrans.TransDate(rs.GetValue(PARM_USERUNLOCKDATE));
                    entry.LASTEDITPASS = StructTrans.TransDate(rs.GetValue(PARM_LASTEDITPASS));
                    entry.IPstart = rs.GetValue(PARM_IPSTART) ?? "";
                    entry.Ipend = rs.GetValue(PARM_IPEND) ?? "";
                    entry.IpstartNum = rs.GetValue(PARM_IPSTARTNUM) ?? "";
                    entry.Ipendnum = rs.GetValue(PARM_IPENDNUM) ?? "";
                    entry.Flag = StructTrans.TransNum(rs.GetValue(PARM_FLAG));
                    entry.TRYNUM = StructTrans.TransNum(rs.GetValue(PARM_TRYNUM));
                    entry.SHOWFLAG = StructTrans.TransNum(rs.GetValue(PARM_SHOWFLAG));
                    entry.GroupID = rs.GetValue(PARM_GROUPID) ?? "";
                    entry.REMARK = rs.GetValue(PARM_REMARK) ?? "";
                    entry.LOGINMODE = StructTrans.TransNum(rs.GetValue(PARM_LOGINMODE));
                    entry.ICCardNum = rs.GetValue(PARM_ICCARDNUM) ?? "";
                    entry.ICCardEmail = rs.GetValue(PARM_ICCARDEMAIL) ?? "";
                    entry.UserType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                    entry.UserInterest = rs.GetValue(PARM_USERINTEREST) ?? "";
                    entry.TERMINALCOUNT = StructTrans.TransNum(rs.GetValue(PARM_TERMINALCOUNT));
                    entry.PwdQuestion = rs.GetValue(PARM_PWDQUESTION) ?? "";
                    entry.pwdAnswer = rs.GetValue(PARM_PWDANSWER) ?? "";
                    entry.workUnit = rs.GetValue(PARM_WORKUNIT) ?? "";
                    entry.SYS_FLD_HEADPIC = rs.GetValue(PARM_SYS_FLD_HEADPIC) ?? "";
                    entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                    entry.TOKEN = rs.GetValue(PARM_TOKEN) ?? "";
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
        /// 设置用户角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool SetUserRole(string role, string username)
        {
            IList<string> paralist = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return false;
                }
                else
                {
                    paralist.Add(PARM_ROLE);
                    paralist.Add(role);
                    if (TPIHelper.Update(TABLE_NAME, PARM_USERNAME + "='" + username + "'", paralist))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="right"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool SetUserRight(string right, string username)
        {
            IList<string> paralist = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return false;
                }
                else
                {
                    paralist.Add(PARM_RIGHTSTR);
                    paralist.Add(right);
                    if (TPIHelper.Update(TABLE_NAME, PARM_USERNAME + "='" + username + "'", paralist))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 设置密级
        /// </summary>
        /// <param name="level"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool SetLevel(string level, string username)
        {
            if (string.IsNullOrEmpty(level) || string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (TPIHelper.Update(TABLE_NAME, PARM_USERNAME + "='" + username + "'", PARM_LEVEL, level))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ModifyPwd(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                IList<string> par = new List<string>();
                par.Add(PARM_PASSWORD);
                par.Add(password);
                par.Add(PARM_LASTEDITPASS);
                par.Add(DateControl.ChangeDate(DateTime.Now));

                if (TPIHelper.Update(TABLE_NAME, PARM_USERNAME + "='" + username + "' ", par))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="username"></param>
        /// <param name="flag">0是正常 1是锁定 2是禁用</param>
        /// <returns></returns>
        public bool SetFlag(string username, int flag)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            string sql = "update " + TABLE_NAME + " set " + PARM_FLAG + "='" + flag.ToString() + "'";
            if (flag == 0)
            {
                sql += " , " + PARM_USERUNLOCKDATE + "='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            else
            {
                sql += " , " + PARM_USERLOCKDATE + "='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            sql += " where " + PARM_USERNAME + "='" + username + "'";
            return TPIHelper.ExecSql(sql);
        }
        /// <summary>
        /// 增加登录错误次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AddTryNum(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            string sql = "update " + TABLE_NAME + " set " + PARM_TRYNUM + "=" + PARM_TRYNUM + "+1";
            sql += " where " + PARM_USERNAME + "='" + username + "' ";
            return TPIHelper.ExecSql(sql);
        }
        /// <summary>
        /// 获取登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetTryNum(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return -1;
            }
            string sql = "select * from " + TABLE_NAME + " where " + PARM_USERNAME + "='" + username + "'";
            RecordSet rs = TPIHelper.GetRecordSet(sql);
            if (null == rs)
            {
                return -1;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return -1;
            }
            try
            {
                return StructTrans.TransNum(rs.GetValue(PARM_TRYNUM));
            }
            catch
            {
                return -1;
            }
            finally
            {
                rs.Close();
            }
        }
        /// <summary>
        /// 添加登录时间
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AddLoginFlag(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            string sql = "update " + TABLE_NAME + " set " + PARM_LASTVISIT + "='" + DateControl.ChangeDate(DateTime.Now) + "'";
            sql += " where " + PARM_USERNAME + "='" + username + "'";
            return TPIHelper.ExecSql(sql);
        }
        /// <summary>
        /// 上次修改密码时间
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AddEditPassFlag(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            string sql = "update " + TABLE_NAME + " set " + PARM_LASTEDITPASS + "='" + DateControl.ChangeDate(DateTime.Now) + "'";
            sql += " where " + PARM_USERNAME + "='" + username + "'";
            return TPIHelper.ExecSql(sql);
        }
        /// <summary>
        /// 尝试登录次数清零
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ResetTryNum(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            string sql = "update " + TABLE_NAME + " set " + PARM_TRYNUM + "='0'";
            sql += " where " + PARM_USERNAME + "='" + username + "'";
            return TPIHelper.ExecSql(sql);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfo UserLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            string sqlQuery = string.Format("SELECT * FROM {0} WHERE {1} = \"{2}\" and {3}=\"{4}\"", TABLE_NAME, PARM_USERNAME, username,PARM_PASSWORD,password);
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
                UserInfo entry = new UserInfo();
                #region 判断字段并赋值
                entry.UserName = rs.GetValue(PARM_USERNAME) ?? "";
                entry.Password = rs.GetValue(PARM_PASSWORD) ?? "";
                entry.Role = rs.GetValue(PARM_ROLE);
                entry.Rightstr = rs.GetValue(PARM_RIGHTSTR) ?? "";
                entry.IsOrg = StructTrans.TransNum(rs.GetValue(PARM_ISORG));
                entry.OnlineCount = StructTrans.TransNum(rs.GetValue(PARM_ONLINECOUNT));
                entry.MaxOnlineCount = StructTrans.TransNum(rs.GetValue(PARM_MAXONLINECOUNT));
                entry.DEPARTMENT = rs.GetValue(PARM_DEPARTMENT) ?? "";
                entry.RealName = rs.GetValue(PARM_REALNAME) ?? "";
                entry.TEL1 = rs.GetValue(PARM_TEL1) ?? "";
                entry.TEL2 = rs.GetValue(PARM_TEL2) ?? "";
                entry.EMail = rs.GetValue(PARM_EMAIL) ?? "";
                entry.LEVEL = StructTrans.TransNum(rs.GetValue(PARM_LEVEL));
                entry.LASTVISIT = StructTrans.TransDate(rs.GetValue(PARM_LASTVISIT));
                entry.ADDDATE = StructTrans.TransDate(rs.GetValue(PARM_ADDDATE));
                entry.UserLockDATE = StructTrans.TransDate(rs.GetValue(PARM_USERLOCKDATE));
                entry.UserUnlockDate = StructTrans.TransDate(rs.GetValue(PARM_USERUNLOCKDATE));
                entry.LASTEDITPASS = StructTrans.TransDate(rs.GetValue(PARM_LASTEDITPASS));
                entry.IPstart = rs.GetValue(PARM_IPSTART) ?? "";
                entry.Ipend = rs.GetValue(PARM_IPEND) ?? "";
                entry.IpstartNum = rs.GetValue(PARM_IPSTARTNUM) ?? "";
                entry.Ipendnum = rs.GetValue(PARM_IPENDNUM) ?? "";
                entry.Flag = StructTrans.TransNum(rs.GetValue(PARM_FLAG));
                entry.TRYNUM = StructTrans.TransNum(rs.GetValue(PARM_TRYNUM));
                entry.SHOWFLAG = StructTrans.TransNum(rs.GetValue(PARM_SHOWFLAG));
                entry.GroupID = rs.GetValue(PARM_GROUPID) ?? "";
                entry.REMARK = rs.GetValue(PARM_REMARK) ?? "";
                entry.LOGINMODE = StructTrans.TransNum(rs.GetValue(PARM_LOGINMODE));
                entry.ICCardNum = rs.GetValue(PARM_ICCARDNUM) ?? "";
                entry.ICCardEmail = rs.GetValue(PARM_ICCARDEMAIL) ?? "";
                entry.UserType = StructTrans.TransNum(rs.GetValue(PARM_USERTYPE));
                entry.UserInterest = rs.GetValue(PARM_USERINTEREST) ?? "";
                entry.TERMINALCOUNT = StructTrans.TransNum(rs.GetValue(PARM_TERMINALCOUNT));
                entry.PwdQuestion = rs.GetValue(PARM_PWDQUESTION) ?? "";
                entry.pwdAnswer = rs.GetValue(PARM_PWDANSWER) ?? "";
                entry.workUnit = rs.GetValue(PARM_WORKUNIT) ?? "";
                entry.SYS_FLD_HEADPIC = rs.GetValue(PARM_SYS_FLD_HEADPIC) ?? "";
                entry.SYS_FLD_VIRTUALPATHTAG = rs.GetValue(PARM_SYS_FLD_VIRTUALPATHTAG) ?? "";
                entry.TOKEN = rs.GetValue(PARM_TOKEN) ?? "";
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
        ///查看用户名是否可用
        /// </summary>
        /// <param name="username"></param>
        /// <returns>true用户名可以用false用户名不可用</returns>
        public bool UserNameCanUse(string username)
        {
            if (TPIHelper.GetDataCount(TABLE_NAME, PARM_USERNAME + "='" + username + "'") > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region IUser 成员

        /// <summary>
        /// 获当前取终端数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetTerminalNum(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return -1;
            }
            string sql = "select * from " + TABLE_NAME + " where " + PARM_USERNAME + "='" + username + "'";
            RecordSet rs = TPIHelper.GetRecordSet(sql);
            if (null == rs)
            {
                return -1;
            }
            if (rs.GetCount() <= 0)
            {
                rs.Close();
                return -1;
            }
            try
            {
                return StructTrans.TransNum(rs.GetValue(PARM_TERMINALCOUNT));
            }
            catch
            {
                return -1;
            }
            finally
            {
                rs.Close();
            }
        }
        /// <summary>
        /// 设置终端数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool SetTerminalNum(string username,int terminalcount)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            string sql = "update " + TABLE_NAME + " set " + PARM_TERMINALCOUNT + "=" + terminalcount + "";
            sql += " where " + PARM_USERNAME + "='" + username + "'";
            return TPIHelper.ExecSql(sql);
        }
        #endregion

    }
}
