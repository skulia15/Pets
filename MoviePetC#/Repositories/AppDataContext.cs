using MoviePet.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// provides appropriate database for each of the repository classes
/// </summary>
namespace MoviePet.Repositories {
    public class AppDataContext : DbContext {
        public AppDataContext (DbContextOptions<AppDataContext> options) : base (options) {}

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieInGenre> MovieinGenre { get; set; }
    }
}