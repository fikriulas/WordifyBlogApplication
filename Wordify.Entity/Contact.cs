using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wordify.Entity
{
    public class Contact
    {
        public int Id { get; set; }       
        [Required(ErrorMessage ="This area is required")]
        [MaxLength(21,ErrorMessage = "Name field must be a maximum of 21 characters.")]
        [MinLength(5,ErrorMessage = "Name field must be a min of 5 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This area is required")]
        [EmailAddress(ErrorMessage ="E-Mail Address is not Valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This area is required")]
        [StringLength(300, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 10)]
        public string Message { get; set; }
    }
}
