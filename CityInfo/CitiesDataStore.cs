using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<Models.CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<Models.CityDto>()
            {
                new Models.CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = new List<Models.PointOfInterestDto>()
                    {
                        new Models.PointOfInterestDto()
                        {
                           Id = 1,
                           Name = "Central Park",
                           Description = "The most visited urban park"

                        },
                        new Models.PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "Great for sightseeing"
                        }
                    }
                },
                new Models.CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<Models.PointOfInterestDto>()
                    {
                        new Models.PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Place 1",
                            Description = "something about place 1"
                        },
                        new Models.PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Place 2",
                            Description = "blah blah place 2"
                        }
                    }
                },
                new Models.CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                    PointsOfInterest = new List<Models.PointOfInterestDto>()
                    {
                        new Models.PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Place 3",
                            Description = "blah"
                        }
                    }
                }
            };
        }
    }
}
