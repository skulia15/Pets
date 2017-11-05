using MoviePet.Models.EntityModels;
using System.Collections.Generic;
/// <summary>
/// Transfers movie information
/// </summary>
namespace MoviePet.Models.DTOModels {
    public class MovieDTO {
        public int movieID { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public double rating { get; set; }
        public List<Genre> genre { get; set; }
        public int year { get; set; }

    }
}