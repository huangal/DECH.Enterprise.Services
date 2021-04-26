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
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using DECH.Enterprise.Services.Customers.Contracts.Examples;
using System.Reflection;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class OpenApi
    {
        public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();
            services.Configure<ApiInfo>(configuration.GetSection("ApiInfo"));
            services.AddSwaggerGen();
            // .AddSwaggerGenNewtonsoftSupport();

          services.Configure<SwaggerOptions>(c => c.SerializeAsV2 = true);


            return services;

        }


        public static IServiceCollection AddSwaggerExamplesData(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblyOf<CustomerModelExample>();
            services.AddSwaggerExamplesFromAssemblyOf<CustomerModelExamples>();


            return services;

        }

        public static void UseSwaggerSettings(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, string pathBase, bool serializeAsV2 = false)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger( options => {
               // options.SerializeAsV2 = serializeAsV2;



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
                    options.SwaggerEndpoint($"{pathBase}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
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
                options.SpecUrl = $"{pathBase}/swagger/{provider.ApiVersionDescriptions.Last().GroupName}/swagger.json";
                options.DocumentTitle = "Customers.API";
                options.ConfigObject = new Swashbuckle.AspNetCore.ReDoc.ConfigObject()
                {
                };
            });
        }


    }


    public class ControllerHidingConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerName == "Customers")
            {
                controller.ApiExplorer.IsVisible = false;
            }
        }
    }

    public class ActionHidingConvention : IActionModelConvention
    {
        private HideApi _hideApi;

        public ActionHidingConvention(IConfiguration configuration)
        {
            _hideApi = configuration.GetSection("HideApi").Get<HideApi>();

        }

        public void Apply(ActionModel action)
        {
            if (IsExcluded(action.Controller.ControllerName))
            {
                action.ApiExplorer.IsVisible = false;
            }
        }

        private bool IsExcluded( string value)
        {
            var controll = _hideApi.Controllers.FirstOrDefault(x => x.Equals(value, System.StringComparison.OrdinalIgnoreCase));

            return controll != null;
        }

    }

    public class EnableApiExplorerApplicationConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
             //application.ApiExplorer.IsVisible = true;


            foreach (var controller in application.Controllers)
            {

                if (controller.ControllerName.Equals("Customers"))
                {
                    controller.ApiExplorer.IsVisible = false;
                }
                else
                {
                    controller.ApiExplorer.IsVisible = true;
                }
            }




            //var controller = application.Controllers.FirstOrDefault(x => x.ControllerName.Equals("Customers"));


            //if (controller!= null)
            //{
            //    controller.ApiExplorer.IsVisible = false;
            //}
            //else
            //{
            //    controller.ApiExplorer.IsVisible = true;
            //}


            //if ( application.Controllers.     .Controller.ControllerName == "Customers")
            //{
            //    controller.ApiExplorer.IsVisible = false;
            //}
        }
    }

}

