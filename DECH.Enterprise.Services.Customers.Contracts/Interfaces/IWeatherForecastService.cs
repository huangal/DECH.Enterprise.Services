using System.Threading.Tasks;
using DECH.Enterprise.Services.Customers.Contracts.Models;

namespace DECH.Enterprise.Services.Customers.Contracts.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<WeatherResponse> GetWeatherForecastAsync(string location);
    }
}
