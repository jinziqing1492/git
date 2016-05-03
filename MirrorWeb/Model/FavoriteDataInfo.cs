using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class FavoriteDataInfo
    {
        public string ID { get; set; } //记录id
        public string Name { get; set; } //名称
        public string DOI { get; set; } //各种资源的DOI
        public string UrlAddress { get; set; } //图书地址
        public int BookType { get; set; } //统一定义
        public DateTime OperatorDate { get; set; } //添加时间
        public string Operator { get; set; } //添加人
        public string Remark { get; set; } //备注
    }
}
