using System;
using System.Collections.Generic;
using System.Text;

namespace Wordify.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogRepository Blogs { get; }
        ICategoryRepository Categories { get; }        
        IReviewRepository Reviews { get; }        
        IContactRepository Contacts { get; }
        IBlogCategoryRepository BlogCategory { get; }
        ISettingsRepository Settings { get; }
        int SaveChanges();
    }
}
