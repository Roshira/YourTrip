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
    internal class UserAchievementConfig : IEntityTypeConfiguration<UserAchievement>
    {
        public void Configure(EntityTypeBuilder<UserAchievement> builder)
        {
            builder.HasKey(ua => ua.Id);
            builder.HasIndex(ua => new { ua.UserId, ua.AchievementId }).IsUnique();
        }
    }
}
