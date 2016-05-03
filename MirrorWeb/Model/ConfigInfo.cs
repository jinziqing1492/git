using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class ConfigInfo
    {
        public int ID { get; set; } //，唯一标识
        public string RootDir { get; set; } //，图片名称
        public string VirtualPathTag { get; set; } //，虚拟目录的标识
        public string VirtualPathName { get; set; } //，虚拟目录名称
    }
}
