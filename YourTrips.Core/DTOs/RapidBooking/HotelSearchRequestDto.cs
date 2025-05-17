using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking
{ 
        public class HotelSearchRequestDto
        {
            public string Country { get; set; }          // Країна (наприклад: "Ukraine")
            public string City { get; set; }             // Місто (наприклад: "Kyiv")
            public string? HotelName { get; set; }        // Назва готелю (опціонально)
            public DateTime CheckInDate { get; set; }    // Дата заїзду
            public DateTime CheckOutDate { get; set; }   // Дата виїзду
            public int Adults { get; set; } = 2;         // Кількість дорослих (за замовчуванням 2)
            public int Children { get; set; } = 0;       // Кількість дітей
            public decimal? MinPrice { get; set; }       // Мінімальна ціна
            public decimal? MaxPrice { get; set; }       // Максимальна ціна
            public int Rooms { get; set; } = 1;          // Кількість кімнат
        }
    
}

