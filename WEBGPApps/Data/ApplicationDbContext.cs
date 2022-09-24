using WEBGPApps.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WEBGPApps.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<DC> DC { get; set; }
        public DbSet<SC> SC { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Att> Att { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}