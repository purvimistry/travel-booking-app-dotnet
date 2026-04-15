using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelBookingApp.Application.Interfaces;
using TravelBookingApp.Web.Models;

namespace TravelBookingApp.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepo;
        public HotelController(IHotelRepository hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        public async Task<IActionResult> Index(string? searchCity, decimal? maxPrice, int? minStars)
        {
            var hotels = await _hotelRepo.SearchAsync(searchCity, maxPrice, minStars);
            var cities = await _hotelRepo.GetAllCitiesAsync();

            var vm = new HotelSearchViewModel
            {
                SearchCity = searchCity,
                MaxPrice = maxPrice,
                MinStars = minStars,
                Hotels = hotels.Select(h => new HotelItemViewModel
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    City = h.City,
                    Country = h.Country,
                    PricePerNight = h.PricePerNight,
                    StarRating = h.StarRating,
                    TotalBookings = h.Bookings?.Count ?? 0, 
                }),
                Cities = cities.Select(c => new SelectListItem
                {
                    Value = c,
                    Text = c,
                    Selected = c == searchCity
                })
            };
            return View(vm);
        }
    }
}
