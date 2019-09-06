using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wordify.Data.Abstract;
using Wordify.Entity;

namespace Wordify.WebUI.Controllers.Back
{
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;
        public AdminController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }        
        public IActionResult Settings()
        {
            return View(unitOfWork.Settings.Get(1));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(Settings entity)
        {
            if(ModelState.IsValid)
            {                
                unitOfWork.Settings.Edit(entity);
                unitOfWork.SaveChanges();
                ViewData["Success"] = "Settings are succesfully changed.";
                return View();
            }
            return View(entity);
        }
    }
    
}