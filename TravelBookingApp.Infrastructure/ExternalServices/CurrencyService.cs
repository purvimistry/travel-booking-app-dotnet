using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TravelBookingApp.Application.DTOs;
using TravelBookingApp.Application.Services;

namespace TravelBookingApp.Infrastructure.ExternalServices
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public CurrencyService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<CurrencyConvertResponseDto> ConvertAsync(decimal amount, string from, string to)
        {
            var apiKey = _config["ExchangeRateApi:Key"];
            var baseUrl = _config["ExchangeRateApi:BaseUrl"];
            var url = $"{baseUrl}{apiKey}/pair/{from}/{to}/{amount}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();  
            var data = JsonSerializer.Deserialize<ExchangeRateApiResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            return new CurrencyConvertResponseDto
            {
                OriginalAmount = amount,
                FromCurrency = from,
                ConvertedAmount = data!.conversion_result,
                ToCurrency = to,
                ConversionRate = data!.conversion_rate,
            };
        }
    }
}
