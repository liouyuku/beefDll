using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefDLL.protocol
{
    public class FileInfo
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        private int fileId;

        /// <summary>
        /// 文件名称
        /// </summary>
        private String fileName;

        /// <summary>
        /// 文件路径
        /// </summary>
        private String filePath;

        /// <summary>
        /// 文件网络地址
        /// </summary>
        private String fileUrl;

        /// <summary>
        /// 文件大小
        /// </summary>
        private int size;

        public int FileId
        {
            get { return this.fileId; }
            set { this.fileId = value; }
        }

        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }

        public string FileUrl
        {
            get { return this.fileUrl; }
            set { this.fileUrl = value; }
        }

        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
    }
}
