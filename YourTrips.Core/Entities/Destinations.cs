using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Core.Entities
{
    public class Destinations
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string NameCountry { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
