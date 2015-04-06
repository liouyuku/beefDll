using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.notify
{
    public class ReceiveService
    {
        private static ReceiveService instance;

        private static NotifyReceive notifyService;


        public static void setNotifyService(NotifyReceive n)
        {
            notifyService = n;
        }

        private ReceiveService()
        { 
        
        }

        public static ReceiveService getInstance()
        {
            if (instance == null)
            {
                instance = new ReceiveService();
            }
            return instance;
        }

        public void initServer()
        {
            AppServer appServer = new AppServer();

            if (!appServer.Setup(2012)) //Setup with listening port
            {
                return;
            }

            if (!appServer.Start())
            {
                return;
            }
            //新连接触发

            appServer.NewSessionConnected += new SessionHandler<AppSession>(SessionHandler);
            //请求触发
            appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(RequestReceived);
        }

        static void SessionHandler(AppSession session)
        {
            Console.WriteLine("hello wolrd");
        }

        static void RequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            Console.WriteLine("RequestReceived ");
            switch (requestInfo.Key.ToUpper())
            {
                case ("HEARTBEAT")://心跳包
                    break;
                case ("NOTIFY")://通知
                    if (notifyService == null)
                    { 
                        notifyService = new DefaultImpl();
                    }
                    notifyService.Notify(requestInfo);
                    break;
            }
            session.Send("receive");
            session.Close();
        }

    }
}
