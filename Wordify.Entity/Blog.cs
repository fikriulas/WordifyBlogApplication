using System;
using System.Collections.Generic;
using System.Text;

namespace Wordify.Entity
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public short Vote { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public bool IsAppproved { get; set; }
        public bool IsHome { get; set; }
        public bool IsSlider { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
