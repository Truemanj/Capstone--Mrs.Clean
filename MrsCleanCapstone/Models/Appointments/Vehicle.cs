using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ServiceType { get; set; }
        public string Condition{ get; set; }
        
        [Required]
        public string Type { get; set; }
        
        [Required]
        [Range(2, 8, ErrorMessage = "Number of seats must be in the range of 2 to 8 (select 8 for truck)")]
        public int NumSeats { get; set; }
    }
}
