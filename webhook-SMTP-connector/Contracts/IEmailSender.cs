using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webhook_SMTP_connector.Contracts
{
	public interface IEmailSender
	{
		Task SendEmail(SMTPWebhookModel model, SMTPHostConfig smtpConfig);
	}
}
