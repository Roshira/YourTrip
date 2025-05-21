using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities.Saved;

namespace YourTrips.Infrastructure.Data.Configurations.SavedItemsConfigs
{
    internal class SavedConfig
    {
        internal void ConfigureSavedEntities(ModelBuilder builder)
        {
            // General config for all Saved.
            void ConfigureSavedEntity<T>(string externalIdName) where T : class
            {
                builder.Entity<T>(b =>
                {
                    b.Property("Id").ValueGeneratedOnAdd();
                    b.Property(externalIdName).IsRequired();
                    b.Property("SavedAt").HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
                });
            }
            ConfigureSavedEntity<SavedHotel>("HotelJson");
            ConfigureSavedEntity<SavedFlights>("FlightsJson");
            ConfigureSavedEntity<SavedPlaces>("ExternalPlacesId");
            ConfigureSavedEntity<SavedTrainTrips>("ExternalTrainId");
            ConfigureSavedEntity<SavedBlaBlaCarTrips>("ExternalBlaBlaCarId");
        }
    }
}
