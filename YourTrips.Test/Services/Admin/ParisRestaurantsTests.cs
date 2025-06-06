using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

using NUnit.Framework;
using YourTrips.Infrastructure.Services.Admin.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

using NUnit.Framework.Legacy;

namespace YourTrips.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="ParisRestaurants"/> service responsible for retrieving restaurant data from Google Maps API.
    /// </summary>
    [TestFixture]
    public class ParisRestaurantsTests
    {
        /// <summary>
        /// Creates an instance of <see cref="ParisRestaurants"/> with mocked HTTP responses and configuration.
        /// </summary>
        /// <param name="textSearchResponse">The HTTP response returned for the Places Text Search API call.</param>
        /// <param name="detailsResponse">The HTTP response returned for the Place Details API call.</param>
        /// <returns>An instance of <see cref="ParisRestaurants"/> configured with mocked dependencies.</returns>
        private ParisRestaurants CreateService(HttpResponseMessage textSearchResponse, HttpResponseMessage detailsResponse)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(textSearchResponse)
                .ReturnsAsync(detailsResponse);

            var httpClient = new HttpClient(handlerMock.Object);

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["GoogleMaps:ApiKey"]).Returns("fake-api-key");

            return new ParisRestaurants(httpClient, configMock.Object);
        }

        /// <summary>
        /// Verifies that <see cref="ParisRestaurants.GetParisRestaurantsAsync"/> returns the expected restaurant information
        /// when valid mocked responses are provided.
        /// </summary>
        [Test]
        public async Task GetParisRestaurantsAsync_ReturnsExpectedRestaurant()
        {
            var textSearchJson = new
            {
                results = new[]
                {
                    new
                    {
                        place_id = "test-place-id",
                        name = "Test Restaurant",
                        formatted_address = "123 Test St",
                        rating = 4.5,
                        price_level = 2,
                        types = new[] { "restaurant", "food" },
                        photos = new[]
                        {
                            new { photo_reference = "photo-ref-123" }
                        }
                    }
                }
            };

            var detailsJson = new
            {
                result = new
                {
                    opening_hours = new { open_now = true },
                    formatted_phone_number = "+33123456789",
                    website = "http://test.com"
                }
            };

            var textSearchResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(textSearchJson))
            };

            var detailsResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(detailsJson))
            };

            var service = CreateService(textSearchResponse, detailsResponse);

            var results = await service.GetParisRestaurantsAsync();

            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(1));

            var restaurant = results[0];

            Assert.That(restaurant.Id, Is.EqualTo("test-place-id"));
            Assert.That(restaurant.Name, Is.EqualTo("Test Restaurant"));
            Assert.That(restaurant.Address, Is.EqualTo("123 Test St"));
            Assert.That(restaurant.Rating, Is.EqualTo(4.5));
            Assert.That(restaurant.PriceLevel, Is.EqualTo(2));
            Assert.That(restaurant.IsOpenNow, Is.True);
            Assert.That(restaurant.PhoneNumber, Is.EqualTo("+33123456789"));
            Assert.That(restaurant.Website, Is.EqualTo("http://test.com"));
            Assert.That(restaurant.PhotoUrls, Is.Not.Empty);
            StringAssert.Contains("photo_reference=photo-ref-123", restaurant.PhotoUrls[0]);
        }
    }
}
