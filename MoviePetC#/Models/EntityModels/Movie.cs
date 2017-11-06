using System;
/// <summary>
/// Model Class for the Movie Object
/// </summary>
namespace MoviePet.Models.EntityModels {
    public class Movie {
        // Example 1
        public int movieID { get; set; }
        // Example Titanic
        public string title { get; set; }
        public string summary { get; set; }
        public double rating { get; set; }
        public string releaseDate { get; set; }
    }
}