using Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Producer
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
            services.AddMassTransit(x =>
            {
                if (AppSettings.ServiceBusConnectionString.StartsWith(@"Endpoint=sb://"))
                {
                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(AppSettings.ServiceBusConnectionString);
                        cfg.ConfigureEndpoints(context);
                        cfg.AutoStart = true;
                    });
                }
                else if (AppSettings.ServiceBusConnectionString.StartsWith(@"amqp://"))
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(AppSettings.ServiceBusConnectionString);
                        cfg.ConfigureEndpoints(context);
                        cfg.AutoStart = true;
                    });
                }
                else
                {
                    throw new NotSupportedException("Connection string is not a supported format");
                }
                
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
