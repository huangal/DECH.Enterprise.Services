using System.Collections.Generic;
using System.Linq;
using DECH.Enterprise.Services.Customers.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DECH.Enterprise.Services.Customers.OpenApi.Filters
{
    public class CustomSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {


            var pathsToRemove = swaggerDoc.Paths
                .Where(pathItem => pathItem.Key.Contains("Customers"))
                .ToList();

            foreach (var item in pathsToRemove)
            {
                swaggerDoc.Paths.Remove(item.Key);
            }



           // //var nonMobileRoutes = swaggerDoc.Paths
           // //    .Where(x => !x.Key.ToLower().Contains("public"))
           // //    .ToList();
           // //nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });


           // var descriptor = context.ApiDescriptions.ToList();//.ActionDescriptor as ControllerActionDescriptor;

           // //if (descriptor != null && descriptor.ControllerName.IsExcluded())
           // //{
           // //    //descriptor.ActionConstraints
           // //    // action.ApiExplorer.IsVisible = false;
           // //}

           // // var data = descriptor.Where(x => x.)

           // var descriptions = context.ApiDescriptions.SelectMany(group => group.ActionDescriptor.DisplayName);
           //// swaggerDoc.Paths.Remove("Customers")


           // var nonMobileRoutes = swaggerDoc.Paths
           //     .Where(x => !x.Key.IsExcluded())
           //     .ToList();
           // nonMobileRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });


        }
    }


    public class HideControllers : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null && descriptor.ControllerName.IsExcluded())
            {
                //descriptor.ActionConstraints
               // action.ApiExplorer.IsVisible = false;
            }
         }
    }



    
}
