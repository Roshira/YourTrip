using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Core.DTOs;
using YourTrips.Core.Interfaces.Saved;
using YourTrips.Core.Interfaces.SavedServices;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure.Services.SavedServices
{
    public class SavDelJSONModel : ISavDelJSONModel
    {
        private readonly YourTripsDbContext _context;

        public SavDelJSONModel(YourTripsDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> SaveJsonAsync<T>(Guid userId, string json) where T : class, ISavedEntity, new()
        {
            var entity = new T
            {
                UserId = userId,
                SavedAt = DateTime.UtcNow
            };

            var jsonProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.EndsWith("Json", StringComparison.OrdinalIgnoreCase));

            if (jsonProp == null)
            {
                return ResultDto.Fail("Json property not found.");
            }

            jsonProp.SetValue(entity, json);

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return ResultDto.Success("Saved successfully.");
        }
    }
}
