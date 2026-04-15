using System.Globalization;

namespace TravelBookingApp.Web.Models
{
    public class HotelItemViewModel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string City {  get; set; }
        public string Country { get; set; } 
        public decimal PricePerNight {  get; set; } 
        public int StarRating {  get; set; }
        public int TotalBookings {  get; set; }
        public string PriceDisplay => PricePerNight.ToString("C0",CultureInfo.GetCultureInfo("en-IN"));
        public string Stars => new string('★', StarRating);

    }
}
