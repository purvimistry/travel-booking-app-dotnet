using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Domain.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        
        public string Name { get; set; }
       
        public string City { get; set; }
       
        public string Country { get; set; }
        
        public decimal PricePerNight {  get; set; }
       
        public int StarRating {  get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();   
    }
}
