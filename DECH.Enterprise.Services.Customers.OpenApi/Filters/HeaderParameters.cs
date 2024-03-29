﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace DECH.Enterprise.Services.Customers.OpenApi.Models
{
    public class HeaderParameters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            //if (descriptor != null && !descriptor.ControllerName.StartsWith("Weather"))
            //{


            //}




            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-client-name",
                Description = "Client resgistered name",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema() { Type = "string" }
            });

            IDictionary<string, OpenApiExample> examples = new Dictionary<string, OpenApiExample>
            {
                {
                    "par1",
                    new OpenApiExample
                    {
                        Description = "CIS Number:   CISNumber|12345678",
                        Summary = "CISNumber"
                    }
                },

                {
                    "par2",
                    new OpenApiExample
                    {
                        Description = "ELB Number:   ELBNumber|12345678",
                        Summary = "ELBNumber"
                    }
                },
                {
                    "par3",
                    new OpenApiExample
                    {
                        Description = "Routing and ELb:   RoutingAndElbNumber|101000695:123456781231231",
                        Summary = "RoutingAndElbNumber"
                    }
                }
            };

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-customer-id",
                Description = "Customer Id",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema() { Type = "string" },
                Examples = examples
            });

            if (descriptor != null && !descriptor.ControllerName.IsExcluded())
            {

                IDictionary<string, OpenApiExample> requestIdSample = new Dictionary<string, OpenApiExample>
                {
                    {
                        "part1",
                        new OpenApiExample
                        {
                            Description = "2c4d133b-52e6-4e8a-a88b-1e69c069aeae",
                            Summary = "GUID"
                        }
                    }
                };
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "RequestId",
                    Description = "Operation id",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema() { Type = "string" },
                    Examples = requestIdSample
                });
                }

        }


        private bool Excluded(string value)
        {
            var controllers = new List<string>
            {
                "WeatherForecast",
                "Accounts"
            };

            var controll = controllers.FirstOrDefault(x => x.Equals(value, System.StringComparison.OrdinalIgnoreCase));

            if (controll != null) return true;
            return false;
        }


    }
}
