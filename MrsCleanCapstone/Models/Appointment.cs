using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public Customer Customerfk { get; set; }
    }
}
