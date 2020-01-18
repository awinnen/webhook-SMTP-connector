using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webhook_SMTP_connector.Contracts;

namespace webhook_SMTP_connector.Controllers
{
	[ApiController]
	[Route("send")]
	public class SMTPSenderController : ControllerBase
	{

		private readonly ILogger<SMTPSenderController> _logger;
		private readonly IEmailSender _emailSender;
		private readonly ISMTPHostConfigProvider _smtpConfigProvider;

		public SMTPSenderController(ILogger<SMTPSenderController> logger, IEmailSender emailSender, ISMTPHostConfigProvider smtpConfigProvider)
		{
			_emailSender = emailSender;
			_smtpConfigProvider = smtpConfigProvider;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromQuery] string secret, [FromBody] SMTPWebhookModel model)
		{
			if(string.IsNullOrWhiteSpace(secret))
			{
				return BadRequest("Queryparameter secret is required");
			}
			try
			{
				await _emailSender.SendEmail(model, await _smtpConfigProvider.GetConfig(secret));
				return Ok();
			}
			catch(Exception e)
			{
				_logger.LogError(e, "Unknown Error occured");
				return StatusCode(500);
			}
		}
	}
}
