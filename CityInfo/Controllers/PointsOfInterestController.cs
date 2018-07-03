using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers
{
    [Produces("application/json")]
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var result = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}")]
        public IActionResult GetPontOfInterest(int cityId, int id)
        {
            var result = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            var pointofinterest = result.PointsOfInterest.FirstOrDefault(point => point.Id == id);

            if (pointofinterest == null)
            {
                return NotFound();
            }

            return Ok(pointofinterest);
        }
    }
}