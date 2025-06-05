using Microsoft.EntityFrameworkCore;
using YourTrips.Core.Entities.Saved;

namespace YourTrips.Infrastructure.Data.Configurations.SavedItemsConfigs
{
    /// <summary>
    /// Provides configuration for saved entities in the model builder.
    /// </summary>
    internal class SavedConfig
    {
        /// <summary>
        /// Configures all saved entities with common properties.
        /// </summary>
        /// <param name="builder">The model builder used to configure entity mappings.</param>
        internal void ConfigureSavedEntities(ModelBuilder builder)
        {
            // Local method to configure each saved entity with common properties.
            void ConfigureSavedEntity<T>(string externalIdName) where T : class
            {
                builder.Entity<T>(b =>
                {
                    // Configure Id as auto-generated on add
                    b.Property("Id").ValueGeneratedOnAdd();

                    // External ID property is required (e.g. HotelJson, FlightsJson, PlaceJson)
                    b.Property(externalIdName).IsRequired();

                    // SavedAt defaults to current UTC timestamp
                    b.Property("SavedAt").HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
                });
            }

            // Configure each saved entity type with its respective external ID property name
            ConfigureSavedEntity<SavedHotel>("HotelJson");
            ConfigureSavedEntity<SavedFlights>("FlightsJson");
            ConfigureSavedEntity<SavedPlaces>("PlaceJson");
        }
    }
}
