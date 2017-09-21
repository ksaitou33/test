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

            Session["User"] = null;
            var userEdit = new UserEdit();

            return View(userEdit);
        }

        [HttpPost]
        public ActionResult Index(UserEdit userEdit)
        {
            //確認コードの生成
            var user = new User();
            user.MailAddress = userEdit.MailAddress;

            //メール送信
            var mail = new Mail();
            try
            {
                mail.From = "keigo@mdr.to";
                mail.To = user.MailAddress;
                mail.Subject = "test";
                mail.Body = "確認コード : " + user.AuthenticationCode;
            }
            catch(Exception e)
            {
                ViewBag.Exception = e;
                return View(userEdit);
            }

            Session["User"] = user;
            return Redirect("/AuthCode/");
        }
    }
}
