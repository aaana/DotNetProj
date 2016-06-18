using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class Follow
    {
        public int user_id { get; set; }
        public int following_id { get; set; }
        public DateTime followDate { get; set; }

        public Follow(int user_id,int following_id,DateTime followDate)
        {
            this.user_id = user_id;
            this.following_id = following_id;
            this.followDate = followDate;
        }
    }
}