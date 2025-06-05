using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace YourTrips.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds web-related services to the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="configuration">The application's configuration.</param>
        /// <returns>The configured service collection.</returns>
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            // Add support for MVC controllers
            services.AddControllers();

            // Register AutoMapper with mapping profile from the Infrastructure layer
            services.AddAutoMapper(typeof(YourTrips.Infrastructure.MappingProfile));

            // Add support for minimal APIs and Swagger
            services.AddEndpointsApiExplorer();

            // Register IHttpClientFactory
            services.AddHttpClient();

            // Configure Swagger/OpenAPI documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YourTrips API", Version = "v1" });

                // Add Bearer token security definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                // Add security requirement for Bearer token
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Configure CORS (Cross-Origin Resource Sharing)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    // WARNING: AllowAnyOrigin() is NOT recommended in production
                    // Instead, use .WithOrigins() and specify trusted frontend domains

                    policy
                        .WithOrigins(
                            "https://localhost:3000",
                            "https://192.168.0.104:3000" // Replace with your actual frontend URLs
                        )
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH") // Allow PATCH method
                        .AllowAnyMethod()
                        .AllowCredentials(); // Required if frontend is on a different domain
                });
            });

            return services;
        }
    }
}
