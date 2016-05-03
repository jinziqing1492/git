using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace FRAME.CCL
{
    public static class StringExtension
    {
        /// <summary>
        /// 将xml字符串转换成XDocument对象
        /// </summary>
        /// <param name="xmlText">xml字符串</param>
        /// <returns>XDocument对象</returns>
        public static XDocument ToXDocument(this string xmlText)
        {
            if (string.IsNullOrWhiteSpace(xmlText))
            {
                return null;
            }
            XDocument doc = null;
            try
            {
                doc = XDocument.Parse(xmlText);
            }
            catch
            {
                return null;
            }
            return doc;
        }

        /// <summary>
        /// 从utf-8编码转换成ascii编码
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>转换后文本</returns>
        public static string FromUTF8toASCII(this string text)
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            Byte[] encodedBytes = utf8.GetBytes(text);
            Byte[] convertedBytes =
                    Encoding.Convert(Encoding.UTF8, Encoding.ASCII, encodedBytes);
            System.Text.Encoding ascii = System.Text.Encoding.ASCII;

            return ascii.GetString(convertedBytes);
        }

        /// <summary>
        /// 将字符串转换成Guid
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>guid</returns>
        public static Guid ToGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Guid.Empty;
            }
            Guid item;
            Guid.TryParse(input, out item);
            return item;
        }

        /// <summary>
        /// 将字符串转换成时间
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>如果转成成功返回日期时间，失败返回null</returns>
        public static DateTime? ToNullableDateTime(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return (DateTime?)null;
            }
            DateTime output;
            return DateTime.TryParse(input, out output) ? output : (DateTime?)null;
        }

        /// <summary>
        /// 判断字符串是否是合法的Guid（全为0的guid也认为不合法）
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>是否是合法的Guid</returns>
        public static bool IsLegalGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            Guid item;
            bool result = Guid.TryParse(input, out item);
            return result && item != Guid.Empty;
        }

        /// <summary>
        /// 判断字符串是否是Guid
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>是否是合法的Guid</returns>
        public static bool IsGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            Guid item;
            bool result = Guid.TryParse(input, out item);
            return result;
        }

        /// <summary>
        /// 判断字符串是否是合法的整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInteger(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            long item;
            bool result = long.TryParse(input, out item);
            return result;
        }

        /// <summary>
        /// 字符串转换为整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt32(this string input)
        {
            int val;
            Int32.TryParse(input, out val);
            return val;
        }

        /// <summary>
        /// 判断字符串是否是合法的小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDouble(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            double item;
            bool result = double.TryParse(input, out item);
            return result;
        }

        /// <summary>
        /// 字符串转换为小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToDouble(this string input)
        {
            double val;
            double.TryParse(input, out val);
            return val;
        }

        /// <summary>
        /// 检查输入是否存在注入攻击
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool IsIllegal(this string input)
        {
            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|'";
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            if (input == "*")
            {
                return true;
            }
            foreach (string str_t in word.Split('|'))
            {
                if ((input.ToLower().IndexOf(str_t + " ") > -1) || (input.ToLower().IndexOf(" " + str_t) > -1))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将图片转换成Base64编码字符串
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="format">格式</param>
        /// <returns>Base64编码字符串</returns>
        public static string ConvertImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// 将Base64编码的图片字符串还原成图片
        /// </summary>
        /// <param name="base64String">Base64编码的图片字符</param>
        /// <returns>图片</returns>
        public static Image ConvertImageFromBase64(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Image image;
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            image = Image.FromStream(ms, true);

            return image;
        }

        /// <summary>
        /// 提取字符串中的价格
        /// </summary>
        /// <param name="priceStr">要提取的字符串</param>
        /// <returns></returns>
        public static double ExtractPrice(this string priceStr)
        {
            string regex = @"(\d+.\d+)|\d+";
            Match match = Regex.Match(priceStr, regex);
            return match.Value.ToDouble();
        }

        /// <summary>
        /// 去除所有的html标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(this string html)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");
            return strText;
        }
    }
}
