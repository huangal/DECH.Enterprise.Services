using DECH.Enterprise.Services.Customers.DataAccess;
using DECH.Enterprise.Services.Customers.Ioc.Bindings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DECH.Enterprise.Services.Customers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddVersioning()
                .RegisterServices(Configuration)
                .AddAutoMapperConfig<Startup>()
                .AddSwaggerSettings(Configuration)
                .AddAppAuthorization()
                .AddDataProtection();

            services.AddDbContext<CustomersContext>(context => { context.UseInMemoryDatabase("Customers"); });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                  });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/warehouse");

            app.CreateSeedData();
            app.UseSwaggerSettings(provider);
    
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("_myAllowSpecificOrigins");

            app.UseAuthorization();

            //Register custom middleware
            //app.UseClientConfiguration();
            //app.UseLogger();

            //app.UseApiProcessId();
            //app.UseClientConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
