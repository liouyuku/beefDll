using BeefDLL;
using BeefDLL.handler;
using BeefDLL.notify;
using BeefDLL.protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQQ
{
    class Test
    {
        static void Main()
        {

            ReceiveService receiveServer = ReceiveService.getInstance();
            //设置通知服务的操作类/可选
            ReceiveService.setNotifyService(new MyNotify());
            receiveServer.initServer();
            

            //客户端
            Client c = new Client();
            //定义回调
            //默认实现为Console.WriteLine("onSuccess:" + pack.Content);需要自己根据业务实现
            MessageReceive receive = new MyReceive();
            ProtocalPack pack = new ProtocalPack("发送的协议内容,待定");

            //定义上传文件，支持多文件（任意类型）
            FileInfo[] f = new FileInfo[1];
            f[0] = new FileInfo();
            f[0].FilePath = "D:/TDDOWNLOAD/222.txt";
            f[0].FileName = "1111";
            pack.UploadFiles = f;
            //发送文件
            c.sendMessage4File(pack, receive );



            //发送文本消息
            c.sendMessage(pack);



            Console.ReadLine();
        }

    }
    /// <summary>
    /// 发送消息后服务器的返回
    /// </summary>
    class MyReceive : MessageReceive
    {
        /// <summary>
        /// 消息处理成功
        /// </summary>
        /// <param name="pack"></param>
        public void onSuccess(ProtocalPack pack)
        {
            Console.WriteLine("onSuccess:" + pack.Content);
        }
        /// <summary>
        /// 消息处理失败【具体错误代码待定】
        /// </summary>
        /// <param name="pack"></param>
        public void onFailed(ProtocalPack pack)
        {
            Console.WriteLine("onFailed");
        }
        /// <summary>
        /// 服务器连接失败
        /// </summary>
        /// <param name="pack"></param>
        public void connectFailed(ProtocalPack pack)
        {
            Console.WriteLine("connectFailed");
        }
        /// <summary>
        /// 服务器连接成功
        /// </summary>
        /// <param name="pack"></param>
        public void connectSuccess(ProtocalPack pack)
        {
            Console.WriteLine("connectSuccess");
        }
    }

    //通知接口实现
    class MyNotify : NotifyReceive
    {

        public void Notify(StringRequestInfo requestInfo)
        {
            Console.WriteLine("a di wang");
        }
    }
}
