using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configures the entity framework model for the <see cref="User"/> entity.
    /// </summary>
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the <see cref="User"/> entity's relationships and indexes.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure one-to-many relationship: User has many Routes
            builder.HasMany(u => u.Routes)
                   .WithOne(route => route.User)
                   .HasForeignKey(route => route.UserId);

            // Configure one-to-many relationship: User has many UserAchievements
            builder.HasMany(u => u.UserAchievements)
                   .WithOne(ua => ua.User)
                   .HasForeignKey(ua => ua.UserId);

            // Create a non-unique index on UserName for faster queries (but allow duplicates)
            builder.HasIndex(u => u.UserName).IsUnique(false);
        }
    }
}
