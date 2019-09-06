using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wordify.WebUI.Models
{
    public class AdminEditCategoryBlog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AdminEditBlog> Blogs { get; set; }
    }
    public class AdminEditBlog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAppproved { get; set; }
        public bool IsHome { get; set; }
        public bool IsSlider { get; set; }
    }
}
