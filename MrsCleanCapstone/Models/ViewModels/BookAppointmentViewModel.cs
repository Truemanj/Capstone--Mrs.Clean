using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models.ViewModels
{
    public class BookAppointmentViewModel
    {
        public Appointment Appointment { get; set; }

        public Vehicle Vehicle { get; set; }
        public Customer Customer { get; set; }
        public List<string> ServiceTypes { get; set; }
        public List<string> VehicleTypes { get; set; }
    }
}
