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
    /// Configuration class for the UserAchievement entity in Entity Framework Core
    /// </summary>
    /// <remarks>
    /// Defines the database schema and relationships for the UserAchievement join table,
    /// which represents the many-to-many relationship between Users and Achievements
    /// </remarks>
    internal class UserAchievementConfig : IEntityTypeConfiguration<UserAchievement>
    {
        /// <summary>
        /// Configures the entity properties and relationships for UserAchievement
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the UserAchievement entity</param>
        public void Configure(EntityTypeBuilder<UserAchievement> builder)
        {
            // Configure primary key
            builder.HasKey(ua => ua.Id);

            // Create unique index to prevent duplicate user-achievement relationships
            builder.HasIndex(ua => new { ua.UserId, ua.AchievementId })
                  .IsUnique();
        }
    }
}