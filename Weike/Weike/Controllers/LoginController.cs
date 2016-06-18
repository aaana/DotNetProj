using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Register(string email,string name,string password)
        {
            int result = UserDB.Insert(0, email, name, password);
            return Json(result);
        }

        //user_id==0 表示登陆失败
        [HttpPost]
        public ActionResult Login(string email,string pwd)
        {
            User user = UserDB.FindByEmail(email);
            int user_id = 0;
            if (user.password.Equals(pwd))
            {
                user_id = user.id;
                Session["user_id"] = user_id;
                Session["email"] = email;
                Session["name"] = user.name;
            }
           
            return Json(user_id);
         
        }

    }
}