using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Core.Entities.Saved;
using YourTrips.Core.Entities;
using YourTrips.Infrastructure.Data.Configurations.SavedItemsConfigs;
using System.Reflection.Emit;
using YourTrips.Infrastructure.Configurations;


namespace YourTrips.Infrastructure.Data
{
    public class YourTripsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public YourTripsDbContext(DbContextOptions<YourTripsDbContext> options)
            : base(options)
        {
        }

        // Add DbSet for all entities
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        // Saved element
        public DbSet<Route> Routes { get; set; }
        public DbSet<SavedFlights> SavedFlights { get; set; }
        public DbSet<SavedHotel> SavedHotels { get; set; }
        public DbSet<SavedPlaces> SavedPlaces { get; set; }

        // other entities

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // !!!! for Identity
            builder.ApplyConfigurationsFromAssembly(typeof(YourTripsDbContext).Assembly);

            builder.ApplyConfiguration(new RouteConfiguration());
            var savedConfig = new SavedConfig();
            savedConfig.ConfigureSavedEntities(builder);
        }


    }
}
