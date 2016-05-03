using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 合同表
    /// </summary>
    public class ContractInfo:BaseModel
    {
        public string CONTRACTNAME { get; set; } //合同名称，
        public string CONTRACTNO { get; set; } //合同号，
        public DateTime BEGINDATETIME { get; set; } //版权开始时间，
        public DateTime ENDDATETIME { get; set; } //版权截止时间，
        public DateTime SIGNDATE { get; set; } //合同签订时间，
        public string BOOKID { get; set; } //图书ID，授权图书列表 这个应该存储一个字符串列表bookid#booktype;bookid#booktype
        public string Parta { get; set; } //原著作权人，
        public string Author { get; set; }//作者(署名),
        public string Agent { get; set; }//代理人/被授权人
        public string Postcode { get; set; } //邮编，
        public string Phonenum { get; set; } //电话，
        public string Address { get; set; } //通信地址，
        public string Workdep { get; set; } //工作单位，
        public string Partb { get; set; } //乙方，
        public string DESCRIPTION { get; set; } //合同描述，DESCRIPTION
        public string EXECUTIVEDEPT { get; set; } //出版社责任部门，
        public string LICENSEAREA { get; set; } //授权（范围）地区，中国内地/香港地区/澳门地区/台湾地区/其他地区,国内，全球（取值根据具体定夺）
        public string LICENSELANGUAGE { get; set; } //授权语言，中文简体版/繁体中文/少数民族语言/英文/其他
        public string LICENSEMEDIA { get; set; } //授权媒介，图书/期刊/电子(数字化)出版物/信息网络传播
        public string signedRight { get; set; } //签订的著作权，著，编著，编写，编，译，主译，编译，主编，总主编，分卷(册)主编，审定，审校，校订
        public int Issole { get; set; } //是否是独家，独家1，非独家2
        public string IshaveEfile { get; set; } //是否有电子版，
        public string Proportion { get; set; } //分层比例，
        public string Remark { get; set; } //备注，
        /// <summary>
        /// 档案号
        /// </summary>
        public string FileNO { get; set; }
        public string SelectedTopicNum { get; set; }
        public string ISBNStr { get; set; } //ISBN列表
        //public string SYS_FLD_FILEPATH { get; set; } //合同文件路径，
        //public string SYS_FLD_VIRTUALPATHTAG { get; set; } //虚拟路径标识，
        //public string SYS_FLD_DOI { get; set; } //DOI，
    }
}
