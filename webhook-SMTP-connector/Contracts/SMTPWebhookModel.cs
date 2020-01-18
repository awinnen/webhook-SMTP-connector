using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webhook_SMTP_connector.Contracts
{
	public class SMTPWebhookModel
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public EmailParticipant To { get; set; }
		public EmailParticipant From { get; set; }
		public EmailParticipant ReplyTo { get; set; }
	}
}
