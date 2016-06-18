using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;

namespace WeiKe.Controllers
{
    public class UserController : Controller
    {
       
        [HttpPost]
        public ActionResult ModifyPassword(int user_id,string pwd)
        {
            bool result = UserDB.UpdatePassword(user_id, pwd);
            return Json(result);
        }

        [HttpPost]
        public ActionResult ModifyName(int user_id, string name)
        {
            bool result = UserDB.UpdateName(user_id, name);
            return Json(result);
        }
    }
}