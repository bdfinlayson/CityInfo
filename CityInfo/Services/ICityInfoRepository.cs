using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Entities;

namespace CityInfo.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int id, bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int id);

        PointOfInterest GetPointOfInterest(int cityId, int id);

        bool CityExists(int id);

        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        bool Save();
    }
}
