using System;
using System.Collections.Generic;
using System.Linq;
using MoviePet.Models.DTOModels;
using MoviePet.Models.EntityModels;
using MoviePet.Models.ViewModels;

/// <summary>
/// This class querys the database as requested by its service class
/// </summary>
namespace MoviePet.Repositories
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
                        //genre = GetGenreByMovieID(m.movieID),
                        year = m.year
                    }).ToList();
            foreach(MovieDTO movie in movies){
                movie.genre = GetGenreByMovieID(movie.movieID);
            }
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
                        genre = GetGenreByMovieID(movieID),
                        year = m.year
                    }).SingleOrDefault();
        }

        public List<Genre> GetGenreByMovieID(int movieID)
        {
            var genres = (from g in _db.Genres
                    join mg in _db.MovieinGenre
                    on g.genreID equals mg.genreID
                    where mg.movieID == movieID
                    select g).ToList();
            return genres;
        }

        public bool CreateMovie(MovieViewModel newMovie)
        {
            // TODO: Check if the movie already exists, what is the criteria?
            // TODO: Check required fields for the movie
            var movie = _db.Movies.Add(new Movie
            {
                title = newMovie.title,
                summary = newMovie.summary,
                //genre = newMovie.genre,
                year = newMovie.year
            });
            
            try
            {
                _db.SaveChanges();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }

            foreach(string genre in newMovie.genre){
                int genreID = getGenreID(genre);
                _db.MovieinGenre.Add(new MovieInGenre{
                    movieID = movie.Entity.movieID,
                    genreID = genreID
                });
            }

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

        public int getGenreID(string genre){
            var id = (from g in _db.Genres
                    where g.genreTitle.ToLower() == genre.ToLower()
                    select g.genreID).SingleOrDefault();
            return id;
        }

        public bool DeleteMovie(int movieID)
        {
            var movieToDelete = (from m in _db.Movies
                                 where m.movieID == movieID
                                 select m).SingleOrDefault();
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

        public MovieDTO UpdateMovie(int movieID, MovieViewModel updatedMovie)
        {
            // Fetch the movie to update
            var movie = GetMovieByID(movieID);
            // Update its values with the provided information
            movie.title = updatedMovie.title;
            //movie.genre = updatedMovie.genre;
            movie.summary = updatedMovie.summary;
            movie.year = updatedMovie.year;

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
                title = movie.title,
                //genre = movie.genre,
                year = movie.year,
                rating = movie.rating,
                summary = movie.summary
            };
        }
    }
}