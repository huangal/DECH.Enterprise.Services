using AutoMapper;
using AutoMapper.Configuration;
using DECH.Enterprise.Services.Customers.Services.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class AutoMapper
    {
        
        public static IServiceCollection AddAutoMapperConfig<T>(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(T));
            services.AddSingleton<IConfigurationProvider>(RegisterMappings());

            return services;
        }

        private static MapperConfiguration RegisterMappings()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile<CustomerProfile>();

            return new MapperConfiguration(cfg);
        }
    }
}
