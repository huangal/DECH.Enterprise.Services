using System.Text.Json.Serialization;
using DECH.Enterprise.Services.Customers.DataAccess;
using DECH.Enterprise.Services.Customers.Ioc.Bindings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;

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

            //services.AddControllers()
            //    .AddNewtonsoftJson(options =>
            //    {
            //        options.SerializerSettings.Converters.Add(new StringEnumConverter());
            //    });
            //    .AddFluentValidation();



            services.AddControllers(options =>
                    {
                        //options.Conventions.Add(new ControllerHidingConvention());
                         options.Conventions.Add(new ActionHidingConvention( Configuration));
                        //options.Conventions.Add(new EnableApiExplorerApplicationConvention());
                         
                    })
                    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.IgnoreNullValues = true;
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
                    //.AddNewtonsoftJson(options =>
                    //{
                    //    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    //});



        //    .AddNewtonsoftJson(options =>
        //     {
        //         options.SerializerSettings.Converters.Add(new StringEnumConverter());
        //     })
        //.AddFluentValidation();



            services.AddVersioning()
                    .RegisterServices(Configuration)
                    .AddAutoMapperConfig<Startup>()
                    .AddSwaggerSettings(Configuration)
                    .AddSwaggerExamplesData()
                    .AddAppAuthorization()
                    .AddDefaultHealthChecks()
                    .AddDataProtection();


           

           // services.AddSwaggerExamples();

            


            //services.AddSwaggerExamplesFromAssemblyOf<Startup>();

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

            string pathBase = "/warehouse";

            app.UsePathBase(pathBase);


            app.CreateSeedData(Configuration);
            app.UseSwaggerSettings(provider, pathBase, true);
    
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
                endpoints.MapDefaultHealthChecks();
                endpoints.MapControllers();
            });
        }
    }
}
