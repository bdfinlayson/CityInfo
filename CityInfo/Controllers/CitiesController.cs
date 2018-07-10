using CityInfo.Models;
using CityInfo.Services;
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
        private ICityInfoRepository _repository;

        public CitiesController(ICityInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            //return Ok(new JsonResult(CitiesDataStore.Current.Cities));
            var cities = _repository.GetCities();

            var results = AutoMapper.Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities);

            //Old implementation without AutoMapper:

            //var results = new List<CityWithoutPointsOfInterestDto>();
            //foreach (var city in cities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description
            //    });
            //}

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            if (_repository.CityExists(id))
            {
                var city = _repository.GetCity(id, includePointsOfInterest);

                if (includePointsOfInterest)
                {
                    var cityResult = AutoMapper.Mapper.Map<CityDto>(city);
                    return Ok(cityResult);
                }
                else
                {
                    var cityResultWoPoi = AutoMapper.Mapper.Map<CityWithoutPointsOfInterestDto>(city);
                    return Ok(cityResultWoPoi);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
