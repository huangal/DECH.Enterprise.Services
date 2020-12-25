using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using DECH.Enterprise.Services.Customers.OpenApi.Models;
using DECH.Enterprise.Services.Customers.OpenApi;
using System.Linq;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class OpenApi
    {
        public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();
            services.Configure<ApiInfo>(configuration.GetSection("ApiInfo"));
            services.AddSwaggerGen();

            return services;

        }

        public static void UseSwaggerSettings(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            string applicationUri = "warehouse";

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger( options => {
                //options.SerializeAsV2 = false;
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase}" }
                    };
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/{applicationUri}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    options.DocumentTitle = "Customers.API";
                }

                //options.DefaultModelExpandDepth(2);
                //options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

                //options.EnableDeepLinking();
                //options.DisplayOperationId();
                               
            });


            //Add swagger eDocumentation
            app.UseReDoc(options =>
            {
                options.SpecUrl = $"/{applicationUri}/swagger/{provider.ApiVersionDescriptions.Last().GroupName}/swagger.json";
                options.DocumentTitle = "Customers.API";
                options.ConfigObject = new Swashbuckle.AspNetCore.ReDoc.ConfigObject()
                {
                };
            });
        }


    }

}

