using System;
using System.Collections.Generic;
using System.Text;

namespace Wordify.Entity
{
    public class BlogCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
