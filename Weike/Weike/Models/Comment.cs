using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Comment
    {
        private int _id = 0;
        public int id { get { return _id; }set { id = _id; } }
        public int user_id { get; set; }
        public int weike_id { get; set; }
        public string date { get; set; }
        public string content { get; set; }

        public Comment(int id,int user_id,int weike_id,string date,string content)
        {
            this.id = id;
            this.user_id = user_id;
            this.weike_id = weike_id;
            this.date = date;
            this.content = content;
        }
    }

    
}