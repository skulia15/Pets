using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviePet.Models.DTOModels;
using MoviePet.Models.EntityModels;
using MoviePet.Models.ViewModels;
using MoviePet.Services;
using Microsoft.AspNetCore.Mvc;


namespace MoviePet.Controllers {
    [Route("api/Movies")]
    public class MovieController : Controller {
        private readonly IMovieService _MovieService;
        private readonly IGenreService _GenreService;
        public MovieController(IMovieService MovieService, IGenreService GenreService) {
            _MovieService = MovieService;
        }
        // GET /api/Movies
        // Get all movies
        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies() {
            var movies = _MovieService.GetMovies();
            if(movies == null){
                return NotFound("No Movies registered in the database");
            }
            return Ok(movies);
        }

        //GET /api/Movies/:movieID
        // Get a movie by ID
        [HttpGet("{movieID?}", Name = "GetMovieByID")]
        public IActionResult GetMovieByID(int movieID) {
            var movie = _MovieService.GetMovieByID(movieID);
            if(movie == null){
                return NotFound("No Movie is registered with the specified ID");
            }
            return Ok(movie);
        }

        // POST /api/Movies/
        // Create a new movie
        [HttpPost(Name = "CreateMovie")]
        public IActionResult CreateMovie([FromBody] MovieViewModel newMovie) {
            // Validate input
            // TODO: sanitize input
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }
            bool success = _MovieService.CreateMovie(newMovie);
            if (!success) {
                return BadRequest("Failed to create movie");
            }

            return CreatedAtRoute("GetMovieByID", newMovie);
        }

        // Delete /api/Movies/:movieID
        // Delete a movie
        //TODO: delete from genre table
        [HttpDelete("{movieID?}", Name = "DeleteMovie")]
        public IActionResult DeleteMovie(int movieID) {
            // Check if the movie exists
            if(!_MovieService.MovieExistsByID(movieID)){
                return NotFound();
            }

            bool success = _MovieService.DeleteMovie(movieID);
            if (!success) {
                return BadRequest("Failed to delete movie");
            }

            return NoContent();
        }

        // PUT /api/Movies/:movieID
        // Update a movie
        [HttpPut("{movieID?}", Name = "UpdateMovie")]
        public IActionResult UpdateMovie(int movieID, [FromBody] MovieViewModel updatedMovie) {
            // Validate
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }
            // Check if the movie exists
            if(!_MovieService.MovieExistsByID(movieID)){
                return NotFound();
            }

            var updatedMovieDTO = _MovieService.UpdateMovie(movieID, updatedMovie);
            if (updatedMovieDTO == null) {
                return BadRequest("Failed to update movie");
            }

            return Ok(updatedMovieDTO);
        }

    }
}