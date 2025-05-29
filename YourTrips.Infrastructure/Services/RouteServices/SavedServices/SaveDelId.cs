using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.Interfaces.Routes.Saved;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.RouteServices.SavedServices
{
    public class SaveDelId : ISaveDelId
    {
        private readonly YourTripsDbContext _context;

        public SaveDelId(YourTripsDbContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> SaveIdAsync<T>(string externalId, int routeId)
           where T : class, ISavedEntity, new()
        {
            // Перевірка існування маршруту
            var routeExists = await _context.Routes.AnyAsync(r => r.Id == routeId);
            if (!routeExists)
            {
                return ResultDto.Fail("Маршрут не знайдено.");
            }

            var entity = new T
            {
                RouteId = routeId,
                SavedAt = DateTime.UtcNow
            };

            var idProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.StartsWith("External", StringComparison.OrdinalIgnoreCase) &&
                                     p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) &&
                                     p.PropertyType == typeof(string));

            if (idProp == null)
            {
                return ResultDto.Fail("External ID property not found.");
            }

            idProp.SetValue(entity, externalId);

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return ResultDto.Success("Saved successfully.");
        }

    }
}
