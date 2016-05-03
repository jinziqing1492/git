using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.Model
{/// <summary>
    /// 图书表
    /// </summary>
    public class DataBaseListInfo
    {
        public int Id { get; set; } //，标号
        public string Title { get; set; } //，库名称
        public string TableName { get; set; } //，表名称
        public string dirprefix { get; set; } //目录前缀，如：图书book，标准std，工具书toolbook，期刊journal，学位论文thesis，会议论文conpaper，视频video，音频 audio，图片 pic，杂志 magazine，报纸 newspaper，年鉴yearbook，合同contract，作者 author，机构 org
        public int DatabaseType { get; set; } //，1图书，2标准，3工具书，4期刊，5会议论文,6年鉴，7杂志，8报纸,9学位论文, 10视频，11音频，12图片,13合同,14作者,15机构
    }
}
