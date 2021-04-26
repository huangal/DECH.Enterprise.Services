using System.Linq;
using System.Text.Json;
using DECH.Enterprise.Services.Customers.DataAccess;
using DECH.Enterprise.Services.Customers.Ioc.Models;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class HealthChecks
    {
        public static IServiceCollection AddDefaultHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<CustomersContext>()
                .AddCheck("Customers", () =>
                        HealthCheckResult.Degraded("Check failed due latency"));

            services.AddHealthChecksUI(s =>
                {
                    s.AddHealthCheckEndpoint("HealthLive", "https://localhost:44318/health");

                    s.AddHealthCheckEndpoint("Health", "https://localhost:44382/warehouse/healths");

                })
                .AddInMemoryStorage();

        
            //services.AddHealthChecksUI()
            //.AddInMemoryStorage(); 

            // services.AddHealthChecksUI().AddSqlServerStorage(Configuration.GetConnectionString("HealthChecks"));


            return services;
        }

        public static IEndpointRouteBuilder MapDefaultHealthChecks(this IEndpointRouteBuilder endpoints)
        {

            // endpoints.MapHealthChecksUI();





            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = "/health-ui";
                options.AddCustomStylesheet("dotnet.css");

                //options.ApiPath = "/healthApi";

            });


            endpoints.MapHealthChecks("/healthApi", new HealthCheckOptions()
            {
                Predicate = _ => true,
                //ResponseWriter = HealthCheckResponses.WriteStatusResponse
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });



            //endpoints.MapHealthChecksUI("/health", new HealthCheckOptions
            //{
            //    ResponseWriter = HealthCheckResponses.WriteStatusResponse
            //});

            //endpoints.MapHealthChecksUI("/health/live", new HealthCheckOptions
            //{
            //    Predicate = _ => false,
            //    ResponseWriter = HealthCheckResponses.WriteStatusResponse

            //});
            //endpoints.MapHealthChecksUI("/health/ready", new HealthCheckOptions
            //{
            //    ResponseWriter = HealthCheckResponses.WriteStatusResponse
            //});


            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponses.WriteStatusResponse
            });

            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = HealthCheckResponses.WriteStatusResponse

            });
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponses.WriteStatusResponse
            });



            return endpoints;

        }

        private static HealthCheckOptions WriteResponse()
        {
            return new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description

                        }),
                        Duration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            };
        }
    }


    
}
