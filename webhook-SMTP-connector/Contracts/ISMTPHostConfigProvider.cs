using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webhook_SMTP_connector.Contracts
{
	public interface ISMTPHostConfigProvider
	{
		public Task<SMTPHostConfig> GetConfig(string secret);
	}
}
