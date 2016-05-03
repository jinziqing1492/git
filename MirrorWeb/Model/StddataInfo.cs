using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 标准表
    /// </summary>
    public class StdDataInfo : BaseModel
    {
        public string stdtype { get; set; } //标准类型，标准类型 (GB、GJB、HB、可扩展)
        public string stdno { get; set; } //标准号，标准号
        //public string Name { get; set; } //标准名称，中文名称
        public string ENName { get; set; } //标准英文名，英文名称
        public string otherName { get; set; } //其他语言名称，其他语言名称
        public string PartName { get; set; } //部分名称，分部分名称
        public string ENPartName { get; set; } //部分英文名，分部分名称英文
        public string OtherPartName { get; set; } //其他语言部分名称，分部分名称其他语言
        public string Language { get; set; } //语言，语言（语种默认值中文）
        public string stage { get; set; } //阶段，阶段（状体应该是一个枚举类型，大纲、初稿、征求意见稿、送审稿、报批稿、发布稿（默认值 ））
        public DateTime Dateissued { get; set; } //发布时间，发布时间
        public DateTime DateImplement { get; set; } //实施时间，实施时间
        public string Status { get; set; } //状态，有效性（有效、废止、试行、限制使用）
        public string Version { get; set; } //版本，版本
        public string RecordNO { get; set; } //备案号，备案号
        public string ApproveDep { get; set; } //批准单位，批准单位
        public string HostInstitution { get; set; } //主持机构，主持机构
        public string ProposeDep { get; set; } //主编单位，
        public string BelongDep { get; set; } //归口单位（提出单位），归口单位（解释单位）
        public string DraftDep { get; set; } //起草单位，起草单位
        public string DraftPerson { get; set; } //起草人，起草人
        public string RMTHead { get; set; } //审查会议技术负责人，审查会议技术负责人
        public string StyleFormatReview { get; set; } //体例格式审查人，体例格式审查人
        public string ExplainDep { get; set; } //解释单位，调整结构后添加的
        public string CompileDep { get; set; } //参编单位，这个是调整结构添加的
        public string PublishDep { get; set; } //出版发行单位，这个是调整结构添加的
        public string IssuedDep { get; set; } //发布部门，这个是调整结构添加的
        public string StdNoseries { get; set; } //系列标准说明，系列标准说明
        public string PartstdNo { get; set; } //分部分标准说明，分部分标准说明
        public string replaceStdNo { get; set; } //代替标准号，代替标准号
        public string PreRelease { get; set; } //历次发布版本，历次发布版本（这个是版本号）
        public string Internalstdno { get; set; } //国际标准号，采用国际标准号
        public string Internalstdname { get; set; } //国际标准名称，采用国际标准名称
        public string Consistency { get; set; } //一致性，采标一致性程度（枚举值NEQ、IDT、MOD）
        public string Scope { get; set; } //适用范围，适用范围 +规定内容 （范围）
        public string FullText { get; set; } //全文，全文
        public int Fulltextlength { get; set; } //全文长度，全文长度
        public string Keywords { get; set; } //关键词，关键词
        public string Digest { get; set; } //摘要，摘要
        public string RefstdNo { get; set; } //引用标准，引用标准（xml里分号和名称两个项，这里只需要号）
        public string ACCESSORIES { get; set; } //标准相关附件，标准相关附件及说明（多个附件，有名称和附件本身）
        //public string Sys_Fld_Reference { get; set; } //参考文献，参考文献xml

    }
}
