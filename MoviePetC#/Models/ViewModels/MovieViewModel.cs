/// <summary>
/// Handles information given by the user
/// </summary>
namespace MoviePet.Models.ViewModels
{
    public class MovieViewModel
    {   
        public string title { get; set; }
        public string summary { get; set; }
        public string [] genre { get; set; }
        public int year { get; set; }
    }
}