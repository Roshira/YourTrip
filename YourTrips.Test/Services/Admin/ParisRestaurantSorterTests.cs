using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourTrips.Infrastructure.Services.Admin.Data;
using YourTrips.Core.Interfaces.Admin;
using YourTrips.Application.DTOs.Admin.Data;

namespace YourTrips.Tests.Infrastructure.Services.Admin.Data
{
    [TestFixture]
    public class ParisRestaurantSorterTests
    {
        private Mock<IParisRestaurants> _parisRestaurantsMock;
        private ParisRestaurantSorter _sorter;
        private List<RestaurantDto> _mockRestaurants;

        [SetUp]
        public void SetUp()
        {
            _mockRestaurants = new List<RestaurantDto>
            {
                new RestaurantDto { Name = "A", Rating = 3.5 },
                new RestaurantDto { Name = "B", Rating = 4.7 },
                new RestaurantDto { Name = "C", Rating = 4.2 }
            };

            _parisRestaurantsMock = new Mock<IParisRestaurants>();
            _parisRestaurantsMock.Setup(s => s.GetParisRestaurantsAsync())
                .ReturnsAsync(_mockRestaurants);

            _sorter = new ParisRestaurantSorter(_parisRestaurantsMock.Object);
        }

        [Test]
        public async Task SortSequentiallyAsync_ShouldSortByRatingDescending()
        {
            // Act
            var result = await _sorter.SortSequentiallyAsync(_mockRestaurants);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Rating, Is.EqualTo(4.7));
            Assert.That(result[1].Rating, Is.EqualTo(4.2));
            Assert.That(result[2].Rating, Is.EqualTo(3.5));
        }

        [Test]
        public async Task SortInParallelAsync_ShouldSortByRatingDescending()
        {
            // Act
            var result = await _sorter.SortInParallelAsync(_mockRestaurants);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].Rating, Is.EqualTo(4.7));
            Assert.That(result[1].Rating, Is.EqualTo(4.2));
            Assert.That(result[2].Rating, Is.EqualTo(3.5));
        }

        [Test]
        public async Task SortSequentiallyAsync_ShouldFetchDataIfInputIsNull()
        {
            // Act
            var result = await _sorter.SortSequentiallyAsync(null);

            // Assert
            _parisRestaurantsMock.Verify(s => s.GetParisRestaurantsAsync(), Times.Once);
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task SortInParallelAsync_ShouldFetchDataIfInputIsEmpty()
        {
            // Act
            var result = await _sorter.SortInParallelAsync(new List<RestaurantDto>());

            // Assert
            _parisRestaurantsMock.Verify(s => s.GetParisRestaurantsAsync(), Times.Once);
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task CompareSortingMethodsAsync_ShouldReturnEqualResults()
        {
            // Act
            var result = await _sorter.CompareSortingMethodsAsync();

            // Assert
            Assert.That(result.RestaurantCount, Is.EqualTo(3));
            Assert.That(result.SequentialTimeMs, Is.GreaterThanOrEqualTo(0));
            Assert.That(result.ParallelTimeMs, Is.GreaterThanOrEqualTo(0));
            Assert.That(result.ResultsAreEqual, Is.True);
        }
    }
}
