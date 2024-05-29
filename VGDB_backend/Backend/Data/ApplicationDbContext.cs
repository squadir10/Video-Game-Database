using Microsoft.EntityFrameworkCore;
using VideoGameDatabase.Models;

namespace VideoGameDatabase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(g => g.GameReviews)
                .WithOne(gr => gr.Game)
                .HasForeignKey(gr => gr.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameReview>()
                .HasOne(gr => gr.Reviewer)
                .WithMany()
                .HasForeignKey(gr => gr.ReviewerID)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
