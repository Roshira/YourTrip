using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure.Configurations
{
    /// <summary>
    /// Configures the entity framework model for the <see cref="Route"/> entity.
    /// </summary>
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        /// <summary>
        /// Configures the properties and relationships of the <see cref="Route"/> entity.
        /// </summary>
        /// <param name="builder">The builder to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Configure Name property with max length 200
            builder.Property(r => r.Name)
                   .HasMaxLength(200);

            // Configure Review property with max length 1000
            builder.Property(r => r.Review)
                   .HasMaxLength(1000);

            // Configure Rating property with decimal type precision (3,2)
            builder.Property(r => r.Rating)
                   .HasColumnType("decimal(3,2)");

            // Configure relationship: Route has one User, User has many Routes
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Routes)
                   .HasForeignKey(r => r.UserId);

            // Configure one-to-many relationships with saved entities
            builder.HasMany(r => r.SavedHotels)
                   .WithOne(h => h.Route)
                   .HasForeignKey(h => h.RouteId);

            builder.HasMany(r => r.SavedFlights)
                   .WithOne(f => f.Route)
                   .HasForeignKey(f => f.RouteId);

            builder.HasMany(r => r.SavedPlaces)
                   .WithOne(p => p.Route)
                   .HasForeignKey(p => p.RouteId);
        }
    }
}
