using CustomerApi.Messaging.Send.Sender.v1;
using DesafioFrameworkSystem.Api.Infrastructure.Prometheus;
using DesafioFrameworkSystem.Domain.Entities;
using DesafioFrameworkSystem.Messaging.Send.Options.v1;
using DesafioFrameworkSystem.Service.v1.Query;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Prometheus;
using System;
using System.IO;
using System.Reflection;

namespace DesafioFrameworkSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();
            
            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            serviceClientSettingsConfig = Configuration.GetSection("AzureServiceBus");
            services.Configure<AzureServiceBusConfiguration>(serviceClientSettingsConfig);

            services.AddAuthentication();

            services.AddMvc().AddFluentValidation();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Math Calc Api",
                    Description = "Simples Api de cálculo matemático",
                    Contact = new OpenApiContact
                    {
                        Name = "Ian Neru Letelier",
                        Email = "ian.neru@gmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            bool.TryParse(Configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);

            if (useRabbitMq)
            { 
                services.AddSingleton<IMathUpdateSender, MathUpdateSender>();
            }

            services.AddTransient<IRequestHandler<GetNumeroDivisoresQuery, MathEntity>, GetNumeroDivisoresQueryHandler>();
            services.AddTransient<IRequestHandler<GetDivisoresPrimosQuery, MathEntity>, GetDivisoresPrimosQueryHandler>();

            services.AddSingleton<MetricCollecter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Math Calc Api v1");

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMetricServer();
            app.UseMiddleware<ResponseMetricMiddleware>();
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
