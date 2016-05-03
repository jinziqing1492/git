using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 逻辑库信息表
    /// </summary>
    public class LogicalDataBaseInfo : BaseModel
    {
        public string DbId { get; set; } //数据库id，
        public string DbName { get; set; } //数据库名，
        public string DbDescription { get; set; } //数据库描述，逻辑数据库描述
        public string ParentDbId { get; set; } //父数据库id，保留
        public string Remark { get; set; } //备注，
        public int Dbtype { get; set; } //逻辑库类型，1是专题库，2是产品库
        public int isonline { get; set; } //是否上架，0是下架 1是上架
        //public string OnlineDoi { get; set; } //上架后产生的id，上架后产生的id
        public DateTime OnlineDate { get; set; } //上架时间，上架时间
        //public string SYS_FLD_VIRTUALPATHTAG { get; set; } //虚拟目录的标记，虚拟目录标识
        //public string SYS_FLD_COVERPATH { get; set; } //图书封面，封面的相对路径
        public string DbTag { get; set; } //数据库标签，分类标签
        //public int SYS_FLD_CHECK_STATE { get; set; } //审核状态，0未审核，-1审核完
        //public DateTime SYS_FLD_CHECK_DATE { get; set; } //审核时间，审核时间
        //public string SYS_FLD_CHECK_USERNAME { get; set; } //审核用户，
        public string Operator { get; set; } //操作人，设置的操作人
        public DateTime OperateTime { get; set; } //操作时间，设置的时间
        public string SYS_FLD_ICOPATH { get; set; } //小图标
    }
}
