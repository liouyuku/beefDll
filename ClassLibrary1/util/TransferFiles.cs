using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.util
{
    class TransferFiles
    {

        public TransferFiles()
        {

        }

        public static int SendVarData(Socket s, byte[] data) // return integer indicate how many data sent.
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent = 0;
            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            //sent = s.Send(datasize);//send the size of data array.

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }

            return total;
        }

    }
}
