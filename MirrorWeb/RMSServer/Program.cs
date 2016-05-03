using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace DRMS.RMSServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //while (System.IO.File.Exists(@"d:\debug.sleep"))
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
            //MsgServer msg = new MsgServer();
            //msg.StartListen();
            //return;

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new RMSService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
