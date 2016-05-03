using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class ResourceTypeInfo
    {
        public string SYS_FLD_DOI { get; set; } //唯一标示
        public string NAME { get; set; } //名称
        public string PARENTID { get; set; } //父ID
        public string COVERPATH { get; set; } //封面路径
        public DateTime CREATETIME { get; set; } //创建时间
        public string Remark { get; set; }//备注
    }
}
