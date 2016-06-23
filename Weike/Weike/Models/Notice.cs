using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Notice
    {
        private int _notice_id = 0;
        public int notice_id { get { return _notice_id; } set { _notice_id = value; } }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        // !!!
        private int _weike_id = 0;
        public int weike_id { get { return _weike_id; } set { _weike_id = value; } }
        public string type { get; set; }
        public Boolean isread { get; set; }
        public DateTime noticetime { get; set; }

        public Notice(int notice_id,int sender_id,int receiver_id,int weike_id,string type,Boolean isread,DateTime noticetime)
        {
            this.notice_id = notice_id;
            this.sender_id = sender_id;
            this.receiver_id = receiver_id;
            this.weike_id = weike_id;
            this.type = type;
            this.isread = isread;
            this.noticetime = noticetime;
        }
    }
}