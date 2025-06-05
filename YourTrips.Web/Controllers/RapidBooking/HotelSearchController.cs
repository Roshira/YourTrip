using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Core.DTOs.RapidBooking.Describe;
using YourTrips.Core.DTOs.RapidBooking;
using YourTrips.Core.Entities;

using YourTrips.Core.Interfaces.Routes.Saved;

/// <summary>
/// Controller for handling hotel search, description, and suggestion operations using the Booking API.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HotelSearchController : ControllerBase
{
    private readonly IBookingApiService _bookingApiService;
    private readonly IBookingDescribeService _bookingDescribeService;
    private readonly ISuggestBookingService _suggest;
    private readonly UserManager<User> _userManager;
    private readonly ISavDelJSONModel _savDelJSONModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="HotelSearchController"/> class.
    /// </summary>
    /// <param name="bookingApiService">Service for searching hotels.</param>
    /// <param name="bookingDescribeService">Service for describing hotels in detail.</param>
    /// <param name="suggest">Service for hotel name suggestions.</param>
    /// <param name="userManager">ASP.NET Core Identity user manager.</param>
    /// <param name="savDelJSONModel">Service for saving and deleting JSON models.</param>
    public HotelSearchController(
        IBookingApiService bookingApiService,
        IBookingDescribeService bookingDescribeService,
        ISuggestBookingService suggest,
        UserManager<User> userManager,
        ISavDelJSONModel savDelJSONModel)
    {
        _bookingApiService = bookingApiService;
        _bookingDescribeService = bookingDescribeService;
        _suggest = suggest;
        _userManager = userManager;
        _savDelJSONModel = savDelJSONModel;
    }

    /// <summary>
    /// Searches for hotels based on the provided criteria.
    /// </summary>
    /// <param name="request">The search parameters including destination, dates, and other filters.</param>
    /// <returns>A list of hotels that match the search criteria.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchHotels([FromQuery] HotelSearchRequestDto request)
    {
        try
        {
            var hotels = await _bookingApiService.SearchHotelsAsync(request);
            return Ok(hotels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a detailed description of a hotel, including reviews and photos.
    /// </summary>
    /// <param name="request">Information about the hotel to describe, such as hotel ID and locale.</param>
    /// <returns>Detailed hotel description.</returns>
    [HttpGet("describe")]
    public async Task<IActionResult> DescribeHotels([FromQuery] HotelSearchDescribeDto request)
    {
        var result = await _bookingDescribeService.DescribeHotelAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Returns autocomplete suggestions for hotel names based on the user's input.
    /// </summary>
    /// <param name="name">Partial name of the hotel or destination to get suggestions for.</param>
    /// <returns>A list of suggested hotel or destination names.</returns>
    [HttpGet("Suggesting")]
    public async Task<IActionResult> GetHotelSuggestions([FromQuery] string name)
    {
        var suggestions = await _suggest.GetSuggestionsAsync(name);
        return Ok(suggestions);
    }
}
