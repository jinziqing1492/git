using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class OwnerResourceTypeInfo
    {
        public string SYS_FLD_DOI { get; set; }
        public string BASEID { get; set; }
        public int DATATYPE { get; set; }//数据类型
        public string NAME { get; set; } //名称
        public int SYS_SYSID { get; set; } //系统ID
        public string DESCRIPT { get; set; } //描述
        public DateTime SYS_FLD_ADDDATE { get; set; } //添加时间
        public string SYS_FLD_VIRTUALPATHTAG { get; set; }//虚拟路径标示
        public string SYS_FLD_COVERPATH { get; set; }//封面
    }
}
