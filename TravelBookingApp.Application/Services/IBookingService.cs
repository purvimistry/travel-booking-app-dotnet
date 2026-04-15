using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookingApp.Application.DTOs;

namespace TravelBookingApp.Application.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponseDto>> GetAllAsync();
        Task<BookingResponseDto?> GetByIdAsync(int id);
        Task<BookingResponseDto> CreateAsync(BookingDto dto);
        Task<bool> UpdateAsync(int id, BookingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
