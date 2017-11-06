using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using MoviePet.Models.DTOModels;
using MoviePet.Models.EntityModels;
using MoviePet.Models.ViewModels;

/// <summary>
/// This class querys the database as requested by its service class
/// </summary>
namespace MoviePet.Repositories.MovieRepository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDataContext _db;

        public MovieRepository(AppDataContext db)
        {
            _db = db;

        }
        // APIDOCS
        public IEnumerable<MovieDTO> GetMovies()
        {
            var movies = (from m in _db.Movies
                          select new MovieDTO
                          {
                              movieID = m.movieID,
                              title = m.title,
                              summary = m.summary,
                              rating = m.rating,
                              releaseDate = m.releaseDate
                          }).ToList();
            // foreach (MovieDTO movie in movies)
            // {
            //     movie.genre = GetGenreByMovieID(movie.movieID);
            // }
            return movies;
        }


        public MovieDTO GetMovieByID(int movieID)
        {
            return (from m in _db.Movies
                    where m.movieID == movieID
                    select new MovieDTO
                    {
                        movieID = m.movieID,
                        title = m.title,
                        summary = m.summary,
                        rating = m.rating,
                        //genre = GetGenreByMovieID(movieID),
                        releaseDate = m.releaseDate
                    }).SingleOrDefault();
        }

        public Movie GetMovieObjectByID(int movieID)
        {
            return (from m in _db.Movies
                    where m.movieID == movieID
                    select m).SingleOrDefault();
        }

        public bool CreateMovie(MovieViewModel newMovie)
        {
            // TODO: Check if the movie already exists, what is the criteria?
            // TODO: Check required fields for the movie
            // For using TitleCase
            var textInfo = new CultureInfo("en-US").TextInfo;
            var movie = _db.Movies.Add(new Movie
            {
                title = textInfo.ToTitleCase(newMovie.title),
                summary = newMovie.summary,
                // Parse the provided date string to a DateTime Object
                // Then Standardize the input
                releaseDate = DateTime.Parse(newMovie.releaseDate).ToString("yyyy-MM-dd")
            });

            // Save changes to database here so that the movie object gets an ID
            try
            {
                _db.SaveChanges();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
            return true;
        }


        public bool DeleteMovie(int movieID)
        {
            // Find the move in the database
            var movieToDelete = GetMovieObjectByID(movieID);
            try
            {
                _db.Movies.Remove(movieToDelete);
                _db.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        // Checks if movie exists
        public bool MovieExistsByID(int movieID)
        {
            var movie = (from m in _db.Movies
                         where m.movieID == movieID
                         select m).SingleOrDefault();
            if (movie != null)
            {
                return true;
            }
            return false;
        }

        // Updates a movie with the provided information
        public MovieDTO UpdateMovie(int movieID, MovieViewModel updatedMovie)
        {
            // Fetch the movie to update
            var movie = GetMovieObjectByID(movieID);
            // Update its values with the provided information
            if (updatedMovie.title != null)
            {
                var textInfo = new CultureInfo("en-US").TextInfo;
                movie.title = textInfo.ToTitleCase(updatedMovie.title);
            }
            if (updatedMovie.summary != null)
            {
                movie.summary = updatedMovie.summary;
            }
            if (updatedMovie.releaseDate != null)
            {
                // Parse the provided date string to a DateTime Object
                // Then Standardize the input
                movie.releaseDate = DateTime.Parse(updatedMovie.releaseDate).ToString("yyyy-MM-dd");
            }
            if (updatedMovie.genre != null)
            {
                // Remove previous set of genres
                // if(!DeleteMovieFromGenres(movieID)){
                //     Console.WriteLine("Failed removing genre to movie");
                //     return null;
                // }
                // Add the provided genres to the movie
                // if (!AddGenreToMovie(movieID, updatedMovie.genre))
                // {
                //     Console.WriteLine("Failed adding genre to movie");
                //     return null;
                // }
            }
            try
            {
                _db.SaveChanges();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return null;
            }

            return new MovieDTO()
            {
                movieID = movie.movieID,
                title = movie.title,
                //genre = GetGenreByMovieID(movieID),
                releaseDate = movie.releaseDate,
                rating = movie.rating,
                summary = movie.summary
            };
        }
    }
}