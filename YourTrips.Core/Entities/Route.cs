using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using YourTrips.Core.Entities.Saved;

namespace YourTrips.Core.Entities
{
    /// <summary>
    /// Represents a travel route created by a user.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Primary key of the route.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the user who owns the route.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property to the user who owns this route.
        /// Marked to be ignored during JSON serialization.
        /// </summary>
        [JsonIgnore]
        public User User { get; set; }

        /// <summary>
        /// Optional name of the route.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether the route has been completed.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// Optional user review or description of the route.
        /// </summary>
        public string? Review { get; set; }

        /// <summary>
        /// Optional rating of the route, typically from 1 to 5.
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        /// URL of an image representing the route.
        /// Defaults to a placeholder image URL.
        /// </summary>
        public string ImageUrl { get; set; } = "https://videos.openai.com/vg-assets/assets%2Ftask_01jwge5h8geyntwn83hfk36f35%2F1748603511_img_0.webp?st=2025-05-30T10%3A04%3A39Z&se=2025-06-05T11%3A04%3A39Z&sks=b&skt=2025-05-30T10%3A04%3A39Z&ske=2025-06-05T11%3A04%3A39Z&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skoid=3d249c53-07fa-4ba4-9b65-0bf8eb4ea46a&skv=2019-02-02&sv=2018-11-09&sr=b&sp=r&spr=https%2Chttp&sig=ZiLiyj4Zpihm0lvdXIG8nh%2FGgB%2FHjFhekxGsEU1YeuU%3D&az=oaivgprodscus";

        /// <summary>
        /// The UTC date and time when the route was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Collection of saved hotels associated with the route.
        /// </summary>
        public ICollection<SavedHotel> SavedHotels { get; set; }

        /// <summary>
        /// Collection of saved flights associated with the route.
        /// </summary>
        public ICollection<SavedFlights> SavedFlights { get; set; }

        /// <summary>
        /// Collection of saved places associated with the route.
        /// </summary>
        public ICollection<SavedPlaces> SavedPlaces { get; set; }
    }
}
