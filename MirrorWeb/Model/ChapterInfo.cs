using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 文章信息表
    /// </summary>
    public class ChapterInfo : BaseModel
    {
        public string Title { get; set; } //标题，标题
        public string Content { get; set; } //内容，章节的正文
        public string ParentDoi { get; set; } //父id，对应信息标准信息表里的标准的唯一标识或者图书的doi
        public string ParentName { get; set; } //父资源名称，父资源名称
        public string pubdate { get; set; } //发布时间，发布时间
        public string finddate { get; set; } //获取时间，获取时间
        public int doctype { get; set; } //类型，0图书，1标准
        public string keyword { get; set; } //关键词，关键词
        public string SYS_FLD_PARENTDOI { get; set; } //父节点doi，上级节点的doi
        public int Sys_fld_PageNo { get; set; } //实际页码，Pdf中的页号可以直接定位过去
        public int SYS_FLD_ISPART { get; set; } //是否是章，0是节，1是chapter，2是part
        public int Sys_fld_ordernum { get; set; } //排序字段，用来排序
        public string Sys_fld_xpath { get; set; } //对应的xpath，
        public string Sys_fld_originalID { get; set; } //从xml里提取出来的id，
    }
}
