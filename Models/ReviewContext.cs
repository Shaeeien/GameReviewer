using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Runtime.CompilerServices;

namespace GameReviewer.Models
{
    public class ReviewContext : DbContext
    {
        public ReviewContext() 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=DESKTOP-EII9684;Database=GameReviewer;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasKey(x => x.Id);
            builder.Entity<AppUser>().HasIndex(x => x.UserName).IsUnique();
            builder.Entity<Review>().HasKey(x => x.Id);
            builder.Entity<Game>().HasKey(x => x.Id);
            builder.Entity<Game>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Review>().HasKey(c => new { c.UserId, c.GameId });
            //sprawdzić te i dopisać nowe
            builder.Entity<Review>().HasOne(x => x.User).WithMany(b => b.Reviews);
            builder.Entity<Game>().HasMany(x => x.Reviews).WithOne(b => b.Game);
            builder.Entity<Game>().HasMany(x => x.GameImages).WithOne(b => b.Game);
            builder.Entity<Game>().HasMany(x => x.Categories).WithOne(b => b.Game);
            builder.Entity<Game>().HasMany(x => x.GameImages).WithOne(b => b.Game);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
