<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZoomImages.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.ZoomImages" %>
<div class="zoom_images_thumb">
    <img src="<%=ThumbImgSrc %>" alt="<%=ImgDescription %>" id="zoom_images_thumb_img"
        class="coverimg" />
</div>
<a id="zoom_images_click_a" href="javascript:void(0);" class="zoom_images_clicka">点击查看大图</a>