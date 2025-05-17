using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.RapidBooking;

namespace YourTrips.Application.Interfaces.RapidBooking
{
    public interface ISuggestBookingService
    {
        public Task<IEnumerable<HotelSuggestionDto>> GetSuggestionsAsync(string searchTerm);
    }
}

