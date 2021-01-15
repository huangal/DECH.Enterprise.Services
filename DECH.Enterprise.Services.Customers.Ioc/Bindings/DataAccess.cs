using DECH.Enterprise.Services.Customers.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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

        public static void CreateSeedData(this IApplicationBuilder app, IConfiguration configuration)
        {
            int recordCount = configuration.GetValue<int>("TotalCustomerRecords");

            var dataContext = app.ApplicationServices.GetService<CustomersContext>();
            dataContext.CreateSeedData(recordCount);
        }

    }
}
