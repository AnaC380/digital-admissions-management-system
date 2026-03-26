using DAMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAMS.Infrastructure.Persistence
{
    public class DamsDbContext : DbContext
    {
        public DamsDbContext(DbContextOptions<DamsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admission> Admissions => Set<Admission>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Document> Documents => Set<Document>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DamsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}