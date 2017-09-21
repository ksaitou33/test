using System;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace Mailform.Models
{
    public class Mail
    {
        private const string DefaultServer = @"127.0.0.1";
        private const int DefaultPort = 25;

        private string server;
        private int port;

        private string from;
        private string to;
        private string subject;
        private string body;

        public Mail()
        {
			this.server = DefaultServer;
			this.port = DefaultPort;
        }

        public void Send()
        {
			if (String.IsNullOrEmpty(from))
			{
				throw new Exception("送信元のメールアドレスが空です");
			}
			if (String.IsNullOrEmpty(to))
			{
				throw new Exception("送信先のメールアドレスが空です");
			}
			if (String.IsNullOrEmpty(subject))
			{
				throw new Exception("メールの件名あ空です");
			}
			if (String.IsNullOrEmpty(body))
			{
				throw new Exception("メールの本文が空です");
			}

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(to));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(server, port, false);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch(Exception e)
            {
                throw new Exception("メール送信に失敗しました" + e.Message);
            }
        }

        public string From
        {
            get
            {
                return from;
            }
            set
            {
				if (String.IsNullOrEmpty(value))
				{
					throw new Exception("送信元のメールアドレスが空です");
				}
                this.from = value;
            }
        }

		public string To
		{
			get
			{
				return to;
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					throw new Exception("送信先のメールアドレスが空です");
				}
				this.to = value;
			}
		}

		public string Subject
		{
			get
			{
				return subject;
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					throw new Exception("メールの件名が空です");
				}
				this.subject = value;
			}
		}

		public string Body
		{
			get
			{
				return body;
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					throw new Exception("メールの本文が空です");
				}
				this.body = value;
			}
		}

        public string Server
        {
            get
            {
                return server;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("メールサーバのアドレスが空です");
                }
                this.server = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                if(value < 0 || 65535 < value)
                {
                    throw new Exception("メールサーバのポート指定が不正です");
                }
                this.port = value;
            }
        }
    }
}
