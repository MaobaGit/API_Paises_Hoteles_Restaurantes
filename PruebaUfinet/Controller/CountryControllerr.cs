using Microsoft.AspNetCore.Mvc;
using PruebaUfinet.Contracts;
using PruebaUfinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaUfinet.Controller
{
    [Route("api/country")]
    [ApiController]
    public class CountryControllerr : ControllerBase
    {

        private readonly ICountryRepository _countryRepo;
        public CountryControllerr(ICountryRepository countryRepo)
        {
            _countryRepo = countryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountrysWithDetails()
        {
            try
            {
                var country = await _countryRepo.GetCountrysWithDetails();
                
                return Ok(country);
            }
            catch (Exception ex)
            {
                //log error
                Console.Write(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("CountryAllPagined/{page},{amount}")]
        public async Task<IActionResult> GetCountryAllPagined(int page, int amount)
        {
            try
            {
                var country = await _countryRepo.GetCountryAllPagined(page, amount);
                if (country == null)
                    return NotFound();
                return Ok(country);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Filter/{name}")]
        public async Task<IActionResult> GetFilter(string name)
        {
            try
            {
                var filter = await _countryRepo.GetFilter(name);
                if (filter == null)
                    return NotFound();
                return Ok(filter);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("FilterCountry/{name}")]
        public async Task<IActionResult> GetCountryName(string name)
        {
            try
            {
                var country = await _countryRepo.GetCountryName(name);
                if (country == null)
                    return NotFound();
                return Ok(country);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("FilterHotel/{name}")]
        public async Task<IActionResult> GetHotelName(string name)
        {
            try
            {
                var hotels = await _countryRepo.GetHotelName(name);
                if (hotels == null)
                    return NotFound();
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("FilterRestaurant/{name}")]
        public async Task<IActionResult> GetRestaurantName(string name)
        {
            try
            {
                var restaurants = await _countryRepo.GetRestaurantName(name);
                if (restaurants == null)
                    return NotFound();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
