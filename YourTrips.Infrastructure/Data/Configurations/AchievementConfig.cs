using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities.Achievement;

namespace YourTrips.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Entity Framework Core configuration for the <see cref="Achievement"/> entity.
    /// Defines the database schema and relationships for achievements.
    /// </summary>
    public class AchievementConfig : IEntityTypeConfiguration<Achievement>
    {
        /// <summary>
        /// Configures the entity model for the Achievement type.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            // Configure the primary key
            builder.HasKey(a => a.Id);

            // Configure the one-to-many relationship with UserAchievement
            builder.HasMany(a => a.UserAchievements)
                   .WithOne(ua => ua.Achievement)
                   .HasForeignKey(ua => ua.AchievementId)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete when an achievement is deleted
        }
    }
}