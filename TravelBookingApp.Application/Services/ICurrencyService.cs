using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookingApp.Application.DTOs;

namespace TravelBookingApp.Application.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyConvertResponseDto> ConvertAsync(decimal amount, string from, string to);
    }
}
