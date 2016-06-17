using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class PersonalPageController : Controller
    {
        // GET: PersonalPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonalPageWeike()
        {
            return View();
        }

        public ActionResult PersonalPageLikes()
        {
            return View();
        }

        public ActionResult PersonalPageFollows()
        {
            return View();
        }
    }
}