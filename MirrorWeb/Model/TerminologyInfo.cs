using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 术语、缩略语、工具书词条表
    /// </summary>
    public class TerminologyInfo : BaseModel
    {
        //public string Name { get; set; } //名称，术语、缩略语名称，词条名
        public string ENName { get; set; } //英文名，英文名称
        public string NameAllogeneic { get; set; } //术语异体，术语异体
        public string Content { get; set; } //内容，定义内容（包括定义的注解、里面可能还有图表、公式）
        public string ENcontent { get; set; } //外文解释，外文解释
        public string Author { get; set; } //作者，作者
        public string AuthorDESC { get; set; } //作者描述，作者描述
        public int metatype { get; set; } //数据类型，0表示术语，1表示缩略语，2工具书词条
        public string ParentDOI { get; set; } //父资源Id，对应信息标准信息表里的标准的唯一标识
        public string ParentName { get; set; } //父名称，术语和缩略语是标准号，词条就是书名
        public DateTime Pubdate { get; set; } //发布日期，发布时间
        public DateTime Finddate { get; set; } //获取日期，获取时间
        public string accessories { get; set; } //附件地址，图表等附件
        public string AttrPath { get; set; } //父词条的属性路径，存放词条的属性路径，为提供纵向检索提供关联（针对河湖大典）
        public string Smartstr { get; set; } //用于搜索相关词
        public string Sys_fld_partXml { get; set; } //词条的xml字符串，词条xml字符串
        public string Sys_fld_Title { get; set; } //词条的部分标题，词条的部分标题
        public string Sys_fld_Parentdoi { get; set; } //词条的父doi，词条的父doi
        public int Sys_fld_HasSubEntry { get; set; } //是否有子词条，0是没有，1是有
        public int Sys_fld_HasPartContent { get; set; } //是否有partcontent，0是没有，1是有
    }
}
