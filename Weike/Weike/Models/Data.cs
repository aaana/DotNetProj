using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{

    public class CommentData
    {
        public Weike weike { get; set; }
        public DateTime date { get; set; }
        public string comment_content { get; set; }

        public CommentData(Weike weike, DateTime date, string comment_content)
        {
            this.weike = weike;
            this.date = date;
            this.comment_content = comment_content;
        }
    }

    public class FavoriteData
    {
        public Weike weike { get; set; }
        public DateTime favoritedate { get; set; }

        public FavoriteData(Weike weike, DateTime favoritedate)
        {
            this.weike = weike;
            this.favoritedate = favoritedate;
        }
    }

}