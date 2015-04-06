using BeefDLL.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.handler
{
    class FileHandler
    {
        private const int BufferSize = 1024;


        public static void Send(string path, Socket client)
        {

            FileInfo EzoneFile = new FileInfo(path);

            FileStream EzoneStream = EzoneFile.OpenRead();

            int PacketSize = 128;

            int PacketCount = (int)(EzoneStream.Length / ((long)PacketSize));


            int LastDataPacket = (int)(EzoneStream.Length - ((long)(PacketSize * PacketCount)));


            byte[] data = new byte[PacketSize];

            for (int i = 0; i < PacketCount; i++)
            {
                EzoneStream.Read(data, 0, data.Length);

                TransferFiles.SendVarData(client, data);

            }

            if (LastDataPacket != 0)
            {
                data = new byte[LastDataPacket];

                EzoneStream.Read(data, 0, data.Length);

                TransferFiles.SendVarData(client, data);

            }

            EzoneStream.Close();

        }

    }
}
