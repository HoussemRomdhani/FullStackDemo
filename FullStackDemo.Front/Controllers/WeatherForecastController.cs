using System.Threading.Tasks;
using FullStackDemo.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullStackDemo.Front.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _weatherForecastService.Get().ConfigureAwait(false);
            return View(result);
        }


    }
}