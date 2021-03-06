using System;
using System.Collections.Generic;

namespace WeatherWeb.Models
{
    public partial class FavoriteCities
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public DateTime Date { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int Precipitation { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public string Username { get; set; }

        public virtual Users UsernameNavigation { get; set; }
    }
}
