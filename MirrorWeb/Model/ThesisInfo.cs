using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 学术论文表
    /// </summary>
    public class ThesisInfo : BaseModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Author { get; set; } //姓名，CHAR
        /// <summary>
        /// 姓名拼音
        /// </summary>
        public string PYAuthor { get; set; } //姓名拼音，CHAR
        /// <summary>
        /// 学号
        /// </summary>
        public string SNO { get; set; } //学号，CHAR
        /// <summary>
        /// 学院
        /// </summary>
        public string Academy { get; set; } //学院，CHAR
        /// <summary>
        /// 系室
        /// </summary>
        public string DepartmentName { get; set; } //系室，CHAR
        /// <summary>
        /// 学科
        /// </summary>
        public string Subject { get; set; } //学科，CHAR
        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; } //专业，CHAR
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; } //联系电话，CHAR
        public string Email { get; set; } //电子邮件，CHAR
        public string Degree { get; set; } //学位，CHAR
        /// <summary>
        /// 指导教师
        /// </summary>
        public string Instructor { get; set; } //指导教师，CHAR
        public string TutorName1 { get; set; } //导师1姓名，CHAR
        public string PYTutorName1 { get; set; } //导师1姓名拼音，CHAR
        public string PYTutore1Department { get; set; } //导师1单位，MSTRCHAR
        public string TutorName2 { get; set; } //导师2姓名，CHAR
        public string PYTutorName2 { get; set; } //导师2姓名拼音，CHAR
        public string PYTutore2Department { get; set; } //导师2单位，MSTRCHAR
        public string TutorName3 { get; set; } //导师3姓名，CHAR
        public string PYTutorName3 { get; set; } //导师3姓名拼音，CHAR
        public string PYTutore3Department { get; set; } //导师3姓名单位，MSTRCHAR
        //public string Name { get; set; } //中文题名，LTEXT
        public string Keywords { get; set; } //关键词，MSTRCHAR
        public string Abstract { get; set; } //静态摘要，L TEXT
        public DateTime PaperSubmissionDate { get; set; } //论文提交日期，CHAR
        public DateTime OralDefenseDate { get; set; } //论文答辩日期，CHAR
        public DateTime DegreeAwardDate { get; set; } //学位授予日期，CHAR
        /// <summary>
        /// 学位年度
        /// </summary>
        public string DegreeYear { get; set; } //学位年度，CHAR
        public string Fund { get; set; } //基金，MSTRCHAR
        public string ENName { get; set; } //英文题名，LTEXT
        public string ENKeyWord { get; set; } //英文关键词，MSTRCHAR
        public string ENAbstract { get; set; } //英文文摘，L TEXT
        public string ResearchField { get; set; } //研究方向，LTEXT
        //public string Sys_Fld_Reference { get; set; } //参考文献，None
        public string FullText { get; set; } //全文，LTEXT
        public string ReleaseFixedYear { get; set; } //发布年限，CHAR
        public string CollectionNumber { get; set; } //馆藏号，CHAR
        public string SchoolCode { get; set; } //学校代码，CHAR
        public string Themeword { get; set; } //主题词，Mix
    }
}
