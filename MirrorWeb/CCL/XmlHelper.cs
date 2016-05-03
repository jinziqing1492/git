using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAME.CCL
{
    public class XmlHelper
    {
        /// <summary>
        /// 将xml中的特殊字符替换掉 如& " '
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChar(string xml)
        {
            xml = xml.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;");
            return xml;
        }
    }
}
