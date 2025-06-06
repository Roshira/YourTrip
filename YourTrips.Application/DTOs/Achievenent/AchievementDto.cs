using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.Achievenent
{
    /// <summary>
    /// Just achievement DTO
    /// </summary>
    public class AchievementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public DateTime EarnedAt { get; set; }
    }
}
