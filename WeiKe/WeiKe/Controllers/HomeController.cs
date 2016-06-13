﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiKe.Models;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;
using System.Text;
using System.IO;

namespace WeiKe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

      [HttpPost]
        public ActionResult Index(string json)
        {
            ViewBag.message = true;

            List<Weike> weikes = WeikeDB.FindBySubject("软工");
            /*
            List<Weike> data = new List<Weike>();
            data.Add(new Weike { title = "Senseless Suffering", subject = "English", author = "Jeremy Bentham", src = "resource/img/8.jpg", size = "1280x853", description = "The question is not, 'Can they reason ?' nor, 'Can they talk ? ' but rather, 'Can they suffer ? '", star = 0 });
            data.Add(new Weike { title = "Rabbit Intelligence", subject = "English", author = "Robert Brault", src = "resource/img/9.jpg", size = "865x1280", description = "If a rabbit defined intelligence the way man does, then the most intelligent animal would be a rabbit, followed by the animal most willing to obey the commands of a rabbit.", star = 44 });
            data.Add(new Weike { title = "Friendly Terms", subject = "English", author = "Samuel Butler", src = "resource/img/10.jpg", size = "1280x1280", description = "Man is the only animal that can remain on friendly terms with the victims he intends to eat until he eats them. ", star = 44 });
            data.Add(new Weike { title = "Murder of Men", subject = "English", author = "Leonardo Da Vinci", src = "resource/img/11.jpg", size = "1280x1280", description = "The time will come when men such as I will look upon the murder of animals as they now look upon the murder of men. ", star = 44 });
            data.Add(new Weike { title = "Highest Ethics", subject = "Math", author = "Thomas Edison", src = "resource/img/1.jpg", size = "1280x853", description = "Non-violence leads to the highest ethics, which is the goal of all evolution. Until we stop harming all other living beings, we are still savages ", star = 44 });
            data.Add(new Weike { title = "Highest Ethics", subject = "Math", author = "Thomas Edison", src = "resource/img/1.jpg", size = "1280x853", description = "Non-violence leads to the highest ethics, which is the goal of all evolution. Until we stop harming all other living beings, we are still savages ", star = 44 });
            data.Add(new Weike { title = "Senseless Suffering", subject = "English", author = "Jeremy Bentham", src = "resource/img/8.jpg", size = "1280x853", description = "The question is not, 'Can they reason ?' nor, 'Can they talk ? ' but rather, 'Can they suffer ? '", star = 0 });
            data.Add(new Weike { title = "Rabbit Intelligence", subject = "English", author = "Robert Brault", src = "resource/img/9.jpg", size = "865x1280", description = "If a rabbit defined intelligence the way man does, then the most intelligent animal would be a rabbit, followed by the animal most willing to obey the commands of a rabbit.", star = 44 });
    */        
            return Json(weikes);
        }

        [HttpPost]
        public ActionResult playground()
        {
            ViewBag.message = true;
            List<Weike> list = WeikeDB.FindByAuthor("杜");
            Favorite favorite = new Favorite(1, 1, DateTime.Now.ToShortDateString().ToString());
            //FavoriteDB.Insert(favorite);
            /*
            list.Add(new Weike { title = "Senseless Suffering", subject = "English", author = "Jeremy Bentham", src = "resource/img/8.jpg", size = "1280x853", description = "The question is not, 'Can they reason ?' nor, 'Can they talk ? ' but rather, 'Can they suffer ? '", star = 0 });
            list.Add(new Weike { title = "Rabbit Intelligence", subject = "English", author = "Robert Brault", src = "resource/img/9.jpg", size = "865x1280", description = "If a rabbit defined intelligence the way man does, then the most intelligent animal would be a rabbit, followed by the animal most willing to obey the commands of a rabbit.", star = 44 });
            list.Add(new Weike { title = "Friendly Terms", subject = "English", author = "Samuel Butler", src = "resource/img/10.jpg", size = "1280x1280", description = "Man is the only animal that can remain on friendly terms with the victims he intends to eat until he eats them. ", star = 44 });
            list.Add(new Weike { title = "Murder of Men", subject = "English", author = "Leonardo Da Vinci", src = "resource/img/11.jpg", size = "1280x1280", description = "The time will come when men such as I will look upon the murder of animals as they now look upon the murder of men. ", star = 44 });
            list.Add(new Weike { title = "Highest Ethics", subject = "Math", author = "Thomas Edison", src = "resource/img/1.jpg", size = "1280x853", description = "Non-violence leads to the highest ethics, which is the goal of all evolution. Until we stop harming all other living beings, we are still savages ", star = 44 });
    */        
            return Json(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}