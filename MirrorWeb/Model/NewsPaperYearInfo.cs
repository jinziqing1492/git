using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 报纸年信息表
    /// </summary>
    public class NewsPaperYearInfo : BaseModel
    {
        public string BASEID { get; set; } //拼音刊名，拼音刊名
        public string CNAME { get; set; } //中文刊名，中文刊名
        public int YEAR { get; set; } //年，年
        public string ISSUE { get; set; } //期，期
        public string Yearissue { get; set; } //年期，年期
        public string THNAME { get; set; } //，拼音和年期组合（相当于id）
        public string TypeId { get; set; } //分类号，中图分类号
        public string Type { get; set; } //期刊类型，日报、期报（周报，周二报，周三报，周四报）
        public string Pubdep { get; set; } //出版单位，
        public int Recommendcount { get; set; } //推荐次数，
        public DateTime Pubdate { get; set; } //出版时间，
        public string Hostdep { get; set; } //主办单位，
        public string PubPlace { get; set; } //出版地，
        public string Chiefeditor { get; set; } //主编，
        public string Productor { get; set; } //出品人，
        public string Editor { get; set; } //编辑，
        public string Keywords { get; set; }//关键词
    }
}
