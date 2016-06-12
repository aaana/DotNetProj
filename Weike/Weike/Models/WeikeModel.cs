using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class WeikeModel
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string author { get; set; }
        public string src { get; set; }
        public string size { get; set; }
        public string description { get; set; }
        public int star { get; set; }
    }
}