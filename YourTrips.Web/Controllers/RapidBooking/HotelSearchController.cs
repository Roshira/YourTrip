using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HotelSearchController : ControllerBase
{
    private readonly IBookingApiService _bookingApiService;

    public HotelSearchController(IBookingApiService bookingApiService)
    {
        _bookingApiService = bookingApiService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchHotels([FromQuery] HotelSearchRequestDto request)
    {
        var hotels = await _bookingApiService.SearchHotelsAsync(request);
        return Ok(hotels);
    }
}
