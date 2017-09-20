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

            var mail = new Mail();

            return View(mail);
        }

        public void Test()
        {
            var from = @"k_saitou33@hotmail.com";
            var to = @"keigo@mdr.to";
            var subject = @"test";
            var body = "test¥ntest";

            try
            {
				var mail = new Mail()
				{
					From = from,
					To = to,
					Subject = subject,
					Body = body
				};
				mail.Send();
            }
            catch(Exception e)
            {
                throw e;
            }

        }
    }
}
