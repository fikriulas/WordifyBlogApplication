using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wordify.Identity
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("C:/Users/user/Desktop/Çalışmalar/AspnetMvcCore/WordifyBlogApplication/Wordify/appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new ApplicationIdentityDbContext(builder.Options);
        }
    }
}
