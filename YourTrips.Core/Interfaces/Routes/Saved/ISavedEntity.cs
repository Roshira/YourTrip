
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    public interface ISavedEntity
    {
        public int Id { get; set; }
        public int RouteId { get; set; }  // Зв'язок з маршрутом, а не з User
        public Route Route { get; set; }
        DateTime SavedAt { get; set; }  // Для часу створення

    }
}
