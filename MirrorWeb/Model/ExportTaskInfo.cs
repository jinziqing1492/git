using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 导出任务表
    /// </summary>
    public class ExportTaskInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 导出给用户
        /// </summary>
        public string ToUser { get; set; }
        /// <summary>
        /// 导出日期
        /// </summary>
        public DateTime ExportDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建任务用户
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 任务类型1 分销数据打包2镜像数据打包3是MARC数据导出 4是U盘电子书
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 任务状态 0是资源收集1是待处理2处理中 -1是完成状态
        /// </summary>
        public int TaskStatus { get; set; }
        /// <summary>
        /// 分层比例6:4对方：自己
        /// </summary>
        public string Proportion { get; set; }
        /// <summary>
        /// 是否已下载，0是未下载，1是已下载
        /// </summary>
        public int IsDownload { get; set; }
        /// <summary>
        /// 主体文件名
        /// </summary>
        public string SYS_FLD_FILEPATH { get; set; } 
        /// <summary>
        ///  虚拟目录的标记
        /// </summary>
        public string SYS_FLD_VIRTUALPATHTAG { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作字符串
        /// </summary>
        public string OperateStr { get; set; }
        /// <summary>
        /// 选择数据库类型 0是全库，1基础数据库 2业务应用库
        /// </summary>
        public int SelDatabaseType { get; set; }
    }
}
