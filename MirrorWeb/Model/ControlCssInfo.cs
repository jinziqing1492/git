using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    public class ControlCssInfo : BaseModel
    {
        public string ID { get; set; }//编号
        //public string Name { get; set; }//名称
        public string CssParam { get; set; }//保留
        public string Type { get; set; }//控件类型
        public string CssName { get; set; }//Css样式名
    }
}
