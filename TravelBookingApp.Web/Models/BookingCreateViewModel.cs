using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TravelBookingApp.Web.Models
{
    public class BookingCreateViewModel
    {
        [Required(ErrorMessage ="Please select a hotel")]
        public int HotelId {  get; set; }
        [Required(ErrorMessage ="Please select a customer")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="Check-In date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckedIn { get; set; }
        [Required(ErrorMessage ="Check-Out date is required")]
        [DataType(DataType.Date)]   
        public DateTime CheckedOut {  get; set; }
        [Range(1,10,ErrorMessage ="Guests must be between 1 and 10")]
        public int NumberOfGuests {  get; set; }    
        public IEnumerable<SelectListItem> Hotels { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (CheckedOut <= CheckedIn)
                yield return new ValidationResult(
                    "Check-out must be after check-in",
                    new[] { nameof(CheckedOut) }
                    );
        }
    }
}
