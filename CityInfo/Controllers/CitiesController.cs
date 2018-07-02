using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(new JsonResult(Models.CitiesDataStore.Current.Cities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var result = Models.CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
