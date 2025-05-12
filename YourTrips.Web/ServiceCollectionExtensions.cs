using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace YourTrips.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds web-related services to the DI container
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        /// <returns>Configured service collection</returns>
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            // Add MVC controllers support
            services.AddControllers();

            // Add API explorer services (required for Swagger)
            services.AddEndpointsApiExplorer();

            // Configure Swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YourTrips API", Version = "v1" });
            });

            // --- CORS Configuration ---
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    // WARNING: AllowAnyOrigin() is NOT recommended for production, especially with cookies!
                    // Better specify exact frontend domains: .WithOrigins("http://localhost:3000", "https://yourfrontend.com")
                    // Also requires .AllowCredentials() for cookies to work with cross-domain requests
                    // If using .AllowCredentials(), you CANNOT use .AllowAnyOrigin()
                    policy
                        // .WithOrigins("http://localhost:xxxx") // Replace xxxx with your frontend port
                        .AllowAnyOrigin() // Temporary for development, or if API is completely public
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    // .AllowCredentials(); // Needed if frontend is on different domain and you're NOT using AllowAnyOrigin
                });
            });

            return services;
        }
    }
}