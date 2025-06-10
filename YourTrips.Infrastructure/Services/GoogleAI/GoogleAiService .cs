using System.Text.Json;
using System.Text;
using YourTrips.Application.Interfaces.GoogleAI;
using Microsoft.Extensions.Configuration;

public class GoogleAiService : IGoogleAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _aiKey;
    private readonly string _endpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

    public GoogleAiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _aiKey = config["GoogleAi:ApiKey"];
    }

    public async Task<string> GenerateTextAsync(string prompt)
    {
        var request = new
        {
            contents = new[]
            {
            new
            {
                role = "user",
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
        };

        var uri = $"{_endpoint}?key={_aiKey}";
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Google AI API error: {response.StatusCode} - {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(jsonResponse);
        return doc.RootElement
                  .GetProperty("candidates")[0]
                  .GetProperty("content")
                  .GetProperty("parts")[0]
                  .GetProperty("text")
                  .GetString();
    }

}
