namespace Stallsphere.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public double Temperature { get; set; } // In Celsius
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
