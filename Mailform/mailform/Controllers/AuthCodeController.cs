using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mailform.Models;

namespace Mailform.Controllers
{
    public class AuthCodeController : Controller
    {
        public ActionResult Index()
        {
			if (!(Session["User"] is User))
			{
				return Redirect("/");
			}
			var user = Session["User"] as User;

            var userEdit = new UserEdit(user);

            return View(userEdit);
        }

        [HttpPost]
		public ActionResult Index(UserEdit userEdit)
		{
			if (!(Session["User"] is User))
			{
				return Redirect("/");
			}
            var user = Session["User"] as User;

            if(!String.Equals(user.AuthenticationCode, userEdit.AuthenticationCode))
            {
                ViewBag.Exception = new Exception("確認コードが一致しません");
                return View(userEdit);
            }

            return Redirect("/WriteMail/");
		}
    }
}