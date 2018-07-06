using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Entities
{
    public class City
    {
        [Key] // annotation that marks as primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // generates primary key
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
            = new List<PointOfInterest>(); // initialize as empty list to avoid null references
          
    }
}
