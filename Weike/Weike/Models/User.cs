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


        public User(int id, string email, string name, string password)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            this.password = password;
        }
    }

   
}