using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }

        //override OnModelCreating method to seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                id = 1,
                title = "Barbie",
                description = "Barbie suffers a crisis that leads her to question her world and her existence.",
                rating = 8.3,
                image = "https://www.imdb.com/title/tt1517268/mediaviewer/rm2419599361/?ref_=tt_ov_i"
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                id = 2,
                title = "Oppenheimer",
                description = "The story of American scientist J. Robert Oppenheimer and his role in the development of the atomic bomb.",
                rating = 7.6,
                image = "https://www.imdb.com/title/tt15398776/mediaviewer/rm689777665/?ref_=tt_ov_i"
            });
        }
    }
}
