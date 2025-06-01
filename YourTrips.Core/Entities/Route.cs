using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities.Saved;
using static System.Net.WebRequestMethods;

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
        public string ImageUrl { get; set; } = "https://videos.openai.com/vg-assets/assets%2Ftask_01jwge5h8geyntwn83hfk36f35%2F1748603511_img_0.webp?st=2025-05-30T10%3A04%3A39Z&se=2025-06-05T11%3A04%3A39Z&sks=b&skt=2025-05-30T10%3A04%3A39Z&ske=2025-06-05T11%3A04%3A39Z&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skoid=3d249c53-07fa-4ba4-9b65-0bf8eb4ea46a&skv=2019-02-02&sv=2018-11-09&sr=b&sp=r&spr=https%2Chttp&sig=ZiLiyj4Zpihm0lvdXIG8nh%2FGgB%2FHjFhekxGsEU1YeuU%3D&az=oaivgprodscus";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Колекції збережених елементів
        public ICollection<SavedHotel> SavedHotels { get; set; }
        public ICollection<SavedFlights> SavedFlights { get; set; }
        public ICollection<SavedPlaces> SavedPlaces { get; set; }
    }
}

