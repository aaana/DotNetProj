using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiKe.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index(int type)
        {
            ViewBag.authType = type;
            return View();
        }
    }
}