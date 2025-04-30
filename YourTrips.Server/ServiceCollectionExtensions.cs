using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace YourTrips.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            // Add services to the container.

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            services.AddControllers();
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                //Add commentaries with XAML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
            return services;
        }
    }
}
