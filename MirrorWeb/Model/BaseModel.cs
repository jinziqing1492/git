using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
  [Serializable]
    public class BaseModel
    {
        public string Name { get; set; } //名称
        public string SYS_FLD_MARK_USERNAME { get; set; }  //  标引用户    
        public DateTime SYS_FLD_MARK_DATE { get; set; }  //  标引时间    
        public int SYS_FLD_MARK_STATE { get; set; }  //  标引状态  0未标引完，-1标引完  
        public string SYS_FLD_CHECK_USERNAME { get; set; }  //  审核用户    
        public DateTime SYS_FLD_CHECK_DATE { get; set; }  //  审核日期    
        public int SYS_FLD_CHECK_STATE { get; set; }  //  审核状态  0未审核，-1审核通过  
        public string SYS_FLD_ERROR_DESCRIPT { get; set; }  //  审核未通过原因    
        public string SYS_FLD_CLASSFICATION { get; set; }  //  分类号    
        public string SYS_FLD_CATALOG { get; set; }  //  目录  xml形式的目录  
        public int SYS_FLD_RES_LEVEL { get; set; }  //  资源密级  默认是10以10的倍数递增  
        public string SYS_FLD_DOI { get; set; }  //  DOI  编号  
        public string SYS_SYSID { get; set; }  //  系统标识  系统自动编号  
        public int SYS_FLD_ISHASATTACH { get; set; }  //  是否有附件  默认是0没有，1是有  
        public string SYS_FLD_VSM { get; set; }  //  VSM    
        public string SYS_FLD_FILEPATH { get; set; }  //  主体文件名    
        public string SYS_FLD_VIRTUALPATHTAG { get; set; }  //  虚拟目录的标记    
        public string SYS_FLD_COVERPATH { get; set; }  //  图书封面    
        public string SYS_FLD_SRCFILENAME { get; set; }  //  原文件名    
        public string SYS_FLD_XMLPATH { get; set; }  //  加工xml文件    
        public string SYS_FLD_OTHERFORMAT { get; set; }  //  其他载体   路径用分号隔开  
        public string SYS_FLD_PRINTFINGER { get; set; }  //  正文的指纹   正文的映射字段  
        public string SYS_FLD_ABSTRACT { get; set; }  //  动态摘要  正文的映射字段  
        public int Sys_fld_Hitcount { get; set; }  //  点击率  点击数  
        public int Sys_fld_Download { get; set; }  //  下载率  下载数  
       // public DataBaseType SYS_FLD_DBTYPE { get; set; }//数据库类型
        public string SYS_FLD_LDBID { get; set; }//逻辑库的id
        public string Sys_Fld_Reference { get; set; }//xml形式的参考文献 图书 标准 期刊的文章 学位论文 涉及到这个信息
        public int IsOnline { get; set; }//0下架1是上架
        public string OnlineDoi { get; set; }//上架后产生的doi
        public string BookId { get; set; }//对应erp里的id 出版社社内编号
        public DateTime Sys_fld_Adddate { get; set; }//添加时间
        public string Sys_fld_Adduser { get; set; }//添加人

        public DateTime CopyrightBeginDate { get; set; }  //  版权开始日期  中英文刊名映射到出版单位  
        public string CopyrightYear { get; set; }  //  版权年份  拼音刊名映射到来源代码  
        public string Allrightreserved { get; set; }  //  版权所有者    
        public string COPYRIGHTENDDATE { get; set; }//版权到期时间

        public int Sys_fld_isimport { get; set; }//是否是导入 0不是导入 1是导入

        public string Note { get; set; }//注释

        public string SYS_FLD_PARAXML { get; set; }//存放部分xml
        public string SYS_FLD_CHAPTERDOI { get; set; }//章节doi
        public string Department { get; set; } //部门编号

        public string Sys_fld_BookInfo { get; set; }//存放图书xml中的info信息

        public string SYS_FLD_PARAXML_U { get; set; }  //  加工xml文件 unicode编码   

        public string Price { get; set; }  //  纸书定价    

    }
}
