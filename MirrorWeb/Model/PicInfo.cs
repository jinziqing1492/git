using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图片表
    /// </summary>
    public class PicInfo : BaseModel
    {
        //public string Name { get; set; } //名称，标题
        public string Keywords { get; set; } //关键词，关键词
        public string Description { get; set; } //描述，描述信息
        public string Themeword { get; set; } //主题词，主题词，可以做出虚拟字段进行映射
        public int Type { get; set; } //精度，高质量1、普通2、低质量3
        //public string Note { get; set; } //备注，备注
        public string Language { get; set; } //语种，语种
        public string Source { get; set; } //来源，来源
        public DateTime Dateissued { get; set; } //出版时间，发布时间
        public string PicType { get; set; } //图片类型，0，本身资源图片,1公式、2图表，3资源里的插图，4符号类型,5行内图
        public int PicSize { get; set; } //图片大小，大小
        public int PicTime { get; set; } //时长，时长
        public string Author { get; set; } //作者，作者
        public string AuthorDESC { get; set; } //作者简介，作者简介
        public string Place { get; set; } //拍摄地，
        public string ParentDoi { get; set; } //父节点doi，父节点的唯一标识，如果有说明是图书的一个附件，是图书doi或者是标准doi或者是工具书doi
        public string Mode { get; set; } //机型，
        public string ExposureTime { get; set; } //曝光时间，
        public string ISOSensitivity { get; set; } //ISO感光度，
        public string ExifVersion { get; set; } //Exif版本，
        public string Aperture { get; set; } //光圈，
        public string ShootingTime { get; set; } //拍摄时间，
        public string ExposureCompensation { get; set; } //曝光补偿，
        public string focal { get; set; } //焦距，
        public string WhiteBalance { get; set; } //白平衡，
        public string SceneType { get; set; } //场景拍摄类型，
        public int Sys_fld_ParentType { get; set; } //父资源的类型，统一规定
        public int Sys_fld_PageNo { get; set; } //实际页码，Pdf中的页号可以直接定位过去
        public string Sys_fld_ChapterDoi { get; set; } //章节doi，
    }
}
