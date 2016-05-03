using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 杂志表
    /// </summary>
    public class MagazineInfo : BaseModel
    {
        public string BASEID { get; set; } //拼音刊名，拼音刊名
        public string CNAME { get; set; } //中文刊名，中文刊名
        public string Description { get; set; } //简介描述，
        public string FoundDate { get; set; } //建刊时间，
        public string OtherName { get; set; } //其它曾用名，
        public string TypeId { get; set; } //中图分类号，
        public string CN { get; set; } //，
        public string ISSN { get; set; } //，国际标准连续出版物编号
        public string Type { get; set; } //期刊类型，按期，定期(周刊、旬刊、半月刊、月刊、双月刊、季刊、半年刊、年刊等)；不定期
        public string Hostdep { get; set; } //主办单位，
        public string Pubdep { get; set; } //出版单位，
        public string PubPlace { get; set; } //出版地，
        public string Language { get; set; } //语种，
        public int ISRecommend { get; set; } //推荐，0是默认值1是推荐
        public string ChiefEditor { get; set; }//主编
        public string ChiefEmail { get; set; }//主编邮箱
        public string ContributeEmail { get; set; }//投稿邮箱
        public string Contract { get; set; }//联系方式
    }
}
