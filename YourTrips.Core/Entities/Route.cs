using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities.Saved;

namespace YourTrips.Core.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }  // Зв'язок з користувачем

        public string Name { get; set; }  // Назва маршруту (опціонально)
        public bool IsCompleted { get; set; } = false;  // Чи завершений маршрут
        public string? Review { get; set; }  // Відгук про маршрут
        public double? Rating { get; set; }  // Рейтинг (1-5)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Колекції збережених елементів
        public ICollection<SavedHotel> SavedHotels { get; set; }
        public ICollection<SavedFlights> SavedFlights { get; set; }
        public ICollection<SavedPlaces> SavedPlaces { get; set; }
    }
}

