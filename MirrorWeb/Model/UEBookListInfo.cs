using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class UEBookListInfo
    {
        public string ID { get; set; } //记录id
        public string Name { get; set; } //U盘产品名称
        public string BookId { get; set; } //图书id
        public string BookName { get; set; } //图书名称
        public int BookType { get; set; } //统一定义
        public DateTime OperatorDate { get; set; } //添加时间
        public string Operator { get; set; } //添加人
    }
}
