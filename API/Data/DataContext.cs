using API.Entities;
using API.Entities.StoredProcedureEntities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopMostRating>().HasNoKey();
            modelBuilder.Entity<TopMostScreening>().HasNoKey();
            modelBuilder.Entity<TopMostSoldTicket>().HasNoKey();
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<TopMostRating> TopMostRatings { get; set; }
        public DbSet<TopMostScreening> TopMostScreenings { get; set; }
        public DbSet<TopMostSoldTicket> TopMostSoldTickets { get; set; }
    }
}