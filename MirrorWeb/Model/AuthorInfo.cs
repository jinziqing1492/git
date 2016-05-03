using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 作者信息表
    /// </summary>
    public class AuthorInfo:BaseModel
    {
        //public string Name { get; set; } //作者名称，
        public int Sex { get; set; } //性别，0不确定1男2女
        public string Othername { get; set; } //其他曾用名，用于搜索用，用分号隔开
        public DateTime Birthday { get; set; } //出生年月，
        public string Hometown { get; set; } //籍贯，
        public string OrgID { get; set; } //所属机构id，所属机构id
        public string OrgName { get; set; } //所属机构名称，先建机构再选作者
        public string TEL1 { get; set; } //电话，电话 固话
        public string TEL2 { get; set; } //电话，电话 手机
        public string EMail { get; set; } //电子邮件，电子邮件
        public string Digest { get; set; } //人物简介，
        //public string SYS_FLD_FILEPATH { get; set; } //人物照片路径，图片的相对路径
        //public string SYS_FLD_VIRTUALPATHTAG { get; set; } //虚拟路径标识，虚拟路径标识
        //public string SYS_FLD_DOI { get; set; } //DOI，编号
        public string Remark { get; set; } //备注，
        //public DateTime Sys_fld_Adddate { get; set; } //数据入库时间，
        //public string Sys_fld_Adduser { get; set; } //数据入库人，
        public string IDNUM { get; set; } //身份证号，
        public string COUNTRY { get; set; } //国籍，
        public string WRITENAME { get; set; } //笔名，
        public string POSITION { get; set; } //职务，
        public string ACADEMICTITLE { get; set; } //职称，
        public string ADDRESSNOW { get; set; } //现居住地，
        public string Mainwork { get; set; } //主要作品，
        //public int SYS_FLD_CHECK_STATE { get; set; } //审核状态，0未审核，-1审核通过

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string BankDeposit { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountNum { get; set; }
        /// <summary>
        /// 其他联系方式
        /// </summary>
        public string Othercontact { get; set; }
    }
}
