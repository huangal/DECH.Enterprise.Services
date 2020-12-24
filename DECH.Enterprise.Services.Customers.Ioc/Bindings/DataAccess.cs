using DECH.Enterprise.Services.Customers.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class DataAccess
    {
        public static void CreateSeedData(this IApplicationBuilder app)
        {
            int recordCount = 2500;

            var dataContext = app.ApplicationServices.GetService<CustomersContext>();
            dataContext.CreateSeedData(recordCount);
        }

    }
}
