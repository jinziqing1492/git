using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class SubscribeInfo
    {
        public string ID { get; set; } //记录
        public string Name { get; set; } //名称
        public string Exp { get; set; } //订阅关键词
        public int IsUse { get; set; } //是否启用
        public DateTime OperatorDate { get; set; } //结束时间
        public string Operator { get; set; } //操作人
        public string Remark { get; set; } //备注
    }
}
