using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using DECH.Enterprise.Services.Customers.OpenApi.Filters;
using DECH.Enterprise.Services.Customers.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DECH.Enterprise.Services.Customers.OpenApi
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class SwaggerConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly ApiInfo _apiInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerConfigurationOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        /// <param name="configuration"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="configOptions"></param>
        public SwaggerConfigurationOptions(IApiVersionDescriptionProvider provider,
                    IOptions<ApiInfo> configOptions)
        {
            _provider = provider;
            _apiInfo = configOptions.Value;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.SchemaFilter<DescribeEnumMemberValues>();

            //options.ResolveConflictingActions(apiDesctions => apiDesctions.First());

            // options.DocumentFilter<CustomSwaggerFilter>();


            // options.MapType<AccountType>(c =>
            //{
            //    return new OpenApiSchema {type = "string", @enum = Enum.GetNames(typeof(AccountType)) };
            //});



            //options.MapType<AccountType>(() => new OpenApiSchema { Type = "string", Enum = Enum.GetNames(typeof(AccountType)) });


            //  options.MapType<AccountType>(() => new OpenApiSchema { Type = "string", Enum = (IList<IOpenApiAny>)Enum.GetNames(typeof(AccountType)).ToList() });



            options.OperationFilter<SwaggerDefaultValues>();
            //options.OperationFilter<HeaderParameters>();

            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            // integrate xml comments
            XmlCommentsFiles.ForEach(file => options.IncludeXmlComments(file, includeControllerXmlComments: true));

            options.CustomSchemaIds(x =>
             {
                 if(Interfaces.Contains(x.Name))
                     return  $"Interface.{x.Name}";
                 else
                 return  $"Model.{x.Name}";

              });

            options.ExampleFilters();


        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = _apiInfo.Title,
                Version = description.ApiVersion.ToString(),
                Description = _apiInfo.Description,
                TermsOfService = new Uri(_apiInfo.TermsOfService),
                Contact = new OpenApiContact
                {
                    Name = _apiInfo.Contact.Name,
                    Email = _apiInfo.Contact.Email,
                    Url = new Uri(_apiInfo.Contact.Url),
                },
                License = new OpenApiLicense
                {
                    Name = _apiInfo.License.Name,
                    Url = new Uri(_apiInfo.License.Url),
                }
            };


            if (description.IsDeprecated)
            {
                openApiInfo.Description += " This API version has been deprecated.";
            }

            return openApiInfo;
        }


        private static List<string> XmlCommentsFiles
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                return Directory.GetFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            }
        }




        private IEnumerable<string> Interfaces =>
            new string[] { "AccountsQueryResponse", "CustomersResponse" };

    }

    

    
}

