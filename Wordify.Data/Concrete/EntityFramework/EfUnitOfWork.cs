using System;
using System.Collections.Generic;
using System.Text;
using Wordify.Data.Abstract;

namespace Wordify.Data.Concrete.EntityFramework
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly WordifyContext dbContext;
        public EfUnitOfWork(WordifyContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException("DbContext can not be null!");
        }

        private ICategoryRepository _categories;
        private IBlogRepository _blogs;
        private IReviewRepository _reviews;
        private IBlogCategoryRepository _BlogCategory;
        private IContactRepository _contact;
        private ISettingsRepository _settings;
        public ICategoryRepository Categories
        {
            get
            {
                return _categories ?? (_categories = new EfCategoryRepository(dbContext));
            }
        }

        public IReviewRepository Reviews
        {
            get
            {
                return _reviews ?? (_reviews = new EfReviewRepository(dbContext));
            }
        }
        public IBlogRepository Blogs
        {
            get
            {
                return _blogs ?? (_blogs = new EfBlogRepository(dbContext));
            }
        }

        public IContactRepository Contacts
        {
            get
            {
                return _contact ?? (_contact = new EfContactRepository(dbContext));
            }
        }

        public IBlogCategoryRepository BlogCategory
        {
            get
            {
                return _BlogCategory ?? (_BlogCategory = new EfBlogCategoryRepository(dbContext));
            }
        }

        public ISettingsRepository Settings
        {
            get
            {
                return _settings ?? (_settings = new EfSettingsRepository(dbContext));
            }
        }

        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
