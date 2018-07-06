using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Entities
{
    public class CityInfoContext : DbContext // context that works on a specific database
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}
