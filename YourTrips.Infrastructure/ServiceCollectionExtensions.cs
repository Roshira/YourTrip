using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTrips.Application.Common;
using YourTrips.Core.Entities;
using YourTrips.Application.Interfaces;
using YourTrips.Infrastructure.Data;
using YourTrips.Infrastructure.Services.AuthServices;
using YourTrips.Infrastructure.Services;
using YourTrips.Infrastructure.RapidBooking.Services;
using YourTrips.Infrastructure.Services.BookingService;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Infrastructure.Services.Amadeus;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Infrastructure.Services.GoogleMapsServices;
using YourTrips.Application.Interfaces.GoogleMaps;
using YourTrips.Infrastructure.Services.ProfileServices;
using YourTrips.Core.Interfaces;
using YourTrips.Infrastructure.Services.RouteServices.SavedServices;
using YourTrips.Core.Interfaces.Routes.Saved;
using YourTrips.Core.Interfaces.Routes;
using YourTrips.Infrastructure.Services.Routes;
using YourTrips.Infrastructure.Services.RouteServices;
using YourTrips.Core.Interfaces.Achievements;
using YourTrips.Core.Interfaces.Admin;
using YourTrips.Infrastructure.Services.Admin.Data;
using YourTrips.Application.Interfaces.GoogleAI;

namespace YourTrips.Infrastructure
{
    /// <summary>
    /// Provides extension methods to register infrastructure services in the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds infrastructure services, database context, identity, HTTP clients, and other dependencies to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="config">The application configuration to retrieve settings such as connection strings and API URLs.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Configure Entity Framework Core to use PostgreSQL database with connection string from config
            services.AddDbContext<YourTripsDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            // Configure authorization policies if needed (example commented out)
            services.AddAuthorization(options =>
            {
                // Example: options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            // Configure ASP.NET Core Identity with custom options for User entity
            services.AddIdentityApiEndpoints<User>(options =>
            {
                // Require confirmed email account for login
                options.SignIn.RequireConfirmedAccount = true;
                // Ensure emails are unique across users
                options.User.RequireUniqueEmail = true;

                // Lockout settings to prevent brute force attacks
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Additional password requirements can be configured here
            })
            .AddRoles<IdentityRole<Guid>>() // Add support for role management
            .AddEntityFrameworkStores<YourTripsDbContext>() // Use EF Core store for Identity
            .AddDefaultTokenProviders(); // Add token providers for password reset, email confirmation, etc.

            // Register HttpClient for Amadeus flight search API with base address from config
            services.AddHttpClient<IFlightSearchService, AmadeusFlightSearchService>(client =>
            {
                client.BaseAddress = new Uri(config["Amadeus:BaseUrl"]);
            });

            // Register HttpClient for Amadeus location API with base address from config
            services.AddHttpClient<IAmadeusLocationService, AmadeusLocationService>(client =>
            {
                client.BaseAddress = new Uri(config["Amadeus:BaseUrl"]);
            });

            // Register scoped services for application business logic
            services.AddHttpClient<IGoogleAiService, GoogleAiService>();
            services.AddScoped<IRestaurantSorter, ParisRestaurantSorter>();
            services.AddScoped<IParisRestaurants, ParisRestaurants>();
            services.AddScoped<IUserSortingService, UserSortingService>();
            services.AddScoped<ICreateAndShowAdchivement, CreateAndShowAdchivement>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ISavDelJSONModel, SavDelJSONModel>();
            services.AddScoped<IRewriteUserName, RewriteUserName>();
            services.AddHttpClient<IGooglePlacesService, GooglePlacesService>();
            services.AddScoped<IBookingDescribeService, BookingDescribeService>();
            services.AddScoped<IAmadeusAuthService, AmadeusAuthService>();
            services.AddScoped<ISuggestAmadeusService, SuggestAmadeusService>();
            services.AddHttpClient<ISuggestBookingService, SuggestBookingService>();
            services.AddScoped<IBookingApiService, BookingApiService>();

            // Register authentication and email sender services
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));
            services.AddScoped<IAppEmailSender, EmailSender>();

            // Replace default SignInManager with custom implementation
            services.AddScoped<SignInManager<User>, CustomSignInManager>();

            return services;
        }
    }
}
