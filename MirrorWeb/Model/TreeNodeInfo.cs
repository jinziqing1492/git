using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model 
{
   public class TreeNodeInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否含有子节点
        /// </summary>
        public bool IsParent { get; set; }
    }
}
