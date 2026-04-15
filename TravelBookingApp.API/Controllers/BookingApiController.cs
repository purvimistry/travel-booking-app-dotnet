using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelBookingApp.Application.DTOs;
using TravelBookingApp.Application.Services;

namespace TravelBookingApp.API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingApiController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingApiController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if(booking == null)
                    return NotFound(new {message = $"Booking {id} not found"});

            return Ok(booking);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _bookingService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.BookingId }, result);
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]BookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _bookingService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { message = $"Booking {id} not found" });

            return Ok(new { success = true, message = "Booking updated successfully!" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Booking {id} not found" });

            return Ok(new { success = true, message = "Booking deleted successfully!" });
        }
    }
}
