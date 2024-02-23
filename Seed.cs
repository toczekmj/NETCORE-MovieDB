using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bogus.DataSets;
using MovieApi.Data;
using MovieApi.Model;

namespace MovieApi;

public class Seed
{
    private readonly AppDbContext _dbContext;

    public Seed(AppDbContext context)
    {
        this._dbContext = context;
    }

    public void DeleteActors()
    {
        var toDelete = _dbContext.Actors.ToList();
        _dbContext.Actors.RemoveRange(toDelete);
        _dbContext.SaveChanges();
        SeedData();
    }

    public void SeedData()
    {
        var faker = new Bogus.Faker(locale: "pl");
        if (!_dbContext.Actors.Any())
        {
            var actors = new List<Actor>();
            const int amount = 10;

            for (var i = 0; i < amount; i++)
            {
                var movies = new List<Movie>();
                actors.Add(new Actor()
                {
                    Movies = movies,
                    firstName = faker.Name.FirstName(),
                    lastName = faker.Name.LastName(),
                });
            }
            
            
            _dbContext.Actors.AddRange(actors);
            _dbContext.SaveChanges();
        }
        else
        {
            DeleteActors();
        }
    }
}