using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookingApp.Application.DTOs
{
    public class ExchangeRateApiResponse
    {
        public string result { get; set; }
        public string base_code { get; set; }
        public string target_code { get; set; }
        public decimal conversion_rate { get; set; }
        public decimal conversion_result { get; set; }
    }
}
