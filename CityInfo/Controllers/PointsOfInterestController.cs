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

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
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

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] Models.PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            bool nameDescriptionEqual = pointOfInterest.Name == pointOfInterest.Description;
            if (nameDescriptionEqual)
            {
                ModelState.AddModelError("Description", "Description cannot be the same as name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // demo purposes - to be improved
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                             c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new Models.PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] Models.PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            bool nameDescriptionEqual = pointOfInterest.Name == pointOfInterest.Description;
            if (nameDescriptionEqual)
            {
                ModelState.AddModelError("Description", "Name and description cannot be the same");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }
    }
}