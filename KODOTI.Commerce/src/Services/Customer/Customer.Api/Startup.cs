using Customer.Persistence.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Customer.Service.Queries;

namespace Customer.Api
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
			// DbContext
			services.AddDbContext<CustomerDbContext>(
				options => options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"),
					x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Customer")
				)
			);

			// Health check
			services.AddHealthChecks()
					.AddCheck("self", () => HealthCheckResult.Healthy())
					.AddDbContextCheck<CustomerDbContext>(typeof(CustomerDbContext).Name);

			// Event handlers
			services.AddMediatR(Assembly.Load("Customer.Service.EventHandlers"));

			// Query services
			services.AddTransient<IClientQueryService, ClientQueryService>();

			services.AddControllers();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer.Api", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				loggerFactory.AddSyslog(
					Configuration.GetValue<string>("Papertrail:host"),
					Configuration.GetValue<int>("Papertrail:port")
				);
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer.Api v1"));

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
				{
					Predicate = _ => true,
					ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
				});

				endpoints.MapControllers();
			});
		}
	}
}
