using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wordify.Data.Abstract;
using Wordify.WebUI.Models;

namespace Wordify.WebUI.Components
{
    public class Sidebar:ViewComponent
    {
        /*
         * ana sayfada sağ taraftaki sidebarı temsil eder.
         * burada 3 adet blog ve tüm kategoriler listeleneceği için blogandcategory modeli
         * kullanılmıştır. 
         * */
        private IUnitOfWork unitOfWork;
        public Sidebar(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            // vote'a göre ilk 3 bloğu alır. 
            var blogs = unitOfWork.Blogs.GetAll()
                .OrderBy(i=>i.Vote).Take(3)
                .Include(i => i.Reviews)
                .Select(i => new HomeBlog()
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description,
                    DateAdded = i.DateAdded,
                    Vote = i.Vote,
                    ImageUrl = i.ImageUrl,
                    totalReview = i.Reviews.Count(),
                }).ToList();
            var categories = unitOfWork.Categories.GetAll().ToList(); // kategorilerin hepsini alır.

            return View(
                    new BlogAndCategory()
                    {
                         Blogs = blogs,
                         Categories = categories
                    }
                );
        }
    }
}
