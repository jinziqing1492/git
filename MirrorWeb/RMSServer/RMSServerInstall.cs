using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace DRMS.RMSServer
{
    [RunInstaller(true)]
    public partial class RMSServerInstall : System.Configuration.Install.Installer
    {
        public RMSServerInstall()
        {
            InitializeComponent();
        }
    }
}
