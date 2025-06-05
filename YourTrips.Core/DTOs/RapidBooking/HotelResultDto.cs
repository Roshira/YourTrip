using System.Text.Json.Serialization;

/// <summary>
/// Data transfer object representing hotel search result information.
/// </summary>
public class HotelResultDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the hotel.
    /// </summary>
    [JsonPropertyName("hotel_id")]
    public int HotelId { get; set; }

    /// <summary>
    /// Gets or sets the name of the hotel.
    /// </summary>
    [JsonPropertyName("hotel_name")]
    public string HotelName { get; set; }

    /// <summary>
    /// Gets or sets the address of the hotel.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the city where the hotel is located.
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the region or country name of the hotel.
    /// </summary>
    [JsonPropertyName("country_trans")]
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the average review score of the hotel.
    /// </summary>
    [JsonPropertyName("review_score")]
    public double ReviewScore { get; set; }

    /// <summary>
    /// Gets or sets the number of reviews the hotel has received.
    /// </summary>
    [JsonPropertyName("review_nr")]
    public int ReviewCount { get; set; }

    /// <summary>
    /// Gets or sets the URL to the main photo of the hotel.
    /// </summary>
    [JsonPropertyName("main_photo_url")]
    public string MainPhotoUrl { get; set; }

    /// <summary>
    /// Gets or sets the price breakdown details of the hotel.
    /// </summary>
    [JsonPropertyName("composite_price_breakdown")]
    public PriceBreakdown PriceBreakdown { get; set; }

    /// <summary>
    /// Gets or sets the URL to the hotel details page.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the hotel location.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the hotel location.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the type/category of the hotel (custom property).
    /// </summary>
    public string Type { get; set; }
}

/// <summary>
/// Represents the price breakdown details for a hotel.
/// </summary>
public class PriceBreakdown
{
    /// <summary>
    /// Gets or sets the gross amount of the price breakdown.
    /// </summary>
    [JsonPropertyName("gross_amount")]
    public GrossAmount GrossAmount { get; set; }
}

/// <summary>
/// Represents the gross amount with value and currency.
/// </summary>
public class GrossAmount
{
    /// <summary>
    /// Gets or sets the price value.
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Gets or sets the currency code (e.g., USD, EUR).
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}
