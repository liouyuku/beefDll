using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.notify
{
    public interface NotifyReceive
    {
        /// <summary>
        /// 通知接口
        /// </summary>
        void Notify(StringRequestInfo requestInfo);

    }
}
