using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace FRAME.CCL
{
    public class Machine
    {
        /// <summary>  
        /// 获取CPU编号  
        /// </summary>  
        /// <returns>返回一个字符串类型</returns>  
        public static string Get_CPUID()
        {
            try
            {
                //需要在解决方案中引用System.Management.DLL文件  
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                string strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }
        }
    }
}
