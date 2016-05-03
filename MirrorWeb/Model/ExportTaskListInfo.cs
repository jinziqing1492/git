using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 导出任务列表
    /// </summary>
    public class ExportTaskListInfo
    {
        /// <summary>
        ///标识        
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图书id
        /// </summary> 
        public string BookId { get; set; }
        /// <summary>
        /// 图书名
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 图书类型    
        /// </summary>
        public int BookType { get; set; }
        /// <summary>
        /// 分销价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 原始价格
        /// </summary>
        public double OriginalPrice { get; set; }
        /// <summary>
        /// 导出任务id
        /// </summary>
        public string ExportTaskId { get; set; }
        /// <summary>
        /// 导出任务类型
        /// </summary>
        public int ExportTaskType { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime OperatorDate { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 逻辑库id
        /// </summary>
        public string LdbId { get; set; }
    }
}
