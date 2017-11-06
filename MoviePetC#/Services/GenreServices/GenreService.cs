using System.Collections.Generic;
using MoviePet.Models.DTOModels;
using MoviePet.Models.ViewModels;
using MoviePet.Repositories.GenreRepository;

namespace MoviePet.Services.GenreServices
{
    public class GenreService : IGenreService {
        private readonly IGenreRepository _repo;
        public GenreService(IGenreRepository repo) {
            _repo = repo;
        }

        // APIDOCS
        public bool AddGenreToMovie(int movieID, string[] genres)
        {
            return _repo.AddGenreToMovie(movieID, genres);
        }

        public bool DeleteMovieFromGenres(int movieID)
        {
            return _repo.DeleteMovieFromGenres(movieID);
        }

        public List<GenreDTO> GetGenreByMovieID(int movieID)
        {
            return _repo.GetGenreByMovieID(movieID);
        }

        public int getGenreID(string genre)
        {
            return _repo.getGenreID(genre);
        }

        
    }
}