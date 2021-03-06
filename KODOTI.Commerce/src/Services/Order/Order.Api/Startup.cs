using Common.Logging;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.Persistence.Database;
using Order.Service.Proxies;
using Order.Service.Proxies.Catalog;
using Order.Service.Queries;
using System.Reflection;
using System.Text;

namespace Order.Api
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
			// HttpContextAccessor
			services.AddHttpContextAccessor();

			// DbContext
			services.AddDbContext<OrderDbContext>(
				options => options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"),
					x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Order")
				)
			);

			// Health check
			services.AddHealthChecks()
					.AddCheck("self", () => HealthCheckResult.Healthy())
					.AddDbContextCheck<OrderDbContext>(typeof(OrderDbContext).Name);

			// ApiUrls
			services.Configure<ApiUrls>(opts => Configuration.GetSection("ApiUrls").Bind(opts));

			// AzureServiceBus
			services.Configure<AzureServiceBus>(opts => Configuration.GetSection("AzureServiceBus").Bind(opts));

			// Proxies
			//services.AddHttpClient<ICatalogHttpProxy, CatalogHttpProxy>();
			services.AddTransient<ICatalogQueueProxy, CatalogQueueProxy>();

			// Event handlers
			services.AddMediatR(Assembly.Load("Order.Service.EventHandlers"));

			// Query services
			services.AddTransient<IOrderQueryService, OrderQueryService>();

			// API Controllers
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.Api", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				
			}else
			{
				loggerFactory.AddSyslog(
					Configuration.GetValue<string>("Papertrail:host"),
					Configuration.GetValue<int>("Papertrail:port")
				);
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.Api v1"));

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
