using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DRMS.Model;

namespace DRMS.IDAL
{
    public interface IUser
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUser(UserInfo user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool DelUser(string username);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool ModifyUser(UserInfo user, string username);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool ModifyPwd(string username, string password);

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="sqlstr">SQL</param>
        /// <param name="pageno">页码</param>
        /// <param name="pagecount">每页条数</param>
        /// <param name="recordcount">总记录数</param>
        /// <returns></returns>
        List<UserInfo> GetList(string sqlstr, int pageno, int pagecount, out int recordcount);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserInfo UserLogin(string username, string password);

        /// <summary>
        /// 判断用户名是否可用
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        bool UserNameCanUse(string username);

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        UserInfo GetUser(string username);

        /// <summary>
        /// 设置用户状态1正常使用0删除-1锁定-2禁用
        /// </summary>
        /// <param name="username"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        bool SetFlag(string username, int flag);

        /// <summary>
        /// 增加尝试登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool AddTryNum(string username);

        /// <summary>
        /// 获取尝试登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int GetTryNum(string username);

        /// <summary>
        /// 重置最后登录时间
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool AddLoginFlag(string username);

        /// <summary>
        /// 重置最后修改密码时间
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool AddEditPassFlag(string username);

        /// <summary>
        /// 重置尝试登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool ResetTryNum(string username);
    }
}
