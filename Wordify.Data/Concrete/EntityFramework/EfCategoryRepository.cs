using System;
using System.Collections.Generic;
using System.Text;
using Wordify.Data.Abstract;
using Wordify.Entity;

namespace Wordify.Data.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryRepository    //ICategoryRepository
    {
        public EfCategoryRepository(WordifyContext context) : base(context)
        {

        }
        public WordifyContext WordifyContext
        {
            get { return context as WordifyContext; }
        }
        

    }
}
