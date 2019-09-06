using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wordify.Data.Abstract;
using Wordify.Entity;
using Wordify.WebUI.Models;

namespace Wordify.WebUI.Controllers.Back
{
    public class AdminBlogController : Controller
    {
        private IUnitOfWork unitOfWork;
        public AdminBlogController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            var blogs = unitOfWork.Blogs.GetAll()
                .Select(i => new Blog()
                {
                    Id = i.Id,
                    Title = i.Title,
                    IsAppproved = i.IsAppproved,
                    IsHome = i.IsHome,
                    IsSlider = i.IsSlider
                }).ToList();
            ViewBag.SuccessSave = TempData["SuccessSave"] ?? null;
            ViewBag.Hata = TempData["Hata"] ?? null;
            return View(blogs);
        }
        public IActionResult Create()
        {
            var Categories = new List<SelectListItem>();
            foreach (var item in unitOfWork.Categories.GetAll())
            {
                Categories.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Categories = Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog entity, IFormFile mainFile, string[] category)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                if (mainFile != null)
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Templates\\wordify\\images\\", mainFile.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await mainFile.CopyToAsync(stream);
                        entity.ImageUrl = mainFile.FileName;
                        entity.DateAdded = DateTime.Now;
                    }
                }
                unitOfWork.Blogs.Add(entity);
                unitOfWork.SaveChanges();
                List<BlogCategory> blogCategories = new List<BlogCategory>();
                for (int i = 0; i < category.Count(); i++)
                {
                    var categories = new BlogCategory()
                    {
                        Blog = entity,
                        CategoryId = Convert.ToInt32(category[i]),
                    };
                    blogCategories.Add(categories);
                }
                unitOfWork.BlogCategory.AddRange(blogCategories);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }
        public IActionResult Edit(int id)
        {
            var blog = unitOfWork.Blogs.GetAll()
                .Include(i => i.Reviews)
                .Include(i => i.BlogCategories)
                .ThenInclude(i => i.Category)
                .Where(i => i.Id == id)
                .Select(i => new EditBlogModel()
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    IsAppproved = i.IsAppproved,
                    Title = i.Title,
                    Vote = i.Vote,
                    IsSlider = i.IsSlider,
                    IsHome = i.IsHome,
                    DateAdded = i.DateAdded,
                    HtmlContent = i.HtmlContent,
                    Description = i.Description,
                    AuthorId = i.AuthorId,
                    Categories = i.BlogCategories.Select(b => new EditCategoryModel()
                    {
                        Id = b.Category.Id,
                        Name = b.Category.Name
                    }).ToList(),
                    Reviews = i.Reviews.Select(c => new EditReviewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                }).FirstOrDefault();
            if (blog != null)
            {
                var Categories = new List<SelectListItem>();
                foreach (var item in unitOfWork.Categories.GetAll())
                {
                    Categories.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
                ViewBag.SuccessSave = TempData["SuccessSave"] ?? null;
                
                ViewBag.Categories = Categories;
                return View(blog);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity, IFormFile mainFile, string[] category)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                if (mainFile != null)
                {
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Templates\\wordify\\images\\", entity.ImageUrl);
                    System.IO.File.Delete(deletePath);
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Templates\\wordify\\images\\", mainFile.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await mainFile.CopyToAsync(stream);
                        entity.ImageUrl = mainFile.FileName;
                        entity.DateAdded = DateTime.Now;
                    }
                }
                var blog = unitOfWork.Blogs.GetAll()
                    .Include(i => i.BlogCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault(i => i.Id == entity.Id);
                if (category.Length != 0)
                {
                    blog.BlogCategories.Clear();
                    for (int i = 0; i < category.Length; i++)
                    {
                        blog.BlogCategories.Add(new BlogCategory()
                        {
                            BlogId = entity.Id,
                            CategoryId = Convert.ToInt32(category[i]),
                        });
                    }
                }
                blog.Title = entity.Title;
                blog.IsAppproved = entity.IsAppproved;
                blog.IsHome = entity.IsHome;
                blog.IsSlider = entity.IsSlider;
                blog.HtmlContent = entity.HtmlContent;
                blog.Description = entity.Description;
                blog.ImageUrl = entity.ImageUrl;
                unitOfWork.Blogs.Edit(blog);
                unitOfWork.SaveChanges();
                TempData["SuccessSave"] = "Blog Successfully Changed.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(entity);
            }

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var blog = unitOfWork.Blogs.Get(id);
            if (blog != null)
            {
                if (blog.ImageUrl != null)
                {
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Templates\\wordify\\images\\", blog.ImageUrl);
                    System.IO.File.Delete(deletePath);
                }
                unitOfWork.Blogs.Delete(blog);
                unitOfWork.SaveChanges();
                return Ok(blog.Id);
            }
            return BadRequest();
        }
        public IActionResult ReviewDetails(int id)
        {
            var review = unitOfWork.Reviews.GetAll()
                .Where(i => i.Id == id)
                .Include(i => i.Blog)
                .FirstOrDefault();
            if (review != null)
            {
                return View(review);
            }
            else
            {
                return View();
            }

        }
        public IActionResult ReviewDelete(int id)
        {
            var review = unitOfWork.Reviews.GetAll()
               .Where(i => i.Id == id)
               .Include(i => i.Blog)
               .FirstOrDefault();
            if (review != null)
            {
                unitOfWork.Reviews.Delete(review);
                unitOfWork.SaveChanges();
                TempData["SuccessSave"] = "Review Successfully Deleted.";
                return RedirectToAction("Edit", new { id = review.Blog.Id });
            }
            return View(); // not found page.
        }
        [HttpPost]
        public async Task<IActionResult> ReplyReview(string toEmail,string subject, string reply)
        {
            var Result = "";
            var mail = unitOfWork.Settings.Get(1);
            try
            {
                
                var smtpClient = new SmtpClient
                {
                    Host = mail.SmtpAddress, // set your SMTP server name here
                    Port = Convert.ToInt32(mail.Port), // Port 
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail.MailUserName, mail.MailPassword)
                };

                using (var message = new MailMessage(mail.MailUserName, toEmail)
                {
                    Subject = subject,
                    Body = reply
                })
                {
                    await smtpClient.SendMailAsync(message);
                    return Ok();
                }
                
            }            
            catch (Exception ep)
            {
                Result = String.Format("{0} ", ep.Message);
                if(ep.InnerException != null)
                {
                    Result += String.Format("{0}",ep.InnerException.Message);
                }
                return BadRequest(Result.ToString());
            }
            

        }

    }
}