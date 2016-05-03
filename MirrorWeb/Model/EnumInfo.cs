using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace DRMS.Model
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        [EnumDescription("普通用户")]
        USER = 0,
        /// <summary>
        /// 资源收集人员
        /// </summary>
        [EnumDescription("资源收集人员")]
        COLLECTUSER = 1,
        /// <summary>
        /// 收集审核人员
        /// </summary>
        [EnumDescription("收集审核人员")]
        AUDITCOLLECTUSER = 2,
        /// <summary>
        /// 加工人员
        /// </summary>
        [EnumDescription("加工人员")]
        PROCESSUSER = 3,
        /// <summary>
        /// 加工审核人员
        /// </summary>
        [EnumDescription("加工审核人员")]
        AUDITPROCESSUSER = 4,
        /// <summary>
        /// 资源管理员
        /// </summary>
        [EnumDescription("资源管理员")]
        RESADMIN = 5,
        /// <summary>
        /// 系统管理员
        /// </summary>
        [EnumDescription("系统管理员")]
        ADMIN = 6,
        /// <summary>
        /// 任务分配管理员
        /// </summary>
        [EnumDescription("任务分配管理员")]
        ASSIGNTASKUSER = 7

    }

    /// <summary>
    /// 数据资源类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// 图书
        /// </summary>
        [EnumDescription("图书")]
        BOOKTDATA = 1,
        /// <summary>
        /// 标准
        /// </summary>
        [EnumDescription("标准")]
        CRITERION = 2,
        /// <summary>
        ///工具书
        /// </summary>
        [EnumDescription("工具书")]
        REFERENCEBOOK = 3,
        /// <summary>
        /// 期刊
        /// </summary>
        [EnumDescription("期刊")]
        JOURNAL = 4,
        /// <summary>
        /// 会议论文
        /// </summary>
        [EnumDescription("会议论文")]
        CONFERENCEPAPER = 5,
        /// <summary>
        ///  年鉴
        /// </summary>
        [EnumDescription("年鉴")]
        YEARBOOK = 6,
        /// <summary>
        ///  杂志
        /// </summary>
        [EnumDescription("杂志")]
        MAGAZINE = 7,
        /// <summary>
        ///  报纸
        /// </summary>
        [EnumDescription("报纸")]
        NEWSPAPER = 8,
        /// <summary>
        /// 学位论文
        /// </summary>
        [EnumDescription("学位论文")]
        THESIS = 9,
        /// <summary>
        ///  视频
        /// </summary>
        [EnumDescription("视频")]
        VIDEODATA = 10,
        /// <summary>
        /// 音频
        /// </summary>
        [EnumDescription("音频")]
        AUDIODATA = 11,
        /// <summary>
        /// 图片
        /// </summary>
        [EnumDescription("图片")]
        PICDATA = 12,
        /// <summary>
        /// 合同
        /// </summary>
        [EnumDescription("合同")]
        CONTRACT = 13,
        /// <summary>
        /// 作者
        /// </summary>
        [EnumDescription("作者")]
        AUTHOR = 14,
        /// <summary>
        /// 机构
        /// </summary>
        [EnumDescription("机构")]
        ORG = 15,
        /// <summary>
        /// 图书章节
        /// </summary>
        [EnumDescription("图书章节")]
        BOOKCHAPTER = 16,
        /// <summary>
        /// 标准章节
        /// </summary>
        [EnumDescription("标准章节")]
        STDDATACHAPTER = 17,
        /// <summary>
        /// 期刊文章
        /// </summary>
        [EnumDescription("期刊文章")]
        JOURNALARTICLE = 18,
        /// <summary>
        /// 会议论文文章
        /// </summary>
        [EnumDescription("会议论文文章")]
        CONFERENCEARTICLE = 19,
        /// <summary>
        /// 术语
        /// </summary>
        [EnumDescription("术语")]
        TERMINOLOGY = 20,
        /// <summary>
        /// 缩略词
        /// </summary>
        [EnumDescription("缩略词")]
        ABBREVIATIONS = 21,
        /// <summary>
        /// 工具书词条
        /// </summary>
        [EnumDescription("工具书词条")]
        ENTRYDATA = 22,
        /// <summary>
        /// 杂志文章
        /// </summary>
        [EnumDescription("杂志文章")]
        MAGAZINEARTICLE = 23,
        /// <summary>
        /// 报纸文章
        /// </summary>
        [EnumDescription("报纸文章")]
        NEWSPAPERARTICLE = 24,
        /// <summary>
        /// 逻辑数据库
        /// </summary>
        [EnumDescription("逻辑数据库")]
        LOGICALDATABASE = 25,
        /// <summary>
        /// 用户
        /// </summary>
        [EnumDescription("用户")]
        USERDATA = 26,
        /// <summary>
        /// 附件库
        /// </summary>
        [EnumDescription("附件库")]
        ATTACHMENT = 27,
        /// <summary>
        /// 下载申请
        /// </summary>
        [EnumDescription("下载申请")]
        DOWNLOADAPPLAY = 27,
        /// <summary>
        /// 原始资料库
        /// </summary>
        [EnumDescription("原始资料库")]
        ORIGINALDATA = 28,
        /// <summary>
        /// 期刊年信息
        /// </summary>
        [EnumDescription("期刊年信息")]
        JOURNALYEAR = 29,
        /// <summary>
        /// 杂志年信息
        /// </summary>
        [EnumDescription("杂志年信息")]
        MAGAZINEYEAR = 30,
        /// <summary>
        /// 报纸年信息
        /// </summary>
        [EnumDescription("报纸年信息")]
        NEWSPAPERYEAR = 31,
        /// <summary>
        /// 年鉴文章信息
        /// </summary>
        [EnumDescription("年鉴文章信息")]
        YEARBOOKARTICLE = 32,
        /// <summary>
        /// 用户包库记录
        /// </summary>
        [EnumDescription("用户包库记录")]
        USERLDBDATA = 33,
        /// <summary>
        /// U盘电子书
        /// </summary>
        [EnumDescription("U盘电子书")]
        UEBOOK = 34,
        /// <summary>
        /// 英文资源
        /// </summary>
        [EnumDescription("英文资源")]
        ENGLISHRES = 60,
        /// <summary>
        /// 航空学会
        /// </summary>
        [EnumDescription("航空学会")]
        STUDYRES = 61,
        /// <summary>
        /// 自有资源
        /// </summary>
        [EnumDescription("自有资源")]
        OWNERRES=64,
        /// <summary>
        /// 英文资源文章
        /// </summary>
        [EnumDescription("英文资源文章")]
        ENGLISHARTICLE = 70,
        /// <summary>
        /// 贵飞资源
        /// </summary>
        [EnumDescription("贵飞资源")]
        RESOURCETYPE=80
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [EnumDescription("添加")]
        ADD = 0,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumDescription("删除")]
        DELETE = 1,
        /// <summary>
        /// 更新
        /// </summary>
        [EnumDescription("更新")]
        UPDATE = 2,
        /// <summary>
        /// 浏览
        /// </summary>
        [EnumDescription("浏览")]
        BROWSE = 3,
        /// <summary>
        /// 查询
        /// </summary>
        [EnumDescription("查询")]
        SEARCH = 4,
        /// <summary>
        ///用户登录
        /// </summary>
        [EnumDescription("用户登录")]
        LOGNON = 5,
        /// <summary>
        /// 用户退出
        /// </summary>
        [EnumDescription("用户退出")]
        LOGOUT = 6,
        /// <summary>
        /// 批量删除
        /// </summary>
        [EnumDescription("批量删除")]
        BATCHDELETE = 7,
        /// <summary>
        /// 任务分配
        /// </summary>
        [EnumDescription("任务分配")]
        ASSIGNTASK = 8,
        /// <summary>
        /// 收集审核
        /// </summary>
        [EnumDescription("收集审核")]
        COLLECTAUDIT = 9,
        /// <summary>
        /// 加工审核
        /// </summary>
        [EnumDescription("加工审核")]
        PROCESSAUDIT = 10,
        /// <summary>
        /// 编辑审核
        /// </summary>
        [EnumDescription("编辑审核")]
        EDITAUDIT = 11,
        /// <summary>
        /// 编辑审核
        /// </summary>
        [EnumDescription("发布")]
        PUBLISH = 12,
        /// <summary>
        /// 下载
        /// </summary>
        [EnumDescription("下载")]
        DOWNLOAD = 13,
        /// <summary>
        /// 批量上架
        /// </summary>
        [EnumDescription("批量上架")]
        BATCHUP = 14,
        /// <summary>
        /// 批量下架
        /// </summary>
        [EnumDescription("批量下架")]
        BATCHDOWN = 15,
        /// <summary>
        /// 上架
        /// </summary>
        [EnumDescription("上架")]
        RESOURCEUP = 16,
        /// <summary>
        /// 下架
        /// </summary>
        [EnumDescription("下架")]
        RESOURCEDOWN = 17
    }
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum MissionStatusType
    {
        /// <summary>
        /// 收集中
        /// </summary>
        [EnumDescription("收集中")]
        COLLECTING = 0,
        /// <summary>
        /// 收集待审核
        /// </summary>
        [EnumDescription("收集待审核")]
        PENDINGCOLLECTION = 1,
        /// <summary>
        /// 待加工
        /// </summary>
        [EnumDescription("待加工")]
        TOBEPROCESSED = 2,
        /// <summary>
        /// 加工待审核
        /// </summary>
        [EnumDescription("加工待审核")]
        PENDINGPROCESSING = 3,
        /// <summary>
        /// 正式入库
        /// </summary>
        [EnumDescription("正式入库")]
        FORMALSTORAGE = -1,
        /// <summary>
        /// 待编辑
        /// </summary>
        [EnumDescription("待编辑")]
        TOBEEDITED = 4,
        /// <summary>
        /// 编辑待审核
        /// </summary>
        [EnumDescription("编辑待审核")]
        PENDINGPEDITING = 5
    }

    public enum StdStageType
    {
        /// <summary>
        /// 发布稿
        /// </summary>
        [EnumDescription("发布稿")]
        RELEASE = 0,
        /// <summary>
        /// 大纲
        /// </summary>
        [EnumDescription("大纲")]
        OUTLINE = 1,
        /// <summary>
        /// 初稿
        /// </summary>
        [EnumDescription("初稿")]
        ABBAZZO = 2,
        /// <summary>
        /// 征求意见稿
        /// </summary>
        [EnumDescription("征求意见稿")]
        DRAFT = 3,
        /// <summary>
        /// 送审稿
        /// </summary>
        [EnumDescription("送审稿")]
        SEND = 4,
        /// <summary>
        /// 报批稿
        /// </summary>
        [EnumDescription("报批稿")]
        APPROVAL = 5
    }

    /// <summary>
    /// 处理枚举描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDescription : Attribute
    {
        private string enumDisplayText;
        private int enumRank;
        private FieldInfo fieldIno;

        /// <summary>
        /// 描述枚举值
        /// </summary>
        /// <param name="enumDisplayText">描述内容</param>
        /// <param name="enumRank">排列顺序</param>
        public EnumDescription(string enumDisplayText, int enumRank)
        {
            this.enumDisplayText = enumDisplayText;
            this.enumRank = enumRank;
        }

        /// <summary>
        /// 描述枚举值，默认排序为5
        /// </summary>
        /// <param name="enumDisplayText">描述内容</param>
        public EnumDescription(string enumDisplayText)
            : this(enumDisplayText, 5) { }

        public string EnumDisplayText
        {
            get { return this.enumDisplayText; }
        }

        public int EnumRank
        {
            get { return enumRank; }
        }

        public int EnumValue
        {
            get { return (int)fieldIno.GetValue(null); }
        }

        public string FieldName
        {
            get { return fieldIno.Name; }
        }

        #region  =========================================对枚举描述属性的解释相关函数

        /// <summary>
        /// 排序类型
        /// </summary>
        public enum SortType
        {
            /// <summary>
            ///按枚举顺序默认排序
            /// </summary>
            Default,
            /// <summary>
            /// 按描述值排序
            /// </summary>
            DisplayText,
            /// <summary>
            /// 按排序熵
            /// </summary>
            Rank
        }

        private static System.Collections.Hashtable cachedEnum = new Hashtable();


        /// <summary>
        /// 得到对枚举的描述文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumText(Type enumType)
        {
            EnumDescription[] eds = (EnumDescription[])enumType.GetCustomAttributes(typeof(EnumDescription), false);
            if (eds.Length != 1) return string.Empty;
            return eds[0].EnumDisplayText;
        }

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetFieldText(object enumValue)
        {
            if (enumValue == null)
            {
                return string.Empty;
            }
            EnumDescription[] descriptions = GetFieldTexts(enumValue.GetType(), SortType.Default);
            foreach (EnumDescription ed in descriptions)
            {
                if (ed.fieldIno.Name == enumValue.ToString()) return ed.EnumDisplayText;
            }
            return string.Empty;
        }


        /// <summary>
        /// 得到枚举类型定义的所有文本，按定义的顺序返回
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        /// <returns>所有定义的文本</returns>
        public static EnumDescription[] GetFieldTexts(Type enumType)
        {
            return GetFieldTexts(enumType, SortType.Default);
        }

        /// <summary>
        /// 得到枚举类型定义的所有文本
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        /// <param name="sortType">指定排序类型</param>
        /// <returns>所有定义的文本</returns>
        public static EnumDescription[] GetFieldTexts(Type enumType, SortType sortType)
        {
            EnumDescription[] descriptions = null;
            //缓存中没有找到，通过反射获得字段的描述信息
            if (cachedEnum.Contains(enumType.FullName) == false)
            {
                FieldInfo[] fields = enumType.GetFields();
                ArrayList edAL = new ArrayList();
                foreach (FieldInfo fi in fields)
                {
                    object[] eds = fi.GetCustomAttributes(typeof(EnumDescription), false);
                    if (eds.Length != 1) continue;
                    ((EnumDescription)eds[0]).fieldIno = fi;
                    edAL.Add(eds[0]);
                }

                cachedEnum.Add(enumType.FullName, (EnumDescription[])edAL.ToArray(typeof(EnumDescription)));
            }
            descriptions = (EnumDescription[])cachedEnum[enumType.FullName];
            if (descriptions.Length <= 0) throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");

            //按指定的属性冒泡排序
            for (int m = 0; m < descriptions.Length; m++)
            {
                //默认就不排序了
                if (sortType == SortType.Default) break;

                for (int n = m; n < descriptions.Length; n++)
                {
                    EnumDescription temp;
                    bool swap = false;

                    switch (sortType)
                    {
                        case SortType.Default:
                            break;
                        case SortType.DisplayText:
                            if (string.Compare(descriptions[m].EnumDisplayText, descriptions[n].EnumDisplayText) > 0) swap = true;
                            break;
                        case SortType.Rank:
                            if (descriptions[m].EnumRank > descriptions[n].EnumRank) swap = true;
                            break;
                    }

                    if (swap)
                    {
                        temp = descriptions[m];
                        descriptions[m] = descriptions[n];
                        descriptions[n] = temp;
                    }
                }
            }

            return descriptions;
        }

        #endregion
    }
}
