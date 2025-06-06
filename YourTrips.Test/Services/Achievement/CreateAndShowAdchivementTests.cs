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
    [TestFixture]
    public class CreateAndShowAdchivementTests
    {
        private YourTripsDbContext _context;
        private CreateAndShowAdchivement _service;

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

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        private void SeedTestData()
        {
            // Add test achievements
            var achievements = new List<Achievement>
    {
        new Achievement { Id = 1, Name = "First Trip", Description = "Complete your first trip", IconUrl = "icon1.png" },
        new Achievement { Id = 2, Name = "Explorer", Description = "Complete 3 trips", IconUrl = "icon2.png" },
        new Achievement { Id = 3, Name = "Traveler", Description = "Complete 5 trips", IconUrl = "icon3.png" },
        new Achievement { Id = 4, Name = "Globetrotter", Description = "Complete 10 trips", IconUrl = "icon4.png" }
    };
            _context.Achievements.AddRange(achievements);

            // Add test user
            var user = new User { Id = Guid.NewGuid(), UserName = "testuser" };
            _context.Users.Add(user);

            // Add some completed trips for the user - ТЕПЕР ІЗ ВКАЗАННЯМ ОБОВ'ЯЗКОВИХ ПОЛІВ
            _context.Routes.AddRange(
                Enumerable.Range(1, 5).Select(i => new Route
                {
                    UserId = user.Id,
                    Name = $"Test Trip {i}", // Додали обов'язкове поле Name
                    IsCompleted = true,
                    // Додайте інші обов'язкові поля, якщо вони є
                })
            );

            _context.SaveChanges();
        }

        [Test]
        public async Task GetUserAchievementsAsync_ReturnsEmptyList_WhenUserHasNoAchievements()
        {
            // Arrange
            var newUserId = Guid.NewGuid();

            // Act
            var result = await _service.GetUserAchievementsAsync(newUserId);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetUserAchievementsAsync_ReturnsAchievements_OrderedByEarnedAt()
        {
            // Arrange
            var userId = _context.Users.First().Id;

            // Grant some achievements in reverse order
            await _service.GrantAchievementIfNotExists(userId, 3);
            await Task.Delay(100); // Ensure different timestamps
            await _service.GrantAchievementIfNotExists(userId, 1);
            await Task.Delay(100);
            await _service.GrantAchievementIfNotExists(userId, 2);

            // Act
            var result = await _service.GetUserAchievementsAsync(userId);

            // Assert
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result[0].EarnedAt, Is.LessThanOrEqualTo(result[1].EarnedAt));
            Assert.That(result[1].EarnedAt, Is.LessThanOrEqualTo(result[2].EarnedAt));
        }

        [Test]
        public async Task CheckAchievementAsync_TriggersTripChecking()
        {
            // Arrange
            var userId = _context.Users.First().Id;

            // Act
            await _service.CheckAchievementAsync(userId);

            // Assert - Verify that appropriate achievements were granted
            var achievements = await _service.GetUserAchievementsAsync(userId);
            Assert.That(achievements, Has.Count.EqualTo(3)); // Should have achievements for 1, 3, and 5 trips
        }

        [Test]
        public async Task CheckTripsAsync_GrantsAchievements_WhenThresholdsAreMet()
        {
            // Arrange
            var userId = _context.Users.First().Id;

            // Act
            await _service.CheckTripsAsync(userId);

            // Assert
            var achievements = await _service.GetUserAchievementsAsync(userId);
            Assert.That(achievements, Has.Count.EqualTo(3)); // For 5 completed trips, should have achievements 1, 2, and 3
            Assert.That(achievements.Any(a => a.Id == 1), Is.True);
            Assert.That(achievements.Any(a => a.Id == 2), Is.True);
            Assert.That(achievements.Any(a => a.Id == 3), Is.True);
            Assert.That(achievements.Any(a => a.Id == 4), Is.False); // Should not have the 10-trip achievement
        }

        [Test]
        public async Task CheckTripsAsync_RemovesAchievements_WhenThresholdsAreNoLongerMet()
        {
            // Arrange
            var userId = _context.Users.First().Id;

            // First grant all achievements
            await _service.GrantAchievementIfNotExists(userId, 1);
            await _service.GrantAchievementIfNotExists(userId, 2);
            await _service.GrantAchievementIfNotExists(userId, 3);
            await _service.GrantAchievementIfNotExists(userId, 4);

            // Remove some trips (set IsCompleted to false for some)
            var trips = _context.Routes.Where(r => r.UserId == userId).Take(3).ToList();
            trips.ForEach(t => t.IsCompleted = false);
            _context.SaveChanges();

            // Act
            await _service.CheckTripsAsync(userId);

            // Assert
            var achievements = await _service.GetUserAchievementsAsync(userId);
            Assert.That(achievements, Has.Count.EqualTo(1)); // Now only 2 completed trips (should only keep achievement 1)
            Assert.That(achievements.Any(a => a.Id == 1), Is.True);
            Assert.That(achievements.Any(a => a.Id == 2), Is.False); // Should be removed
            Assert.That(achievements.Any(a => a.Id == 3), Is.False); // Should be removed
            Assert.That(achievements.Any(a => a.Id == 4), Is.False); // Should be removed
        }

        [Test]
        public async Task GrantAchievementIfNotExists_AddsAchievement_WhenNotAlreadyGranted()
        {
            // Arrange
            var userId = _context.Users.First().Id;
            var achievementId = 1;

            // Act
            await _service.GrantAchievementIfNotExists(userId, achievementId);

            // Assert
            var exists = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);
            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task GrantAchievementIfNotExists_DoesNotAddDuplicate_WhenAlreadyGranted()
        {
            // Arrange
            var userId = _context.Users.First().Id;
            var achievementId = 1;
            await _service.GrantAchievementIfNotExists(userId, achievementId);
            var initialCount = await _context.UserAchievements.CountAsync();

            // Act
            await _service.GrantAchievementIfNotExists(userId, achievementId);

            // Assert
            var finalCount = await _context.UserAchievements.CountAsync();
            Assert.That(finalCount, Is.EqualTo(initialCount));
        }

        [Test]
        public async Task DeleteAchievement_RemovesAchievement_WhenExists()
        {
            // Arrange
            var userId = _context.Users.First().Id;
            var achievementId = 1;
            await _service.GrantAchievementIfNotExists(userId, achievementId);

            // Act
            await _service.DeleteAchievement(userId, achievementId);

            // Assert
            var exists = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);
            Assert.That(exists, Is.False);
        }

        [Test]
        public async Task DeleteAchievement_DoesNothing_WhenAchievementDoesNotExist()
        {
            // Arrange
            var userId = _context.Users.First().Id;
            var achievementId = 99; // Non-existent achievement
            var initialCount = await _context.UserAchievements.CountAsync();

            // Act
            await _service.DeleteAchievement(userId, achievementId);

            // Assert
            var finalCount = await _context.UserAchievements.CountAsync();
            Assert.That(finalCount, Is.EqualTo(initialCount));
        }
    }
}