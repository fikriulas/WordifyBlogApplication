using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wordify.Entity;

namespace Wordify.Data.Concrete.EntityFramework
{
    public class WordifyContext : DbContext
    {
        public WordifyContext(DbContextOptions<WordifyContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Settings> Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>()
                .HasKey(pk => new { pk.BlogId, pk.CategoryId }); // tablonun iki tane primary key'i var.
        }
    }
}
