using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 年鉴年表
    /// </summary>
    public class YearBookYearInfo : BaseModel
    {
        //public string Name { get; set; } //年鉴中文名，年鉴中文名
        public string ENName { get; set; } //年鉴英文名，年鉴英文名
        public string Baseid { get; set; } //拼音名，拼音名称
        public string Author { get; set; } //主编，作者
        public string LiabilityDesc { get; set; } //责任说明，文献作者加上责任方式例如：刘晓黎 主编
        public string EditOrg { get; set; } //编辑单位，
        public string ChargeDep { get; set; } //主管部门，
        public string Hostdep { get; set; } //主办单位，
        public string LayoutDep { get; set; } //排版单位，
        public string PrintDep { get; set; } //印刷单位，
        public string ISBN { get; set; } //书号，
        public string issn { get; set; } //，
        public string Cn { get; set; } //，
        public string IssueDep { get; set; } //出版单位，
        public DateTime IssueDate { get; set; } //出版时间，
        public string Year { get; set; } //年鉴年份，
        public string Country { get; set; } //国家，
        public string City { get; set; } //市，全省范围
        public string Provice { get; set; } //省，全国范围
        public string Digest { get; set; } //内容提要，
        public string County { get; set; } //县，全市范围
        public string ThemeWord { get; set; } //主题词，
        public string Language { get; set; } //文(语)种，
        public string ExecutiveEditor { get; set; } //责任编辑，
        public string CharCount { get; set; } //字数，字数和单位
        public string Sheets { get; set; } //印张，
        public string Printing { get; set; } //印数，
        public string MaxPageNO { get; set; } //图书最大页码，
        public int PdfTotalCount { get; set; } //pdf总页数，
        public string BindingFormat { get; set; } //装帧形式，
     
        public string EPrice { get; set; } //电子书定价，
        public string FullText { get; set; } //全文入库描述，全文
        public string Annotations { get; set; } //附注，
        public int TotalVolume { get; set; } //总卷数，
        public int TotalBook { get; set; } //总册数，
        public string Keywords { get; set; } //关键词，标引的关键词
        //public string Sys_Fld_Reference { get; set; } //参考文献，参考文献xml
        public string SYS_Fld_Ordernum { get; set; }//排序字段
        //public string SYS_Fld_ParaXml { get; set; }//文章对应的xml
        public string Issue { get; set; } //期，CHAR
    }
}
