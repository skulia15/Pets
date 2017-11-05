/// <summary>
/// Information about each movie
/// </summary>
namespace MoviePet.Models.EntityModels {
    public class Movie {
        // Example 1
        public int movieID { get; set; }
        // Example Titanic
        public string title { get; set; }
        public string summary { get; set; }
        public double rating { get; set; }
        //public int genreID { get; set; }
        public int year { get; set; }
    }
}