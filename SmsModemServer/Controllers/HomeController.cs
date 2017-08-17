using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmsModemServer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult API()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
    }
}