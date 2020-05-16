using FullStackDemo.Front.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStackDemo.Front.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WeatherForecastService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var backEndUrl = _configuration["BackEndUrl"];
            var httpResponse = await _httpClient.GetStringAsync(backEndUrl);
             return JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(httpResponse);
        }
    }
}
