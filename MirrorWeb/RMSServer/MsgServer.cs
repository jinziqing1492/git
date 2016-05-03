using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace DRMS.RMSServer
{
    /// <summary>
    /// 服务器端监听服务类
    /// </summary>
    class MsgServer
    {
        static Queue<Message> msgQueue = new Queue<Message>();

        private bool started = true;

        public void StartListen() 
        {
            try
            {
                Thread doMsg = new Thread(DoMessage);
                doMsg.Start();
                Thread acceptMsg = new Thread(WaitMessage);
                acceptMsg.Start();
            }
            catch (Exception ex)
            {

            }
        }

        public void StopListen() 
        {
            started = false;
        }

        public void WaitMessage() 
        {
            try
            {
                IPEndPoint point = new IPEndPoint(0, 4569);
                TcpListener listener = new TcpListener(point);
                listener.Start();
                while (started)
                {
                    Socket socket = listener.AcceptSocket();
                    Message msg = new Message(socket);
                    lock (msgQueue)
                    {
                        msgQueue.Enqueue(msg);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DoMessage()
        {
            while (started)
            {
                if (msgQueue.Count > 0)
                {
                    Message msg = null;
                    lock (msgQueue)
                    {
                        msg = msgQueue.Dequeue();
                    }
                    ServerIO si = new ServerIO(msg);
                    Thread handleThread = new Thread(si.ServerHandle);
                    handleThread.Start();
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }

        public void Close() 
        {
            lock (msgQueue)
            {
                while (msgQueue.Count>0) 
                {
                    Message msg = msgQueue.Dequeue();
                    msg.Close();
                }
            }
        }
    }
}
