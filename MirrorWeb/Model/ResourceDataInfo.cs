using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class ResourceDataInfo
    {
        public string SYS_FLD_DOI { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string AUTHOR { get; set; } //作者
        public string PUBDEP { get; set; } //出版单位
        public DateTime PUBDATE { get; set; } //出版时间
        public string PRICE { get; set; } //价格
        public string DESCRIPTION { get; set; } //简介
        public string FILEPATH { get; set; }
        public string COVERPATH { get; set; }
        public string CATALOGPATH { get; set; }
        public DateTime CREATETIME { get; set; } //创建时间
        public int STATUS { get; set; } //状态
        public string PARENTID { get; set; } //父ID
    }
}
