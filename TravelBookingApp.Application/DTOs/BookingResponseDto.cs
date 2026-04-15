using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Application.DTOs
{
    public class BookingResponseDto
    {
        public int BookingId { get; set; }
        public string HotelName { get; set; }
        public string HotelCity { get; set; } 
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; } 
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get; set; }
        public int NumberOfGuests { get; set; } 
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime BookedOn { get; set; }
    }
}
