using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Runtime.CompilerServices;

namespace GameReviewer.Models
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions options) : base(options) 
        {
        }

        public ReviewContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=DESKTOP-EII9684;Database=GameReviewer;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasKey(x => x.Id);
            builder.Entity<AppUser>().HasIndex(x => x.UserName).IsUnique();
            builder.Entity<Review>().HasKey(x => x.Id);
            builder.Entity<Review>().HasKey(c => new { c.UserId, c.GameId });
            builder.Entity<Review>().HasOne(x => x.User).WithMany(b => b.Reviews);
            builder.Entity<Review>().HasOne(x => x.User).WithMany(b => b.Reviews);
            builder.Entity<Game>().HasKey(x => x.Id);
            builder.Entity<Game>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Game>().HasMany(x => x.Reviews).WithOne(b => b.Game);
            builder.Entity<Game>().HasMany(x => x.GameImages).WithOne(b => b.Game);
            builder.Entity<Game>().HasMany(x => x.Categories).WithOne(b => b.Game);
            builder.Entity<Game>().HasMany(x => x.GameImages).WithOne(b => b.Game);
            builder.Entity<Game>().HasOne(x => x.Producer).WithMany(b => b.Games);
            builder.Entity<Game>().HasMany(x => x.GameImages).WithOne(b => b.Game);
            builder.Entity<Producer>().HasKey(x => x.Id);
            builder.Entity<Image>().HasKey(x => x.Id);
            builder.Entity<Image>().HasOne(x => x.Game).WithMany(b => b.GameImages);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}
