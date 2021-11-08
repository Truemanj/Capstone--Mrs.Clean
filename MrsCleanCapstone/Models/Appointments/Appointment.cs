using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Date { get; set; }
        public bool AnyPetHair { get; set; }
        public bool WaterHoseAvailability { get; set; }
        public bool WaterSupplyConnection { get; set; }
        public bool PowerOutletAvailable { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public Customer Customerfk { get; set; }
    }
}
