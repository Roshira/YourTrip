using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Core.DTOs.RapidBooking;
using YourTrips.Core.DTOs.RapidBooking.Describe;
using YourTrips.Infrastructure.Services.BookingService;

[ApiController]
[Route("api/[controller]")]
public class HotelSearchController : ControllerBase
{
    private readonly IBookingApiService _bookingApiService;
    private readonly IBookingDescribeService _bookingDescribeService;
    private readonly ISuggestBookingService _suggest;

    public HotelSearchController(IBookingApiService bookingApiService, IBookingDescribeService bookingDescribeService, ISuggestBookingService suggest)
    {
        _bookingApiService = bookingApiService;
        _bookingDescribeService = bookingDescribeService;
        _suggest = suggest;

    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchHotels([FromQuery] HotelSearchRequestDto request)
    {

        Console.WriteLine($"{request}");
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
    [HttpGet("describe")]
    public async Task<IActionResult> DescribeHotels([FromQuery] HotelSearchDescribeDto request)
    {
        var result = await _bookingDescribeService.DescribeHotelAsync(request);
        return Ok(result);
    }
    // YourTrips.Web/Controllers/BookingController.cs


    [HttpGet("Suggesting")]
    public async Task<IActionResult> GetHotelSuggestions([FromQuery] string name)
    {
        var suggestions = await _suggest.GetSuggestionsAsync(name);
        return Ok(suggestions);
    }




}