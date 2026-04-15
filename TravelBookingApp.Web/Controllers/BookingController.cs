using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TravelBookingApp.Application.DTOs;
using TravelBookingApp.Application.Services;
using TravelBookingApp.Domain.Entities;
using TravelBookingApp.Infrastructure.Data;
using TravelBookingApp.Web.Models;

namespace TravelBookingApp.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly AppDbContext _context;

        public BookingController(IBookingService bookingService, AppDbContext context)
        {
            _bookingService = bookingService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings =await _bookingService.GetAllAsync();
            var vm = bookings.Select(b => new BookingViewModel
            {
                BookingId = b.BookingId,
                HotelName = b.HotelName,
                HotelCity = b.HotelCity,
                CustomerName = b.CustomerName,
                CustomerEmail = b.CustomerEmail,
                CheckedIn = b.CheckIn,
                CheckedOut = b.CheckOut,
                NumberOfNights = b.NumberOfNights,
                NumberOfGuests = b.NumberOfGuests,
                TotalAmount = b.TotalAmount,
                Status = b.Status,
                BookedOn = b.BookedOn
            });

            return View(vm);
        }
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) { return NotFound(); }
            var vm = new BookingViewModel
            {
                BookingId = booking.BookingId,
                HotelName = booking.HotelName,
                HotelCity = booking.HotelCity,
                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                CheckedIn = booking.CheckIn,
                CheckedOut = booking.CheckOut,
                NumberOfNights = booking.NumberOfNights,
                NumberOfGuests = booking.NumberOfGuests,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
                BookedOn = booking.BookedOn
            };
            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
            var vm = new BookingCreateViewModel
            {
                Hotels = await GetHotelSelectList(),
                Customers = await GetCustomerSelectList(),
            };
            return View(vm);    

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Hotels = await GetHotelSelectList();
                vm.Customers = await GetCustomerSelectList();
                return View(vm);
            }

            var dto = new BookingDto
            {
                HotelId = vm.HotelId,
                CustomerId = vm.CustomerId,
                CheckIn = vm.CheckedIn,
                CheckOut = vm.CheckedOut,
                NumberOfGuests = vm.NumberOfGuests
            };

            await _bookingService.CreateAsync(dto);
            TempData["Success"] = "Booking created successfully!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();

            var vm = new BookingCreateViewModel
            {
                Hotels = await GetHotelSelectList(),
                Customers = await GetCustomerSelectList(),
                CheckedIn = booking.CheckIn,
                CheckedOut = booking.CheckOut,
                NumberOfGuests = booking.NumberOfGuests
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Hotels = await GetHotelSelectList();
                vm.Customers = await GetCustomerSelectList();
                return View(vm);
            }

            var dto = new BookingDto
            {
                HotelId = vm.HotelId,
                CustomerId = vm.CustomerId,
                CheckIn = vm.CheckedIn,
                CheckOut = vm.CheckedOut,
                NumberOfGuests = vm.NumberOfGuests
            };

            var updated = await _bookingService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            TempData["Success"] = "Booking updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingService.DeleteAsync(id);
            if (!deleted)
                return Json(new { success = false, message = "Booking not found" });

            return Json(new { success = true, message = "Booking deleted!" });
        }


        private async Task<IEnumerable<SelectListItem>> GetHotelSelectList()
        {
            var hotels = await _context.Hotels.Where(h => h.IsActive).ToListAsync();
            return hotels.Select(h => new SelectListItem
            {
                Value = h.HotelId.ToString(),
                Text = $"{h.Name} - {h.City} (₹{h.PricePerNight}/night)",
            });
        }
        private async Task<IEnumerable<SelectListItem>> GetCustomerSelectList()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers.Select(c => new SelectListItem
            {
                Value = c.CustomerId.ToString(),
                Text = $"{c.FullName} ({c.Email})"
            });
        }
    }
}
