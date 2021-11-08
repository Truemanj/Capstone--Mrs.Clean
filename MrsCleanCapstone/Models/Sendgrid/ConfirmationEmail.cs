using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    public class ConfirmationEmail
    {
        [JsonProperty("order")]
        public Guid Order { get; set; }
    }
}
