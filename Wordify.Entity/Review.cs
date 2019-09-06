using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wordify.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime dateAdded { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
