using System.Collections.Generic;
using MoviePet.Models.DTOModels;
using MoviePet.Models.EntityModels;
using MoviePet.Models.ViewModels;
/// <summary>
/// movie repo interface, each function in the movieRepo must also be here.
/// </summary>
namespace MoviePet.Repositories.MovieRepository
{
    public interface IMovieRepository {
        IEnumerable<MovieDTO> GetMovies();
        MovieDTO GetMovieByID(int movieID);
        Movie GetMovieObjectByID(int movieID);
        bool CreateMovie(MovieViewModel newMovie);
        bool DeleteMovie(int movieID);
        bool MovieExistsByID(int movieID);
        MovieDTO UpdateMovie(int movieID, MovieViewModel updatedMovie);
    }
}