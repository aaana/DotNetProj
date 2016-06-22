using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class MyFile
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string mimeType { get; set; }
        public string fileUrl { get; set; }

        public MyFile(int id,string fileName,string mimeType,string fileUrl)
        {
            this.id = id;
            this.fileName = fileName;
            this.mimeType = mimeType;
            this.fileUrl = fileUrl;
        }
    }
}