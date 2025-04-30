using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace YourTrips.Infrastructure.Data.Configurations
{
    public class TripHistoryConfig : IEntityTypeConfiguration<TripHistory>
    {
        public void Configure(EntityTypeBuilder<TripHistory> builder)
        {
            builder.HasKey(th => th.Id);
            builder.Property(th => th.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}