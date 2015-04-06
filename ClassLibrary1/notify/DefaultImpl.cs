using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.notify
{
    public class DefaultImpl : NotifyReceive
    {
        public void Notify(StringRequestInfo requestInfo)
        {
            Console.WriteLine("requestInfo : " + requestInfo.Key);   
        }
    }
}
