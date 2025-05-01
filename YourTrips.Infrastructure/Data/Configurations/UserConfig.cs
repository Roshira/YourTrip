using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;

namespace YourTrips.Infrastructure.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.SavedHotels).WithOne(sh => sh.User).HasForeignKey(sh => sh.UserId);
            builder.HasMany(u => u.SavedFlights).WithOne(sf => sf.User).HasForeignKey(sf => sf.UserId);
            builder.HasMany(u => u.SavedPlaces).WithOne(sp => sp.User).HasForeignKey(sp => sp.UserId);
            builder.HasMany(u => u.SavedBlaBlaCarTrips).WithOne(sb => sb.User).HasForeignKey(sb => sb.UserId);
            builder.HasMany(u => u.SavedTrainTrips).WithOne(st => st.User).HasForeignKey(st => st.UserId);
            builder.HasMany(u => u.TripHistories).WithOne(th => th.User).HasForeignKey(th => th.UserId);
            builder.HasMany(u => u.UserAchievements).WithOne(ua => ua.User).HasForeignKey(ua => ua.UserId);
        }
    }
}
