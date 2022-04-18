using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using USATU.Monitoring.Core;
using USATU.Monitoring.Data;
using USATU.Monitoring.Web.Extensions;
using USATU.Monitoring.Web.HostedServices;
using USATU.Monitoring.Web.HostedServices.TaskWorker;
using USATU.Monitoring.Web.Logging;
using USATU.Monitoring.Web.RabbitMQ;

namespace USATU.Monitoring.Web
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
            services.AddSingleton<ILogger>(_ => new Logger("logs.txt"));
            services.AddControllers();
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "USATU.Monitoring.Web", Version = "v1" });
            });

            services
                .AddData(Configuration)
                .AddCore();

            services.AddHostedService<MigrationHostedService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionMiddleware();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "USATU.Monitoring v1"));
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
