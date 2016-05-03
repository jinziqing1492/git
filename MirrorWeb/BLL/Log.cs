using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

using DRMS.Model;
using DRMS.IDAL;
using DRMS.DALFactory;
using Tool = CNKI.BaseFunction;

namespace DRMS.BLL
{
    public class Log
    {
        private static readonly ILog ReLog = SelectData.CreateLog();
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Add(LogInfo log)
        {
            if (null == log)
            {
                return false;
            }
            return ReLog.Add(log);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            LogInfo info = GetItem(id);
            if (info == null)
            {
                return false;
            }

            return ReLog.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                return false;
            }
            return ReLog.DeleteByWhere(strWhere);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        public bool Update(LogInfo book)
        {
            if (null == book)
            {
                return false;
            }

            return ReLog.Update(book);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogInfo GetItem(string id)
        {
            return ReLog.GetItem(id);
        }

        /// <summary>
        /// 获取多条
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="pageno"></param>
        /// <param name="pagecount"></param>
        /// <param name="textDatacount"></param>
        /// <returns></returns>
        public IList<LogInfo> GetList(string strwhere, int pageno, int pagecount, out int recordcount, bool IsAll)
        {
            return ReLog.GetList(strwhere, pageno, pagecount, out recordcount, IsAll);
        }

        /// <summary>
        /// 根据条件获取记录条数
        /// </summary>
        /// <param name="strWhere">查询条件  注：不需判断为空，为空默认为获取全部</param>
        /// <returns>记录条数</returns>
        public int GetCount(string strWhere)
        {
            return ReLog.GetCount(strWhere);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="dbType">数据类型，详细信息可参考枚举“DataBaseType”里面有详细注释</param>
        /// <param name="logType">操作类型，分为增删改 浏览和批量删除</param>
        /// <param name="doi">该条数据的doi</param>
        /// <param name="name">该条数据的名称</param>
        /// <param name="remark">备注</param>
        /// <returns>是否成功</returns>
        public bool Add(DataBaseType dbType, LogType logType, string doi, string name, string remark)
        {
            string ID = Tool.RandomId.GetGUID();
            string ipAddress = "127.0.0.1";
            if (HttpContext.Current != null && HttpContext.Current.Request != null)//可能winform程序用，所以Request可能为空
            {
                HttpRequest request = HttpContext.Current.Request;
                ipAddress = request.UserHostAddress;
            }
            if (ipAddress.ToLower().Equals("::1"))
            {
                ipAddress = "127.0.0.1";
            }
            string ipNum = Tool.IPConvert.IP2Int(ipAddress).ToString();
            string userName = "";
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }
            if (logType.Equals(LogType.LOGNON) || logType.Equals(LogType.LOGOUT))
            {
                userName = doi;
            }
            if (logType.Equals(LogType.ADD) && dbType.Equals(DataBaseType.USERDATA))
            {
                userName = doi;
            }
            if (string.IsNullOrEmpty(userName))
            {
                userName = "未登陆的用户";
            }
            //判断是机构用户还是个人用户
            int userType = 0;
            if (GetRole().Equals("1"))
            {
                userType = 1;
            }
            LogInfo info = new LogInfo()
            {
                Id = ID,
                Name = name,
                ResType = Convert.ToInt32(dbType),
                ResDoi = doi,
                LogType = Convert.ToInt32(logType),
                Remark = remark,
                Adddate = DateTime.Now,
                username = userName,
                Ip = ipAddress,
                IpNum = ipNum,
                userType = userType
            };

            Log bll = new Log();
            return bll.Add(info);
        }

        /// <summary>
        /// 获取热词
        /// </summary>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<LogInfo> GetHotWord(int pageCount)
        {
            return ReLog.GetHotWord(pageCount);
        }

        /// <summary>
        /// 获取当前用户角色
        /// </summary>
        /// <returns>角色</returns>
        public static string GetRole()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
                if (id == null)
                {
                    return "";
                }
                // 取得FormsAuthenticationTicket对象
                FormsAuthenticationTicket ticket = id.Ticket;
                if (ticket == null)
                {
                    return "";
                }
                //  取得UserData字段数据
                return ticket.UserData;
            }
            return "";
        }
    }
}
