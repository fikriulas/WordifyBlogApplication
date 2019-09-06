using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wordify.Entity;

namespace Wordify.Data.Concrete.EntityFramework
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) //Configure/IApplicationBuilder için.
        {
            var context = app.ApplicationServices.GetRequiredService<WordifyContext>();
            context.Database.Migrate(); // otomatik migration yapar.
            if (!context.Blogs.Any()) // daha önceden bu işlem yapılmış mı?
            {
                var blogs = new[]
                {
                    new Blog(){Title="Lorem ipsum dolor sit amet",IsHome=true,IsAppproved=true,IsSlider=true,Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, nec dignissim justo.",HtmlContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, <b>nec dignissim justo.</b>",DateAdded=DateTime.Now.AddDays(-10)},
                    new Blog(){Title="Maecenas suscipit sapien quis molestie placerat",IsHome=true,IsAppproved=true,IsSlider=true,Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, nec dignissim justo.",HtmlContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, <b>nec dignissim justo.</b>",DateAdded=DateTime.Now.AddDays(-8)},
                    new Blog(){Title="Lorem ipsum dolor sit amet",IsHome=true,IsAppproved=true,IsSlider=true,Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, nec dignissim justo.",HtmlContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, <b>nec dignissim justo.</b>",DateAdded=DateTime.Now.AddDays(-12)},
                    new Blog(){Title="Lorem ipsum dolor sit amet",IsHome=true,IsAppproved=true,IsSlider=true,Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, nec dignissim justo.",HtmlContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, <b>nec dignissim justo.</b>",DateAdded=DateTime.Now.AddDays(-120)},
                    new Blog(){Title="Lorem ipsum dolor sit amet",IsHome=true,IsAppproved=true,IsSlider=true,Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, nec dignissim justo.",HtmlContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu consequat magna, <b>nec dignissim justo.</b>",DateAdded=DateTime.Now.AddDays(-32)},
                };
                context.Blogs.AddRange(blogs);                

                var categories = new[]
                {
                    new Category(){Name="Siber Güvenlik"},
                    new Category(){Name="Entity Framework"},
                    new Category(){Name="İnternet"},
                    new Category(){Name="Programlama Dilleri"},
                };
                context.Categories.AddRange(categories);

                var blogcategory = new[]
                {
                    new BlogCategory(){Blog=blogs[0],Category=categories[0]},
                    new BlogCategory(){Blog=blogs[1],Category=categories[1]},
                    new BlogCategory(){Blog=blogs[2],Category=categories[2]},
                    new BlogCategory(){Blog=blogs[3],Category=categories[3]},
                    new BlogCategory(){Blog=blogs[4],Category=categories[3]},
                };
                context.AddRange(blogcategory);

                var reviews = new[]
                {
                    new Review(){Blog = blogs[0],Name="Emine Erdoğan",Message="Nice Blog",Email="emine@yahoo.com"},
                    new Review(){Blog = blogs[1],Name="Demir Yükselen",Message="This case really interesting!",Email="demir@gmail.com"},
                    new Review(){Blog = blogs[2],Name="Ayşe Yücedağ",Message="Thanks for this blog.",Email="ayse@yahoo.com"},
                    new Review(){Blog = blogs[3],Name="Melih Güngörmüş",Message="Can you contact me ? Please.",Email="melih@yandex.com"},
                };
                context.AddRange(reviews);
                context.SaveChanges();

            }
        }
    }
}
