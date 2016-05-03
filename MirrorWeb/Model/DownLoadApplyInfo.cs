using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 下载申请表
    /// </summary>
    public class DownLoadApplyInfo
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 描述 自己拼接信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 附件id
        /// </summary>
        public string AttachmentID { get; set; }
        /// <summary>
        /// 附件名
        /// </summary>
        public string AttachmentName { get; set; }
        /// <summary>
        /// 审核用户
        /// </summary>
        public string AuditUser { get; set; }
        /// <summary>
        /// 0是待审核 -1是审核通过 -2是审核没通过
        /// </summary>
        public int CheckStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string CheckDescription { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDate { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckDate { get; set; }
        /// <summary>
        /// 0是没有下载，1是下载过
        /// </summary>
        public int IsDownload { get; set; }
        /// <summary>
        /// 0是附件表1是路径  路径存在 AttachmentID
        /// </summary>
        public int AttachmentType { get; set; }
        /// <summary>
        /// 操作字符串 
        /// </summary>
        public string  OperateStr { get; set; }
        /// <summary>
        /// 审核状态字符串
        /// </summary>
        public string CheckStatusStr { get; set; }
    }
}
