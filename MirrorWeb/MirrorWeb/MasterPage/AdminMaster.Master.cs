using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRMS.MirrorWeb.MasterPage
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get { return HttpContext.Current.User.Identity.Name; } }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetProperty();
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        private void SetProperty()
        {
            //string role = Util.GetRole();
            //this.RoleName = User.GetRoleName(role);
        }
    }
}