using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using YourTrips.Application;
using YourTrips.Core.Entities;
using YourTrips.Infrastructure;
using YourTrips.Infrastructure.Data;
using YourTrips.Web;

// Create the application builder
var builder = WebApplication.CreateBuilder(args);

// --- URL Configuration ---
// Listen on all network interfaces on port 7271 (HTTP)
// In production, you would typically use a reverse proxy (nginx, IIS) that handles HTTPS
builder.WebHost.UseUrls("https://0.0.0.0:7271");

// --- Service Registration ---
// Chain registration of services from different layers
builder.Services
    .AddWeb(builder.Configuration)      // Adds controllers, Swagger (without JWT) etc. from Web layer
    .AddApplication(builder.Configuration)                   // Adds services from Application layer (if any)
    .AddInfrastructure(builder.Configuration); // Adds DbContext, Identity, Cookie Auth, EmailSender etc. from Infrastructure layer

// Build the application
var app = builder.Build();

// --- HTTP Request Processing Pipeline (Middleware Pipeline) ---
// The order of middleware registration is CRUCIAL!
app.MapIdentityApi<User>();
// Development-specific middleware
if (app.Environment.IsDevelopment())
{
    // OpenAPI/Swagger middleware for API documentation and testing
    // app.MapOpenApi(); // Might not be needed if UseSwaggerUI is used
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "YourTrips API V1");
        // Additional UI settings can be added here
    });
}

// HTTP to HTTPS redirection (recommended for production)
// Can be commented if not using HTTPS locally or behind reverse proxy
app.UseHttpsRedirection();

// Enable routing to determine which endpoint should handle the request
app.UseRouting();

// Apply CORS policy *before* authentication/authorization
app.UseCors("AllowAll");

// Enable authentication middleware (checks cookies)
app.UseAuthentication();

// Enable authorization middleware (checks access rights, e.g. [Authorize])
app.UseAuthorization();

// Map requests to appropriate controller methods
app.MapControllers();

// Run the application
app.Run();