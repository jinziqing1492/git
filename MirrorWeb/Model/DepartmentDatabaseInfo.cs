using System;
using System.Collections.Generic;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// �Ƿֲ�ʽ���ݿ�Ķ�Ӧ��ϵ
    /// </summary>
    public class DepartmentDatabaseInfo
    {

        string _Departmentid;
        /// <summary>
        /// ���ź�
        /// </summary>
        public string Departmentid
        {
            get { return _Departmentid; }
            set { _Departmentid = value; }
        }

        string _ip;
        /// <summary>
        ///  ���ݿ�ip
        /// </summary>
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        string _port;
        /// <summary>
        /// ���ݿ�˿�
        /// </summary>
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        string _userName;
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        string _password;
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        string _tableName;
        /// <summary>
        ///  ����
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        string _dbtype;


        /// <summary>
        /// ���ݿ����� 0��ʾ��̬���ϣ�1��ʾ���2��ʾ��֯������3�����ƣ�4��ʾvsm
        /// </summary>
        public string Dbtype
        {
            get { return _dbtype; }
            set { _dbtype = value; }
        }

    }
}
