using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.UI.Services; // Потрібно, тільки якщо використовуєте UI за замовчуванням
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourTrips.Application.Common; // Ваш Application Common
using YourTrips.Core.Entities;
using YourTrips.Application.Interfaces; // Ваш Application Interfaces (для IAppEmailSender)
using YourTrips.Core.Interfaces.Services; // Ваш Core Interfaces (для IAuthService)
using YourTrips.Infrastructure.Data;
using YourTrips.Infrastructure.Services.AuthServices; // Ваш AuthService та EmailSender

namespace YourTrips.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<YourTripsDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            // --- НАЛАШТУВАННЯ IDENTITY (РЕЄСТРУЄ СЕРВІСИ ТА БАЗОВУ СХЕМУ) ---
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                // Налаштування Identity (вимоги до паролю, блокування і т.д.)
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Можна додати налаштування паролю тут
            })
                .AddEntityFrameworkStores<YourTripsDbContext>()
                .AddDefaultTokenProviders(); // Додає провайдери для токенів (email, password reset)
            // -----------------------------------------------------------

            // --- НАЛАШТУВАННЯ COOKIE, ЯКУ ВИКОРИСТОВУЄ IDENTITY ---
            // Використовуємо ConfigureApplicationCookie для налаштування IdentityConstants.ApplicationScheme
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax; // Рекомендовано для API + SPA
                options.ExpireTimeSpan = TimeSpan.FromDays(14); // Тривалість сесії
                options.SlidingExpiration = true; // Подовжувати сесію при активності
                options.Cookie.Name = "YourTrips.AuthCookie"; // Кастомне ім'я кукі

                // Перевизначаємо поведінку редіректів для API
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToLogout = context => // Обробка редіректу після виходу (опціонально)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    return Task.CompletedTask;
                };
            });
            // -----------------------------------------------------

            // --- ВИДАЛЕНО ЯВНИЙ AddAuthentication ---
            // services.AddAuthentication(IdentityConstants.ApplicationScheme)
            //    .AddIdentityCookies(...); // Цей блок більше НЕ ПОТРІБЕН тут
            // ------------------------------------------

            // Реєстрація Авторизації (правила/політики)
            services.AddAuthorization(options =>
            {
                // options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            // Реєстрація ваших сервісів
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));
            services.AddScoped<IAppEmailSender, EmailSender>();

            return services;
        }
    }
}