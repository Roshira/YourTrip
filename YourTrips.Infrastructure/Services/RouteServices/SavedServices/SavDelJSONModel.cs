using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Core.DTOs;
using YourTrips.Core.Interfaces.Routes.Saved;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.RouteServices.SavedServices
{
    public class SavDelJSONModel : ISavDelJSONModel
    {
        private readonly YourTripsDbContext _context;

        public SavDelJSONModel(YourTripsDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> SaveJsonAsync<T>(string json, int routeId)
     where T : class, ISavedEntity, new()
        {
            // Перевірка існування маршруту (без прив'язки до юзера)
            var routeExists = await _context.Routes.AnyAsync(r => r.Id == routeId);

            if (!routeExists)
            {
                return ResultDto.Fail("Route not found.");
            }

            var entity = new T
            {
                RouteId = routeId,
                SavedAt = DateTime.UtcNow
            };

            var jsonProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.EndsWith("Json", StringComparison.OrdinalIgnoreCase));

            if (jsonProp == null)
            {
                return ResultDto.Fail("Json property not found.");
            }

            jsonProp?.SetValue(entity, json);

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return ResultDto.Success("Saved successfully.");
        }
    }
}
