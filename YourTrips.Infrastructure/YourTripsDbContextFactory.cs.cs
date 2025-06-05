using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using YourTrips.Infrastructure.Data;

namespace YourTrips.Infrastructure
{
    /// <summary>
    /// Factory to create instances of YourTripsDbContext at design time.
    /// This is used by EF Core tools (like migrations).
    /// </summary>
    public class YourTripsDbContextFactory : IDesignTimeDbContextFactory<YourTripsDbContext>
    {
        // Connection string is hidden here. Ideally, load it from environment variables or configuration.
        private readonly string _connectionString;

        /// <summary>
        /// Default constructor.
        /// You can initialize _connectionString here or load it from environment/configuration.
        /// </summary>
        public YourTripsDbContextFactory()
        {
            // For security, do NOT hardcode your connection string here.
            // Example: read from environment variable:
            _connectionString = Environment.GetEnvironmentVariable("YOURTRIPS_CONNECTION_STRING")
                                ?? throw new InvalidOperationException("Connection string not found.");
        }

        /// <summary>
        /// Creates a new instance of YourTripsDbContext with configured options.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Instance of YourTripsDbContext.</returns>
        public YourTripsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourTripsDbContext>();

            // Configure DbContext to use PostgreSQL with the connection string.
            optionsBuilder.UseNpgsql(_connectionString);

            return new YourTripsDbContext(optionsBuilder.Options);
        }
    }
}
