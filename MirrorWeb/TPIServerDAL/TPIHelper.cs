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
    /// 添加unicode字段的时候用
    /// </summary>
    //public class FieldInfo
    //{
    //    string fieldName;
    //     /// <summary>
    //     ///  数据库的字段名
    //     /// </summary>
    //    public string FieldName
    //    {
    //        get { return fieldName; }
    //        set { fieldName = value; }
    //    }

    //    string fieldValue;
    //    /// <summary>
    //    /// 数据库的字段值
    //    /// </summary>
    //    public string FieldValue
    //    {
    //        get { return fieldValue; }
    //        set { fieldValue = value; }
    //    }
    //}

    /// <summary>
    /// KBASE的数据库操作类
    /// </summary>
    public abstract class TPIHelper
    {
        /// <summary>
        /// 构造函数
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
        /// 根据查询条件，返回结果集,使用完毕结果集后请手动关闭
        /// </summary>
        /// <param name="strSql">查询条件</param>
        /// <returns>结果集</returns>
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
        /// 用完显式关闭连接及结果集
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
        /// 用完显式关闭连接及结果集
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
        /// 由表名,过滤条件返回结果,使用完毕结果集后请手动关闭
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <returns>结果表</returns>
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
        /// 由表名,过滤条件返回结果,使用完毕结果集后请手动关闭
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <returns>结果表</returns>
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
        /// 获取列表但只获取部分字段
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
        /// 由表名,过滤条件返回结果,使用完毕结果集后请手动关闭
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <returns>结果表</returns>
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
        /// 根据表名和查询条件，取得符合条件的记录数。使用完毕结果集后请手动关闭
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>返回符合条件的记录个数</returns>
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
        /// 获取一个二进制数据操作的记录集
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
        /// 获取一个二进制数据操作的记录集
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
        /// 写入二进制数据
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="bindata">二进制数据</param>
        /// <param name="conn">连接</param>
        /// <param name="ncol">二进制数据的列号</param>
        /// <param name="perlen">每次写入的大小 默认8192(4K)</param>
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
        /// 得到满足条件的结果集的记录数
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <returns>记录数目</returns>
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
        /// 保存修改
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <param name="param">参数数组，奇数表示字段名称，偶数表示值</param>
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
        /// 保存修改
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <param name="param">参数数组，奇数表示字段名称，偶数表示值</param>
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
        /// 保存修改
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Condition">过滤条件</param>
        /// <param name="param">参数数组，奇数表示字段名称，偶数表示值</param>
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
        /// 插入值,只插入1条
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static bool Insert(string Table, params string[] param)
        {
            try
            {
                //插入值
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
        /// 插入记录
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static bool Insert(string Table, IList<string> param)
        {
            try
            {
                //插入值
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
        /// 插入记录
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="param">参数</param>
        /// <param name="client">传入一个client</param>
        /// <returns></returns>
        public static bool Insert(string Table, IList<string> param, Client client)
        {
            try
            {
                //插入值
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
        /// 执行删除sql语句，为删除专门写的，也可以用ExecSql函数
        /// </summary>
        /// <param name="strSql">删除sql语句</param>
        /// <returns></returns>
        public static bool Delete(string strSql)
        {
            return ExecSql(strSql);
        }


        /// <summary>
        /// 执行删除sql语句，为删除专门写的，也可以用ExecSql函数
        /// </summary>
        /// <param name="strSql">删除sql语句</param>
        /// <param name="client">一个新的客户端</param>
        /// <returns></returns>
        public static bool Delete(string strSql, Client client)
        {
            return ExecSql(strSql, client);
        }

        /// <summary>
        /// 执行sql语句,请确保是正确的sql语句，因为TPI的sql和SQLServer的sql有不同的地方，呵呵，我也不太清楚
        /// </summary>
        /// <param name="strSql">sql语句</param>
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
        /// 执行sql语句,请确保是正确的sql语句，因为TPI的sql和SQLServer的sql有不同的地方，呵呵，我也不太清楚
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="client">一个新的客户端</param>
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
        /// 执行sql语句,请确保是正确的sql语句，因为TPI的sql和SQLServer的sql有不同的地方，呵呵，我也不太清楚
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="client">一个新的客户端</param>
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
        /// 根据表名判断是否存在该表
        /// </summary>
        /// <param name="tableName">表名</param>
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
        /// 根据查询条件返回DataView
        /// </summary>
        /// <param name="strSql">查询条件</param>
        /// <returns></returns>
        public static DataView GetDataSet(string strSql)
        {
            DataTable tblRtn = new DataTable();

            //得到结果
            RecordSet rst = GetRecordSet(strSql);
            if (rst == null)
            {
                return null;
            }
            int FieldCount = rst.GetFieldCount();
            //添列
            for (int i = 0; i < FieldCount; i++)
            {
                tblRtn.Columns.Add(rst.GetFieldName(i));
            }
            //添行
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
        /// 清空表
        /// </summary>
        /// <param name="tablename">表名</param>
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
        /// 清空表
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
        /// 删除表
        /// </summary>
        /// <param name="tablename">表名</param>
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
        /// 获取表的字段列表
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
        /// 获取真实表名
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
        /// 获取真实表名 带连接的重载
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
        /// 添加表
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
        /// 视图是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="viewname"></param>
        /// <returns></returns>
        public static bool ViewIsExist(Client client, string viewname)
        {
            if (null == client)
            {
                throw new Exception("连接错误");
            }
            return client.IsExistView(viewname);
        }
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static bool TableIsExist(Client client, string tablename)
        {
            if (null == client)
            {
                throw new Exception("连接错误");
            }
           return client.IsExistTable(tablename);
        }
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="client"></param>
        /// <param name="viewname">视图名称</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="bmodify">是否修改视图</param>
        /// <returns></returns>
        public static bool CreateView(Client client, string viewname, string sql, bool bmodify)
        {
            if (null == client)
                return false;
            return client.CreateView(viewname, sql, bmodify);
        }


        /// <summary>
        ///  根据给定的标题，正文，关键词 摘要获取分类
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
        ///  智能处理
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
        ///  智能处理
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
        ///  切词
        /// </summary>
        /// <param name="strSql">sql语句</param>
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
