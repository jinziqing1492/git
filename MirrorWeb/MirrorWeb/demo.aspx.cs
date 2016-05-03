using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRMS.Model;

namespace DRMS.MirrorWeb
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ipstr = "192.168.22.111";
            uint ipnum = Utility.Utility.IP2Int(ipstr);
          bool  isInIP = isInIpScopeList(ipnum);
        }

        protected bool isInIpScopeList(uint ipnum)
        {
            bool isinIP = false;
            List<IPScopeInfo> IpList = Utility.Utility.myIpList;
            if (IpList != null)
            {
                foreach (IPScopeInfo ipscopeinfo in IpList)
                {
                    if (ipscopeinfo.IpStart <= ipnum && ipnum <= ipscopeinfo.IpEnd)
                    {
                        isinIP = true;
                        break;
                    }
                }
            }
            return isinIP;
        }
    }
}