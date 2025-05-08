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
using YourTrips.Infrastructure.Data.Configurations.SavedItemsConfigs;


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
        public DbSet<SavedBlaBlaCarTrips> SavedBlaBlaCarTrips { get; set; }
        public DbSet<SavedFlights> SavedFlights { get; set; }
        public DbSet<SavedHotel> SavedHotels { get; set; }
        public DbSet<SavedPlaces> SavedPlaces { get; set; }
        public DbSet<SavedTrainTrips> SavedTrainTrips { get; set; }

        // other entities
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<TripHistory> TripHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // !!!! for Identity
            builder.ApplyConfigurationsFromAssembly(typeof(YourTripsDbContext).Assembly);

            var savedConfig = new SavedConfig();
            savedConfig.ConfigureSavedEntities(builder);
        }

       
    }
}
