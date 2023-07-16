using AutoMapper;
using Moq;
using MovieAPI.Controllers;
using MovieAPI.Data;
using MovieAPI.Models.Dto;
using MovieAPI.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using MovieAPI.Services.IServices;
using System.Data;

namespace MovieAPI.Tests
{
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _movieService;
        private readonly MovieController _movieController;

        public MovieControllerTests()
        {
            _movieService = new Mock<IMovieService>();
            _movieController = new MovieController(_movieService.Object);
        }
        private List<Movie> GetMoviesData()
        {
            List<Movie> moviesData = new List<Movie>
            {
                new Movie
                {
                    id = 1,
                    title = "Movie 1",
                    description = "Description 1",
                    image = "http://image.com",
                    rating = 9.5
                },
                 new Movie
                {
                    id = 2,
                    title = "Movie 2",
                    description = "Description 2",
                    image = "http://image.com",
                    rating = 8.5
                },
                 new Movie
                {
                    id = 3,
                    title = "Movie 3",
                    description = "Description 3",
                    image = "http://image.com",
                    rating = 7.5
                },
            };
            return moviesData;
        }

        [Fact]
        public void GetMovies_Should_Return_All_Movies()
        {
            //arrange
            var movieList = GetMoviesData();
            _movieService.Setup(x => x.GetMovieList())
                .Returns(movieList);

            //act
            var response = _movieController.GetMovies();

            var result = Assert.IsType<ResponseDto>(response);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.IsAssignableFrom<IEnumerable<Movie>>(result.Result);
        }

        [Fact]
        public void GetMovieById_Should_Return_Single_Movie()
        {
            // Arrange
            int movieId = 1;
            _movieService.Setup(service => service.IsMovieExist(movieId)).Returns(true);
            _movieService.Setup(service => service.GetMovieById(movieId)).Returns(new Movie { id = movieId, title = "Movie 1", rating = 7.5 });

            // Act
            var response = _movieController.GetMovieById(movieId);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.IsType<Movie>(result.Result);
            Assert.Equal(movieId, (result.Result as Movie).id);
        }

        [Fact]
        public void GetMovieById_Should_Return_NotFound_If_Movie_Does_Not_Exist()
        {
            // Arrange
            int movieId = 1;
            _movieService.Setup(service => service.IsMovieExist(movieId)).Returns(false);

            // Act
            var response = _movieController.GetMovieById(movieId);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.False(result.IsSuccess);
            Assert.Equal("Movie id does not exist", result.Message);
        }

        [Fact]
        public void AddMovie_Should_Create_New_Movie()
        {
            // Arrange
            var newMovie = new Movie { id = 1, title = "New Movie", rating = 8.0 };
            _movieService.Setup(service => service.IsMovieExist(newMovie.id)).Returns(false);
            _movieService.Setup(service => service.AddMovie(newMovie)).Returns(newMovie);

            // Act
            var response = _movieController.AddMovie(newMovie);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Equal(newMovie, result.Result);
            Assert.Equal("Movie was created successfully", result.Message);
        }

        [Fact]
        public void AddMovie_Should_Return_BadRequest_If_Movie_Id_Already_Exists()
        {
            // Arrange
            var existingMovie = new Movie { id = 1, title = "Existing Movie", rating = 7.5 };
            _movieService.Setup(service => service.IsMovieExist(existingMovie.id)).Returns(true);

            // Act
            var response = _movieController.AddMovie(existingMovie);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.False(result.IsSuccess);
            Assert.Equal("Movie id already exists", result.Message);
        }

        [Fact]
        public void UpdateMovie_Should_Update_Existing_Movie()
        {
            // Arrange
            int movieId = 1;
            var existingMovie = new Movie { id = movieId, title = "Existing Movie", rating = 7.5 };
            var updatedMovie = new Movie { id = movieId, title = "Updated Movie", rating = 8.0 };
            _movieService.Setup(service => service.IsMovieExist(movieId)).Returns(true);
            _movieService.Setup(service => service.UpdateMovie(updatedMovie)).Returns(updatedMovie);

            // Act
            var response = _movieController.UpdateMovie(updatedMovie);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Equal(updatedMovie, result.Result);
            Assert.Equal($"Movie id {movieId} updated successfully", result.Message);
        }

        [Fact]
        public void UpdateMovie_Should_Return_NotFound_If_Movie_Does_Not_Exist()
        {
            // Arrange
            var nonExistingMovie = new Movie { id = 1, title = "Non-Existing Movie", rating = 7.5 };
            _movieService.Setup(service => service.IsMovieExist(nonExistingMovie.id)).Returns(false);

            // Act
            var response = _movieController.UpdateMovie(nonExistingMovie);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.False(result.IsSuccess);
            Assert.Equal("Movie id does not exist", result.Message);
        }

        [Fact]
        public void DeleteMovie_Should_Delete_Existing_Movie()
        {
            // Arrange
            int movieId = 1;
            _movieService.Setup(service => service.IsMovieExist(movieId)).Returns(true);
            _movieService.Setup(service => service.DeleteMovie(movieId)).Returns(true);

            // Act
            var response = _movieController.DeleteMovie(movieId);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.True(result.IsSuccess);
            Assert.Equal($"Movie id {movieId} deleted successfully", result.Message);
        }

        [Fact]
        public void DeleteMovie_Should_Return_NotFound_If_Movie_Does_Not_Exist()
        {
            // Arrange
            int movieId = 1;
            _movieService.Setup(service => service.IsMovieExist(movieId)).Returns(false);

            // Act
            var response = _movieController.DeleteMovie(movieId);

            // Assert
            var result = Assert.IsType<ResponseDto>(response);
            Assert.False(result.IsSuccess);
            Assert.Equal("Movie id does not exist", result.Message);
        }
    }
}