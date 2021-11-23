using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Address { get; set; }
    }
}
