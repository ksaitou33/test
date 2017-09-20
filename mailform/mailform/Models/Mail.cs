﻿using System;
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
				throw new Exception();
			}
			if (String.IsNullOrEmpty(to))
			{
				throw new Exception();
			}
			if (String.IsNullOrEmpty(subject))
			{
				throw new Exception();
			}
			if (String.IsNullOrEmpty(body))
			{
				throw new Exception();
			}

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using(var client = new SmtpClient())
            {
                client.Connect(server, port, false);
                client.Send(message);
                client.Disconnect(true);
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
					throw new Exception();
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
					throw new Exception();
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
					throw new Exception();
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
					throw new Exception();
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
                    throw new Exception();
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
                    throw new Exception();
                }
                this.port = value;
            }
        }
    }
}
