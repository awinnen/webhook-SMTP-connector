using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webhook_SMTP_connector.Contracts
{
	public class SMTPHost
	{
		public string Secret { get; set; }
		public SMTPHostConfig Config { get; set; }
	}
}
