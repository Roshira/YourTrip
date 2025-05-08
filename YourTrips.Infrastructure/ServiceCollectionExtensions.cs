using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTrips.Application.Common;
using YourTrips.Core.Entities;
using YourTrips.Application.Interfaces;
using YourTrips.Core.Interfaces.Services;
using YourTrips.Infrastructure.Data;
using YourTrips.Infrastructure.Services.AuthServices;

namespace YourTrips.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
           

            services.AddDbContext<YourTripsDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<YourTripsDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));
            services.AddScoped<IAppEmailSender, EmailSender>();



            return services;
        }
    }
}
