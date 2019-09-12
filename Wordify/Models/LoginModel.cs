using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wordify.WebUI.Models
{
    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        [Display(Name="E-Mail")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")] // kullanıcıya gösterilmez.
        public string Password { get; set; }
    }
}
