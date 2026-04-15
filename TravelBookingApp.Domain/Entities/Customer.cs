using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        
        public string FullName { get; set; }
       
        public string Email {  get; set; }
       
        public string PhoneNumber {  get; set; }    
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
