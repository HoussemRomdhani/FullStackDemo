using FullStackDemo.Front.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackDemo.Front.Services
{
    public interface IWeatherForecastService
    {
         Task<IEnumerable<WeatherForecast>> Get();
    }
}
