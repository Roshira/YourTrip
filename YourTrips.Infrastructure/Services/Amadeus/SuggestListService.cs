using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using YourTrips.Application.Amadeus.Models;
using YourTrips.Application.Interfaces.Amadeus;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Core.DTOs.Amadeus.Locations;

namespace YourTrips.Infrastructure.Services.Amadeus
{
    public class SuggestListService : ISuggestListService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IAmadeusAuthService _service;

        public SuggestListService(HttpClient httpClient, IConfiguration configuration, IAmadeusAuthService service)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _service = service;
        }

        public async Task<List<LocationData>> GetLocationSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<LocationData>();

            // Отримуємо токен (з кешуванням)
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