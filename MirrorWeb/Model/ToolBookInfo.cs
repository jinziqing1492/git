using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 工具书表
    /// </summary>
    public class ToolBookInfo : BaseModel
    {
        //public string Name { get; set; }  //  书名  含副书名  
        public string ENName { get; set; }  //  原书名  原版书外文名称  
        public string Author { get; set; }  //  作者  作者  
        public string AuthorDesc { get; set; }  //  作者简介  作者简介  
        public string LiabilityForm { get; set; }  //  责任方式  文献作者  
        public string OtherLiable { get; set; }  //  其他责任者    
        public string OtherLiableDesc { get; set; }  //  其他责任者简介    
        public string OtherLiableForm { get; set; }  //  其他责任方式    
        public string ENauthor { get; set; }  //  原作者姓名    
        public string ENAuthorDesc { get; set; }  //  原作者简介    
        public string ENLiabilityForm { get; set; }  //  原责任方式    
        public string ISBN { get; set; }  //  书号    
        public string IssueDep { get; set; }  //  出版单位    
        public DateTime FirstIssueDate { get; set; }  //  首版时间    
        public string PrintNUM { get; set; }  //  本版版次印次    
        public string Digest { get; set; }  //  内容提要    
        public string OnLineSaleAdvice { get; set; }  //  上架建议    
        public string EssenceDigest { get; set; }  //  精华书摘    
        public string ThemeWord { get; set; }  //  主题词    
        public DateTime IssueDate { get; set; }  //  出版时间    
        public string Language { get; set; }  //  文(语)种    
        public string ExecutiveEditor { get; set; }  //  责任编辑    
        public string Format { get; set; }  //  开本  包含纸张尺寸信息  
        public string CharCount { get; set; }  //  字数  字数和单位  
        public string Sheets { get; set; }  //  印张    
        public string Printing { get; set; }  //  印数    
        public string MaxPageNO { get; set; }  //  图书最大页码    
        public int PdfTotalCount { get; set; }  //  pdf总页数    
        public string BindingFormat { get; set; }  //  装帧形式    
        //public string Price { get; set; }  //  纸书定价    
        public string EPrice { get; set; }  //  电子书定价    
        public string LegalStatement { get; set; }  //  法律声明    
        public string FullText { get; set; }  //  全文入库描述  全文  
        public DateTime RegistrationDate { get; set; }  //  登记日期    
        public string Annotations { get; set; }  //  附注    
        public string SeriesName { get; set; }  //  丛/套书名    
        public string SeriesEName { get; set; }  //  丛/套书外文名    
        public string SeriesAuthor { get; set; }  //  丛/套书作者    
        public string SeriesLiableForm { get; set; }  //  丛/套书责任方式    
        public string SeriesBookNo { get; set; }  //  套书的书号  ISBN，统一书号  
        public string SeriesPrice { get; set; }  //  丛/套书价格    
        public string SeriesSynopsis { get; set; }  //  丛/套书的简介    
        public string SeriesAnnotation { get; set; }  //  丛/套书附注    
        public DateTime SeriesEndIssueDate { get; set; }  //  丛套书的结束出版日期    
        public int SeriesIsIssue { get; set; }  //  丛套书是否已经全部出版  0是没出版，1是已出版  
        public int TotalVolume { get; set; }  //  总卷数    
        public int TotalBook { get; set; }  //  总册数    
        public int TotalCharactor { get; set; }  //  总字数    
        public int TotalPrinting { get; set; }  //  总印张    
        public string Keywords { get; set; }//关键词
        //public string Note { get; set; }//将存在标题中的注释提取出来放在这个字段中，这个字段不与数据库相对应
    }
}
