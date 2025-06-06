using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using YourTrips.Application.Amadeus.Models;
using YourTrips.Application.DTOs.Amadeus.Locations;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Application.Interfaces.Interfaces;

namespace YourTrips.Infrastructure.Services.Amadeus
{
    /// <summary>
    /// Service for retrieving city and airport location suggestions using the Amadeus API.
    /// </summary>
    public class SuggestAmadeusService : ISuggestAmadeusService
    {
        private readonly HttpClient _httpClient;
        private readonly IAmadeusAuthService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestAmadeusService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> used for sending API requests.</param>
        /// <param name="service">Authentication service that provides Amadeus API access tokens.</param>
        public SuggestAmadeusService(HttpClient httpClient, IAmadeusAuthService service)
        {
            _httpClient = httpClient;
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of city and airport suggestions based on a user-provided keyword.
        /// </summary>
        /// <param name="query">The keyword to search for city or airport locations.</param>
        /// <returns>
        /// A list of <see cref="LocationData"/> representing the suggested cities and airports.
        /// If the query is empty or null, an empty list is returned.
        /// </returns>
        /// <exception cref="HttpRequestException">Thrown when the Amadeus API returns a non-success status code.</exception>
        public async Task<List<LocationData>> GetLocationSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<LocationData>();

            // Retrieve a cached or fresh access token
            var token = await _service.GetAccessTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(
                $"https://test.api.amadeus.com/v1/reference-data/locations?subType=CITY,AIRPORT&keyword={query}"
            );

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Amadeus API error: {response.StatusCode}");

            var responseData = await response.Content.ReadFromJsonAsync<LocationResponse>();
            return responseData?.Data ?? new List<LocationData>();
        }
    }
}
