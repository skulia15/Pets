using System;
using System.Collections.Generic;
using MoviePet.Models.DTOModels;
using MoviePet.Models.ViewModels;
/// <summary>
/// Interface for Movieservice, each function in Movieservice must also be here.
/// </summary>
namespace MoviePet.Services {
    public interface IGenreService {
        List<GenreDTO> GetGenreByMovieID(int movieID);
        int getGenreID(string genre);
        bool AddGenreToMovie(int movieID, string[] genres);
        bool DeleteMovieFromGenres(int movieID);
    }
}