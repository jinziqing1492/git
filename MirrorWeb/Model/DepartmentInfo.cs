using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 部门信息表
    /// </summary>
    public class DepartmentInfo
    {
        public string Id { get; set; } //部门编号
        public string Name { get; set; } //某一分类的名称
        public string ParentID { get; set; } //所属的父分类的ID号(因为支持多级分类)
        public string SourceCode { get; set; } //源来的编码格式
        public string Ordernum { get; set; } //排序字段
        public string remark { get; set; } //备注
        public int HASCHILD { get; set; } //0表示无孩子，1表示有孩子
    }
}
