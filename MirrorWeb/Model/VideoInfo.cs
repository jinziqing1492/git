using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{
    /// <summary>
    /// 视频表
    /// </summary>
    public class VideoInfo : BaseModel
    {
        //public string Name { get; set; } //名称，视频标题
        public string Keywords { get; set; } //关键词，关键词
        public string Description { get; set; } //描述，视频描述信息
        public string Themeword { get; set; } //主题词，主题词，可以做出虚拟字段进行映射
        public int Type { get; set; } //精度：2高清、0标清、1普通
        //public string Note { get; set; } //备注，备注
        public string Language { get; set; } //语种，语种
        public string Source { get; set; } //来源，来源
        public DateTime IssueDate { get; set; } //出版时间，出版时间
        public string VideoType { get; set; } //视频类别，视频类别
        public string VideoSize { get; set; } //视频大小，视频大小
        public string videoTime { get; set; } //视频时长，视频时长
        public string Author { get; set; } //作者，作者
        public string AuthorDesc { get; set; } //作者简介，作者简介
        public string Accessories { get; set; } //片段信息，图片的相对路径#时间+下一个图片的相对路径#时间
        public string ParentDoi { get; set; } //父节点doi，
        public int Sys_fld_ParentType { get; set; } //父资源的类型，统一设置
    }
}
