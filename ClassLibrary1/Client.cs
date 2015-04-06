using BeefDLL.handler;
using BeefDLL.protocol;
using BeefDLL.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeefDLL
{
    public class Client
    {

        private Socket clientSocket;

        private Boolean connect(ProtocalPack pack, MessageReceive receive, int timeout,int port)
        {
            if (receive == null)
            {
                receive = new DefaultImpl();
            }

            try
            {
                IPAddress ip = IPAddress.Parse(System.Configuration.ConfigurationManager.AppSettings["serverIp"]);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //配置服务器IP与端口  
                clientSocket.Connect(new IPEndPoint(ip, port));
            }
            catch
            {
                receive.connectFailed(pack);
                return false;
            }
            return true;

        }

        private void close()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="pack">协议包</param>
        /// <param name="receive">返回接口</param>
        /// <param name="timeout">超时时间</param>
        public void sendMessage(ProtocalPack pack, MessageReceive receive, int timeout)
        {
            byte[] bytes = new byte[256];
            int port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["serverMsgPort"]);
            //连接服务器
            if (connect(pack, receive, timeout, port))
            {
                //发送数据
                clientSocket.Send(encoder(pack));

                //定义接收数据的缓存  
                clientSocket.Receive(bytes, SocketFlags.None);

                //返回
                ProtocalPack resultPack = decode(bytes);
                if (resultPack.Flag == 1)
                {
                    receive.onSuccess(resultPack);
                }
                else
                {
                    receive.onFailed(resultPack);
                }
                close();
            }


        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="pack">协议包</param>
        /// <param name="timeout">超时时间</param>
        public void sendMessage(ProtocalPack pack, MessageReceive receive)
        {
            sendMessage(pack, receive, 1000);
        }

        public void sendMessage(ProtocalPack pack, int timeout)
        {
            sendMessage(pack, new DefaultImpl(), timeout);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="pack">协议包</param>
        public void sendMessage(ProtocalPack pack)
        {
            sendMessage(pack, new DefaultImpl(), 1000);
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="pack"></param>
        /// <returns></returns>
        private byte[] encoder(ProtocalPack pack)
        {
            ByteBuffer buf = ByteBuffer.Allocate(256);
            byte[] cByte = Encoding.UTF8.GetBytes(pack.Content);
            buf.WriteInt(cByte.Length + 5);
            buf.WriteByte(pack.Flag);
            buf.WriteBytes(cByte);
            return buf.ToArray();
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ProtocalPack decode(byte[] data)
        {
            ByteBuffer buf = ByteBuffer.Allocate(data);
            int length = buf.ReadInt();
            byte flag = buf.ReadByte();

            byte[] contentByte = new byte[length];
            Buffer.BlockCopy(data, 5, contentByte, 0, length);

            string content = System.Text.Encoding.UTF8.GetString(contentByte);

            ProtocalPack m = new ProtocalPack(length, flag, content);
            return m;
        }

        private string decode4File(byte[] data)
        {
            byte[] contentByte = new byte[2];
            Buffer.BlockCopy(data, 0, contentByte, 0, 2);
            return System.Text.Encoding.UTF8.GetString(contentByte);
        }


        private void sendFile4Thread(ProtocalPack pack, MessageReceive receive, int timeout)
        {
            byte[] bytes = new byte[128];
            int port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["serverFilePort"]);
            //连接服务器
            if (connect(pack, receive, timeout, port))
            {
                //打包数据
                string zipPath = System.Configuration.ConfigurationManager.AppSettings["localPath"] + "/upload.zip";
                ZipUtil.ZipFiles(pack.UploadFiles, zipPath, 1, null, "zizi ge comment.");
                FileHandler.Send(zipPath, clientSocket);

                //定义接收数据的缓存  
                clientSocket.Receive(bytes, SocketFlags.None);

                //返回
                string resultPack = decode4File(bytes);
                ProtocalPack result = new ProtocalPack(resultPack);
                if (resultPack.Equals("ok"))
                {
                    receive.onSuccess(result);
                }
                else 
                {
                    receive.onFailed(result);
                }
                close();
            }
        }

        public void sendMessage4File(ProtocalPack pack, MessageReceive receive, int timeout)
        {
            sendFile4Thread(pack, receive, timeout);
        }

        public void sendMessage4File(ProtocalPack pack, MessageReceive receive)
        {
            sendMessage4File(pack, receive, 10000);
        }

        public void sendMessage4File(ProtocalPack pack)
        {
            sendMessage4File(pack, null, 10000);
        }

        public void sendMessage4File(ProtocalPack pack,int timeout)
        {
            sendMessage4File(pack, null, timeout);
        }



    }


}
