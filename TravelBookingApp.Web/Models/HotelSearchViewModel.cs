using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelBookingApp.Web.Models
{
    public class HotelSearchViewModel
    {
        public string? SearchCity {  get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStars { get; set; }

        public IEnumerable<HotelItemViewModel> Hotels { get; set; } = new List<HotelItemViewModel>();
        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}
