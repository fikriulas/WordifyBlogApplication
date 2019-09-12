using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wordify.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
