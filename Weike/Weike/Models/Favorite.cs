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
        public string date { get; set; }

        public Favorite(int user_id,int weike_id, string date)
        {
            this.user_id = user_id;
            this.weike_id = weike_id;
            this.date = date;
        }

    }
}