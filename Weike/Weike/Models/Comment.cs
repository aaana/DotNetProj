using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Comment
    {
        private int _id = 0;
        public int comment_id { get { return _id; }set { _id = value; } }
        public int user_id { get; set; }
        public int weike_id { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }

        public Comment(int id,int user_id,int weike_id,DateTime date,string content)
        {
            this.comment_id = id;
            this.user_id = user_id;
            this.weike_id = weike_id;
            this.date = date;
            this.content = content;
        }
    }

    
}