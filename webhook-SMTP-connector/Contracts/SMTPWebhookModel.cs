using FluentValidation;
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

	public class SMTPWebhookModelValidator: AbstractValidator<SMTPWebhookModel>
	{
		public SMTPWebhookModelValidator()
		{
			RuleFor(x => x.Body).NotEmpty();
			RuleFor(x => x.Subject).NotEmpty();
			RuleFor(x => x.From).NotNull()
				.ChildRules(mp => mp.RuleFor(z => z.EmailAddress).NotEmpty());
			RuleFor(x => x.To).NotNull()
				.ChildRules(mp => mp.RuleFor(z => z.EmailAddress).NotEmpty());
		}
	}
}
