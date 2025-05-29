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
using YourTrips.Application.Common; // Ваш Application Common
using YourTrips.Core.Entities;
using YourTrips.Application.Interfaces; // Ваш Application Interfaces (для IAppEmailSender)
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

namespace YourTrips.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<YourTripsDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddAuthorization(options =>
            {
                // options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            services.AddIdentityApiEndpoints<User>(options =>
            {
                // setting Identity (вимоги до паролю, блокування і т.д.)
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Можна додати налаштування паролю тут
            })
                .AddEntityFrameworkStores<YourTripsDbContext>()
                .AddDefaultTokenProviders(); // Додає провай
            services.AddHttpClient<IFlightSearchService, AmadeusFlightSearchService>(client =>
            {
                client.BaseAddress = new Uri(config["Amadeus:BaseUrl"]);
            });
            services.AddHttpClient<IAmadeusLocationService, AmadeusLocationService>(client =>
            {
                client.BaseAddress = new Uri(config["Amadeus:BaseUrl"]);
            });
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ISavDelJSONModel, SavDelJSONModel>();
            services.AddScoped<IRewriteUserName, RewriteUserName>();
            services.AddHttpClient<IGooglePlacesService ,GooglePlacesService>();
            services.AddScoped<IBookingDescribeService, BookingDescribeService>();
            services.AddScoped<IAmadeusAuthService, AmadeusAuthService>();
            services.AddScoped<ISuggestAmadeusService, SuggestAmadeusService>();
            services.AddHttpClient<ISuggestBookingService, SuggestBookingService>();
            services.AddScoped<IBookingApiService, BookingApiService>();
            // Реєстрація ваших сервісів
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));
            services.AddScoped<IAppEmailSender, EmailSender>();
            services.AddScoped<SignInManager<User>, CustomSignInManager>();
            return services;
        }
    }
}