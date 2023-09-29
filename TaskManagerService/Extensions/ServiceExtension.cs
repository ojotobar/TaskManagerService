using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Service.Contracts;
using Services;

namespace TaskManagerService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureController(this IServiceCollection services) =>
            services
                .AddControllers(config => {
                    config.RespectBrowserAcceptHeader = false;
                    config.ReturnHttpNotAcceptable = true;
                })
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
               .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddXmlDataContractSerializerFormatters();

        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task List Manager",
                    Version = "v1",
                    Description = "Task Management API",
                    Contact = new OpenApiContact
                    {
                        Name = "Toba R. Ojo",
                        Email = "ojotobar@gmail.com",
                        Url = new Uri("https://ojotobar.netlify.app")
                    }
                });
            });

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
    }
}
