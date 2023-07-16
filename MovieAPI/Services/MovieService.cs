using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Services.IServices;
using System.Data.Common;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _dbContext;

        public MovieService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Movie> GetMovieList()
        {
            return _dbContext.Movies.ToList();
        }
        public Movie GetMovieById(int id)
        {
            return _dbContext.Movies.First(u => u.id == id);
        }

        public Movie AddMovie(Movie movie)
        {
            var result = _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public Movie UpdateMovie(Movie movie)
        {
            var result = _dbContext.Movies.Update(movie);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public bool DeleteMovie(int id)
        {
            var filteredData = _dbContext.Movies.First(u => u.id == id);
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

        public bool IsMovieExist(int id)
        {
            return _dbContext.Movies.Any(x => x.id == id);
        }
    }
}
