using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class MoviesServiceTests
    {
        AppDbContext ctx;

        MoviesService moviesService;

        [SetUp]
        public void Setup()
        {
            moviesService = new MoviesService(ctx);
        }

        [Test]
        public void FilterMoviesByName_Works()
        {

            // Arrange - define variables
            var name = "Ghost";
            var movies = new List<Movie>()
            {
                new Movie
                {
                    Name = "Ghost",
                    Description = "This is the Ghost movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    CinemaId = 4,
                    ProducerId = 4,
                    MovieCategory = MovieCategory.Horror
                },
                new Movie
                {
                    Name = "Scoob",
                    Description = "This is the Scoob movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-2),
                    CinemaId = 1,
                    ProducerId = 3,
                    MovieCategory = MovieCategory.Cartoon
                }
            };

            // Act - call the method

            var getMovies = moviesService.FilterMoviesByName(name, movies);
          

            // Assert - run the assert statements

            Assert.That(getMovies, Is.EqualTo(movies.Where(m => m.Name == name)));

        }

        [Test]
        public void FilterMoviesById_Works()
        {
            // Arrange 
            string name = "Ghost";

            var movies = new List<Movie>()
            {
                new Movie
                {
                    Name = "Ghost",
                    Description = "This is the Ghost movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    CinemaId = 4,
                    ProducerId = 4,
                    MovieCategory = MovieCategory.Horror
                },
                new Movie
                {
                    Name = "Scoob",
                    Description = "This is the Scoob movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-2),
                    CinemaId = 1,
                    ProducerId = 3,
                    MovieCategory = MovieCategory.Cartoon
                }
            };
            // Act 
            var filteredMovies = moviesService.FilterMoviesByName(name, movies);

            // Assert
            Assert.That(filteredMovies, Is.Not.Null);
        }

        [Test]
        public void FilterMoviesByCategory_Works()
        {
            // Arrange 

            int category = 6;

            var movies = new List<Movie>()
            {
                new Movie
                {
                    Name = "Ghost",
                    Description = "This is the Ghost movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    CinemaId = 4,
                    ProducerId = 4,
                    MovieCategory = MovieCategory.Horror
                },
                new Movie
                {
                    Name = "Scoob",
                    Description = "This is the Scoob movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-2),
                    CinemaId = 1,
                    ProducerId = 3,
                    MovieCategory = MovieCategory.Cartoon
                }
            };

            // Act 

            var filteredMovies = moviesService.FilterMoviesByCategory(category, movies);

            // Assert

            Assert.That(filteredMovies, Is.EqualTo(movies.Where(m => (int)m.MovieCategory == category)));
        }
        
    }
}
