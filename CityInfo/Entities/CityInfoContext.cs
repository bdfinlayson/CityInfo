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
            Database.Migrate();
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                  new City() { Id = 1, Name = "New York City", Description = "Awesome place to live" },
                  new City() { Id = 2, Name = "Nashville", Description = "Pretty good place to live" }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                  new PointOfInterest() { Id = 1, Name = "Dad's house", Description = "Has rooftop patio", CityId = 1 },
                  new PointOfInterest() { Id = 2, Name = "Bryan's house", Description = "Has large garden", CityId = 2}
                );
        }
    }
}
