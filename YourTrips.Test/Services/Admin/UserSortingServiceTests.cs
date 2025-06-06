using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Core.Entities;
using YourTrips.Infrastructure.Data;
using YourTrips.Infrastructure.Services.Admin.Data;

namespace YourTrips.Test.Services.Admin
{
    /// <summary>
    /// Contains unit tests for <see cref="UserSortingService"/>, which is responsible for sorting users by the number of routes they have.
    /// </summary>
    [TestFixture]
    public class UserSortingServiceTests
    {
        private YourTripsDbContext _context;
        private UserSortingService _service;

        /// <summary>
        /// Initializes a new in-memory database and seeds test data before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<YourTripsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            _context = new YourTripsDbContext(options);

            SeedData(_context);
            _service = new UserSortingService(_context);
        }

        /// <summary>
        /// Disposes the in-memory database context after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Seeds test data into the in-memory database.
        /// </summary>
        /// <param name="context">The database context to seed data into.</param>
        private void SeedData(YourTripsDbContext context)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "User1",
                    Email = "u1@test.com",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Route A" },
                        new Route { Name = "Route B" }
                    }
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "User2",
                    Email = "u2@test.com",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Route C" }
                    }
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "User3",
                    Email = "u3@test.com",
                    Routes = new List<Route>
                    {
                        new Route { Name = "Route D" },
                        new Route { Name = "Route E" },
                        new Route { Name = "Route F" }
                    }
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "User4",
                    Email = "u4@test.com",
                    Routes = new List<Route>() // No routes
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        /// <summary>
        /// Verifies that the results from sequential and parallel sorting are equal in content and order.
        /// </summary>
        [Test]
        public async Task CompareSortingMethods_ShouldReturnSameSortedResult()
        {
            var result = await _service.CompareSortingMethods();

            var sequential = result.SequentialResult.UserRoutes;
            var parallel = result.ParallelResult.UserRoutes;

            Assert.That(sequential.Count, Is.EqualTo(parallel.Count));

            for (int i = 0; i < sequential.Count; i++)
            {
                Assert.That(sequential[i].User.Id, Is.EqualTo(parallel[i].User.Id));
                Assert.That(sequential[i].RoutesCount, Is.EqualTo(parallel[i].RoutesCount));
            }
        }

        /// <summary>
        /// Ensures that the sequential sorting method sorts users by route count in descending order.
        /// </summary>
        [Test]
        public async Task SequentialSort_ShouldSortByRouteCountDescending()
        {
            var result = await _service.CompareSortingMethods();
            var sorted = result.SequentialResult.UserRoutes;

            for (int i = 1; i < sorted.Count; i++)
            {
                Assert.That(sorted[i - 1].RoutesCount, Is.GreaterThanOrEqualTo(sorted[i].RoutesCount));
            }
        }

        /// <summary>
        /// Ensures that the parallel sorting method sorts users by route count in descending order.
        /// </summary>
        [Test]
        public async Task ParallelSort_ShouldSortByRouteCountDescending()
        {
            var result = await _service.CompareSortingMethods();
            var sorted = result.ParallelResult.UserRoutes;

            for (int i = 1; i < sorted.Count; i++)
            {
                Assert.That(sorted[i - 1].RoutesCount, Is.GreaterThanOrEqualTo(sorted[i].RoutesCount));
            }
        }
    }
}
