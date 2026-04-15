using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Application.DTOs
{
    public class BookingDto
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; } = 1;
    }
}
