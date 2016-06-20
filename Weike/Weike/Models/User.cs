using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiKe.Models
{
    public class User
    {
        private int _id = 0;
        public int id { get { return _id;  } set { _id = value; } }
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int followNum { get; set; }
        public int favoriteNum { get; set; }
        public int postNum { get; set; }
        public string des { get; set; }
        public string tag { get; set; }

        public User(int id, string email, string name, string password,int followNum,int favoriteNum,int postNum,string des,string tag)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            this.password = password;
            this.followNum = followNum;
            this.favoriteNum = favoriteNum;
            this.postNum = postNum;
            this.des = des;
            this.tag = tag;
        }
    }

   
}