using Microsoft.EntityFrameworkCore;
using ShortenedUrlAPIApp.Helper;
using ShortenedUrlAPIApp.Models;


namespace ShortenedUrlAPIApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder
                    .Property(shortenedUrl => shortenedUrl.Code)
                    .HasMaxLength(ShortLinkSettings.Length);

                builder
                    .HasIndex(shortenedUrl => shortenedUrl.Code)
                    .IsUnique();
            });
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    }
}
