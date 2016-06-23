﻿using System;
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
        public int weike_id { get; set; }

        public MyFile(int id,string fileName,string mimeType,string fileUrl,int weike_id)
        {
            this.id = id;
            this.fileName = fileName;
            this.mimeType = mimeType;
            this.fileUrl = fileUrl;
            this.weike_id = weike_id;
        }
    }
}