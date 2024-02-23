using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Actor> Actors { get; set; } = null!;
    public virtual DbSet<Movie> Movies { get; set; } = null!;
    public virtual DbSet<Rating> Ratings { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     
    // }

    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connectionString = "server=localhost;user=root;password=password;database=EFMovies";
    //     var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
    //     optionsBuilder.UseMySql(connectionString, serverVersion);
    // }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<MovieModel>()
    //         .HasMany(e => e.actors)
    //         .WithMany(e => e.movies)
    //         .UsingEntity(
    //             "movies_acted",
    //             l => l.HasOne(typeof(ActorModel)).WithMany().HasForeignKey())
    // }
}