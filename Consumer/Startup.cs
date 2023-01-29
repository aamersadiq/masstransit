using Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Azure.Messaging.ServiceBus.Administration;

namespace Consumer
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
                x.AddConsumer<QueueMessageConsumer>();
                x.AddConsumer<TopicMessageConsumer>();

                if (AppSettings.ServiceBusConnectionString.StartsWith(@"Endpoint=sb://"))
                {

                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        // Required to hook consumer to queue
                        cfg.ReceiveEndpoint(AppSettings.QueueName, endpointConfigurator =>
                        {
                            endpointConfigurator.ConfigureConsumer<QueueMessageConsumer>(context);
                        });
                        // below code only required if custom subscription is needed e.g.subscription for environment, else it will create default subscription as name of the type
                        cfg.SubscriptionEndpoint(
                            "topic-message-for-staging",
                            "common/topicMessage",
                            ep =>
                            {
                                ep.Consumer(() => new TopicMessageConsumer());
                                ep.MaxConcurrentCalls = 24;
                                ep.Rule = new CreateRuleOptions
                                {
                                    Name = "topic-message-for-staging",
                                    Filter = new SqlRuleFilter("Tenant='staging'") // Set in  publishEndpoint.Publish call with p.Headers.Set("Environment", "staging")
                                };

                            });
                        cfg.Host(AppSettings.ServiceBusConnectionString);
                        cfg.ConfigureEndpoints(context);
                        cfg.AutoStart = true;
                    });
                }
                else if (AppSettings.ServiceBusConnectionString.StartsWith(@"amqp://"))
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        // Required to hook consumer to queue
                        cfg.ReceiveEndpoint(AppSettings.QueueName, endpointConfigurator =>
                        {
                            endpointConfigurator.ConfigureConsumer<QueueMessageConsumer>(context);
                        });

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
