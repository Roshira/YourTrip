using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure
{
    public class YourTripsDbContextFactory : IDesignTimeDbContextFactory<YourTripsDbContext>
    {
        public YourTripsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourTripsDbContext>();

            var connectionString = "Host=localhost;Port=5432;Database=YourTrips;Username=YourTrips_User;Password=123qwe123qwe";
            optionsBuilder.UseNpgsql(connectionString);

            return new YourTripsDbContext(optionsBuilder.Options);
        }
    }
}
