using System.Text.Json.Serialization;

public class HotelResultDto
{
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }
    [JsonPropertyName("hotel_name")]
    public string HotelName { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("country_trans")]
    public string Region { get; set; }

    [JsonPropertyName("review_score")]
    public double ReviewScore { get; set; }

    [JsonPropertyName("review_nr")]
    public int ReviewCount { get; set; }

    [JsonPropertyName("main_photo_url")]
    public string MainPhotoUrl { get; set; }

    [JsonPropertyName("composite_price_breakdown")]
    public PriceBreakdown PriceBreakdown { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    public string Type { get; set; }
}

public class PriceBreakdown
{
    [JsonPropertyName("gross_amount")]
    public GrossAmount GrossAmount { get; set; }
}

public class GrossAmount
{
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}