using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 机构信息表
    /// </summary>
    public class OrgInfo:BaseModel
    {
        //public string Name { get; set; } //名称，组织机构名
        public string Place { get; set; } //组织地点，组织地点
        public DateTime Founddate { get; set; } //成立日期，
        public string TEL1 { get; set; } //电话，电话 固话
        public string TEL2 { get; set; } //电话，电话 手机
        public string Contractperson { get; set; } //联系人，联系人
        public string Contracttel { get; set; } //联系电话，联系电话
        //public string SYS_FLD_DOI { get; set; } //DOI，编号
        public string Remark { get; set; } //备注，
        //public string Sys_fld_Adddate { get; set; } //数据入库时间，
        //public string Sys_fld_Adduser { get; set; } //数据入库人，
        public string PROVINCE { get; set; } //省份，
        public string CITY { get; set; } //城市，
        public string ADDRESS { get; set; } //通信地址，
        public string EMAIL { get; set; } //电子邮箱，
        public string WEBSITE { get; set; } //网址，
        public string DESCRIPTION { get; set; } //机构简介，
        //public int SYS_FLD_CHECK_STATE { get; set; } //审核状态，0未审核，-1审核通过
        //public string SYS_FLD_FILEPATH { get; set; } //文件路径，文件的相对路径
        //public string SYS_FLD_VIRTUALPATHTAG { get; set; } //虚拟路径标识，虚拟路径标识
    }
}
