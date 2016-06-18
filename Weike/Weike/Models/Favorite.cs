using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Favorite
    {
        public int user_id { get; set; }
        public int weike_id { get; set; }
        public DateTime date { get; set; }
        //private Boolean _isread = false;
        //public Boolean isread { get { return _isread; } set { _isread = value; } }

        public Favorite(int user_id,int weike_id, DateTime date)
        {
            this.user_id = user_id;
            this.weike_id = weike_id;
            this.date = date;
        }

    }
}