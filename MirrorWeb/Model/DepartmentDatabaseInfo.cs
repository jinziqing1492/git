using System;
using System.Collections.Generic;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 是分布式数据库的对应关系
    /// </summary>
    public class DepartmentDatabaseInfo
    {

        string _Departmentid;
        /// <summary>
        /// 部门号
        /// </summary>
        public string Departmentid
        {
            get { return _Departmentid; }
            set { _Departmentid = value; }
        }

        string _ip;
        /// <summary>
        ///  数据库ip
        /// </summary>
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        string _port;
        /// <summary>
        /// 数据库端口
        /// </summary>
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        string _password;
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        string _tableName;
        /// <summary>
        ///  表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        string _dbtype;


        /// <summary>
        /// 数据库类型 0表示动态资料，1表示人物，2表示组织机构，3二进制，4表示vsm
        /// </summary>
        public string Dbtype
        {
            get { return _dbtype; }
            set { _dbtype = value; }
        }

    }
}
