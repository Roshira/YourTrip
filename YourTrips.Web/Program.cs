using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using YourTrips.Application;
using YourTrips.Core.Entities;
using YourTrips.Core.Interfaces.Services;
using YourTrips.Infrastructure;
using YourTrips.Infrastructure.Data;
using YourTrips.Infrastructure.Services;
using YourTrips.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:7271");
builder.Services
    .AddWeb(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
