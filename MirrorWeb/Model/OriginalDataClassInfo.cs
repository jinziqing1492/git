using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class OriginalDataClassInfo
    {
        public string id { get; set; }//分类ID，唯一标识
        public string ThemeName { get; set; }//某一分类的名称
        public string ParentID { get; set; }//所属父分类的ID号
        public string SourceCode { get; set; }//原来的代码格式
        public string FileFormat { get; set; }//文件格式doc;tif，多个用分号隔开
        public string Remark { get; set; }//备注
    }
}
