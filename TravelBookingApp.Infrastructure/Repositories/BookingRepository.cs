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
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext dbContext )
        {
            _context = dbContext;
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync( booking );    
            await _context.SaveChangesAsync();
            return booking; 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking =await GetByIdAsync( id );
            if(booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync(); 
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingId == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                .Include(b => b.Customer)
                .OrderByDescending(b => b.BookedOn)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
                
        }

        public async Task<IEnumerable<Booking>> GetByHotelIdAsync(int hotelId)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Where(b => b.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Hotel)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}
