using System.Collections.Generic;
using MoviePet.Models.DTOModels;
using MoviePet.Models.ViewModels;
/// <summary>
/// movie repo interface, each function in the movieRepo must also be here.
/// </summary>
namespace MoviePet.Repositories.GenreRepository {
    public interface IGenreRepository {
        List<GenreDTO> GetGenreByMovieID(int movieID);
        int getGenreID(string genre);
        bool AddGenreToMovie(int movieID, string[] genres);
        bool DeleteMovieFromGenres(int movieID);
    }
}