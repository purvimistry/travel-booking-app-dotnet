using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelBookingApp.Application.Services;

namespace TravelBookingApp.API.Controllers
{
    [ApiController]
    [Route("api/currency")]
    public class CurrencyApiController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyApiController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpGet("convert")]
        public async Task<IActionResult> Convert(decimal amount,string from="INR", string to = "USD")
        {
            if (amount <= 0)
                return BadRequest(new { message = "Amount must be greater than 0" });

            try
            {
                var result = await _currencyService.ConvertAsync(amount, from, to);
                return Ok(result);
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "Currency conversion failed", error = ex.Message });
            }
        }
    }
}
