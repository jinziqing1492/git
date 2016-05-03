using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class SubterminologyInfo
    {
        public string Name { get; set; } //名称，搜索名称
        public string SYS_FLD_DOI { get; set; } //DOI，编号
        public int isonline { get; set; } //是否上架，0是下架 1是上架
        public string Sys_fld_partXml { get; set; } //词条的部分xml，词条的部分xml
        public string EntryDoi { get; set; } //所属词条doi，所属词条doi
        public string EntryName { get; set; } //所属词条名称，所属词条名称
        public string Parentdoi { get; set; } //词条的父doi，词条的父doi
        public string ParentName { get; set; } //父词条的名称，词条的父名称
        public string parentAttrPath { get; set; } //父词条的属性路径，存放词条的属性路径，为提供纵向检索提供关联（针对河湖大典）
        public string Sys_fld_partXml_U { get; set; } //词条的部分xml，词条的部分xml
    }
}
