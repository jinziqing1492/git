using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 期刊年期信息表
    /// </summary>
    public class JournalYearInfo : BaseModel
    {
        public string BASEID { get; set; } //拼音刊名，拼音刊名
        public string CNAME { get; set; } //中文刊名，中文刊名
        public int YEAR { get; set; } //年，年
        public string ISSUE { get; set; } //期，期
        public string Yearissue { get; set; } //年期，年期
        public string THNAME { get; set; } //，拼音和年期组合（相当于id）
        public string Type { get; set; } //期刊类型，按期，定期(周刊、旬刊、半月刊、月刊、双月刊、季刊、半年刊、年刊等)；不定期
        public string Pubdep { get; set; } //出版单位，
        public int Recommendcount { get; set; } //推荐次数，
        public DateTime Pubdate { get; set; } //出版时间，
        //public int Sys_fld_ordernum { get; set; } //排序字段，
        public string Sys_fld_xpath { get; set; } //对应的xpath，
        //public string Sys_fld_originalID { get; set; } //从xml里提取出来的id，
        public string Keywords { get; set; }//关键词
    }
}
