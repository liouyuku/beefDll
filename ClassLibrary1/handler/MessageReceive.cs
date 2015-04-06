using BeefDLL.protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.handler
{
    public interface MessageReceive
    {
        void onSuccess(ProtocalPack pack);

        void onFailed(ProtocalPack pack);

        void connectFailed(ProtocalPack pack);

        void connectSuccess(ProtocalPack pack);
    }
}
