using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mailform.Models;

namespace Mailform.Controllers
{
    public class WriteMailController : Controller
    {
        public ActionResult Index()
        {
			if (!(Session["User"] is User))
			{
				return Redirect("/");
			}
			var user = Session["User"] as User;

            var mailEdit = new MailEdit()
            {
                From = user.MailAddress,
                To = "k_saitou33@hotmail.com"
            };

            return View (mailEdit);
        }

        [HttpPost]
        public ActionResult Index(MailEdit mailEdit)
        {
			if (!(Session["User"] is User))
			{
				return Redirect("/");
			}
			var user = Session["User"] as User;


            return View();
        }
    }
}
