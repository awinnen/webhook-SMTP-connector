using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using webhook_SMTP_connector.Contracts;

namespace webhook_SMTP_connector.Services
{
	public class SMTPService : IEmailSender
	{
		public SMTPService()
		{

		}

		public async Task SendEmail(SMTPWebhookModel model, SMTPHostConfig smtpConfig)
		{
			using (var smtpClient = new SmtpClient(smtpConfig.Host, smtpConfig.Port)
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(smtpConfig.Username, smtpConfig.Password),
				EnableSsl = smtpConfig.UseSTARTTLS,
				DeliveryMethod = SmtpDeliveryMethod.Network,
			})
			{
				var message = new MailMessage(new MailAddress(model.From.EmailAddress, model.From.Name), new MailAddress(model.To.EmailAddress, model.To.Name))
				{
					Subject = model.Subject,
					Body = model.Body,
					IsBodyHtml = true,
				};
				if (model.ReplyTo != null)
				{
					message.ReplyToList.Clear();
					message.ReplyToList.Add(new MailAddress(model.ReplyTo.EmailAddress, model.ReplyTo.Name));
				}
				await smtpClient.SendMailAsync(message);
			}
		}
	}
}