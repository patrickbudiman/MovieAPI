using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Models.Dto;
using MovieAPI.Services.IServices;
using System.Data;

namespace MovieAPI.Controllers
{
    [Route("Movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private ResponseDto _response;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
            _response = new ResponseDto();
        }

        [HttpGet()]
        public ResponseDto GetMovies()
        {
            try
            {
                IEnumerable<Movie> objList = _movieService.GetMovieList();
                _response.Result = objList;
            }
            catch (DataException ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("{id}")]
        public ResponseDto GetMovieById(int id)
        {
            try
            {
                //check exist
                if (!_movieService.IsMovieExist(id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Movie id does not exist";
                    return _response;
                }
                Movie movie = _movieService.GetMovieById(id);
                _response.Result = movie;
            }
            catch (DataException ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto AddMovie(Movie movie)
        {
            try
            {
                //check exist
                if (movie.id > 0 && _movieService.IsMovieExist(movie.id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Movie id already exists";
                    return _response;
                }
                movie.created_at = DateTime.Now.ToUniversalTime();
                _response.Result = _movieService.AddMovie(movie);
                _response.Message = $"Movie was created successfully";
            }
            catch (DataException ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPatch("{id}")]
        public ResponseDto UpdateMovie(Movie movie)
        {
            try
            {
                //check exist
                if (!_movieService.IsMovieExist(movie.id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Movie id does not exist";
                    return _response;
                }
                movie.updated_at = DateTime.Now.ToUniversalTime();
                _response.Result = _movieService.UpdateMovie(movie);
                _response.Message = $"Movie id {movie.id} updated successfully";
            }
            catch (DataException ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public ResponseDto DeleteMovie(int id)
        {
            try
            {
                //check exist
                if (!_movieService.IsMovieExist(id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Movie id does not exist";
                    return _response;
                }
                _response.IsSuccess = _movieService.DeleteMovie(id);
                _response.Message = $"Movie id {id} deleted successfully";
            }
            catch (DataException ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
