using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.DTOs;
using YourTrips.Application.DTOs.Route.Saved;
using YourTrips.Application.Interfaces.RapidBooking;
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

        public async Task<ResultDto> SaveJsonAsync<T>(SavedDto savedDto)
     where T : class, ISavedEntity, new()
        {
            // Перевірка існування маршруту (без прив'язки до юзера)
            var routeExists = await _context.Routes.AnyAsync(r => r.Id == savedDto.RouteId);

            if (!routeExists)
            {
                return ResultDto.Fail("Route not found.");
            }

            var entity = new T
            {
                RouteId = savedDto.RouteId,
                SavedAt = DateTime.UtcNow
            };

            var jsonProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.EndsWith("Json", StringComparison.OrdinalIgnoreCase));

            if (jsonProp == null)
            {
                return ResultDto.Fail("Json property not found.");
            }

            jsonProp?.SetValue(entity, savedDto.Json);

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return ResultDto.Success("Saved successfully.");
        }
        public async Task<ResultDto> DeleteJsonAsync<T>(DeleteSavedDto delete)
       where T : class, ISavedEntity, new()
        {
            // Перевірка чи існує маршрут
            var routeExists = await _context.Routes.AnyAsync(r => r.Id == delete.RouteId);
            if (!routeExists)
            {
                return ResultDto.Fail("Route not found.");
            }

            // Пошук об'єкта, який потрібно видалити
            var saved = await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == delete.Id && x.RouteId == delete.RouteId);

            if (saved == null)
            {
                return ResultDto.Fail("Saved item not found.");
            }

            _context.Set<T>().Remove(saved);
            await _context.SaveChangesAsync();

            return ResultDto.Success("Deleted successfully.");
        }

    }
}
