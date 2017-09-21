using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Mailform.Models
{
    public class User : Controller
    {
        public User()
        {
            MailAddress = "";

			var r = new Random();
			AuthenticationCode = String.Format("{0:D6}", r.Next(100000));

            IsAdmin = false;
        }

        public string MailAddress { get; set; }
        public string AuthenticationCode { get; }
        public bool IsAdmin { get; set; }
    }
}
