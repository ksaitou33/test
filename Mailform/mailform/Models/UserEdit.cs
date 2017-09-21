using System;

namespace Mailform.Models
{
    public class UserEdit
    {
        public UserEdit()
        {
			MailAddress = "";
			AuthenticationCode = "";
            AdminPassword = "";
        }
        public UserEdit(User user) : this()
        {
            MailAddress = user.MailAddress;
        }

		public string MailAddress { get; set; }
        public string AuthenticationCode { get; set; }
        public string AdminPassword { get; set; }
    }
}
