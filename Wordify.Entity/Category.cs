using System;
using System.Collections.Generic;
using System.Text;

namespace Wordify.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
    }
}
