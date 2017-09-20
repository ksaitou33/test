using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Mailform.Models;

namespace Mailform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Mail mail)
        {
            //Test();

            return View(mail);
        }

        public void Test()
        {
            string from = @"k_saitou33@hotmail.com";
            string to = @"keigo@mdr.to";
            string subject = @"test";
            string body = "test¥ntest";

            var mail = new Mail(from, to, subject, body);
            mail.Send();
        }
    }
}
