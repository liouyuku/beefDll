using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.handler
{
    public class DefaultImpl : MessageReceive
    {

        void MessageReceive.onSuccess(protocol.ProtocalPack pack)
        {
        }

        void MessageReceive.onFailed(protocol.ProtocalPack pack)
        {
        }

        void MessageReceive.connectFailed(protocol.ProtocalPack pack)
        {
        }

        void MessageReceive.connectSuccess(protocol.ProtocalPack pack)
        {
        }
    }
}
