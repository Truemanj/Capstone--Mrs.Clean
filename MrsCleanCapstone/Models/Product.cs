using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        public string ProductImageName { get; set; }

        [NotMapped]
        public IFormFile ProductImage { get; set; }
    }
}
