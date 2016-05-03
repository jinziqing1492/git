using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.UserControl
{
    public partial class ZoomImages : System.Web.UI.UserControl
    {

        /// <summary>
        /// 包裹缩略IMAGE的DIV的Class
        /// </summary>
        public string ImgPanelClass { get; set; }
        /// <summary>
        /// 点击浏览大图的A标签的Class
        /// </summary>
        public string ImgBigClass { get; set; }
        /// <summary>
        /// 包裹社交媒体的DIV的Class
        /// </summary>
        public string SocialClass { get; set; }

        /// <summary>
        /// 缩略图虚拟路径
        /// </summary>
        public string ThumbImgSrc { get; set; }
        /// <summary>
        /// 大图虚拟路径
        /// </summary>
        public string BigImgSrc { get; set; }
        /// <summary>
        /// 搜索按钮图片虚拟路径
        /// </summary>
        public string SearchImgSrc { get; set; }
        /// <summary>
        /// 无图显示的图片虚拟路径
        /// </summary>
        public string NoImgSrc { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public string ImgDescription { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitProperpty();
        }

        /// <summary>
        /// 属性初始化
        /// </summary>
        private void InitProperpty()
        {
            ImgPanelClass = String.IsNullOrEmpty(ImgPanelClass) ? "zoom_images_thumb" : ImgPanelClass;
            ImgBigClass = String.IsNullOrEmpty(ImgBigClass) ? "zoom_images_thumb_img" : ImgBigClass;
            SocialClass = string.IsNullOrEmpty(SocialClass) ? "zoom_images_clicka" : SocialClass;

            ThumbImgSrc = string.IsNullOrEmpty(ThumbImgSrc) ? NoImgSrc : ThumbImgSrc;
            //BigImgSrc = "../images/book17.png";
            BigImgSrc = string.IsNullOrEmpty(BigImgSrc) ? BigImgSrc : BigImgSrc;
            //if (BigImgSrc.IndexOf("http://") < 0)
            //{
            //    string server_ip = "http://" + Request.Url.Authority;
            //    BigImgSrc = server_ip + BigImgSrc;
            //}

            //SearchImgSrc = "../images/magnifier.png";
            SearchImgSrc = string.IsNullOrEmpty(SearchImgSrc) ? "../images/magnifier.png" : SearchImgSrc;
            ImgDescription = string.IsNullOrEmpty(ImgDescription) ? "图片" : ImgDescription;

        }
    }
}