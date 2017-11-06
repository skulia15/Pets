using System;
/// <summary>
/// View Model for the Movie Object
/// </summary>
namespace MoviePet.Models.ViewModels
{
    public class MovieViewModel
    {   
        public string title { get; set; }
        public string summary { get; set; }
        public string [] genre { get; set; }
        public string releaseDate { get; set; }
    }
}