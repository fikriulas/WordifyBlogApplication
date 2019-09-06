using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wordify.Data.Abstract;
using Wordify.Entity;
using Wordify.WebUI.Models;

namespace Wordify.WebUI.Controllers.Back
{
    public class CategoryController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            ViewBag.SuccessSave = TempData["SuccessSave"] ?? null;
            return View(unitOfWork.Categories.GetAll().ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category entity)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Categories.Add(entity);
                unitOfWork.SaveChanges();
                return Ok(entity); // success çalıştır.
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var category = unitOfWork.Categories.Get(Id);
            if(category!=null)
            {
                unitOfWork.Categories.Delete(category);
                unitOfWork.SaveChanges();
                return Ok(Id);
            }
            return BadRequest();
        }        
        public IActionResult Edit(int id)
        {
            var categories = unitOfWork.Categories.GetAll()
                .Where(i => i.Id == id)
                .Include(i => i.BlogCategories)
                .ThenInclude(i => i.Blog)
                .Select(i => new AdminEditCategoryBlog()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Blogs = i.BlogCategories.Select(b => new AdminEditBlog()
                    {
                        Id = b.Blog.Id,
                        Title = b.Blog.Title,
                        IsAppproved = b.Blog.IsAppproved,
                        IsHome = b.Blog.IsHome,
                        IsSlider = b.Blog.IsSlider
                    }).ToList(),
                }).FirstOrDefault();
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category entity)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.Categories.Edit(entity);
                unitOfWork.SaveChanges();
                TempData["SuccessSave"] = "Category Successfully Changed.";
                return RedirectToAction("Index");
            }
            return View(entity);
        }
    }
}