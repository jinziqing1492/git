using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using TPI;

namespace DRMS.TPIServerDAL
{
    /// <summary>
    /// 获取客户端
    /// </summary>
    public class ClientAnalyse
    {

        static TPI.Client _Client = new Client();
        static TPIBINREADERLib.TPIConn _BinConn;
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="dbaseinfo">ip;port;username;password</param>
        /// <returns></returns>
        public static Client GetClient(string dbaseinfo)
        {
            if (string.IsNullOrEmpty(dbaseinfo))
            {
                return null;
            }
            Match m = Regex.Match(dbaseinfo, @"^(?<ip>[^;]*);(?<port>[^;]*);(?<username>[^;]*);(?<password>[^;]*);?$", RegexOptions.IgnoreCase);
            if (!m.Success)
            {
                return null;
            }
            string ip = m.Groups["ip"].Value;
            int port = CNKI.BaseFunction.StructTrans.TransNum(m.Groups["port"].Value);
            string username = m.Groups["username"].Value;
            string password = m.Groups["password"].Value;
            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(username))//IP和用户名必须有
            {
                return null;
            }
            if (string.IsNullOrEmpty(password))//password为NULL置成空
            {
                password = "";
            }
            if (_Client.IsConnected())
            {
                return _Client;
            }
            else
            {
                if (_Client == null)
                {
                    _Client = new Client();
                }
                if (port != 0)
                {
                    _Client.Connect(ip, port, username, password);
                }
                else
                {
                    _Client.Connect(ip, username, password);
                }
                if (_Client.IsConnected())
                {
                    return _Client;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取二进制的链接
        /// </summary>
        /// <param name="dbaseinfo"></param>
        /// <returns></returns>
        public static TPIBINREADERLib.TPIConn GetTPIConn(string dbaseinfo)
        {
            if (string.IsNullOrEmpty(dbaseinfo))
            {
                return null;
            }
            Match m = Regex.Match(dbaseinfo, @"^(?<ip>[^;]*);(?<port>[^;]*);(?<username>[^;]*);(?<password>[^;]*);?$", RegexOptions.IgnoreCase);
            if (!m.Success)
            {
                return null;
            }
            string ip = m.Groups["ip"].Value;
            int port = CNKI.BaseFunction.StructTrans.TransNum(m.Groups["port"].Value);
            string username = m.Groups["username"].Value;
            string password = m.Groups["password"].Value;
            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(username))//IP和用户名必须有
            {
                return null;
            }
            if (string.IsNullOrEmpty(password))//password为NULL置成空
            {
                password = "";
            }

            if (_BinConn == null)
            {
                _BinConn = new TPIBINREADERLib.TPIConn();
            }
            if (_BinConn.IsConnected > 0)
            {
                return _BinConn;
            }
            else
            {
                if (port != 0)
                {
                    _BinConn.OpenConn(ip, port, username, password);
                }
                else
                {
                    _BinConn.OpenConn(ip, 4567, username, password);
                }
                if (_BinConn.IsConnected > 0)
                {
                    return _BinConn;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
