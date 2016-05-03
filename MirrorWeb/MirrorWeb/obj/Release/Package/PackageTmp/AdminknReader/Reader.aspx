<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reader.aspx.cs" Inherits="AdminKNReader.Reader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="css/read.css" />
    <script language="javascript" type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/Reader.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            Expand();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="read_main">
        <div class="read_layer">
            <div id="read_top">
                <h5>
                    <img src="images/reader/tryread_tit.gif" /></h5>
                <div class="read_midopt">
                    <span class="page">第<label id="labPage">1</label>页</span>
                    
                </div>
     <%--           <div class="rig_opt">
                    <a id="aExpand" href="javascript:void(0);" title="全屏视图" onclick="Expand();">全屏视图</a><a
                        id="aRestore" class="restore_view" style="display: none;" href="javascript:void(0);"
                        title="标准视图" onclick="Restore();">标准视图</a><a href="javascript:void(0);" title="关闭"
                            class="close" onclick="Close();">关闭</a></div>--%>
            </div>
            <div id="read_content">
                <div class="read_contentmid">
                    <!-- 中间五列开始-->
                    <div id="read_lef" class="read_lef">
                        <!-- 图书信息部分-->
                        <span class="close_bookinfo"><a id="aInfoClose" href="javascript:void(0);" title="合并"
                            onclick="InfoClose();">合并</a> <a href="javascript:void(0);" title="展开" id="aInfoOpen"
                                class="open" style="display: none;" onclick="InfoOpen();">展开</a></span>
                        <input id="hidBuyStyle" type="hidden" value="0" />
                        <div id="divInfo" class="readbook_info">               
                        </div>
                        <!-- 图书目录-->
                        <h4 id="h4merge" onclick="CatalogClick();">
                            <em>目录</em></h4>
                        <div id="divcon_text" class="con_text">
                        </div>
                    </div>
                    <div id="read_closelef" class="read_closelef">
                        <a id="aread_closelef" href="javascript:void(0);" onclick="SideClose('read_lef',178,0);"
                            title="合并左侧"></a>
                    </div>
                    <div id="read_mid" class="read_mid">
                    <div class="midcenter">
                        <div class="zoom_btn">
                            <a id="azoom_small" class="zoom_small" title="缩小" href="javascript:void(0);" onclick="ZoomSmall();">
                                缩小</a><a id="azoom_big" class="zoom_big" title="放大" href="javascript:void(0);" onclick="ZoomBig();">放大</a>
                        </div>
                        <div class="searchbox">
   <input name="txtSearch" id="txtSearch" onkeydown="return EnterClick();" type="text" class="searinpt" />
     <input name="" type="button" onclick="SearchClick();" class="searbtn"/>
   </div>
   </div>
                        <div id="read_mid_srollbar" class="read_mid_srollbar">
                            <div id="read_midbox" class="read_midbox">
                                <div id="read_over" class="read_over" style="cursor: url(images/reader/hand1.cur),auto;">
                                </div>
                                <!-- 书内页 -->
                                <div id="divImg1" class="readerpage">
                                    <span><img id="Img1" src="request/TebReadHandler.ashx?b=<%=mBookID %>&p=<%=mPressID %>&page=1&cc=<%=mPath %>" /></span></div>
                                <div id="divImg2" class="readerpage">
                                    <span><img id="Img2" src="request/TebReadHandler.ashx?b=<%=mBookID %>&p=<%=mPressID %>&page=2&cc=<%=mPath %>" /></span></div>
                                <div id="divImg3" class="readerpage">
                                    <span><img id="Img3" src="request/TebReadHandler.ashx?b=<%=mBookID %>&p=<%=mPressID %>&page=3&cc=<%=mPath %>" /></span></div>
                                <!-- 书内页 -->
                            </div>
                        </div>
                        <span class="pre_page"><a id="aPrePage" href="javascript:void(0);" title="前一页" onclick="PrePage();">
                            前一页</a></span> <span class="next_page"><a id="aNextPage" href="javascript:void(0);"
                                title="后一页" onclick="NextPage();">后一页</a></span>
                    </div>
                    <div id="read_closerig" class="read_closerig">
                     <%--   <a id="aread_closerig" href="javascript:void(0);" onclick="SideClose('read_rig',198,1);"
                            title="合并右侧"></a>--%>
                    </div>
                    <div class="clear">
                    </div>
                    <div id="read_rig" class="read_rig">
                        <!-- hang -->
                        <h4 id="h4History" class="rig_toptit" style="display: none;">
                            <em>您最近浏览的图书</em></h4>
                        <h4 id="h4Comment" class="rig_toptit">
                            <em>读者书评</em><span><a href="../reply/BookCommentList.aspx?pressid=<%=mPressID %>&bookid=<%=mBookID %>"
                            target="_blank">more</a></span>
                        </h4>
                        <div class="rigbottit">
                            <ul>
                                <li class="showtext" onclick="ShowComment(this);">读者书评</li>
                                <li onclick="ShowHistory(this);">浏览历史</li>
                            </ul>
                        </div>
                        <div id="divBrowse_book" class="browse_book" style="display: none;">                       
                        </div>
                        <!-- hang -->
                        <div id="divComment_book" class="comment_book">                           
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <!-- 中间五列结束-->
                </div>
            </div>
            <span class="top_TLy"></span><span class="top_TRy"></span><span class="bot_BLy">
            </span><span class="bot_BRy"></span>
        </div>
    </div>
    <input type="hidden" id="hidStart" value="0" />
    <input type="hidden" id="hidEnd" value="0" />
    <input type="hidden" id="hidPressID" value="<%=mPressID %>" />
    <input type="hidden" id="hidBookID" value="<%=mBookID %>" />
    </form>
</body>
</html>
