using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webhook_SMTP_connector.Contracts
{
	public class SMTPHostConfig
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool UseSTARTTLS { get; set; }
	}
}
