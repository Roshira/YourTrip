using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.GoogleMaps.PlaceDetail.ResponseClase;

namespace YourTrips.Application.DTOs.GoogleMaps.PlaceDetail
{
    /// <summary>
    /// Represents the response from Google Places API containing detailed information about a place.
    /// </summary>
    public class GooglePlaceDetailsResponse
    {
        /// <summary>
        /// Gets or sets the detailed place result returned by the API.
        /// </summary>
        public GooglePlaceResult Result { get; set; }
    }
}
