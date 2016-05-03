using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.Win32;
using System.Configuration;
using System.Data.SqlClient;

using TPI;
using TPIBINREADERLib;

namespace DRMS.TPIServerDAL
{

    /// <summary>
    /// ���unicode�ֶε�ʱ����
    /// </summary>
    //public class FieldInfo
    //{
    //    string fieldName;
    //     /// <summary>
    //     ///  ���ݿ���ֶ���
    //     /// </summary>
    //    public string FieldName
    //    {
    //        get { return fieldName; }
    //        set { fieldName = value; }
    //    }

    //    string fieldValue;
    //    /// <summary>
    //    /// ���ݿ���ֶ�ֵ
    //    /// </summary>
    //    public string FieldValue
    //    {
    //        get { return fieldValue; }
    //        set { fieldValue = value; }
    //    }
    //}

    /// <summary>
    /// KBASE�����ݿ������
    /// </summary>
    public abstract class TPIHelper
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public TPIHelper()
        {

        }
        static string _TPIServerIP = ConfigurationManager.AppSettings["IP"].ToString();
        static string _TPIUserName = ConfigurationManager.AppSettings["UserName"].ToString();
        static string _TPIPassword = ConfigurationManager.AppSettings["Password"].ToString();
        static string _TPIPort = ConfigurationManager.AppSettings["Port"].ToString();
        static TPI.Client _Client = new Client();
        static TPIBINREADERLib.TPIConn _BinConn;
        /// <summary>
        /// ���ݲ�ѯ���������ؽ����,ʹ����Ͻ���������ֶ��ر�
        /// </summary>
        /// <param name="strSql">��ѯ����</param>
        /// <returns>�����</returns>
        public static RecordSet GetRecordSet(string strSql)
        {
            if (!_Client.IsConnected())
            {
                _Client.Close();
                _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
            }
            RecordSet rst = _Client.OpenRecordSet(strSql);
            return rst;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static RecordSet GetRecordSet_U(string strSql)
        {
            if (!_Client.IsConnected())
            {
                _Client.Close();
                _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
            }
            RecordSet rst = _Client.OpenRecordSet_U(strSql);

            return rst;
        }
        /// <summary>
        /// ������ʽ�ر����Ӽ������
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static RecordSet GetRecordSet(string strSql, TPI.Client client)
        {
            if (!client.IsConnected())
            {
                return null;
            }
            RecordSet rst = client.OpenRecordSet(strSql);
            return rst;
        }
        /// <summary>
        /// ������ʽ�ر����Ӽ������
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static RecordSet GetRecordSet_U(string strSql, TPI.Client client)
        {
            if (!client.IsConnected())
            {
                return null;
            }
            RecordSet rst = client.OpenRecordSet_U(strSql);
            return rst;
        }
        /// <summary>
        /// �ɱ���,�����������ؽ��,ʹ����Ͻ���������ֶ��ر�
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <returns>�����</returns>
        /// <remarks > </remarks>
        public static RecordSet GetRecordSetByCondition(string Table, string Condition)
        {
            try
            {
                string strSql = "SELECT  * FROM " + Table + " WHERE " + Condition;
                return GetRecordSet(strSql);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        /// <summary>
        /// �ɱ���,�����������ؽ��,ʹ����Ͻ���������ֶ��ر�
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <returns>�����</returns>
        /// <remarks > </remarks>
        public static RecordSet GetRecordSetByCondition(string Table, string Condition, TPI.Client client)
        {
            if (!client.IsConnected())
            {
                return null;
            }
            try
            {
                string strSql = "SELECT  * FROM " + Table + " WHERE " + Condition;
                return client.OpenRecordSet(strSql);// GetRecordSet(strSql);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        /// <summary>
        /// ��ȡ�б�ֻ��ȡ�����ֶ�
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Condition"></param>
        /// <param name="Fields"></param>
        /// <returns></returns>
        public static RecordSet GetRecordPartField(string Table, string Condition, string Fields)
        {
            try
            {
                string strSql = "SELECT " + Fields + " FROM " + Table + " WHERE " + Condition;
                return GetRecordSet(strSql);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// �ɱ���,�����������ؽ��,ʹ����Ͻ���������ֶ��ر�
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <returns>�����</returns>
        /// <remarks > </remarks>
        public static RecordSet GetRecordSetByConditionNoCache(string Table, string Condition)
        {
            try
            {
                string strSql = "SELECT SQL_NO_CACHE * FROM " + Table + " WHERE " + Condition;
                return GetRecordSet(strSql);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        /// <summary>
        /// ���ݱ����Ͳ�ѯ������ȡ�÷��������ļ�¼����ʹ����Ͻ���������ֶ��ر�
        /// </summary>
        /// <param name="tableName">���ݱ���</param>
        /// <param name="queryCondition">��ѯ����</param>
        /// <returns>���ط��������ļ�¼����</returns>
        public static int GetRecordsCount(string tableName, string queryCondition)
        {
            string sqlQuery = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tableName, queryCondition);
            RecordSet recordSet = null;
            try
            {
                recordSet = GetRecordSet(sqlQuery);
                if (recordSet == null || recordSet.GetCount() <= 0)
                {
                    return 0;
                }
                int count = 0;
                Int32.TryParse(recordSet.GetValue(0), out count);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (recordSet != null)
                    recordSet.Close();
            }
        }

        /// <summary>
        /// ��ȡһ�����������ݲ����ļ�¼��
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DOBReader GetDOBReader(string sql)
        {
            if (_BinConn == null)
            {
                _BinConn = new TPIConn();
            }
            if (_BinConn.IsConnected <= 0)
            {
                _BinConn.OpenConn(_TPIServerIP, 4567, _TPIUserName, _TPIPassword);
            }
            DOBReader rs = new DOBReader();
            rs.OpenRecordSet(sql, _BinConn);
            return rs;
        }
        /// <summary>
        /// ��ȡһ�����������ݲ����ļ�¼��
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DOBReader GetDOBReader(string sql, TPIConn conn)
        {
            if (null == conn)
            {
                return null;
            }
            if (conn.IsConnected <= 0)
            {
                return null;
            }
            DOBReader rs = new DOBReader();
            rs.OpenRecordSet(sql, conn);
            return rs;
        }
        /// <summary>
        /// д�����������
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="bindata">����������</param>
        /// <param name="conn">����</param>
        /// <param name="ncol">���������ݵ��к�</param>
        /// <param name="perlen">ÿ��д��Ĵ�С Ĭ��8192(4K)</param>
        /// <returns></returns>
        public static bool AddBinData(string sql, string bindata, TPIConn conn, int ncol, int perlen)
        {
            if (conn.IsConnected <= 0)
            {
                return false;
            }
            DOBReader rs = GetDOBReader(sql, conn);
            if (null == rs)
            {
                return false;
            }
            if (rs.IsBOF > 0 && rs.IsEOF > 0)
            {
                rs.CloseRecordSet();
                return false;
            }
            int nowflag = 0;
            if (perlen <= 0)
            {
                perlen = 8192;
            }
            int istrue;
            rs.Edit();
            try
            {
                while (nowflag < bindata.Length)
                {
                    int insertlen = 0;
                    if (nowflag + perlen > bindata.Length)
                    {
                        insertlen = bindata.Length - nowflag;
                    }
                    else
                    {
                        insertlen = perlen;
                    }
                    int nmode;
                    if (nowflag == 0)
                    {
                        nmode = 0;
                    }
                    else
                    {
                        nmode = 1;
                    }
                    rs.SetBinDataByMode(ncol, 0, nmode, bindata.Substring(nowflag, insertlen), out istrue);
                    if (istrue < 0)
                    {
                        return false;
                    }
                    nowflag += insertlen;
                }
                rs.Update();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                rs.CloseRecordSet();
            }
        }
        /// <summary>
        /// �õ����������Ľ�����ļ�¼��
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <returns>��¼��Ŀ</returns>
        public static int GetDataCount(string Table, string Condition)
        {
            try
            {
                string strSql = "SELECT * FROM " + Table + " WHERE " + Condition;
                RecordSet rs = GetRecordSet(strSql);

                if (rs == null)
                {
                    return 0;
                }
                else
                {
                    int count = rs.GetCount();
                    rs.Close();
                    return count;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }


        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <param name="param">�������飬������ʾ�ֶ����ƣ�ż����ʾֵ</param>
        public static bool Update(string Table, string Condition, params string[] param)
        {
            string strSql = "SELECT * FROM " + Table + " WHERE " + Condition;
            RecordSet rst = GetRecordSet(strSql);
            if (rst == null)
            {
                return false;
            }
            if (rst.GetCount() <= 0)
            {
                return false;
            }
            try
            {
                rst.MoveFirst();
                while (!rst.IsEOF())
                {
                    rst.Edit();
                    for (int i = 0; i < (param.Length / 2); i++)
                    {
                        rst.SetValue(param[i * 2], param[i * 2 + 1]);
                    }

                    rst.Update();
                    rst.MoveNext();
                }
                rst.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <param name="param">�������飬������ʾ�ֶ����ƣ�ż����ʾֵ</param>
        public static bool Update(string Table, string Condition, IList<string> param)
        {
            string strSql = "SELECT * FROM " + Table + " WHERE " + Condition;
            RecordSet rst = GetRecordSet(strSql);
            if (rst == null)
            {
                return false;
            }
            try
            {
                rst.MoveFirst();
                while (!rst.IsEOF())
                {
                    rst.Edit();
                    for (int i = 0; i < (param.Count / 2); i++)
                    {
                        rst.SetValue(param[i * 2], param[i * 2 + 1]);
                    }

                    rst.Update();
                    rst.MoveNext();
                }
                rst.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Condition"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool Update_U(string Table, string Condition, IList<string> param)
        {
            string strSql = "UPDATE " + Table + " SET {0} WHERE " + Condition;

            try
            {
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < (param.Count / 2); i++)
                {
                    //rst.SetValue(param[i * 2], param[i * 2 + 1]);
                    if (i == 0)
                    {
                        str.Append(param[i * 2] + "=" + param[i * 2 + 1]);
                    }
                    else
                    {
                        str.Append("," + param[i * 2] + "='" + param[i * 2 + 1] + "'");
                    }
                }
                return ExecSql_U(string.Format(strSql, str.ToString()));
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// �����޸�
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="Condition">��������</param>
        /// <param name="param">�������飬������ʾ�ֶ����ƣ�ż����ʾֵ</param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static bool Update(string Table, string Condition, IList<string> param, Client client)
        {
            string strSql = "SELECT * FROM " + Table + " WHERE " + Condition;
            RecordSet rst = client.OpenRecordSet(strSql);
            if (rst == null)
            {
                return false;
            }
            try
            {
                rst.MoveFirst();
                while (!rst.IsEOF())
                {
                    rst.Edit();
                    for (int i = 0; i < (param.Count / 2); i++)
                    {
                        rst.SetValue(param[i * 2], param[i * 2 + 1]);
                    }

                    rst.Update();
                    rst.MoveNext();
                }
                rst.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ����ֵ,ֻ����1��
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="param">����</param>
        /// <returns></returns>
        public static bool Insert(string Table, params string[] param)
        {
            try
            {
                //����ֵ
                string[] fields = new string[param.Length / 2];
                string[] values = new string[param.Length / 2];
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = param[i * 2];
                    values[i] = param[i * 2 + 1];
                }
                if (!_Client.IsConnected())
                {
                    _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
                }
                _Client.BeginAppend(Table);
                _Client.Append(fields, values);
                _Client.EndAppend();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �����¼
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="param">����</param>
        /// <returns></returns>
        public static bool Insert(string Table, IList<string> param)
        {
            try
            {
                //����ֵ
                string[] fields = new string[param.Count / 2];
                string[] values = new string[param.Count / 2];
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = param[i * 2];
                    values[i] = param[i * 2 + 1];
                }
                if (!_Client.IsConnected())
                {
                    _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
                }
                _Client.BeginAppend(Table);
                _Client.Append(fields, values);
                _Client.EndAppend();
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// �����¼
        /// </summary>
        /// <param name="Table">����</param>
        /// <param name="param">����</param>
        /// <param name="client">����һ��client</param>
        /// <returns></returns>
        public static bool Insert(string Table, IList<string> param, Client client)
        {
            try
            {
                //����ֵ
                string[] fields = new string[param.Count / 2];
                string[] values = new string[param.Count / 2];
                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = param[i * 2];
                    values[i] = param[i * 2 + 1];
                }
                if (!client.IsConnected())
                {
                    return false;
                }
                client.BeginAppend(Table);
                client.Append(fields, values);
                client.EndAppend();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ִ��ɾ��sql��䣬Ϊɾ��ר��д�ģ�Ҳ������ExecSql����
        /// </summary>
        /// <param name="strSql">ɾ��sql���</param>
        /// <returns></returns>
        public static bool Delete(string strSql)
        {
            return ExecSql(strSql);
        }


        /// <summary>
        /// ִ��ɾ��sql��䣬Ϊɾ��ר��д�ģ�Ҳ������ExecSql����
        /// </summary>
        /// <param name="strSql">ɾ��sql���</param>
        /// <param name="client">һ���µĿͻ���</param>
        /// <returns></returns>
        public static bool Delete(string strSql, Client client)
        {
            return ExecSql(strSql, client);
        }

        /// <summary>
        /// ִ��sql���,��ȷ������ȷ��sql��䣬��ΪTPI��sql��SQLServer��sql�в�ͬ�ĵط����Ǻǣ���Ҳ��̫���
        /// </summary>
        /// <param name="strSql">sql���</param>
        /// <returns></returns>
        public static bool ExecSql(string strSql)
        {
            try
            {
                if (!_Client.IsConnected())
                {
                    _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
                }
                _Client.ExecSQL(strSql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool ExecSql_U(string strSql)
        {
            try
            {
                if (!_Client.IsConnected())
                {
                    _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
                }
                _Client.ExecSQL_U(strSql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ִ��sql���,��ȷ������ȷ��sql��䣬��ΪTPI��sql��SQLServer��sql�в�ͬ�ĵط����Ǻǣ���Ҳ��̫���
        /// </summary>
        /// <param name="strSql">sql���</param>
        /// <param name="client">һ���µĿͻ���</param>
        /// <returns></returns>
        public static bool ExecSql(string strSql, Client client)
        {
            try
            {
                if (!client.IsConnected())
                {
                    return false;
                }
                client.ExecSQL(strSql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ִ��sql���,��ȷ������ȷ��sql��䣬��ΪTPI��sql��SQLServer��sql�в�ͬ�ĵط����Ǻǣ���Ҳ��̫���
        /// </summary>
        /// <param name="strSql">sql���</param>
        /// <param name="client">һ���µĿͻ���</param>
        /// <returns></returns>
        public static bool ExecSql_dele(string strSql, string tablename)
        {
            try
            {
                if (!_Client.IsConnected())
                {
                    _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
                }
                _Client.ExecSQL(strSql);
                _Client.PackTable(tablename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ���ݱ����ж��Ƿ���ڸñ�
        /// </summary>
        /// <param name="tableName">����</param>
        /// <returns></returns>
        public static bool IsExistTable(string tableName)
        {

            if (!_Client.IsConnected())
            {
                _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
            }
            return _Client.IsExistTable(tableName);
        }


        /// <summary>
        /// ���ݲ�ѯ��������DataView
        /// </summary>
        /// <param name="strSql">��ѯ����</param>
        /// <returns></returns>
        public static DataView GetDataSet(string strSql)
        {
            DataTable tblRtn = new DataTable();

            //�õ����
            RecordSet rst = GetRecordSet(strSql);
            if (rst == null)
            {
                return null;
            }
            int FieldCount = rst.GetFieldCount();
            //����
            for (int i = 0; i < FieldCount; i++)
            {
                tblRtn.Columns.Add(rst.GetFieldName(i));
            }
            //����
            rst.MoveFirst();
            while (!rst.IsEOF() && rst.GetCount() > 0)
            {
                DataRow r = tblRtn.NewRow();
                for (int i = 0; i < FieldCount; i++)
                {
                    r[i] = rst.GetValue(i);
                }
                tblRtn.Rows.Add(r);
                rst.MoveNext();
            }
            rst.Close();
            return tblRtn.DefaultView;
        }

        /// <summary>
        /// ��ձ�
        /// </summary>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public static bool ClearTable(string tablename)
        {
            if (!_Client.IsConnected())
            {
                _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
            }
            return _Client.ClearTable(tablename);
        }
        /// <summary>
        /// ��ձ�
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static bool ClearTable(string tablename, Client client)
        {
            if (null == client)
                return false;
            return client.ClearTable(tablename);
        }
        /// <summary>
        /// ɾ����
        /// </summary>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public static bool DelTable(string tablename)
        {
            if (!_Client.IsConnected())
            {
                _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
            }
            return _Client.DropTable(tablename);
        }

        /// <summary>
        /// ��ȡ����ֶ��б�
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static IList<string> GetTableFields(string tablename)
        {
            RecordSet rs = GetRecordSetByCondition(tablename, "");
            if (null == rs)
            {
                return null;
            }
            IList<string> fieldlist = new List<string>();
            int FieldCount = rs.GetFieldCount();
            for (int i = 0; i < FieldCount; i++)
            {
                fieldlist.Add(rs.GetFieldName(i));
            }
            rs.Close();
            return fieldlist;
        }
        /// <summary>
        /// ��ȡ��ʵ����
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static string GetTableName(RecordSet rs)
        {
            if (!_Client.IsConnected())
            {
                _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
            }
            return _Client.GetRecordTableName(rs);
        }
        /// <summary>
        /// ��ȡ��ʵ���� �����ӵ�����
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static string GetTableName(RecordSet rs, Client client)
        {
            if (null == client)
                return string.Empty;
            return client.GetRecordTableName(rs);
        }

        /// <summary>
        /// ��ӱ�
        /// </summary>
        /// <param name="client"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool CreateTable(Client client, TableInfo table)
        {
            if (null == client || null == table)
                return false;
            return client.CreateTable(table);
        }
        /// <summary>
        /// ��ͼ�Ƿ����
        /// </summary>
        /// <param name="client"></param>
        /// <param name="viewname"></param>
        /// <returns></returns>
        public static bool ViewIsExist(Client client, string viewname)
        {
            if (null == client)
            {
                throw new Exception("���Ӵ���");
            }
            return client.IsExistView(viewname);
        }
        /// <summary>
        /// ���Ƿ����
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static bool TableIsExist(Client client, string tablename)
        {
            if (null == client)
            {
                throw new Exception("���Ӵ���");
            }
           return client.IsExistTable(tablename);
        }
        /// <summary>
        /// ������ͼ
        /// </summary>
        /// <param name="client"></param>
        /// <param name="viewname">��ͼ����</param>
        /// <param name="sql">SQL���</param>
        /// <param name="bmodify">�Ƿ��޸���ͼ</param>
        /// <returns></returns>
        public static bool CreateView(Client client, string viewname, string sql, bool bmodify)
        {
            if (null == client)
                return false;
            return client.CreateView(viewname, sql, bmodify);
        }


        /// <summary>
        ///  ���ݸ����ı��⣬���ģ��ؼ��� ժҪ��ȡ����
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="keyword"></param>
        /// <param name="digest"></param>
        /// <returns></returns>
        public static string GetZTClassStr(string title, string content, string keyword, string digest)
        {
            if (_BinConn == null)
            {
                _BinConn = new TPIConn();
            }
            if (_BinConn.IsConnected <= 0)
            {
                _BinConn.OpenConn(_TPIServerIP, 4567, _TPIUserName, _TPIPassword);
            }
            return _BinConn.GetZTCls(title, keyword, digest, content);
        }

        /// <summary>
        ///  ���ܴ���
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetSTMStr(string title, string content)
        {
            if (_BinConn == null)
            {
                _BinConn = new TPIConn();
            }
            if (_BinConn.IsConnected <= 0)
            {
                _BinConn.OpenConn(_TPIServerIP, 4567, _TPIUserName, _TPIPassword);
            }
            return _BinConn.STMGenerate(title, content);
        }
        /// <summary>
        ///  ���ܴ���
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetDiffStr(string srcTxt, string descTxt)
        {
            if (_BinConn == null)
            {
                _BinConn = new TPIConn();
            }
            if (_BinConn.IsConnected <= 0)
            {
                _BinConn.OpenConn(_TPIServerIP, 4567, _TPIUserName, _TPIPassword);
            }
            _BinConn.CompareContent(srcTxt, descTxt);
            StringBuilder str = new StringBuilder();
            int i = 0;
            string tempstr = string.Empty;
            i = _BinConn.GetCompareResult(out tempstr);
            str.Append(tempstr);
            while (i > 0)
            {
                i = _BinConn.GetCompareResult(out tempstr);
                str.Append(tempstr);
            }

            return str.ToString();
        }

        /// <summary>
        ///  �д�
        /// </summary>
        /// <param name="strSql">sql���</param>
        /// <returns></returns>
        public static string GetWord(string input)
        {
            try
            {
                if (!_Client.IsConnected())
                {
                    _Client.Connect(_TPIServerIP, _TPIUserName, _TPIPassword);
                }
                return _Client.WordSegment(input);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
