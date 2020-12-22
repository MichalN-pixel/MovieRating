using Microsoft.EntityFrameworkCore;
using MovieRating.Models;

namespace MovieRating.DAL
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
        }

        public DbSet<MovieRatingModel> MovieRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRatingModel>()
                .ToTable("MovieRating")
                .Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}

