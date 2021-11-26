using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.DTOs
{
    public class AppointmentLookupDto
    {
        [DisplayName("Email")]
        public String CustomerEmail { get; set; }

        [DisplayName("Enter Appointment ID")]
        public Guid AppointmentId { get; set; }
    }
}
