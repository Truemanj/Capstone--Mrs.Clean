using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MrsCleanCapstone.Models
{
    public class Product
    {
        [Key] 
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        
        [Column (TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string description { get; set; }

        public string ProductImageName { get; set; }

        [NotMapped]
        public IFormFile ProductImage { get; set; }


    }
}
