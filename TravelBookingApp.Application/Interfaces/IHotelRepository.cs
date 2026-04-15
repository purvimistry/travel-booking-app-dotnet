using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookingApp.Domain.Entities;

namespace TravelBookingApp.Application.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel?> GetByIdAsync(int id);
        Task<IEnumerable<Hotel>> SearchAsync(string? city, decimal? maxPrice, int? minStars);
        Task<IEnumerable<string>> GetAllCitiesAsync();
        Task<Hotel> AddHotel(Hotel hotel);
        Task<Hotel> UpdateHotel(Hotel hotel);  
        Task<bool> DeleteHotel(int id);   
    }
}
