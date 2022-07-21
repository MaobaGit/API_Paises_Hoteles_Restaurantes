using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaUfinet.Models
{
    public class Country
    {
        public int id { get; set; }

        public string name { get; set; }

        public string isoCode { get; set; }

        public int population { get; set; }

        public List<Restaurant> listRestaurants { get; set; } = new List<Restaurant>();
        public List<Hotel> listHotels { get; set; } = new List<Hotel>();
    }
}
