using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class UserLDBInfo
    {
        public string Id { get; set; } //记录id，记录id
        public string UserName { get; set; } //用户名，用户名
        public string LDBID { get; set; } //逻辑库id，逻辑库id
        public string LDBName { get; set; } //逻辑库名称，逻辑库名称
        public DateTime SartDate { get; set; } //开始时间，包库的起始时间
        public DateTime EndDate { get; set; } //结束时间，包库的结束时间
        public string Operator { get; set; } //操作人，设置的操作人
        public DateTime OperateTime { get; set; } //操作时间，设置的时间
        public int Flag { get; set; } //审核状态，0是默认未审核，-1是审核状态
        public int Status { get; set; } //包库状态，1正常，2超期
        public string OnlineDoi { get; set; } //上架后产生的id，上架后产生的id
    }
}
