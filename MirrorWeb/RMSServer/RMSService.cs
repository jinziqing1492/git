using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace DRMS.RMSServer
{
    public partial class RMSService : ServiceBase
    {
        public RMSService()
        {
            InitializeComponent();
        }

        internal static MsgServer ms = null;

        protected override void OnStart(string[] args)
        {
            ms = new MsgServer();
            ms.StartListen();
        }

        protected override void OnStop()
        {
            if (ms != null)
            {
                ms.StopListen();
                ms.Close();
            }
        }
    }
}
