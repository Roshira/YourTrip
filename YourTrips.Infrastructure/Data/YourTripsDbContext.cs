using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Entities.Saved;
using YourTrips.Infrastructure.Data.Configurations.SavedItemsConfigs;
using YourTrips.Infrastructure.Configurations;

namespace YourTrips.Infrastructure.Data
{
    /// <summary>
    /// The Entity Framework Core database context for the YourTrips application.
    /// Inherits from IdentityDbContext to include ASP.NET Core Identity support.
    /// </summary>
    public class YourTripsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YourTripsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a DbContext.</param>
        public YourTripsDbContext(DbContextOptions<YourTripsDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the achievements table.
        /// </summary>
        public DbSet<Achievement> Achievements { get; set; }

        /// <summary>
        /// Gets or sets the user achievements table.
        /// </summary>
        public DbSet<UserAchievement> UserAchievements { get; set; }

        /// <summary>
        /// Gets or sets the routes saved by users.
        /// </summary>
        public DbSet<Route> Routes { get; set; }

        /// <summary>
        /// Gets or sets the saved flights.
        /// </summary>
        public DbSet<SavedFlights> SavedFlights { get; set; }

        /// <summary>
        /// Gets or sets the saved hotels.
        /// </summary>
        public DbSet<SavedHotel> SavedHotels { get; set; }

        /// <summary>
        /// Gets or sets the saved places.
        /// </summary>
        public DbSet<SavedPlaces> SavedPlaces { get; set; }

        // Add additional DbSet properties for other entities here

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in DbSet properties on this context.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Call base method to configure Identity models
            base.OnModelCreating(builder);

            // Apply all IEntityTypeConfiguration implementations in the assembly
            builder.ApplyConfigurationsFromAssembly(typeof(YourTripsDbContext).Assembly);

            // Apply specific entity configurations
            builder.ApplyConfiguration(new RouteConfiguration());

            // Configure saved entities using external saved config
            var savedConfig = new SavedConfig();
            savedConfig.ConfigureSavedEntities(builder);
        }
    }
}
