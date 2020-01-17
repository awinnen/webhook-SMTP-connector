using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webhook_SMTP_connector.Contracts;

namespace webhook_SMTP_connector.Providers
{
	public class SMTPHostConfigProvider: ISMTPHostConfigProvider
	{
		private readonly Dictionary<string, SMTPHostConfig> _hosts;

		public SMTPHostConfigProvider(IEnumerable<SMTPHost> hosts)
		{
			_hosts = hosts.ToDictionary(x => x.Secret, x => x.Config);
		}

		public Task<SMTPHostConfig> GetConfig(string secret)
		{
			var success =_hosts.TryGetValue(secret, out var config);

			if(!success)
			{
				throw new KeyNotFoundException($"No Host found for Secret {secret}");
			}
			return Task.FromResult(config);
		}
	}
}
