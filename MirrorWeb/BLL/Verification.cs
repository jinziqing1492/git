using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PortalProduct.BaseLib.Verification;

namespace DRMS.BLL
{
   public class Verification
    {
       private const string LOGINKEY = "CNKIKDRMSDRMSLK";

        private static Licence lic;

        /// <summary>
        /// 读入证书信息
        /// </summary>
        public static void LoadLic()
        {
            Register.Login = LOGINKEY;

            lic = Register.GetLicence(Register.GenerateMachineNum());
        }

        /// <summary>
        /// 证书是否过期
        /// </summary>
        /// <returns></returns>
        public static bool IsOutDate()
        {
            if (null == lic)
            {
                LoadLic();
            }
            if (null == lic)
            {
                return true;
            }
            if (lic.OutDate != DateTime.MinValue && lic.OutDate > DateTime.Now)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否可注册某个系统
        /// </summary>
        /// <param name="sysType"></param>
        /// <returns></returns>
        public static bool CanReg(string sysType)
        {
            if (null == lic)
            {
                LoadLic();
            }
            if (null == lic)
            {
                return false;
            }
            //  int allowCount = lic.Message.GetCount(sysType.ToLower());
            ////  int nowCount = SystemCache.GetCount(sysType);
            //  if (allowCount <= nowCount)
            //  {
            //      return false;
            //  }
            return true;
        }

        /// <summary>
        /// 获取当前注册的系统信息
        /// </summary>
        /// <returns></returns>
        public static RegMessage GetRegMessage()
        {
            if (null == lic)
            {
                LoadLic();
            }
            if (null == lic)
            {
                return null;
            }
            return lic.Message;
        }

        /// <summary>
        /// 获取当前系统的过期时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetOutDate()
        {
            if (null == lic)
            {
                LoadLic();
            }
            if (null == lic)
            {
                return DateTime.MinValue;
            }
            return lic.OutDate;
        }

        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns></returns>
        public static string GetMachineNum()
        {
            Register.Login = LOGINKEY;
            return Register.GenerateMachineNum();
        }
    }
}
