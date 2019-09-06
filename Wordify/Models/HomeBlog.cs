using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wordify.Entity;

namespace Wordify.WebUI.Models
{
    public class HomeBlog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public short Vote { get; set; }
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public int totalReview { get; set; }
        public string ImageUrl { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class BlogDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public short Vote { get; set; }
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAppproved { get; set; }
        public int totalReview { get; set; }
        public List<Category> Categories { get; set; }
        public List<Review> Reviews { get; set; }
    }
    public class ForRelatedModel
    {
        public IEnumerable<BlogDetail> blogs { get; set; }
        public BlogDetail blog { get; set; }
        public Review review { get; set; }
    }
    
}
