using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Achievement;

namespace YourTrips.Infrastructure.Data.Configurations
{
    public class DestinationsConfig : IEntityTypeConfiguration<Destinations>
    {
        public void Configure(EntityTypeBuilder<Destinations> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.NameCountry).HasMaxLength(40).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(100).IsRequired();
        }
    }
}
