using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookingApp.Application.DTOs;
using TravelBookingApp.Application.Interfaces;
using TravelBookingApp.Application.Services;
using TravelBookingApp.Domain.Entities;

namespace TravelBookingApp.Infrastructure.ExternalServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IHotelRepository _hotelRepo;
        public BookingService(IBookingRepository bookingRepository,IHotelRepository hotelRepository)
        {
            _bookingRepo = bookingRepository;
            _hotelRepo = hotelRepository;   
        }
        public async Task<BookingResponseDto> CreateAsync(BookingDto dto)
        {
            var hotel = await _hotelRepo.GetByIdAsync(dto.HotelId);
            if (hotel == null) throw new Exception("Hotel not found");

            int nights = (dto.CheckOut - dto.CheckIn).Days;
            if (nights <= 0) throw new Exception("Check-out must be after check-in");

            var booking = new Booking
            {
                HotelId = dto.HotelId,
                CustomerId = dto.CustomerId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                NumberOfGuests = dto.NumberOfGuests,
                TotalAmount = nights * hotel.PricePerNight,
                Status = "Confirmed",
                BookedOn = DateTime.Now,
            };
            await _bookingRepo.AddAsync(booking);
            return new BookingResponseDto
            {
                BookingId = booking.BookingId,
                HotelName = hotel.Name,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                NumberOfNights = nights,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await _bookingRepo.ExistsAsync(id);
            if(!exists) return false;

            await _bookingRepo.DeleteAsync(id); 
            return true;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllAsync()
        {
            var booking = await _bookingRepo.GetAllAsync();
            return booking.Select(b => new BookingResponseDto
            {
                BookingId = b.BookingId,
                HotelName = b.Hotel?.Name ?? "N/A",
                HotelCity = b.Hotel?.City ?? "N/A",
                CustomerName = b.Customer?.FullName ?? "N/A",
                CustomerEmail = b.Customer?.Email ?? "N/A",
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                NumberOfNights = b.NumberOfNights,
                NumberOfGuests = b.NumberOfGuests,
                TotalAmount = b.TotalAmount,
                Status = b.Status,
                BookedOn = b.BookedOn,

            });
        }

        public async Task<BookingResponseDto?> GetByIdAsync(int id)
        {
           var b = await _bookingRepo.GetByIdAsync(id);
            if(b == null) return null;

            return new BookingResponseDto { 
                BookingId = b.BookingId,
                HotelName = b.Hotel?.Name ?? "N/A",
                HotelCity = b.Hotel?.City ?? "N/A",
                CustomerName = b.Customer?.FullName ?? "N/A",
                CustomerEmail = b.Customer?.Email ?? "N/A",
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                NumberOfNights = b.NumberOfNights,  
                NumberOfGuests= b.NumberOfGuests,   
                TotalAmount = b.TotalAmount,
                Status = b.Status,  
                BookedOn = b.BookedOn,
            };
        }

        public async Task<bool> UpdateAsync(int id, BookingDto dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if(booking == null) return false;

            var hotel = await _hotelRepo.GetByIdAsync(dto.HotelId);
            if(hotel == null) return false;

            int nights = (dto.CheckOut - dto.CheckIn).Days;
            
            booking.HotelId = dto.HotelId;
            booking.CustomerId = dto.CustomerId;
            booking.CheckIn = dto.CheckIn;
            booking.CheckOut = dto.CheckOut;
            booking.NumberOfGuests = dto.NumberOfGuests;
            booking.TotalAmount = nights * hotel.PricePerNight;

            await _bookingRepo.UpdateAsync(booking);
            return true;
        }
    }
}
