using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wordify.WebUI.Models
{
    public class BlogsListPagination
    {
        public IEnumerable<BlogDetail> Blogs { get; set; }
        public BlogListPagingInfo PagingInfo { get; set; }
    }
    public class BlogHomePagination
    {
        public IEnumerable<HomeBlog> Blogs { get; set; }
        public BlogListPagingInfo PagingInfo { get; set; }
    }
    public class BlogListPagingInfo
    {
        public int TotalItems { get; set; } // kaç ürün var
        public int ItemsPerPage { get; set; } // kaç ürün gelecek
        public int CurrentPage { get; set; } // hangi sayfadayız.

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
    public class BlogHomePagingInfo
    {
        public int TotalItems { get; set; } // kaç ürün var
        public int ItemsPerPage { get; set; } // kaç ürün gelecek
        public int CurrentPage { get; set; } // hangi sayfadayız.

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
