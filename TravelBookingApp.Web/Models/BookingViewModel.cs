using System.Globalization;

namespace TravelBookingApp.Web.Models
{
    public class BookingViewModel
    {
        public int BookingId {  get; set; } 
        public string HotelName {  get; set; }  
        public string HotelCity { get; set;}
        public string CustomerName {  get; set; }   
        public string CustomerEmail {  get; set; }
        public DateTime CheckedIn {  get; set; }    
        public DateTime CheckedOut { get; set; }
        public int NumberOfNights {  get; set; }
        public int NumberOfGuests {  get; set; }    
        public decimal TotalAmount {  get; set; }   
        public string Status {  get; set; }
        public DateTime BookedOn { get; set; }
        public string CheckInDisplay => CheckedIn.ToString("dd MMM yyyy");
        public string CheckOutDisplay => CheckedOut.ToString("dd MMM yyyy");
        public string TotalAmountDisplay => TotalAmount.ToString("C0", CultureInfo.GetCultureInfo("en-IN"));
    }
}
