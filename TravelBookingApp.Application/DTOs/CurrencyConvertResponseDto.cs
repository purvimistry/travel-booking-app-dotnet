using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Application.DTOs
{
    public class CurrencyConvertResponseDto
    {
        public decimal OriginalAmount { get; set; }
        public string FromCurrency { get; set; }
        public decimal ConvertedAmount { get; set; }
        public string ToCurrency { get; set; }
        public decimal ConversionRate { get; set; }

    }
}
