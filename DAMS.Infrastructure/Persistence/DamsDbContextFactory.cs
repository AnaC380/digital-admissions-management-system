using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAMS.Infrastructure.Persistence
{
    public class DamsDbContextFactory : IDesignTimeDbContextFactory<DamsDbContext>
    {
        public DamsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DAMS.Api"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DamsDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DamsDbContext(optionsBuilder.Options);
        }
    }
}
