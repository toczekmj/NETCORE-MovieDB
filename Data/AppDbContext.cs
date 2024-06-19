using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Actor> Actors { get; set; } = null!;
    public virtual DbSet<Movie> Movies { get; set; } = null!;
    public virtual DbSet<Rating> Ratings { get; set; } = null!;
    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");   
    }
}