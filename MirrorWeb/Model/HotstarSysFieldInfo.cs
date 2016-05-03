using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 导出excel所用的系统表
    /// </summary>
    public class HotstarSysFieldInfo
    {
        public string Table_Name { get; set; } //表名
        public string Column_Name { get; set; } //字段名
        public int Field_Type { get; set; } //字段的数据类型
        public int Column_Size { get; set; } //字段大小
        public string Column_Def { get; set; } //字段定义
        public string Field_Check { get; set; } //来源，来源
        public int Field_IndexType { get; set; } //字段的索引类型
        public string Field_AliasName { get; set; } //字段别名
        public string Field_DispName { get; set; } //字段展示名
        public string Type_Name { get; set; } //索引类型名称
        public int Num_Prec_radix { get; set; } //字段的....
        public string Remarks { get; set; } //备注
        public int Char_Octet_Length { get; set; } //字符octet长度
        public int Ordinal_Position { get; set; } //原始位置
        public bool Is_Nullable { get; set; } //是否可为空
    }
}
