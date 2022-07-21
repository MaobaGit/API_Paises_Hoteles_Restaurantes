using PruebaUfinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaUfinet.Contracts
{
    public interface ICountryRepository
    {
        public Task<IEnumerable<Country>> GetCountrysWithDetails();

        public Task<IEnumerable<Hotel>> GetHotelByCountryID(int idCountry);

        public Task<IEnumerable<Restaurant>> GetRestaurantsByCountryID(int idCountry);

        public Task<IEnumerable<Country>> GetCountryAllPagined(int page, int amount);

        public Task<IEnumerable<Country>> GetCountryName(string countryName);

        public Task<IEnumerable<Hotel>> GetHotelName(string nameHotel);

        public Task<IEnumerable<Restaurant>> GetRestaurantName(string nameRestaurant);

        public Task<IEnumerable<Object>> GetFilter(string filter);



    }
}
