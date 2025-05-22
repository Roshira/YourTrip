using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YourTrips.Application.Interfaces.Interfaces;
using YourTrips.Application.Interfaces.RapidBooking;
using YourTrips.Core.DTOs.RapidBooking;
using YourTrips.Core.DTOs.RapidBooking.Describe;
using YourTrips.Core.Entities;
using YourTrips.Core.Entities.Saved;
using YourTrips.Core.Interfaces.SavedServices;
using YourTrips.Infrastructure.Services.BookingService;

[ApiController]
[Route("api/[controller]")]
public class HotelSearchController : ControllerBase
{
    private readonly IBookingApiService _bookingApiService;
    private readonly IBookingDescribeService _bookingDescribeService;
    private readonly ISuggestBookingService _suggest;
    private readonly UserManager<User> _userManager;
    private readonly ISavDelJSONModel _savDelJSONModel;

    public HotelSearchController(IBookingApiService bookingApiService, IBookingDescribeService bookingDescribeService, ISuggestBookingService suggest, UserManager<User> userManager, ISavDelJSONModel savDelJSONModel)
    {
        _bookingApiService = bookingApiService;
        _bookingDescribeService = bookingDescribeService;
        _suggest = suggest;
        _userManager = userManager;
        _savDelJSONModel = savDelJSONModel;
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

    [HttpPost("Saved")]
    [Authorize]
    public async Task<IActionResult> Saved([FromQuery] string hotelJson)
    {
        var user = await _userManager.GetUserAsync(User);
        await _savDelJSONModel.SaveJsonAsync<SavedHotel>(user.Id, hotelJson);
        return Ok();
    }
}
