using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class OriginalDataInfo:BaseModel
    {
        //public string Name { get; set; } //资源名称
        public string Desciption { get; set; } //描述
        public string ParentName { get; set; } //所属图书名
        public string OrginalresType { get; set; } //资源类型
        //public string SYS_FLD_CLASSFICATION { get; set; } //分类号
        public string SYS_FLD_CLASSNAME { get; set; } //分类名称
        public string SYS_FLD_READUSER { get; set; } //可读用户
        public string SYS_FLD_DOWNLOADUSER { get; set; } //下载用户
        public int SYS_FLD_VERSION { get; set; } //版本
        public string SYS_FLD_GROUPID { get; set; } //组id
        public int SYS_FLD_STATUS { get; set; } //文件状态
        public string KeyWords { get; set; } //关键词
        public string FileType { get; set; }//文件类型
        public string Source { get; set; }//来源
        //public string SYS_FLD_FILEPATH { get; set; }  //  主体文件名    
        //public string SYS_FLD_VIRTUALPATHTAG { get; set; }  //  虚拟目录的标记 
        //public string SYS_FLD_DOI { get; set; }  //  DOI  编号  
        //public string SYS_SYSID { get; set; }  //  系统标识  系统自动编号  
        //public DateTime Sys_fld_Adddate { get; set; }//添加时间
        //public string Sys_fld_Adduser { get; set; }//添加人   
    }
}
