using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookingApp.Application.Interfaces;
using TravelBookingApp.Domain.Entities;
using TravelBookingApp.Infrastructure.Data;

namespace TravelBookingApp.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;
        public HotelRepository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<Hotel> AddHotel(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();  
            return hotel;
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var hotel = await GetByIdAsync(id);
            if(hotel != null)
            {
                hotel.IsActive = false; 
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                .Where(h => h.IsActive)
                .OrderBy(h => h.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllCitiesAsync()
        {
            return await _context.Hotels
                .Where (h => h.IsActive)
                .Select(h => h.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<Hotel?> GetByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Bookings)
                .FirstOrDefaultAsync(h => h.HotelId == id);  
        }

        public async Task<IEnumerable<Hotel>> SearchAsync(string? city, decimal? maxPrice, int? minStars)
        {
            var query =  _context.Hotels.Where(h => h.IsActive).AsQueryable();
            if(!string.IsNullOrEmpty(city))
            {
                query = query.Where(h => h.City.Contains(city));
            }
            if(maxPrice.HasValue)
            {
                query = query.Where(h => h.PricePerNight <= maxPrice.Value);
            }
            if(minStars.HasValue)
            {
                query = query.Where(h => h.StarRating >= minStars.Value);
            }
            return await query.OrderBy(h => h.PricePerNight).ToListAsync();
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();  
            return hotel;
        }
    }
}
