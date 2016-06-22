﻿using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public ActionResult UploadAvatar() {
            if (Session["user"] != null)
            {
                int user_id = ((User)Session["user"]).id;
                foreach (string upload in Request.Files)
                {
                    if (!Request.Files[upload].HasFile()) continue;
                    string mimetype = Request.Files[upload].ContentType;
                    string path = AppDomain.CurrentDomain.BaseDirectory + "avatars\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    filename = user_id+filename;
                    Request.Files[upload].SaveAs(Path.Combine(path, filename));
                    UserDB.UpdateAvatar(user_id, Path.Combine(filename));
                  
                }
            }
            return Json(true);

        }

    }
}