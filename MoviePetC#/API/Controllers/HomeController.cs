using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviePet.Models.DTOModels;
using MoviePet.Models.EntityModels;
using MoviePet.Models.ViewModels;
using MoviePet.Services;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// MoviesController handles requests and sends them to its the service class
/// </summary>
namespace MoviePet.Controllers {
    [Route("api")]
    public class HomeController : Controller {
        // public HomeController() {
        // }
        //GET /api/Movies
        //[HttpGet("{semester?}")]
        [HttpGet()]
        public IActionResult Index() {
            var welcome = "This is the Home page";
            return Ok(welcome);
        }
    }
}