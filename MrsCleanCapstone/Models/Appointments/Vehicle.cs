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
        public string ServiceType { get; set; }
        public string Condition{ get; set; }
        public string Type { get; set; }
        public int NumSeats { get; set; }
    }
}
