using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 中图类别
    /// </summary>
   public class ZTFlfClsInfo
    {
        public string SYS_FLD_CLASS_NAME { get; set; } //中图名称
        public string SYS_FLD_CLASS_CODE { get; set; } //中图编号（A A1 A11 A19）
        public int SYS_FLD_CLASS_GRADE { get; set; } //分类级别
        public int SYS_FLD_CHILD_FLAG { get; set; } //有无子级 有1 无0
        public string SYS_FLD_SYS_CODE { get; set; } //中图主键
        public string SYS_FLD_PARENT_CODE { get; set; } //
        public string SYS_FLD_PREVSILIBING_CODE { get; set; } //
        public string SYS_FLD_NEXTSILIBING_CODE { get; set; } //
        public string SYS_FLD_CHILD_SORTSN { get; set; } //
        public int SYS_FLD_SYSID { get; set; } //
        public string SYS_FLD_RECORD_COUNT { get; set; } //
    }
}
