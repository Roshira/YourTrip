using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking
{ 
        public class HotelSearchRequestDto
        {
            public string Country { get; set; }          
            public string City { get; set; }             
            public string? HotelName { get; set; }        
            public DateTime CheckInDate { get; set; }    
            public DateTime CheckOutDate { get; set; }   
            public int Adults { get; set; } = 1;        
            public int Children { get; set; } = 0;       
            public decimal? MinPrice { get; set; }       
            public decimal? MaxPrice { get; set; }       
            public int Rooms { get; set; } = 1;          
        }
    
}

