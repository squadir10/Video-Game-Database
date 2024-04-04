namespace VideoGameDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // You can configure relationships and database mappings here
        }
    }
}
