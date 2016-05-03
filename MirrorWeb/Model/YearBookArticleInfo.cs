using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 年鉴文章表
    /// </summary>
    public class YearBookArticleInfo : BaseModel
    {
        public string Title { get; set; } //文章名，CHAR
        public string EnTitle { get; set; } //英文文章名，CHAR
        public string ChiefEditor { get; set; } //主编，MSTRCHAR
        public string EditOrg { get; set; } //主编单位，CHAR
        public string Author { get; set; } //作者，MSTRCHAR
        public string AuthorDep { get; set; } //作者单位，MSTRCHAR
        public string Keyword { get; set; } //关键词，MSTRCHAR
        public string Provice { get; set; } //省，CHAR
        public string City { get; set; } //市，CHAR
        public string County { get; set; } //县，CHAR
        public string Citation { get; set; } //引文，MSTRCHAR
        public string Year { get; set; } //年，CHAR
        public string Issue { get; set; } //期，CHAR
        public string CN { get; set; } //国内标准刊号，CHAR
        public string ISSN { get; set; } //国际标准刊号，CHAR
        public string PageNUM { get; set; } //页码，CHAR
        public string ParentName { get; set; } //年鉴中文名，CHAR
        public string ParentEName { get; set; } //年鉴英文名，CHAR
        public string BASEID { get; set; } //拼音年鉴名，ECHAR
        public string ParentDoi { get; set; } //某年的年鉴的doi，Char
        public string SYS_FLD_PARENTDOI { get; set; } //父节点doi，CHAR
        public string FullText { get; set; } //全文，LTEXT
        public DateTime UpdateDate { get; set; } //更新日期，ECHAR
        //public string Sys_Fld_Reference { get; set; } //参考文献，None
        public string Sys_fld_xpath { get; set; } //对应的xpath，None
        public string Sys_fld_originalID { get; set; } //从xml里提取出来的id，CHAR
        public string SYS_Fld_ParaXml { get; set; }//文章对应的xml
        public int SYS_FLD_ISPART { get; set; } //是否是章，0是节，1是chapter，2是part
    }
}
