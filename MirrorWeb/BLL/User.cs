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
    public class User
    {
        private readonly static IUser ReUser = SelectData.CreateUser();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageCount"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<UserInfo> GetList(string strWhere ,int pageNo ,int pageCount ,out int recordCount)
        {
            return ReUser.GetList(strWhere ,pageNo ,pageCount ,out recordCount);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string doi)
        {
            return ReUser.DelUser(doi);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ModifyUser(UserInfo user ,string username)
        {
            return ReUser.ModifyUser(user ,username);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ModifyPwd(string username ,string password)
        {
            return ReUser.ModifyPwd(username ,password);
        }
        /// <summary>
        ///添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(UserInfo user)
        {
            return ReUser.AddUser(user);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfo UserLogin(string username ,string password)
        {
            return ReUser.UserLogin(username ,password);
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>角色</returns>
        public string GetUserRole(UserInfo user)
        {
            if(user == null)
            {
                return "user";
            }
            return GetUserRole(user.Rightstr);
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="rightFlag">用户权限标识：0 user(普通用户) 1 supplier(供应商) 2 admin（管理员）</param>
        /// <returns>角色</returns>
        public static string GetUserRole(string rightFlag)
        {
            string role = "";
            if(rightFlag == "1")
            {
                role = "supplier";
            }
            else if(rightFlag == "2")
            {
                role = "admin";
            }
            else
            {
                role = "user";
            }
            return role;
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns>名称</returns>
        public static string GetRoleName(string role)
        {
            string name = "";
            switch(role)
            {
                case "user":
                    name = "普通用户";
                    break;
                case "supplier":
                    name = "内容管理员";
                    break;
                case "admin":
                    name = "系统管理员";
                    break;
                default:
                    break;
            }
            return name;
        }

        /// <summary>
        /// 用户名是否可用
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UserNameCanUse(string username)
        {
            return ReUser.UserNameCanUse(username);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo GetUser(string username)
        {
            return ReUser.GetUser(username);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="username"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool SetFlag(string username ,int flag)
        {
            return ReUser.SetFlag(username ,flag);
        }

        /// <summary>
        /// 添加登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AddTryNum(string username)
        {
            return ReUser.AddTryNum(username);
        }

        /// <summary>
        /// 获取测试登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetTryNum(string username)
        {
            return ReUser.GetTryNum(username);
        }

        /// <summary>
        /// 添加登录时间
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AddLoginFlag(string username)
        {
            return ReUser.AddLoginFlag(username);
        }

        /// <summary>
        /// 添加用户上次修改时间
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AddEditPassFlag(string username)
        {
            return ReUser.AddEditPassFlag(username);
        }

        /// <summary>
        /// 重置尝试次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ResetTryNum(string username)
        {
            return ReUser.ResetTryNum(username);
        }
    }
}
