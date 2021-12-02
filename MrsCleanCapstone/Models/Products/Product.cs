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
        
        [Required]
        [Display(Name = "Product Name")] 
        public string ProductName { get; set; }
        
        [Column (TypeName = "decimal(8,2)")]
        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }
        
        [Required]
        [Display(Name = "Product Category")]
        [StringLength(20)]
        public string Category { get; set; }
        
        [Required]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        
        public string ProductImageName { get; set; }
        
        [Display(Name = "Attach Product Image")]
        public byte[] ProductImage { get; set; }


    }
}
