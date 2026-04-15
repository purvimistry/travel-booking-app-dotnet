using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Domain.Entities
{
    public class Booking
    {
        public int BookingId {  get; set; }
        
        public int HotelId {  get; set; }
       
        public int CustomerId {  get; set; }
       
        public DateTime CheckIn { get; set; }
       
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; } = 1;
        public decimal TotalAmount {  get; set; }
        public string Status { get; set; } = "Confirmed";
        public DateTime BookedOn { get; set; } = DateTime.Now;
        public Hotel Hotel {  get; set; }
        public Customer Customer { get; set; }
        public int NumberOfNights => (CheckOut - CheckIn).Days;

    }
}
