using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 用户表
    /// </summary>
    public class UserInfo
    {
        public string UserName { get; set; } //用户名，用户名
        public string Password { get; set; } //密码加密，密码加密
        public string Role { get; set; } //角色，0普通用户，1资源收集人员，2收集审核，3是加工人员，4加工审核人员，5资源管理员，6系统管理员（可以维护作者，机构，合同表，原始资料库都能看）
        public string Rightstr { get; set; } //权限，用户能访问的数据库列表,就是数据库枚举类型的id序列用分号隔开
        public int IsOrg { get; set; } //是否是机构用户，0不是1是
        public int OnlineCount { get; set; } //当前在线用户数，当前在线用户数
        public int MaxOnlineCount { get; set; } //最大在线用户数，最大在线用户数
        public string DEPARTMENT { get; set; } //部门编号，部门编号
        public string RealName { get; set; } //真实姓名，真实姓名
        public string TEL1 { get; set; } //电话，电话
        public string TEL2 { get; set; } //电话，电话
        public string EMail { get; set; } //电子邮件，电子邮件
        public int LEVEL { get; set; } //密级，密级
        public DateTime LASTVISIT { get; set; } //上次登录时间，上次登录时间
        public DateTime ADDDATE { get; set; } //用户创建时间，用户创建时间
        public DateTime UserLockDATE { get; set; } //用户锁定时间，用户锁定时间
        public DateTime UserUnlockDate { get; set; } //用户解锁时间，用户解锁时间
        public DateTime LASTEDITPASS { get; set; } //上次修改密码时间，上次修改密码时间
        public string IPstart { get; set; } //绑定的ip开始，绑定起始ip
        public string Ipend { get; set; } //绑定的ip结束，绑定结束ip
        public string IpstartNum { get; set; } //Ip数字，起始ip对应数字
        public string Ipendnum { get; set; } //Ip数字，结束ip对应数字
        public int Flag { get; set; } //用户的状态，0正常，1锁定，2停用
        public int TRYNUM { get; set; } //登录失败的次数，登录失败的次数
        public int SHOWFLAG { get; set; } //保留，保留
        public string GroupID { get; set; } //用户分组，用户分组
        public string REMARK { get; set; } //备注，备注
        public int LOGINMODE { get; set; } //登录模式，0用户名密码，1是ic卡
        public string ICCardNum { get; set; } //Ic卡卡号，Ic卡卡号
        public string ICCardEmail { get; set; } //Ic卡email，Ic卡内记录的email
        public int UserType { get; set; } //用户类别，用户类别：1政府部门，2高等院校，3研究机构，4决策咨询机构，5大中型企业单位，6图书馆
        public string UserInterest { get; set; } //用户兴趣，列出一些关键词
        public int TERMINALCOUNT { get; set; } //终端数，
        public string PwdQuestion { get; set; } //密码提示问题，
        public string pwdAnswer { get; set; } //密码提示答案，
        public string workUnit { get; set; }//工作单位

        /// <summary>
        /// 头像
        /// </summary>
        public string SYS_FLD_HEADPIC { get; set; }
        /// <summary>
        /// 虚拟路径标识
        /// </summary>
        public string SYS_FLD_VIRTUALPATHTAG { get; set; }
        /// <summary>
        /// 用户身份令牌
        /// </summary>
        public string TOKEN { get; set; }
    }
}
