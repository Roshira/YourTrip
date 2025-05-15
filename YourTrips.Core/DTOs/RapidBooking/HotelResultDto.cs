using System;

namespace YourTrips.Core.DTOs.RapidBooking
{
    public class HotelResultDto
    {
        public string Name { get; set; }                // Назва готелю
        public string Address { get; set; }             // Адреса
        public string City { get; set; }                // Місто
        public double Latitude { get; set; }            // Координати
        public double Longitude { get; set; }
        public string Description { get; set; }         // Короткий опис
        public double Rating { get; set; }              // Рейтинг (наприклад, 8.5)
        public int ReviewCount { get; set; }            // Кількість відгуків
        public string ImageUrl { get; set; }            // Фото
        public decimal PricePerNight { get; set; }      // Ціна за ніч
        public string Currency { get; set; }            // Валюта (наприклад, EUR)
        public string BookingUrl { get; set; }          // Посилання на бронювання
    }
}