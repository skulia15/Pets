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
namespace MoviePet.Repositories.GenreRepository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDataContext _db;

        public List<GenreDTO> GetGenreByMovieID(int movieID)
        {
            var genres = (from g in _db.Genres
                          join mg in _db.MovieinGenre
                          on g.genreID equals mg.genreID
                          where mg.movieID == movieID
                          select new GenreDTO()
                          {
                              genreTitle = g.genreTitle
                          }).ToList();
            return genres;
        }

        public int getGenreID(string genre)
        {
            var id = (from g in _db.Genres
                      where g.genreTitle.ToLower() == genre.ToLower()
                      select g.genreID).SingleOrDefault();
            return id;
        }
        public bool AddGenreToMovie(int movieID, string[] genres)
        {
            foreach (string genre in genres)
            {
                int genreID = getGenreID(genre);
                _db.MovieinGenre.Add(new MovieInGenre
                {
                    movieID = movieID,
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

        public bool DeleteMovieFromGenres(int movieID)
        {
            // Get the movie-genre relation for all genres the movie has
            var movieInGenreToDelete = (from mig in _db.MovieinGenre
                                        where mig.movieID == movieID
                                        select mig).ToList();
            try
            {
                // Remove all relations for movie to delete
                foreach (MovieInGenre movieInGenre in movieInGenreToDelete)
                {
                    _db.MovieinGenre.Remove(movieInGenre);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

    }
}