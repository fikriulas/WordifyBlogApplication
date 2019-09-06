using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wordify.WebUI.Models
{
    public class EditBlogModel
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
        public List<EditCategoryModel> Categories { get; set; }
        public List<EditReviewModel> Reviews { get; set; }
    }
    public class EditCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EditReviewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
