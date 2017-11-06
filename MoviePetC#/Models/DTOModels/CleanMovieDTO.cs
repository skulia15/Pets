using MoviePet.Models.EntityModels;
using System.Collections.Generic;
using System;
/// <summary>
/// Data Transfer Object for a movie
/// </summary>
namespace MoviePet.Models.DTOModels {
    public class CleanMovieDTO {
        public string title { get; set; }
        public string summary { get; set; }
        public double rating { get; set; }
        public List<GenreDTO> genre { get; set; }
        public string releaseDate { get; set; }

    }
}