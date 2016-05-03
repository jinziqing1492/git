using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.AdminUserControl
{
    public partial class NotificationBoxView : System.Web.UI.UserControl
    {
        #region 属性

        /// <summary>
        /// 是否显示
        /// </summary>
        public new bool Visible
        {
            get { return this.boxContainer.Visible; }
            set { this.boxContainer.Visible = value; }
        }

        /// <summary>
        /// 通知信息类型 的枚举值
        /// </summary>
        public NotificationType MessageType { get; set; }

        /// <summary>
        /// 通知内容(支持html元素)
        /// </summary>
        public string Content { get; set; }

        #endregion

        /// <summary>
        /// 获取通知类型对应的css className
        /// </summary>
        /// <returns>className</returns>
        protected string GetClassName()
        {
            return Enum.GetName(typeof(NotificationType), this.MessageType).ToLower();
        }

        /// <summary>
        /// 获取通知内容
        /// </summary>
        /// <returns>通知内容</returns>
        protected string GetContent()
        {
            return string.IsNullOrWhiteSpace(this.Content) ? "您的通知信息将会在这里显示" : this.Content;
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="type">类型，AdminUserControl.NotificationType的枚举</param>
        /// <param name="content">内容（支持html）</param>
        public void ShowMessage(NotificationType type, string content)
        {
            this.MessageType = type;
            this.Content = content;
            this.Visible = true;
        }
    }

    /// <summary>
    /// 通知信息类型
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// 提示
        /// </summary>
        Information,
        /// <summary>
        /// 警告
        /// </summary>
        Attention,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        /// <summary>
        /// 成功
        /// </summary>
        Success
    }
}