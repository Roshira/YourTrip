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
            builder.HasMany(u => u.Routes).WithOne(sh => sh.User).HasForeignKey(sh => sh.UserId);
            builder.HasMany(u => u.UserAchievements).WithOne(ua => ua.User).HasForeignKey(ua => ua.UserId);
            builder.HasIndex(u => u.UserName).IsUnique(false);
        }
    }
}
