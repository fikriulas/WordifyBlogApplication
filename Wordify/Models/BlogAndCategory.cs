using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wordify.Entity;

namespace Wordify.WebUI.Models
{
    public class BlogAndCategory
    {
        public List<Category> Categories { get; set; }
        public List<HomeBlog> Blogs { get; set; }
    }
}
