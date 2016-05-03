using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace DRMS.EditorBox
{
    #region 枚举
    /// <summary>
    /// 注册表基项枚举
    /// </summary>
    public enum RegKeyType
    {
        /// <summary>
        /// 注册表基项 HKEY_CLASSES_ROOT
        /// </summary>
        HKEY_CLASS_ROOT,
        /// <summary>
        /// 注册表基项 HKEY_CURRENT_USER
        /// </summary>
        HKEY_CURRENT_USER,
        /// <summary>
        /// 注册表基项 HKEY_LOCAL_MACHINE
        /// </summary>
        HKEY_LOCAL_MACHINE,
        /// <summary>
        /// 注册表基项 HKEY_USERS
        /// </summary>
        HKEY_USERS,
        /// <summary>
        /// 注册表基项 HKEY_CURRENT_CONFIG
        /// </summary>
        HKEY_CURRENT_CONFIG
    }
    #endregion

    public class RegeditEditor
    {
        #region 私有方法
        /// <summary>
        /// 返回RegistryKey对象
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <returns></returns>
        private static RegistryKey getRegistryKey(RegKeyType keytype)
        {
            RegistryKey rk = null;

            switch (keytype)
            {
                case RegKeyType.HKEY_CLASS_ROOT:
                    rk = Registry.ClassesRoot;
                    break;
                case RegKeyType.HKEY_CURRENT_USER:
                    rk = Registry.CurrentUser;
                    break;
                case RegKeyType.HKEY_LOCAL_MACHINE:
                    rk = Registry.LocalMachine;
                    break;
                case RegKeyType.HKEY_USERS:
                    rk = Registry.Users;
                    break;
                case RegKeyType.HKEY_CURRENT_CONFIG:
                    rk = Registry.CurrentConfig;
                    break;
            }

            return rk;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roottype"></param>
        /// <param name="regpath">使用‘/’符号分隔</param>
        /// <returns></returns>
        public static string GetRegData(RegKeyType roottype, string regpath, string keyname)
        {
            try
            {
                if (string.IsNullOrEmpty(regpath) || string.IsNullOrEmpty(keyname))
                    return "";
                regpath = regpath.TrimStart('/').TrimEnd('/');
                string[] keyNames = regpath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                if (keyNames == null || keyNames.Length <= 0)
                    return "";

                RegistryKey root = getRegistryKey(roottype);
                if (root == null)
                    return "";
                for (int i = 0; i < keyNames.Length; i++)
                {
                    root = root.OpenSubKey(keyNames[i], false);
                    if (root == null)
                        return "";
                }
                object objvalue = root.GetValue(keyname);
                if (objvalue == null)
                    return "";
                string registData = root.GetValue(keyname).ToString();
                return registData;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static string KeyName = "6.0";

        public static void CreateRootKey()
        {
            try
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
                RegistryKey arbortext = null;
                RegistryKey editor = null;
                RegistryKey aimdir = null;
                if (!IsExist(software, "Arbortext"))
                {
                    arbortext = software.CreateSubKey("Arbortext", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                else
                {
                    arbortext = software.OpenSubKey("Arbortext", true);
                }
                if (!IsExist(arbortext, "Editor"))
                {
                    editor = arbortext.CreateSubKey("Editor", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                else
                {
                    editor = arbortext.OpenSubKey("Editor", true);
                }
                if (!IsExist(editor, KeyName))
                {
                    aimdir = editor.CreateSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                else
                {
                    aimdir = editor.OpenSubKey(KeyName, true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /**/
        /// <summary>
        /// 读取注册表值
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetRegEditData(string strName)
        {
            try
            {
                string registData;
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey javasoft = software.OpenSubKey("Arbortext", true);
                RegistryKey prefs = javasoft.OpenSubKey("Editor", true);
                RegistryKey aimdir = prefs.OpenSubKey(KeyName, true);
                registData = aimdir.GetValue(strName).ToString();
                return registData;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetRegEditDataByPath(string strName)
        {
            try
            {
                string registData;
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey javasoft = software.OpenSubKey("TTKN", true);
                RegistryKey prefs = javasoft.OpenSubKey("SSAP", true);
                //RegistryKey aimdir = javasoft.OpenSubKey(KeyName, true);
                registData = prefs.GetValue(strName).ToString();
                return registData;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /**/
        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="strName"></param>
        public static void SetRegEditData(string strName, string strValue)
        {
            try
            {
                CreateRootKey();
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
                RegistryKey javasoft = software.OpenSubKey("Arbortext", true);
                RegistryKey prefs = javasoft.OpenSubKey("Editor", true);
                RegistryKey aimdir = prefs.OpenSubKey(KeyName, true);
                aimdir.SetValue(strName, strValue);
            }
            catch (Exception ex)
            {
            }
        }
        /**/
        /// <summary>
        /// 修改注册表项
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public static void ModifyRegEditData(string strName, string strValue)
        {
            try
            {
                CreateRootKey();
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey software = hklm.OpenSubKey("SOFTWARE\\" + KeyName, true);
                RegistryKey javasoft = software.OpenSubKey("Arbortext", true);
                RegistryKey prefs = javasoft.OpenSubKey("Editor", true);
                RegistryKey aimdir = prefs.OpenSubKey(KeyName, true);
                aimdir.SetValue(strName, strValue);
            }
            catch (Exception ex)
            {
            }
        }

        public static bool IsExist(RegistryKey parent, string name)
        {
            string[] subkeyNames = parent.GetSubKeyNames();
            bool exit = false;
            foreach (string key in subkeyNames)
            {
                if (key == name)
                {
                    exit = true;
                    break;
                }
            }
            return exit;
        }

    }


}
