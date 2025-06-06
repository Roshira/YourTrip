using NUnit.Framework;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YourTrips.Infrastructure.Services.GoogleMapsServices;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using Moq.Protected;
using YourTrips.Application.DTOs.GoogleMaps.PlaceDetail;
using YourTrips.Application.DTOs.GoogleMaps.PlaceDetail.ResponseClase;
using Microsoft.EntityFrameworkCore;

namespace YourTrips.Infrastructure.Tests.Services
{
    /// <summary>
    /// Unit tests for the GooglePlacesService class which interacts with the Google Places API.
    /// </summary>
    [TestFixture]
    public class GooglePlacesServiceTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private Mock<IConfiguration> _configurationMock;
        private GooglePlacesService _service;

        /// <summary>
        /// Setup method to initialize mocks and the service before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["GoogleMaps:ApiKey"]).Returns("test_api_key");

            _service = new GooglePlacesService(_httpClient, _configurationMock.Object);
        }

        /// <summary>
        /// Cleanup resources after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        /// <summary>
        /// Verifies that GetFullPlaceDetailsAsync returns valid details when the API call succeeds.
        /// </summary>
        [Test]
        public async Task GetFullPlaceDetailsAsync_ReturnsPlaceDetails_WhenApiCallIsSuccessful()
        {
            var placeId = "test_place_id";
            var expectedResponse = new GooglePlaceDetailsResponse
            {
                Result = new GooglePlaceResult
                {
                    Name = "Test Place",
                    Formatted_Address = "123 Test St",
                    Geometry = new Geometry { Location = new Location { Lat = 12.34, Lng = 56.78 } },
                    Photos = new List<Photo> { new Photo { Photo_Reference = "photo_ref" } }
                }
            };

            SetupHttpResponse(HttpStatusCode.OK, expectedResponse);

            var result = await _service.GetFullPlaceDetailsAsync(placeId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Test Place"));
            Assert.That(result.Formatted_Address, Is.EqualTo("123 Test St"));
            Assert.That(result.Latitude, Is.EqualTo(12.34));
            Assert.That(result.Longitude, Is.EqualTo(56.78));
            Assert.That(result.PhotoUrl, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Verifies that GetFullPlaceDetailsAsync throws an exception when the API returns an error status.
        /// </summary>
        [Test]
        public void GetFullPlaceDetailsAsync_ThrowsException_WhenApiCallFails()
        {
            var placeId = "test_place_id";
            SetupHttpResponse(HttpStatusCode.BadRequest, null);

            Assert.ThrowsAsync<HttpRequestException>(() => _service.GetFullPlaceDetailsAsync(placeId));
        }

        /// <summary>
        /// Verifies that GetPlacesInfoAsync returns an empty list when the API fails.
        /// </summary>
        [Test]
        public async Task GetPlacesInfoAsync_ReturnsEmptyList_WhenApiCallFails()
        {
            var locationName = "test location";
            SetupHttpResponse(HttpStatusCode.BadRequest, null);

            var result = await _service.GetPlacesInfoAsync(locationName);

            Assert.That(result, Is.Empty);
        }

        // The test below is commented out. If uncommented, it verifies correct parsing of a successful places search response.
        /*
        [Test]
        public async Task GetPlacesInfoAsync_ReturnsPlaces_WhenApiCallIsSuccessful()
        {
            var locationName = "test location";
            var expectedResponse = new
            {
                results = new[]
                {
                    new
                    {
                        place_id = "place1",
                        name = "Place 1",
                        formatted_address = "Address 1",
                        photos = new[] { new { photo_reference = "photo1" } }
                    },
                    new
                    {
                        place_id = "place2",
                        name = "Place 2",
                        formatted_address = "Address 2",
                        photos = (object)null
                    }
                }
            };

            SetupHttpResponse(HttpStatusCode.OK, expectedResponse);

            var result = await _service.GetPlacesInfoAsync(locationName);

            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Place 1"));
            Assert.That(result[0].PhotoUrls, Is.Not.Null);
            Assert.That(result[1].PhotoUrls, Is.Null);
        }
        */

        /// <summary>
        /// Verifies that GetPlacesInfoAsync handles empty API results correctly.
        /// </summary>
        [Test]
        public async Task GetPlacesInfoAsync_HandlesEmptyResponseCorrectly()
        {
            var locationName = "test location";
            var expectedResponse = new { results = new object[0] };
            SetupHttpResponse(HttpStatusCode.OK, expectedResponse);

            var result = await _service.GetPlacesInfoAsync(locationName);

            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Sets up a mocked HTTP response for use in tests.
        /// </summary>
        /// <param name="statusCode">HTTP status code to simulate</param>
        /// <param name="content">Content object to serialize and return as response body</param>
        private void SetupHttpResponse(HttpStatusCode statusCode, object content)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = content != null
                    ? new StringContent(JsonConvert.SerializeObject(content))
                    : null
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }
    }
}
