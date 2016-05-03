using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 任务信息表
    /// </summary>
    public class MissionInfo
    {
        public int ID { get; set; } //标识
        public string Name { get; set; } //名称
        public int ResType { get; set; } //资源类型
        public string ResDOI { get; set; } //资源数据DOI
        public string ResName { get; set; } //资源数据名称
        public string ExecuteUser { get; set; } //执行用户
        public string SendUser { get; set; } //派遣用户
        public DateTime DeadLine { get; set; } //截止日期
        public DateTime FinishDate { get; set; } //完成日期
        public DateTime CreateTime { get; set; } //创建日期
        public string Remark { get; set; } //备注
        public string Operator { get; set; } //指派的审核用户
        public int WorkStatus { get; set; } //任务状态
        public string AuditRemark { get; set; } //审核备注
        public int SYS_FLD_CHECK_STATE { get; set; }  //  审核状态  0未审核，-1审核通过  
        public int isSended { get; set; }//是否分配 0是未分配，1是分配
        public string Reject { get; set; }//驳回原因
        public int FinishStatus { get; set; }//完成状态 0正常1驳回
        public int IsBat { get; set; }//是否是批量导入 0不是 1是批量导入
        public string Department { get; set; }//部门编号
        public string DepartmentName { get; set; }//部门名称

        //以下的字段是用于在列表中显示
        public string ResTypeStr { get; set; }//资源类型名称
        public string WorkStatusStr { get; set; }//任务状态名称
        public string OperateStr { get; set; }//对应记录操作的 html 串

    }
}
