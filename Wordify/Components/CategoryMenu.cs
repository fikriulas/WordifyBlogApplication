using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wordify.Data.Abstract;

namespace Wordify.WebUI.Components
{
    public class CategoryMenu:ViewComponent
    {
        /*
         * Ana sayfada üst tarafta listelenen category'lerin temsilidir. 
         * */
        private ICategoryRepository _repository;
        public CategoryMenu(ICategoryRepository repo)
        {
            _repository = repo;
        }
        public IViewComponentResult Invoke()
        {            
            return View(_repository.GetAll());
        }
    }
}
