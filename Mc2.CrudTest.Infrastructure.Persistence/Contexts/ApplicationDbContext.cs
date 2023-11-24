using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SampleCRUD;User ID=sa;Password=123;TrustServerCertificate=True;");
        }


        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {

                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);

            }
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {

                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
            return base.SaveChangesAsync();
        }
    }
}
