using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wordify.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "This area is required")]
        [StringLength(25, ErrorMessage = "Must be between 5 and 25 characters", MinimumLength = 4)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This area is required")]
        [StringLength(25, ErrorMessage = "Must be between 5 and 25 characters", MinimumLength = 4)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This area is required")]
        [EmailAddress]
        public string Email { get; set; }        
        [Required(ErrorMessage = "This area is required")]
        [EmailAddress]
        [Compare("Email")]
        [Display(Name = "Email Confirm")]
        public string EmailConfirm { get; set; }
        [Required(ErrorMessage = "This area is required")]
        public string Password { get; set; }        
        [Required(ErrorMessage = "This area is required")]
        [Compare("Password")]
        [Display(Name ="Password Confirm")]
        public string PasswordConfirm { get; set; }
        


    }
}
