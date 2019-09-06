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
    public class BlogController : Controller
    {
        public int PageSize = 2; // bir sayfada gözükecek ürün sayısı.
        private IUnitOfWork unitOfWork;
        public BlogController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            var blog = unitOfWork.Blogs.GetAll()
                .Include(i => i.Reviews)
                .Include(i => i.BlogCategories)
                .ThenInclude(i => i.Category)
                .Where(i => i.Id == id)
                .Where(i => i.IsAppproved)
                .Select(i => new BlogDetail()
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description,
                    HtmlContent = i.HtmlContent,
                    DateAdded = i.DateAdded,
                    AuthorId = i.AuthorId,
                    Vote = i.Vote,
                    ImageUrl = i.ImageUrl,
                    Reviews = i.Reviews.Select(b => new Review()
                    {
                        Name = b.Name,
                        Message = b.Message,
                        dateAdded = b.dateAdded,
                        Id = b.Id,
                    }).ToList(),
                    totalReview = i.Reviews.Count(),
                    Categories = i.BlogCategories.Select(c => c.Category).ToList()
                }).FirstOrDefault();
          

            string cat = blog.Categories[0].Name;
            /*
            string[] cats = new string[3] { "Entity Framework", "İnternet", "Programlama Dilleri" };
            Category ckta = new Category();
            ckta.Name = "Programlama Dilleri";
            List<Category> ckt = new List<Category>()
            {
                new Category(){Name="Entity Framework"},
                new Category(){Name="İnternet"},
                new Category(){Name="Programlama Dilleri"},

            };
            */
            var related = unitOfWork.Blogs.GetAll()
                .Include(i => i.Reviews)
                .Include(i => i.BlogCategories)
                .ThenInclude(i => i.Category)
                .Where(i => i.IsAppproved)
                .Select(i => new BlogDetail()
                {
                    Id = i.Id,
                    Title = i.Title,                    
                    DateAdded = i.DateAdded,
                    AuthorId = i.AuthorId,
                    Vote = i.Vote,
                    ImageUrl = i.ImageUrl,                    
                    totalReview = i.Reviews.Count(),
                    Categories = i.BlogCategories.Select(c => c.Category).ToList()
                }).AsQueryable();
            related = related.Include(i => i.Categories)
                .Where(i => i.Categories.Any(a => a.Name == cat)).Take(3).AsQueryable();                
            
            if (blog != null)
            {
                return View(new ForRelatedModel
                {
                    blog = blog,
                    blogs = related
                });
            }
            else
            {
                return View();
                //return bulunamadı hatası.
            }


        }
        public IActionResult List(int id,int page=1)
        {
            var categoryName = unitOfWork.Categories.Get(id);
            ViewBag.catname = categoryName.Name;
            var blogs = unitOfWork.Blogs.GetAll()
                .Include(i => i.Reviews)
                .Include(i => i.BlogCategories)
                .ThenInclude(i => i.Category)                
                .Where(i => i.IsAppproved)
                .Select(i => new BlogDetail()
                {
                    Id = i.Id,
                    Title = i.Title,
                    DateAdded = i.DateAdded,
                    AuthorId = i.AuthorId,
                    Vote = i.Vote,
                    ImageUrl = i.ImageUrl,
                    totalReview = i.Reviews.Count(),
                    Categories = i.BlogCategories.Select(c => c.Category).ToList()
                }).AsQueryable();
            blogs = blogs
                .Include(i => i.Categories)
                .Where(i => i.Categories.Any(b => b.Id == id));


            var count = blogs.Count();
            blogs = blogs.Skip((page - 1) * PageSize).Take(PageSize);
            return View(
                    new BlogsListPagination()
                    {
                        Blogs = blogs,
                        PagingInfo = new BlogListPagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                            TotalItems = count
                        }
                    }
                );

        }
        [HttpPost]
        public IActionResult Review(Review entity)
        {
            if(ModelState.IsValid)
            {                
                entity.dateAdded = DateTime.Now;
                unitOfWork.Reviews.Add(entity);
                unitOfWork.SaveChanges();
                return Ok();
            }
            return BadRequest();
            
        }
    }
}