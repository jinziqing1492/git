using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMS.EditorBox
{
    public static class Workor
    {
        //与服务程序通信的参数
        public static string ServerIP = string.Empty;
        public static string ServerPort = "80";
        public static string ServerUserName = string.Empty;
        public static string ServerPwd = string.Empty;

        //处理方式
        public static string WorkType = string.Empty;
        //图书的DOI
        public static string BookID = string.Empty;
        //服务器端存放的图书目录
        public static string BookPath = string.Empty;
        //当前工作用户
        public static string WorkUser = string.Empty;
        //当前操作任务ID
        public static string MissionID = string.Empty;
        //当前用户填入的修改备注
        public static string Note = string.Empty;

        //客户端XML Editor存放目录
        public static string XEditorPath
        {
            get
            {
                //string xmlEditorDir = RegeditEditor.GetRegData(RegKeyType.HKEY_LOCAL_MACHINE,
                //    "SOFTWARE/TTKN/SSAP", "Install");
                //if (string.IsNullOrEmpty(xmlEditorDir))
                //    return "";
                //return xmlEditorDir.Substring(0, xmlEditorDir.LastIndexOf('\\')) + @"\XMLEditor.exe";
                return AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\xmleditor\\XMLEditor.exe";
            }
        }
    }
}
