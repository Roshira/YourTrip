using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure.Configurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(200);

            builder.Property(r => r.Review)
                .HasMaxLength(1000);

            builder.Property(r => r.Rating)
                .HasColumnType("decimal(3,2)");

            builder.HasOne(r => r.User)
                .WithMany(u => u.Routes)
                .HasForeignKey(r => r.UserId);

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
