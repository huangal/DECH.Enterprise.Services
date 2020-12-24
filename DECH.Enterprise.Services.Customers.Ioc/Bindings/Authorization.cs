

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class Authorization
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(PolicyType.PartnerAccess,
            //        policy =>
            //        {
            //            policy.Requirements.Add(new PartnerAccessRequirement());

            //        });
            //});


            //services.AddScoped<IAuthorizationHandler, PartnerAccessHandler>();
            return services;
        }
    }
}
