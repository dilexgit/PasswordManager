using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using PasswordManagerMVC.Models;

namespace PasswordManagerMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") 
        {
        }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Passwords)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}