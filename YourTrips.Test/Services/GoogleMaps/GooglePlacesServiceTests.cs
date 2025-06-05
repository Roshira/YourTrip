using NUnit.Framework;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YourTrips.Infrastructure.Services.GoogleMapsServices;
using YourTrips.Core.DTOs.GoogleMaps;
using YourTrips.Core.DTOs.GoogleMaps.PlaceDetail;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using Moq.Protected;
using YourTrips.Core.DTOs.GoogleMaps.PlaceDetail.ResponseClase;

namespace YourTrips.Infrastructure.Tests.Services
{
    [TestFixture]
    public class GooglePlacesServiceTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private Mock<IConfiguration> _configurationMock;
        private GooglePlacesService _service;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["GoogleMaps:ApiKey"]).Returns("test_api_key");

            _service = new GooglePlacesService(_httpClient, _configurationMock.Object);
        }

        [Test]
        public async Task GetFullPlaceDetailsAsync_ReturnsPlaceDetails_WhenApiCallIsSuccessful()
        {
            // Arrange
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

            // Act
            var result = await _service.GetFullPlaceDetailsAsync(placeId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Test Place"));
            Assert.That(result.Formatted_Address, Is.EqualTo("123 Test St"));
            Assert.That(result.Latitude, Is.EqualTo(12.34));
            Assert.That(result.Longitude, Is.EqualTo(56.78));
            Assert.That(result.PhotoUrl, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetFullPlaceDetailsAsync_ThrowsException_WhenApiCallFails()
        {
            // Arrange
            var placeId = "test_place_id";
            SetupHttpResponse(HttpStatusCode.BadRequest, null);

            // Act & Assert
            Assert.ThrowsAsync<HttpRequestException>(() => _service.GetFullPlaceDetailsAsync(placeId));
        }

        [Test]
        public async Task GetPlacesInfoAsync_ReturnsEmptyList_WhenApiCallFails()
        {
            // Arrange
            var locationName = "test location";
            SetupHttpResponse(HttpStatusCode.BadRequest, null);

            // Act
            var result = await _service.GetPlacesInfoAsync(locationName);

            // Assert
            Assert.That(result, Is.Empty);
        }

        //[Test]
        //public async Task GetPlacesInfoAsync_ReturnsPlaces_WhenApiCallIsSuccessful()
        //{
        //    // Arrange
        //    var locationName = "test location";
        //    var expectedResponse = new
        //    {
        //        results = new[]
        //        {
        //            new
        //            {
        //                place_id = "place1",
        //                name = "Place 1",
        //                formatted_address = "Address 1",
        //                photos = new[] { new { photo_reference = "photo1" } }
        //            },
        //            new
        //            {
        //                place_id = "place2",
        //                name = "Place 2",
        //                formatted_address = "Address 2",
        //                photos = (object)null
        //            }
        //        }
        //    };

        //    SetupHttpResponse(HttpStatusCode.OK, expectedResponse);

        //    // Act
        //    var result = await _service.GetPlacesInfoAsync(locationName);

        //    // Assert
        //    Assert.That(result, Has.Count.EqualTo(2));
        //    Assert.That(result[0].Name, Is.EqualTo("Place 1"));
        //    Assert.That(result[0].PhotoUrls, Is.Not.Null);
        //    Assert.That(result[1].PhotoUrls, Is.Null);
        //}

        [Test]
        public async Task GetPlacesInfoAsync_HandlesEmptyResponseCorrectly()
        {
            // Arrange
            var locationName = "test location";
            var expectedResponse = new { results = new object[0] };
            SetupHttpResponse(HttpStatusCode.OK, expectedResponse);

            // Act
            var result = await _service.GetPlacesInfoAsync(locationName);

            // Assert
            Assert.That(result, Is.Empty);
        }


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