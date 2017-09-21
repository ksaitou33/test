using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mailform.Models;

namespace Mailform.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminPassword = @"admin#pass";
        private const string AdminMailAddress = @"k_saitou33@hotmail.com";

        public ActionResult Index()
        {
            var userEdit = new UserEdit();

            return View(userEdit);
        }

        [HttpPost]
        public ActionResult Index(UserEdit userEdit)
        {
            if(String.Equals(userEdit.AdminPassword, AdminPassword))
            {
                var user = new User()
                {
                    MailAddress = AdminMailAddress,
                    IsAdmin = true
                };
                Session["User"] = user;

                return Redirect("/Admin/Menu/");
            }

            return View(userEdit);
        }

        public ActionResult Menu()
        {

            return View();
        }
    }
}
