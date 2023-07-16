using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Services.IServices
{
    public interface IMovieService
    {
        public IEnumerable<Movie> GetMovieList();
        public Movie GetMovieById(int id);
        public Movie AddMovie(Movie movie);
        public Movie UpdateMovie(Movie movie);
        public bool DeleteMovie(int id);
        public bool IsMovieExist(int id);
    }
}
