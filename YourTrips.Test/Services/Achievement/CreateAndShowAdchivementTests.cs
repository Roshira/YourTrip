using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using YourTrips.Core.Entities.Achievement;
using YourTrips.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Core.Entities;

namespace YourTrips.Core.Tests.Services
{
    /// <summary>
    /// Unit tests for <see cref="CreateAndShowAdchivement"/>, which manages granting, checking,
    /// and retrieving achievements for users.
    /// </summary>
    [TestFixture]
    public class CreateAndShowAdchivementTests
    {
        private YourTripsDbContext _context;
        private CreateAndShowAdchivement _service;

        /// <summary>
        /// Sets up an in-memory database and seeds test data before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<YourTripsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new YourTripsDbContext(options);
            SeedTestData();
            _service = new CreateAndShowAdchivement(_context);
        }

        /// <summary>
        /// Cleans up the in-memory database after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Seeds test data: achievements, one test user, and five completed trips for that user.
        /// </summary>
        private void SeedTestData()
        {
            var achievements = new List<Achievement>
            {
                new Achievement { Id = 1, Name = "First Trip", Description = "Complete your first trip", IconUrl = "icon1.png" },
                new Achievement { Id = 2, Name = "Explorer", Description = "Complete 3 trips", IconUrl = "icon2.png" },
                new Achievement { Id = 3, Name = "Traveler", Description = "Complete 5 trips", IconUrl = "icon3.png" },
                new Achievement { Id = 4, Name = "Globetrotter", Description = "Complete 10 trips", IconUrl = "icon4.png" }
            };
            _context.Achievements.AddRange(achievements);

            var user = new User { Id = Guid.NewGuid(), UserName = "testuser" };
            _context.Users.Add(user);

            _context.Routes.AddRange(
                Enumerable.Range(1, 5).Select(i => new Route
                {
                    UserId = user.Id,
                    Name = $"Test Trip {i}",
                    IsCompleted = true
                })
            );

            _context.SaveChanges();
        }

        /// <summary>
        /// Verifies that no achievements are returned for a user with no earned achievements.
        /// </summary>
        [Test]
        public async Task GetUserAchievementsAsync_ReturnsEmptyList_WhenUserHasNoAchievements()
        {
            var newUserId = Guid.NewGuid();
            var result = await _service.GetUserAchievementsAsync(newUserId);
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Verifies that user achievements are returned in chronological order by EarnedAt.
        /// </summary>
        [Test]
        public async Task GetUserAchievementsAsync_ReturnsAchievements_OrderedByEarnedAt()
        {
            var userId = _context.Users.First().Id;

            await _service.GrantAchievementIfNotExists(userId, 3);
            await Task.Delay(100);
            await _service.GrantAchievementIfNotExists(userId, 1);
            await Task.Delay(100);
            await _service.GrantAchievementIfNotExists(userId, 2);

            var result = await _service.GetUserAchievementsAsync(userId);

            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[0].EarnedAt, Is.LessThanOrEqualTo(result[1].EarnedAt));
            Assert.That(result[1].EarnedAt, Is.LessThanOrEqualTo(result[2].EarnedAt));
        }

        /// <summary>
        /// Verifies that calling CheckAchievementAsync triggers the trip-based achievement logic.
        /// </summary>
        [Test]
        public async Task CheckAchievementAsync_TriggersTripChecking()
        {
            var userId = _context.Users.First().Id;
            await _service.CheckAchievementAsync(userId);
            var achievements = await _service.GetUserAchievementsAsync(userId);
            Assert.That(achievements, Has.Count.EqualTo(3));
        }

        /// <summary>
        /// Tests that trip-based achievements are correctly granted when thresholds are met.
        /// </summary>
        [Test]
        public async Task CheckTripsAsync_GrantsAchievements_WhenThresholdsAreMet()
        {
            var userId = _context.Users.First().Id;
            await _service.CheckTripsAsync(userId);
            var achievements = await _service.GetUserAchievementsAsync(userId);

            Assert.That(achievements, Has.Count.EqualTo(3));
            Assert.That(achievements.Any(a => a.Id == 1), Is.True);
            Assert.That(achievements.Any(a => a.Id == 2), Is.True);
            Assert.That(achievements.Any(a => a.Id == 3), Is.True);
            Assert.That(achievements.Any(a => a.Id == 4), Is.False);
        }

        /// <summary>
        /// Verifies that achievements are removed if a user no longer meets the requirements (e.g., fewer completed trips).
        /// </summary>
        [Test]
        public async Task CheckTripsAsync_RemovesAchievements_WhenThresholdsAreNoLongerMet()
        {
            var userId = _context.Users.First().Id;

            await _service.GrantAchievementIfNotExists(userId, 1);
            await _service.GrantAchievementIfNotExists(userId, 2);
            await _service.GrantAchievementIfNotExists(userId, 3);
            await _service.GrantAchievementIfNotExists(userId, 4);

            var trips = _context.Routes.Where(r => r.UserId == userId).Take(3).ToList();
            trips.ForEach(t => t.IsCompleted = false);
            _context.SaveChanges();

            await _service.CheckTripsAsync(userId);
            var achievements = await _service.GetUserAchievementsAsync(userId);

            Assert.That(achievements, Has.Count.EqualTo(1));
            Assert.That(achievements.Any(a => a.Id == 1), Is.True);
            Assert.That(achievements.Any(a => a.Id == 2), Is.False);
            Assert.That(achievements.Any(a => a.Id == 3), Is.False);
            Assert.That(achievements.Any(a => a.Id == 4), Is.False);
        }

        /// <summary>
        /// Tests that an achievement is granted if not already assigned to a user.
        /// </summary>
        [Test]
        public async Task GrantAchievementIfNotExists_AddsAchievement_WhenNotAlreadyGranted()
        {
            var userId = _context.Users.First().Id;
            var achievementId = 1;

            await _service.GrantAchievementIfNotExists(userId, achievementId);

            var exists = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);
            Assert.That(exists, Is.True);
        }

        /// <summary>
        /// Ensures that granting the same achievement twice does not create duplicates.
        /// </summary>
        [Test]
        public async Task GrantAchievementIfNotExists_DoesNotAddDuplicate_WhenAlreadyGranted()
        {
            var userId = _context.Users.First().Id;
            var achievementId = 1;
            await _service.GrantAchievementIfNotExists(userId, achievementId);
            var initialCount = await _context.UserAchievements.CountAsync();

            await _service.GrantAchievementIfNotExists(userId, achievementId);
            var finalCount = await _context.UserAchievements.CountAsync();

            Assert.That(finalCount, Is.EqualTo(initialCount));
        }

        /// <summary>
        /// Ensures that an existing achievement can be removed from a user.
        /// </summary>
        [Test]
        public async Task DeleteAchievement_RemovesAchievement_WhenExists()
        {
            var userId = _context.Users.First().Id;
            var achievementId = 1;
            await _service.GrantAchievementIfNotExists(userId, achievementId);

            await _service.DeleteAchievement(userId, achievementId);

            var exists = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);
            Assert.That(exists, Is.False);
        }

        /// <summary>
        /// Ensures that deleting a non-existent achievement does not throw or affect existing data.
        /// </summary>
        [Test]
        public async Task DeleteAchievement_DoesNothing_WhenAchievementDoesNotExist()
        {
            var userId = _context.Users.First().Id;
            var achievementId = 99;
            var initialCount = await _context.UserAchievements.CountAsync();

            await _service.DeleteAchievement(userId, achievementId);
            var finalCount = await _context.UserAchievements.CountAsync();

            Assert.That(finalCount, Is.EqualTo(initialCount));
        }
    }
}
