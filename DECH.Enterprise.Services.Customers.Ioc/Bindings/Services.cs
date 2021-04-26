using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DECH.Enterprise.Services.Customers.Contracts.Interfaces;
using DECH.Enterprise.Services.Customers.Services;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using DECH.Enterprise.Services.Customers.OpenApi.Models;

namespace DECH.Enterprise.Services.Customers.Ioc.Bindings
{
    public static class Services
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HideApi>(configuration.GetSection("HideApi"));

            services.AddSingleton<ICustomerService, CustomerService>();

            //services.AddSingleton<IAuthorizationHandler, PartnerAccessHandler>();

            //Log.Logger = new LoggerConfiguration()
            //.Enrich.FromLogContext()
            //.WriteTo.Console()
            //.CreateLogger();

            services.RegisterLogger(configuration);

            services.AddScoped<IClientConfiguration, ClientConfiguration>();

            services.AddScoped<IApiTransactionId, ApiTransactionId>();
            //services.AddScoped<ModelValidationAttribute>();
            //services.AddScoped<AsyncModelValidationAttribute>();



            //services.AddSingleton<IGreeting, Saludos>();

            //services.AddSingleton<IGreeting<EnglishGreetings>, EnglishGreetings>();



            // services.Configure<Person>();


            //services.AddHttpClient<IWeatherForecastService, WeatherForecastService>()
            //    .ConfigurePrimaryHttpMessageHandler(handler => new MockWeatherForecastHandler());
            //services.AddSingleton<IWeatherManager, WeatherManager>();


            //services.AddHttpClient<IConfigurationService, ConfigurationService>()
            //   .ConfigurePrimaryHttpMessageHandler(handler => new Return200ResponseHandler());

            //services.AddHttpClient();

            //services.AddSingleton<IImageService, ImageService>();
            //services.AddWkhtmltopdf("wkhtmltopdf");

            //services.AddSingleton<IPdfEngine, PdfEngine>();

            services.AddSingleton<UniqueCode>();
            services.AddSingleton<IServiceDataProtection, DataProtectionService>();




            return services;
        }

        

        //private static ApiInfo LoadApiInfo(IConfiguration configuration)
        //{

        //    var apiInfo = configuration.GetSection("ApiInfo").Get<ApiInfo>();
        //    return apiInfo;
        //}


        //private static void DBConfigure<T>(this IServiceCollection services) where T: class
        //{
        //    IServiceProvider serviceProvider = services.BuildServiceProvider();
        //    var configuration = serviceProvider.GetService<IConfiguration>();

        //    services.AddSingleton<T>(configuration.GetSection("ApiInfo").Get<T>());
           
        //}


        public static IServiceCollection RegisterLogger(this IServiceCollection services, IConfiguration configuration)
        {

            //var formatter = new SplunkFormatter();
           // var formatter = new CustomSerilogFormatter("TestApp", "1.2.3");
            //formatter.FormatEvent(logEvent, output, new JsonValueFormatter(typeTagName: "$type"));  

//#if DEBUG
//            string logfile = configuration["logFile"];

//            Log.Logger = new LoggerConfiguration()
//                 .WriteTo.File(logfile,
//                rollingInterval: RollingInterval.Day,
//                retainedFileCountLimit: 7,
//                outputTemplate: "Timestamp={Timestamp:yyyy-MM-dd HH:mm:ss fff}|Level={Level:u3}|Application=\"{Application}\"|Message=\"{Message}\"|Exception=\"{Exception}\" {NewLine} ")
//                .Enrich.WithProperty("Application", "Banking.Customer")
//                 .WriteTo.Console()
//                  .CreateLogger();
//#else
//            Log.Logger = new LoggerConfiguration()
//                            .ReadFrom.Configuration(configuration)
//                            //.Enrich.WithExceptionDetails()
//                            .CreateLogger();
//#endif

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();


            return services;
        }


        //private static void Configure<T>(this IServiceCollection services) where T : class
        //{
        //    services.AddSingleton<IConfigOptions<ApiInfo>, AppConfigurations<ApiInfo>>(); 
        //}

    }

}
