using System.Collections.Generic;
using MoviePet.Models.DTOModels;
using MoviePet.Models.ViewModels;
using MoviePet.Repositories.MovieRepository;

namespace MoviePet.Services.MovieServices
{
    public class MovieService : IMovieService {
        private readonly IMovieRepository _repo;
        public MovieService(IMovieRepository repo) {
            _repo = repo;
        }
        // APIDOCS
        public IEnumerable<MovieDTO> GetMovies() {
            return _repo.GetMovies();
        }

        public MovieDTO GetMovieByID(int movieID)
        {
           return _repo.GetMovieByID(movieID);
        }

        public bool CreateMovie(MovieViewModel newMovie)
        {
            return _repo.CreateMovie(newMovie);
        }

        public bool DeleteMovie(int movieID)
        {
            return _repo.DeleteMovie(movieID);
        }

        public bool MovieExistsByID(int movieID)
        {
            return _repo.MovieExistsByID(movieID);
        }

        public MovieDTO UpdateMovie(int movieID, MovieViewModel updatedMovie)
        {
            return _repo.UpdateMovie(movieID, updatedMovie);
        }
    }
}