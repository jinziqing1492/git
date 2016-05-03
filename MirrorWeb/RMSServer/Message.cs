using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace DRMS.RMSServer
{
    /// <summary>
    ///  基于Socket通信的消息类
    /// </summary>
    class Message
    {
        private int _PER_FILE_LENGTH = 1024;
        private int _PER_BLOCK_LENGTH = 1024;

        private Socket _client;

        public Socket Client
        {
            get { return _client; }
        }

        public Message(Socket s)
        {
            _client = s;
        }

        private long GetLength()
        {
            long length = 0;
            try
            {
                byte[] len = new byte[1024];
                _client.Receive(len);
                string strLen = Encoding.UTF8.GetString(len);
                length = Convert.ToInt64(strLen);
            }
            catch { }
            return length;
        }

        public string RecieveMessge()
        {
            string msg = string.Empty;
            try
            {
                long msgLength = GetLength();
                byte[] msgArray = new byte[msgLength];
                if (msgLength <= _PER_BLOCK_LENGTH)
                {
                    _client.Receive(msgArray);
                }
                else
                {
                    int recieved = 0;
                    while (recieved + _PER_BLOCK_LENGTH < msgLength)
                    {
                        recieved += _client.Receive(msgArray, recieved, _PER_BLOCK_LENGTH, SocketFlags.None);
                        System.Threading.Thread.Sleep(100);
                    }
                    if (msgLength - recieved > 0)
                    {
                        long left = msgLength - recieved;
                        recieved += _client.Receive(msgArray, recieved, (int)left, SocketFlags.None);
                    }
                }
                msg = Encoding.UTF8.GetString(msgArray);
                if (!string.IsNullOrEmpty(msg))
                {
                    msg = DesSecurity.DesDecrypt(msg);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            SendByte(1);
            return msg;
        }

        public bool RecieveFile(string path)
        {
            bool flag = false;
            try
            {
                long fileLength = GetLength();
                int perLength = _PER_FILE_LENGTH;
                byte[] perArray = new byte[perLength];
                long recieved = 0;
                int perRecieved = 0;
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    while (recieved + perLength < fileLength)
                    {
                        perRecieved = _client.Receive(perArray);
                        fs.Write(perArray, 0, perRecieved);
                        fs.Flush();
                        recieved += perRecieved;
                    }
                    if (recieved < fileLength)
                    {
                        long left = fileLength - recieved;
                        byte[] leftArray = new byte[left];
                        perRecieved = _client.Receive(leftArray);
                        fs.Write(leftArray, 0, perRecieved);
                        fs.Flush();
                        recieved += perRecieved;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            return flag;
        }

        public bool SendMessage(string msg)
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    msg = DesSecurity.DesEncrypt(msg);
                }
                byte[] msgArray = Encoding.UTF8.GetBytes(msg);
                long msgLength = msgArray.LongLength;
                byte[] msgLen = GetLengthBytes(msgLength);
                _client.Send(msgLen);
                _client.Send(msgArray);
                flag = true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            RecieveByte();
            return flag;
        }

        private byte[] GetLengthBytes(long length)
        {
            string strLength = length.ToString();
            byte[] msgLen = new byte[1024];
            byte[] len = Encoding.UTF8.GetBytes(strLength);
            len.CopyTo(msgLen, 0);
            return msgLen;
        }

        public bool SendFile(string path)
        {
            bool flag = false;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = fs.Length;
                    int perLength = _PER_FILE_LENGTH;
                    byte[] len = GetLengthBytes(fileLength);
                    _client.Send(len);
                    byte[] perArray = new byte[perLength];
                    int bytes = 0;
                    while ((bytes = fs.Read(perArray, 0, perArray.Length)) > 0)
                    {
                        _client.Send(perArray, bytes, 0);
                    }
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            return flag;
        }

        public void SendByte(byte bit)
        {
            byte[] buffer = { bit };
            _client.Send(buffer);
        }

        public bool RecieveByte()
        {
            int attemp = 0;
            byte[] buffer = new byte[1];
            while (attemp++ <= 3)
            {
                int bytes = _client.Receive(buffer, buffer.Length, SocketFlags.None);
                if (bytes == buffer.Length)
                {
                    break;
                }
                System.Threading.Thread.Sleep(1000);
            }
            if (buffer[0] == 1)
            {
                return true;
            }
            return false;
        }

        public void WriteLog(string msg)
        {
            string path = Environment.CurrentDirectory.TrimEnd('\\') + "\\log_server.txt";
            File.AppendAllText(path, msg + "\r\n");
        }

        public void Close()
        {
            if (_client != null)
            {
                _client.Disconnect(false);
                _client.Close();
            }
        }
    }
}
