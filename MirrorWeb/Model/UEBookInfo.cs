using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class UEBookInfo
    {
        public string Id { get; set; } //记录id，记录id
        public string Name { get; set; } //U盘产品名称，产品名
        public string Description { get; set; } //描述，产品介绍信息，包含内容
        public string SYS_FLD_VIRTUALPATHTAG { get; set; } //虚拟目录的标记，
        public string SYS_FLD_COVERPATH { get; set; } //图书封面，
        public DateTime OperatorDate { get; set; } //结束时间，添加时间
        public string Operator { get; set; } //操作人，添加人
        public int Isonline { get; set; } //上架状态，是否在线
        public int SYS_FLD_CHECK_STATE { get; set; } //审核状态，0未审核，-1审核通过
        public DateTime SYS_FLD_CHECK_DATE { get; set; }//审核时间
    }
}
