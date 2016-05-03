using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class NewsPaperArticleInfo : BaseModel
    {
        public string Title { get; set; } //篇名，EXTITLE词干
        public string FirstAuthor { get; set; } //第一作者，MVCHAR
        public string Author { get; set; } //作者，MSTRCHAR
        public string Institution { get; set; } //机构，MSTRCHAR
        public string Keywords { get; set; } //关键词，MSTRCHAR
        public string CNAbstract { get; set; } //中文摘要，LFTEXT
        public string ENTitle { get; set; } //英文篇名，EXTITLE词干
        public string ENAuthor { get; set; } //英文作者，MSTRCHAR
        public string ENAbstract { get; set; } //英文摘要，LFTEXT
        public string ENKeyWord { get; set; } //英文关键词，MSTRCHAR
        public string LayoutName { get; set; } //版面名称，CHAR
        public string Ordernum { get; set; } //序号，CHAR
        public string SYS_FLD_parentdoi { get; set; } //上级doi，CHAR
        public string Mapstr { get; set; } //区域坐标，LTEXT
        public string Year { get; set; } //年，CHAR
        public string Issue { get; set; } //期，CHAR
        public string CN { get; set; } //国内标准刊号，CHAR
        public string ISSN { get; set; } //国际标准刊号，CHAR
        public int PageNUM { get; set; } //页码，CHAR
        public string yearissue { get; set; } //年期，MVCHAR
        public string THNAME { get; set; } //THNAME，Echar
        public string BASEID { get; set; } //拼音刊名，ECHAR
        public string ParentDoi { get; set; } //所属刊物年期的sys_fld_doi，Char
        public string FullText { get; set; } //全文，LTEXT
        public string SubTitle { get; set; } //副标题
        public string SubEntitle { get; set; } //英文副标题
        public string Sys_fld_xpath { get; set; } //对应的xpath，None
        public string Sys_fld_originalID { get; set; } //从xml里提取出来的id，CHAR

    }
}
