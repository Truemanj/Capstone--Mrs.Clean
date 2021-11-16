using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.DTOs
{
    public class AppointmentLookupDto
    {
        public String CustomerEmail { get; set; }

        public Guid AppointmentId { get; set; }
    }
}
