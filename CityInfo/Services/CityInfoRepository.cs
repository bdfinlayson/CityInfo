using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Services;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public City GetCity(int id, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == id).FirstOrDefault();
            }

            return _context.Cities.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(city => city.Name).ToList();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int id)
        {
            return _context.PointsOfInterest.Where(p => p.CityId == cityId && p.Id == id).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointsOfInterest.Where(c => c.CityId == cityId).ToList();
        }
    }
}
