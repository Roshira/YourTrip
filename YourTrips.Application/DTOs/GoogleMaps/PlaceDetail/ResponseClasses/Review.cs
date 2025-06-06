using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.GoogleMaps.PlaceDetail.ResponseClase
{
    public class Review
    {
        public string Author_Name { get; set; }
        public double Rating { get; set; }
        public string Text { get; set; }
    }
}
