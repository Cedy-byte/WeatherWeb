using System;
using System.Collections.Generic;

namespace WeatherWeb.Models
{
    public partial class Users
    {
        public Users()
        {
            FavoriteCities = new HashSet<FavoriteCities>();
        }

        public string Username { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }

        public virtual ICollection<FavoriteCities> FavoriteCities { get; set; }
    }
}
