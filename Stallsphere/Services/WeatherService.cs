using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Stallsphere.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "e31e25f07b56e89f7f862b01dca701dc"; // Replace with your key

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResult> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<WeatherResult>(response);
        }
    }

    public class WeatherResult
    {
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
    }
}
