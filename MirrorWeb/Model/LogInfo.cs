using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 日志表
    /// </summary>
    public class LogInfo
    {
        public string Id { get; set; } //日志id，
        public string Name { get; set; } //名称，
        public int ResType { get; set; } //类型，统一规定
        public string ResDoi { get; set; } //资源的doi，
        public int LogType { get; set; } //日志类型，
        public string Remark { get; set; } //备注，
        public DateTime Adddate { get; set; } //记录时间，
        public string username { get; set; } //用户名，
        public int userType { get; set; } //用户类别，
        public string Ip { get; set; } //用户访问的ip，
        public string IpNum { get; set; } //Ip对应的数字，
    }
}
