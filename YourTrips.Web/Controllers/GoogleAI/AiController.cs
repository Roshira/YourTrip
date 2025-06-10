using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourTrips.Application.Interfaces.GoogleAI;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly IGoogleAiService _googleAiService;

    public AiController(IGoogleAiService googleAiService)
    {
        _googleAiService = googleAiService;
    }

    [HttpPost("Generate")]
    public async Task<IActionResult> Generate([FromBody] string prompt)
    {
        var result = await _googleAiService.GenerateTextAsync(prompt);
        return Ok(result);
    }
}
