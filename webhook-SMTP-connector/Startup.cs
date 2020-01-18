using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using webhook_SMTP_connector.Contracts;
using webhook_SMTP_connector.Providers;
using webhook_SMTP_connector.Services;

namespace webhook_SMTP_connector
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddFluentValidation(opt => {
				opt.RegisterValidatorsFromAssemblyContaining<SMTPWebhookModelValidator>();
				opt.ImplicitlyValidateChildProperties = true;
				opt.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
			});

			services.AddScoped<IEmailSender, SMTPService>();
			services.AddScoped<ISMTPHostConfigProvider, SMTPHostConfigProvider>();

			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.KnownProxies.Clear();
				options.KnownNetworks.Clear();
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
