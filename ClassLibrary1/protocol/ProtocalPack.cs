using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeefDLL.protocol;

namespace BeefDLL.protocol
{
    public class ProtocalPack
    {
        /**
         * 数据长度 
         */
        private int length;
        /**
         * 数据标识
         * 1 ：发送成功
         * 0 ：发送失败
         */
        private byte flag;
        /**
         * 协议内容
         */
        private String content;

        /// <summary>
        /// 需上传的文件
        /// </summary>
        private FileInfo[] uploadFiles;


        public ProtocalPack()
        {
        }

        public ProtocalPack(String content)
        {
            this.content = content;
            this.flag = 1;
            this.length = content.Length + 5;
        }

        public ProtocalPack(int length,byte flag,String content)
        {
            this.content = content;
            this.flag = flag;
            this.length = length - 5;
        }

        public int Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public byte Flag
        {
            get { return this.flag; }
            set { this.flag = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public BeefDLL.protocol.FileInfo[] UploadFiles
        {
            get { return this.uploadFiles; }
            set { this.uploadFiles = value; }
        }


    }
}
