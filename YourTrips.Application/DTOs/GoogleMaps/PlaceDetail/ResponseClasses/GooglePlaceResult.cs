﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.GoogleMaps.PlaceDetail.ResponseClase
{
    /// <summary>
    /// It is google place result wich has many part
    /// </summary>
    public class GooglePlaceResult
    {
        public string Name { get; set; }
        public string Formatted_Address { get; set; }
        public string Vicinity { get; set; }
        public string Formatted_Phone_Number { get; set; }
        public string International_Phone_Number { get; set; }
        public string Website { get; set; }
        public Geometry Geometry { get; set; }
        public List<Photo> Photos { get; set; }
        public OpeningHours Opening_Hours { get; set; }
        public double? Rating { get; set; }
        public int? User_Ratings_Total { get; set; }
        public List<Review> Reviews { get; set; }
        public List<string> Types { get; set; }

        [JsonPropertyName("price_level")]
        public int? Price_Level { get; set; }
        public string Url { get; set; }
        public string Business_Status { get; set; }
        public EditorialSummary Editorial_Summary { get; set; }
    }
}
