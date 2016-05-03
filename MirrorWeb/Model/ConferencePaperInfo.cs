using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 会议论文集表
    /// </summary>
    public class ConferencePaperInfo : BaseModel
    {
        public string ConferenceName { get; set; } //会议名称，
        public string ConferenceTime { get; set; } //会议时间，
        public string ConferenceAddress { get; set; } //会议地点，
        public string ConferenceOrganiser { get; set; } //会议主办单位，
        public string Co_sponsor { get; set; } //协办单位，
        public string sponsor { get; set; } //承办单位，
        public int InternalResourcesFlag { get; set; } //是否内部资源，
        //public string Name { get; set; } //书名，含副书名
        public string ENName { get; set; } //原书名，原版书外文名称
        public string Author { get; set; } //作者（主编），作者（主编）
        public string AuthorDesc { get; set; } //作者简介，作者简介
        public string LiabilityForm { get; set; } //责任方式，
        public string OtherLiable { get; set; } //其他责任者（编辑），
        public string OtherLiableDesc { get; set; } //其他责任者简介，
        public string OtherLiableForm { get; set; } //其他责任方式，
        public string ENauthor { get; set; } //原作者姓名，
        public string ENAuthorDesc { get; set; } //原作者简介，
        public string ENLiabilityForm { get; set; } //原责任方式，
        public string ISBN { get; set; } //书号，国际标准书号
        public string ISSN { get; set; } //国际标准连续出版物编号，国际标准连续出版物编号
        public string CN { get; set; } //国内统一刊号，国内统一刊号
        public string IssueDep { get; set; } //出版单位，
        public DateTime FirstIssueDate { get; set; } //首版时间，
        public string PrintNUM { get; set; } //本版版次印次，
        public string Digest { get; set; } //内容提要，
        public string OnLineSaleAdvice { get; set; } //上架建议，
        public string EssenceDigest { get; set; } //精华书摘，
        public string ThemeWord { get; set; } //主题词，
        public DateTime IssueDate { get; set; } //出版时间，
        public string Language { get; set; } //文(语)种，
        public string ExecutiveEditor { get; set; } //责任编辑（副主编），
        public string Format { get; set; } //开本，包含纸张尺寸信息
        public string CharCount { get; set; } //字数，字数和单位
        public string Sheets { get; set; } //印张，
        public string Printing { get; set; } //印数，
        public string MaxPageNO { get; set; } //图书最大页码，
        public int PdfTotalCount { get; set; } //pdf总页数，
        public string BindingFormat { get; set; } //装帧形式，
        public string EPrice { get; set; } //电子书定价，
        public string LegalStatement { get; set; } //法律声明，
        public string FullText { get; set; } //全文入库描述，全文
        public DateTime RegistrationDate { get; set; } //登记日期，
        public string Keywords { get; set; } //关键词，
        //public string ENName { get; set; } //英文刊名
    }
}
