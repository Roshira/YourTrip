﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.DTOs.RapidBooking.Describe
{
    public class HotelDescribeResultDto
    {
        public List<HotelDescribePurtDto> HotelDescriptions { get; set; } = new();
    }
}
