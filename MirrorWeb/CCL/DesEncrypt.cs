using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace FRAME.CCL
{
    public class DesEncrypt
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private static string key = "Microsoft";

        /// <summary>
        /// 构造函数
        /// </summary>
        public DesEncrypt()
        {
        }

        #region 使用 缺省密钥字符串 加密/解密string
        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, key);
        }
        /// <summary>
        /// 使用缺省密钥字符串解密
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, key);
        }
        #endregion
        #region 使用 给定密钥字符串 加密/解密string
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="Text">明文</param>
        /// <param name="sKey">密钥</param>
        /// <returns>密文</returns>
        public static string Encrypt(string Text, string sKey)
        {
            byte[] inputByteArray = Encoding.Default.GetBytes(Text);
            byte[] encrypted = Encrypt(inputByteArray, sKey);
            return BytesToHexString(encrypted);
        }
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="Text">密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string Text, string sKey)
        {
            byte[] inputByteArray = HexStringToBytes(Text);
            byte[] original = Decrypt(inputByteArray, sKey);
            return Encoding.Default.GetString(original);
        }
        #endregion
        #region 使用 缺省密钥字符串 加密/解密/byte[]
        /// <summary>
        /// 使用缺省密钥字符串解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(DesEncrypt.key);
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(DesEncrypt.key);
            return Encrypt(original, key);
        }
        #endregion
        #region 使用 给定密钥 加密/解密/byte[]
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Encrypt(byte[] input, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.FlushFinalBlock();
            byte[] buffer = ms.ToArray();
            ms.Close();
            return buffer;
        }
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="input">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] input, byte[] key)
        {
            string sKey = System.Text.Encoding.Default.GetString(key);
            return Encrypt(input, sKey);
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] input, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.FlushFinalBlock();
            byte[] buffer = ms.ToArray();
            ms.Close();
            return buffer;
        }
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] input, byte[] key)
        {
            string sKey = System.Text.Encoding.Default.GetString(key);
            return Decrypt(input, sKey);
        }
        #endregion
        #region string、byte[]之间转换
        /// <summary>
        /// 将字节数组转换成十六进制string
        /// </summary>
        /// <param name="byteArray">目标字节数组</param>
        /// <returns>转换后的字符串</returns>
        private static string BytesToHexString(byte[] byteArray)
        {
            if (byteArray == null)
            {
                return null;
            }
            StringBuilder ret = new StringBuilder();
            foreach (byte b in byteArray)
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// 将十六进制string转换成字节数组
        /// </summary>
        /// <param name="input">目标字符串</param>
        /// <returns>转换后的字节数组</returns>
        private static byte[] HexStringToBytes(string input)
        {
            if (input == null)
            {
                return null;
            }
            int nLen = input.Length;
            if ((nLen % 2) != 0) // 如果长度为奇数，则忽略最后一个十六位字符
            {
                nLen--;
            }
            byte[] buffer = new byte[nLen / 2];
            for (int i = 0; i < nLen; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            }
            return buffer;
        }
        #endregion
    }
}
