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
    public class AchievementConfig : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(a => a.UserAchievements)
                   .WithOne(ua => ua.Achievement)
                   .HasForeignKey(ua => ua.AchievementId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
