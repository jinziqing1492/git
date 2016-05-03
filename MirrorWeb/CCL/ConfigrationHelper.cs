using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace FRAME.CCL
{
    /// <summary>
    /// 读取配置
    /// </summary>
    public class ConfigrationHelper
    {
        /// <summary>
        /// 获取最大的请求长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetMaxRequestLength()
        {
            string maxrequestLength = ConfigurationManager.AppSettings["maxrequestLength"];
            object lengthstr = EvaluatorHelper.Eval(maxrequestLength);
            int length = Convert.ToInt32(lengthstr);
            return length;
        }
    }
}
