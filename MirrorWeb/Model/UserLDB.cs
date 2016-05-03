using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    class UserLDB:BaseModel
    {
        public string ID { get; set; } //记录ID
        public string UserName { get; set; } //用户名
        public string LDBID { get; set; } //逻辑库ID
        public string LDBName { get; set; } //逻辑库名称
        public DateTime StartDate { get; set; } //开始时间
        public DateTime EndDate { get; set; } //结束时间
        public string Operator { get; set; } //操作人
        public DateTime OperatorTime { get; set; } //操作时间
        public int Flag { get; set; } //审核状态
        public int Status { get; set; } //包库状态
        public string OnlineDOI { get; set; } //上架后产生的ID
    }
}
