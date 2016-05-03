<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedDBControl.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.AdvancedDBControl" %>
<link href="../css/index.css" rel="stylesheet" />
<script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="../js/Util.js"></script>
<script src="../js/artDialog/jquery.artDialog.js?skin=chrome" type="text/javascript"></script>
<script src="../js/artDialog/plugins/iframeTools.js" type="text/javascript"></script>
<script type="text/javascript">
    //最后一行的index
    var ROW_CURRENT_INDEX = 1;
    //总行数
    var ROW_TOTAL_COUNT = <%=Counts%>;
    
    
    //var ZtreeSql=QueryString("queryConn");
    //var contentUrlFormat = "DatabaseContent.aspx?type={0}&queryConn={1}";
    $(function(){
        //初始化加载
        ShowTime();
        //alert(ZtreeSql);
        $("#AddRow_A").click(function () {
            if (ROW_CURRENT_INDEX+1 > ROW_TOTAL_COUNT) {
                return false;
            }else {
                ROW_CURRENT_INDEX++;
                var id = "txt_"+ROW_CURRENT_INDEX;
                $("#"+id).show();

                var height = $(".adv-search-main").height();
                height+=25;
                $(".adv-search-main").height(height);
                return true;
            }
                
        });

        $("#MinusRow_A").click(function () {
            if (ROW_CURRENT_INDEX <= 1) {
                return false;
            }else {
                var id = "txt_"+ROW_CURRENT_INDEX;
                $("#"+id).hide();
                ROW_CURRENT_INDEX--;

                var height = $(".adv-search-main").height();
                height-=25;
                $(".adv-search-main").height(height);

                return true;
            }
        });
            
    })

    //根据资源类型显示加载时间检索
    function ShowTime(){
        switch(TYPE){
            default:break;
            case"4":
                $("#tdName").text("建刊时间：");
                break;
            case"7":
                $("#tdName").text("建刊时间：");
                break;
            case"8":
                $("#tdName").text("建刊时间：");
                break;
            case"9":
                $("#tdName").text("提交时间：");
                break;
            case"10":
                $("#tdName").text("发布时间：");
                break;
            case"11":
                $("#tdName").text("发布时间：");
                break;
            case"12":
                $("#tbShow").hide();
                $("#pTime").hide();
                break;

        }
    }

    //根据时间添加sql条件
    function getSqlTime()
    {
        var sql="";
        switch(TYPE){
            default:break;
            case"1":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"issuedate");
                break;
            case"2":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"Dateissued");
                    
                break;
            case"3":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"issuedate");
                    
                break;
            case"4":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"FoundDate");
                    
                break;
            case"5":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"IssueDate");
                    
                break;
            case"6":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"issuedate");
                    
                break;
            case"7":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"FoundDate");
                    
                break;
            case"8":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"FoundDate");
                    
                break;
            case"9":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"PaperSubmissionDate");
                    
                break;
            case"10":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"issuedate");
                    
                break;
            case"11":
                sql+=FormateTime($("#beginTime").val(),$("#endTime").val(),"issuedate");
                    
                break
        }
        //sql=(sql=="")?"":((getSqlConn()=="")?sql:(" AND "+sql));
        return sql;
    }

    function FormateTime(beginTime,endTime,data)
    {
        var str="";
        if ((beginTime=="")&&(endTime=="")) {
            str="";
        }
       
        if ((beginTime!="")&&(endTime!="")) {
            if (beginTime>endTime) {
                $("#beginTime").val(endTime);
                $("#endTime").val(beginTime);
                str="("+data+">='"+endTime+"' AND "+data+"<='"+beginTime+"')";
            }else{
                str="("+data+">='"+beginTime+"' AND "+data+"<='"+endTime+"')";
            }
        }
        if ((beginTime!="")&&(endTime=="")) {
            str+="("+data+">='"+beginTime+"')";
        }
        if ((endTime!="")&&(beginTime=="")) {
            str+="("+data+"<='"+endTime+"')";
        }
        return str;
    }

    function getSqlConn(){
        //获得当前显示的总行数
        var _row_curr_total_count = ROW_CURRENT_INDEX;
        var sqlConn = [];
        for (var i = 1; i <= _row_curr_total_count; i++) {
            var sel = $("#txt_"+i+"_sel").val();
            var txt1 = $.trim($("#txt_"+i+"_value1").val());
            var txt2 = $.trim($("#txt_"+i+"_value2").val());
            var ral = $("#txt_"+i+"_relation").val();
            var spe = $("#txt_"+i+"_special").val();
            var log = $("#txt_"+i+"_logical").val();
            var conn = "(";
            if (i!==1&&(txt1!==""||txt2!=="")) {
                sqlConn.push(log);
            }
            if (spe==="=") {//精确
                if (txt1===""&&txt2==="") {
                    continue;
                }
                if (txt1!=="") {
                    conn+="("+sel+"='"+txt1+"'";
                    if (txt2!=="") {
                        conn+=" "+ral+" '"+txt2+"')";
                    }else {
                        conn+=")";
                    }
                }else {
                    conn+="("+sel+"=''";
                    if (txt2!=="") {
                        conn+=" "+ral+" '"+txt2+"')";
                    }else {
                        conn+=")";
                    }
                }          
                    
            }else {//模糊
                if (txt1===""&&txt2==="") {
                    continue;
                }
                if (txt1!=="") {
                    conn+="("+sel+" % '"+txt1+"'";
                    if (txt2!=="") {
                        conn+=" "+ral+" '"+txt2+"')";
                    }else {
                        conn+=")";
                    }
                }else {
                    conn+="("+sel+" % ''";
                    if (txt2!=="") {
                        conn+=" "+ral+" '"+txt2+"')";
                    }else {
                        conn+=")";
                    }
                }
            }
            conn+=")";
            sqlConn.push(conn);
        }
        //alert(sqlConn.join(" "));
        return sqlConn.join(" ");
    };
  

    function GetClassficationSql(){
        var ids=$("#hdn_themeIds").val();
       
        var sql="";
        if (ids!="-1") {
            var arr=ids.split(';').clean("");
          
            if (arr!=null&&arr.length>0) {
                sql+="("
                for (var i = 0; i < arr.length; i++) {
                    sql+= " (SYS_FLD_CLASSFICATION='"+arr[i]+"?')"+" or";
                }
                sql= sql.substring(0,sql.length-2);
                sql+=")";
            }
        }
        return sql;
    }

    function ChooseClassfication(){
        var names=$("#hdn_themeNames").val();
        var ids=$("#hdn_themeIds").val();
        
        var url="../auditadmin/DBChooseTheme.aspx?ids="+ids+"&names="+names;
        art.dialog.open(url,{title:"编辑分类信息",width:500,height:500});
    };
</script>

<div class="adv-search-main">
    <font color="#E60000">输入内容检索条件：</font>
    <asp:Literal ID="ltShow" runat="server"></asp:Literal>
    <p></p>
    <table id="tbShow" cellspacing="7">
        <tr>
            <td id="tdName">出版时间：</td>
            <td>从<input type="text" id="beginTime" onclick="WdatePicker();" />到<input type="text" id="endTime" onclick="    WdatePicker();" /></td>
        </tr>
        <tr id="trClassfication">
            <td>分类号：
            </td>
            <td>
                <input type="text" id="CLASSFICATION" readonly="readonly" />&nbsp;&nbsp;<img src="../images/TYsubnav_Subrdinate1.png" style="cursor: pointer;" onclick="ChooseClassfication();" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hdn_themeNames" value="-1" />
    <input type="hidden" id="hdn_themeIds" value="-1" />
</div>