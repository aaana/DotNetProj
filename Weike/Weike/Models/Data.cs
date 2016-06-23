﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{

    public class CommentData
    {
        public Weike weike { get; set; }
        public Comment comment { get; set; }
        public string author { get; set; }
        public string commenter { get; set; }
        public string avatar { get; set; }

        public CommentData(Weike weike, Comment comment,string author,string commenter,string avatar)
        {
            this.weike = weike;
            this.comment = comment;
            this.author = author;
            this.commenter = commenter;
            this.avatar = avatar;
        }
    }

    public class FavoriteData
    {
        public Weike weike { get; set; }
        public string author { get; set; }
        public string avatar { get; set; }
        public DateTime favoritedate { get; set; }

        public FavoriteData(Weike weike, string author,DateTime favoritedate,string avatar)
        {
            this.weike = weike;
            this.author = author;
            this.favoritedate = favoritedate;
            this.avatar = avatar;
        }
    }

    public class WeikeData
    {
        public Weike weike { get; set; }
        public string author { get; set; }
        public string avatar { get; set; }
        public MyFile attachment { get; set; }
        public WeikeData(Weike weike,string author,MyFile attachment,string avatar)
        {
            this.weike = weike;
            this.author = author;
            this.attachment = attachment;
            this.avatar = avatar;
        }
    }

    public class FollowData
    {
        public Follow follow { get; set; }
        //如果type为"following"即关注的人，则name为关注人的名字；如果type为"follower"即粉丝，则name为粉丝名
        public string name { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }

        public FollowData(Follow follow,string name,string type,string email,string avatar)
        {
            this.follow = follow;
            this.name = name;
            this.type = type;
            this.email = email;
            this.avatar = avatar;
        }
    }

    public class NoticeData
    {
        public Notice notice;
        public string name;
        public string title;
        public string avatar;


        public NoticeData(Notice notice, string name,  string title,string avatar)
        {
            this.notice = notice;
            this.name = name;
            this.title = title;
            this.avatar = avatar;
        }
    }


}