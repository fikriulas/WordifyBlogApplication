using System;
using System.Collections.Generic;
using System.Text;
using Wordify.Data.Abstract;
using Wordify.Entity;

namespace Wordify.Data.Concrete.EntityFramework
{
    public class EfContactRepository : EfGenericRepository<Contact>, IContactRepository
    {
        public EfContactRepository(WordifyContext context) : base(context)
        {

        }
        public WordifyContext WordifyContext
        {
            get { return context as WordifyContext; }
        }
    }
}
