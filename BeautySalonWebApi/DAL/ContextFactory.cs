using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BeautySalonWebApi.DAL
{
    public sealed class ContextFactory : IDesignTimeDbContextFactory<CustomerOrdersContext>
    {
        public CustomerOrdersContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<CustomerOrdersContext>();

            builder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("BeautySalonWebApi"));

            return new CustomerOrdersContext(builder.Options);
        }
    }
}
