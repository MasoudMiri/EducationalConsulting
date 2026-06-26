using Microsoft.EntityFrameworkCore;
using EducationalConsulting.Models;

namespace EducationalConsulting.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "عمومی", IsActive = true, CreateDate = DateTime.Now },
                new Category { Id = 2, Name = "ریاضی", IsActive = true, CreateDate = DateTime.Now },
                new Category { Id = 3, Name = "پزشکی", IsActive = true, CreateDate = DateTime.Now },
                new Category { Id = 4, Name = "انسانی", IsActive = true, CreateDate = DateTime.Now }
            );
        }
    }
}