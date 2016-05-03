using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class AttachmentInfo
    {
        public string SYS_FLD_DOI { get; set; } //系统doi，doi
        public string PARENTDOI { get; set; } //所属资源doi，主表id
        public string Name { get; set; } //，附件名称
        public string Type { get; set; } //文件类型，资源类型描述高精度pdf，封面的排版文件，内文的排版文件，扫描文件，配套文件，课件
        public string SYS_FLD_FILEPATH { get; set; } //文件路径，文件路径
        public string SYS_FLD_VIRTUALPATHTAG { get; set; } //虚拟路径标识，虚拟标识
        public string Sys_fld_filetype { get; set; } //文件类型，用以区分附件，是什么类型的文件是否能直接浏览
        public int SYS_SYSID { get; set; } //系统id，系统标识
        public DateTime Sys_fld_Adddate { get; set; } //数据入库时间，
        public string Sys_fld_Adduser { get; set; } //数据入库人，
        public string Audituser { get; set; } //审核人，
        public string EditTool { get; set; } //制作软件，
        public string Remark { get; set; } //备注，
        public string Sys_fld_ChapterDoi { get; set; } //章节doi，
        public string Department { get; set; }//部门编号 
        public int SYS_FLD_PARENTTYPE { get; set; }//父资源类型
        public string Parentname { get; set; } //所属资源名称

    }
}
