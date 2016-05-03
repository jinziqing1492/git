using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Tool = CNKI.BaseFunction;

namespace DRMS.MirrorWeb.BasePage
{
    public abstract class AdminBaseControl : System.Web.UI.UserControl
    {
        #region 方法

        /// <summary>
        /// 格式日期输出
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string FormatDate(object value)
        {
            DateTime? dt = value as DateTime?;
            if (dt != null)
            {
                DateTime time = Convert.ToDateTime(dt);
                if (time != DateTime.MinValue)
                {
                    return time.ToString("yyyy-MM-dd HH:mm");
                }
            }
            return "";
        }


        /// <summary>
        /// 展示在线状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string DisplayOnlineStatus(object obj)
        {
            string result = "下架";
            if (obj != null)
            {
                string str = RemoveRed(obj.ToString());
                switch (str)
                {
                    case "0":
                        result = "下架";
                        break;
                    case "1":
                        result = "上架";
                        break;
                    default:
                        result = "未上架";
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 替换标红
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        protected string ReplaceRed(object input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            return CNKI.BaseFunction.NormalFunction.ReplaceRed(input.ToString());
        }

        /// <summary>
        /// 去除标红
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        protected string RemoveRed(object input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            return CNKI.BaseFunction.NormalFunction.ResetRedFlag(input.ToString());
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string DisplayStatus(object obj)
        {
            string result = "待审核";
            if (obj != null)
            {
                if (RemoveRed(obj.ToString()) == "-1")
                {
                    result = "已审核";
                }
            }
            return result;
        }

        /// <summary>
        /// 超期状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string DisplayTimeStatus(object obj, object obj2)
        {
            string result = "读取失败";
            if (obj != null && obj2 != null)
            {
                DateTime startDate = Tool.StructTrans.TransDate(obj.ToString());
                DateTime endDate = Tool.StructTrans.TransDate(obj2.ToString());
                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    if (DateTime.Now <= endDate && DateTime.Now >= startDate)
                    {
                        result = "正常";
                    }
                    else if (DateTime.Now > endDate)
                    {
                        result = "超期";
                    }
                }
            }
            return result;
        }
        #endregion
    }
}