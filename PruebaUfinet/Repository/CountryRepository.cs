using Dapper;
using PruebaUfinet.Context;
using PruebaUfinet.Contracts;
using PruebaUfinet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaUfinet.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DapperContext _context;
        public CountryRepository(DapperContext context)
        {
            _context = context;
        }

        /*
         * Test fast connectin
        public async Task<IEnumerable<Country>> GetCountrys()
        {
            var query = "SELECT * FROM country";
            using (var connection = _context.CreateConnection())
            {
                var countrys = await connection.QueryAsync<Country>(query);
                return countrys.ToList();
            }
        }
        */

        //Paises con su informacion
        public async Task<IEnumerable<Country>> GetCountrysWithDetails()
        {
            var query = "SELECT * FROM country";
            using (var connection = _context.CreateConnection())
            {
                var countrys = await connection.QueryAsync<Country>(query);

                foreach (var v in countrys)
                {
                    var hotels = await GetHotelByCountryID(v.id);
                    var restaurant = await GetRestaurantsByCountryID(v.id);

                    foreach (var countHotel in hotels)
                    {
                        v.listHotels.Add(countHotel);
                    }
                    foreach (var countRestaurant in restaurant)
                    {
                        v.listRestaurants.Add(countRestaurant);
                    }

                }


                return countrys.ToList();
            }
        }

        //Paises con infromacion paginados
        public async Task<IEnumerable<Country>> GetCountryAllPagined(int page, int amount)
        {
            var procedureName = "spPages";
            var parameters = new DynamicParameters();
            parameters.Add("page",page, DbType.Int32, ParameterDirection.Input);
            parameters.Add("quantityReg", amount, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var country = await connection.QueryAsync<Country>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);


                foreach (var v in country)
                {
                    var hotels = await GetHotelByCountryID(v.id);
                    var restaurant = await GetRestaurantsByCountryID(v.id);

                    foreach (var countHotel in hotels)
                    {
                        v.listHotels.Add(countHotel);
                    }
                    foreach (var countRestaurant in restaurant)
                    {
                        v.listRestaurants.Add(countRestaurant);
                    }

                }

                return country;
            }
        }

        public async Task<IEnumerable<Hotel>> GetHotelByCountryID(int idCountry)
        {
            var query = "SELECT  dbo.hotel.* FROM  dbo.country_hotel INNER JOIN dbo.hotel ON dbo.country_hotel.fk_idHotel = dbo.hotel.id WHERE country_hotel.fk_idCountry = @idCountry";
            using (var connection = _context.CreateConnection())
            {
                var hotels = await connection.QueryAsync<Hotel>(query, new { idCountry });

                return hotels;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByCountryID(int idCountry)
        {
            var query = "SELECT  dbo.restaurant.* FROM  dbo.country_restaurant INNER JOIN dbo.restaurant ON dbo.country_restaurant.fk_idRestaurant = dbo.restaurant.id WHERE country_restaurant.fk_idCountry =  @idCountry";
            using (var connection = _context.CreateConnection())
            {
                var restaurants = await connection.QueryAsync<Restaurant>(query, new { idCountry });

                return restaurants;
            }
        }


        //Filtro - Paises por nombre o codigo iso
        public async Task<IEnumerable<Country>> GetCountryName(string countryName)
        {
            var query = "SELECT * FROM country WHERE LOWER(name) = @name OR LOWER(isoCode) = @name";

            using (var connection = _context.CreateConnection())
            {
                var Country = await connection.QueryAsync<Country>(query, new { name = countryName});

                foreach (var v in Country)
                {
                    var hotels = await GetHotelByCountryID(v.id);
                    var restaurant = await GetRestaurantsByCountryID(v.id);

                    foreach (var countHotel in hotels)
                    {
                        v.listHotels.Add(countHotel);
                    }
                    foreach (var countRestaurant in restaurant)
                    {
                        v.listRestaurants.Add(countRestaurant);
                    }

                }

                return Country;
            }
        }

        //Filtro - Hoteles por nombre
        public async Task<IEnumerable<Hotel>> GetHotelName(string nameHotel)
        {
            var query = "SELECT * FROM hotel WHERE (LOWER(name) LIKE @name) ";
            using (var connection = _context.CreateConnection())
            {
                var hotels = await connection.QueryAsync<Hotel>(query, new { name = nameHotel });

                return hotels;
            }
        }

        //Filtro - Restaurantes por nombre
        public async Task<IEnumerable<Restaurant>> GetRestaurantName(string nameRestaurant)
        {
            var query = "SELECT * FROM restaurant WHERE (LOWER(name) LIKE @name)";
            using (var connection = _context.CreateConnection())
            {
                var restaurant = await connection.QueryAsync<Restaurant>(query, new { name =  nameRestaurant });

                return restaurant;
            }
        }

        public async Task<IEnumerable<Object>> GetFilter(string filter)
        {
            var hotel = await GetHotelName(filter);
            if (hotel.Count() != 0)
            {
                return hotel;
            }
            var restaurant = await GetRestaurantName(filter);
            if (restaurant.Count() != 0)
            {
                return restaurant;
            }
            var country = await GetCountryName(filter);
            
            return country;
            
        }
    }
}
