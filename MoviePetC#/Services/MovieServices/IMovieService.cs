using System;
using System.Collections.Generic;
using MoviePet.Models.DTOModels;
using MoviePet.Models.ViewModels;
/// <summary>
/// Interface for Movieservice, each function in Movieservice must also be here.
/// </summary>
namespace MoviePet.Services {
    public interface IMovieService {
        IEnumerable<MovieDTO> GetMovies();
        MovieDTO GetMovieByID(int movieID);
        bool CreateMovie(MovieViewModel newMovie);
        bool DeleteMovie(int movieID);
        bool MovieExistsByID(int movieID);
        MovieDTO UpdateMovie(int movieID, MovieViewModel updatedMovie);
    }
}