using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wordify.Data.Abstract;
using Wordify.Entity;
using Wordify.WebUI.Models;

namespace Wordify.WebUI.Controllers.Front
{
    public class HomeController : Controller
    {
        public int PageSize = 4; // bir sayfada gözükecek ürün sayısı.
        private IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index(int pg = 1)
        {
            var blogs = unitOfWork.Blogs.GetAll()
                .Include(i => i.Reviews)
                .Select(i => new HomeBlog()
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description,
                    DateAdded = i.DateAdded,
                    ImageUrl = i.ImageUrl,
                    Vote = i.Vote,
                    totalReview = i.Reviews.Count(),
                });
            var count = blogs.Count();
            blogs = blogs.Skip((pg - 1) * PageSize).Take(PageSize);

            return View(
                new BlogHomePagination()
                {
                    Blogs = blogs,
                    PagingInfo = new BlogListPagingInfo()
                    {
                        CurrentPage = pg,
                        ItemsPerPage = PageSize,
                        TotalItems = count
                    }
                });
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact entity)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.Contacts.Add(entity);
                unitOfWork.SaveChanges();
                ViewBag.Message = "Your Message Successfully Sended.";
                return View();
            }
            else
            {
                return View(entity);
            }
        }
    }
}