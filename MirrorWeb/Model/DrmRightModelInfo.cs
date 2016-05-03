using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class DrmRightModelInfo
    {
        public int Type { get; set; }//加密模式  0 服务器加密：1 证书加密
        public bool TimeLimit { get; set; }//是否启用时间限制
        public int TimeLength { get; set; }//时间长度 默认1
        public string TimeUnit { get; set; }//时间单位 d m y 从下载之时算起到下载的页面再算时间
        // public int TryReadPage { get; set; }//试读的最大页数 
        public bool CopyLimit { get; set; }//是否允许复制
        public bool PrintLimit { get; set; }//是否允许打印
        public bool OpenTimeLimit { get; set; }//是否限制打开次数
        public string OpenTime { get; set; }//打开次数 限制打开次数为真时 可用

        public bool CopyTextLimit { get; set; }//复制文本限制
        public bool CopyCharLimit { get; set; }//复制字符限制
        public int CopyCharCount { get; set; }//复制字符数

        public int TerminalCount { get; set; }//终端数
    }
}
